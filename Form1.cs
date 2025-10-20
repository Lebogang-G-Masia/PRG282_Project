using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperHero
{
    public partial class Form1 : Form
    {
        private void LoadSuperheroes()
        {
            try
            {
                // Clear existing data
                dgvSuperheroes.DataSource = null;
                dgvSuperheroes.Rows.Clear();
                dgvSuperheroes.Columns.Clear();

                // Load heroes from file
                List<SuperHero> heroes = FileManager.LoadSuperheroes();

                if (heroes.Count == 0)
                {
                    lblCount.Text = "Total Heroes: 0";
                    MessageBox.Show("No superhero records found.\nClick 'Add New Hero' to get started!",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                
                dgvSuperheroes.Columns.Add("HeroID", "Hero ID");
                dgvSuperheroes.Columns.Add("Name", "Name");
                dgvSuperheroes.Columns.Add("Age", "Age");
                dgvSuperheroes.Columns.Add("Superpower", "Superpower");
                dgvSuperheroes.Columns.Add("ExamScore", "Exam Score");
                dgvSuperheroes.Columns.Add("Rank", "Rank");
                dgvSuperheroes.Columns.Add("ThreatLevel", "Threat Level");

               
                foreach (SuperHero hero in heroes)
                {
                    int rowIndex = dgvSuperheroes.Rows.Add(
                        hero.HeroID,
                        hero.Name,
                        hero.Age,
                        hero.Superpower,
                        hero.ExamScore,
                        hero.Rank,
                        hero.ThreatLevel
                    );

                    
                    DataGridViewRow row = dgvSuperheroes.Rows[rowIndex];
                    switch (hero.Rank)
                    {
                        case "S-Rank":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 215, 0); // Au real ones know
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                            break;
                        case "A-Rank":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 192); // Ag only if u tapped in
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            break;
                        case "B-Rank":
                            row.DefaultCellStyle.BackColor = Color.FromArgb(205, 127, 50); // CuSn
                            row.DefaultCellStyle.ForeColor = Color.White;
                            break;
                    }
                }

                // Update count label
                lblCount.Text = $"Total Heroes: {heroes.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading superheroes: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Form1()
        {
            InitializeComponent();
            LoadSuperheroes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSuperheroesForm addForm = new AddSuperheroesForm();

            // When a hero is added, refresh the grid
            addForm.SuperheroAdded += (s, args) =>
            {
                LoadSuperheroes();
            };

            addForm.ShowDialog();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadSuperheroes();
            MessageBox.Show("Database refreshed successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
