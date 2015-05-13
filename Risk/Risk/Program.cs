using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                numPlayers = p.getNumPlayers();
                RiskGame risk = new RiskGame(numPlayers);
                Application.Run(risk);
                List<Player> players = risk.getPlayers();
                StatsScreen stats = new StatsScreen(players);
                Application.Run(stats);
                keepPlaying = stats.stillPlaying();
            }
        }
    }
}
