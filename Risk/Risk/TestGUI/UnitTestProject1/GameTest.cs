﻿using System;
using System.IO;
using NUnit.Framework;
using TestGUI;
using System.Collections.Generic;
using System.Xml.XPath;

namespace UnitTestProject1
{
    [TestFixture()]
    public class GameTest
    {
        private readonly Game game = new Game();

        [Test()]
        public void TestMakeMapFromXMLEmptyString()
        {
            //if a valid empty xml document is given it simply returns null
            Dictionary<String, Territory> target;

            String str = "";

            target = game.makeMapFromXML(str);
            Assert.IsNull(target);
        }

        [Test()]
        public void TestMakeMapFromXMLOneTerritory()
        {
            Game gameTest = new Game(2);
            Dictionary<String, Territory> target = new Dictionary<String, Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> </territory></territories>";
            target = gameTest.makeMapFromXML(str);
            
            Territory testTerritory = new Territory("North America", "Alaska");

            Assert.AreEqual(testTerritory.ToString(), target["Alaska"].ToString());
        }

        [Test()]
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

        [Test()]
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