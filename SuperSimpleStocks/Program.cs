using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    class Program
    {
        static void Main(string[] args)
        {
            // define stocks
            var stocks = new List<Stock>();
            stocks.Add(new Stock("TEA", StockType.Common, 0, 0, 100));
            stocks.Add(new Stock("POP", StockType.Common, 8, 0, 100));
            stocks.Add(new Stock("ALE", StockType.Common, 23, 0, 60));
            stocks.Add(new Stock("GIN", StockType.Preferred, 8, 2, 100));
            stocks.Add(new Stock("JOE", StockType.Common, 13, 0, 250));

            // trade on stock exchange
            var tradePeriodInMinutes = 15;
            var exchangeStartTime = DateTime.Now;
            var exchangeStopTime = exchangeStartTime.AddMinutes(tradePeriodInMinutes);
            var exchange = new GlobalBeverageCorporationExchange(stocks, exchangeStartTime, exchangeStopTime);
            
            // display trade info
            var trades = exchange.Trade();
            Console.WriteLine("Exchange opened at: {0}", exchangeStartTime);
            Console.WriteLine("Exchange closed at: {0} with {1} trades.", exchangeStopTime, trades.Count);
            Console.WriteLine("\nLast 5 trades are:");
            foreach (var trade in trades.Skip(Math.Max(0, trades.Count() - 5)))
            {
                Console.WriteLine("Stock {0} {1} at {2} with price {3}", trade.Stock.Symbol, trade.Action, trade.TradeTime, trade.Price.ToString("F2"));
            }

            // display stock statistics
            Console.WriteLine("\nStatistics:");
            Console.WriteLine("|Stock name\t|Dividend yield\t|P\\E ratio\t|Stock price\t|");
            foreach (var stock in stocks)
            {
                var stockPrice = StockCalculator.CalculateStockPrice(stock, trades);
                var dividendYield = StockCalculator.CalculateDividend(stock, stockPrice);
                var peRatio = StockCalculator.CalculatePeRatio(stock, stockPrice);

                Console.WriteLine("|{0}\t\t|{1}\t\t|{2}\t\t|{3}\t\t|", stock.Symbol, dividendYield.ToString("F2"),
                    peRatio.ToString("F2"), stockPrice.ToString("F2"));
            }

            var gbceAllShareIndex = StockCalculator.CalculateGbceAllShareIndex(trades);
            Console.WriteLine("\nGlobal Beverage Corporation Exchange All share index: {0}", gbceAllShareIndex);

            //prevent autoclose of cmd
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
