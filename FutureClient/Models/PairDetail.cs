using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureClient.Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class PairDetail
    {
        public PairDetail()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string queue_name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string combo { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double minus { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double pairs { get; set; }



        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double longPosition { get; set; }



        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double shortPosition { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double longBalance { get; set; }



        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double shortBalance { get; set; }
        public double LongEx { get; set; }
        public string LongExString { get; set; } = "";
        public double LongExCost { get; set; }
        public double ShortEx { get; set; }
        public string ShortExString { get; set; } = "";
        public double ShortExCost { get; set; }
        public double LongRatio { get; set; }
        public double ShortRatio { get; set; }

        public double LongMultiplier { get; set; }
        public double ShortMultiplier { get; set; }
        public double sumcost { get; set; }

        public double LongExBalance { get; set; }
        public double ShortExBalance { get; set; }


        public string LongString { get; set; } = "";
        public string ShortString { get; set; } = "";

    }
}
