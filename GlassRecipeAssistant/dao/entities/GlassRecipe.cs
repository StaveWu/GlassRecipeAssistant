using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao.entities
{
    public class GlassRecipe
    {
        public string Customer { get; set; }
        public string Glass { get; set; }
        public int PowderId { get; set; }
        public double Weight { get; set; }

        public GlassRecipe(string cus, string glass, int powId, double w)
        {
            initFields(cus, glass, powId, w);
        }

        private void initFields(string cus, string glass, int powId, double w)
        {
            Customer = cus;
            Glass = glass;
            PowderId = powId;
            Weight = w;
        }

        public override string ToString()
        {
            return String.Format("[Customer = {0}, Glass = {1}, PowderId = {2}, Weight = {3}]",
                Customer, Glass, PowderId, Weight);
        }
    }
}
