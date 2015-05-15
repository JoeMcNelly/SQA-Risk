using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Risk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool keepPlaying = true;
            while (keepPlaying)
            {
                
                Popup p = new Popup();
                Application.Run(p);
                int numPlayers = 0;
                List<Color> colorList = new List<Color>();
                List<String> playerNames = new List<String>();
                numPlayers = p.getNumPlayers();
                colorList = p.getPlayerColors();
                playerNames = p.getPlayerNames();
                RiskGame risk = new RiskGame(numPlayers, playerNames, colorList);
                Application.Run(risk);
                List<Player> players = risk.getPlayers();
                StatsScreen stats = new StatsScreen(players);
                Application.Run(stats);
                keepPlaying = stats.stillPlaying();
            }
        }
    }
}
