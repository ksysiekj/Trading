using Trading.Data.Models;

namespace Trading.Data.Repositories.Abstract;

public interface IStockRepository
{
    IReadOnlyList<Stock> GetAllStocks();
}