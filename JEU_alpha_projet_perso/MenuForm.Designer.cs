/*

 ▄▄▄▄   ▓█████  █    ██  ██▀███    ▄████  ██░ ██▓██   ██▓
▓█████▄ ▓█   ▀  ██  ▓██▒▓██ ▒ ██▒ ██▒ ▀█▒▓██░ ██▒▒██  ██▒
▒██▒ ▄██▒███   ▓██  ▒██░▓██ ░▄█ ▒▒██░▄▄▄░▒██▀▀██░ ▒██ ██░
▒██░█▀  ▒▓█  ▄ ▓▓█  ░██░▒██▀▀█▄  ░▓█  ██▓░▓█ ░██  ░ ▐██▓░
░▓█  ▀█▓░▒████▒▒▒█████▓ ░██▓ ▒██▒░▒▓███▀▒░▓█▒░██▓ ░ ██▒▓░
░▒▓███▀▒░░ ▒░ ░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ░▒   ▒  ▒ ░░▒░▒  ██▒▒▒ 
▒░▒   ░  ░ ░  ░░░▒░ ░ ░   ░▒ ░ ▒░  ░   ░  ▒ ░▒░ ░▓██ ░▒░ 
 ░    ░    ░    ░░░ ░ ░   ░░   ░ ░ ░   ░  ░  ░░ ░▒ ▒ ░░  
 ░         ░  ░   ░        ░           ░  ░  ░  ░░ ░     
      ░                                          ░ ░     
*/
/*
auteur : maxime frossard
date de création : 10 mars 2025
date de modification : 05 mai 2025
description : designer du menu du jeu
*/

namespace MonJeu
{
    partial class MenuForm
    {
        // conteneur pour les composants graphiques
        private System.ComponentModel.IContainer components = null;

        // boutons du menu
        private Button startButton;
        private Button button1; // bouton pour afficher le classement
        private Button creditsButton;
        private Button siteButton;

        // libère les ressources utilisées
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // initialise tous les éléments graphiques de la fenêtre
        private void InitializeComponent()
        {
            // création des boutons
            startButton = new Button();
            button1 = new Button();
            creditsButton = new Button();
            siteButton = new Button();
            SuspendLayout();

            // bouton pour démarrer le jeu
            
            startButton.Click += startButton_Click;

            // bouton pour afficher le classement
            
            button1.Click += button1_Click;

            // bouton pour afficher les crédits
            
            creditsButton.Click += CreditsButton_Click;

            // bouton pour ouvrir le site ou portfolio
            
            siteButton.Click += SiteButton_Click;

            // configuration générale de la fenêtre du menu
            
            ResumeLayout(false);
        }
    }
}
