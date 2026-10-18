using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PRG282_BoababRecords
{
    public partial class MainForm : Form
    {
        private List<Animal> animalList = new List<Animal>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
           
        }

      

        private void dgvAnimals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string filePath = "animals.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                return;
            }

            animalList.Clear();
            int skippedLineCount = 0;

            try
            {
                string[] Lines = File.ReadAllLines(filePath);

                foreach (string line in Lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');

                    if (parts.Length < 5)
                    {
                        skippedLineCount++;
                        continue;
                    }

                    if (!int.TryParse(parts[3].Trim(), out int age) || !int.TryParse(parts[4].Trim(), out int score))
                    {
                        skippedLineCount++;
                        continue;
                    }

                    if (parts.Length == 5)
                    {
                        animalList.Add(new Animal(parts[0].Trim().ToUpper(), parts[1].Trim(), parts[2].Trim(), age, score));
                    }
                    else if (parts.Length >= 7)
                    {
                        animalList.Add(new Animal(parts[0].Trim().ToUpper(), parts[1].Trim(), parts[2].Trim(), age, score, parts[5].Trim(), parts[6].Trim()));
                    }
                }

                dgvAnimals.DataSource = null;
                dgvAnimals.DataSource = animalList;

                if (dgvAnimals.Columns.Count > 0)
                {
                    dgvAnimals.Columns["AnimalID"].HeaderText = "Animal ID";
                    dgvAnimals.Columns["Name"].HeaderText = "Name";
                    dgvAnimals.Columns["Species"].HeaderText = "Species";
                    dgvAnimals.Columns["Age"].HeaderText = "Age";
                    dgvAnimals.Columns["RecoveryScore"].HeaderText = "Recovery Score";
                    dgvAnimals.Columns["Status"].HeaderText = "Status";
                    dgvAnimals.Columns["HousingUnit"].HeaderText = "Housing Unit";
                }

                if (skippedLineCount > 0)
                {
                    MessageBox.Show($"{skippedLineCount} Malformed line(s) were skipped during loading.", "File Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Could not read {filePath}: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Reloads animal records and updates the DataGridView.
        public void RefreshGrid()

        {
            animalList = AnimalFileManager.LoadAnimals();

            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = animalList;
            dgvAnimals.Columns["AnimalID"].HeaderText = "Animal ID";
            dgvAnimals.Columns["RecoveryScore"].HeaderText = "Recovery Score";
            dgvAnimals.Columns["HousingUnit"].HeaderText = "Housing Unit";
        }

        /// <summary>
        /// Member 4: Calculates animal summary statistics and exports summary.txt.
        /// </summary>
        private void btnSummaryReport_Click(object sender, EventArgs e)
        {
            try
            {
                string report = SummaryReportEngine.GenerateAndExportSummary();
                MessageBox.Show(report, "Summary Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"File error exporting summary: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Permission error exporting summary: {ex.Message}", "Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Member 4: Deletes the selected animal record after explicit confirmation.
        /// </summary>
        private void btnDeleteAnimal_Click(object sender, EventArgs e)
        {
            if (dgvAnimals.CurrentRow == null || dgvAnimals.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select an animal from the list to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Animal selectedAnimal = (Animal)dgvAnimals.CurrentRow.DataBoundItem;

            DialogResult confirm = MessageBox.Show(
                $"Delete {selectedAnimal.AnimalID} ({selectedAnimal.Name})? This cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                bool deleted = SummaryReportEngine.DeleteAnimalRecord(selectedAnimal.AnimalID);

                if (deleted)
                {
                    MessageBox.Show($"Animal record {selectedAnimal.AnimalID} deleted successfully.", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AnimalsFromFile();
                }
                else
                {
                    MessageBox.Show("Record could not be found or was already removed.", "Delete Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"File error during deletion: {ex.Message}", "File Locked", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Permission error during deletion: {ex.Message}", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}




