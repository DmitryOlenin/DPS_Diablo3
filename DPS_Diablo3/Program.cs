using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DPS_Diablo3
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new dps_diablo());
        }
    }
}
