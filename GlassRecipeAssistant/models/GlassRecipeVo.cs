using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.models
{
    public class GlassRecipeVo
    {
        public string Customer { get; set; }
        public string GlassName { get; set; }
        public string PowderName { get; set; }
        public double Weight { get; set; }

        public GlassRecipeVo(string cus, string glass, string powder, double w)
        {
            Customer = cus;
            GlassName = glass;
            PowderName = powder;
            Weight = w;
        }
    }
}
