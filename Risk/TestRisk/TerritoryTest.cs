using System;
using System.IO;
using NUnit.Framework;
using Risk;

namespace TestRisk
{
    [TestFixture()]
    public class TerritoryTest
    {
        [Test()]
        public void TestTerritoryEqualsEmpty()
        {
            Territory target = new Territory();
            Territory test = new Territory();
            test.setAdjacencyList(target.getAdjancencies());
            
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

        [Test()]
        public void TestTerritoryAreNotEqualWhenContAreDifferent()
        {
            Territory target = new Territory("A", "");
            Territory test = new Territory("B", "");

            Assert.IsFalse(test.Equals(target));
        }

        [Test()]
        public void TestTerritoryAreNotEqualWhenTroopNumberDiffers()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("A", "a");
            test.addTroops();

            Assert.IsFalse(test.Equals(target));
        }

        [Test()]
        public void TestTerritoryAreNotEqualWhenAdjacenciesAreDifferent()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("A", "a");

            Territory adjacency = new Territory("B", "b");
            target.addAdjancent(adjacency);

            Assert.IsFalse(test.Equals(target));
        }


     
        //toString() method tests
        [Test()]
        public void TestToStringOnEmptyTerritoryReturnsEmpty()
        {
            Territory target = new Territory();

            Assert.AreEqual("Empty", target.ToString());
        }
        
        [Test()]
        public void TestToStringCont()
        {
            Territory target = new Territory("A", "");

            Assert.AreEqual("Continent: A\nTerritory: \nTroops: 0\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [Test()]
        public void TestToStringTerritoryName()
        {
            Territory target = new Territory("", "a");

            Assert.AreEqual("Continent: \nTerritory: a\nTroops: 0\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [Test()]
        public void TestToStringTroops()
        {
            Territory target = new Territory("", "");
            target.addTroops();
            target.saveTroops();

            Assert.AreEqual("Continent: \nTerritory: \nTroops: 1\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [Test()]
        public void TestToStringAdjacencies()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("B", "b");
            target.addAdjancent(test);

            Assert.AreEqual("Continent: A\nTerritory: a\nTroops: 0\nOwner: -1\nAdjacencies: b; ", target.ToString());
        }

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
