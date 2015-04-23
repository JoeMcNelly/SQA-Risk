using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestGUI;

namespace UnitTestProject1
{
    [TestClass]
    public class MapTest
    {
        [TestMethod]
        public void TestMapInitalizes()
        {
            Map target = new Map();

            Assert.IsNotNull(target);
        }
    }
}
