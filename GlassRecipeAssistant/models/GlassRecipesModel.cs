using GlassRecipeAssistant;
using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAssistant.models
{
    class GlassRecipesModel : IGlassRecipesModel
    {
        // 两个事件
        public event GlassAddedHandler GlassChanged;
        public event RecipeAddedHandler RecipeChanged;

        private GlassRecipesDatabaseDataSet.GlassesDataTable gdt;   // glass表
        private GlassRecipesDatabaseDataSet.RecipesDataTable rdt;   // recipes表
        private GlassesTableAdapter gda;    // glass表适配器
        private RecipesTableAdapter rda;    // recipes表适配器


        public GlassRecipesModel()
        {
            gdt = new GlassRecipesDatabaseDataSet.GlassesDataTable();
            rdt = new GlassRecipesDatabaseDataSet.RecipesDataTable();
            gda = new GlassesTableAdapter();
            rda = new RecipesTableAdapter();
            gda.Fill(gdt);
            rda.Fill(rdt);
        }

        public void addGlass(string glassName)
        {
            DataRow newRow = gdt.NewRow();
            newRow["GlassName"] = glassName;
            gdt.Rows.Add(newRow);
            gda.Update(gdt);
            GlassChanged();   // 通知更新了
        }

        public void addRecipe(string glassName, string recipeName, double quality)
        {
            DataRow newRow = rdt.NewRow();
            newRow["RecipeName"] = recipeName;
            newRow["Quality"] = quality;
            newRow["Owner"] = glassName;
            newRow["id"] = getNextId(rdt);
            rdt.Rows.Add(newRow);
            rda.Update(rdt);
            RecipeChanged();  // 通知更新了
        }

        private int getNextId(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(table.Rows[rdt.Rows.Count - 1]["id"]) + 1;
            }
        }

        public bool contains(string glassName)
        {
            return gdt.Rows.Contains(glassName);
        }

        public void deleteGlass(string glassName)
        {
            if (contains(glassName))
            {
                // 删除该镜片的所有配方
                String filter = String.Format("Owner = '{0}'", glassName);
                DataRow[] rows = rdt.Select(filter);
                foreach (DataRow ele in rows)
                {
                    rdt.Rows[rdt.Rows.IndexOf(ele)].Delete();
                    rda.Update(rdt);
                }
                // 删除该镜片
                filter = String.Format("GlassName = '{0}'", glassName);
                gdt.Rows[gdt.Rows.IndexOf(gdt.Select(filter)[0])].Delete();
                gda.Update(gdt);
                GlassChanged();     // 通知更新了
            }
        }

        public void deleteRecipe(string glassName, string recipeName)
        {
            String filter = String.Format("Owner = '{0}' and RecipeName = '{1}'", glassName, recipeName);
            DataRow[] rows = rdt.Select(filter);
            foreach (DataRow ele in rows)
            {
                rdt.Rows[rdt.Rows.IndexOf(ele)].Delete();
                rda.Update(rdt);
                RecipeChanged();    // 通知更新了
            }
        }

        public List<string> findAllGlasses()
        {
            List<string> res = new List<string>();
            foreach (DataRow row in gdt.Rows)
            {
                res.Add(row["GlassName"] as string);
            }
            return res;
        }

        public Dictionary<string, double> findRecipes(string glassName)
        {
            Dictionary<string, double> res = new Dictionary<string, double>();
            try
            {
                String filter = String.Format("Owner = '{0}'", glassName);
                DataRow[] rows = rdt.Select(filter);
                foreach (DataRow row in rows)
                {
                    res.Add(row["RecipeName"] as string, Convert.ToDouble(row["Quality"]));
                }
                return res;
            }
            catch (Exception ex)
            {
                return res;
            }
        }
    }
}
