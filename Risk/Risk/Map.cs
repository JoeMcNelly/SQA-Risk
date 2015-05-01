using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Risk
{
    public class Map
    {
        //Dictionary to hold Graph
        private Dictionary<String, Territory> map;

        //constructor for empty map
        public Map()
        {
            this.map = new Dictionary<String, Territory>();
        }

        //Build map from XML file
        public Map(String XML)
        {
            this.map = new Dictionary<String,Territory>();
            makeMapFromXML(XML);
        }

        public Dictionary<String, Territory> getMap()
        {
            return this.map;
        }
        public List<Territory> GetMapAsList()
        {
            return this.map.Values.ToList();
        }

        public List<Territory> GetTerritoriesByContinent(String cont)
        {
            List<Territory> list = new List<Territory>();
            foreach(Territory t in this.GetMapAsList())
            {
                if (t.getContinent().Equals(cont))
                {
                    list.Add(t);
                }
            }
            return list;
        }

        public void setTerritoryOwner(String territoryName, int player)
        {
            this.map[territoryName].setOwner(player);
        }

        public void Add(String name, Territory territory)
        {
            this.map.Add(name, territory);
        }

        public bool ContainsTerritory(String name)
        {
            return this.map.ContainsKey(name);
        }
        public Territory getTerritory(String name)
        {

            return this.map[name];
        }


        //Creats a Dictionary of Territories from an XML file
        public Dictionary<String, Territory> makeMapFromXML(string xml)
        {
            if (xml.Equals(""))
                return new Dictionary<string,Territory>();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            foreach (XmlNode node in doc.DocumentElement.SelectNodes("territory"))
            {
                String name = node.SelectSingleNode("name").InnerText;
                String continent = node.SelectSingleNode("continent").InnerText;

                Territory terr = new Territory(continent, name);

                this.map.Add(name, terr);
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
                        if(!this.map.ContainsKey(adjName))
                            break;
                        adjacencyList.Add(this.map[adjName]);
                    }
                    this.map[node.SelectSingleNode("name").InnerText].setAdjacencyList(adjacencyList);

                }
            }
            return this.map;
        }

        //Note: going to represent player as an int because its represented as different things in different places
        //Second Note: if we want we can alternativly have this method return the end territory if found and null otherwise
        public Boolean IsInPath(String start, String end, int player)
        {
            if (!this.map.ContainsKey(start) || !this.map.ContainsKey(end))
                return false;

            Queue<Territory> Q = new Queue<Territory>();
            HashSet<Territory> S = new HashSet<Territory>();
            Q.Enqueue(this.map[start]);
            S.Add(this.map[start]);

            while(Q.Count > 0)
            {
                Territory t = Q.Dequeue();
                if (t.getName().Equals(end) && t.getOwner() == player)//if we need to change the player thing, just 
                    return true;                                      //change the == to a .Equals() method and change
                                                                      //the type in the method deff 
                foreach(Territory adjacency in t.getAdjancencies())
                {
                    if(!S.Contains(adjacency) && adjacency.getOwner() == player)
                    {
                        Q.Enqueue(adjacency);
                        S.Add(adjacency);
                    }
                    else
                    {
                        S.Add(adjacency);
                    }
                }
            }
            return false;
        }
    }
}
