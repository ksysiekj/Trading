using Microsoft.Extensions.Logging;
using Trading.Application.Exceptions;
using Trading.Application.Models;
using Trading.Application.Services.Abstract;
using Trading.Data.Repositories.Abstract;

namespace Trading.Application.Services
{
    public sealed class TradingService: ITradingService
    {
        private readonly ISellStockServiceFactory _sellStockServiceFactory;
        private readonly IStockRepository _stockRepository;
        private readonly ILogger _logger;

        public TradingService(IStockRepository stockRepository, ILoggerFactory loggerFactory, ISellStockServiceFactory sellStockServiceFactory)
        {
            _stockRepository = stockRepository;
            _logger = loggerFactory.CreateLogger(GetType());
            _sellStockServiceFactory = sellStockServiceFactory;
        }
        
        public SellStockResult SellStocks(SellStockRequest sellStockRequest)
        {
            // _logger.LogInformation();

            var stocks = _stockRepository.GetAllStocks();

            var sellStockService = _sellStockServiceFactory.GetInstance(sellStockRequest.Strategy);

            if (sellStockService == null)
            {
                throw new TradingException($"Unable to find handler for {sellStockRequest.Strategy} strategy");
            }

            var result = sellStockService.SellStocks(stocks, sellStockRequest);

            return result;
        }
    }
}