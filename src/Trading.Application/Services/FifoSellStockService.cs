using Microsoft.Extensions.Logging;
using Trading.Application.Models;
using Trading.Application.Services.Abstract;
using Trading.Data.Models;

namespace Trading.Application.Services;

public sealed class FifoSellStockService : SellStockServiceBase
{
    public FifoSellStockService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    public override SellStockStrategy Strategy => SellStockStrategy.Fifo;

    protected override SellStockResult SellStocksInternal(IReadOnlyList<Stock> stocks, SellStockRequest sellStockRequest)
    {
        var remainingNumberOfStocks = sellStockRequest.NumberOfStocks;

        var orderedStocks = stocks.OrderBy(q => q.PurchaseDate).ToList();
        var monthCounter = 0;

        var sellStockParts = new List<SellStockPart>();
        var remainingStocks = new List<RemainingStock>();

        while (remainingNumberOfStocks > 0)
        {
            var tmpStock = orderedStocks[monthCounter++];

            var stockCount = 0;

            if (remainingNumberOfStocks >= tmpStock.Count)
            {
                stockCount = tmpStock.Count;
                remainingNumberOfStocks -= tmpStock.Count;
            }
            else
            {
                stockCount = remainingNumberOfStocks;
                remainingNumberOfStocks -= remainingNumberOfStocks;
            }

            sellStockParts.Add(new SellStockPart
            {
                SellPrice = sellStockRequest.Price,
                PurchasePrice = tmpStock.PurchasePrice,
                StockCount = stockCount
            });

            if (remainingNumberOfStocks == 0)
            {
                remainingStocks = orderedStocks.Where((_, idx) => idx >= monthCounter).Select(s => new RemainingStock
                        { PurchasePrice = s.PurchasePrice, StockCount = s.Count })
                    .ToList();
                remainingStocks.Add(new RemainingStock
                    { PurchasePrice = tmpStock.PurchasePrice, StockCount = tmpStock.Count - stockCount });

                break;
            }
        }

        var remainingStocksSum= remainingStocks.Sum(s => s.StockCount);

        var result = new SellStockResult
        {
            TotalProfit = sellStockParts.Sum(q => q.Profit),
            RemainingNumberOfStocks = stocks.Sum(q => q.Count) - sellStockParts.Sum(q => q.StockCount),
            CostBasisPerStockOfSoldStocks = sellStockParts.Sum(q => q.StockCount * q.PurchasePrice) /
                                            sellStockParts.Sum(q => q.StockCount),
            CostBasisPerStockOfRemainingStocks = remainingStocksSum == 0
                ? 0
                : remainingStocks.Sum(w => w.StockCount * w.PurchasePrice) /
                  remainingStocksSum
        };

        return result;
    }
}