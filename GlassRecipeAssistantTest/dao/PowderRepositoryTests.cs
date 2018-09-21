using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlassRecipeAssistant.dao.implementions;
using GlassRecipeAssistant.dao;
using GlassRecipeAssistant.dao.entities;
using System.Collections.Generic;

namespace GlassRecipeAssistantTest.dao
{
    [TestClass]
    public class PowderRepositoryTests
    {
        IPowderRepository<Powder> repository = new PowderRepository();

        [TestMethod]
        public void testSave()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            repository.save(new Powder("a")); // skip cu'z name duplicated
            Assert.AreEqual(1, repository.count());

            repository.save(new Powder("b"));
            Assert.AreEqual(2, repository.count());

            repository.deleteAll();
            repository.save(new Powder("a"));
            int id = repository.findByPowderName("a").get().Id;
            repository.save(new Powder(id, "b"));
          
            Assert.AreEqual(1, repository.count());
        }

        [TestMethod]
        public void testExistByPowderName()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));

            Assert.IsTrue(repository.existsByPowderName("a"));
            Assert.IsFalse(repository.existsByPowderName("b"));
        }

        [TestMethod]
        public void testCount()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            repository.save(new Powder("b"));
            repository.save(new Powder("c"));

            Assert.AreEqual(3, repository.count());
        }

        [TestMethod]
        public void testDeleteAll()
        {
            repository.deleteAll();

            Assert.AreEqual(0, repository.count());
        }

        [TestMethod]
        public void testDelete()
        {
            repository.deleteAll();
            repository.delete(null);

            Powder powder = new Powder("test");
            repository.save(powder);
            repository.delete(powder);

            Assert.AreEqual(0, repository.count());
        }

        [TestMethod]
        public void testFindAll()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            repository.save(new Powder("b"));
            List<Powder> res = repository.findAll();

            Assert.AreEqual(2, res.Count);
            Assert.AreEqual("a", res[0].PowderName);
            Assert.AreEqual("b", res[1].PowderName);

            repository.deleteAll();
            res = repository.findAll();
            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void testFindByPowderName()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            Optional<Powder> p = repository.findByPowderName("a");

            Assert.IsTrue(p.isPresent());

            repository.deleteAll();
            Assert.IsFalse(repository.findByPowderName("a").isPresent());
            
        }

        [TestMethod]
        public void testFindById()
        {
            repository.deleteAll();
            repository.save(new Powder("a")); // id would be generate by database
            Optional<Powder> powder = repository.findByPowderName("a"); // get id by powder name, suppose that find by powder name works
            Optional<Powder> p2 = repository.findById(powder.get().Id);
            // check id is equal or not
            Assert.AreEqual(powder.get().PowderName, p2.get().PowderName);

            repository.deleteAll();
            Assert.IsFalse(repository.findById(powder.get().Id).isPresent());
            
        }

        [TestMethod]
        public void testDeleteById()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            Assert.AreEqual(1, repository.count());

            repository.deleteById(99);
            Assert.AreEqual(1, repository.count());

            int id = repository.findByPowderName("a").get().Id;
            repository.deleteById(id);
            Assert.AreEqual(0, repository.count());
        }

        [TestMethod]
        public void testExistById()
        {
            repository.deleteAll();
            repository.save(new Powder("a"));
            int id = repository.findByPowderName("a").get().Id;

            Assert.IsTrue(repository.existsById(id));
            Assert.IsFalse(repository.existsById(id + 99));
        }

        [TestMethod]
        public void testSaveAll()
        {
            repository.deleteAll();
            List<Powder> list = new List<Powder>();
            list.Add(new Powder("a"));
            list.Add(new Powder("b"));
            list.Add(new Powder("c"));
            repository.saveAll(list);

            Assert.AreEqual(3, repository.count());
        }
    }
}
