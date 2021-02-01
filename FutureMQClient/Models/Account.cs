using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureMQClient.Models
{
    public class Account
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string key_id { get; set; }
        public string queue_name { get; set; }
        public string user { get; set; }
        public string code { get; set; }
        public string direction { get; set; }
        public double total_amount { get; set; }
        public double enable_amount { get; set; }
        public double total_balance { get; set; }
        public double cost { get; set; }
        public double total_profit { get; set; }
        public double float_profit { get; set; }
        public double real_profit { get; set; }
        public DateTime update_time { get; set; }


    }
}
