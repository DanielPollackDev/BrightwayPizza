using BrightwayPizza.Console.Models;
using BrightwayPizza.Console.Utilities.Implementation;
using BrightwayPizza.Console.Utilities.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace PizzaTopping
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            SetupConfiguration(builder);
            var host = SetupDependencies();

            var pizzaUtil = ActivatorUtilities.CreateInstance<PizzaUtility>(host.Services);
            var webUtil = ActivatorUtilities.CreateInstance<WebJsonUtility>(host.Services);

            var pizzaList = webUtil.GetPizzaList();
            var pizzaGroupNumbers = pizzaUtil.GetPizzaCombinationNumber(pizzaList);
            var mostPopular = pizzaUtil.GetMostPopularPizza(pizzaList);
            var mosttoLeastPopular = pizzaUtil.OrderBy("Count", pizzaList, true, null);
            var topTwenty = pizzaUtil.OrderBy("Count", pizzaList, true, 20);

            OutputStatsToConsole("Pizzas by Number:", pizzaGroupNumbers);
            OutputStatsToConsole("Most Popular:", new List<PizzaGroup>() { mostPopular });
            OutputStatsToConsole("Sorted List by most to least popular", mosttoLeastPopular);
            OutputStatsToConsole("Top 20", topTwenty);

          
        }


        public static void OutputStatsToConsole( string title, IEnumerable<PizzaGroup> pizzaGroupList)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(title);
            foreach (var pizzaGroup in pizzaGroupList)
            {
                Console.WriteLine($"Toppings: {pizzaGroup.PizzaGroupKey}, Count: {pizzaGroup.Count}");
            }

        }



        public static IHost SetupDependencies()
        {

            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddTransient<IPizzaUtility, PizzaUtility>();
                            services.AddTransient<IWebJsonUtility, WebJsonUtility>();
                        })
                        .Build();
            return host;

        }
        public static void SetupConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .AddEnvironmentVariables();
        }




    }
}