using BrightwayPizza.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightwayPizza.Console.Utilities.Interfaces
{
    public interface IPizzaUtility
    {
        public IEnumerable<PizzaGroup> GetPizzaCombinationNumber(List<Pizza> pizzaList);
        public PizzaGroup GetMostPopularPizza(List<Pizza> pizzaList);
        public IEnumerable<PizzaGroup> OrderBy(string propertyName, List<Pizza> pizzaList, bool orderByDescending = true, int? topNumRecords = null);
    }
}
