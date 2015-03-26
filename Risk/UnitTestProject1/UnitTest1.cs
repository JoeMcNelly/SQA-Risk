using System;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture()]
    public class UnitTest1
    {
        [Test()]
        public void TestGameInitializes()
        {
            Game newgame = new Game(0);
            Assert.IsNotNull(newgame);
        }
    }
}
