using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.dao.implementions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassRecipeAssistant.dao
{
    public class RepositoryFactory
    {
        private static IPowderRepository<Powder> powderRepoInstance;
        private static IGlassRecipeRepository<GlassRecipe> grRepoInstance;

        public static IPowderRepository<Powder> getPowderRepository()
        {
            if (powderRepoInstance == null)
            {
                powderRepoInstance = new PowderRepository();
            }
            return powderRepoInstance;
        }

        public static IGlassRecipeRepository<GlassRecipe> getGlassRecipeRepository()
        {
            if (grRepoInstance == null)
            {
                grRepoInstance = new GlassRecipeRepository();
            }
            return grRepoInstance;
        }
    }
}
