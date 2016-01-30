using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    /// <summary>
    /// Class for modeling stock.
    /// </summary>
    public class Stock
    {
        public string Symbol { get; set; }
        public StockType Type { get; set; }
        public decimal LastDividend { get; set; }
        public decimal FixedDividend { get; set; }
        public decimal ParValue { get; set; }

        public Stock(string symbol, StockType type, decimal lastDividend, decimal fixedDividend, decimal parValue)
        {
            Symbol = symbol;
            Type = type;
            LastDividend = lastDividend;
            FixedDividend = fixedDividend;
            ParValue = parValue;
        }
    }
}
