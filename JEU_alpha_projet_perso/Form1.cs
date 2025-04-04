﻿using MonJeu;

using System;

using System.Drawing;

using System.Windows.Forms;

using System.Media;

using System.Numerics;





namespace JEU_alpha_projet_perso

{

    public partial class Form1 : Form

    {

        private int originalX, originalY;

        private int originalXx, originalYy;

        private int HP = 100;

        private int score = 0;

        private Label hpLabel, scoreLabel;

        private double enemy1Speed = 9;

        private double enemy2Speed = 11;

        private double speedIncrement = 0.256789;



        private System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer killTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer archerTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer knightTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer difficultyTimer = new System.Windows.Forms.Timer();



        private bool archerShootEnemy1 = true;

        private int spawnInterval = 9000;

        private int archerFireRate = 2200;

        private int archerUpgradeCost = 20;



        private bool archerHired = false;

        private int archerLevel = 1;



        private bool knightHired = false;

        private int knightFireRate = 3000;

        private int knightUpgradeCost = 25;



        public Form1()

        {

            SoundPlayer player = new SoundPlayer(@"C:\Users\maxfrossard\Downloads\lucky-streak-christian-game-show-music-4jesus-312720.wav");

            player.PlayLooping();         // Joue le son (asynchrone)







            InitializeComponent();

            originalX = pictureBoxMoving.Location.X;

            originalY = pictureBoxMoving.Location.Y;



            originalXx = pictureBoxEnemy2.Location.X;

            originalYy = pictureBoxEnemy2.Location.Y;



            hpLabel = new Label() { Text = "HP : " + HP, Location = new Point(570, 28), AutoSize = true };

            scoreLabel = new Label() { Text = "Score : " + score, Location = new Point(10, 10), AutoSize = true };

            Controls.Add(hpLabel);

            Controls.Add(scoreLabel);



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



            movementTimer.Start();

            speedTimer.Start();

        }



        private void pictureBoxRecruit_Click(object sender, EventArgs e)

        {

            DialogResult dialogResult = MessageBox.Show("Hire:\nYes = Knight (350)\nNo = Archer (300 / Upgrade " + archerUpgradeCost + ")", "Recruit", MessageBoxButtons.YesNoCancel);



            if (dialogResult == DialogResult.Yes)

            {

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

            if (spawnInterval > 3000)

            {

                spawnInterval -= 1000;

                spawnTimer.Interval = spawnInterval;

            }

        }



        private void SpawnTimer_Tick(object sender, EventArgs e)

        {

            pictureBoxEnemy2.Visible = true;

            pictureBoxEnemy2.Location = new Point(originalXx, originalYy);

            killTimer.Start();

        }



        private void KillTimer_Tick(object sender, EventArgs e)

        {



        }



        private void MovementTimer_Tick(object sender, EventArgs e)

        {

            if (pictureBoxMoving.Visible) pictureBoxMoving.Left += (int)enemy1Speed;

            if (pictureBoxEnemy2.Visible) pictureBoxEnemy2.Left += (int)enemy2Speed;



            if (pictureBoxMoving.Bounds.IntersectsWith(pictureBoxTarget.Bounds)) { HP--; UpdateHpLabel(); pictureBoxMoving.Location = new Point(originalX, originalY); }

            if (pictureBoxEnemy2.Bounds.IntersectsWith(pictureBoxTarget.Bounds)) { HP--; UpdateHpLabel(); pictureBoxEnemy2.Location = new Point(originalXx, originalYy); }



            if (pictureBoxMoving.Left > Width) pictureBoxMoving.Location = new Point(originalX, originalY);

            if (pictureBoxEnemy2.Left > Width) pictureBoxEnemy2.Location = new Point(originalXx, originalYy);



            if (HP <= 0)

            {



                movementTimer.Stop(); speedTimer.Stop(); spawnTimer.Stop(); killTimer.Stop();

                archerTimer.Stop(); loopTimer.Stop(); knightTimer.Stop(); difficultyTimer.Stop();

                MessageBox.Show("GAME OVER!");

                this.Close();

                MenuForm menu = new MenuForm();

                menu.Show();

            }

        }



        private void ArcherTimer_Tick(object sender, EventArgs e)

        {

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

            if (pictureBoxEnemy2.Visible)

            {

                pictureBoxEnemy2.Visible = false;

                pictureBoxEnemy2.Location = new Point(originalXx, originalYy);

                score += 25;

                UpdateScoreLabel();

            }

            else if (pictureBoxMoving.Visible)

            {

                pictureBoxMoving.Visible = false;

                pictureBoxMoving.Location = new Point(originalX, originalY);

                score += 25;

                UpdateScoreLabel();

            }

        }



        private void LoopTimer_Tick(object sender, EventArgs e)

        {

            if (pictureBoxMoving.Visible) { pictureBoxMoving.Visible = false; pictureBoxMoving.Location = new Point(originalX, originalY); score += 10; }

            if (pictureBoxEnemy2.Visible) { pictureBoxEnemy2.Visible = false; pictureBoxEnemy2.Location = new Point(originalXx, originalYy); score += 10; }

            UpdateScoreLabel();

            reappearTimer.Start();

        }



        private void PictureBoxMoving_Click(object sender, EventArgs e)

        {

            pictureBoxMoving.Visible = false;

            pictureBoxMoving.Location = new Point(originalX, originalY);

            score += 25;

            UpdateScoreLabel();

            reappearTimer.Start();

        }



        private void pictureBoxEnemy2_Click(object sender, EventArgs e)

        {

            pictureBoxEnemy2.Visible = false;

            pictureBoxEnemy2.Location = new Point(originalXx, originalYy);

            score += 15;

            UpdateScoreLabel();

            reappearTimer.Start();

        }



        private void ReappearTimer_Tick(object sender, EventArgs e)

        {

            if (!pictureBoxMoving.Visible) pictureBoxMoving.Visible = true;

            if (!pictureBoxEnemy2.Visible) pictureBoxEnemy2.Visible = true;

            reappearTimer.Stop();

        }



        private void UpdateHpLabel()

        {

            hpLabel.Text = "HP : " + HP;

            int newWidth = 800 - (8 * (100 - HP));

            pictureBox3.Size = new Size(Math.Max(0, newWidth), pictureBox3.Height);

        }



        private void UpdateScoreLabel()

        {

            scoreLabel.Text = "Score : " + score;

        }



        private void SpeedTimer_Tick(object sender, EventArgs e)

        {

            enemy1Speed += speedIncrement;

            enemy2Speed += speedIncrement;

        }

    }

}

