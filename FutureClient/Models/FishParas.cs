using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureClient.Models
{
    public class FishParas
    {
        public string SCode { get; set; }
        public double PrePrice { get; set; }
        public double? BuyFishPrice { get; set; }
        public int BuyFishAmt { get; set; } = 30;
        //public double BuyFishThreshold { get; set; }
        public double? SellFishPrice { get; set; }

        public int SellFishAmt { get; set; } = 30;
        //public double SellFishThreshold { get; set; }
        public bool IsSelected { get; set; } = true;

    }
}
