using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;

namespace TestRisk
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestIncReinforcement()
        {
        }

        [TestMethod]
        public void TestNumberOfPlayersConstructor()
        {
            Game newgame = new Game(0);
            Assert.AreEqual(0, newgame.getPlayers().Count);

        }
    }
}
