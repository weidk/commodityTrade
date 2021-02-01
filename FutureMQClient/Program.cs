using FutureMQClient.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FutureMQClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MQClient mq = new MQClient();
            mq.OnReceiveMsg += (msg) =>
            {
            };
            mq.OnReceiveHQ += (hq) =>
            {
                Console.WriteLine(hq.NewPrice);
            };


                while (true)
                {
                    try
                    {
                        Console.WriteLine("输入业务类型：");
                        string businessType = Console.ReadLine();
                    if (businessType == "1")
                    {
                        Console.WriteLine("输入代码：");
                        string code = Console.ReadLine();

                        Console.WriteLine("输入方向：");
                        string direction = Console.ReadLine();

                        Console.WriteLine("输入价格：");
                        string price = Console.ReadLine();

                        Console.WriteLine("输入数量：");
                        string amount = Console.ReadLine();
                        mq.SendOrder(new Models.RequestClass
                        {
                            businessType = "1",
                            user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                            code = code,
                            direction = direction,
                            original_price = price,
                            entrust_amount = amount,
                            clordId = DateTime.Now.ToString("hhmmss")
                        });
                    }
                    else
                    {
                        Console.WriteLine("输入委托编号：");
                        string entrust_no = Console.ReadLine();
                        mq.SendOrder(new Models.RequestClass
                        {
                            businessType = "0",
                            user = XmlHelper.GetInnerText(G.doc, "ClientName"),
                            entrust_no= entrust_no,
                            clordId = DateTime.Now.ToString("hhmmss")
                        });
                    }

                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                }
                finally
                {
                    Console.WriteLine("****************************************");
                    Thread.Sleep(1000);
                }
                }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}
