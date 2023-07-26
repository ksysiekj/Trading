using Microsoft.Extensions.DependencyInjection;
using Trading.Application.Services;
using Trading.Application.Services.Abstract;
using Trading.Data.Repositories;
using Trading.Data.Repositories.Abstract;

namespace Trading.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IStockRepository, StockRepository>();
            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ITradingService, TradingService>();
            services.AddSingleton<ISellStockService, FifoSellStockService>();
            services.AddSingleton<ISellStockServiceFactory, SellStockServiceFactory>();

            return services;
        }
    }
}
