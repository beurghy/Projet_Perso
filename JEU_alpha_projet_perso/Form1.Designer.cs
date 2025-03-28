namespace JEU_alpha_projet_perso
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxTarget;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxMoving;
        private System.Windows.Forms.PictureBox pictureBoxArcher;
        private System.Windows.Forms.PictureBox pictureBoxKnight;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer reappearTimer;
        private System.Windows.Forms.Timer speedTimer;
        private System.Windows.Forms.Timer loopTimer;
        private System.Windows.Forms.Timer knightTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer spawnTimer;
        private System.Windows.Forms.Timer archerTimer;
        private System.Windows.Forms.Timer difficultyTimer;
        private System.Windows.Forms.PictureBox pictureBox4;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // pictureBox4 (zone de spawn)
            pictureBox4 = new System.Windows.Forms.PictureBox();
            pictureBox4.BackColor = Color.Transparent;
            pictureBox4.Location = new System.Drawing.Point(12, 400); // Ajuste comme tu veux
            pictureBox4.Size = new System.Drawing.Size(100, 300);     // Taille de la zone de spawn
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            this.Controls.Add(pictureBox4);

            components = new System.ComponentModel.Container();
            spawnTimer = new System.Windows.Forms.Timer(components);
            archerTimer = new System.Windows.Forms.Timer(components);
            difficultyTimer = new System.Windows.Forms.Timer(components);
            movementTimer = new System.Windows.Forms.Timer(components);
            reappearTimer = new System.Windows.Forms.Timer(components);
            pictureBoxTarget = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBoxMoving = new PictureBox();
            pictureBoxArcher = new PictureBox();
            pictureBoxKnight = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            speedTimer = new System.Windows.Forms.Timer(components);
            loopTimer = new System.Windows.Forms.Timer(components);
            knightTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            pictureBox4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMoving).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxArcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxKnight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // spawnTimer
            // 
            spawnTimer.Tick += SpawnTimer_Tick;
            // 
            // archerTimer
            // 
            archerTimer.Tick += ArcherTimer_Tick;
            // 
            // difficultyTimer
            // 
            difficultyTimer.Tick += DifficultyTimer_Tick;
            // 
            // movementTimer
            // 
            movementTimer.Interval = 50;
            movementTimer.Tick += MovementTimer_Tick;
            // 
            // reappearTimer
            // 
            reappearTimer.Interval = 2000;
            reappearTimer.Tick += ReappearTimer_Tick;
            // 
            // pictureBoxTarget
            // 
            pictureBoxTarget.Image = Properties.Resources.medieval_tower_transparent_removebg_preview1;
            pictureBoxTarget.Location = new Point(1034, 153);
            pictureBoxTarget.Name = "pictureBoxTarget";
            pictureBoxTarget.Size = new Size(858, 720);
            pictureBoxTarget.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTarget.TabIndex = 6;
            pictureBoxTarget.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.vsyyy;
            pictureBox2.Location = new Point(-1, 86);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1079, 787);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBoxMoving
            // 
            pictureBoxMoving.BackColor = Color.Ivory;
            pictureBoxMoving.Image = Properties.Resources.pourquoi_removebg_preview;
            pictureBoxMoving.Location = new Point(12, 513);
            pictureBoxMoving.Name = "pictureBoxMoving";
            pictureBoxMoving.Size = new Size(66, 64);
            pictureBoxMoving.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMoving.TabIndex = 5;
            pictureBoxMoving.TabStop = false;
            pictureBoxMoving.Click += PictureBoxMoving_Click;
            // 
            // pictureBoxArcher
            // 
            pictureBoxArcher.Image = Properties.Resources.add;
            pictureBoxArcher.Location = new Point(100, 754);
            pictureBoxArcher.Name = "pictureBoxArcher";
            pictureBoxArcher.Size = new Size(100, 100);
            pictureBoxArcher.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxArcher.TabIndex = 0;
            pictureBoxArcher.TabStop = false;
            pictureBoxArcher.Click += pictureBoxRecruit_Click;
            // 
            // pictureBoxKnight
            // 
            pictureBoxKnight.Image = Properties.Resources.add;
            pictureBoxKnight.Location = new Point(250, 754);
            pictureBoxKnight.Name = "pictureBoxKnight";
            pictureBoxKnight.Size = new Size(100, 100);
            pictureBoxKnight.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxKnight.TabIndex = 1;
            pictureBoxKnight.TabStop = false;
            pictureBoxKnight.Click += pictureBoxRecruit_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Location = new Point(520, 86);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 100);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Crimson;
            pictureBox3.Location = new Point(520, 86);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(800, 100);
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // speedTimer
            // 
            speedTimer.Interval = 1000;
            speedTimer.Tick += SpeedTimer_Tick;
            // 
            // loopTimer
            // 
            loopTimer.Interval = 10000;
            loopTimer.Tick += LoopTimer_Tick;
            // 
            // knightTimer
            // 
            knightTimer.Interval = 3000;
            knightTimer.Tick += KnightTimer_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 138);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 4;
            label1.Text = "health:";
            // 
            // pictureBox4
            // 
            pictureBox4.Location = new Point(-1, 246);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(27, 486);
            pictureBox4.TabIndex = 8;
            pictureBox4.TabStop = false;
            // 
            // Form1
            // 
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(1904, 1041);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBoxArcher);
            Controls.Add(pictureBoxKnight);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(pictureBoxMoving);
            Controls.Add(pictureBoxTarget);
            Controls.Add(pictureBox2);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMoving).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxArcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxKnight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private PictureBox pictureBox4;
    }
}
