using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.dao.implementions;
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

        private IPowderRepository<Powder> powderRepository;

        public PowderModel()
        {
            powderRepository = RepositoryFactory.getPowderRepository();
        }

        public void addPowder(Powder powder)
        {
            powderRepository.save(powder);
            PowdersUpdated();
        }

        public List<Powder> findPowders()
        {
            return powderRepository.findAll();
        }

        public void renamePowder(int id, string newName)
        {
            Powder powder = new Powder(id, newName);
            powderRepository.save(powder);
            PowdersUpdated();
        }

    }
}
