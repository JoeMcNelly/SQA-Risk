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

            Assert.IsTrue(test.Equals(target));
        }
        [Test()]
        public void TestTerritoryIsNotEqualToNullObj()
        {
            Territory target = new Territory();

            Assert.IsFalse(target.Equals(null));
        }

        [Test()]
        public void TestTerritoryIsNotEqualToOtherObjects()
        {
            Territory target = new Territory();

            Assert.IsFalse(target.Equals(new Object()));
        }

        [Test()]
        public void TestTerritoryAreNotEqualWhenNameDifference()
        {
            Territory target = new Territory("", "A");
            Territory test = new Territory("", "B");

            Assert.IsFalse(test.Equals(target));
        }
    }
}
