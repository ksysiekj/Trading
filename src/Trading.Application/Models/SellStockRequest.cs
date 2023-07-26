using Trading.Application.Exceptions;

namespace Trading.Application.Models
{
    public struct SellStockRequest
    {
        public int NumberOfStocks { get; init; }
        public decimal Price { get; init; }
        public SellStockStrategy Strategy { get; init; }

        internal void Validate()
        {
            if (NumberOfStocks <= 0)
            {
                throw new SellStockValidationException($"Invalid request: {nameof(NumberOfStocks)} cannot be less than 0");
            }
            
            if (Price <= 0)
            {
                throw new SellStockValidationException($"Invalid request: {nameof(Price)} cannot be less than 0");
            }
        }
    }
}
