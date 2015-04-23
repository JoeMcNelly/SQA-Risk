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
        public Map(String path)
        {
            this.map = new Dictionary<String,Territory>();
            makeMapFromXML(System.IO.File.ReadAllText(path));
        }

        public Dictionary<String, Territory> getMap()
        {
            return this.map;
        }

        public void setTerritoryOwner(String territoryName, int player)
        {
            this.map[territoryName].setOwner(player);
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
                        adjacencyList.Add(this.map[adjName]);
                    }
                    this.map[node.SelectSingleNode("name").InnerText].setAdjacencyList(adjacencyList);

                }
            }
            return this.map;
        }

        //going to represent player as an int because its represented as different things in different places
        public Boolean IsInPath(Territory start, Territory end, int player)
        {
            return false;
        }
    }
}
