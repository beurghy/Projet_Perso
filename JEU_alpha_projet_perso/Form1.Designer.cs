namespace JEU_alpha_projet_perso
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBoxTarget;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBoxMoving;
        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer reappearTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBoxEnemy2;
        private System.Windows.Forms.Timer speedTimer;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Timer loopTimer;
        

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
            movementTimer = new System.Windows.Forms.Timer(components);
            reappearTimer = new System.Windows.Forms.Timer(components);
            pictureBoxTarget = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBoxMoving = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBoxEnemy2 = new PictureBox();
            speedTimer = new System.Windows.Forms.Timer(components);
            pictureBox4 = new PictureBox();
            loopTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBoxTarget).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMoving).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
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
            pictureBox2.Location = new Point(-1, 439);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1079, 320);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBoxMoving
            // 
            pictureBoxMoving.Image = Properties.Resources.pourquoi_removebg_preview;
            pictureBoxMoving.Location = new Point(12, 513);
            pictureBoxMoving.Name = "pictureBoxMoving";
            pictureBoxMoving.Size = new Size(66, 64);
            pictureBoxMoving.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxMoving.TabIndex = 5;
            pictureBoxMoving.TabStop = false;
            pictureBoxMoving.Click += PictureBoxMoving_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(324, 138);
            label1.MaximumSize = new Size(100, 100);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 100;
            label1.Text = "health:";
            label1.Click += label1_Click;
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
            // pictureBoxEnemy2
            // 
            pictureBoxEnemy2.Image = Properties.Resources.goblin;
            pictureBoxEnemy2.Location = new Point(12, 614);
            pictureBoxEnemy2.Name = "pictureBoxEnemy2";
            pictureBoxEnemy2.Size = new Size(66, 65);
            pictureBoxEnemy2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxEnemy2.TabIndex = 1;
            pictureBoxEnemy2.TabStop = false;
            pictureBoxEnemy2.Click += pictureBoxEnemy2_Click;
            // 
            // speedTimer
            // 
            speedTimer.Interval = 1000;
            speedTimer.Tick += SpeedTimer_Tick;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.add;
            pictureBox4.Location = new Point(185, 754);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(161, 154);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // loopTimer
            // 
            loopTimer.Interval = 10000;
            loopTimer.Tick += LoopTimer_Tick;
            // 
            // Form1
            // 
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(1904, 1041);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBoxEnemy2);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnemy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
