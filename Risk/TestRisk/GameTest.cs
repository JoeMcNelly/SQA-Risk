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
            game.turnOffInit();
            game.saveReinforcements(game.getCurrentPlayer());
            Assert.AreEqual(1, game.getPhase());
        }
        [TestMethod]
        public void TestgetPhaseAfter2Reinforces()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);
            game.turnOffInit();

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
            game.turnOffInit();
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
            game.turnOffInit();
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
            game.turnOffInit();
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
            game.turnOffInit();
            foreach (Territory t in territories)
            {
                game.clickTerritory(t);
            }
            game.resetClick(player);
            Assert.AreEqual(expected, game.getReinforcements());
        }

        [TestMethod]
        //TODO: FIX THE SHIT OUT OF ME
        public void TestCorrectlyFortifyFiveTroops()
        {
            //this test is sooooo aggravating to work around


            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Peru");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Brazil");
            p1Owned.Add(t2);
            Territory t3 = game.getMap().getTerritory("Congo");
            p2Owned.Add(t3);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
                t3.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t3.saveTroops();
            t1.setOwner(0);
            t2.setOwner(0);
            t3.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);
            game.turnOffInit();
            for (int i = 0; i < 15; i++)
            {
                game.clickTerritory(t1);
            }
            game.saveReinforcements(p1);
            // player 1 sets 15 reinforcements into testTerritory

            game.endAttack();
            // puts game into fortify phase

            game.clickTerritory(t1);
            game.clickTerritory(t2);
            for (int i = 0; i < 5; i++)
            {
                game.clickTerritory(t2);
            }
            // player selects first territory as testTerritory1, then second as testTerritory2,
            // then clicks testTerritory2 5 times to add 5 troops

            game.endFortify();

            Assert.AreEqual(7, t2.getNumTroops());
        }

        [TestMethod]
        public void testGenerateReinforcementsNoArgs()
        {
            Game game = new Game(); // game constructor with no args sets numOfPlayers to 6
            Assert.AreEqual(13, game.initialReinforcements());
        }

        [TestMethod]
        public void testGenerateReinforcementsArgs()
        {
            Game game1 = new Game(2);
            Game game2 = new Game(3);
            Game game3 = new Game(4);
            Assert.AreEqual(33, game1.initialReinforcements());
            Assert.AreEqual(28, game2.initialReinforcements());
            Assert.AreEqual(23, game3.initialReinforcements());


        }
        [TestMethod]
        public void TestTerrReinforcements1Terr()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Territory> terr = new List<Territory>();
            terr.Add(new Territory());
            Player p = new Player("test", 0, terr);
            Assert.AreEqual(3, game.getTerritoryBonus());
        }
        [TestMethod]
        public void TestTerrReinforcements12Terr()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> terr = new List<Territory>();
            Player p = new Player("test", 0, terr);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);
            
            for (int i = 0; i < 12;i++ )
                terr.Add(new Territory());
            
            Assert.AreEqual(4, game.getTerritoryBonus());
        }
        [TestMethod]
        public void TestTerrReinforcements15Terr()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> terr = new List<Territory>();
            Player p = new Player("test", 0, terr);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            for (int i = 0; i < 15; i++)
                terr.Add(new Territory());

            Assert.AreEqual(5, game.getTerritoryBonus());
        }
        [TestMethod]
        public void TestTerrReinforcements37Terr()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> terr = new List<Territory>();
            Player p = new Player("test", 0, terr);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            for (int i = 0; i < 37; i++)
                terr.Add(new Territory());

            Assert.AreEqual(12, game.getTerritoryBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsNone()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory alaska = game.getMap().getTerritory("Alaska");
            territoriesOwned.Add(alaska);
            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(0, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsOne()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Venezuela");
            Territory t2 = game.getMap().getTerritory("Brazil");
            Territory t3 = game.getMap().getTerritory("Peru");
            Territory t4 = game.getMap().getTerritory("Argentina");
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);
            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(2, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsTwo()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Venezuela");
            Territory t2 = game.getMap().getTerritory("Brazil");
            Territory t3 = game.getMap().getTerritory("Peru");
            Territory t4 = game.getMap().getTerritory("Argentina");
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);

            Territory t5 = game.getMap().getTerritory("Eastern Australia");
            Territory t6 = game.getMap().getTerritory("Indonesia");
            Territory t7 = game.getMap().getTerritory("New Guinea");
            Territory t8 = game.getMap().getTerritory("Western Australia");
            territoriesOwned.Add(t5);
            territoriesOwned.Add(t6);
            territoriesOwned.Add(t7);
            territoriesOwned.Add(t8);


            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(4, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsNorthAmerica()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Alaska");
            Territory t2 = game.getMap().getTerritory("Alberta");
            Territory t3 = game.getMap().getTerritory("Central America");
            Territory t4 = game.getMap().getTerritory("Eastern United States");
            Territory t5 = game.getMap().getTerritory("Greenland");
            Territory t6 = game.getMap().getTerritory("Northwest Territory");
            Territory t7 = game.getMap().getTerritory("Ontario");
            Territory t8 = game.getMap().getTerritory("Quebec");
            Territory t9 = game.getMap().getTerritory("Western United States");
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);
            territoriesOwned.Add(t5);
            territoriesOwned.Add(t6);
            territoriesOwned.Add(t7);
            territoriesOwned.Add(t8);
            territoriesOwned.Add(t9);



            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(5, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsEurope()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Great Britain");
            Territory t2 = game.getMap().getTerritory("Iceland");
            Territory t3 = game.getMap().getTerritory("Northern Europe");
            Territory t4 = game.getMap().getTerritory("Scandinavia");
            Territory t5 = game.getMap().getTerritory("Southern Europe");
            Territory t6 = game.getMap().getTerritory("Ukraine");
            Territory t7 = game.getMap().getTerritory("Western Europe");
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);
            territoriesOwned.Add(t5);
            territoriesOwned.Add(t6);
            territoriesOwned.Add(t7);



            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(5, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsAsia()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Afghanistan");
            Territory t2 = game.getMap().getTerritory("China");
            Territory t3 = game.getMap().getTerritory("India");
            Territory t4 = game.getMap().getTerritory("Irkutsk");
            Territory t5 = game.getMap().getTerritory("Japan");
            Territory t6 = game.getMap().getTerritory("Kamchatka");
            Territory t7 = game.getMap().getTerritory("Middle East");
            Territory t8 = game.getMap().getTerritory("Mongolia");
            Territory t9 = game.getMap().getTerritory("Siam");
            Territory t10 = game.getMap().getTerritory("Siberia");
            Territory t11 = game.getMap().getTerritory("Ural");
            Territory t12 = game.getMap().getTerritory("Yakutsk");
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);
            territoriesOwned.Add(t5);
            territoriesOwned.Add(t6);
            territoriesOwned.Add(t7);
            territoriesOwned.Add(t8);
            territoriesOwned.Add(t9);
            territoriesOwned.Add(t10);
            territoriesOwned.Add(t11);
            territoriesOwned.Add(t12);



            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(7, game.getContinentBonus());
        }

        [TestMethod]
        public void TestContinentReinforcementsAfrica()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();
            List<Territory> territoriesOwned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            Territory t2 = game.getMap().getTerritory("East Africa");
            Territory t3 = game.getMap().getTerritory("Egypt");
            Territory t4 = game.getMap().getTerritory("Madagascar");
            Territory t5 = game.getMap().getTerritory("North Africa");
            Territory t6 = game.getMap().getTerritory("South Africa");
            
            territoriesOwned.Add(t1);
            territoriesOwned.Add(t2);
            territoriesOwned.Add(t3);
            territoriesOwned.Add(t4);
            territoriesOwned.Add(t5);
            territoriesOwned.Add(t6);
            



            Player p = new Player("test", 0, territoriesOwned);
            playerList.Add(p);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            Assert.AreEqual(3, game.getContinentBonus());
        }

        [TestMethod]
        public void testThatDrawingCardRemovesFromDeck()
        {
            Game game = new Game();
            game.initializeDeck();

            game.drawCard();
            Assert.AreEqual(41, game.getDeck().Count);
        }

        [TestMethod]
        public void testCardGoesIntoPlayerHandOnDraw()
        {
            Game game = new Game();
            game.initializeDeck();

            game.drawCard();
            Assert.AreNotEqual(0, game.getCurrentPlayer().getHand().Count);
        }

        [TestMethod]
        public void testAttackDefenderWinsTwoRolls()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(1);
            attackRolls.Add(1);
            attackRolls.Add(1);
            defendRolls.Add(6);
            defendRolls.Add(6);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            game.attack();

            Assert.AreEqual(3, t1.getNumTroops());
            Assert.AreEqual(5, t2.getNumTroops());
                   
        }

        [TestMethod]
        public void testAttackAttackerWinsTwoRolls()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();

            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            game.attack();

            Assert.AreEqual(5, t1.getNumTroops());
            Assert.AreEqual(3, t2.getNumTroops());

        }

        [TestMethod]
        public void testAttackAttackerTakesOverTerritory()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            Assert.AreEqual(1, t2.getOwner());
            game.attack();

            Assert.AreEqual(2, t1.getNumTroops());
            Assert.AreEqual(3, t2.getNumTroops());
            Assert.AreEqual(0, t2.getOwner());

        }

        [TestMethod]
        public void TestWinningAttackerDrawsCardAtEndOfTurn()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);

            int cardsBeforeAttack = p1.getHand().Count;

            game.attack();
            Assert.AreEqual(cardsBeforeAttack + 1, game.getCurrentPlayer().getHand().Count);
        }
        [TestMethod]
        public void TestMaxCardsDrawnPerTurnIsOne()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);
            Territory t4 = game.getMap().getTerritory("South Africa");
            p1Owned.Add(t4);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);
            Territory t3 = game.getMap().getTerritory("Egypt");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
                t4.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
                t3.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t3.saveTroops();
            t4.saveTroops();
            t1.setOwner(0);
            t4.setOwner(0);
            t2.setOwner(1);
            t3.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);

            int cardsBeforeAttack = p1.getHand().Count;
            int cardsInDeckBeforeAttack = game.getDeck().Count;
            //take first territory
            game.attack();

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t4);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t3);
           
            game.attack();

            Assert.AreEqual(0, t2.getOwner());
            Assert.AreEqual(0, t3.getOwner());
            Assert.AreEqual(cardsBeforeAttack + 1, game.getCurrentPlayer().getHand().Count);
            Assert.AreEqual(cardsInDeckBeforeAttack - 1, game.getDeck().Count);
        }

        [TestMethod]
        public void testTroopsKilledIncreasesFromSuccessfulAttack()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            Assert.AreEqual(0, p1.getTroopsKilled());
            Assert.AreEqual(0, p2.getTroopsKilled());
            game.attack();

            Assert.AreEqual(2, p1.getTroopsKilled());
            Assert.AreEqual(0, p2.getTroopsKilled());

        }

        [TestMethod]
        public void testTroopsLostIncreasesFromUnsuccessfulAttack()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(1);
            attackRolls.Add(1);
            defendRolls.Add(4);
            defendRolls.Add(4);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            Assert.AreEqual(0, p1.getLostTroops());
            Assert.AreEqual(0, p2.getLostTroops());
            game.attack();

            Assert.AreEqual(1, p1.getLostTroops());
            Assert.AreEqual(1, p2.getLostTroops());

        }

        [TestMethod]
        public void testTerritoriesLostIncreasesFromBeingAttackedAndLosing()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            Assert.AreEqual(0, p1.getTerritoriesLost());
            Assert.AreEqual(0, p2.getTerritoriesLost());
            game.attack();

            Assert.AreEqual(0, p1.getTerritoriesLost());
            Assert.AreEqual(1, p2.getTerritoriesLost());

        }

        [TestMethod]
        public void testTerritoriesConqueredIncreasesFromAttackingAndWinning()
        {
            Game game = new Game();
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);


            Assert.AreEqual(0, p1.getTerritoriesConquered());
            Assert.AreEqual(0, p2.getTerritoriesConquered());
            game.attack();

            Assert.AreEqual(1, p1.getTerritoriesConquered());
            Assert.AreEqual(0, p2.getTerritoriesConquered());

        }
        [TestMethod]
        public void TestGameShouldNotEnd()
        {
            Game game = new Game(2);
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            Territory t3 = game.getMap().getTerritory("China");
            p2Owned.Add(t2);
            p2Owned.Add(t3);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
                t3.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);
            t3.saveTroops();
            t3.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);



            game.checkEndGame();
            Assert.IsFalse(game.isOver());

        }
        [TestMethod]
        public void TestGameShouldEnd()
        {
            Game game = new Game(2);
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            Territory t3 = game.getMap().getTerritory("China");
            p2Owned.Add(t2);
            p2Owned.Add(t3);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
                t3.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);
            t3.saveTroops();
            t3.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);
            typeof(Game).GetField("playersLeft", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, 1);


            game.checkEndGame();
            Assert.IsTrue(game.isOver());

        }
        [TestMethod]
        public void TestGameEndsByAttacking()
        {
            Game game = new Game(2);
            game.turnOffInit();
            List<Player> playerList = new List<Player>();

            List<Territory> p1Owned = new List<Territory>();
            Territory t1 = game.getMap().getTerritory("Congo");
            p1Owned.Add(t1);

            List<Territory> p2Owned = new List<Territory>();
            Territory t2 = game.getMap().getTerritory("Madagascar");
            p2Owned.Add(t2);

            for (int i = 0; i < 5; i++)
            {
                t1.addTroops();
            }
            for (int i = 0; i < 2; i++)
            {
                t2.addTroops();
            }
            t1.saveTroops();
            t2.saveTroops();
            t1.setOwner(0);
            t2.setOwner(1);


            Player p1 = new Player("test1", 0, p1Owned);
            Player p2 = new Player("test2", 1, p2Owned);
            playerList.Add(p1);
            playerList.Add(p2);

            typeof(Game).GetField("players", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, playerList);

            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(5);
            attackRolls.Add(5);
            defendRolls.Add(1);
            defendRolls.Add(1);

            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            typeof(Game).GetField("source", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t1);
            typeof(Game).GetField("dest", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, t2);

            game.attack();

            Assert.IsTrue(game.isOver());


        }
        [TestMethod]
        public void TestGetAttackerRolls531()
        {
            Game game = new Game(2);
            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(5);
            attackRolls.Add(3);
            attackRolls.Add(1);


            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            Assert.AreEqual("5 : 3 : 1", game.getAttackerRolls());
        }
        [TestMethod]
        public void TestGetAttackerRolls321()
        {
            Game game = new Game(2);
            List<int> attackRolls = new List<int>();
            List<int> defendRolls = new List<int>();
            attackRolls.Add(3);
            attackRolls.Add(2);
            attackRolls.Add(1);


            typeof(Game).GetField("attackerRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, attackRolls);
            Assert.AreEqual("3 : 2 : 1", game.getAttackerRolls());
        }
        [TestMethod]
        public void TestGetDefenderRolls1()
        {
            Game game = new Game(2);
            List<int> defendRolls = new List<int>();

            defendRolls.Add(1);


            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            Assert.AreEqual("1", game.getDefenderRolls());
        }
        [TestMethod]
        public void TestGetDefenderRolls2()
        {
            Game game = new Game(2);
            List<int> defendRolls = new List<int>();

            defendRolls.Add(2);


            typeof(Game).GetField("defenderRolls", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(game, defendRolls);
            Assert.AreEqual("2", game.getDefenderRolls());
        }
       
    }
}
