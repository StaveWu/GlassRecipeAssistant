using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeAssistant
{
    class RegexUtils
    {
        public static bool isNumber(string param)
        {
            return Regex.IsMatch(param, @"^\d+\.\d+$")
                || Regex.IsMatch(param, @"^\d+$");
        }
    }
}
