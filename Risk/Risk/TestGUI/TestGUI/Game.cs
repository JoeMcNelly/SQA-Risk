﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace TestGUI
{
    //Move all dis to RiskGame.cs at some point
    public class Game
    {

        private int numOfPlayers; //Remove me!!!
        private Dictionary<String, Territory> map;

        private List<Player> players;

        public Game()
        {
            this.map = new Dictionary<String, Territory>();
            this.players = new List<Player>();
        }

        public Game(int numOfPlayers)
        {

            this.numOfPlayers = numOfPlayers;
            this.map = new Dictionary<String, Territory>();
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
            this.map.Add(terr.getName(), terr);
        }

        public Dictionary<String, Territory> getMapList()
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

        public Dictionary<String, Territory> makeMapFromXML(string xml)
        {
            if (xml.Equals(""))
                return null;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            foreach (XmlNode node in doc.DocumentElement.SelectNodes("territory"))
            {
                String name = node.SelectSingleNode("name").InnerText;
                String continent = node.SelectSingleNode("continent").InnerText;

                Territory terr = new Territory(continent, name);
                
                addTerritoryToMap(terr);
            }

            foreach (XmlNode node in doc.DocumentElement.SelectNodes("territory"))
            {
                if (node.SelectSingleNode("adjacent") != null)
                {
                    String adjacencies = node.SelectSingleNode("adjacent").InnerText;
                    string[] adjacenciesList = adjacencies.Split(':');
                    List<Territory> adjacencyList = new List<Territory>();

                    foreach (String adjName in adjacenciesList)
                    {
                        adjacencyList.Add(this.map[adjName]);
                    }
                    this.map[node.SelectSingleNode("name").InnerText].setAdjacencyList(adjacencyList);

                }
            }
            
            return this.map;
        }
    }
}
