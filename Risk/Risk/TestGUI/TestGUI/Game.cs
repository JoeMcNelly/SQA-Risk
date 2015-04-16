using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
    //Move all dis to RiskGame.cs at some point
    public class Game
    {
        private int numOfPlayers; // Remove me!!!
        private List<Territory> map;
        private List<Player> players;

        public Game()
        {
            this.map = new List<Territory>();
            this.players = new List<Player>();
        }

        public Game(int numOfPlayers)
        {
            this.map = new List<Territory>();
            this.players = new List<Player>();

            for (int i = 0; i < numOfPlayers; i++)
            {
                String name = "Player " + i;
                Player player = new Player(name, true); // For testing purposes; remove boolean later
                this.players.Add(player);
            }
        }

        public List<Player> getPlayers() 
        {
            return this.players;
        }

        public void addTerritoryToMap(Territory terr)
        {
            this.map.Add(terr);
        }

        public List<Territory> getMapList()
        {
            return this.map;
        }

        //public void reinforce(Player player, int allowedTroops)
        //{
        //    List<int> troops = new List<int>();
        //    for (int i = 0; i < player.getTerritories().Count; i++)
        //    {
        //        troops.Add(0);
        //    }


        //}

        public bool isOver()
        {
            return true;

        }   

    }
}
