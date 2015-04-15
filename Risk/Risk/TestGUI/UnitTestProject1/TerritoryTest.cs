using System;
using System.IO;
using NUnit.Framework;
using TestGUI;

namespace UnitTestProject1
{
    [TestFixture()]
    public class TerritoryTest
    {
        [Test()]
        public void TestTerritoryEqualsEmpty()
        {
            Territory target = new Territory();
            Territory test = new Territory();

            Assert.IsTrue(target.Equals(test));
        }
    }
}
