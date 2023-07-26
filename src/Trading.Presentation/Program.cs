using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trading.Application.Exceptions;
using Trading.Application.Extensions;
using Trading.Application.Models;
using Trading.Application.Services.Abstract;
using Trading.Presentation.Model;

namespace Trading.Presentation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(_=>_.AddConsole())
                .RegisterRepositories()
                .RegisterServices()
                .BuildServiceProvider();
            
            RunApplication(serviceProvider.GetService<ITradingService>());
        }

        private static void RunApplication(ITradingService tradingService)
        {
            char @continue = 'y';

            do
            {
                try
                {
                    var requestViewModel = PromptUser();
                    requestViewModel.Validate();
                    
                    MakeSpace();

                    var result = tradingService.SellStocks(new SellStockRequest
                    {
                        Price = requestViewModel.Price,
                        NumberOfStocks = requestViewModel.NumberOfStocks,
                        Strategy = SellStockStrategy.Fifo
                    });

                    Console.WriteLine(result);
                }
                catch (TradingException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (SellStockValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                MakeSpace();
                Console.WriteLine("Do you want to continue? (Y/N):");

                @continue = Console.ReadKey().KeyChar;

            } while (@continue == 'y');
        }

        private static SellStockRequestViewModel PromptUser()
        {
            Console.WriteLine("Enter number of stock you want to sell:");

            var stockNumberStr = Console.ReadLine();

            if (!int.TryParse(stockNumberStr, out var stockNumber))
            {
                // handle invalid input
            }
            
            Console.WriteLine("Enter price per stock:");

            var stockPriceStr = Console.ReadLine();

            if (!decimal.TryParse(stockPriceStr, out var stockPrice))
            {
                // handle invalid input
            }

            return new SellStockRequestViewModel { Price = stockPrice, NumberOfStocks = stockNumber };
        }

        static void MakeSpace()
        {
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}