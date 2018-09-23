using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.views
{
    class PowderInfo
    {
        public string PowderName { get; set; }

        public double StandardWeight { get; set; }

        public double CurrentWeight { get; set; }

        public PowderInfo(string powderName, double standardWright)
        {
            this.PowderName = powderName;
            this.StandardWeight = standardWright;
        }
    }
}
