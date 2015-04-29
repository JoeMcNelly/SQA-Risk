using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;
using System.Drawing;

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
            Card target = new Card(0, "Alaska", new Bitmap(255, 255));

            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void TestCardGetTroopType()
        {
            Card target = new Card(0, "Alaska", new Bitmap(255, 255));

            Assert.AreEqual(0, target.GetTroopType());
        }
    }
}
