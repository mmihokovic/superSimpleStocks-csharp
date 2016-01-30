using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    /// <summary>
    /// Class for modeling a single trade on stock exchange.
    /// </summary>
    public class Trade
    {
        public Stock Stock { get; set; }
        public DateTime TradeTime { get; set; }
        public decimal Quantity { get; set; }
        public TradeAction Action { get; set; }
        public decimal Price { get; set; }

        public Trade(Stock stock, DateTime tradeTime, decimal quantity, TradeAction action, decimal price)
        {
            Stock = stock;
            TradeTime = tradeTime;
            Quantity = quantity;
            Action = action;
            Price = price;
        }
    }
}
