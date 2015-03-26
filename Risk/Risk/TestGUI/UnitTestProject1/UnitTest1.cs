using System;
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
    }
}
