using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureClient.Models
{
    public class FutureInfo
    {
        public string S_INFO_NAME { get; set; }
        public string S_INFO_CODE { get; set; }
        public string S_INFO_EXCHMARKET { get; set; }
        public string FS_INFO_SCCODE { get; set; }
        public string S_INFO_LISTDATE { get; set; }
        public string S_INFO_DELISTDATE { get; set; }
        public string FS_INFO_DLMONTH { get; set; }
        public string FS_INFO_LTDLDATE { get; set; }
        public string S_INFO_FULLNAME { get; set; }
        public string HS_MARKET_CODE { get; set; } = "7";
        public double CHANGE_TICK { get; set; } = 1;
    }
}
