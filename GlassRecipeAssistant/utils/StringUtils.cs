using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeAssistant
{
    public class StringUtils
    {
        public static bool isNumber(string param)
        {
            return Regex.IsMatch(param, @"^\d+\.\d+$")
                || Regex.IsMatch(param, @"^\d+$");
        }

        public static string filterIllegalChars(string fileName)
        {
            MatchCollection matches = Regex.Matches(fileName, "[^*/?:\\\\<>|\"]");
            string res = "";
            foreach (Match match in matches)
            {
                res += match.Value;
            }

            return res;
        }
    }
}
