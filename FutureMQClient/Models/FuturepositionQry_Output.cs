using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutureMQClient.Models
{
    public class FuturepositionQry_Output
    {
        public string out_error_no { get; set; }
        public string out_error_info { get; set; }
        public string out_account_code { get; set; }//C32	账户编号
        public string out_asset_no { get; set; }//C16	资产单元编号
        public string out_combi_no { get; set; }//C8	组合编号
        public string out_market_no { get; set; }//C3	交易市场
        public string out_stock_code { get; set; }//C16	证券代码
        public string out_stockholder_id { get; set; }//	C20	股东代码
        public string out_hold_seat { get; set; }//	C6	持仓席位
        public string out_position_flag { get; set; }//	C1	多空标志
        public string out_invest_type { get; set; }//C1	投资类型
        public string out_current_amount { get; set; }//	N16	当前数量
        public string out_enable_amount { get; set; }//N16	可用数量
        public string out_begin_cost { get; set; }//	N16.2	期初成本
        public string out_current_cost { get; set; }//N16.2	当前成本
        public string out_current_cost_price { get; set; }//	N9.3	当前成本价
        public string out_pre_buy_amount { get; set; }//	N16	开仓挂单数量
        public string out_pre_sell_amount { get; set; }//N16	平仓挂单数量
        public string out_pre_buy_balance { get; set; }//N16.2	开仓挂单金额
        public string out_pre_sell_balance { get; set; }//	N16.2	平仓挂单金额
        public string out_buy_amount { get; set; }//N16	当日开仓数量
        public string out_sell_amount { get; set; }//N16	当日平仓数量
        public string out_buy_balance { get; set; }//N16.2	当日开仓金额
        public string out_sell_balance { get; set; }//N16.2	当日平仓金额
        public string out_buy_fee { get; set; }//	N16.2	当日开仓费用
        public string out_sell_fee { get; set; }//N16.2	当日平仓费用
        public string out_accumulate_profit { get; set; }//N16.2	实现盈亏

    }
}
