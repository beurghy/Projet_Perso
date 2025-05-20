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
                                                         


/*
auteur : maxime frossard
date de création : 10 mars 2025
date de modification : 05 mai 2025
description : la page principale où le jeu se déroule
*/

using MonJeu;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JEU_alpha_projet_perso
{
    public partial class Form1 : Form
    {
        private Color backgroundColor = Color.FromArgb(27, 38, 59);
        private Color buttonColor = Color.FromArgb(15, 76, 117);
        private Color buttonHoverColor = Color.FromArgb(50, 130, 184);
        private Color textColor = Color.FromArgb(187, 225, 250);
        private Font titleFont;
        private Font buttonFont;
        private Image backgroundImage;
        private Image gameLogo;
        // durée de survie du joueur
        private TimeSpan survivalTime = TimeSpan.Zero;

        // timers pour gérer différents événements du jeu
        private System.Windows.Forms.Timer survivalTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer killTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer archerTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer knightTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer difficultyTimer = new System.Windows.Forms.Timer();

        // étiquette pour afficher le temps de survie
        private Label survivalLabel;

        // positions de départ des ennemis
        private int originalX, originalY;
        private int originalXx, originalYy;

        // points de vie du joueur
        private int HP = 100;

        // score du joueur
        private int score = 0;

        // étiquettes pour les points de vie et le score
        private Label hpLabel, scoreLabel;

        // vitesses des ennemis
        private double enemy1Speed = 18;
        private double enemy2Speed = 22;

        // augmentation de la vitesse avec le temps
        private double speedIncrement = 0.3;

        // fréquence d’apparition des ennemis
        private int spawnInterval = 9000;

        // cadence de tir de l’archer
        private int archerFireRate = 2200;

        // coût d’amélioration de l’archer
        private int archerUpgradeCost = 20;

        // état d’embauche des alliés
        private bool archerHired = false;
        private bool knightHired = false;

        // niveau de l’archer
        private int archerLevel = 1;

        // cadence de tir du chevalier
        private int knightFireRate = 3000;

        // coût d’amélioration du chevalier
        private int knightUpgradeCost = 25;

        // contrôle pour alterner les tirs de l’archer
        private bool archerShootEnemy1 = true;

        // client http pour enregistrer le score en ligne (base de donnée)
        private static readonly HttpClient client = new HttpClient();
        private string firebaseUrl = "https://jeu-de-tower-defense-default-rtdb.europe-west1.firebasedatabase.app/scores.json";

        public Form1()
        {
            // lance la musique de fond en boucle
            SoundPlayer player = new SoundPlayer(@"C:\Users\maxfrossard\Downloads\lucky-streak-christian-game-show-music-4jesus-312720.wav");
            player.PlayLooping();

            InitializeComponent();

            // mémorise les positions de départ des ennemis
            originalX = pictureBoxMoving.Location.X;
            originalY = pictureBoxMoving.Location.Y;
            originalXx = pictureBoxEnemy2.Location.X;
            originalYy = pictureBoxEnemy2.Location.Y;

            // crée les étiquettes de points de vie et de score
            hpLabel = new Label() { Text = "HP : " + HP, Location = new Point(570, 28), AutoSize = true };
            scoreLabel = new Label() { Text = "Score : " + score };
            Controls.Add(hpLabel);
            Controls.Add(scoreLabel);

            // configure les timers
            spawnTimer.Interval = spawnInterval;
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();

            killTimer.Interval = 10000;
            killTimer.Tick += KillTimer_Tick;

            loopTimer.Interval = 10000;
            loopTimer.Tick += LoopTimer_Tick;

            archerTimer.Interval = archerFireRate;
            archerTimer.Tick += ArcherTimer_Tick;

            knightTimer.Interval = knightFireRate;
            knightTimer.Tick += KnightTimer_Tick;

            difficultyTimer.Interval = 30000;
            difficultyTimer.Tick += DifficultyTimer_Tick;
            difficultyTimer.Start();

            // démarre les autres timers nécessaires
            movementTimer.Start();
            speedTimer.Start();
        }
        private void InitializeCustomDesign()
        {
            // Configuration de base du formulaire
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = backgroundColor;
            this.DoubleBuffered = true;

            // Chargement des polices et images
            try
            {
                titleFont = new Font("Segoe UI", 32, FontStyle.Bold);
                buttonFont = new Font("Segoe UI", 14, FontStyle.Bold);

                // Essayez de charger l'image de fond - utilisez une image par défaut si échoue
                

                // Essayez de charger le logo - utilisez un texte par défaut si échoue
                try
                {
                    gameLogo = Image.FromFile("Resources/logo.png");
                }
                catch
                {
                    gameLogo = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement des ressources: " + ex.Message);
            }

            
        }
        private void SurvivalTimer_Tick(object sender, EventArgs e)
        {
            // ajoute une seconde au temps de survie et met à jour l’affichage
            survivalTime = survivalTime.Add(TimeSpan.FromSeconds(1));
            survivalLabel.Text = "Temps : " + survivalTime.ToString(@"m\:ss");
        }

        private void pictureBoxRecruit_Click(object sender, EventArgs e)
        {
            // affiche la boîte de recrutement
            DialogResult dialogResult = MessageBox.Show("Hire:\nYes = Knight  (300 / Upgrade "+ knightUpgradeCost +")\nNo = Archer (300 / Upgrade " + archerUpgradeCost + ")", "Recruit", MessageBoxButtons.YesNoCancel);

            if (dialogResult == DialogResult.Yes)
            {
                // recrutement ou amélioration du chevalier
                if (!knightHired)
                {
                    if (score >= 300)
                    {
                        score -= 300;
                        knightHired = true;
                        knightTimer.Start();
                        pictureBoxKnight.Image = Properties.Resources.knight;
                    }
                    else MessageBox.Show("Not enough score for Knight!");
                }
                else
                {
                    if (score >= knightUpgradeCost)
                    {
                        score -= knightUpgradeCost;
                        knightFireRate = Math.Max(200, knightFireRate - 300);
                        knightTimer.Interval = knightFireRate;
                        knightUpgradeCost += 25;
                    }
                    else MessageBox.Show("Not enough score to upgrade Knight!");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                // recrutement ou amélioration de l’archer
                if (!archerHired)
                {
                    if (score >= 300)
                    {
                        score -= 300;
                        archerHired = true;
                        archerTimer.Start();
                        pictureBoxArcher.Image = Properties.Resources.okayokayokay;
                    }
                    else MessageBox.Show("Not enough score for Archer!");
                }
                else
                {
                    if (score >= archerUpgradeCost)
                    {
                        score -= archerUpgradeCost;
                        archerFireRate = Math.Max(200, archerFireRate - 300);
                        archerTimer.Interval = archerFireRate;
                        archerUpgradeCost += 20;
                    }
                    else MessageBox.Show("Not enough score to upgrade Archer!");
                }
            }

            UpdateScoreLabel();
        }

        private void DifficultyTimer_Tick(object sender, EventArgs e)
        {
            // rend le jeu plus difficile en accélérant l’apparition des ennemis
            if (spawnInterval > 3000)
            {
                spawnInterval -= 1000;
                spawnTimer.Interval = spawnInterval;
            }
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            // fait apparaître un ennemi
            pictureBoxEnemy2.Visible = true;
            pictureBoxEnemy2.Location = new Point(originalXx, originalYy);
            killTimer.Start();
        }

        private void KillTimer_Tick(object sender, EventArgs e) { }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            // fait bouger les ennemis vers la cible
            if (pictureBoxMoving.Visible) pictureBoxMoving.Left += (int)enemy1Speed;
            if (pictureBoxEnemy2.Visible) pictureBoxEnemy2.Left += (int)enemy2Speed;

            // si un ennemi touche la cible, le joueur perd de la vie
            if (pictureBoxMoving.Bounds.IntersectsWith(pictureBoxTarget.Bounds)) { HP--; UpdateHpLabel(); pictureBoxMoving.Location = new Point(originalX, originalY); }
            if (pictureBoxEnemy2.Bounds.IntersectsWith(pictureBoxTarget.Bounds)) { HP--; UpdateHpLabel(); pictureBoxEnemy2.Location = new Point(originalXx, originalYy); }

            // repositionne les ennemis s’ils sortent de l’écran
            if (pictureBoxMoving.Left > Width) pictureBoxMoving.Location = new Point(originalX, originalY);
            if (pictureBoxEnemy2.Left > Width) pictureBoxEnemy2.Location = new Point(originalXx, originalYy);

            // fin du jeu si le joueur n’a plus de vie
            if (HP <= 0)
            {
                movementTimer.Stop(); speedTimer.Stop(); spawnTimer.Stop(); killTimer.Stop();
                archerTimer.Stop(); loopTimer.Stop(); knightTimer.Stop(); difficultyTimer.Stop();
                survivalTimer.Stop();
                MessageBox.Show("GAME OVER!");
                string pseudo = DemanderPseudo();
                EnregistrerScoreFirebase(pseudo, score, survivalTime).Wait();
                MessageBox.Show($"GAME OVER, {pseudo}!", "Fin du jeu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                MenuForm menu = new MenuForm();
                menu.Show();
            }
        }

        private string DemanderPseudo()
        {
            // demande au joueur d’entrer un pseudo
            Form prompt = new Form() { Width = 400, Height = 200, Text = "Pseudo", FormBorderStyle = FormBorderStyle.FixedDialog, StartPosition = FormStartPosition.CenterScreen, MinimizeBox = false, MaximizeBox = false };
            Label textLabel = new Label() { Left = 50, Top = 40, Text = "Quel est votre pseudo ?" };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "OK", Left = 180, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(inputBox); prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            this.Close();
            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "Joueur";
        }

        private async Task EnregistrerScoreFirebase(string pseudo, int score, TimeSpan temps)
        {
            // envoie le score sur firebase
            var scoreData = new { pseudo = pseudo, score = score, temps = temps.ToString(@"m\:ss"), date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            var json = System.Text.Json.JsonSerializer.Serialize(scoreData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(firebaseUrl, content);
        }

        private void ArcherTimer_Tick(object sender, EventArgs e)
        {
            // archer tire sur un ennemi en alternant
            if (archerShootEnemy1)
            {
                if (pictureBoxMoving.Visible) { pictureBoxMoving.Visible = false; pictureBoxMoving.Location = new Point(originalX, originalY); score += 10; }
            }
            else
            {
                if (pictureBoxEnemy2.Visible) { pictureBoxEnemy2.Visible = false; pictureBoxEnemy2.Location = new Point(originalXx, originalYy); score += 10; }
            }
            archerShootEnemy1 = !archerShootEnemy1;
            UpdateScoreLabel();
        }

        private void KnightTimer_Tick(object sender, EventArgs e)
        {
            // chevalier tue un ennemi
            if (pictureBoxEnemy2.Visible) { pictureBoxEnemy2.Visible = false; pictureBoxEnemy2.Location = new Point(originalXx, originalYy); score += 25; }
            else if (pictureBoxMoving.Visible) { pictureBoxMoving.Visible = false; pictureBoxMoving.Location = new Point(originalX, originalY); score += 25; }
            UpdateScoreLabel();
        }

        private void LoopTimer_Tick(object sender, EventArgs e)
        {
            // tue automatiquement les ennemis et redonne des points
            if (pictureBoxMoving.Visible) { pictureBoxMoving.Visible = false; pictureBoxMoving.Location = new Point(originalX, originalY); score += 10; }
            if (pictureBoxEnemy2.Visible) { pictureBoxEnemy2.Visible = false; pictureBoxEnemy2.Location = new Point(originalXx, originalYy); score += 10; }
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void PictureBoxMoving_Click(object sender, EventArgs e)
        {
            // clic sur ennemi 1 pour le tuer
            pictureBoxMoving.Visible = false;
            pictureBoxMoving.Location = new Point(originalX, originalY);
            score += 45;
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void pictureBoxEnemy2_Click(object sender, EventArgs e)
        {
            // clic sur ennemi 2 pour le tuer
            pictureBoxEnemy2.Visible = false;
            pictureBoxEnemy2.Location = new Point(originalXx, originalYy);
            score += 45;
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void ReappearTimer_Tick(object sender, EventArgs e)
        {
            // réapparition des ennemis
            if (!pictureBoxMoving.Visible) pictureBoxMoving.Visible = true;
            if (!pictureBoxEnemy2.Visible) pictureBoxEnemy2.Visible = true;
            reappearTimer.Stop();
        }

        private void UpdateHpLabel()
        {
            // met à jour les points de vie et la barre de vie
            hpLabel.Text = "HP : " + HP;
            int newWidth = 800 - (8 * (100 - HP));
            pictureBox3.Size = new Size(Math.Max(0, newWidth), pictureBox3.Height);
        }

        private void UpdateScoreLabel()
        {
            // met à jour l’affichage du score
            survivalLabel.Text = "Score : " + score;
        }

        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            // augmente progressivement la vitesse des ennemis
            enemy1Speed += speedIncrement;
            enemy2Speed += speedIncrement;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void AddExitButton()
        {
            // Créer un bouton pour quitter
            Button exitButton = new Button
            {
                Name = "exitButton",
                Text = "X",
                Size = new Size(40, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(150, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += (s, e) => Application.Exit();
            exitButton.MouseEnter += (s, e) => exitButton.BackColor = Color.FromArgb(200, 50, 50);
            exitButton.MouseLeave += (s, e) => exitButton.BackColor = Color.FromArgb(150, 30, 30);

            this.Controls.Add(exitButton);
            exitButton.Location = new Point(this.ClientSize.Width - exitButton.Width - 20, 20);
        }

    }
}
