using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperSimpleStocks.Tests
{
    [TestClass]
    public class StockCalculatorTests
    {
        [TestMethod]
        public void CalculateGbceAllShareIndexTest()
        {
            var stock = new Stock("TEA", StockType.Common, 0, 0, 100);
            
            var trades = new List<Trade>();
            trades.Add(new Trade(stock, DateTime.Now, 5, TradeAction.Buy, 10));
            trades.Add(new Trade(stock, DateTime.Now, 10, TradeAction.Buy, 100));

            var gbceAllShareIndex = StockCalculator.CalculateGbceAllShareIndex(trades);
            AssertDiff(31.62277660168379m, gbceAllShareIndex);
        }

        [TestMethod]
        public void CalculateDividendTest()
        {
            var stockCommon = new Stock("TEA", StockType.Common, 10, 0, 100);
            var dividendCommon = StockCalculator.CalculateDividend(stockCommon, 100m);
            AssertDiff(0.1m, dividendCommon);

            var stockPreferred = new Stock("TEA", StockType.Preferred, 10, 5, 100);
            var dividendPreffered = StockCalculator.CalculateDividend(stockPreferred, 100);
            AssertDiff(5m, dividendPreffered);
        }

        [TestMethod]
        public void CalculatePeRatioTest()
        {
            var stock = new Stock("TEA", StockType.Common, 10, 0, 100);
            var peRatio = StockCalculator.CalculatePeRatio(stock, 105);
            AssertDiff(10.5m, peRatio );

            var stockWithZeroDividend = new Stock("TEA", StockType.Common, 0, 0, 100);
            var peRatioWithZeroDividend = StockCalculator.CalculatePeRatio(stockWithZeroDividend, 105);
            AssertDiff(0m, peRatioWithZeroDividend);
        }

        [TestMethod]
        public void CalculateStockPriceTest()
        {
            var stock = new Stock("TEA", StockType.Common, 0, 0, 100);

            var trades = new List<Trade>();
            trades.Add(new Trade(stock, DateTime.Now, 5, TradeAction.Buy, 50));
            trades.Add(new Trade(stock, DateTime.Now, 10, TradeAction.Buy, 100));
            trades.Add(new Trade(stock, DateTime.Now, 10, TradeAction.Sell, 100));

            var stockPrice = StockCalculator.CalculateStockPrice(stock, trades);
            AssertDiff(90, stockPrice);
        }

        void AssertDiff(decimal a, decimal b, decimal diff = 0.0001m)
        {
            Assert.IsTrue(Math.Abs(a - b) < diff);
        }
    }
}
