using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    public delegate void GlassAddedHandler();

    public delegate void RecipeAddedHandler();

    /// <summary>
    /// 定义该接口主要是考虑到以后可能会换数据库的情况
    /// </summary>
    public interface IGlassRecipesModel
    {
        void addGlass(string glassName);

        void deleteGlass(string glassName);

        void deleteRecipe(string glassName, string recipeName);

        void addRecipe(string glassName, string recipeName, double quality);

        Dictionary<string, double> findRecipes(string glassName);

        List<string> findAllGlasses();

        bool contains(string glassName);

        event GlassAddedHandler GlassChanged;

        event RecipeAddedHandler RecipeChanged;


    }
}
