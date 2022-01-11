using BrightwayPizza.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightwayPizza.Console.Utilities.Interfaces
{
    public interface IWebJsonUtility
    {
        public List<Pizza> GetPizzaList();
    }
}
