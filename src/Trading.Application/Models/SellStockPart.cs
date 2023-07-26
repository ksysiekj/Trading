namespace Trading.Application.Models;

internal struct SellStockPart
{
    public decimal PurchasePrice { get; init; }
    public decimal SellPrice { get; init; }
    public int StockCount { get; init; }
    public decimal Profit => StockCount * (SellPrice - PurchasePrice);
}