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
        private int spawnInterval = 9000;
        private int archerFireRate = 2200;
        private int archerUpgradeCost = 20;

        private bool archerHired = false;
        private int archerLevel = 1;

        private bool knightHired = false;
        private int knightFireRate = 3000;
        private int knightUpgradeCost = 25;

        private List<PictureBox> enemyList = new List<PictureBox>();
        private Random rand = new Random();

        public Form1()
        {
            SoundPlayer player = new SoundPlayer(@"C:\Users\maxfrossard\Downloads\lucky-streak-christian-game-show-music-4jesus-312720.wav");
            player.PlayLooping();

            InitializeComponent(); // ← doit venir AVANT toute utilisation des composants

            originalX = pictureBoxMoving.Location.X;
            originalY = pictureBoxMoving.Location.Y;

            hpLabel = new Label() { Text = "HP : " + HP, Location = new Point(570, 28), AutoSize = true };
            scoreLabel = new Label() { Text = "Score : " + score, Location = new Point(10, 10), AutoSize = true };
            Controls.Add(hpLabel);
            Controls.Add(scoreLabel);

            // Maintenant on peut configurer les timers
            movementTimer.Interval = 50;
            movementTimer.Tick += MovementTimer_Tick;
            movementTimer.Start();

            speedTimer.Interval = 30000;
            speedTimer.Tick += SpeedTimer_Tick;
            speedTimer.Start();

            loopTimer.Interval = 10000;
            loopTimer.Tick += LoopTimer_Tick;

            reappearTimer.Interval = 5000;
            reappearTimer.Tick += ReappearTimer_Tick;

            knightTimer.Interval = knightFireRate;
            knightTimer.Tick += KnightTimer_Tick;

            spawnTimer.Interval = spawnInterval;
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();

            archerTimer.Interval = archerFireRate;
            archerTimer.Tick += ArcherTimer_Tick;

            difficultyTimer.Interval = 30000;
            difficultyTimer.Tick += DifficultyTimer_Tick;
            difficultyTimer.Start();
        }


        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnMultipleEnemies(3); // Nombre d'ennemis par vague
        }

        private void SpawnMultipleEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PictureBox newEnemy = new PictureBox();
                newEnemy.Image = Properties.Resources.goblin;
                newEnemy.Size = new Size(66, 65);
                newEnemy.SizeMode = PictureBoxSizeMode.StretchImage;
                newEnemy.Location = new Point(0, rand.Next(100, this.Height - 200));
                newEnemy.Visible = true;

                newEnemy.Click += (s, ev) =>
                {
                    PictureBox clickedEnemy = (PictureBox)s;
                    score += 15;
                    UpdateScoreLabel();
                    Controls.Remove(clickedEnemy);
                    enemyList.Remove(clickedEnemy);
                };

                enemyList.Add(newEnemy);
                this.Controls.Add(newEnemy);
                newEnemy.BringToFront();
            }
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxMoving.Visible)
            {
                pictureBoxMoving.Left += (int)enemy1Speed;
                if (pictureBoxMoving.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
                {
                    HP--;
                    UpdateHpLabel();
                    pictureBoxMoving.Location = new Point(originalX, originalY);
                }
                else if (pictureBoxMoving.Left > Width)
                {
                    pictureBoxMoving.Location = new Point(originalX, originalY);
                }
            }

            foreach (var enemy in enemyList.ToArray())
            {
                enemy.Left += (int)enemy2Speed;

                if (enemy.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
                {
                    HP--;
                    UpdateHpLabel();
                    Controls.Remove(enemy);
                    enemyList.Remove(enemy);
                }
                else if (enemy.Left > this.Width)
                {
                    Controls.Remove(enemy);
                    enemyList.Remove(enemy);
                }
            }

            if (HP <= 0)
            {
                movementTimer.Stop(); speedTimer.Stop(); spawnTimer.Stop();
                archerTimer.Stop(); loopTimer.Stop(); knightTimer.Stop(); difficultyTimer.Stop();
                MessageBox.Show("GAME OVER!");
                this.Close();
                MenuForm menu = new MenuForm();
                menu.Show();
            }
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

        private void LoopTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxMoving.Visible)
            {
                pictureBoxMoving.Visible = false;
                pictureBoxMoving.Location = new Point(originalX, originalY);
                score += 10;
            }

            foreach (var enemy in enemyList.ToArray())
            {
                Controls.Remove(enemy);
                enemyList.Remove(enemy);
                score += 10;
            }

            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void ReappearTimer_Tick(object sender, EventArgs e)
        {
            if (!pictureBoxMoving.Visible) pictureBoxMoving.Visible = true;
            reappearTimer.Stop();
        }

        private void PictureBoxMoving_Click(object sender, EventArgs e)
        {
            pictureBoxMoving.Visible = false;
            pictureBoxMoving.Location = new Point(originalX, originalY);
            score += 25;
            UpdateScoreLabel();
            reappearTimer.Start();
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

        private void DifficultyTimer_Tick(object sender, EventArgs e)
        {
            if (spawnInterval > 3000)
            {
                spawnInterval -= 1000;
                spawnTimer.Interval = spawnInterval;
            }
        }
    }
}
