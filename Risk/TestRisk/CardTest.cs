using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;

namespace UnitTestProject1
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void TestCardInitalizesForEmpty()
        {
            Card target = new Card();

            Assert.IsNotNull(target);   
        }
        [TestMethod]
        public void TestCardInitalizesWithBaseParameters()
        {
            Card target = new Card(0, "Alaska", Risk.Properties.Resources.Blank);

            Assert.IsNotNull(target);
        }
    }
}
