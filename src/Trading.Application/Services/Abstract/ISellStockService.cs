using Trading.Application.Models;
using Trading.Data.Models;

namespace Trading.Application.Services.Abstract;

public interface ISellStockService
{
    public SellStockStrategy Strategy { get; }

    SellStockResult SellStocks(IReadOnlyList<Stock> stocks, SellStockRequest sellStockRequest);
}