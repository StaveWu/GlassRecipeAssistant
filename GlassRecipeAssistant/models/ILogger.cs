using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    interface ILogger
    {
        void write(string clientName, string glassName, Dictionary<string, double[]> recipes);
    }
}
