using JEU_alpha_projet_perso;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonJeu
{
    public partial class MenuForm : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private string firebaseUrl = "https://jeu-de-tower-defense-default-rtdb.europe-west1.firebasedatabase.app/scores.json";

        public MenuForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Form1 jeu = new Form1();
            jeu.Show();
            this.Hide();
        }
        private void CreditsButton_Click(object sender, EventArgs e)
        {
            string credits = "Jeu développé par beurghy (Maxime Frossard)\n" +
                             "Musique : [PixaBay]\n" +
                             "Graphismes : beurghy, Maxime Frossard\n" +
                             "Merci d'avoir joué !";

            MessageBox.Show(credits, "Crédits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SiteButton_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("Impossible d'ouvrir le lien : " + ex.Message);
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await client.GetAsync(firebaseUrl);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Erreur lors de la récupération des scores Firebase.");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(json) || json == "null")
                {
                    MessageBox.Show("Aucun score enregistré pour l'instant.");
                    return;
                }

                

                var scoresDict = JsonSerializer.Deserialize<Dictionary<string, ScoreEntry>>(json);

                if (scoresDict == null || scoresDict.Count == 0)
                {
                    MessageBox.Show("Aucun score valide trouvé.");
                    return;
                }

                var topScores = scoresDict
                    .Values
                    .OrderByDescending(s => s.score)
                    .Take(10)
                    .ToList();

                string message = "🏆 Classement des meilleurs scores :\n\n";
                foreach (var s in topScores)
                {
                    if (!string.IsNullOrWhiteSpace(s.date))
                        message += $"{s.pseudo} - {s.score} pts - {s.temps} (le {s.date})\n";
                    else
                        message += $"{s.pseudo} - {s.score} pts - {s.temps}\n";
                }

                MessageBox.Show(message, "Classement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

    }

    public class ScoreEntry
    {
        public string pseudo { get; set; }
        public int score { get; set; }
        public string temps { get; set; }
        public string date { get; set; }
    }
}
