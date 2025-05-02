namespace MonJeu
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button startButton;
        private Button button1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            startButton = new Button();
            button1 = new Button();
            creditsButton = new Button();
            siteButton = new Button();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Font = new Font("Segoe UI", 14F);
            startButton.Location = new Point(96, 64);
            startButton.Name = "startButton";
            startButton.Size = new Size(200, 50);
            startButton.TabIndex = 0;
            startButton.Text = "Démarrer le jeu";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 14F);
            button1.Location = new Point(96, 120);
            button1.Name = "button1";
            button1.Size = new Size(200, 50);
            button1.TabIndex = 1;
            button1.Text = "Classement";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // creditsButton
            // 
            creditsButton.Location = new Point(96, 176);
            creditsButton.Name = "creditsButton";
            creditsButton.Size = new Size(200, 50);
            creditsButton.TabIndex = 2;
            creditsButton.Text = "Crédits";
            creditsButton.Click += CreditsButton_Click;
            // 
            // siteButton
            // 
            siteButton.Location = new Point(96, 232);
            siteButton.Name = "siteButton";
            siteButton.Size = new Size(200, 50);
            siteButton.TabIndex = 3;
            siteButton.Text = "Mon Portfolio";
            siteButton.Click += SiteButton_Click;
            // 
            // MenuForm
            // 
            ClientSize = new Size(400, 350);
            Controls.Add(button1);
            Controls.Add(startButton);
            Controls.Add(creditsButton);
            Controls.Add(siteButton);
            Name = "MenuForm";
            Text = "Menu Principal";
            ResumeLayout(false);
        }

        private Button creditsButton;
        private Button siteButton;
    }
}
