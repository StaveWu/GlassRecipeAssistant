using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    public delegate void GlassAddedHandler();

    public delegate void RecipeAddedHandler();

    public delegate void ClientAddedHandler();

    /// <summary>
    /// 定义该接口主要是考虑到以后可能会换数据库的情况
    /// </summary>
    public interface IGlassRecipesModel
    {
        void addClient(String clientName);

        void deleteClient(string clientName);

        List<string> findClients();


        void addGlass(string glassName, string clientName);

        void deleteGlass(string glassName, string clientName);

        List<string> findGlasses(string clientName);


        void addRecipe(string clientName, string glassName, string recipeName, double quality);

        void deleteRecipe(string clientName, string glassName, string recipeName);

        Dictionary<string, double> findRecipes(string clientName, string glassName);


        bool containsClient(string clientName);

        bool containsGlass(string glassName, string clientName);

        event GlassAddedHandler GlassChanged;

        event RecipeAddedHandler RecipeChanged;

        event ClientAddedHandler ClientChanged;


    }
}
