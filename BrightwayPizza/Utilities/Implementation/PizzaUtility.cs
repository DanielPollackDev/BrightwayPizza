using BrightwayPizza.Console.Models;
using BrightwayPizza.Console.Utilities.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightwayPizza.Console.Utilities.Implementation
{
    public class PizzaUtility : IPizzaUtility
    {
        private readonly ILogger<PizzaUtility> logger;
        public PizzaUtility(ILogger<PizzaUtility> logger)
        {
            this.logger = logger;
        }
        public IEnumerable<PizzaGroup> GetPizzaCombinationNumber(List<Pizza> pizzaList)
        {
            try
            {
                return GetPizzaGroups(pizzaList);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                throw;
            }
        }

        public PizzaGroup GetMostPopularPizza(List<Pizza> pizzaList)
        {
            try
            {
                var pizzaGroups = GetPizzaGroups(pizzaList);
                var mostPopular = pizzaGroups.MaxBy(pg => pg.Count);
                return mostPopular;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }

        public IEnumerable<PizzaGroup> OrderBy(string proppertyName, List<Pizza> pizzaList, bool orderByDescending = true, int? topNumRecords = null)
        {
            try
            {
                var pizzaGroups = GetPizzaGroups(pizzaList);
                System.Reflection.PropertyInfo prop = typeof(PizzaGroup).GetProperty(proppertyName);
                var orderedPizzas = orderByDescending ? pizzaGroups.OrderByDescending(x => prop.GetValue(x, null)) : pizzaGroups.OrderBy(x => prop.GetValue(x, null));

                return topNumRecords == null ? orderedPizzas : orderedPizzas.Take(topNumRecords.GetValueOrDefault());
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        private IEnumerable<PizzaGroup> GetPizzaGroups(List<Pizza> pizzaList)
        {
            var groupResult = pizzaList.GroupBy(item => item.SortedToppingKey).Select(group =>
                       new PizzaGroup
                       {
                           PizzaGroupKey = group.Key,
                           Pizzas = group.ToList(),
                           Count = group.Count()
                       });

            return groupResult;
        }



    }
}
