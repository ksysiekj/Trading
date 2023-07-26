using Trading.Application.Models;

namespace Trading.Application.Services.Abstract;

public interface ISellStockServiceFactory
{
    ISellStockService? GetInstance(SellStockStrategy strategy);
}