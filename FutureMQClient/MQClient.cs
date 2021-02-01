using FutureMQClient.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FutureMQClient
{
    public class MQClient
    {

        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string workQueueName;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly IBasicProperties props;
        public delegate void CallBackEventHandler(RequestClass msg);
        public event CallBackEventHandler OnReceiveMsg;
        public static object _lock = new object();
        public static object cancleLock = new object();
        public static object hqLock = new object();

        public delegate void FutureHQEventHandler(FutureHQ hq);
        public event FutureHQEventHandler OnReceiveHQ;

        public delegate void ErrorEventHandler(string ex);
        public event ErrorEventHandler OnError;

        public delegate void PositionEventHandler(List<FuturepositionQry_Output> pos);
        public event PositionEventHandler OnPostion;

        public object _lockPos = new object();

        public MQClient()
        {
            Init();
            var factory = new ConnectionFactory();
            factory.UserName = G.CONFIG.UserName;
            factory.Password = G.CONFIG.Password;
            factory.HostName = G.CONFIG.HostName;
            factory.AutomaticRecoveryEnabled = true;
            //factory.RequestedHeartbeat = 0;

            connection = factory.CreateConnection();

            FutureHQ();

            channel = connection.CreateModel();
            channel.BasicQos(0, 1, false);
            channel.ConfirmSelect();
            workQueueName = G.CONFIG.WorkQueueName;
            replyQueueName = G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName");
            channel.QueueDeclare(queue: replyQueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            #region 发送消息确认
            //没有找到work队列
            channel.BasicReturn += (sender, ea) =>
            {
                RequestClass msg = new RequestClass() { error_info = "没有找到工作队列" };
                //CallBackMsg msg = new CallBackMsg() { ErrorMsg = "没有找到工作队列" };
                OnReceiveMsg(msg);
            };
            //work队列已收到消息，消息被确认
            channel.BasicAcks += (sender, ea) =>
            {

            };
            //Erlang进程中发生内部错误，无法处理消息
            channel.BasicNacks += (sender, ea) =>
            {
                //CallBackMsg msg = new CallBackMsg() { ErrorMsg = "BasicNacks,进程中发生内部错误，无法处理消息" };
                RequestClass msg = new RequestClass() { error_info = "BasicNacks,进程中发生内部错误，无法处理消息" };
                OnReceiveMsg(msg);
            };
            #endregion

            #region 接收服务端返回的消息结果
            consumer = new EventingBasicConsumer(channel);
            props = channel.CreateBasicProperties();
            props.ReplyTo = replyQueueName;
            props.Expiration = "30000";

            consumer.Received += (model, ea) =>
            {
                try
                {
                    byte[] body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    RequestClass request = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestClass>(message);
                    if (request.entrust_no != null)
                    {
                        if (G.OrderDicts.ContainsKey(request.entrust_no))
                        {
                            G.OrderDicts[request.entrust_no] = request;
                        }
                        else
                        {
                            G.OrderDicts.Add(request.entrust_no, request);
                        }
                    }
                    //UpdateAccount(request);
                    OnReceiveMsg(request);
                }
                catch (Exception ex)
                {
                    OnError(ex.ToString());
                }
                finally
                {
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
            };
            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: false);
            #endregion

            UpdatePositionAsync();
        }


        public void FutureHQ()
        {
            string HQEXCHANGENAME = "COMMODITY";
            var channelHQ = connection.CreateModel();
            channelHQ.ExchangeDeclare(exchange: HQEXCHANGENAME, type: "fanout", durable: true);

            var consumerHQ = new EventingBasicConsumer(channelHQ);

            consumerHQ.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body.ToArray());
                FutureHQ hq = Newtonsoft.Json.JsonConvert.DeserializeObject<FutureHQ>(message);
                OnReceiveHQ(hq);
                //UpdateProfit(hq);

            };
            var queueName = channelHQ.QueueDeclare().QueueName;
            channelHQ.QueueBind(queue: queueName,
                              exchange: HQEXCHANGENAME,
                              routingKey: "");
            channelHQ.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumerHQ);
        }


        public void SendOrder(RequestClass order)
        {
            try
            {
                order.queue_name = replyQueueName;
                string body = Newtonsoft.Json.JsonConvert.SerializeObject(order);
                channel.BasicPublish(exchange: "", routingKey: workQueueName, basicProperties: props, body: Encoding.UTF8.GetBytes(body), mandatory: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                RequestClass msg = new RequestClass() { error_info = e.ToString() };
                //CallBackMsg msg = new CallBackMsg() { ErrorMsg = e.ToString() };
                OnReceiveMsg(msg);
                Thread.Sleep(100);
                SendOrder(order);
            }

        }

        public void Send(RequestClass request)
        {


            AmtClass AMT = CalAmt(request);

            if (AMT.closeAmt > 0)
            {

                var requestClose = DeepCopy<RequestClass>(request);
                requestClose.futures_direction = "2";
                requestClose.entrust_amount = AMT.closeAmt.ToString();

                SendOrder(requestClose);
            }
            if (AMT.openAmt > 0)
            {
                var requestOpen = DeepCopy<RequestClass>(request);
                requestOpen.futures_direction = "1";
                requestOpen.entrust_amount = AMT.openAmt.ToString();

                SendOrder(requestOpen);
            }


            // 计算开平头寸
            AmtClass CalAmt(RequestClass order)
            {
                AmtClass OCAmt = new AmtClass() { closeAmt = 0, openAmt = 0 };
                try
                {
                    lock (_lock)
                    {
                        var LongPostion = G.PosList.Where(o => o.out_stock_code == order.code && o.out_position_flag == "1").FirstOrDefault();
                        var ShortPostion = G.PosList.Where(o => o.out_stock_code == order.code && o.out_position_flag == "2").FirstOrDefault();

                        int currentPosition = 0;

                        if (order.direction == "1")
                        {
                            if (ShortPostion != null)
                            {
                                int.TryParse(ShortPostion.out_enable_amount.ToString(), out currentPosition);
                            }

                        }
                        else
                        {
                            if (LongPostion != null)
                            {
                                int.TryParse(LongPostion.out_enable_amount.ToString(), out currentPosition);
                            }

                        }

                        if (currentPosition > 0)
                        {
                            int Amt = int.Parse(request.entrust_amount) - currentPosition;
                            if (Amt >= 0)
                            {
                                OCAmt.closeAmt = currentPosition;
                                OCAmt.openAmt = Amt;
                            }
                            else
                            {
                                OCAmt.closeAmt = int.Parse(request.entrust_amount);
                            }
                        }
                        // 无反向持仓
                        else
                        {
                            OCAmt.openAmt = int.Parse(request.entrust_amount);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return OCAmt;

            }

        }


        public void Cancle(string rawClordid, string CancleClordid)
        {
            lock (cancleLock)
            {
                var toCancleList = G.OrderDicts.Values.Where(o => o.clordId == rawClordid).ToList();
                foreach (var cl in toCancleList)
                {
                    SendOrder(new Models.RequestClass
                    {
                        businessType = "0",
                        user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                        entrust_no = cl.entrust_no,
                        clordId = CancleClordid
                    });
                }
            }
        }



        // 初始化
        public static void Init()
        {

            ConnDataBase();

            G.CONFIG = G.db.Queryable<ConfigPars>().First();
            //ReadAccounts();
            G.rClient = ConnectionMultiplexer.Connect(G.CONFIG.redis).GetDatabase();
            G.rClientHQ = ConnectionMultiplexer.Connect(G.CONFIG.redisHQ).GetDatabase();

            ReadOrders();

            void ConnDataBase()
            {
                G.db = G.GetInstance("SqlConnString");
            }

            void ReadAccounts()
            {

                List<Account> dt = G.db.Ado.SqlQuery<Account>("select * from Account where queue_name = '" + G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName") + "'");
                lock (_lock)
                {
                    foreach (var acc in dt)
                    {
                        G.UserDict.Add(acc.direction + "_" + acc.code, acc);
                    }
                }

            }

            void ReadOrders()
            {
                List<RequestClass> dt = G.db.Ado.SqlQuery<RequestClass>("select a.* from requestclass a inner join (select entrust_no,MAX(id) maxid from OrderFlowVty  where  entrust_no is not null  group by entrust_no) b on a.id = b.maxid  and queue_name = '" + G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName") + "'");
                foreach (var request in dt)
                {
                    G.OrderDicts.Add(request.entrust_no, request);
                }
            }

        }


        // 更新持仓
        public static void UpdatePosition(RequestClass order)
        {
            if (!(order.code == null || order.error_info == "订单已结束，操作无效" || order.entrust_no == "0"))
            {
                lock (_lock)
                {

                    if (!G.UserDict.ContainsKey("1_" + order.code))
                    {
                        Account newPos = new Account();
                        newPos.queue_name = order.queue_name;
                        newPos.user = XmlHelper.GetInnerText(G.doc, "ClientName");
                        newPos.queue_name = G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName");
                        newPos.code = order.code;
                        newPos.direction = "1";
                        newPos.cost = 0;
                        newPos.enable_amount = 0;
                        newPos.total_amount = 0;
                        newPos.float_profit = 0;
                        newPos.real_profit = 0;
                        newPos.total_profit = 0;
                        newPos.key_id = newPos.user + "_" + newPos.code + "_" + newPos.direction;
                        G.UserDict.Add("1_" + order.code, newPos);
                    }
                    if (!G.UserDict.ContainsKey("2_" + order.code))
                    {
                        Account newPos = new Account();
                        newPos.queue_name = order.queue_name;
                        newPos.user = XmlHelper.GetInnerText(G.doc, "ClientName");
                        newPos.queue_name = G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName");
                        newPos.code = order.code;
                        newPos.direction = "2";
                        newPos.cost = 0;
                        newPos.enable_amount = 0;
                        newPos.total_amount = 0;
                        newPos.float_profit = 0;
                        newPos.real_profit = 0;
                        newPos.total_profit = 0;
                        newPos.key_id = newPos.user + "_" + newPos.code + "_" + newPos.direction;
                        G.UserDict.Add("2_" + order.code, newPos);
                    }

                    G.UserDict["1_" + order.code].update_time = DateTime.Now;
                    G.UserDict["2_" + order.code].update_time = DateTime.Now;

                    // 委托
                    if (order.orderState == null & order.entrust_no != null)
                    {
                        OrderChangePosition(order.businessType, order.entrust_direction, order.futures_direction, order.entrust_no, order.code, order.entrust_amount);
                    }
                    // 成交
                    else if (order.orderState == "6" || order.orderState == "7")
                    {
                        DealChangePosition(order.entrust_direction, order.futures_direction, double.Parse(order.onetime_deal_amount), double.Parse(order.onetime_deal_balance), order.code);
                    }
                    // 废单
                    else if (order.orderState == "5")
                    {
                        InValidOrderChangePosition(order.businessType, order.entrust_direction, order.futures_direction, order.entrust_no, order.code, order.entrust_amount);
                    }
                    // 撤单
                    else if (order.orderState == "8" || order.orderState == "9")
                    {
                        CancleChangePosition(order, order.entrust_direction, order.futures_direction, order.code);
                    }
                    CheckPosition();
                }
            }




            #region functions
            // 持仓变化——新增
            void OrderChangePosition(string businessType, string entrust_direction, string futures_direction, string entrust_no, string code, string amt)
            {
                try
                {
                    if (businessType == "1")
                    {
                        if (futures_direction == "2") //平仓 可用持仓减少
                        {
                            if (entrust_direction == "1") // 买入平仓  多平 空头可用减少
                            {
                                string Key = "2" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount - double.Parse(amt);
                                G.UserDict[Key] = thisPos;
                            }
                            else if (entrust_direction == "2") // 卖出平仓 空平  多头可用减少
                            {
                                string Key = "1" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount - double.Parse(amt);
                                G.UserDict[Key] = thisPos;
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    e.ToString();
                }

            }
            // 持仓变化——撤销
            void CancleChangePosition(RequestClass od, string entrust_direction, string futures_direction, string code)
            {
                try
                {
                    int entrustAmt = 0;
                    int dealAmt = 0;
                    int.TryParse(od.entrust_amount, out entrustAmt);
                    int.TryParse(od.deal_amount, out dealAmt);

                    int UndealAmt = entrustAmt - dealAmt;
                    if (futures_direction == "2") //平仓
                    {
                        if (entrust_direction == "1") // 买入平仓  多平撤单  空头可用增加
                        {
                            string Key = "2" + "_" + code;
                            var thisPos = G.UserDict[Key];

                            thisPos.enable_amount = thisPos.enable_amount + UndealAmt;
                            G.UserDict[Key] = thisPos;

                        }
                        else if (entrust_direction == "2") // 卖出平仓 空平撤单 多头可用增加
                        {
                            string Key = "1" + "_" + code;
                            var thisPos = G.UserDict[Key];
                            thisPos.enable_amount = thisPos.enable_amount + UndealAmt;
                            G.UserDict[Key] = thisPos;

                        }
                    }

                }
                catch (Exception e)
                {
                    e.ToString();
                }

            }
            // 持仓变化——废单
            void InValidOrderChangePosition(string businessType, string entrust_direction, string futures_direction, string entrust_no, string code, string amt)
            {

                try
                {
                    // 废单的时候，将下面减少的加回去，加上的再重新减掉
                    if (businessType == "1")
                    {
                        if (futures_direction == "2") //平仓
                        {
                            if (entrust_direction == "1") // 买入平仓  多平 空头可用减少
                            {
                                string Key = "2" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount + double.Parse(amt);
                                G.UserDict[Key] = thisPos;

                            }
                            else if (entrust_direction == "2") // 卖出平仓 空平  多头可用减少
                            {
                                string Key = "1" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount + double.Parse(amt);
                                G.UserDict[Key] = thisPos;

                            }
                        }
                    }
                    else  // 撤单
                    {
                        // 根据撤单编号找到对应的委托
                        RequestClass cancleOrder = G.OrderDicts[entrust_no];

                        if (cancleOrder.futures_direction == "2") //平仓
                        {
                            if (cancleOrder.entrust_direction == "1") // 买入平仓  多平撤单  空头可用增加
                            {
                                string Key = "2" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount - double.Parse(amt);
                                G.UserDict[Key] = thisPos;

                            }
                            else if (cancleOrder.entrust_direction == "2") // 卖出平仓 空平撤单 多头可用增加
                            {
                                string Key = "1" + "_" + code;
                                var thisPos = G.UserDict[Key];
                                thisPos.enable_amount = thisPos.enable_amount - double.Parse(amt);
                                G.UserDict[Key] = thisPos;

                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
            // 持仓变化——成交
            void DealChangePosition(string entrust_direction, string futures_direction, double amt, double balance, string code)
            {
                try
                {
                    // 开仓 
                    // 开仓成功，可用持仓和实际持仓都会增加
                    // 开仓会改变建仓的平均成本
                    if (futures_direction == "1")
                    {
                        if (entrust_direction == "1") // 多开 多头持仓增加 多头可用持仓增加 
                        {
                            string Key = "1" + "_" + code;
                            // 如果存在key，则用原始值，不存在则添加
                            var thisPos = G.UserDict[Key];
                            thisPos.enable_amount = thisPos.enable_amount + amt;
                            thisPos.total_amount = thisPos.total_amount + amt;
                            thisPos.total_balance = thisPos.total_balance + balance;
                            if (thisPos.total_amount != 0)
                            {
                                thisPos.cost = thisPos.total_balance / (thisPos.total_amount * 10000d);
                            }
                            else
                            {
                                thisPos.cost = 0;
                            }
                            G.UserDict[Key] = thisPos;

                        }
                        else // 空开  空头持仓增加 空头可用持仓增加 
                        {
                            string Key = "2" + "_" + code;

                            var thisPos = G.UserDict[Key];
                            thisPos.enable_amount = thisPos.enable_amount + amt;
                            thisPos.total_amount = thisPos.total_amount + amt;
                            thisPos.total_balance = thisPos.total_balance + balance;
                            if (thisPos.total_amount != 0)
                            {
                                thisPos.cost = thisPos.total_balance / (thisPos.total_amount * 10000d);
                            }
                            else
                            {
                                thisPos.cost = 0;
                            }
                            G.UserDict[Key] = thisPos;
                        }
                    }
                    // 平仓
                    // 只有真实持仓会发生变动，可用持仓在委托的时候已经改变过了
                    // 平仓会改变实现盈亏
                    else
                    {
                        if (entrust_direction == "1") // 多平 空头真实持仓减少
                        {
                            string Key = "2" + "_" + code;
                            var thisPos = G.UserDict[Key];
                            //double profit = Math.Round((thisPos.total_balance / thisPos.total_amount) * amt, MidpointRounding.AwayFromZero) - balance;
                            //thisPos.real_profit = thisPos.real_profit + profit;
                            thisPos.total_amount = thisPos.total_amount - amt;
                            thisPos.total_balance = thisPos.total_balance - balance;

                            double closePrice = balance / (amt * 10000d);
                            //double profit = Math.Round((thisPos.cost - closePrice) * amt * 10000, MidpointRounding.AwayFromZero);
                            double profit = (thisPos.cost - closePrice) * amt * 10000;

                            //var d = profit % 50;
                            //if (Math.Abs(d) <= 10)
                            //{
                            //    profit = profit - d;
                            //}
                            //if (Math.Abs(d) >= 40)
                            //{
                            //    if (d > 0)
                            //    {
                            //        profit = profit + (50 - d);
                            //    }
                            //    else
                            //    {
                            //        profit = profit + (-50 - d);
                            //    }

                            //}
                            //profit = ModifyPorfit(profit);
                            thisPos.real_profit = profit + thisPos.real_profit;

                            if (thisPos.total_amount == 0)
                            {
                                thisPos.total_balance = 0;
                                thisPos.cost = 0;
                                thisPos.float_profit = 0;
                            }
                            thisPos.total_profit = thisPos.float_profit + thisPos.real_profit;
                            G.UserDict[Key] = thisPos;
                        }
                        else // 空平  多头真实持仓减少
                        {
                            string Key = "1" + "_" + code;
                            var thisPos = G.UserDict[Key];
                            //double profit = balance - Math.Round((thisPos.total_balance / thisPos.total_amount) * amt, MidpointRounding.AwayFromZero);
                            //thisPos.real_profit = thisPos.real_profit + profit;
                            thisPos.total_amount = thisPos.total_amount - amt;
                            thisPos.total_balance = thisPos.total_balance - balance;

                            double closePrice = balance / (amt * 10000d);
                            //double profit = Math.Round((closePrice - thisPos.cost) * amt * 10000, MidpointRounding.AwayFromZero);
                            double profit = (closePrice - thisPos.cost) * amt * 10000;

                            //var d = profit % 50;
                            //if (Math.Abs(d) <= 10)
                            //{
                            //    profit = profit - d;
                            //}
                            //if (Math.Abs(d) >= 40)
                            //{
                            //    if (d > 0)
                            //    {
                            //        profit = profit + (50 - d);
                            //    }
                            //    else
                            //    {
                            //        profit = profit + (-50 - d);
                            //    }

                            //}
                            //profit = ModifyPorfit(profit);
                            thisPos.real_profit = profit + thisPos.real_profit;

                            if (thisPos.total_amount == 0)
                            {
                                thisPos.total_balance = 0;
                                thisPos.cost = 0;
                                thisPos.float_profit = 0;
                            }
                            thisPos.total_profit = thisPos.float_profit + thisPos.real_profit;
                            G.UserDict[Key] = thisPos;
                        }
                    }

                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }

            void CheckPosition()
            {
                try
                {
                    if (G.UserDict["1_" + order.code].enable_amount > G.UserDict["1_" + order.code].total_amount)
                    {
                        G.UserDict["1_" + order.code].enable_amount = G.UserDict["1_" + order.code].total_amount;
                    }
                    if (G.UserDict["2_" + order.code].enable_amount > G.UserDict["2_" + order.code].total_amount)
                    {
                        G.UserDict["2_" + order.code].enable_amount = G.UserDict["2_" + order.code].total_amount;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            #endregion
        }


        public static void UpdatePublishData()
        {
            lock (_lock)
            {
                List<Account> accList = G.UserDict.Values.ToList<Account>();
                G.db.Saveable<Account>(accList).ExecuteReturnEntity();
            }

        }


        public static void UpdateAccount(RequestClass order)
        {
            UpdatePosition(order);
            UpdatePublishData();
        }


        // 更新浮动盈亏数据
        public static void UpdateProfit(FutureHQ hq)
        {
            string code = hq.SCode.Substring(0, hq.SCode.Length - 3);

            lock (hqLock)
            {
                if (G.UserDict.ContainsKey("1_" + code))
                {
                    var longAcc = G.UserDict["1_" + code];
                    if (longAcc.total_amount > 0)
                    {
                        var float_profit = (hq.NewPrice - longAcc.cost) * longAcc.total_amount * 10000;

                        //var d = float_profit % 50;
                        //if (Math.Abs(d) <= 10)
                        //{
                        //    float_profit = float_profit - d;
                        //}
                        //if (Math.Abs(d) >= 40)
                        //{
                        //    if (d > 0)
                        //    {
                        //        float_profit = float_profit + (50 - d);
                        //    }
                        //    else
                        //    {
                        //        float_profit = float_profit + (-50 - d);
                        //    }

                        //}

                        //float_profit = ModifyPorfit(float_profit);
                        longAcc.float_profit = float_profit;
                        longAcc.total_profit = longAcc.float_profit + longAcc.real_profit;
                        longAcc.update_time = DateTime.Now;
                    }
                    else
                    {
                        longAcc.float_profit = 0;
                        longAcc.total_profit = longAcc.float_profit + longAcc.real_profit;
                        longAcc.update_time = DateTime.Now;
                    }

                }

                if (G.UserDict.ContainsKey("2_" + code))
                {
                    var shortAcc = G.UserDict["2_" + code];
                    if (shortAcc.total_amount > 0)
                    {
                        var float_profit = (shortAcc.cost - hq.NewPrice) * shortAcc.total_amount * 10000;
                        //var d = float_profit % 50;
                        //if (Math.Abs(d) <= 10)
                        //{
                        //    float_profit = float_profit - d;
                        //}
                        //if (Math.Abs(d) >= 40)
                        //{
                        //    if (d > 0)
                        //    {
                        //        float_profit = float_profit + (50-d);
                        //    }
                        //    else
                        //    {
                        //        float_profit = float_profit + (-50-d);
                        //    }
                        //}

                        //float_profit = ModifyPorfit(float_profit);
                        shortAcc.float_profit = float_profit;
                        shortAcc.total_profit = shortAcc.float_profit + shortAcc.real_profit;
                        shortAcc.update_time = DateTime.Now;
                    }
                    else
                    {
                        shortAcc.float_profit = 0;
                        shortAcc.total_profit = shortAcc.float_profit + shortAcc.real_profit;
                        shortAcc.update_time = DateTime.Now;
                    }
                }
            }
        }

        public async Task UpdatePositionAsync()
        {
            var PositionKey = XmlHelper.GetInnerText(G.doc, "ClientName");
            void LongTask()
            {
                while (true)
                {
                    try
                    {
                        var str = G.rClient.StringGet(PositionKey);
                        if (str != G.posString)
                        {
                            lock (_lockPos)
                            {
                                G.PosList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FuturepositionQry_Output>>(str);
                                OnPostion(G.PosList);
                                G.posString = str;
                            }
                        }


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                    }
                    finally
                    {
                        Thread.Sleep(100);
                    }

                }
            }
            await Task.Factory.StartNew(() => LongTask(), TaskCreationOptions.LongRunning);
        }

        public static double ModifyPorfit(double profit)
        {
            var d = profit % 50;
            if (d != 0)
            {
                if (Math.Abs(d) < 20)
                {
                    profit = profit - d;
                }
                if (Math.Abs(d) >= 30)
                {
                    if (d > 0)
                    {
                        profit = profit + (50 - d);
                    }
                    else
                    {
                        profit = profit + (-50 - d);
                    }

                }
            }

            return profit;
        }

        public void Close()
        {
            connection.Close();
        }

        private class AmtClass
        {
            public int closeAmt { get; set; }
            public int openAmt { get; set; }
        }

        public static T DeepCopy<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopy(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
    }


}
