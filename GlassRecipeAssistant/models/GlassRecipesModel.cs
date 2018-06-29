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
        // 事件
        public event ClientAddedHandler ClientChanged;
        public event GlassAddedHandler GlassChanged;
        public event RecipeAddedHandler RecipeChanged;

        private GlassRecipesDatabaseDataSet.ClientsDataTable cdt;   // client表
        private GlassRecipesDatabaseDataSet.GlassesDataTable gdt;   // glass表
        private GlassRecipesDatabaseDataSet.RecipesDataTable rdt;   // recipes表
        private ClientsTableAdapter cda;    // client表适配器
        private GlassesTableAdapter gda;    // glass表适配器
        private RecipesTableAdapter rda;    // recipes表适配器


        public GlassRecipesModel()
        {
            cdt = new GlassRecipesDatabaseDataSet.ClientsDataTable();
            gdt = new GlassRecipesDatabaseDataSet.GlassesDataTable();
            rdt = new GlassRecipesDatabaseDataSet.RecipesDataTable();
            cda = new ClientsTableAdapter();
            gda = new GlassesTableAdapter();
            rda = new RecipesTableAdapter();
            cda.Fill(cdt);
            gda.Fill(gdt);
            rda.Fill(rdt);
        }

        public void addClient(String clientName)
        {
            DataRow newRow = cdt.NewRow();
            newRow["ClientName"] = clientName;
            cdt.Rows.Add(newRow);
            cda.Update(cdt);
            ClientChanged();   // 通知更新了
        }

        public void addGlass(string glassName, string clientName)
        {
            DataRow newRow = gdt.NewRow();
            newRow["GlassName"] = glassName;
            newRow["Client"] = clientName;
            newRow["id"] = getNextId(gdt);
            gdt.Rows.Add(newRow);
            gda.Update(gdt);
            GlassChanged();   // 通知更新了
        }

        public void addRecipe(string clientName, string glassName, int powderId, double quality)
        {
            DataRow newRow = rdt.NewRow();
            newRow["PowderId"] = powderId;
            newRow["Quality"] = quality;
            newRow["Glass"] = glassName;
            newRow["Client"] = clientName;
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
                return Convert.ToInt32(table.Rows[table.Rows.Count - 1]["id"]) + 1;
            }
        }

        public bool containsClient(string clientName)
        {
            return cdt.Rows.Contains(clientName);
        }

        /// <summary>
        /// 查看glasstable里面是否有指定的镜片
        /// 该方法之所以用不了Contains方法，是因为我在镜片数据库中
        /// 设置主键为id，后期可以选择采用联合主键
        /// </summary>
        /// <param name="glassName"></param>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public bool containsGlass(string glassName, string clientName)
        {
            String filter = String.Format("GlassName = '{0}' and Client = '{1}'", glassName, clientName);
            return gdt.Select(filter) != null && gdt.Select(filter).Length > 0;
        }


        public void deleteClient(string clientName)
        {
            if (containsClient(clientName))
            {
                // 删除该客户的所有配方
                String filter = String.Format("Client = '{0}'", clientName);
                DataRow[] rows = rdt.Select(filter);
                foreach (DataRow ele in rows)
                {
                    rdt.Rows[rdt.Rows.IndexOf(ele)].Delete();
                    rda.Update(rdt);
                }
                // 删除该客户的所有镜片
                filter = String.Format("Client = '{0}'", clientName);
                DataRow[] rows2 = gdt.Select(filter);
                foreach (DataRow ele in rows2)
                {
                    gdt.Rows[gdt.Rows.IndexOf(ele)].Delete();
                    gda.Update(gdt);
                }

                // 删除该客户
                filter = String.Format("ClientName = '{0}'", clientName);
                cdt.Rows[cdt.Rows.IndexOf(cdt.Select(filter)[0])].Delete();
                cda.Update(cdt);
                ClientChanged();     // 通知更新了
            }
        }

        public void deleteGlass(string glassName, string clientName)
        {
            if (containsGlass(glassName, clientName))
            {
                // 删除该镜片的所有配方
                String filter = String.Format("Glass = '{0}' and Client = '{1}'", glassName, clientName);
                DataRow[] rows = rdt.Select(filter);
                foreach (DataRow ele in rows)
                {
                    rdt.Rows[rdt.Rows.IndexOf(ele)].Delete();
                    rda.Update(rdt);
                }
                // 删除该镜片
                filter = String.Format("GlassName = '{0}' and Client = '{1}'", glassName, clientName);
                gdt.Rows[gdt.Rows.IndexOf(gdt.Select(filter)[0])].Delete();
                gda.Update(gdt);
                GlassChanged();     // 通知更新了
            }
        }

        public void deleteRecipe(string clientName, string glassName, int powderId)
        {
            String filter = String.Format("Client = '{0}' and Glass = '{1}' and PowderId = '{2}'", clientName, glassName, powderId);
            DataRow[] rows = rdt.Select(filter);
            foreach (DataRow ele in rows)
            {
                rdt.Rows[rdt.Rows.IndexOf(ele)].Delete();
                rda.Update(rdt);
                RecipeChanged();    // 通知更新了
            }
        }

        public List<String> findClients()
        {
            List<string> res = new List<string>();
            foreach (DataRow row in cdt.Rows)
            {
                res.Add(row["ClientName"] as string);
            }
            return res;
        }

        public List<string> findGlasses(string clientName)
        {
            List<string> res = new List<string>();
            try
            {
                String filter = String.Format("Client = '{0}'", clientName);
                DataRow[] rows = gdt.Select(filter);
                foreach (DataRow row in rows)
                {
                    res.Add(row["GlassName"] as string);
                }
                return res;
            }
            catch (Exception ex)
            {
                return res;
            }

        }

        public Dictionary<int, double> findRecipes(string clientName, string glassName)
        {
            Dictionary<int, double> res = new Dictionary<int, double>();
            try
            {
                String filter = String.Format("Client = '{0}' and Glass = '{1}'", clientName, glassName);
                DataRow[] rows = rdt.Select(filter);
                foreach (DataRow row in rows)
                {
                    res.Add(Convert.ToInt32(row["PowderId"]), Convert.ToDouble(row["Quality"]));
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
