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

namespace SuperHero
{
    public partial class AddSuperheroesForm : Form
    {
        public event EventHandler SuperheroAdded;

        // Validate all input fields
        private bool ValidateInput()
        {
            // Check Hero ID
            if (string.IsNullOrWhiteSpace(txtHeroID.Text))
            {
                MessageBox.Show("Hero ID cannot be empty!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHeroID.Focus();
                return false;
            }

            // Check Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // Check Age make sure its a number
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Age cannot be empty!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                MessageBox.Show("Age must be a valid number!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            if (age < 0 || age > 150)
            {
                MessageBox.Show("Age must be between 0 and 150!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }

            // Check Superpower
            if (string.IsNullOrWhiteSpace(txtSuperpower.Text))
            {
                MessageBox.Show("Superpower cannot be empty!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSuperpower.Focus();
                return false;
            }

            // Check Exam Score (0-100)
            if (string.IsNullOrWhiteSpace(txtExamScore.Text))
            {
                MessageBox.Show("Exam Score cannot be empty!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExamScore.Focus();
                return false;
            }

            if (!int.TryParse(txtExamScore.Text.Trim(), out int examScore))
            {
                MessageBox.Show("Exam Score must be a valid number!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExamScore.Focus();
                return false;
            }

            if (examScore < 0 || examScore > 100)
            {
                MessageBox.Show("Exam Score must be between 0 and 100!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtExamScore.Focus();
                return false;
            }

            return true; // All validations passed type shii!
        }

        // Clear all textboxes
        private void ClearForm()
        {
            txtHeroID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtSuperpower.Clear();
            txtExamScore.Clear();
            txtHeroID.Focus(); 
        }


        public AddSuperheroesForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Validate all inputs
                if (!ValidateInput())
                {
                    return;
                }

                // Get data from textboxes
                string heroID = txtHeroID.Text.Trim();
                string name = txtName.Text.Trim();
                int age = int.Parse(txtAge.Text.Trim());
                string superpower = txtSuperpower.Text.Trim();
                int examScore = int.Parse(txtExamScore.Text.Trim());

                // Check for duplicate Hero ID
                if (FileManager.HeroIDExists(heroID))
                {
                    MessageBox.Show("A superhero with this Hero ID already exists!\nPlease use a unique ID.",
                        "Duplicate Hero ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHeroID.Focus();
                    return;
                }

                // Create new superhero object
                SuperHero newHero = new SuperHero(heroID, name, age, superpower, examScore);

                // Save to file
                FileManager.SaveSuperhero(newHero);

                // Show Ws message
                MessageBox.Show($"Hero added successfully! 🎉\n\n" +
                    $"Hero: {name}\n" +
                    $"Rank: {newHero.Rank}\n" +
                    $"Threat Level: {newHero.ThreatLevel}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger event to refresh Form1
                SuperheroAdded?.Invoke(this, EventArgs.Empty);

                // Clear form for next hero input
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving superhero: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
