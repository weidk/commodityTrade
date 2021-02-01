using FutureMQClient.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureClient
{
    public class Global
    {
        public static Dictionary<string, bool> ValidateDict { get; set; } = new Dictionary<string, bool>();
    }
}
