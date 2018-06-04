using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant
{
    interface IRecipeQualityCalculateStrategy
    {
        double getCurrentQuality(double realTimeQuality);
    }
}
