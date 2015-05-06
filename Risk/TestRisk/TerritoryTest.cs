using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;

namespace TestRisk
{
    [TestClass]
    public class TerritoryTest
    {
        [TestMethod]
        public void TestTerritoryEqualsEmpty()
        {
            Territory target = new Territory();
            Territory test = new Territory();
            test.setAdjacencyList(target.getAdjancencies());
            
            Assert.IsTrue(test.Equals(target));
        }
        [TestMethod]
        public void TestTerritoryIsNotEqualToNullObj()
        {
            Territory target = new Territory();

            Assert.IsFalse(target.Equals(null));
        }

        [TestMethod]
        public void TestTerritoryIsNotEqualToOtherObjects()
        {
            Territory target = new Territory();

            Assert.IsFalse(target.Equals(new Object()));
        }

        [TestMethod]
        public void TestTerritoryAreNotEqualWhenNameDifference()
        {
            Territory target = new Territory("", "A");
            Territory test = new Territory("", "B");

            Assert.IsFalse(test.Equals(target));
        }

        [TestMethod]
        public void TestTerritoryAreNotEqualWhenContAreDifferent()
        {
            Territory target = new Territory("A", "");
            Territory test = new Territory("B", "");

            Assert.IsFalse(test.Equals(target));
        }

        [TestMethod]
        public void TestTerritoryAreNotEqualWhenTroopNumberDiffers()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("A", "a");
            test.addTroops();

            Assert.IsFalse(test.Equals(target));
        }

        [TestMethod]
        public void TestTerritoryAreNotEqualWhenAdjacenciesAreDifferent()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("A", "a");

            Territory adjacency = new Territory("B", "b");
            target.addAdjancent(adjacency);

            Assert.IsFalse(test.Equals(target));
        }


     
        //toString() method tests
        [TestMethod]
        public void TestToStringOnEmptyTerritoryReturnsEmpty()
        {
            Territory target = new Territory();

            Assert.AreEqual("Empty", target.ToString());
        }
        
        [TestMethod]
        public void TestToStringCont()
        {
            Territory target = new Territory("A", "");

            Assert.AreEqual("Continent: A\nTerritory: \nTroops: 0\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [TestMethod]
        public void TestToStringTerritoryName()
        {
            Territory target = new Territory("", "a");

            Assert.AreEqual("Continent: \nTerritory: a\nTroops: 0\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [TestMethod]
        public void TestToStringTroops()
        {
            Territory target = new Territory("", "");
            target.addTroops();
            target.saveTroops();

            Assert.AreEqual("Continent: \nTerritory: \nTroops: 1\nOwner: -1\nAdjacencies: ", target.ToString());
        }

        [TestMethod]
        public void TestToStringAdjacencies()
        {
            Territory target = new Territory("A", "a");
            Territory test = new Territory("B", "b");
            target.addAdjancent(test);

            Assert.AreEqual("Continent: A\nTerritory: a\nTroops: 0\nOwner: -1\nAdjacencies: b; ", target.ToString());
        }

        [TestMethod]
        public void TestTerritoryInitializes()
        {
            Territory terr = new Territory();
            Assert.IsNotNull(terr);
        }

        [TestMethod]
        public void TestCorrectlyAddedTerritoryToGameMap()
        {
            Game newgame = new Game(2);
            Territory terr = new Territory();
            newgame.addTerritoryToMap(terr);
            Assert.IsTrue(newgame.getMap().ContainsTerritory(terr.getName()));

        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void TestCanAttackNoTroops()
        {
            Territory src = new Territory();
            Territory dest = new Territory();
            int numSoldiers = 0;
            for (int i = 0; i < numSoldiers; i++)
            {
                src.addTroops();
                dest.addTroops();
            }
            src.saveTroops();
            dest.saveTroops();

            Assert.IsFalse(src.canAttack(dest));
        }
    }

}
