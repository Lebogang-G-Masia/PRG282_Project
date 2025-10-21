using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SuperHero
{
    public partial class UpdateSuperHeroForm : Form
    {
        public event EventHandler SuperheroUpdated;
        private string originalHeroID;
        public UpdateSuperHeroForm(string heroID, string name, int age, string superpower, int examScore)
        {
            InitializeComponent();
            // Store original ID
            originalHeroID = heroID;

            // Pre-fill the form with existing data
            txtHeroID.Text = heroID;
            txtName.Text = name;
            numAge.Text = age.ToString();
            txtSuperpower.Text = superpower;
            numExamScore.Text = examScore.ToString();

            // Make HeroID readonly since it's the unique identifier
            txtHeroID.ReadOnly = true;
            txtHeroID.BackColor = Color.LightGray;
   
        }



        private void UpdateSuperHeroForm_Load(object sender, EventArgs e)
        {
            UpdateRankDisplay();
        }

        private void UpdateRankDisplay()
        {
            int score = int.Parse(numExamScore.Text);
            string rank = "";
            string threatLevel = "";

            if (score >= 81 && score <= 100)
            {
                rank = "S-Rank";
                threatLevel = "Finals Week";
            }
            else if (score >= 61 && score <= 80)
            {
                rank = "A-Rank";
                threatLevel = "Midterm Madness";
            }
            else if (score >= 41 && score <= 60)
            {
                rank = "B-Rank";
                threatLevel = "Group Project Gone Wrong";
            }
            else if (score >= 0 && score <= 40)
            {
                rank = "C-Rank";
                threatLevel = "Pop Quiz";
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Please enter a hero name.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSuperpower.Text))
                {
                    MessageBox.Show("Please enter a superpower.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSuperpower.Focus();
                    return;
                }

                // Load all heroes
                List<SuperHero> heroes = FileManager.LoadSuperheroes();

                // Find the hero to update
                SuperHero heroToUpdate = heroes.FirstOrDefault(h => h.HeroID == originalHeroID);

                if (heroToUpdate != null)
                {
                    // Remove old hero
                    heroes.Remove(heroToUpdate);

                    // Create updated hero
                    SuperHero updatedHero = new SuperHero(
                        txtHeroID.Text.Trim(),
                        txtName.Text.Trim(),
                        int.Parse(numAge.Text),
                        txtSuperpower.Text.Trim(),
                        int.Parse(numExamScore.Text)
                    );

                    // Add updated hero
                    heroes.Add(updatedHero);

                    // Save all heroes
                    FileManager.SaveSuperheroes(heroes);

                    MessageBox.Show("Hero updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trigger the event to refresh main form
                    SuperheroUpdated?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hero not found in database.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating superhero: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
