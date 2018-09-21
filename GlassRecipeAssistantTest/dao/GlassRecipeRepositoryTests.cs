using System;
using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.dao.entities;
using GlassRecipeAssistant.dao.implementions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlassRecipeAssistantTest.dao
{
    [TestClass]
    public class GlassRecipeRepositoryTests
    {
        [TestMethod]
        public void testDeleteAll()
        {
            IGlassRecipeRepository<GlassRecipe> repository = new GlassRecipeRepository();
            repository.deleteAll();
            repository.save(new GlassRecipe("a", "b", -1, 12.2));
            repository.save(new GlassRecipe("c", "b", -1, 12.2));
            repository.save(new GlassRecipe("d", "b", -1, 12.2));

            Assert.AreEqual(3, repository.count());

            repository.deleteAll();
            repository.save(new GlassRecipe("d", "b", -1, 12.2));

            repository.deleteAll();
            Assert.AreEqual(0, repository.count());
        }

        [TestMethod]
        public void testSave()
        {
            IGlassRecipeRepository<GlassRecipe> repository = new GlassRecipeRepository();
            repository.deleteAll();
            repository.save(new GlassRecipe("a", "b", -1, 12.2));
            repository.save(new GlassRecipe("a", "b", -1, 12.2));

            repository.save(new GlassRecipe("a", "b", -1, 13.2));
            Assert.AreEqual(1, repository.count());
            
        }

        [TestMethod]
        public void testDelete()
        {
            IGlassRecipeRepository<GlassRecipe> repository = new GlassRecipeRepository();
            repository.deleteAll();
            repository.save(new GlassRecipe("a", "b", -1, 12.2));
            repository.save(new GlassRecipe("a", "c", -1, 12.2));
            repository.delete(new GlassRecipe("a", "c", -1, 12.2));

            Assert.AreEqual(1, repository.count());
        }
    }
}
