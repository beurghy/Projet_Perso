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
        private double enemy1Speed = 10;
        private double enemy2Speed = 7;
        private double speedIncrement = 0.1; // Speed increase per second

        public Form1()
        {
            InitializeComponent();

            originalX = pictureBoxMoving.Location.X;
            originalY = pictureBoxMoving.Location.Y;

            // Label HP
            hpLabel = new Label();
            hpLabel.Text = "HP : " + HP;
            hpLabel.Location = new Point(570, 28);
            hpLabel.AutoSize = true;
            this.Controls.Add(hpLabel);

            // Label Score
            scoreLabel = new Label();
            scoreLabel.Text = "Score : " + score;
            scoreLabel.Location = new Point(10, 10);   // ✅ En haut à gauche
            scoreLabel.AutoSize = true;
            this.Controls.Add(scoreLabel);

            // Start timers
            movementTimer.Start();
            speedTimer.Start();
        }

        // ✅ Timer that runs every 50ms for movement
        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            if (pictureBoxMoving.Visible)
                pictureBoxMoving.Left += (int)enemy1Speed;

            if (pictureBoxEnemy2.Visible)
                pictureBoxEnemy2.Left += (int)enemy2Speed;

            if (pictureBoxMoving.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
            {
                HP--;
                UpdateHpLabel();
                pictureBoxMoving.Location = new Point(originalX, originalY);
            }

            if (pictureBoxEnemy2.Bounds.IntersectsWith(pictureBoxTarget.Bounds))
            {
                HP--;
                UpdateHpLabel();
                pictureBoxEnemy2.Location = new Point(originalX, originalY);
            }

            if (pictureBoxMoving.Left > this.Width)
                pictureBoxMoving.Location = new Point(originalX, originalY);

            if (pictureBoxEnemy2.Left > this.Width)
                pictureBoxEnemy2.Location = new Point(originalX, originalY);

            if (HP <= 0)
            {
                movementTimer.Stop();
                speedTimer.Stop();
                MessageBox.Show("GAME OVER !");
            }
        }

        // ✅ Click on Moving Enemy (earn points)
        private void PictureBoxMoving_Click(object sender, EventArgs e)
        {
            pictureBoxMoving.Visible = false;
            pictureBoxMoving.Location = new Point(originalX, originalY);
            score += 10;
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        // ✅ Click on Enemy2 (earn points)
        private void pictureBoxEnemy2_Click(object sender, EventArgs e)
        {
            pictureBoxEnemy2.Visible = false;
            pictureBoxEnemy2.Location = new Point(originalX, originalY);
            score += 10;
            UpdateScoreLabel();
            reappearTimer.Start();
        }

        // ✅ Respawn after click
        private void ReappearTimer_Tick(object sender, EventArgs e)
        {
            if (!pictureBoxMoving.Visible)
                pictureBoxMoving.Visible = true;

            if (!pictureBoxEnemy2.Visible)
                pictureBoxEnemy2.Visible = true;

            reappearTimer.Stop();
        }

        private void UpdateHpLabel()
        {
            hpLabel.Text = "HP : " + HP;

            // Calcul de la largeur de la barre de vie (200 - 2 * (100 - HP))
            int newWidth = 200 - (2 * (100 - HP));

            // Évite que la taille devienne négative
            if (newWidth < 0) newWidth = 0;

            pictureBox3.Size = new Size(newWidth, pictureBox3.Height);
        }


        private void UpdateScoreLabel()
        {
            scoreLabel.Text = "Score : " + score;
        }

        // ✅ Speed increase every second
        private void SpeedTimer_Tick(object sender, EventArgs e)
        {
            enemy1Speed += speedIncrement;
            enemy2Speed += speedIncrement;
        }

    }
}
