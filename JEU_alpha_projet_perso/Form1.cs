using System;
using System.Drawing;
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

        private System.Windows.Forms.Timer spawnTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer killTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer archerTimer = new System.Windows.Forms.Timer();

        private System.Windows.Forms.Timer difficultyTimer = new System.Windows.Forms.Timer();


        private bool archerShootEnemy1 = true; // Pour alterner les tirs
        private int spawnInterval = 9000;
        private int archerFireRate = 2953;
        private int archerUpgradeCost = 2;

        private bool archerHired = false;
        private int archerLevel = 1;

        public Form1()
        {
            InitializeComponent();

            originalX = pictureBoxMoving.Location.X;
            originalY = pictureBoxMoving.Location.Y;

            // UI
            hpLabel = new Label() { Text = "HP : " + HP, Location = new Point(570, 28), AutoSize = true };
            scoreLabel = new Label() { Text = "Score : " + score, Location = new Point(10, 10), AutoSize = true };
            Controls.Add(hpLabel);
            Controls.Add(scoreLabel);

            // Timers
            spawnTimer.Interval = spawnInterval;
            spawnTimer.Tick += SpawnTimer_Tick;
            spawnTimer.Start();

            killTimer.Interval = 10000;
            killTimer.Tick += KillTimer_Tick;

            loopTimer.Interval = 10000;
            loopTimer.Tick += LoopTimer_Tick;

            archerTimer.Interval = archerFireRate;
            archerTimer.Tick += ArcherTimer_Tick;

            difficultyTimer.Interval = 30000;
            difficultyTimer.Tick += DifficultyTimer_Tick;
            difficultyTimer.Start();

            movementTimer.Start();
            speedTimer.Start();
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
            pictureBoxEnemy2.Location = new Point(originalX, originalY);
            killTimer.Start();
        }

        private void KillTimer_Tick(object sender, EventArgs e)
        {
            pictureBoxEnemy2.Visible = false;
            score += 10;
            UpdateScoreLabel();
            killTimer.Stop();
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxMoving.Visible) pictureBoxMoving.Left += (int)enemy1Speed;
            if (pictureBoxEnemy2.Visible) pictureBoxEnemy2.Left += (int)enemy2Speed;

            if (pictureBoxMoving.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
            {
                HP--; UpdateHpLabel();
                pictureBoxMoving.Location = new Point(originalX, originalY);
            }

            if (pictureBoxEnemy2.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
            {
                HP--; UpdateHpLabel();
                pictureBoxEnemy2.Location = new Point(originalX, originalY);
            }

            if (pictureBoxMoving.Left > Width) pictureBoxMoving.Location = new Point(originalX, originalY);
            if (pictureBoxEnemy2.Left > Width) pictureBoxEnemy2.Location = new Point(originalX, originalY);

            if (HP <= 0)
            {
                movementTimer.Stop();
                speedTimer.Stop();
                spawnTimer.Stop();
                killTimer.Stop();
                archerTimer.Stop();
                loopTimer.Stop();
                difficultyTimer.Stop();
                MessageBox.Show("GAME OVER!");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                $"Which unit?\nYes = Knight (Unavailable)\nNo = Archer (300 to hire / {archerUpgradeCost} to upgrade)",
                "Buy / Upgrade", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Knight not available.");
            }
            else
            {
                if (!archerHired)
                {
                    if (score >= 2)
                    {
                        score -= 2;
                        archerHired = true;
                        archerTimer.Start();
                        MessageBox.Show("Archer hired!");
                    }
                    else
                    {
                        MessageBox.Show("Not enough score! (300 needed)");
                        return;
                    }
                }
                else
                {
                    if (score >= archerUpgradeCost)
                    {
                        score -= archerUpgradeCost;
                        archerLevel++;
                        archerFireRate = Math.Max(500, archerFireRate - 300);
                        archerTimer.Interval = archerFireRate;
                        archerUpgradeCost += 2;
                        
                    }
                    else
                    {
                        MessageBox.Show($"Not enough score! Upgrade costs {archerUpgradeCost}.");
                        return;
                    }
                }
            }

            UpdateScoreLabel();

            if (!loopTimer.Enabled)
            {
                loopTimer.Start();
                pictureBox4.Image = Properties.Resources.okayokayokay;
            }
        }

        private void LoopTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxMoving.Visible)
            {
                pictureBoxMoving.Visible = false;
                pictureBoxMoving.Location = new Point(originalX, originalY);
                score += 5;
            }

            if (pictureBoxEnemy2.Visible)
            {
                pictureBoxEnemy2.Visible = false;
                pictureBoxEnemy2.Location = new Point(originalX, originalY);
                score += 10;
            }

            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void ArcherTimer_Tick(object sender, EventArgs e)
        {
            if (archerShootEnemy1)
            {
                if (pictureBoxMoving.Visible)
                {
                    pictureBoxMoving.Visible = false;
                    pictureBoxMoving.Location = new Point(originalX, originalY);
                    score += 10;
                    UpdateScoreLabel();
                }
            }
            else
            {
                if (pictureBoxEnemy2.Visible)
                {
                    pictureBoxEnemy2.Visible = false;
                    pictureBoxEnemy2.Location = new Point(originalX, originalY);
                    score += 10;
                    UpdateScoreLabel();
                }
            }

            // On alterne pour la prochaine fois
            archerShootEnemy1 = !archerShootEnemy1;
        }

        private void PictureBoxMoving_Click(object sender, EventArgs e)
        {
            pictureBoxMoving.Visible = false;
            pictureBoxMoving.Location = new Point(originalX, originalY);
            score += 10;
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        private void pictureBoxEnemy2_Click(object sender, EventArgs e)
        {
            pictureBoxEnemy2.Visible = false;
            pictureBoxEnemy2.Location = new Point(originalX, originalY);
            score += 10;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
