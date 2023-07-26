namespace Trading.Application.Models;

public enum SellStockStrategy
{
    Fifo,
    Lifo,
    AverageCost,
    LowestTaxExposure,
    HighestTaxExposure,
    LotBased
}