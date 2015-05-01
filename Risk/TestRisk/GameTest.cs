using System;
using System.IO;
using Risk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Reflection;

namespace TestRisk
{
    [TestClass]
    public class GameTest
    {
        private readonly Game game = new Game();

        [TestInitialize()]

        [TestMethod]
        public void TestGameInitializes()
        {
            Game newgame = new Game();
            Assert.IsNotNull(newgame);
        }

        [TestMethod]
        public void TestGameConstructorWithOnePlayer()
        {
            //Should throw an error; worry about this later
        }

        [TestMethod]
        public void TestgetPhaseOnConstruction()
        {
            Game game = new Game();
            Assert.AreEqual(0, game.getPhase());

        }
        [TestMethod]
        public void TestgetPhaseAfterReinforce()
        {
            Game game = new Game();
            Player player0 = new Player("player0", 0);
            Player player1 = new Player("player1", 1);
            List<Player> playerList = new List<Player>();
            playerList.Add(player0);
            playerList.Add(player1);

            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 0);
            typeof(Game).GetField("numOfPlayers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);
            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            game.saveReinforcements(game.getCurrentPlayer());
            Assert.AreEqual(1, game.getPhase());
        }
        [TestMethod]
        public void TestgetPhaseAfter2Reinforces()
        {
            Game game = new Game();
            Player player0 = new Player("player0", 0);
            Player player1 = new Player("player1", 1);
            List<Player> playerList = new List<Player>();
            playerList.Add(player0);
            playerList.Add(player1);

            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 0);
            typeof(Game).GetField("numOfPlayers", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);
            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);


