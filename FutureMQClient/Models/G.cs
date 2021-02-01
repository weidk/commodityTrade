using SqlSugar;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FutureMQClient.Models
{
    public static class G
    {
        public static SqlSugarClient db;

        public static List<RequestClass> OrderLists = new List<RequestClass>();

        public static Dictionary<String, RequestClass> OrderDicts = new Dictionary<string, RequestClass>();

        public static Dictionary<string, Account> UserDict = new Dictionary<string, Account>();

        public static List<string> FinishedState = new List<string> { "5", "7", "8", "9", "F", "E" };

        public static XmlDocument doc = XmlHelper.ReadXml(@"config.xml");

        public static IDatabase rClient;

        public static IDatabase rClientHQ;

        public static List<FuturepositionQry_Output> PosList = new List<FuturepositionQry_Output>();

        public static string posString = "";

        public static ConfigPars CONFIG;
        public static SqlSugarClient GetInstance(string connstr)
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = ConfigurationManager.AppSettings[connstr],
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            return db;
        }

    }
}
