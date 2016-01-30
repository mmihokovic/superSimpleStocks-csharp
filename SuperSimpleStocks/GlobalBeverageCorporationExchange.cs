using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    /// <summary>
    /// Class for simulating trade on Global Beverage Corporation Exchange
    /// </summary>
    public class GlobalBeverageCorporationExchange
    {
        public List<Stock> Stocks { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stocks">Stock avaliable for trading</param>
        /// <param name="startTime">Stock exchange open date and time</param>
        /// <param name="endTime">Stock exchange close date and time</param>
        public GlobalBeverageCorporationExchange(List<Stock> stocks, DateTime startTime, DateTime endTime)
        {
            Stocks = stocks;
            StartTime = startTime;
            EndTime = endTime;
        }

        /// <summary>
        /// Perform trade on stock exchange.
        /// </summary>
        /// <returns>List of performed trades.</returns>
        public List<Trade> Trade()
        {
            var rand = new Random();
            var tradesCount = rand.Next((int)(EndTime - StartTime).TotalSeconds);
            var tradePeriod = (EndTime - StartTime).TotalSeconds / tradesCount;

            var trades = new List<Trade>();
            for (int i = 0; i < tradesCount; i++)
            {
                var stock = Stocks.ElementAt(rand.Next(Stocks.Count));

                var tradeAction = (rand.NextDouble() >= 0.5) ? TradeAction.Buy : TradeAction.Sell;
                var price = stock.ParValue*(decimal) 0.5 +
                            (decimal) (rand.NextDouble()*(double) (stock.ParValue*2 - stock.ParValue*(decimal) 0.5));
                var quantity = rand.Next(1, 1000);
                var tradeTime = StartTime.AddSeconds(i*tradePeriod);
                var trade = new Trade(stock, tradeTime, quantity, tradeAction, price);
                
                trades.Add(trade); 
            }
            return trades;
        }
    }
}
