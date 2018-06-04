using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant
{
    class ReCheckQualityStrategy : IRecipeQualityCalculateStrategy
    {
        private double baseQuality;
        private double currentQuality;

        public ReCheckQualityStrategy(double baseQuality, double currentQuality)
        {
            this.baseQuality = baseQuality;
            this.currentQuality = currentQuality;
        }
        public double getCurrentQuality(double realTimeQuality)
        {
            return Math.Round(realTimeQuality - baseQuality + currentQuality, 4);
        }
    }
}
