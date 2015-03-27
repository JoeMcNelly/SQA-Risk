﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGUI
{
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
        }

        public int getNumOfPlayers()
        {
            return this.numOfPlayers;
        }
    }
}
