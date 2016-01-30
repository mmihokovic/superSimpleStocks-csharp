using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStocks
{
    public static class StockCalculator
    {
        /// <summary>
        /// Calculate Global Beverage Corporation Exchange All shares index
        /// </summary>
        /// <param name="trades">List of all performed trades on stock exchange.</param>
        /// <returns>All shares index</returns>
        public static decimal CalculateGbceAllShareIndex(List<Trade> trades)
        {
            return CalculateGeometricMean(trades.Select(t => t.Price).ToList());
        }

        /// <summary>
        /// Calculates share dividend.
        /// </summary>
        /// <param name="stock">Stock for dividend calculation</param>
        /// <param name="price">Stock price</param>
        /// <returns>Stock dividend</returns>
        public static decimal CalculateDividend(Stock stock, decimal price)
        {
            if (stock.Type == StockType.Common)
            {
                return CalculateDividendYieldCommon(stock.LastDividend, price);
            }
            else
            {
                return CalculateDividentYieldPreffered(stock.FixedDividend, stock.ParValue, price);
            }
        }

        /// <summary>
        /// Calculates P/E ration of selected stock.
        /// </summary>
        /// <param name="stock">Stock for calculation</param>
        /// <param name="price">Stock price</param>
        /// <returns>P/E ration of selected stock.</returns>
        public static decimal CalculatePeRatio(Stock stock, decimal price)
        {
            return CalculatePeRatio(stock.LastDividend, price);
        }

        /// <summary>
        /// Calculates share price based on previous trade on stock market.
        /// </summary>
        /// <param name="stock">Stock for calcuation</param>
        /// <param name="trades">All performed trades on stock marked.</param>
        /// <returns>Atock price for selected stock</returns>
        public static decimal CalculateStockPrice(Stock stock, List<Trade> trades)
        {
            var stockTrades = trades.Where(t => t.Stock == stock).ToList();
            return stockTrades.Sum(t => t.Price * t.Quantity) / stockTrades.Sum(t => t.Quantity);
        }

        private static decimal CalculateDividendYieldCommon(decimal lastDividend, decimal ticketPrice)
        {
            return lastDividend / ticketPrice; 
        }

        private static decimal CalculateDividentYieldPreffered(decimal fixedDividend, decimal parValue, decimal tickerPrice)
        {
            return (fixedDividend*parValue)/tickerPrice;
        }

        private static decimal CalculatePeRatio(decimal dividend, decimal tickedPrice)
        {
            return dividend != 0 ? tickedPrice / dividend : 0;
        }

        private static decimal CalculateGeometricMean(List<decimal> tradePrices)
        {
            if (tradePrices == null || !tradePrices.Any())
            {
                return 0m;
            }

            double sum = 0;
            tradePrices.ForEach(t => sum += Math.Log((double)t, 2));
            sum *= 1.0 / tradePrices.Count;
            var geometricMean = Math.Pow(2.0, sum);
           
            return (decimal)geometricMean;
        }
    }
}
