namespace Trading.Application.Models;

internal struct RemainingStock
{
    public decimal PurchasePrice { get; init; }
    public int StockCount { get; init; }
}