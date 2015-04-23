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
        public void TestMakeMapFromXMLEmptyString()
        {
            //if a valid empty xml document is given it simply returns null
            Dictionary<String, Territory> target;

            String str = "";

            target = game.makeMapFromXML(str);
            Assert.IsNull(target);
        }

        [TestMethod]
        public void TestMakeMapFromXMLOneTerritory()
        {
            Game gameTest = new Game(2);
            Dictionary<String, Territory> target = new Dictionary<String, Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> </territory></territories>";
            target = gameTest.makeMapFromXML(str);
            
            Territory testTerritory = new Territory("North America", "Alaska");

            Assert.AreEqual(testTerritory.ToString(), target["Alaska"].ToString());
        }

        [TestMethod]
        public void TestMakeMapFromXMLTwoTerritories()
        {
            Game gameTest = new Game(2);
            Dictionary<String, Territory> target = new Dictionary<String, Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> </territory> <territory> <name>Northwest Territory</name> <continent>North America</continent> </territory></territories>";
            target = gameTest.makeMapFromXML(str);

            Territory testTerritory = new Territory("North America", "Alaska");
            Territory testTerritory2 = new Territory("North America", "Northwest Territory");

            Assert.AreEqual(testTerritory.ToString(), target["Alaska"].ToString());
            Assert.AreEqual(testTerritory2.ToString(), target["Northwest Territory"].ToString());
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
        public void TestSaveReinforcements()
        {
            Game game = new Game();
            List<Territory> territories = game.getTerritories();
            territories[1].addTroops();
            game.saveReinforcements();
            Assert.AreEqual(1, territories[1].getNumTroops());
        }



        [TestMethod]
        public void TestMakeMapFromXMLWithAdjacent()
        {
            Game gameTest = new Game(2);
            Dictionary<String, Territory> target = new Dictionary<String, Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                            "<territories>" +
                              "<territory>" +
                                "<name>Alaska</name>" +
                                "<continent>North America</continent>" +
                                "<adjacent>Northwest Territory</adjacent>" +
                              "</territory>" +
                              "<territory>" +
                                "<name>Northwest Territory</name>" +
                                "<continent>North America</continent>" +
                                "<adjacent>Alaska:Alberta</adjacent>" +
                              "</territory>" +
                              "<territory>" +
                                "<name>Alberta</name>" +
                                "<continent>North America</continent>" +
                                "<adjacent>Northwest Territory</adjacent>" +
                              "</territory>" +
                            "</territories>";
            target = gameTest.makeMapFromXML(str);

            Territory testTerritory = new Territory("North America", "Alaska");
            Territory testTerritory2 = new Territory("North America", "Northwest Territory");

            testTerritory.addAdjancent(testTerritory2);

            Assert.AreEqual(gameTest.getMapList()["Alaska"].ToString(), testTerritory.ToString());
        }


    }
}
