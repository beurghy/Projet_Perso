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
        private System.Windows.Forms.PictureBox pictureBox4;

        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer reappearTimer;
        private System.Windows.Forms.Timer speedTimer;
        private System.Windows.Forms.Timer loopTimer;
        private System.Windows.Forms.Timer knightTimer;
        private System.Windows.Forms.Timer spawnTimer;
        private System.Windows.Forms.Timer archerTimer;
        private System.Windows.Forms.Timer difficultyTimer;
        private System.Windows.Forms.Label label1;

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
            spawnTimer = new System.Windows.Forms.Timer(components);
            archerTimer = new System.Windows.Forms.Timer(components);
            difficultyTimer = new System.Windows.Forms.Timer(components);
            movementTimer = new System.Windows.Forms.Timer(components);
            reappearTimer = new System.Windows.Forms.Timer(components);
            speedTimer = new System.Windows.Forms.Timer(components);
            loopTimer = new System.Windows.Forms.Timer(components);
            knightTimer = new System.Windows.Forms.Timer(components);
            pictureBoxTarget = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBoxMoving = new System.Windows.Forms.PictureBox();
            pictureBoxArcher = new System.Windows.Forms.PictureBox();
            pictureBoxKnight = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(pictureBoxTarget)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxMoving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxArcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxKnight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox4)).BeginInit();

            // pictureBox4
            pictureBox4 = new System.Windows.Forms.PictureBox();
            pictureBox4.BackColor = Color.Transparent; // ou Color.Red pour tester visuellement
            pictureBox4.Location = new System.Drawing.Point(12, 400);
            pictureBox4.Size = new System.Drawing.Size(100, 300);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.TabStop = false;
            this.Controls.Add(pictureBox4);


            // (tu peux configurer ici les autres PictureBox comme dans ton ancien code...)

            this.Controls.Add(pictureBox4);

            ((System.ComponentModel.ISupportInitialize)(pictureBoxTarget)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxMoving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxArcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxKnight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox4)).EndInit();
        }
    }
}
