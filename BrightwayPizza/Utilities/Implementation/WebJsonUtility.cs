using BrightwayPizza.Console.Models;
using BrightwayPizza.Console.Utilities.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace BrightwayPizza.Console.Utilities.Implementation
{
    public class WebJsonUtility : IWebJsonUtility
    {
        private readonly ILogger<WebJsonUtility> logger;
        private readonly string brightwayJsonUrl;

        public WebJsonUtility(IConfiguration config, ILogger<WebJsonUtility> logger)
        {
            brightwayJsonUrl = config.GetValue<string>("BrightwayJsonUrl");
            this.logger = logger;   
        }

        public List<Pizza> GetPizzaList()
        {
            try
            {
                var jsonStr = GetJsonFromUrl();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                if (!string.IsNullOrEmpty(jsonStr))
                {
                    var pizzaList = JsonSerializer.Deserialize<List<Pizza>>(jsonStr, options);
                    if (pizzaList != null)
                    {
                        SortPizzaToppingsKey(pizzaList);
                        return pizzaList;
                    }
                    return new List<Pizza>();
                }
                return new List<Pizza>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }


        private string GetJsonFromUrl()
        {
            using WebClient wc = new();
            var json = wc.DownloadString(brightwayJsonUrl);
            return json;
        }

        private void SortPizzaToppingsKey(List<Pizza> pizzaList)
        {
            foreach (var pizza in pizzaList)
            {
                pizza.SortedToppingKey = String.Join(",", pizza.Toppings.OrderByDescending(t => t));
            }
        }

    }
}
