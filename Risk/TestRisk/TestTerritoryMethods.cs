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
            Assert.True(newgame.getMapList().ContainsKey(terr.getName()));

        }

        [Test()]
        public void TestReturnCorrectNumberOfTempTroops()
        {
            Territory terr = new Territory();
            int numOfSoldiers = 5;
            for (int i = 0; i < numOfSoldiers; i++)
            {
                terr.addTroops();
            }
            Assert.AreEqual(numOfSoldiers, terr.getTemporaryReinforcements());
        }

        [Test()]
        public void TestSaveTroopsMethod()
        {
            Territory terr = new Territory();
            int numSoldiers = 15;
            for (int i = 0; i < numSoldiers; i++)
            {
                terr.addTroops();
            }
            terr.saveTroops();
            Assert.AreEqual(numSoldiers, terr.getNumTroops());
        }

        [Test()]
        public void TestResetTroopsMethod()
        {
            Territory terr = new Territory();
            int numSoldiers = 7;
            for (int i = 0; i < numSoldiers; i++)
            {
                terr.addTroops();
            }
            terr.resetReinforcements();
            terr.saveTroops();
            Assert.AreEqual(0, terr.getTemporaryReinforcements());
            Assert.AreEqual(0, terr.getNumTroops());
        }

    }
}
