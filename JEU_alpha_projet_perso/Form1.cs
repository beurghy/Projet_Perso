using MonJeu;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace JEU_alpha_projet_perso
{
    public partial class Form1 : Form
    {
        private int originalX, originalY;
        private int HP = 100;
        private int score = 0;
        private Label hpLabel, scoreLabel;
        private double enemy1Speed = 9;
        private double enemy2Speed = 11;
        private double speedIncrement = 0.256789;

        private bool archerShootEnemy1 = true;
        private int spawnInterval = 2000;
        private int archerFireRate = 2200;
        private int archerUpgradeCost = 20;

        private bool archerHired = false;
        private int archerLevel = 1;

        private bool knightHired = false;
        private int knightFireRate = 3000;
        private int knightUpgradeCost = 25;

        private List<PictureBox> enemyList = new List<PictureBox>();
        private Random rand = new Random();

        private System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer archerTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer knightTimer = new System.Windows.Forms.Timer();

        public Form1()
        {
            InitializeComponent();

            SoundPlayer player = new SoundPlayer(@"C:\Users\maxfrossard\Downloads\lucky-streak-christian-game-show-music-4jesus-312720.wav");
            player.PlayLooping();

            originalX = pictureBoxMoving.Location.X;
            originalY = pictureBoxMoving.Location.Y;

            hpLabel = new Label() { Text = "HP : " + HP, Location = new Point(570, 28), AutoSize = true };
            scoreLabel = new Label() { Text = "Score : " + score, Location = new Point(10, 10), AutoSize = true };
            Controls.Add(hpLabel);
            Controls.Add(scoreLabel);

            // Timers
            spawnTimer.Interval = 2000; // 1 ennemi toutes les 2 secondes
            spawnTimer.Tick += (s, e) => SpawnEnemy();
            spawnTimer.Start();


            archerTimer.Interval = archerFireRate;
            archerTimer.Tick += ArcherTimer_Tick;

            knightTimer.Interval = knightFireRate;
            knightTimer.Tick += KnightTimer_Tick;
        }

        private void SpawnEnemy()
        {
            PictureBox enemy = new PictureBox();
            enemy.Image = rand.Next(2) == 0 ? Properties.Resources.goblin : Properties.Resources.pourquoi_removebg_preview;
            enemy.Tag = "enemy";
            enemy.Size = new Size(66, 64);
            enemy.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy.BackColor = Color.Transparent;

            // Position à l'intérieur de pictureBox4
            Point spawnPoint = pictureBox4.PointToScreen(new Point(0, rand.Next(0, pictureBox4.Height - enemy.Height)));
            spawnPoint = this.PointToClient(spawnPoint); // convertir en coordonnées du formulaire
            enemy.Location = spawnPoint;
            enemy.Parent = this;

            enemy.Click += (s, e) =>
            {
                PictureBox clicked = (PictureBox)s;
                Controls.Remove(clicked);
                enemyList.Remove(clicked);
                score += 15;
                UpdateScoreLabel();
            };

            enemyList.Add(enemy);
            Controls.Add(enemy);
            enemy.BringToFront();
        }


        private void ArcherTimer_Tick(object sender, EventArgs e)
        {
            if (enemyList.Count > 0)
            {
                var target = enemyList[0];
                Controls.Remove(target);
                enemyList.RemoveAt(0);
                score += 10;
                UpdateScoreLabel();
            }
        }

        private void KnightTimer_Tick(object sender, EventArgs e)
        {
            if (enemyList.Count > 0)
            {
                var target = enemyList[enemyList.Count - 1];
                Controls.Remove(target);
                enemyList.RemoveAt(enemyList.Count - 1);
                score += 25;
                UpdateScoreLabel();
            }
        }

        private void UpdateScoreLabel()
        {
            scoreLabel.Text = "Score : " + score;
        }
    }
}
