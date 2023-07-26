using Microsoft.Extensions.Logging;
using Trading.Application.Exceptions;
using Trading.Application.Models;
using Trading.Data.Models;

namespace Trading.Application.Services.Abstract;

public abstract class SellStockServiceBase : ISellStockService
{
    protected readonly ILogger _logger;

    protected SellStockServiceBase(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public abstract SellStockStrategy Strategy { get; }


    public SellStockResult SellStocks(IReadOnlyList<Stock> stocks, SellStockRequest sellStockRequest)
    {
        // _logger.LogInformation();

        ValidateRequest(stocks, sellStockRequest);

        var result = SellStocksInternal(stocks, sellStockRequest);

        // _logger.LogInformation();

        return result;
    }

    protected abstract SellStockResult SellStocksInternal(IReadOnlyList<Stock> stocks, SellStockRequest sellStockRequest);

    private void ValidateRequest(IReadOnlyList<Stock> stocks, SellStockRequest sellStockRequest)
    {
        sellStockRequest.Validate();

        var totalStock = stocks.Sum(q => q.Count);

        if (totalStock < sellStockRequest.NumberOfStocks)
        {
            throw new SellStockValidationException($"Invalid request: cannot sell {sellStockRequest.NumberOfStocks} stocks are there are {totalStock} available");
        }
    }
}