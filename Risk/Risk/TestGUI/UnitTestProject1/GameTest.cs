using System;
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
            List<Territory> target = new List<Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories></territories>";

            target = game.makeMapFromXML(str);
            Assert.IsNull(target);
        }

        [Test()]
        public void TestMakeMapFromXMLOneTerritory()
        {
            Game gameTest = new Game(2);
            List<Territory> target = new List<Territory>();

            String str = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><territories> <territory> <name>Alaska</name> <continent>North America</continent> <adjacent></adjacent> </territory></territories>";
            target = gameTest.makeMapFromXML(str);
            
            Territory testTerritory = new Territory("North America", "Alaska");

            Assert.AreEqual(testTerritory, target[0]);
        }


    }
}
