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
            Popup p = new Popup();
            Application.Run(p);
            int numPlayers = 0;
            numPlayers = p.getNumPlayers();
            Application.Run(new RiskGame(numPlayers));
        }
    }
}
