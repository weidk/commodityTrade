using GalaSoft.MvvmLight;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureClient.Models
{
    public class PairOrders: ViewModelBase
    {
        public bool IsSelected { get; set; } = false;
        public double LongRatio { get; set; }
        public string LongCode { get; set; }
        
        public double ShortRatio { get; set; }
        public string ShortCode { get; set; }
        public double? Minus { get; set; }
        public double Pairs { get; set; }
        


        private bool isTrigger { get; set; }=false;
        public bool IsTrigger
        {
            get { return isTrigger; }

            set
            {
                isTrigger = value;
                RaisePropertyChanged("IsTrigger");
            }
        }

        private bool isDelete { get; set; } = false;
        public bool IsDelete
        {
            get { return isDelete; }

            set
            {
                isDelete = value;
                RaisePropertyChanged("IsDelete");
            }
        }


        //[SugarColumn(IsPrimaryKey = true)]
        public string PairId { get; set; }

        private string addInfo1 { get; set; }
        public string AddInfo1
        {
            get { return addInfo1; }

            set
            {
                addInfo1 = value;
                RaisePropertyChanged("AddInfo1");
            }
        }


        private string addInfo2 { get; set; }
        public string AddInfo2
        {
            get { return addInfo2; }

            set
            {
                addInfo2 = value;
                RaisePropertyChanged("AddInfo2");
            }
        }

        public string queue_name { get; set; }


        private double finishedPairs { get; set; } = 0;
        public double FinishedPairs
        {
            get { return finishedPairs; }

            set
            {
                finishedPairs = value;
                RaisePropertyChanged("FinishedPairs");
            }
        }

        private double sendedPairs { get; set; } = 0;
        public double SendedPairs
        {
            get { return sendedPairs; }

            set
            {
                sendedPairs = value;
                RaisePropertyChanged("SendedPairs");
            }
        }


        private double progress { get; set; } = 0;
        public double Progress
        {
            get { return progress; }

            set
            {
                progress = value;
                RaisePropertyChanged("Progress");
            }
        }


        private bool isFinish { get; set; }=false;
        public bool IsFinish
        {
            get { return isFinish; }

            set
            {
                isFinish = value;
                RaisePropertyChanged("IsFinish");
            }
        }


        private double finishedLongLeg;
        public double FinishedLongLeg
        {
            get { return finishedLongLeg; }

            set
            {
                finishedLongLeg = value;
                RaisePropertyChanged("FinishedLongLeg");
            }
        }


        private double finishedShortLeg;
        public double FinishedShortLeg
        {
            get { return finishedShortLeg; }

            set
            {
                finishedShortLeg = value;
                RaisePropertyChanged("FinishedShortLeg");
            }
        }


        private double finishedLongLegAmt;
        public double FinishedLongLegAmt
        {
            get { return finishedLongLegAmt; }

            set
            {
                finishedLongLegAmt = value;
                RaisePropertyChanged("FinishedLongLegAmt");
            }
        }



        private double finishedShortLegAmt;
        public double FinishedShortLegAmt
        {
            get { return finishedShortLegAmt; }

            set
            {
                finishedShortLegAmt = value;
                RaisePropertyChanged("FinishedShortLegAmt");
            }
        }


        private double meanCost;
        public double MeanCost
        {
            get { return meanCost; }

            set
            {
                meanCost = value;
                RaisePropertyChanged("MeanCost");
            }
        }

        private double longPartDeal;
        public double LongPartDeal
        {
            get { return longPartDeal; }

            set
            {
                longPartDeal = value;
                RaisePropertyChanged("LongPartDeal");
            }
        }


        private double shortPartDeal;
        public double ShortPartDeal
        {
            get { return shortPartDeal; }

            set
            {
                shortPartDeal = value;
                RaisePropertyChanged("ShortPartDeal");
            }
        }



        private double longPartDealBalance;
        public double LongPartDealBalance
        {
            get { return longPartDealBalance; }

            set
            {
                longPartDealBalance = value;
                RaisePropertyChanged("LongPartDealBalance");
            }
        }


        private double shortPartDealBalance;
        public double ShortPartDealBalance
        {
            get { return shortPartDealBalance; }

            set
            {
                shortPartDealBalance = value;
                RaisePropertyChanged("ShortPartDealBalance");
            }
        }


        private double currentMinus;
        public double CurrentMinus
        {
            get { return currentMinus; }

            set
            {
                currentMinus = value;
                RaisePropertyChanged("CurrentMinus");
            }
        }


        private bool isPause { get; set; } = false;
        public bool IsPause
        {
            get { return isPause; }

            set
            {
                isPause = value;
                RaisePropertyChanged("IsPause");
            }
        }

        public DateTime OrderSendTime { get; set; } = DateTime.Now;

        public double LongEx { get; set; } = 0;
        public double LongExBalance { get; set; } = 0;
        public double ShortEx { get; set; } = 0;
        public double ShortExBalance { get; set; } = 0;


        public double LongMultiplier { get; set; }

        public double ShortMultiplier { get; set; }
    }
}
