using System;
using System.Collections.Generic;
using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeAssistant.models;

namespace GlassRecipeAssistantTest.models
{
    [TestClass]
    public class GlassRecipeDataMapperTests
    {
        [TestMethod]
        public void testSave()
        {
            IGlassRecipePowderMapper mapper = new GlassRecipePowderMapper();
            mapper.deleteAll();
            mapper.save(new GlassRecipeVo("a", "b", "c", 12.4));
            Assert.AreEqual(1, mapper.count());
            mapper.save(new GlassRecipeVo("a", "b", "c", 15.0));
            Assert.AreEqual(1, mapper.count());

            Optional<GlassRecipeVo> vo1 = mapper.findByCustomerAndGlassAndPowder("a", "b", "c");
            Assert.AreEqual(15.0, vo1.get().Weight);

            mapper.save(new GlassRecipeVo("a", "b", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "bd", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "b", "cc", 15.0));
        }

        [TestMethod]
        public void testFindPK()
        {
            IGlassRecipePowderMapper mapper = new GlassRecipePowderMapper();
            mapper.deleteAll();
            mapper.save(new GlassRecipeVo("a", "b", "c", 15.0));
            Optional<GlassRecipeVo> vo1 = mapper.findByCustomerAndGlassAndPowder("a", "b", "c");
            Assert.AreEqual(15.0, vo1.get().Weight);

            Assert.ThrowsException<InvalidOperationException>(() =>
                mapper.findByCustomerAndGlassAndPowder("", "", ""));
        }

        [TestMethod]
        public void testFindAll()
        {
            IGlassRecipePowderMapper mapper = new GlassRecipePowderMapper();
            mapper.deleteAll();

            mapper.save(new GlassRecipeVo("a", "b", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "bd", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "b", "cc", 15.0));

            Assert.AreEqual(3, mapper.findAll().Count);

            mapper.deleteAll();
            mapper.findAll();
        }

        [TestMethod]
        public void testSaveAll()
        {
            IGlassRecipePowderMapper mapper = new GlassRecipePowderMapper();
            mapper.deleteAll();

            List<GlassRecipeVo> list = new List<GlassRecipeVo>();
            list.Add(new GlassRecipeVo("a", "b", "c", 11.0));
            list.Add(new GlassRecipeVo("av", "b", "c", 12));
            list.Add(new GlassRecipeVo("ad", "b", "c", 13));

            mapper.saveAll(list);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void testSearch()
        {
            IGlassRecipePowderMapper mapper = new GlassRecipePowderMapper();
            mapper.deleteAll();

            mapper.save(new GlassRecipeVo("a", "b", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "bd", "c", 15.0));
            mapper.save(new GlassRecipeVo("a", "b", "cc", 15.0));

            Assert.AreEqual(3, mapper.search("b").Count);
            Assert.AreEqual(1, mapper.search("d").Count);

            mapper.deleteAll();
            Assert.AreEqual(0, mapper.search("b").Count);
        }
    }
}
