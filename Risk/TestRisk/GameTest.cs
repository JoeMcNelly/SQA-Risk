using System;
using System.IO;
using Risk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.XPath;

namespace TestRisk
{
    [TestClass]
    public class GameTest
    {
        private readonly Game game = new Game();

        [TestMethod]
        public void TestGameInitializes()
        {
            Game newgame = new Game();
            Assert.IsNotNull(newgame);
        }

        [TestMethod]
        public void TestGameConstructorWithOnePlayer()
        {
            //Should throw an error; worry about this later
        }

        [TestMethod]
        public void TestgetPhaseOnConstruction()
        {
            Game game = new Game();
            Assert.AreEqual(0, game.getPhase());

        }
        [TestMethod]
        public void TestgetPhaseAfterReinforce()
        {
            Game game = new Game();
            game.saveReinforcements();
            Assert.AreEqual(1, game.getPhase());
        }
        [TestMethod]
        public void TestgetPhaseAfter2Reinforces()
        {
            Game game = new Game();
            game.saveReinforcements();
            game.endAttack();
            game.endFortify();
            game.saveReinforcements();
            Assert.AreEqual(1, game.getPhase());
        }
        [TestMethod]
        public void TestNextPhase0() 
        {
            Game game = new Game();
            Assert.AreEqual(0, game.getPhase());
        }

        [TestMethod]
        public void TestNextPhase1()
        {
            Game game = new Game();
            game.nextGamePhase();
            Assert.AreEqual(1, game.getPhase());
        }

        [TestMethod]
        public void TestNextPhase2()
        {
            Game game = new Game();
            game.nextGamePhase();
            game.nextGamePhase();
            Assert.AreEqual(2, game.getPhase());
        }


        [TestMethod]
        public void TestNextPhase3()
        {
            Game game = new Game();
            game.nextGamePhase();
            game.nextGamePhase();
            game.nextGamePhase();
            Assert.AreEqual(0, game.getPhase());
        }

        [TestMethod]
        public void TestSaveReinforcementsOneTerritory()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            territories[0].addTroops();
            game.saveReinforcements();
            Assert.AreEqual(1, territories[0].getNumTroops());
        }

        [TestMethod]
        public void TestSaveReinforcementsTwoTerritories()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            territories[0].addTroops();
            territories[1].addTroops();
            game.saveReinforcements();
            Assert.AreEqual(1, territories[0].getNumTroops());
            Assert.AreEqual(1, territories[1].getNumTroops());
        }

        [TestMethod]
        public void TestSaveReinforcementsAllTerritories()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            foreach (Territory t in territories)
            {
                t.addTroops();
            }
            game.saveReinforcements();

            foreach (Territory t in territories)
            {
                Assert.AreEqual(1, t.getNumTroops());
            }
        }

        [TestMethod]
        public void TestClickTerritoryOnceIncrementsTroops()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            game.clickTerritory(0);
            Assert.AreEqual(1,territories[0].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryOnceDecrementsReinforcements()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            int num = game.getReinforcements();
            game.clickTerritory(0);
            Assert.AreEqual(num - 1, game.getReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceInAttackPhase()
        {
            Game game = new Game();
            game.nextGamePhase();
            List<Territory> territories = game.getTerritories();
            int num = game.getReinforcements();
            game.clickTerritory(0);
            Assert.AreEqual(num, game.getReinforcements());
            Assert.AreEqual(0, territories[0].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceInFortifyPhase()
        {
            Game game = new Game();
            game.nextGamePhase();
            game.nextGamePhase();
            List<Territory> territories = game.getTerritories();
            int num = game.getReinforcements();
            game.clickTerritory(0);
            Assert.AreEqual(num, game.getReinforcements());
            Assert.AreEqual(0, territories[0].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceWhenOutOfTroops()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            int num = game.getReinforcements();
            for (int i = 0; i < num + 1; i++)
            {
                game.clickTerritory(0);
            }
            Assert.AreEqual(0, game.getReinforcements());
            Assert.AreEqual(num, territories[0].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceWhenNotOwned()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            Territory terr = territories[0];
            int current = game.getCurrentPlayer();
            terr.setOwner(current + 1);
            game.clickTerritory(0);
            Assert.AreEqual(0, territories[0].getTemporaryReinforcements());
        }

        [TestMethod]

        public void TestResetMethodWorksForOneTerritory()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            territories[0].addTroops();
            game.resetClick();
            Assert.AreEqual(0, territories[0].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestResetMethodWorksForTwoTerritories()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            territories[0].addTroops();
            territories[1].addTroops();
            game.resetClick();
            Assert.AreEqual(0, territories[0].getTemporaryReinforcements());
            Assert.AreEqual(0, territories[1].getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestResetMethodWorksForAllTerritories()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            foreach (Territory t in territories)
            {
                t.addTroops();
            }
            game.resetClick();
            foreach (Territory t in territories)
            {
                Assert.AreEqual(0, t.getTemporaryReinforcements());
            }
        }

        [TestMethod]
        public void TestResetMethodResetsAvailableReinforcements()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            int expected = game.getReinforcements();
            int index = 0;
            foreach (Territory t in territories)
            {
                game.clickTerritory(index);
                index++;
            }
            game.resetClick();
            Assert.AreEqual(expected, game.getReinforcements());
        }


    }
}
