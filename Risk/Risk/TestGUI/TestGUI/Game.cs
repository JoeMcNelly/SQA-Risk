using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace TestGUI
{
    //Move all dis to RiskGame.cs at some point
    public class Game
    {
        private int numOfPlayers;
        private List<Territory> map;
        private List<Player> players;

        public Game()
        {

        }

        public Game(int numOfPlayers)
        {
            this.numOfPlayers = numOfPlayers;
            this.map = new List<Territory>();
            this.players = new List<Player>();
        }

        public int getNumOfPlayers()
        {
            return this.numOfPlayers;
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
        public List<Territory> makeMapFromXML(String xmlString)
        {
            return null;
        }
    }
}
