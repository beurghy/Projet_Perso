namespace JEU_alpha_projet_perso
{
    partial class Form1
    {
        // composants graphiques et timers du formulaire
        private System.ComponentModel.IContainer components = null;

        // tous les picturebox utilisés dans le jeu
        private System.Windows.Forms.PictureBox pictureBoxTarget;
        private System.Windows.Forms.PictureBox pictureBoxMoving;
        private System.Windows.Forms.PictureBox pictureBoxEnemy2;
        private System.Windows.Forms.PictureBox pictureBoxArcher;
        private System.Windows.Forms.PictureBox pictureBoxKnight;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;

        // timers pour gérer le mouvement et la logique du jeu
        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer reappearTimer;
        private System.Windows.Forms.Timer speedTimer;
        private System.Windows.Forms.Timer loopTimer;

        // étiquette de texte
        private System.Windows.Forms.Label label1;

        // libération des ressources à la fermeture
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
            components = new System.ComponentModel.Container();
            survivalLabel = new Label();
            movementTimer = new System.Windows.Forms.Timer(components);
            reappearTimer = new System.Windows.Forms.Timer(components);
            speedTimer = new System.Windows.Forms.Timer(components);
            loopTimer = new System.Windows.Forms.Timer(components);
            knightTimer = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            pictureBoxTarget = new PictureBox();
            pictureBoxMoving = new PictureBox();
            pictureBoxEnemy2 = new PictureBox();
            pictureBoxArcher = new PictureBox();
            pictureBoxKnight = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMoving).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxArcher).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxKnight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // survivalLabel
            // 
            survivalLabel.BackColor = Color.WhiteSmoke;
            survivalLabel.Location = new Point(0, -2);
            survivalLabel.Name = "survivalLabel";
            survivalLabel.Size = new Size(94, 33);
            survivalLabel.TabIndex = 0;
            
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
            label1.BackColor = Color.FloralWhite;
            label1.Location = new Point(46, 104);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 6;
            label1.Text = "health:";
            // 
            // pictureBoxTarget
            // 
            pictureBoxTarget.BackColor = Color.Transparent;
            pictureBoxTarget.Location = new Point(1372, 212);
            pictureBoxTarget.Name = "pictureBoxTarget";
            pictureBoxTarget.Size = new Size(520, 475);
            pictureBoxTarget.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxTarget.TabIndex = 8;
            pictureBoxTarget.TabStop = false;
            // 
            // pictureBoxMoving
            // 
            pictureBoxMoving.BackColor = Color.Transparent;
            pictureBoxMoving.Image = Properties.Resources.pourquoi_removebg_preview;
            pictureBoxMoving.Location = new Point(-14, 347);
            pictureBoxMoving.Name = "pictureBoxMoving";
            pictureBoxMoving.Size = new Size(150, 144);
            pictureBoxMoving.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMoving.TabIndex = 7;
            pictureBoxMoving.TabStop = false;
            pictureBoxMoving.Click += PictureBoxMoving_Click;
            // 
            // pictureBoxEnemy2
            // 
            pictureBoxEnemy2.BackColor = Color.Transparent;
            pictureBoxEnemy2.Image = Properties.Resources.goblin;
            pictureBoxEnemy2.Location = new Point(12, 486);
            pictureBoxEnemy2.Name = "pictureBoxEnemy2";
            pictureBoxEnemy2.Size = new Size(124, 120);
            pictureBoxEnemy2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxEnemy2.TabIndex = 3;
            pictureBoxEnemy2.TabStop = false;
            pictureBoxEnemy2.Click += pictureBoxEnemy2_Click;
            // 
            // pictureBoxArcher
            // 
            pictureBoxArcher.BackColor = Color.Transparent;
            pictureBoxArcher.Image = Properties.Resources.add;
            pictureBoxArcher.Location = new Point(100, 754);
            pictureBoxArcher.Name = "pictureBoxArcher";
            pictureBoxArcher.Size = new Size(100, 100);
            pictureBoxArcher.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxArcher.TabIndex = 1;
            pictureBoxArcher.TabStop = false;
            pictureBoxArcher.Click += pictureBoxRecruit_Click;
            // 
            // pictureBoxKnight
            // 
            pictureBoxKnight.BackColor = Color.Transparent;
            pictureBoxKnight.Image = Properties.Resources.add;
            pictureBoxKnight.Location = new Point(250, 754);
            pictureBoxKnight.Name = "pictureBoxKnight";
            pictureBoxKnight.Size = new Size(100, 100);
            pictureBoxKnight.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxKnight.TabIndex = 2;
            pictureBoxKnight.TabStop = false;
            pictureBoxKnight.Click += pictureBoxRecruit_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Location = new Point(100, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(832, 73);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Crimson;
            pictureBox3.Location = new Point(118, 21);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(800, 54);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // Form1
            // 
            BackgroundImage = Properties.Resources.backgroundni;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1904, 1041);
            Controls.Add(survivalLabel);
            Controls.Add(pictureBoxArcher);
            Controls.Add(pictureBoxKnight);
            Controls.Add(pictureBoxEnemy2);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(pictureBoxMoving);
            
            Controls.Add(pictureBoxTarget);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMoving).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxArcher).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxKnight).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
            
            SuspendLayout();

           

            

            
        }
    }
}

/*
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
    }
}
*/