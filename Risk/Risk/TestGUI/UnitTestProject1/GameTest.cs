using System;
using NUnit.Framework;
using TestGUI;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestFixture()]
    public class GameTest
    {
        private readonly Game game = new Game();

        [Test()]
        public void TestMakeMapFromXMLEmptyString()
        {
            List<Territory> target = new List<Territory>();
            target = game.makeMapFromXML("");
            Assert.IsNull(target);
        }
    }
}
