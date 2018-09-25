using GlassRecipeAssistant.models;
using GlassRecipeAssistant.dao.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlassRecipeAssistant.dao;

namespace RecipeAssistant.models
{
    /// <summary>
    /// this is a data mapper in fact
    /// </summary>
    public interface IGlassRecipePowderMapper : CrudRepository<GlassRecipeVo>
    {
        List<GlassRecipeVo> search(string filterStr);

        List<string> findCustomers();

        List<string> findGlasses();

        List<string> findGlassesByCustomer(string customer);

        Dictionary<string, double> findPowders(string customer, string glass);

        Optional<GlassRecipeVo> findByCustomerAndGlassAndPowder(string customer, string glass, string powder);

        bool existsByCustomerAndGlassAndPowder(string customer, string glass, string powder);

    }
}
