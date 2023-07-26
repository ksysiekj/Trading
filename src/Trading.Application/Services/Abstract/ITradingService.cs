using Trading.Application.Models;

namespace Trading.Application.Services.Abstract
{
    public interface ITradingService
    {
        SellStockResult SellStocks(SellStockRequest sellStockRequest);
    }
}
