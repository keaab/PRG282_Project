using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace PRG282_BoababRecords
{
    public partial class MainForm : Form
    {
        private List<Animal> animalList = AnimalFileManager.LoadAnimals();


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
            string filePath = AnimalFileManager.filePath;

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
                    SummaryReportEngine.ReadAnimalsFromFile();
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

        private void UpdateAnimalRecord_Btn_Click(object sender, EventArgs e)
        {
            UpdateRecordHandler.UpdateDataInTextBoxes(UpdateName_Tbx, UpdateSpecies_Tbx, UpdateAge_Tbx, RecoveryScore_Tbx, animalList[dgvAnimals.CurrentRow.Index]);
            ClearChanges_Btn.Visible = true;
            SaveUpdateChanges_Btn.Visible = true;
            UpdateAnimalRecord_Btn.Visible = false;
        }

        private void UpdateName_Tbx_TextChanged(object sender, EventArgs e)
        {
            string currentText = UpdateName_Tbx.Text;
        }

        private void ClearChanges_Btn_Click(object sender, EventArgs e)
        {
            //Toggles the update menu button so that they cannot be intercted with. Turns on the Update button.
            ClearChanges_Btn.Visible = false;
            SaveUpdateChanges_Btn.Visible = false;
            UpdateAnimalRecord_Btn.Visible = true;

            //Clears the textboxes text field to look more visibly appealing.
            UpdateName_Tbx.Text = string.Empty;
            UpdateSpecies_Tbx.Text = string.Empty;
            UpdateAge_Tbx.Text = string.Empty;
            RecoveryScore_Tbx.Text = string.Empty;
        }

        private void SaveUpdateChanges_Btn_Click(object sender, EventArgs e)
        {
            string[] fileData = UpdateRecordHandler.DataReadFromTextBoxes(UpdateName_Tbx, UpdateSpecies_Tbx, UpdateAge_Tbx, RecoveryScore_Tbx);
            int rowIndex = dgvAnimals.CurrentRow.Index;
            bool ValidTextBoxFields = UpdateRecordHandler.AllTextBoxesValid(fileData);

            if (ValidTextBoxFields)
            {
                Animal updatedAnimalRecord = UpdateRecordHandler.CreateUpdatedAnimalRecord(animalList, rowIndex, fileData);
                UpdateRecordHandler.WriteUpdatedAnimalToFile(updatedAnimalRecord, rowIndex);
            }

            //Refreshs the DVG to display the new data that has been written to the txt file.
            RefreshGrid();

            //Toggles the update menu button so that they cannot be intercted with. Turns on the Update button.
            ClearChanges_Btn.Visible = false;
            SaveUpdateChanges_Btn.Visible = false;
            UpdateAnimalRecord_Btn.Visible = true;

            //Clears the textboxes text field to look more visibly appealing.
            UpdateName_Tbx.Text = string.Empty;
            UpdateSpecies_Tbx.Text = string.Empty;
            UpdateAge_Tbx.Text = string.Empty;
            RecoveryScore_Tbx.Text = string.Empty;
        }

        private void Search_Btn_Click(object sender, EventArgs e)
        {
            //Compares the text field of the search box to all the animal ids in the system
            string id = UpdateRecordHandler.IdQueryResult(SearchBox_Tbx.Text, UpdateRecordHandler.ReadIdsFromFile(animalList));
            List<string> listOfIds = UpdateRecordHandler.ReadIdsFromFile(animalList);
            int rowIndex = listOfIds.IndexOf(id);

            if (rowIndex >= 0)
            {
                dgvAnimals.CurrentCell = dgvAnimals.Rows[rowIndex].Cells[0];
            }

            SearchBox_Tbx.Text = "ID:";
        }

        private void SearchBox_Tbx_Click(object sender, EventArgs e)
        {
            SearchBox_Tbx.Text = string.Empty;
        }
    }

}




