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
    }
}
