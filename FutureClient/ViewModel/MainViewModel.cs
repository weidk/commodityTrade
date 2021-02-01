using FutureClient.Models;
using FutureMQClient;
using FutureMQClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FutureClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public object _hqLock = new object();
        public object _PairLock = new object();
        MQClient mq;
        //List<string> FinishedState = new List<string> { "5", "7", "8", "9", "F", "E" };

        public static List<string> FinishedStateText = new List<string> { "废单", "已成", "部撤", "已撤", "F", "E" };

        private List<FutureHQ> _hqlist;
        SqlSugarClient PairDb;
        SqlSugarClient PairDb2;
        SqlSugarClient HQDb;
        public Dictionary<string, FutureInfo> FutureInfoDict = new Dictionary<string, FutureInfo>();


        MainWindow MV;

        public object _lockPos = new object();

        public MainViewModel(MainWindow mv)
        {
            DispatcherHelper.Initialize();
            MV = mv;
        }

        public void Initial()
        {
            InitialModel();

            MQCallBack();

            InitialMQ();

            PairsTask();

            SummaryTask();

            void InitialModel()
            {
                PairDb = G.GetInstance("SqlConnString");
                PairDb2 = G.GetInstance("SqlConnString");
                HQDb = G.GetInstance("HQSqlConnString");
                Global.ValidateDict.Add("price", false);
                Global.ValidateDict.Add("amount", false);
                Global.ValidateDict.Add("LongRatio", true);
                Global.ValidateDict.Add("ShortRatio", true);
                Global.ValidateDict.Add("Pairs", false);
                Global.ValidateDict.Add("LongMultiplier", true);
                Global.ValidateDict.Add("ShortMultiplier", true);

                IsAutoOC = true;
                traderColor = "#FF4500";
                Direction = "1";
                CommonNewOrderCommand = new RelayCommand(CommonNewOrder, CanNewOrderExcute);

                PairCommonNewOrderCommand = new RelayCommand(PairCommonNewOrder, PairCanNewOrderExcute);

                CommonCancelOrder = new RelayCommand(CancelOrder);
                CommonCancelAllOrder = new RelayCommand(CancelAllOrder);
                CommonCancelPariOrder = new RelayCommand(CancelPariOrder);
                CommonCancelAllPairsOrder = new RelayCommand(CancelAllPairsOrder);
                CommonInitialFish = new RelayCommand(InitialFish);
                CommonSendFishOrders = new RelayCommand(SendFishOrders);

                IpList = new ObservableCollection<RequestClass>();
                LatestIpList = new ObservableCollection<RequestClass>();
                ExpiredlatestIpList = new ObservableCollection<RequestClass>();
                AccountIpList = new ObservableCollection<FuturepositionQry_Output>(G.PosList);
                PairList = new ObservableCollection<PairOrders>();
                HqDict = new Dictionary<string, FutureHQ>();
                ShowHQ = new FutureHQ();

                ShortRatio = 1;
                longRatio = 1;
                IsFishOk = false;
                //LongMultiplier = 1;
                //ShortMultiplier = 1;


                //MainTitle = $"歡迎  {XmlHelper.GetInnerText(G.doc, "ClientName")}  使用終端，日進斗金！";
                MainTitle = $"歡迎  {Login.USER.USERNAME}  使用終端，日進斗金！";
            }

            void InitialMQ()
            {
                mq.FutureHQ();
                FutureInfoDict = G.rClientHQ.HashGetAll("FutureBasicInfo").ToDictionary(
                            x => x.Name.ToString(),
                            x => Newtonsoft.Json.JsonConvert.DeserializeObject<FutureInfo>(x.Value),
                            StringComparer.Ordinal);

                string codeListString = G.rClientHQ.StringGet("CommodityCodesList");
                var codestring = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(codeListString);
                CodeList = new ObservableCollection<FutureCode>();
                _hqlist = new List<FutureHQ>();
                foreach (string code in codestring)
                {
                    CodeList.Add(new FutureCode() { Code = code, Name = FutureInfoDict[code].S_INFO_NAME });
                    string codeHq = G.rClientHQ.StringGet(code);
                    if (codeHq != null)
                    {
                        FutureHQ futurehq = Newtonsoft.Json.JsonConvert.DeserializeObject<FutureHQ>(codeHq);
                        if (futurehq.SCode != null)
                        {
                            _hqlist.Add(futurehq);
                        }
                    }

                }
                //SelectedCodeItem = CodeList[0];
                //SelectedLongCodeItem = CodeList[0];
                //SelectedShortCodeItem = CodeList[0];
                PairList = new ObservableCollection<PairOrders>(G.db.Ado.SqlQuery<PairOrders>($"select * from pairorders where queue_name = '{G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName")}' and DATEDIFF(dd, OrderSendTime, GETDATE()) = 0  and ID  in (select MAX(ID) from pairorders group by pairid) order by id desc").ToList());
                HqList = new ObservableCollection<FutureHQ>();
                FishList = new ObservableCollection<FishParas>();


                foreach (var hq in _hqlist)
                {
                    //char[] charsToTrim = { '.', 'Z', 'J' };
                    //string code = hq.SCode.TrimEnd(charsToTrim);

                    string code = hq.SCode;
                    if (!HqDict.ContainsKey(code))
                    {
                        HqDict.Add(code, hq);
                    }


                    HqList.Add(hq);

                    double _lowerLimitPrice = hq.LowerLimitPrice;
                    double _upperLimitPrice = hq.UpperLimitPrice;

                    FishList.Add(new FishParas()
                    {
                        SCode = code,
                        PrePrice = hq.PrePrice,
                        BuyFishPrice = _lowerLimitPrice,
                        SellFishPrice = _upperLimitPrice,
                        IsSelected = true
                    });
                }


                HqList = new ObservableCollection<FutureHQ>(HqList.OrderBy(o => o.SCode));


                foreach (var msg in G.OrderDicts.Values.ToList())
                {
                    if (msg.entrust_direction == "1")
                    {
                        msg.entrust_direction = "多";
                    }
                    else if (msg.entrust_direction == "2")
                    {
                        msg.entrust_direction = "空";
                    }

                    if (msg.futures_direction == "1")
                    {
                        msg.futures_direction = "开";
                    }
                    else if (msg.futures_direction == "2")
                    {
                        msg.futures_direction = "平";
                    }

                    if (msg.businessType == "1")
                    {
                        msg.businessType = "新增";
                    }
                    else
                    {
                        msg.businessType = "撤单";
                    }

                    if (msg.orderState == "1")
                    {
                        msg.orderState = "未报";
                    }
                    else if (msg.orderState == "2")
                    {
                        msg.orderState = "待报";
                    }
                    else if (msg.orderState == "3")
                    {
                        msg.orderState = "正报";
                    }
                    else if (msg.orderState == "4")
                    {
                        msg.orderState = "已报";
                    }
                    else if (msg.orderState == "5")
                    {
                        msg.orderState = "废单";
                    }
                    else if (msg.orderState == "6")
                    {
                        msg.orderState = "部成";
                    }
                    else if (msg.orderState == "7")
                    {
                        msg.orderState = "已成";
                    }
                    else if (msg.orderState == "8")
                    {
                        msg.orderState = "部撤";
                    }
                    else if (msg.orderState == "9")
                    {
                        msg.orderState = "已撤";
                    }
                    else if (msg.orderState == "a")
                    {
                        msg.orderState = "待撤";

                    }

                    LatestIpList.Add(msg);


                    if (FinishedStateText.Contains(msg.orderState) || msg.entrust_no == "0" || msg.error_info.Contains("已撤单"))
                    {
                        ExpiredlatestIpList.Add(msg);
                    }


                }
            }
            void MQCallBack()
            {
                mq = new MQClient();
                mq.OnReceiveMsg += (msg) =>
                {

                    if (msg.entrust_direction == "1")
                    {
                        msg.entrust_direction = "多";
                    }
                    else if (msg.entrust_direction == "2")
                    {
                        msg.entrust_direction = "空";
                    }

                    if (msg.futures_direction == "1")
                    {
                        msg.futures_direction = "开";
                    }
                    else if (msg.futures_direction == "2")
                    {
                        msg.futures_direction = "平";
                    }

                    if (msg.businessType == "1")
                    {
                        msg.businessType = "新增";
                    }
                    else
                    {
                        msg.businessType = "撤单";
                    }

                    if (msg.orderState == "1")
                    {
                        msg.orderState = "未报";
                    }
                    else if (msg.orderState == "2")
                    {
                        msg.orderState = "待报";
                    }
                    else if (msg.orderState == "3")
                    {
                        msg.orderState = "正报";
                    }
                    else if (msg.orderState == "4")
                    {
                        msg.orderState = "已报";
                    }
                    else if (msg.orderState == "5")
                    {
                        msg.orderState = "废单";
                    }
                    else if (msg.orderState == "6")
                    {
                        msg.orderState = "部成";
                    }
                    else if (msg.orderState == "7")
                    {
                        msg.orderState = "已成";
                    }
                    else if (msg.orderState == "8")
                    {
                        msg.orderState = "部撤";
                    }
                    else if (msg.orderState == "9")
                    {
                        msg.orderState = "已撤";
                    }
                    else if (msg.orderState == "a")
                    {
                        msg.orderState = "待撤";
                    }

                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {

                        //IpList.Insert(0, msg);
                        // 更新到最新视图
                        if (LatestIpList.Where(o => o.entrust_no == msg.entrust_no).ToList().Count > 0)
                        {
                            for (int i = LatestIpList.Count - 1; i >= 0; i--)
                            {
                                if (LatestIpList[i].entrust_no == msg.entrust_no)
                                {
                                    if (LatestIpList[i].IsSelected)
                                    {
                                        msg.IsSelected = true;
                                    }
                                    LatestIpList[i] = msg;

                                    if (FinishedStateText.Contains(msg.orderState) || msg.entrust_no == "0" || msg.error_info != "" || msg.direction == null)
                                    {
                                        ExpiredlatestIpList.Insert(0, msg);
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (msg.entrust_no != "")
                            {
                                if (FinishedStateText.Contains(msg.orderState) || msg.entrust_no == "0" || msg.error_info != "")
                                {
                                    ExpiredlatestIpList.Insert(0, msg);
                                }
                                else
                                {
                                    if (msg.orderState == "部成" && msg.error_info.Contains("已撤单"))
                                    {
                                        ExpiredlatestIpList.Add(msg);
                                    }
                                    else
                                    {
                                        LatestIpList.Insert(0, msg);
                                    }
                                }
                            }

                        }

                        // 更新配對數據
                        if (msg.addInfo2 != null)
                        {
                            PairOrders p = pairList.Where(o => o.PairId == msg.addInfo2).FirstOrDefault();
                            if (msg.orderState == "已成")
                            {
                                double dealAmt = double.Parse(msg.deal_amount);
                                double dealBalance = double.Parse(msg.deal_amount)* double.Parse(msg.deal_price);
                                if (msg.direction == "1")
                                {
                                    p.FinishedLongLeg = p.FinishedLongLeg - p.LongPartDeal + dealAmt;
                                    //p.FinishedLongLegAmt += dealBalance;
                                    p.FinishedLongLegAmt = p.FinishedLongLegAmt - p.LongPartDealBalance + dealBalance;
                                    p.LongPartDeal = 0;
                                    p.LongPartDealBalance = 0;

                                    p.LongExBalance = dealBalance;
                                    p.LongEx = dealAmt;
                                }
                                if (msg.direction == "2")
                                {
                                    p.FinishedShortLeg = p.FinishedShortLeg - p.ShortPartDeal + dealAmt;
                                    //p.FinishedShortLegAmt += dealBalance;
                                    p.FinishedShortLegAmt = p.FinishedShortLegAmt - p.ShortPartDealBalance + dealBalance;
                                    p.ShortPartDeal = 0;
                                    p.ShortPartDealBalance = 0;

                                    p.ShortExBalance = dealBalance;
                                    p.ShortEx = dealAmt;
                                }




                                // 多空对数匹配，计算完成对数、进度、是否结束、成本
                                if (p.FinishedLongLeg / p.LongRatio == p.FinishedShortLeg / p.ShortRatio)
                                {
                                    p.FinishedPairs = p.FinishedLongLeg / p.LongRatio;
                                    p.Progress = Math.Round(100 * (p.FinishedPairs / p.Pairs));

                                    
                                    p.MeanCost = Math.Round((p.LongMultiplier * p.FinishedLongLegAmt - p.ShortMultiplier * p.FinishedShortLegAmt ) / (p.FinishedPairs), 4);


                                    if (p.FinishedPairs == p.Pairs)
                                    {
                                        p.IsFinish = true;
                                    }
                                    p.LongEx = 0;
                                    p.ShortEx = 0;
                                    p.LongExBalance = 0;
                                    p.ShortExBalance = 0;
                                }

                                p.LongEx = p.FinishedLongLeg - p.FinishedPairs * p.LongRatio;
                                p.ShortEx = p.FinishedShortLeg - p.FinishedPairs * p.ShortRatio;

                                //int ind = pairList.IndexOf(p);
                                //if (ind > -1)
                                //{
                                //    pairList.RemoveAt(ind);
                                //    pairList.Insert(ind, p);
                                //}
                                PairDb2.Insertable(p).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();
                            }
                            if (msg.orderState == "废单" || msg.error_info != "")
                            {
                                p.AddInfo2 = msg.error_info;
                                p.IsFinish = true;
                                //int ind = pairList.IndexOf(p);
                                //if (ind > -1)
                                //{
                                //    pairList.RemoveAt(ind);
                                //    pairList.Insert(ind, p);
                                //}
                                PairDb2.Insertable(p).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();
                            }


                            if (msg.orderState == "部成")
                            {
                                double dealAmt = double.Parse(msg.deal_amount);
                                double dealBalance = double.Parse(msg.deal_amount) * double.Parse(msg.deal_price);
                                //double dealBalance = double.Parse(msg.deal_balance);
                                if (msg.direction == "1")
                                {
                                    p.FinishedLongLeg = p.FinishedLongLeg - p.LongPartDeal + dealAmt;
                                    p.LongPartDeal = dealAmt;

                                    p.FinishedLongLegAmt = p.FinishedLongLegAmt - p.LongPartDealBalance + dealBalance;
                                    p.LongPartDealBalance = dealBalance;

                                    p.LongExBalance = dealBalance;
                                    p.LongEx = dealAmt;

                                }
                                if (msg.direction == "2")
                                {
                                    p.FinishedShortLeg = p.FinishedShortLeg - p.ShortPartDeal + dealAmt;
                                    p.ShortPartDeal = dealAmt;

                                    p.FinishedShortLegAmt = p.FinishedShortLegAmt - p.ShortPartDealBalance + dealBalance;
                                    p.ShortPartDealBalance = dealBalance;

                                    p.ShortExBalance = dealBalance;
                                    p.ShortEx = dealAmt;
                                }

                                //int ind = pairList.IndexOf(p);
                                //if (ind > -1)
                                //{
                                //    pairList.RemoveAt(ind);
                                //    pairList.Insert(ind, p);
                                //}
                                PairDb2.Insertable(p).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();
                            }

                        }


                    });



                };
                mq.OnError += (err) =>
                {
                    MessageBox.Show(err);
                };
                mq.OnReceiveHQ += (hq) =>
                {
                    try
                    {
                        string code = hq.SCode;
                        //lock (_hqLock)
                        //{
                        if (HqDict.ContainsKey(code))
                        {
                            HqDict[code] = hq;
                        }
                        else
                        {
                            HqDict.Add(code, hq);
                        }
                        HqList = new ObservableCollection<FutureHQ>(HqDict.Values.OrderBy(o => o.SCode));


                        // 报价面板行情list，恢复报价面板界面需要取消注释
                        //DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        //{
                        //    var h = HqList.Where(o => o.SCode == hq.SCode).FirstOrDefault();
                        //    int ind = HqList.IndexOf(h);
                        //    if (ind > -1)
                        //    {
                        //        HqList.RemoveAt(ind);
                        //        HqList.Insert(ind, hq);
                        //    }

                        //    //HqList.Remove(h);
                        //    //HqList.Add(hq);
                        //    //HqList.OrderBy(o => o.SCode);
                        //});


                        //HqList.OrderBy(o => o.SCode);
                        //}
                        if (ShowHQ != null && ShowHQ.SCode == hq.SCode)
                        {
                            ShowHQ = hq;
                        }
                        if (selectedLongCodeItem != null && selectedShortCodeItem != null)
                        {
                            if (selectedLongCodeItem.Code == hq.SCode || selectedShortCodeItem.Code == hq.SCode)
                            {
                                CurrentMinusPrice = Math.Round(LongMultiplier * LongRatio * HqDict[selectedLongCodeItem.Code].SelPrice1 - ShortMultiplier * ShortRatio * HqDict[selectedShortCodeItem.Code].BuyPrice1, 4);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }



                };
                mq.OnPostion += (pos) =>
                {
                    AccountIpList = new ObservableCollection<FuturepositionQry_Output>(pos);
                };
            }
        }

        public async Task PairsTask()
        {
            string ReplyName = G.CONFIG.ReplyName;
            void LongTask()
            {
                Dictionary<string, FutureHQ> thisHqDict = new Dictionary<string, FutureHQ>();
                while (true)
                {
                    try
                    {
                        lock (_PairLock)
                        {
                            thisHqDict = HqDict;
                        }
                            foreach (var pair in PairList)
                            {
                                if (!pair.IsDelete)
                                {
                                    if (!pair.IsFinish)
                                    {
                                        if (thisHqDict.ContainsKey(pair.LongCode) && thisHqDict.ContainsKey(pair.ShortCode))
                                        {
                                            double HqMinus = Math.Round(pair.LongMultiplier * pair.LongRatio * thisHqDict[pair.LongCode].SelPrice1 - pair.ShortMultiplier * pair.ShortRatio * thisHqDict[pair.ShortCode].BuyPrice1, 4);

                                            pair.CurrentMinus = HqMinus;

                                            if (HqMinus <= pair.Minus && !pair.IsPause)
                                            {
                                                if ((pair.FinishedPairs == pair.SendedPairs && pair.SendedPairs > 0) || pair.SendedPairs == 0)
                                                {
                                                    double longPrice = thisHqDict[pair.LongCode].SelPrice1;
                                                    double shortPrice = thisHqDict[pair.ShortCode].BuyPrice1;

                                                    FutureInfo LongFuture = new FutureInfo();
                                                    FutureInfo ShortFuture = new FutureInfo();
                                                    FutureInfoDict.TryGetValue(pair.LongCode, out LongFuture);
                                                    FutureInfoDict.TryGetValue(pair.LongCode, out ShortFuture);

                                                    while ((pair.LongMultiplier * pair.LongRatio * longPrice - pair.ShortMultiplier * pair.ShortRatio * shortPrice) < pair.Minus)
                                                    {
                                                        double newLong = Math.Round(longPrice + LongFuture.CHANGE_TICK, 3);
                                                        double newShort = Math.Round(shortPrice - ShortFuture.CHANGE_TICK, 3);
                                                        if ((pair.LongMultiplier * pair.LongRatio * newLong - pair.ShortMultiplier * pair.ShortRatio * newShort) <= pair.Minus)
                                                        {
                                                            longPrice = newLong;
                                                            shortPrice = newShort;
                                                        }
                                                        else if ((pair.LongMultiplier * pair.LongRatio * longPrice - pair.ShortMultiplier * pair.ShortRatio * newShort) <= pair.Minus)
                                                        {
                                                            shortPrice = newShort;
                                                        }
                                                        else if ((pair.LongMultiplier * pair.LongRatio * newLong - pair.ShortMultiplier * pair.ShortRatio * shortPrice) <= pair.Minus)
                                                        {
                                                            longPrice = newLong;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }

                                                    }

                                                    if (IsOnlyOpen)
                                                    {
                                                        mq.SendOrder(new RequestClass
                                                        {
                                                            businessType = "1",
                                                            code = pair.LongCode,
                                                            direction = "1",
                                                            original_price = longPrice.ToString(),
                                                            entrust_price = longPrice.ToString(),
                                                            entrust_amount = pair.LongRatio.ToString(),
                                                            entrust_direction = "1",
                                                            futures_direction = "1",
                                                            queue_name = ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName"),
                                                            addInfo1 = pair.AddInfo1,
                                                            addInfo2 = pair.PairId,
                                                            addInfo3 = DateTime.Now.ToString(),
                                                            clordId = "long_" + Guid.NewGuid().ToString("N") + pair.PairId
                                                        });

                                                        mq.SendOrder(new RequestClass
                                                        {
                                                            businessType = "1",
                                                            code = pair.ShortCode,
                                                            direction = "2",
                                                            original_price = shortPrice.ToString(),
                                                            entrust_price = shortPrice.ToString(),
                                                            entrust_amount = pair.ShortRatio.ToString(),
                                                            entrust_direction = "2",
                                                            futures_direction = "1",
                                                            queue_name = ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName"),
                                                            addInfo1 = pair.AddInfo1,
                                                            addInfo2 = pair.PairId,
                                                            addInfo3 = DateTime.Now.ToString(),
                                                            clordId = "short_" + Guid.NewGuid().ToString("N") + pair.PairId
                                                        });
                                                    }
                                                    else
                                                    {
                                                        mq.Send(new RequestClass
                                                        {
                                                            businessType = "1",
                                                            code = pair.LongCode,
                                                            direction = "1",
                                                            original_price = longPrice.ToString(),
                                                            entrust_price = longPrice.ToString(),
                                                            entrust_amount = pair.LongRatio.ToString(),
                                                            entrust_direction = "1",
                                                            queue_name = ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName"),
                                                            addInfo1 = pair.AddInfo1,
                                                            addInfo2 = pair.PairId,
                                                            addInfo3 = DateTime.Now.ToString(),
                                                            clordId = "long_" + Guid.NewGuid().ToString("N") + pair.PairId
                                                        });

                                                        mq.Send(new RequestClass
                                                        {
                                                            businessType = "1",
                                                            code = pair.ShortCode,
                                                            direction = "2",
                                                            original_price = shortPrice.ToString(),
                                                            entrust_price = shortPrice.ToString(),
                                                            entrust_amount = pair.ShortRatio.ToString(),
                                                            entrust_direction = "2",
                                                            queue_name = ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName"),
                                                            addInfo1 = pair.AddInfo1,
                                                            addInfo2 = pair.PairId,
                                                            addInfo3 = DateTime.Now.ToString(),
                                                            clordId = "short_" + Guid.NewGuid().ToString("N") + pair.PairId
                                                        });
                                                    }


                                                    pair.SendedPairs += 1;
                                                    pair.IsTrigger = true;
                                                    PairDb.Insertable(pair).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();
                                                    DispatcherHelper.CheckBeginInvokeOnUI(() => { MV.PairsChange(); });
                                                }

                                            }
                                        }
                                    }

                                }

                            }
                        //}
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                    }
                    finally
                    {
                        Thread.Sleep(50);
                    }

                }

            }
            await Task.Factory.StartNew(() => LongTask(), TaskCreationOptions.LongRunning);
        }



        public async Task SummaryTask()
        {
            string clientName = XmlHelper.GetInnerText(G.doc, "ClientName");
            var SummaryDb = G.GetInstance("SqlConnString");
            List<PairDetail> newPairList = new List<PairDetail>();
            //SummaryDb.DbFirst.Where("DirectionDetail").CreateClassFile(@"D:\workspace\C#", "FutureClient.Models");
            //SummaryDb.DbFirst.Where("PairDetail").CreateClassFile(@"D:\workspace\C#", "FutureClient.Models");
            void LongTask()
            {
                while (true)
                {
                    try
                    {
                        var DirectionList = SummaryDb.Queryable<DirectionDetail>().Where(it => it.queue_name.Contains(clientName)).ToList();
                        //DirectionList.ForEach(o => o.avgcostt /=  FutureInfoDict[o.code].CHANGE_TICK);
                        DirectionSummaryCollection = new ObservableCollection<DirectionDetail>(DirectionList);

                        List<PairDetail> PairSummaryList = SummaryDb.Queryable<PairDetail>().Where(it => it.queue_name.Contains(clientName)).ToList();

                        if (newPairList.Sum(o => o.longPosition) == PairSummaryList.Sum(o => o.longPosition) && newPairList.Sum(o => o.shortPosition) == PairSummaryList.Sum(o => o.shortPosition))
                        {
                            continue;
                        }

                        foreach (PairDetail p in PairSummaryList)
                        {
                            if (p.pairs > 0)
                            {

                                p.LongExCost = Math.Round(p.LongExBalance / p.LongEx, 3);
                                p.ShortExCost = Math.Round(p.ShortExBalance /  p.ShortEx, 3);

                                p.minus = Math.Round(p.sumcost / p.pairs, 4);

                                double ExPairs = 0;
                                // 可以再构成配对
                                if (p.LongEx / p.LongRatio >= 1 && p.ShortEx / p.ShortRatio >= 1)
                                {


                                    double ExPairLong = Math.Floor(p.LongEx / p.LongRatio);
                                    double ExPairShort = Math.Floor(p.ShortEx / p.ShortRatio);

                                    if (ExPairLong <= ExPairShort)
                                    {
                                        ExPairs = ExPairLong;
                                    }
                                    else
                                    {
                                        ExPairs = ExPairShort;
                                    }

                                    double ExSumCost = (p.LongMultiplier * p.LongExCost * p.LongRatio - p.ShortMultiplier * p.ShortExCost * p.ShortRatio) * ExPairs;


                                    p.pairs += ExPairs;
                                    p.LongEx -= ExPairs * p.LongRatio;
                                    p.ShortEx -= ExPairs * p.ShortRatio;
                                    p.minus = Math.Round((p.sumcost + ExSumCost) / p.pairs, 4);
                                }


                                if (p.LongEx > 0)
                                {
                                    p.LongExString = p.LongEx.ToString() + " / " + p.LongExCost.ToString();
                                }
                                if (p.ShortEx > 0)
                                {
                                    p.ShortExString = p.ShortEx.ToString() + " / " + p.ShortExCost.ToString();
                                }

                                if (p.longPosition > 0)
                                {
                                    p.LongString = p.longPosition.ToString() + " / " + Math.Round(p.longBalance / p.longPosition, 3).ToString();
                                }

                                if (p.shortPosition > 0)
                                {
                                    p.ShortString = p.shortPosition.ToString() + " / " + Math.Round(p.shortBalance /  p.shortPosition, 3).ToString();
                                }
                            }
                        }

                        newPairList = PairSummaryList.Where(o => o.pairs > 0).OrderBy(p =>
                        {
                            if (p.longBalance < p.shortBalance)
                            {
                                string[] comboPart = p.combo.Split('-');
                                return $"{comboPart[1]}-{comboPart[0]}s";
                            }
                            else
                            {
                                return p.combo;
                            }
                        }
                        ).ToList();

                        PariSummaryCollection = new ObservableCollection<PairDetail>(newPairList);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                    }
                    finally
                    {
                        Thread.Sleep(3000);
                    }

                }

            }
            await Task.Factory.StartNew(() => LongTask(), TaskCreationOptions.LongRunning);
        }


        public void ChangeMinus(bool isOntime)
        {
            try
            {
                if (selectedLongCodeItem.Code != null && selectedShortCodeItem.Code != null && HqDict.ContainsKey(selectedLongCodeItem.Code) && HqDict.ContainsKey(selectedShortCodeItem.Code))
                {
                    if (isOntime)
                    {

                        CurrentMinusPrice = Math.Round(LongMultiplier * LongRatio * HqDict[selectedLongCodeItem.Code].SelPrice1 - ShortMultiplier * ShortRatio * HqDict[selectedShortCodeItem.Code].BuyPrice1, 4);
                    }
                    else
                    {
                        MinusPrice = Math.Round(LongMultiplier * LongRatio * HqDict[selectedLongCodeItem.Code].SelPrice1 - ShortMultiplier * ShortRatio * HqDict[selectedShortCodeItem.Code].BuyPrice1, 4);
                    }

                }
                else
                {
                    CurrentMinusPrice = null;
                    MinusPrice = null;
                }
            }
            catch (Exception e)
            {

            }
        }

        #region 定义变量

        private double? minusPrice;
        public double? MinusPrice
        {
            get { return minusPrice; }
            set { minusPrice = value; RaisePropertyChanged(); }
        }

        private double? currentMinusPrice;
        public double? CurrentMinusPrice
        {
            get { return currentMinusPrice; }
            set { currentMinusPrice = value; RaisePropertyChanged(); }
        }

        private double pairs;
        public double Pairs
        {
            get { return pairs; }
            set { pairs = value; RaisePropertyChanged(); }
        }

        private FutureCode selectedLongCodeItem;
        public FutureCode SelectedLongCodeItem
        {
            get { return selectedLongCodeItem; }
            set { selectedLongCodeItem = value; RaisePropertyChanged(); }
        }

        private FutureCode selectedShortCodeItem;
        public FutureCode SelectedShortCodeItem
        {
            get { return selectedShortCodeItem; }
            set { selectedShortCodeItem = value; RaisePropertyChanged(); }
        }

        private double shortRatio;
        public double ShortRatio
        {
            get { return shortRatio; }
            set { shortRatio = value; RaisePropertyChanged(); }
        }

        private double longRatio;
        public double LongRatio
        {
            get { return longRatio; }
            set { longRatio = value; RaisePropertyChanged(); }
        }

        private double longMultiplier { get; set; } = 1;
        public double LongMultiplier
        {
            get { return longMultiplier; }
            set { longMultiplier = value; RaisePropertyChanged(); }
        }
        private double shortMultiplier { get; set; } = 1;
        public double ShortMultiplier
        {
            get { return shortMultiplier; }
            set { shortMultiplier = value; RaisePropertyChanged(); }
        }

        private bool isOnlyOpen = false;
        public bool IsOnlyOpen
        {
            get { return isOnlyOpen; }
            set { isOnlyOpen = value; RaisePropertyChanged(); }
        }

        private bool isAutoOC;
        public bool IsAutoOC
        {
            get { return isAutoOC; }
            set { isAutoOC = value; RaisePropertyChanged(); }
        }

        private string mainTitle;
        public string MainTitle
        {
            get { return mainTitle; }
            set { mainTitle = value; RaisePropertyChanged(); }
        }

        private string entrust_direction;
        public string Entrust_direction
        {
            get { return entrust_direction; }
            set { entrust_direction = value; RaisePropertyChanged(); }
        }

        private string futures_direction;
        public string Futures_direction
        {
            get { return futures_direction; }
            set { futures_direction = value; RaisePropertyChanged(); }
        }

        private FutureHQ showHQ;
        public FutureHQ ShowHQ
        {
            get { return showHQ; }
            set { showHQ = value; RaisePropertyChanged(); }
        }


        public Dictionary<string, FutureHQ> HqDict { get; set; }

        private ObservableCollection<FutureHQ> hqList;
        public ObservableCollection<FutureHQ> HqList
        {
            get { return hqList; }
            set { hqList = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<RequestClass> ipList;
        public ObservableCollection<RequestClass> IpList
        {
            get { return ipList; }
            set { ipList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<RequestClass> latestIpList;
        public ObservableCollection<RequestClass> LatestIpList
        {
            get { return latestIpList; }
            set { latestIpList = value; RaisePropertyChanged(); }
        }


        public List<RequestClass> AllOrderList { get; set; }


        private ObservableCollection<RequestClass> alivedlatestIpList;
        public ObservableCollection<RequestClass> AlivedlatestIpList
        {
            get { return alivedlatestIpList; }
            set { alivedlatestIpList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<RequestClass> expiredlatestIpList;
        public ObservableCollection<RequestClass> ExpiredlatestIpList
        {
            get { return expiredlatestIpList; }
            set { expiredlatestIpList = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<FuturepositionQry_Output> accountIpList;
        public ObservableCollection<FuturepositionQry_Output> AccountIpList
        {
            get { return accountIpList; }
            set { accountIpList = value; RaisePropertyChanged(); }
        }



        private ObservableCollection<PairOrders> pairList;
        public ObservableCollection<PairOrders> PairList
        {
            get { return pairList; }
            set { pairList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<FutureCode> codeList;
        public ObservableCollection<FutureCode> CodeList
        {
            get { return codeList; }
            set { codeList = value; RaisePropertyChanged(); }
        }

        private FutureCode selectedCodeItem;
        public FutureCode SelectedCodeItem
        {
            get { return selectedCodeItem; }
            set { selectedCodeItem = value; RaisePropertyChanged(); }
        }

        private string traderColor;
        public string TraderColor
        {
            get { return traderColor; }
            set { traderColor = value; RaisePropertyChanged(); }
        }


        private string direction;
        public string Direction
        {
            get { return direction; }
            set { direction = value; RaisePropertyChanged(); }
        }

        private string price;
        public string Price
        {
            get { return price; }
            set { price = value; RaisePropertyChanged(); }
        }


        private decimal? amount;
        public decimal? Amount
        {
            get { return amount; }
            set { amount = value; RaisePropertyChanged(); }
        }

        private decimal? fishamount;
        public decimal? FishAmount
        {
            get { return fishamount; }
            set { fishamount = value; RaisePropertyChanged(); }
        }



        private ObservableCollection<DirectionDetail> directionSummaryCollection;
        public ObservableCollection<DirectionDetail> DirectionSummaryCollection
        {
            get { return directionSummaryCollection; }
            set { directionSummaryCollection = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<PairDetail> pariSummaryCollection;
        public ObservableCollection<PairDetail> PariSummaryCollection
        {
            get { return pariSummaryCollection; }
            set { pariSummaryCollection = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<FishParas> fishList;
        public ObservableCollection<FishParas> FishList
        {
            get { return fishList; }
            set { fishList = value; RaisePropertyChanged(); }
        }



        private bool? isFishOk;
        public bool? IsFishOk
        {
            get { return isFishOk; }
            set { isFishOk = value; RaisePropertyChanged(); }
        }

        private string mainColWidth_0 = "24*";
        public string MainColWidth_0
        {
            get { return mainColWidth_0; }
            set { mainColWidth_0 = value; RaisePropertyChanged(); }
        }

        private string mainColWidth_1 = "0*";
        public string MainColWidth_1
        {
            get { return mainColWidth_1; }
            set { mainColWidth_1 = value; RaisePropertyChanged(); }
        }

        #endregion


        #region 定义command
        public RelayCommand CommonNewOrderCommand { get; set; }

        public void CommonNewOrder()
        {
            if (IsAutoOC)
            {
                mq.Send(new RequestClass
                {
                    businessType = "1",
                    code = this.SelectedCodeItem.Code,
                    direction = direction,
                    original_price = this.Price,
                    entrust_price = this.Price,
                    entrust_amount = this.Amount.ToString(),
                    entrust_direction = direction,
                    addInfo3 = DateTime.Now.ToString(),
                    clordId = Guid.NewGuid().ToString("N")
                });

            }
            else
            {
                mq.SendOrder(new RequestClass
                {
                    businessType = "1",
                    code = this.SelectedCodeItem.Code,
                    direction = direction,
                    original_price = this.Price,
                    entrust_price = this.Price,
                    entrust_amount = this.Amount.ToString(),
                    entrust_direction = Entrust_direction,
                    futures_direction = Futures_direction,
                    addInfo3 = DateTime.Now.ToString(),
                    clordId = Guid.NewGuid().ToString("N")
                });
            }

            this.Amount = null;
            Global.ValidateDict["amount"] = false;
        }

        private bool CanNewOrderExcute()
        {
            return Global.ValidateDict["amount"];
        }


        public RelayCommand CommonCancelOrder { get; set; }

        public void CancelOrder()
        {
            foreach (var item in LatestIpList)
            {
                if (!(FinishedStateText.Contains(item.orderState) || item.entrust_no == "0" || item.error_info.Contains("已撤单")))
                {
                    if (item.IsSelected)
                    {
                        mq.SendOrder(new RequestClass
                        {
                            businessType = "0",
                            code = item.code,
                            user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                            entrust_no = item.entrust_no,
                            addInfo3 = DateTime.Now.ToString(),
                            clordId = Guid.NewGuid().ToString("N")
                        });
                    }
                }

            }
        }


        public RelayCommand CommonCancelAllOrder { get; set; }

        public void CancelAllOrder()
        {
            foreach (var item in LatestIpList)
            {
                if (!(FinishedStateText.Contains(item.orderState) || item.entrust_no == "0" || item.error_info.Contains("已撤单")))
                {
                    item.IsSelected = true;
                    mq.SendOrder(new RequestClass
                    {
                        businessType = "0",
                        code = item.code,
                        user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                        entrust_no = item.entrust_no,
                        addInfo3 = DateTime.Now.ToString(),
                        clordId = Guid.NewGuid().ToString("N")
                    });
                }

            }
        }



        public RelayCommand PairCommonNewOrderCommand { get; set; }

        public void PairCommonNewOrder()
        {
            PairOrders newPair = new PairOrders
            {
                LongRatio = this.LongRatio,
                LongMultiplier = this.longMultiplier,
                LongCode = this.SelectedLongCodeItem.Code,
                ShortRatio = this.ShortRatio,
                ShortMultiplier = this.ShortMultiplier,
                ShortCode = this.SelectedShortCodeItem.Code,
                Minus = this.MinusPrice,
                Pairs = this.Pairs,
                PairId = $"pair_{DateTime.Now.ToString("HHmmss")}_{Guid.NewGuid().ToString("N")}",
                queue_name = G.CONFIG.ReplyName + XmlHelper.GetInnerText(G.doc, "ClientName"),
                AddInfo1 = $"{LongRatio}*{SelectedLongCodeItem.Code}-{ShortRatio}*{SelectedShortCodeItem.Code}={MinusPrice}"

            };
            lock (_PairLock)
            {
                PairList.Insert(0, newPair);
                //PairList.Add(newPair);
                PairDb.Insertable(newPair).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();
            }

            this.Pairs = 0;
            Global.ValidateDict["Pairs"] = false;
        }

        private bool PairCanNewOrderExcute()
        {
            return Global.ValidateDict["LongRatio"] && Global.ValidateDict["ShortRatio"] && Global.ValidateDict["Pairs"] && Global.ValidateDict["LongMultiplier"] && Global.ValidateDict["ShortMultiplier"];
        }


        public RelayCommand CommonCancelPariOrder { get; set; }

        public void CancelPariOrder()
        {
            foreach (var item in PairList)
            {
                if (item.IsSelected && !item.IsDelete)
                {
                    item.IsDelete = true;
                    item.IsFinish = true;
                    PairDb.Insertable(item).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();

                    foreach (var order in LatestIpList)
                    {
                        if (order.addInfo2 == item.PairId)
                        {
                            if (!(FinishedStateText.Contains(order.orderState) || order.entrust_no == "0" || order.error_info.Contains("已撤单")))
                            {
                                item.IsSelected = true;
                                mq.SendOrder(new RequestClass
                                {
                                    businessType = "0",
                                    code = order.code,
                                    user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                                    entrust_no = order.entrust_no,
                                    addInfo3 = DateTime.Now.ToString(),
                                    clordId = Guid.NewGuid().ToString("N")
                                });
                            }
                        }
                    }

                }
            }
            DispatcherHelper.CheckBeginInvokeOnUI(() => { MV.PairsChange(); });
        }


        public RelayCommand CommonCancelAllPairsOrder { get; set; }

        public void CancelAllPairsOrder()
        {
            foreach (var item in PairList)
            {
                if (!item.IsDelete)
                {
                    item.IsDelete = true;
                    item.IsFinish = true;
                    PairDb.Insertable(item).IgnoreColumns(it => it.IsInDesignMode).ExecuteReturnIdentity();

                    foreach (var order in LatestIpList)
                    {
                        if (order.addInfo2 == item.PairId)
                        {
                            if (!(FinishedStateText.Contains(order.orderState) || order.entrust_no == "0" || order.error_info.Contains("已撤单")))
                            {
                                item.IsSelected = true;
                                mq.SendOrder(new RequestClass
                                {
                                    businessType = "0",
                                    code = order.code,
                                    user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                                    entrust_no = order.entrust_no,
                                    addInfo3 = DateTime.Now.ToString(),
                                    clordId = Guid.NewGuid().ToString("N")
                                });
                            }
                        }
                    }

                }
            }
            DispatcherHelper.CheckBeginInvokeOnUI(() => { MV.PairsChange(); });

        }

        public RelayCommand CommonInitialFish { get; set; }

        public void InitialFish()
        {
            IsFishOk = false;
            FishList.Clear();
            foreach (var hq in _hqlist)
            {
                //char[] charsToTrim = { '.', 'Z', 'J' };
                //string code = hq.SCode.TrimEnd(charsToTrim);

                string code = hq.SCode;
                double _lowerLimitPrice;
                double _upperLimitPrice;

                if (code.Contains('S'))
                {
                    _lowerLimitPrice = hq.LowerLimitPrice + 0.2d;
                    _upperLimitPrice = hq.UpperLimitPrice - 0.2d;
                }
                else if (code.Contains('F'))
                {
                    _lowerLimitPrice = hq.LowerLimitPrice + 0.5d;
                    _upperLimitPrice = hq.UpperLimitPrice - 0.5d;
                }
                else
                {
                    _lowerLimitPrice = hq.LowerLimitPrice + 1d;
                    _upperLimitPrice = hq.UpperLimitPrice - 1d;
                }


                FishList.Add(new FishParas()
                {
                    SCode = code,
                    PrePrice = hq.PrePrice,
                    BuyFishPrice = _lowerLimitPrice,
                    SellFishPrice = _upperLimitPrice,
                    IsSelected = true
                });
            }

        }


        public RelayCommand CommonSendFishOrders { get; set; }

        public void SendFishOrders()
        {
            foreach (var order in FishList)
            {
                if (order.IsSelected)
                {
                    mq.SendOrder(new RequestClass
                    {
                        businessType = "1",
                        code = order.SCode,
                        direction = "1",
                        original_price = order.BuyFishPrice.ToString(),
                        entrust_price = order.BuyFishPrice.ToString(),
                        entrust_amount = FishAmount.ToString(),
                        entrust_direction = "1",
                        futures_direction = "1",
                        addInfo3 = DateTime.Now.ToString(),
                        clordId = Guid.NewGuid().ToString("N")
                    });

                    mq.SendOrder(new RequestClass
                    {
                        businessType = "1",
                        code = order.SCode,
                        direction = "2",
                        original_price = order.SellFishPrice.ToString(),
                        entrust_price = order.SellFishPrice.ToString(),
                        entrust_amount = FishAmount.ToString(),
                        entrust_direction = "2",
                        futures_direction = "1",
                        addInfo3 = DateTime.Now.ToString(),
                        clordId = Guid.NewGuid().ToString("N")
                    });
                }

            }

        }

        #endregion
    }
}