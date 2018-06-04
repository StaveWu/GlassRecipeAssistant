using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant
{
    class ZeroQualityStrategy : IRecipeQualityCalculateStrategy
    {
        private double baseQuality;

        public ZeroQualityStrategy(double baseQuality)
        {
            this.baseQuality = baseQuality;
        }

        public double getCurrentQuality(double realTimeQuality)
        {
            return Math.Round(realTimeQuality - baseQuality, 4);
        }
    }
}
