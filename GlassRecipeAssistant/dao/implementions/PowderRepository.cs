using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.GlassRecipesDatabaseDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao.implementions
{
    public class PowderRepository : IPowderRepository<Powder>
    {
        private GlassRecipesDatabaseDataSet.PowdersDataTable powderDataTable;
        private PowdersTableAdapter powderTableAdapter;

        public PowderRepository()
        {
            powderDataTable = new GlassRecipesDatabaseDataSet.PowdersDataTable();
            powderTableAdapter = new PowdersTableAdapter();
            powderTableAdapter.Fill(powderDataTable);
        }

        public int count()
        {
            powderTableAdapter.Fill(powderDataTable);
            return powderDataTable.Count();
        }

        public void delete(Powder entity)
        {
            if (entity == null)
            {
                return;
            }
            powderDataTable.AsEnumerable()
                .Where(r => r.PowderName == entity.PowderName)
                .ToList()
                .ForEach(r => r.Delete());

            powderTableAdapter.Update(powderDataTable);
        }

        public void deleteAll()
        {
            powderDataTable.AsEnumerable()
                .ToList()
                .ForEach(r => r.Delete());
            powderTableAdapter.Update(powderDataTable);
        }

        public void deleteById(int id)
        {
            powderDataTable.AsEnumerable()
                .Where(r => r.Id == id)
                .ToList()
                .ForEach(r => r.Delete());

            powderTableAdapter.Update(powderDataTable);
        }

        public bool existsById(int id)
        {
            return powderDataTable.AsEnumerable()
                .Where(r => r.Id == id)
                .Any();
        }

        public bool existsByPowderName(string name)
        {
            return powderDataTable.AsEnumerable()
                .Where(r => r.PowderName == name)
                .Any();
        }

        public List<Powder> findAll()
        {
            List<Powder> res = new List<Powder>();
            powderDataTable.AsEnumerable()
                .ToList()
                .ForEach(r => res.Add(new Powder(r.Id, r.PowderName)));
            return res;
        }

        public Optional<Powder> findById(int id)
        {
            var query = powderDataTable.AsEnumerable()
                .Where(r => r.Id == id)
                .FirstOrDefault();
            return new Optional<Powder>(query == null ? 
                null : new Powder(query.Id, query.PowderName));
        }

        public Optional<Powder> findByPowderName(string powderName)
        {
            var query = powderDataTable.AsEnumerable()
                .Where(r => r.PowderName == powderName)
                .FirstOrDefault();
            return new Optional<Powder>(query == null ?
               null : new Powder(query.Id, query.PowderName));
        }

        public void save(Powder entity)
        {
            if (entity == null)
            {
                return;
            }

            if (existsById(entity.Id))
            {
                var query = powderDataTable
                    .Where(r => r.Id == entity.Id)
                    .First().PowderName = entity.PowderName;
            }
            else if (existsByPowderName(entity.PowderName))
            {
                return;
            }
            else
            {
                DataRow newRow = powderDataTable.NewRow();
                newRow["PowderName"] = entity.PowderName;
                powderDataTable.Rows.Add(newRow);
            }

            powderTableAdapter.Update(powderDataTable);

        }

        public void saveAll(List<Powder> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return;
            }
            foreach (Powder p in entities)
            {
                save(p);
            }
        }

    }
}
