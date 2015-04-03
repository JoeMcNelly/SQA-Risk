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
            StringReader reader = new StringReader(str);

            XPathDocument doc = new XPathDocument(reader);

            target = game.makeMapFromXML(doc);
            Assert.IsNull(target);
        }

        [Test()]
        public void TestMakeMapFromXMLNoTerritory()
        {
            List<Territory> target = new List<Territory>();

            String str = "<?xml version= \" 1.0 \" encoding= \" utf-8 \" ?> <territory> <name>Alaska</name> <adjacent></adjacent> </territory>";
            StringReader reader = new StringReader(str);

            XPathDocument doc = new XPathDocument(reader);

            target = game.makeMapFromXML(doc);

            Territory testTerritory = new Territory("North America", "Alaska");

            Assert.Contains(testTerritory, target);
        }
    }
}
