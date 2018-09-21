using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using RecipeAssistant.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.models
{
    class RecipeModel : IGlassRecipesModel
    {
        public event GlassAddedHandler GlassChanged;
        public event RecipeAddedHandler RecipeChanged;
        public event ClientAddedHandler ClientChanged;

        //public RecipeModel()
        //{
        //    rdt = new GlassRecipesDatabaseDataSet.RecipeDataTable();
        //    rta = new RecipeTableAdapter();
        //    rta.Fill(rdt);
        //}

        public void addClient(string clientName)
        {
            //DataRow newRow = rdt.NewRow();
            //newRow["Customer"] = clientName;
            //newRow["GlassName"] = null;
            //newRow["PowderId"] = null;
            //newRow["Widget"] = null;
            //rdt.Rows.Add(newRow);
            //rta.Update(rdt);
            //ClientChanged();   // 通知更新了
        }

        public void addGlass(string glassName, string clientName)
        {
            
        }

        public void addRecipe(string clientName, string glassName, int powderId, double quality)
        {
            throw new NotImplementedException();
        }

        public bool containsClient(string clientName)
        {
            throw new NotImplementedException();
        }

        public bool containsGlass(string glassName, string clientName)
        {
            throw new NotImplementedException();
        }

        public void deleteClient(string clientName)
        {
            throw new NotImplementedException();
        }

        public void deleteGlass(string glassName, string clientName)
        {
            throw new NotImplementedException();
        }

        public void deleteRecipe(string clientName, string glassName, int powderId)
        {
            throw new NotImplementedException();
        }

        public List<string> findClients()
        {
            throw new NotImplementedException();
        }

        public List<string> findGlasses(string clientName)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, double> findRecipes(string clientName, string glassName)
        {
            throw new NotImplementedException();
        }
    }
}
