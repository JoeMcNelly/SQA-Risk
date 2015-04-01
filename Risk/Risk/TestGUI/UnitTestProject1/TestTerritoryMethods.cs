using System;
using NUnit.Framework;
using TestGUI;

namespace UnitTestProject1
{
    [TestFixture()]
    public class TestTerritoryMethods
    {

        [Test()]
        public void TestTerritoryInitializes()
        {
            Territory terr = new Territory();
            Assert.IsNotNull(terr);
        }

        [Test()]
        public void TestCorrectlyAddedTerritoryToGameMap()
        {
            Game newgame = new Game(2);
            Territory terr = new Territory();
            newgame.addTerritoryToMap(terr);
            Assert.AreEqual(terr, newgame.getMapList()[0]);

        }

        [Test()]
        public void TestReturnCorrectNumberOfTroops()
        {
            Territory terr = new Territory();
            int numOfSoldiers = 5;
            terr.addTroops(numOfSoldiers);
            Assert.AreEqual(numOfSoldiers, terr.getNumTroops());
        }

    }
}
