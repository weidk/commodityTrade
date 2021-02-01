using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace FutureClient
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserInfo
    {

        [SugarColumn(IsPrimaryKey = true)]
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string USERNAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PASSWORD {get;set;}

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string account_code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string asset_no { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string combi_no { get; set; }


        public string token { get; set; } = null;

    }
}
