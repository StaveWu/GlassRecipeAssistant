using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeAssistant;

namespace GlassRecipeAssistantTest.utils
{
    [TestClass]
    public class StringUtilsTests
    {
        [TestMethod]
        public void testFilterIllegalChars()
        {
            string filename = "tr 73*0.5C*2.0/A\\|S0<9>?1:6";
            Assert.AreEqual("tr 730.5C2.0AS0916", StringUtils.filterIllegalChars(filename));
        }
    }
}
