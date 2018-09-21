using System;
using GlassRecipeAssistant.dao.entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlassRecipeAssistantTest.dao
{
    [TestClass]
    public class PowderTests
    {
        [TestMethod]
        public void testConstructor()
        {
            Powder powder = new Powder(1, "a");
            Assert.AreEqual(1, powder.Id);
            Assert.AreEqual("a", powder.PowderName);
            Assert.IsFalse(-1 == powder.Id);
        }
    }
}
