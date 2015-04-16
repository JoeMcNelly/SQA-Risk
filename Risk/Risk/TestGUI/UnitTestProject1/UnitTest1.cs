﻿using System;
using NUnit.Framework;
using TestGUI;

namespace UnitTestProject1
{
    [TestFixture()]
    public class UnitTest1
    {
        [Test()]
        public void TestGameInitializes()
        {
            Game newgame = new Game();
            Assert.IsNotNull(newgame);
        }

        [Test()]
        public void TestGameConstructorWithOnePlayer()
        {
            //Should throw an error; worry about this later
        }

        [Test()]
        public void TestNumberOfPlayersConstructor()
        {
            Game newgame = new Game(0);
            Assert.AreEqual(0, newgame.getPlayers().Count);
            
        }

        [Test()]
        public void testisOverBasic()
        {
            Game game = new Game();
            Assert.AreEqual(true, game.isOver());
        }

        //[Test()]
        //public void testIsOverWithOnePlayer()
        //{
        //    Game game = new Game(1);
        //    Assert.AreEqual(true, game.isOver());
        //}
    }
}
