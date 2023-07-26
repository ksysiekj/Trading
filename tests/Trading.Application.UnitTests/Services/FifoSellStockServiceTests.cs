using System.Collections;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Trading.Application.Exceptions;
using Trading.Application.Models;
using Trading.Application.Services;
using Trading.Application.Services.Abstract;
using Trading.Data.Models;
using Xunit;

namespace Trading.Application.UnitTests.Services
{
    public class FifoSellStockServiceTests
    {
        private static ISellStockService PrepareSut(ILoggerFactory loggerFactory = null)
        {
            loggerFactory = loggerFactory ?? PrepareLoggerFactory();

            return new FifoSellStockService(loggerFactory);
        }

        private static ILoggerFactory PrepareLoggerFactory()
        {
            var service = Substitute.For<ILoggerFactory>();

            return service;
        }

        private static IReadOnlyList<Stock> PrepareStocks()
        {
            return new List<Stock>
            {
                new Stock { Count = 100, PurchaseDate = new DateTime(2023, 1, 1), PurchasePrice = 20 },
                new Stock { Count = 200, PurchaseDate = new DateTime(2023, 2, 1), PurchasePrice = 30 }
            }.AsReadOnly();
        }

        [Fact]
        public void SellStocks_NumberOfStocks_IsNegative_ThrowsSellStockValidationException()
        {
            var sut = PrepareSut();

            Assert.Throws<SellStockValidationException>(() => sut.SellStocks(new List<Stock>(), new SellStockRequest { NumberOfStocks = -20 }));
        }

        [Fact]
        public void SellStocks_SellPrice_IsNegative_ThrowsSellStockValidationException()
        {
            var sut = PrepareSut();

            Assert.Throws<SellStockValidationException>(() =>
                sut.SellStocks(new List<Stock>(), new SellStockRequest { NumberOfStocks = 20, Price = -20m }));
        }

        [Fact]
        public void SellStocks_NotEnoughStocksAvailable_ThrowsSellStockValidationException()
        {
            var sut = PrepareSut();

            Assert.Throws<SellStockValidationException>(() =>
                sut.SellStocks(new List<Stock>
                {
                    new Stock { Count = 100 }
                }, new SellStockRequest { NumberOfStocks = 101, Price = 20m }));
        }

        [Theory]
        [ClassData(typeof(SellStocksTestData))]
        public void SellStocks(decimal price, int numberOfStocks, int remainingNumberOfStocks,
            decimal costBasisPerStockOfSoldStocks,
            decimal costBasisPerStockOfRemainingStocks, decimal totalProfit)
        {
            var sut = PrepareSut();

            var result = sut.SellStocks(PrepareStocks(),
                new SellStockRequest
                    { Price = price, NumberOfStocks = numberOfStocks, Strategy = SellStockStrategy.Fifo });

            Assert.Equal(remainingNumberOfStocks, result.RemainingNumberOfStocks);
            Assert.Equal(costBasisPerStockOfSoldStocks, Math.Round(result.CostBasisPerStockOfSoldStocks,2));
            Assert.Equal(costBasisPerStockOfRemainingStocks, Math.Round(result.CostBasisPerStockOfRemainingStocks,2));
            Assert.Equal(totalProfit, Math.Round(result.TotalProfit,2));
        }

        internal class SellStocksTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 40m, 150, 150, 23.33m, 30, 2500 };
                yield return new object[] { 25m, 300, 0, 26.67m, 0, -500 };
                yield return new object[] { 5m, 278, 22, 26.4m, 30, -5950 };
                yield return new object[] { 42m, 175, 125, 24.29m, 30, 3100 };
                yield return new object[] { 22.5m, 5, 295, 20m, 26.78m, 12.5m };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}