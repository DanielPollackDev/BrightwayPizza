using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightwayPizza.Console.Models
{
    public class PizzaGroup
    {
        public string PizzaGroupKey { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public int Count { get; set; }

    }
}