            game.saveReinforcements(game.getCurrentPlayer());
            game.endAttack();
            game.endFortify();
            game.saveReinforcements(game.getCurrentPlayer());
            Assert.AreEqual(1, game.getPhase());
        }
        [TestMethod]
        public void TestNextPhase0() 
        {
            Game game = new Game();
            Assert.AreEqual(0, game.getPhase());
        }

        [TestMethod]
        public void TestNextPhase1()
        {
            Game game = new Game();
            game.nextGamePhase();
            Assert.AreEqual(1, game.getPhase());
        }

        [TestMethod]
        public void TestNextPhase2()
        {
            Game game = new Game();
            game.nextGamePhase();
            game.nextGamePhase();
            Assert.AreEqual(2, game.getPhase());
        }


        [TestMethod]
        public void TestNextPhase3()
        {
            Game game = new Game();
            game.nextGamePhase();
            game.nextGamePhase();
            game.nextGamePhase();
            Assert.AreEqual(0, game.getPhase());
        }

        [TestMethod]
        public void TestSaveReinforcementsOneTerritory()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            player.AddTerritory(map.getTerritory("Alaska"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);

            game.getMap().getTerritory("Alaska").addTroops();
            game.saveReinforcements(player);
            Assert.AreEqual(1, map.getTerritory("Alaska").getNumTroops());
        }

        [TestMethod]
        public void TestSaveReinforcementsTwoTerritories()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Territory ontario = new Territory("NA", "Ontario", 1);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            map.Add("Ontario", ontario);
            player.AddTerritory(map.getTerritory("Alaska"));
            player.AddTerritory(map.getTerritory("Ontario"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);

            game.getMap().getTerritory("Alaska").addTroops();
            game.getMap().getTerritory("Ontario").addTroops();
            game.saveReinforcements(player);
            Assert.AreEqual(1, map.getTerritory("Alaska").getNumTroops());
            Assert.AreEqual(1, map.getTerritory("Ontario").getNumTroops());
        }

        [TestMethod]
        public void TestSaveReinforcementsAllTerritories()
        {
            Game game = new Game(); //totally cheating here but whatever
            List<Territory> territories = game.getMap().GetMapAsList();
            Player player = new Player("name", 1, territories);

            foreach (Territory t in territories)
            {
                t.addTroops();
            }
            game.saveReinforcements(player);

            foreach (Territory t in territories)
            {
                Assert.AreEqual(1, t.getNumTroops());
            }
        }

        [TestMethod]
        public void TestClickTerritoryOnceIncrementsTroops()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            player.AddTerritory(map.getTerritory("Alaska"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);
            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);

            game.clickTerritory(map.getTerritory("Alaska"));
            Assert.AreEqual(1, map.getTerritory("Alaska").getTemporaryReinforcements());             
        }

        [TestMethod]
        public void TestClickTerritoryOnceDecrementsReinforcements()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            player.AddTerritory(map.getTerritory("Alaska"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);
            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);
            int num = game.getReinforcements();

            game.clickTerritory(map.getTerritory("Alaska"));
            Assert.AreEqual(num - 1, game.getReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceInAttackPhase()
        {
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            map.Add("Alaska", alaska);
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);
            int num = game.getReinforcements();
            game.nextGamePhase();

            game.clickTerritory(map.getTerritory("Alaska"));
            Assert.AreEqual(num, game.getReinforcements());
            Assert.AreEqual(0, map.getTerritory("Alaska").getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceInFortifyPhase()
        {
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            map.Add("Alaska", alaska);
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);
            int num = game.getReinforcements();
            game.nextGamePhase();
            game.nextGamePhase();

            Assert.AreEqual(num, game.getReinforcements());
            Assert.AreEqual(0, map.getTerritory("Alaska").getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceWhenOutOfTroops()
        {
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            map.Add("Alaska", alaska);
            map.getTerritory("Alaska").setOwner(1);
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);
            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);
            int num = game.getReinforcements();
            for (int i = 0; i < num + 1; i++)
            {
                game.clickTerritory(map.getTerritory("Alaska"));
            }
            Assert.AreEqual(0, game.getReinforcements());
            //Assert.AreEqual(num, map.getTerritory("Alaska").getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestClickTerritoryDoesNotReinforceWhenNotOwned()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 2);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            player.AddTerritory(map.getTerritory("Alaska"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);

            game.clickTerritory(map.getTerritory("Alaska"));
            Assert.AreEqual(0, map.getTerritory("Alaska").getTemporaryReinforcements());
        }

        [TestMethod]

        public void TestResetMethodWorksForOneTerritory()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Player player = new Player("Player 1", 1);
            player.AddTerritory(alaska);
            //link them together
            map.Add("Alaska", alaska);
            player.AddTerritory(map.getTerritory("Alaska"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);

            map.getTerritory("Alaska").addTroops();
            game.resetClick(player);

            Assert.AreEqual(0, map.getTerritory("Alaska").getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestResetMethodWorksForTwoTerritories()
        {
            //make required objects
            Game game = new Game();
            Map map = new Map();
            Territory alaska = new Territory("NA", "Alaska", 1);
            Territory ontario = new Territory("NA", "Ontario", 1);
            Player player = new Player("Player 1", 1);
            //link them together
            map.Add("Alaska", alaska);
            map.Add("Ontario", ontario);
            player.AddTerritory(map.getTerritory("Alaska"));
            player.AddTerritory(map.getTerritory("Ontario"));
            //reflect them into he game object
            typeof(Game).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, map);

            game.getMap().getTerritory("Alaska").addTroops();
            game.getMap().getTerritory("Ontario").addTroops();
            game.resetClick(player);
            Assert.AreEqual(0, map.getTerritory("Alaska").getTemporaryReinforcements());
            Assert.AreEqual(0, map.getTerritory("Ontario").getTemporaryReinforcements());
        }

        [TestMethod]
        public void TestResetMethodWorksForAllTerritories()
        {
            Game game = new Game(); //totally cheating here but whatever
            List<Territory> territories = game.getMap().GetMapAsList();
            Player player = new Player("name", 1, territories);

            foreach (Territory t in territories)
            {
                t.addTroops();
            }
            game.resetClick(player);

            foreach (Territory t in territories)
            {
                Assert.AreEqual(0, t.getTemporaryReinforcements());
            }
        }

        [TestMethod]
        public void TestResetMethodResetsAvailableReinforcements()
        {
            Game game = new Game();
            List<Territory> territories = game.getMap().GetMapAsList();
            int expected = game.getReinforcements();
            Player player = new Player("name", -1, territories);
            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, -1);

            foreach (Territory t in territories)
            {
                game.clickTerritory(t);
            }
            game.resetClick(player);
            Assert.AreEqual(expected, game.getReinforcements());
        }

        [TestMethod]
        //TODO: FIX THE SHIT OUT OF ME
        public void TestCorrectlyFortifyFiveTroops() {
            Game game = new Game(2);
            List<Territory> terrList = new List<Territory>();
            Territory testTerritory1 = new Territory("Terr1", "Dummy cont");
            Territory testTerritory2 = new Territory("Terr2", "Dummy cont");
            testTerritory1.setOwner(0);
            testTerritory2.setOwner(0);
            terrList.Add(testTerritory1);
            terrList.Add(testTerritory2);
            Player p1 = new Player("Dummy Player", 0, terrList);
            typeof(Game).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 0);
            for (int i = 0; i < 15; i++)
            {
                game.clickTerritory(testTerritory1);
            }
            game.saveReinforcements(p1);
            // player 1 sets 15 reinforcements into testTerritory

            game.nextGamePhase();
            // puts game into fortify phase

            game.clickTerritory(testTerritory1);
            game.clickTerritory(testTerritory2);
            for(int i = 0; i < 5; i++) {
                game.clickTerritory(testTerritory2);
            }
            // player selects first territory as testTerritory1, then second as testTerritory2,
            // then clicks testTerritory2 5 times to add 5 troops

            game.endFortify();

            Assert.AreEqual(5, testTerritory2.getNumTroops());
        }

        [TestMethod]
        public void testGenerateReinforcementsNoArgs()
        {
            Game game = new Game(); // game constructor with no args sets numOfPlayers to 6
            Assert.AreEqual(20, game.generateReinforcements());
        }

        [TestMethod]
        public void testGenerateReinforcementsArgs()
        {
            Game game1 = new Game(2);
            Game game2 = new Game(3);
            Game game3 = new Game(4);
            Game game4 = new Game(10);
            Assert.AreEqual(40, game1.generateReinforcements());
            Assert.AreEqual(35, game2.generateReinforcements());
            Assert.AreEqual(30, game3.generateReinforcements());
            Assert.AreEqual(0, game4.generateReinforcements());

        }

        [TestMethod]
        public void testInitializeDeck()
        {
            Game game = new Game();
            game.initializeDeck();

            Assert.AreEqual(42, game.getDeck().Count);
            for (int i = 0; i < 42; i++)
            {
                Assert.AreEqual("Infantry", game.getDeck().Peek().getTroopType());
            }

        }

        [TestMethod]
        public void testDrawCard()
        {
            Game game = new Game();
            game.initializeDeck();

            Card card = game.drawCard(game.getCurrentPlayer());
            Assert.AreEqual("Infantry", card.getTroopType());
        }

        [TestMethod]
        public void testThatDrawingCardRemovesFromDeck()
        {
            Game game = new Game();
            game.initializeDeck();

            Card card = game.drawCard(game.getCurrentPlayer());
            Assert.AreEqual(41, game.getDeck().Count);
        }

        [TestMethod]
        public void testCardGoesIntoPlayerHandOnDraw()
        {
            Game game = new Game();
            game.initializeDeck();

            game.drawCard(game.getCurrentPlayer());
            Assert.AreNotEqual(0, game.getCurrentPlayer().hand.Count);
        }

        [TestMethod]
        public void testCardTurnInNoSets()
        {
            Game game = new Game();
            game.initializeDeck();

            game.drawCard(game.getCurrentPlayer());
            Assert.IsFalse(game.cardTurnIn(game.getCurrentPlayer()));
        }

        [TestMethod]
        public void testCardTurnInOneSet()
        {
            Game game = new Game();
            game.initializeDeck();

            game.drawCard(game.getCurrentPlayer());
            game.drawCard(game.getCurrentPlayer());
            game.drawCard(game.getCurrentPlayer());

            Assert.IsTrue(game.cardTurnIn(game.getCurrentPlayer()));
            Assert.IsNotNull(game.getDiscard());
            Assert.AreEqual(3, game.getDiscard().Count);
        }

        [TestMethod]
        public void testShuffleDeckAlgorithm()
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            for (int i = 1; i < 51; i++)
            {
                list1.Add(i);
            }

            Random rnd = new Random();
            int randomIndex = 0;
            while (list1.Count > 0)
            {
                randomIndex = rnd.Next(0, list1.Count); //Choose a random object in the list
                list2.Add(list1[randomIndex]); //add it to the new, random list
                list1.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            Assert.AreNotEqual(list1, list2);
        }

        [TestMethod]
        public void testShuffleDeck()
        {
            Game game = new Game();
            game.initializeDeck();

            for (int i = 0; i < 42; i++)
            {
                game.drawCard(game.getCurrentPlayer()); //empties deck
            }

            for (int i = 0; i < 14; i++) // player has 14 sets of 3 to turn in; this populates the discard pile 
            {
                game.cardTurnIn(game.getCurrentPlayer());
            }

            Assert.AreEqual(0, game.getCurrentPlayer().hand.Count); // checks to ensure player hand is empty

            List<List<Card>> beforeAndAfterLists = game.shuffleDeck();
            Assert.AreNotEqual(beforeAndAfterLists[0], beforeAndAfterLists[1]);
        }

    }
}
