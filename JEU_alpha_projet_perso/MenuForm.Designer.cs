namespace MonJeu
{
    partial class MenuForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button startButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.startButton.Location = new System.Drawing.Point(100, 100);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(200, 50);
            this.startButton.Text = "Démarrer le jeu";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // MenuForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.startButton);
            this.Name = "MenuForm";
            this.Text = "Menu Principal";
            this.ResumeLayout(false);
        }
    }
}
