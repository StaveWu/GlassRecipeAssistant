using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao
{
    public interface IGlassRecipeRepository<T> : CrudRepository<T>
    {
        void deleteByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId);

        List<T> findByCustomer(string customer);

        List<T> findByCustomerAndGlass(string customer, string glassName);

        bool existsByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId);

        Optional<T> findByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId);
    }
}
