using System;
using System.Windows.Forms;

namespace MonJeu
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();         // Active les styles Windows
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());          // Lancement du menu
        }
    }
}
