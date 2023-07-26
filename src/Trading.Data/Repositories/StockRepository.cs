using Trading.Data.Models;
using Trading.Data.Repositories.Abstract;

namespace Trading.Data.Repositories;

public sealed class StockRepository : IStockRepository
{
    public IReadOnlyList<Stock> GetAllStocks()
    {
        return new List<Stock>
        {
            new Stock { Count = 100, PurchaseDate = new DateTime(2023, 1, 1), PurchasePrice = 20 },
            new Stock { Count = 150, PurchaseDate = new DateTime(2023, 2, 1), PurchasePrice = 30 },
            new Stock { Count = 120, PurchaseDate = new DateTime(2023, 3, 1), PurchasePrice = 10 },
        }.AsReadOnly();
    }
}