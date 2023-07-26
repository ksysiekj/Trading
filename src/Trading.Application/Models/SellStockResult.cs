namespace Trading.Application.Models;

public struct SellStockResult
{
    public int RemainingNumberOfStocks { get; init; }
    public decimal CostBasisPerStockOfSoldStocks { get; init; }
    public decimal CostBasisPerStockOfRemainingStocks { get; init; }
    public decimal TotalProfit { get; init; }

    public override string ToString()
    {
        return $"{nameof(RemainingNumberOfStocks)}:\t{RemainingNumberOfStocks},{Environment.NewLine}"
               + $"{nameof(CostBasisPerStockOfSoldStocks)}:\t{Math.Round(CostBasisPerStockOfSoldStocks, 2)},{Environment.NewLine}"
               + $"{nameof(CostBasisPerStockOfRemainingStocks)}:\t{Math.Round(CostBasisPerStockOfRemainingStocks, 2)},{Environment.NewLine}"
               + $"{nameof(TotalProfit)}:\t{Math.Round(TotalProfit, 2)}";
    }
}