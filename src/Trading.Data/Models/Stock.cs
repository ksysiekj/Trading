namespace Trading.Data.Models
{
    public record Stock
    {
        public DateTime PurchaseDate { get; init; }
        public decimal PurchasePrice { get; init; }
        public int Count { get; init; }
        public string Currency => "USD";
    }
}