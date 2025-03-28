using JEU_alpha_projet_perso;
using System;
using System.Windows.Forms;

namespace MonJeu
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form1 jeu = new Form1();
            jeu.Show();
            this.Hide(); // Cache le menu si tu veux
        }
    }
}
