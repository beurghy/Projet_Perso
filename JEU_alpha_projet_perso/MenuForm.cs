using JEU_alpha_projet_perso;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonJeu
{
    public partial class MenuForm : Form
    {
        // Client HTTP pour communiquer avec Firebase
        private static readonly HttpClient client = new HttpClient();

        // URL de la base de données Firebase contenant les scores
        private string firebaseUrl = "https://jeu-de-tower-defense-default-rtdb.europe-west1.firebasedatabase.app/scores.json";

        // Éléments visuels
        private Color backgroundColor = Color.FromArgb(27, 38, 59);
        private Color buttonColor = Color.FromArgb(15, 76, 117);
        private Color buttonHoverColor = Color.FromArgb(50, 130, 184);
        private Color textColor = Color.FromArgb(187, 225, 250);
        private Font titleFont;
        private Font buttonFont;
        private Image backgroundImage;
        private Image gameLogo;

        // Pour l'animation
        private System.Windows.Forms.Timer animationTimer;
        private List<Particle> particles = new List<Particle>();
        private Random random = new Random();

        public MenuForm()
        {
            // Initialise les composants du formulaire
            InitializeComponent();
            InitializeCustomDesign();
        }

        private void InitializeCustomDesign()
        {
            // Configuration de base du formulaire
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = backgroundColor;
            this.DoubleBuffered = true;

            // Chargement des polices et images
            try
            {
                titleFont = new Font("Segoe UI", 32, FontStyle.Bold);
                buttonFont = new Font("Segoe UI", 14, FontStyle.Bold);

                // Essayez de charger l'image de fond - utilisez une image par défaut si échoue
                try
                {
                    backgroundImage = Image.FromFile("Resources/background.png");
                }
                catch
                {
                    // Créez un dégradé si l'image n'est pas disponible
                    backgroundImage = CreateGradientImage(this.Width, this.Height);
                }

                // Essayez de charger le logo - utilisez un texte par défaut si échoue
                try
                {
                    gameLogo = Image.FromFile("Resources/logo.png");
                }
                catch
                {
                    gameLogo = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement des ressources: " + ex.Message);
            }

            // Création des boutons stylisés
            CreateCustomButtons();

            // Ajout d'un bouton pour quitter
            AddExitButton();

            // Configuration de l'animation
            ConfigureAnimation();

            // Gestionnaire d'événements pour redessiner
            this.Paint += MenuForm_Paint;
            this.Resize += MenuForm_Resize;
        }

        private void MenuForm_Resize(object sender, EventArgs e)
        {
            // Recréer le fond dégradé si l'image de fond n'est pas disponible
            if (backgroundImage is null || backgroundImage.Width < 10)
            {
                backgroundImage = CreateGradientImage(this.Width, this.Height);
            }

            // Repositionner les boutons
            RepositionControls();
        }

        private void RepositionControls()
        {
            // Calculer le centre pour bien positionner les boutons
            int centerX = this.ClientSize.Width / 2;
            int startY = this.ClientSize.Height / 2 - 100;

            // Positionner chaque bouton
            foreach (Control control in this.Controls)
            {
                if (control is Button btn)
                {
                    if (btn.Name == "exitButton")
                    {
                        btn.Location = new Point(this.ClientSize.Width - btn.Width - 20, 20);
                        continue;
                    }

                    btn.Location = new Point(centerX - btn.Width / 2, startY);
                    startY += btn.Height + 20;
                }
            }
        }

        private void ConfigureAnimation()
        {
            // Configurer le timer pour l'animation
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 40;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Animation des particules
            UpdateParticles();

            // Générer de nouvelles particules aléatoirement
            if (random.Next(100) < 15)
            {
                CreateNewParticle();
            }

            // Demander un rafraîchissement de l'affichage
            this.Invalidate();
        }

        private void UpdateParticles()
        {
            // Mettre à jour la position et opacité des particules
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update();
                if (particles[i].Alpha <= 0 ||
                    particles[i].Position.X < -20 ||
                    particles[i].Position.X > this.Width + 20 ||
                    particles[i].Position.Y < -20 ||
                    particles[i].Position.Y > this.Height + 20)
                {
                    particles.RemoveAt(i);
                }
            }
        }

        private void CreateNewParticle()
        {
            // Créer une nouvelle particule à une position aléatoire
            int x = random.Next(this.Width);
            int y = random.Next(this.Height);
            Color color = Color.FromArgb(random.Next(100, 255), textColor);

            particles.Add(new Particle(
                new PointF(x, y),
                new PointF((float)(random.NextDouble() * 2 - 1), (float)(random.NextDouble() * 2 - 1)),
                random.Next(5, 20),
                color
            ));
        }

        private void MenuForm_Paint(object sender, PaintEventArgs e)
        {
            // Rendre le fond
            if (backgroundImage != null)
            {
                e.Graphics.DrawImage(backgroundImage, 0, 0, this.Width, this.Height);
            }

            // Dessiner le titre/logo
            if (gameLogo != null)
            {
                int logoWidth = Math.Min(400, this.Width - 100);
                int ratio = logoWidth * 100 / gameLogo.Width;
                int logoHeight = gameLogo.Height * ratio / 100;

                e.Graphics.DrawImage(gameLogo,
                    (this.Width - logoWidth) / 2,
                    100,
                    logoWidth, logoHeight);
            }
            else
            {
                // Dessiner le titre s'il n'y a pas de logo
                string gameTitle = "BEURGHY";
                SizeF titleSize = e.Graphics.MeasureString(gameTitle, titleFont);

                // Créer un effet d'ombre pour le titre
                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0)))
                {
                    e.Graphics.DrawString(gameTitle, titleFont, shadowBrush,
                        (this.Width - titleSize.Width) / 2 + 3,
                        120 + 3);
                }

                using (Brush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(gameTitle, titleFont, textBrush,
                        (this.Width - titleSize.Width) / 2,
                        120);
                }

                // Sous-titre
                string subTitle = "Tower Defense";
                Font subTitleFont = new Font(titleFont.FontFamily, titleFont.Size / 2);
                SizeF subTitleSize = e.Graphics.MeasureString(subTitle, subTitleFont);

                using (Brush textBrush = new SolidBrush(Color.FromArgb(200, textColor)))
                {
                    e.Graphics.DrawString(subTitle, subTitleFont, textBrush,
                        (this.Width - subTitleSize.Width) / 2,
                        120 + titleSize.Height);
                }
            }

            // Dessiner les particules
            foreach (var particle in particles)
            {
                particle.Draw(e.Graphics);
            }
        }

        private Image CreateGradientImage(int width, int height)
        {
            // Créer une image avec un dégradé pour le fond
            Bitmap gradient = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(gradient))
            {
                Rectangle rect = new Rectangle(0, 0, width, height);
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(15, 34, 58),
                    Color.FromArgb(32, 58, 96),
                    LinearGradientMode.ForwardDiagonal))
                {
                    g.FillRectangle(brush, rect);

                    // Ajouter des étoiles/points pour une ambiance spatiale
                    using (Brush starBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 255)))
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            int x = random.Next(width);
                            int y = random.Next(height);
                            int size = random.Next(1, 3);
                            g.FillEllipse(starBrush, x, y, size, size);
                        }
                    }
                }
            }
            return gradient;
        }

        private void CreateCustomButtons()
        {
            // Définir les propriétés communes des boutons
            int buttonWidth = 250;
            int buttonHeight = 50;

            // Créer le bouton de démarrage
            Button startButton = new Button
            {
                Name = "startButton",
                Text = "JOUER",
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = buttonFont,
                Cursor = Cursors.Hand,
                TabStop = false
            };

            // Créer le bouton des scores
            Button scoresButton = new Button
            {
                Name = "button1",
                Text = "CLASSEMENT",
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = buttonFont,
                Cursor = Cursors.Hand,
                TabStop = false
            };

            // Créer le bouton des crédits
            Button creditsButton = new Button
            {
                Name = "CreditsButton",
                Text = "CRÉDITS",
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = buttonFont,
                Cursor = Cursors.Hand,
                TabStop = false
            };

            // Créer le bouton du site
            Button siteButton = new Button
            {
                Name = "SiteButton",
                Text = "SITE WEB",
                Size = new Size(buttonWidth, buttonHeight),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = buttonFont,
                Cursor = Cursors.Hand,
                TabStop = false
            };

            // Ajouter les gestionnaires d'événements
            startButton.Click += startButton_Click;
            scoresButton.Click += button1_Click;
            creditsButton.Click += CreditsButton_Click;
            siteButton.Click += SiteButton_Click;

            // Supprimer les bordures plates
            startButton.FlatAppearance.BorderSize = 0;
            scoresButton.FlatAppearance.BorderSize = 0;
            creditsButton.FlatAppearance.BorderSize = 0;
            siteButton.FlatAppearance.BorderSize = 0;

            // Ajouter l'effet de survol
            startButton.MouseEnter += Button_MouseEnter;
            startButton.MouseLeave += Button_MouseLeave;
            scoresButton.MouseEnter += Button_MouseEnter;
            scoresButton.MouseLeave += Button_MouseLeave;
            creditsButton.MouseEnter += Button_MouseEnter;
            creditsButton.MouseLeave += Button_MouseLeave;
            siteButton.MouseEnter += Button_MouseEnter;
            siteButton.MouseLeave += Button_MouseLeave;

            // Ajouter les boutons au formulaire
            this.Controls.Add(startButton);
            this.Controls.Add(scoresButton);
            this.Controls.Add(creditsButton);
            this.Controls.Add(siteButton);

            // Positionner les boutons
            RepositionControls();
        }

        private void AddExitButton()
        {
            // Créer un bouton pour quitter
            Button exitButton = new Button
            {
                Name = "exitButton",
                Text = "X",
                Size = new Size(40, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(150, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            exitButton.FlatAppearance.BorderSize = 0;
            exitButton.Click += (s, e) => Application.Exit();
            exitButton.MouseEnter += (s, e) => exitButton.BackColor = Color.FromArgb(200, 50, 50);
            exitButton.MouseLeave += (s, e) => exitButton.BackColor = Color.FromArgb(150, 30, 30);

            this.Controls.Add(exitButton);
            exitButton.Location = new Point(this.ClientSize.Width - exitButton.Width - 20, 20);
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Effet visuel au survol
            if (sender is Button button)
            {
                button.BackColor = buttonHoverColor;
                button.ForeColor = Color.White;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            // Retour à l'état normal
            if (sender is Button button)
            {
                button.BackColor = buttonColor;
                button.ForeColor = textColor;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Lance le jeu et cache le menu
            Form1 jeu = new Form1();
            jeu.Show();
            this.Hide();
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            // Créer une boîte de dialogue personnalisée pour les crédits
            Form creditsForm = new Form
            {
                Text = "Crédits",
                Size = new Size(400, 300),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = backgroundColor
            };

            Label creditsLabel = new Label
            {
                Text = "JEU DÉVELOPPÉ PAR\nBEURGHY & MAXIME FROSSARD\n\n" +
                       "MUSIQUE : PIXABAY\n\n" +
                       "GRAPHISMES : BEURGHY & MAXIME FROSSARD\n\n" +
                       "MERCI D'AVOIR JOUÉ !",
                ForeColor = textColor,
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            Button closeButton = new Button
            {
                Text = "FERMER",
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                Cursor = Cursors.Hand
            };

            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, ev) => creditsForm.Close();

            creditsForm.Controls.Add(creditsLabel);
            creditsForm.Controls.Add(closeButton);

            creditsForm.ShowDialog(this);
        }

        private void SiteButton_Click(object sender, EventArgs e)
        {
            // Ouvre le site web du jeu dans le navigateur
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "https://jeu-de-tower-defense.web.app",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ouvrir le lien : " + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Afficher un indicateur de chargement
                using (var loadingForm = new LoadingForm())
                {
                    loadingForm.Show(this);

                    // Récupère les scores depuis Firebase
                    var response = await client.GetAsync(firebaseUrl);

                    loadingForm.Close();

                    if (!response.IsSuccessStatusCode)
                    {
                        ShowCustomMessageBox("Erreur lors de la récupération des scores Firebase", "Erreur", MessageBoxIcon.Error);
                        return;
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(json) || json == "null")
                    {
                        ShowCustomMessageBox("Aucun score enregistré pour l'instant", "Classement", MessageBoxIcon.Information);
                        return;
                    }

                    // Désérialise les données JSON en dictionnaire
                    var scoresDict = JsonSerializer.Deserialize<Dictionary<string, ScoreEntry>>(json);

                    if (scoresDict == null || scoresDict.Count == 0)
                    {
                        ShowCustomMessageBox("Aucun score valide trouvé", "Classement", MessageBoxIcon.Information);
                        return;
                    }

                    // Trie les scores du plus grand au plus petit et garde les 10 meilleurs
                    var topScores = scoresDict
                        .Values
                        .OrderByDescending(s => s.score)
                        .Take(10)
                        .ToList();

                    // Afficher les scores dans une forme personnalisée
                    ShowScoresForm(topScores);
                }
            }
            catch (Exception ex)
            {
                ShowCustomMessageBox("Erreur : " + ex.Message, "Erreur", MessageBoxIcon.Error);
            }
        }

        private void ShowScoresForm(List<ScoreEntry> scores)
        {
            // Créer une forme personnalisée pour les scores
            Form scoresForm = new Form
            {
                Text = "Meilleurs Scores",
                Size = new Size(500, 500),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = backgroundColor
            };

            Label titleLabel = new Label
            {
                Text = "CLASSEMENT DES MEILLEURS SCORES",
                ForeColor = textColor,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            Panel scoresPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = backgroundColor
            };

            // Créer un tableau pour les scores
            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 4,
                RowCount = scores.Count + 1,
                Dock = DockStyle.Top,
                AutoSize = true,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                BackColor = Color.FromArgb(20, 50, 80)
            };

            // Ajouter les en-têtes
            table.Controls.Add(CreateHeaderLabel("RANG"), 0, 0);
            table.Controls.Add(CreateHeaderLabel("JOUEUR"), 1, 0);
            table.Controls.Add(CreateHeaderLabel("SCORE"), 2, 0);
            table.Controls.Add(CreateHeaderLabel("TEMPS"), 3, 0);

            // Ajouter les données
            for (int i = 0; i < scores.Count; i++)
            {
                table.Controls.Add(CreateScoreLabel("#" + (i + 1), i == 0), 0, i + 1);
                table.Controls.Add(CreateScoreLabel(scores[i].pseudo, i == 0), 1, i + 1);
                table.Controls.Add(CreateScoreLabel(scores[i].score + " pts", i == 0), 2, i + 1);
                table.Controls.Add(CreateScoreLabel(scores[i].temps, i == 0), 3, i + 1);
            }

            // Définir la taille des colonnes
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            scoresPanel.Controls.Add(table);

            Button closeButton = new Button
            {
                Text = "FERMER",
                Size = new Size(150, 40),
                FlatStyle = FlatStyle.Flat,
                BackColor = buttonColor,
                ForeColor = textColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Dock = DockStyle.Bottom,
                Cursor = Cursors.Hand
            };

            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (s, e) => scoresForm.Close();

            scoresForm.Controls.Add(titleLabel);
            scoresForm.Controls.Add(scoresPanel);
            scoresForm.Controls.Add(closeButton);

            scoresForm.ShowDialog(this);
        }

        private Label CreateHeaderLabel(string text)
        {
            return new Label
            {
                Text = text,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(30, 70, 110),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
        }

        private Label CreateScoreLabel(string text, bool isFirst)
        {
            return new Label
            {
                Text = text,
                ForeColor = isFirst ? Color.Gold : textColor,
                BackColor = isFirst ? Color.FromArgb(40, 60, 90) : Color.Transparent,
                Font = new Font("Segoe UI", isFirst ? 10 : 9, isFirst ? FontStyle.Bold : FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };
        }

        private void ShowCustomMessageBox(string message, string title, MessageBoxIcon icon)
        {
            // Version personnalisée de MessageBox plus adaptée au style
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }
    }

    // Structure de données pour stocker un score
    public class ScoreEntry
    {
        public string pseudo { get; set; } // Nom du joueur
        public int score { get; set; } // Score obtenu
        public string temps { get; set; } // Temps de survie
        public string date { get; set; } // Date d'enregistrement
    }

    // Classe pour l'animation des particules
    public class Particle
    {
        public PointF Position { get; set; }
        public PointF Velocity { get; set; }
        public float Size { get; set; }
        public int Alpha { get; set; }
        public Color Color { get; set; }

        public Particle(PointF position, PointF velocity, float size, Color color)
        {
            Position = position;
            Velocity = velocity;
            Size = size;
            Alpha = color.A;
            Color = color;
        }

        public void Update()
        {
            // Mettre à jour la position
            Position = new PointF(
                Position.X + Velocity.X,
                Position.Y + Velocity.Y
            );

            // Réduire l'opacité progressivement
            Alpha -= 1;
            if (Alpha < 0) Alpha = 0;
        }

        public void Draw(Graphics g)
        {
            if (Alpha <= 0) return;

            using (Brush brush = new SolidBrush(Color.FromArgb(Alpha, Color)))
            {
                g.FillEllipse(brush, Position.X - Size / 2, Position.Y - Size / 2, Size, Size);
            }
        }
    }

    // Formulaire pour l'indicateur de chargement
    public class LoadingForm : Form
    {
        private System.Windows.Forms.Timer animationTimer;
        private int angle = 0;

        public LoadingForm()
        {
            this.Size = new Size(200, 100);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(27, 38, 59);
            this.ShowInTaskbar = false;

            Label loadingLabel = new Label
            {
                Text = "Chargement...",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(loadingLabel);

            animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 40
            };

            animationTimer.Tick += (s, e) => {
                angle = (angle + 10) % 360;
                this.Invalidate();
            };

            this.Paint += LoadingForm_Paint;

            animationTimer.Start();
        }

        private void LoadingForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(187, 225, 250), 3))
            {
                Rectangle rect = new Rectangle(this.Width - 50, 10, 30, 30);
                e.Graphics.DrawArc(pen, rect, angle, 270);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            animationTimer.Stop();
            base.OnFormClosing(e);
        }
    }
}