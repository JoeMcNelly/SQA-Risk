﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;

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

        //need to preform this test with a mock, dont want to do that now, it's 6:30 am
        //[TestMethod]
        //public void TestMakeMapFromXMLEmptyString()
        //{
            //if a valid empty xml document is given it simply returns null
        //    Map target = new Map("");
        //
        //    Assert.IsNotNull(target.getMap());
        //}

        [TestMethod]
        public void TestMakeMapFromXMLOneTerritory()
        {
            Map target = new Map();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> </territory></territories>";
            target.makeMapFromXML(str);

            Territory testTerritory = new Territory("North America", "Alaska");

            Assert.AreEqual(testTerritory.ToString(), target.getMap()["Alaska"].ToString());
        }

        [TestMethod]
        public void TestMakeMapFromXMLTwoTerritories()
        {
            Map target = new Map();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> </territory> <territory> <name>Northwest Territory</name> <continent>North America</continent> </territory></territories>";
            target.makeMapFromXML(str);

            Territory testTerritory = new Territory("North America", "Alaska");
            Territory testTerritory2 = new Territory("North America", "Northwest Territory");

            Assert.AreEqual(testTerritory.ToString(), target.getMap()["Alaska"].ToString());
            Assert.AreEqual(testTerritory2.ToString(), target.getMap()["Northwest Territory"].ToString());
        }

        [TestMethod]
        public void TestMakeMapFromXMLWithAdjacent()
        {
            Map target = new Map();

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
            target.makeMapFromXML(str);

            Territory testTerritory = new Territory("North America", "Alaska");
            Territory testTerritory2 = new Territory("North America", "Northwest Territory");

            testTerritory.addAdjancent(testTerritory2);

            Assert.AreEqual(target.getMap()["Alaska"].ToString(), testTerritory.ToString());
        }

        String testXML = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                        "<territories>" +
                          "<territory>" +
                            "<name>Alaska</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Northwest Territory:Alberta:Kamchatka</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Alberta</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Northwest Territory:Alaska:Ontario:Western United States</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Central America</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Western United States:Eastern United States:Venezuela</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Eastern United States</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Quebec:Ontario:Western United States:Central America</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Greenland</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Quebec:Ontario:Northwest Territory:Iceland</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Northwest Territory</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Alaska:Alberta:Ontario:Greenland</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Ontario</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Quebec:Northwest Territory:Alberta:Western United States:Eastern United States</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Quebec</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Ontario:Eastern United States:Greenland</adjacent>" +
                          "</territory>" +
                          "<territory>" +
                            "<name>Western United States</name>" +
                            "<continent>North America</continent>" +
                            "<adjacent>Alberta:Ontario:Eastern United States:Central America</adjacent>" +
                          "</territory>" +
                        "</territories>";


        //Dont think I can finish this before 9:30 so I set up all the test to be written

        [TestMethod]
        public void TestIsInPathForStartNotInMap()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            target.setTerritoryOwner("Alaska", 1);

            Assert.IsFalse(target.IsInPath("test", "Alaska", 1));
        }
        [TestMethod]
        public  void TestIsInPathForEndNotInMap()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            target.setTerritoryOwner("Alaska", 1);

            Assert.IsFalse(target.IsInPath("Alaska", "test", 1));
        }

        [TestMethod]
        public void TestIsInPathForSelf()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            target.setTerritoryOwner("Alaska", 1);


            Assert.IsTrue(target.IsInPath("Alaska", "Alaska", 1));
        }

        [TestMethod]
        public void TestIsInPathForDirectlyAdjacentTerr()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            target.setTerritoryOwner("Alaska", 1);
            target.setTerritoryOwner("Alberta", 1);

            Assert.IsTrue(target.IsInPath("Alaska", "Alberta", 1));
        }

       

        [TestMethod]
        public void TestIsInPathForTerrSeperatedByOne()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            //going to use the -1 player since all territories are set to it by default

            Assert.IsTrue(target.IsInPath("Alaska", "Ontario", -1));
        }

        [TestMethod]
        public void TestIsInPathForDirectlyAdjacentTerrThatIsNotOwned()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);
            target.setTerritoryOwner("Alaska", 1);

            Assert.IsFalse(target.IsInPath("Alaska", "Alberta", 1));
        }

        [TestMethod]
        public void TestIsInPathForTerrSeperatedByOneWhereStartAndEndAreOwnedButNotPath()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);

            target.setTerritoryOwner("Alaska", 1);
            target.setTerritoryOwner("Greenland", 1);

            Assert.IsFalse(target.IsInPath("Alaska", "Greenland", 1));

        }
        [TestMethod]
        public void TestIsInPathForTerrSeperatedByOneWhereStartAndEndAreOwnedButNotPathTwoAway()
        {
            Map target = new Map();
            target.makeMapFromXML(testXML);

            target.setTerritoryOwner("Alaska", 1);
            target.setTerritoryOwner("Northwest Territory", 2);
            target.setTerritoryOwner("Alberta", 2);
            target.setTerritoryOwner("Ontario", 2);
            target.setTerritoryOwner("Central America", 1);

            Assert.IsFalse(target.IsInPath("Alaska", "Central America", 1));

        }

        [TestMethod]
        public void TestGetTerritoriesByContinentAfricaContainsCongo()
        {
            Map target = new Map(global::TestRisk.Properties.Resources.Map);

            List<Territory> terrs = target.GetTerritoriesByContinent("Africa");
            Assert.IsTrue(terrs.Contains(target.getTerritory("Congo")));
        }

        [TestMethod]
        public void TestGetTerritoriesByContinentAfricaContainsAll()
        {
            Map target = new Map(global::TestRisk.Properties.Resources.Map);

            List<Territory> terrs = target.GetTerritoriesByContinent("Africa");
            Assert.IsTrue(terrs.Contains(target.getTerritory("Congo")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("East Africa")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("Egypt")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("Madagascar")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("North Africa")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("South Africa")));
            Assert.AreEqual(6, terrs.Count);
        }

        [TestMethod]
        public void TestGetTerritoriesByContinentSouthAmericaContainsAll()
        {
            Map target = new Map(global::TestRisk.Properties.Resources.Map);

            List<Territory> terrs = target.GetTerritoriesByContinent("South America");
            Assert.IsTrue(terrs.Contains(target.getTerritory("Brazil")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("Peru")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("Venezuela")));
            Assert.IsTrue(terrs.Contains(target.getTerritory("Argentina")));
            Assert.AreEqual(4, terrs.Count);
        }
        [TestMethod]
        public void TestMakeMapFromBlankXML()
        {
            var map = new Map();
            map.makeMapFromXML("");
            Assert.IsTrue(map.getMap().Count == 0);
        }

    }
}
