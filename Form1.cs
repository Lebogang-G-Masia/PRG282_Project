using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dgvSuperheroes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a hero to delete.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected row
                DataGridViewRow selectedRow = dgvSuperheroes.SelectedRows[0];
                string heroID = selectedRow.Cells["HeroID"].Value.ToString();
                string heroName = selectedRow.Cells["Name"].Value.ToString();

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to delete {heroName} (ID: {heroID})?\n\nThis action cannot be undone.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Load all heroes
                    List<SuperHero> heroes = FileManager.LoadSuperheroes();

                    // Find and remove the hero
                    SuperHero heroToDelete = heroes.FirstOrDefault(h => h.HeroID == heroID);

                    if (heroToDelete != null)
                    {
                        heroes.Remove(heroToDelete);

                        // Save the updated list
                        FileManager.SaveSuperheroes(heroes);

                        // Refresh the grid
                        LoadSuperheroes();

                        MessageBox.Show($"{heroName} has been successfully deleted.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Hero not found in database.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting superhero: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dgvSuperheroes.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a hero to update.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the selected row data
                DataGridViewRow selectedRow = dgvSuperheroes.SelectedRows[0];
                string heroID = selectedRow.Cells["HeroID"].Value.ToString();
                string name = selectedRow.Cells["Name"].Value.ToString();
                int age = Convert.ToInt32(selectedRow.Cells["Age"].Value);
                string superpower = selectedRow.Cells["Superpower"].Value.ToString();
                int examScore = Convert.ToInt32(selectedRow.Cells["ExamScore"].Value);

                // Create and show the update form with pre-filled data
                UpdateSuperHeroForm updateForm = new UpdateSuperHeroForm(heroID, name, age, superpower, examScore);

                // When a hero is updated, refresh the grid
                updateForm.SuperheroUpdated += (s, args) =>
                {
                    LoadSuperheroes();
                };

                updateForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating superhero: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                // Load all heroes
                List<SuperHero> heroes = FileManager.LoadSuperheroes();

                if (heroes.Count == 0)
                {
                    MessageBox.Show("No superhero data available to generate a report.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Create report content
                StringBuilder report = new StringBuilder();

                // Header
                report.AppendLine("═══════════════════════════════════════════════════════════");
                report.AppendLine("           SUPERHERO ASSOCIATION DATABASE REPORT");
                report.AppendLine("═══════════════════════════════════════════════════════════");
                report.AppendLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                report.AppendLine($"Total Heroes Registered: {heroes.Count}");
                report.AppendLine("═══════════════════════════════════════════════════════════\n");

                // Statistics Section
                report.AppendLine("STATISTICS OVERVIEW");
                report.AppendLine("───────────────────────────────────────────────────────────");

                // Rank distribution
                var rankCounts = heroes.GroupBy(h => h.Rank)
                                      .OrderByDescending(g => g.Key)
                                      .ToDictionary(g => g.Key, g => g.Count());

                report.AppendLine("\nRank Distribution:");
                foreach (var rank in new[] { "S-Rank", "A-Rank", "B-Rank", "C-Rank" })
                {
                    int count = rankCounts.ContainsKey(rank) ? rankCounts[rank] : 0;
                    double percentage = (count / (double)heroes.Count) * 100;
                    report.AppendLine($"   {rank,-10} : {count,3} heroes ({percentage:F1}%)");
                }

                // Age statistics
                report.AppendLine("\nAge Statistics:");
                report.AppendLine($"   Average Age    : {heroes.Average(h => h.Age):F1} years");
                report.AppendLine($"   Youngest Hero  : {heroes.Min(h => h.Age)} years");
                report.AppendLine($"   Oldest Hero    : {heroes.Max(h => h.Age)} years");

                // Exam score statistics
                report.AppendLine("\nExam Score Statistics:");
                report.AppendLine($"   Average Score  : {heroes.Average(h => h.ExamScore):F1}");
                report.AppendLine($"   Highest Score  : {heroes.Max(h => h.ExamScore)}");
                report.AppendLine($"   Lowest Score   : {heroes.Min(h => h.ExamScore)}");

                // Threat level distribution
                report.AppendLine("\nThreat Level Distribution:");
                var threatCounts = heroes.GroupBy(h => h.ThreatLevel)
                                        .OrderByDescending(g => g.Count())
                                        .ToDictionary(g => g.Key, g => g.Count());
                foreach (var threat in threatCounts)
                {
                    double percentage = (threat.Value / (double)heroes.Count) * 100;
                    report.AppendLine($"   {threat.Key,-30} : {threat.Value,3} ({percentage:F1}%)");
                }

                report.AppendLine("\n═══════════════════════════════════════════════════════════\n");

                // Top Heroes Section
                report.AppendLine("TOP 10 HEROES BY EXAM SCORE");
                report.AppendLine("───────────────────────────────────────────────────────────");
                report.AppendLine($"{"Rank",-8} {"Hero ID",-10} {"Name",-20} {"Score",-8} {"Superpower",-25}");
                report.AppendLine("───────────────────────────────────────────────────────────");

                var topHeroes = heroes.OrderByDescending(h => h.ExamScore).Take(10);
                int position = 1;
                foreach (var hero in topHeroes)
                {
                    report.AppendLine($"{position + ".",-8} {hero.HeroID,-10} {hero.Name,-20} {hero.ExamScore,-8} {hero.Superpower,-25}");
                    position++;
                }

                report.AppendLine("\n═══════════════════════════════════════════════════════════\n");

                // Heroes by Rank Section
                report.AppendLine("HEROES BY RANK");
                report.AppendLine("───────────────────────────────────────────────────────────");

                foreach (var rank in new[] { "S-Rank", "A-Rank", "B-Rank", "C-Rank" })
                {
                    var heroesInRank = heroes.Where(h => h.Rank == rank).OrderByDescending(h => h.ExamScore);
                    if (heroesInRank.Any())
                    {
                        report.AppendLine($"\n{rank} ({heroesInRank.Count()} heroes):");
                        foreach (var hero in heroesInRank)
                        {
                            report.AppendLine($"   - {hero.Name,-20} (ID: {hero.HeroID,-10}) | Score: {hero.ExamScore,3} | Age: {hero.Age,3} | Power: {hero.Superpower}");
                        }
                    }
                }

                report.AppendLine("\n═══════════════════════════════════════════════════════════");
                report.AppendLine("                    END OF REPORT");
                report.AppendLine("═══════════════════════════════════════════════════════════");

                // Save the report as summary.txt
                string fileName = "summary.txt";
                File.WriteAllText(fileName, report.ToString());

                MessageBox.Show($"Report generated successfully!\n\nFile saved as: {fileName}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
