using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightwayPizza.Console.Models
{
    public class Pizza
    {
        public List<string> Toppings { get; set; }
        public string SortedToppingKey { get; set; }
    }
}
