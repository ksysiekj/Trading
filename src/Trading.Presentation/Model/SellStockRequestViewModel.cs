using Trading.Application.Exceptions;

namespace Trading.Presentation.Model
{
    internal struct SellStockRequestViewModel
    {
        public int NumberOfStocks { get; init; }
        public decimal Price { get; init; }

        internal void Validate()
        {
            if (NumberOfStocks <= 0)
            {
                throw new SellStockValidationException($"Invalid request: {nameof(NumberOfStocks)} cannot be less than 0");
            }

            if (NumberOfStocks >= 1_000_000)
            {
                throw new SellStockValidationException($"Invalid request: {nameof(NumberOfStocks)} cannot be greater than {1_000_000}");
            }

            if (Price <= 0)
            {
                throw new SellStockValidationException($"Invalid request: {nameof(Price)} cannot be less than 0");
            }
        }
    }
}
