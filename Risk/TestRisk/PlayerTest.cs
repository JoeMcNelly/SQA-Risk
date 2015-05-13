using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Risk;
using System.Collections.Generic;

namespace TestRisk
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestgetHand()
        {
            Game newgame = new Game(0);
            Player p = new Player("test", 0);

            Card card = new Card();
   
            p.addCard(card);

            Card testCard = p.getHand()[""];
            Assert.AreEqual(testCard, card);

        }

        [TestMethod]
        public void TestNumberOfPlayersConstructor()
        {
            Game newgame = new Game(0);
            Assert.AreEqual(0, newgame.getPlayers().Count);

        }

        [TestMethod]
        public void TestOwnsAll()
        {
            Map map = new Map(global::TestRisk.Properties.Resources.Map);
            Player p = new Player("test", 0);
            foreach(Territory t in map.GetMapAsList())
            {
                p.AddTerritory(t);
            }
            Assert.AreEqual(p.ownsAll(), true);

        }
    }
}
