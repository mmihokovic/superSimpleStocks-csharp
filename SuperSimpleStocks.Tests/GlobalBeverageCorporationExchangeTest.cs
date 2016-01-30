using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperSimpleStocks.Tests
{
    [TestClass]
    public class GlobalBeverageCorporationExchangeTest
    {

        [TestMethod]
        public void TradeTest()
        {
            var stocks = new List<Stock>();
            stocks.Add(new Stock("TEA", StockType.Common, 0, 0, 100));
            stocks.Add(new Stock("POP", StockType.Common, 8, 0, 100));
            stocks.Add(new Stock("ALE", StockType.Common, 23, 0, 60));
            stocks.Add(new Stock("GIN", StockType.Preferred, 8, 2, 100));
            stocks.Add(new Stock("JOE", StockType.Common, 13, 0, 250));
            var exchange = new GlobalBeverageCorporationExchange(stocks, DateTime.Today.Date, DateTime.Now);
            var trades = exchange.Trade();

            Assert.IsNotNull(trades);
            Assert.IsTrue(trades.Count > 0);

            Assert.IsFalse(trades.AsQueryable().Any(t => t.Price == 0));
            Assert.IsFalse(trades.AsQueryable().Any(t => t.Quantity == 0));
            Assert.IsFalse(trades.AsQueryable().Any(t => t.Quantity == 0));
            Assert.IsTrue(trades.AsQueryable().Any(t => t.Action == TradeAction.Buy));
            Assert.IsTrue(trades.AsQueryable().Any(t => t.Action == TradeAction.Sell));
        }
    }
}
