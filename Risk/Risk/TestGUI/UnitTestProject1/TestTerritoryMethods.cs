using System;
using NUnit.Framework;
using TestGUI;

namespace UnitTestProject1
{
    [TestFixture()]
    public class TestTerritoryMethods
    {

        [Test()]
        public void TestTerritoryInitializes()
        {
            Territory terr = new Territory();
            Assert.IsNotNull(terr);
        }

        //[Test()]
        //public void TestCorrectlyAddedTerritoryToGameMap()
        //{
        //    Territory terr = new Territory();

        //}

        //[Test()]
        //public void TestReturnCorrectNumberOfTroops()
        //{
        //    Game newgame = new Game(2);
        //    int numOfSoldiers = 5;

        //}

    }
}
