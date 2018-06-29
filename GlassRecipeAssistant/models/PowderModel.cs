using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.models
{
    class PowderModel : IPowderModel
    {
        public event PowdersUpdateHandler PowdersUpdated;

        private GlassRecipesDatabaseDataSet.PowdersDataTable pdt;
        private PowdersTableAdapter pda;

        public PowderModel()
        {
            pdt = new GlassRecipesDatabaseDataSet.PowdersDataTable();
            pda = new PowdersTableAdapter();
            pda.Fill(pdt);
        }

        public void addPowder(string powderName)
        {
            DataRow newRow = pdt.NewRow();
            newRow["PowderName"] = powderName;
            pdt.Rows.Add(newRow);
            pda.Update(pdt);
            PowdersUpdated();
        }

        public List<string> findPowders()
        {
            List<string> res = new List<string>();
            foreach (DataRow ele in pdt.Rows)
            {
                res.Add(ele["PowderName"] as string);
            }
            return res;
        }

        public void renamePowder(int index, string newName)
        {
            int count = 0;
            foreach (DataRow ele in pdt.Rows)
            {
                if (count == index)
                {
                    ele["PowderName"] = newName;
                }
                count++;
            }
            pda.Update(pdt);
            PowdersUpdated();
        }

        public int getPowderId(int selectedIndex)
        {
            return Convert.ToInt32(pdt.Rows[selectedIndex]["Id"]);
        }

        public string getPowderName(int powderId)
        {
            foreach (DataRow ele in pdt.Rows)
            {
                if (Convert.ToInt32(ele["Id"]) == powderId)
                {
                    return ele["PowderName"] as string;
                }
            }
            return null;
        }
    }
}
