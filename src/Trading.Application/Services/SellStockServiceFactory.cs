using Trading.Application.Models;
using Trading.Application.Services.Abstract;

namespace Trading.Application.Services;

public sealed class SellStockServiceFactory : ISellStockServiceFactory
{
    private readonly IEnumerable<ISellStockService> _sellStockServices;

    public SellStockServiceFactory(IEnumerable<ISellStockService> sellStockServices)
    {
        _sellStockServices = sellStockServices;
    }

    public ISellStockService GetInstance(SellStockStrategy strategy)
    {
        return _sellStockServices.FirstOrDefault(q => q.Strategy == strategy);
    }
}