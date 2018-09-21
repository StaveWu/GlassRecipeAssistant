using GlassRecipeAssistant.dao.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using System.Data;

namespace GlassRecipeAssistant.dao.implementions
{
    /// <summary>
    /// database only use id as key, so any condition e.g. you don't want 
    /// customer + glass + powder repeat, you should confirm by yourself.
    /// </summary>
    public class GlassRecipeRepository : IGlassRecipeRepository<GlassRecipe>
    {
        private GlassRecipesDatabaseDataSet.GlassRecipesDataTable grDataTable;
        private GlassRecipesTableAdapter grAdapter;

        public GlassRecipeRepository()
        {
            grDataTable = new GlassRecipesDatabaseDataSet.GlassRecipesDataTable();
            grAdapter = new GlassRecipesTableAdapter();
            grAdapter.Fill(grDataTable);
        }

        public int count()
        {
            grAdapter.Fill(grDataTable);
            return grDataTable.Count();
        }

        public void delete(GlassRecipe entity)
        {
            if (entity == null)
            {
                return;
            }

            grDataTable.AsEnumerable()
                .Where(r => r.Customer == entity.Customer
                && r.GlassName == entity.Glass
                && r.PowderId == entity.PowderId)
                .ToList()
                .ForEach(r => r.Delete());

            grAdapter.Update(grDataTable);
        }

        public void deleteAll()
        {
            grDataTable.AsEnumerable()
                .ToList()
                .ForEach(r => r.Delete());

            grAdapter.Update(grDataTable);
        }

        public void deleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool existsById(int id)
        {
            throw new NotImplementedException();
        }

        public bool existsByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId)
        {
            return grDataTable.AsEnumerable()
                .Where(r => r.Customer == customer
                && r.GlassName == glassName
                && r.PowderId == powderId)
                .Any();
        }

        public List<GlassRecipe> findAll()
        {
            List<GlassRecipe> res = new List<GlassRecipe>();
            grDataTable.AsEnumerable()
                .ToList()
                .ForEach(r => 
                res.Add(new GlassRecipe(r.Customer, r.GlassName, r.PowderId, r.Weight)));
            return res;
        }

        public List<GlassRecipe> findByCustomer(string customer)
        {
            List<GlassRecipe> res = new List<GlassRecipe>();
            grDataTable.AsEnumerable()
                .Where(r => r.Customer == customer)
                .ToList()
                .ForEach(r => res.Add(new GlassRecipe(r.Customer, r.GlassName, r.PowderId, r.Weight)));
            return res;
        }

        public List<GlassRecipe> findByCustomerAndGlass(string customer, string glassName)
        {
            List<GlassRecipe> res = new List<GlassRecipe>();
            grDataTable.AsEnumerable()
                .Where(r => r.Customer == customer && r.GlassName == glassName)
                .ToList()
                .ForEach(r => res.Add(new GlassRecipe(r.Customer, r.GlassName, r.PowderId, r.Weight)));
            return res;
        }

        public Optional<GlassRecipe> findByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId)
        {
            var row = grDataTable.AsEnumerable()
                .Where(r => r.Customer == customer && r.GlassName == glassName && r.PowderId == powderId)
                .FirstOrDefault();
            return new Optional<GlassRecipe>(row == null ? null :
                new GlassRecipe(row.Customer, row.GlassName, row.PowderId, row.Weight));
        }

        public Optional<GlassRecipe> findById(int id)
        {
            throw new NotImplementedException();
        }

        public void save(GlassRecipe entity)
        {
            if (entity == null)
            {
                return;
            }

            if (existsByCustomerAndGlassAndPowderId(entity.Customer, entity.Glass, entity.PowderId))
            {
                var query = grDataTable.AsEnumerable()
                    .Where(r =>
                    r.Customer == entity.Customer
                    && r.GlassName == entity.Glass
                    && r.PowderId == entity.PowderId)
                    .First();
                query.Weight = entity.Weight;
            }
            else
            {
                DataRow newRow = grDataTable.NewRow();
                newRow["Customer"] = entity.Customer;
                newRow["GlassName"] = entity.Glass;
                newRow["PowderId"] = entity.PowderId;
                newRow["Weight"] = entity.Weight;
                grDataTable.Rows.Add(newRow);
            }

            grAdapter.Update(grDataTable);
        }

        public void saveAll(List<GlassRecipe> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return;
            }
            foreach (GlassRecipe p in entities)
            {
                save(p);
            }
        }

        public void deleteByCustomerAndGlassAndPowderId(string customer, string glassName, int powderId)
        {
            grDataTable.AsEnumerable()
                .Where(r =>
                r.Customer == customer
                && r.GlassName == glassName
                && r.PowderId == powderId)
                .ToList()
                .ForEach(r => r.Delete());
        }
    }
}
