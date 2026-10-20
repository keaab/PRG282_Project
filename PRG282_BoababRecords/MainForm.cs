using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        // When a row in the grid is clicked, load the record into the edit textboxes
        private void dgvAnimals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < animalList.Count)
            {
                Animal selected = animalList[e.RowIndex];
                LoadAnimalIntoForm(selected);
            }
        }

        // Helper to load animal data and switch to Edit Mode
        private void LoadAnimalIntoForm(Animal animal)
        {
            AnimalID_Tbx.Text = animal.AnimalID;
            AnimalID_Tbx.ReadOnly = true;

            UpdateRecordHandler.UpdateDataInTextBoxes(UpdateName_Tbx, UpdateSpecies_Tbx, UpdateAge_Tbx, RecoveryScore_Tbx, animal);

            ClearChanges_Btn.Visible = true;
            SaveUpdateChanges_Btn.Visible = true;
            UpdateAnimalRecord_Btn.Visible = false;
            AddAnimalRecord_Btn.Enabled = false;
        }

        // Clears placeholder text in search box
        private void SearchBox_Tbx_Click(object sender, EventArgs e)
        {
            if (SearchBox_Tbx.Text == "ID:")
            {
                SearchBox_Tbx.Text = string.Empty;
            }
        }

        // Searches for an animal by ID and loads it into the edit textboxes 
        private void Search_Btn_Click(object sender, EventArgs e)
        {
            string searchId = SearchBox_Tbx.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchId) || searchId == "ID:")
            {
                MessageBox.Show("Please enter an Animal ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int foundIndex = animalList.FindIndex(a => a.AnimalID.Equals(searchId, StringComparison.OrdinalIgnoreCase));

            if (foundIndex >= 0)
            {
                dgvAnimals.ClearSelection();
                dgvAnimals.Rows[foundIndex].Selected = true;
                dgvAnimals.CurrentCell = dgvAnimals.Rows[foundIndex].Cells[0];

                LoadAnimalIntoForm(animalList[foundIndex]);
            }
            else
            {
                MessageBox.Show($"Animal with ID '{searchId}' was not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Add a new animal
        private void AddAnimalRecord_Btn_Click(object sender, EventArgs e)
        {
            string id = AnimalID_Tbx.Text.Trim().ToUpper();
            string name = UpdateName_Tbx.Text.Trim();
            string species = UpdateSpecies_Tbx.Text.Trim();
            string ageText = UpdateAge_Tbx.Text.Trim();
            string scoreText = RecoveryScore_Tbx.Text.Trim();

            // Validate Animal ID 
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Animal ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AnimalID_Tbx.Focus();
                return;
            }

            // Must start with WR- and be exactly 7 characters long, and the last 4 characters must be digits
            if (!id.StartsWith("WR-") || id.Length != 7 || !int.TryParse(id.Substring(3), out _))
            {
                MessageBox.Show("Animal ID must start with 'WR-' followed by 4 digits (ex. WR-0007).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AnimalID_Tbx.Focus();
                return;
            }

            // Check uniqueness using a standard foreach loop
            foreach (Animal a in animalList)
            {
                if (a.AnimalID.ToUpper() == id)
                {
                    MessageBox.Show("Animal ID already exists. Please enter a unique ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    AnimalID_Tbx.Focus();
                    return;
                }
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(name) || name.Length > 30 || name.Contains("|"))
            {
                MessageBox.Show("Name must be between 1 and 30 characters and cannot contain '|'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UpdateName_Tbx.Focus();
                return;
            }

            // Validate Species
            if (string.IsNullOrWhiteSpace(species) || species.Length > 30 || species.Contains("|"))
            {
                MessageBox.Show("Species must be between 1 and 30 characters and cannot contain '|'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UpdateSpecies_Tbx.Focus();
                return;
            }

            // Validate Age
            if (!int.TryParse(ageText, out int age) || age < 0 || age > 100)
            {
                MessageBox.Show("Age must be a whole number between 0 and 100.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UpdateAge_Tbx.Focus();
                return;
            }

            // Validate Recovery Score
            if (!int.TryParse(scoreText, out int score) || score < 0 || score > 100)
            {
                MessageBox.Show("Recovery Score must be a whole number between 0 and 100.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RecoveryScore_Tbx.Focus();
                return;
            }

            try
            {
                // Create the animal object
                Animal newAnimal = new Animal(id, name, species, age, score);

                // Append to animals.txt using StreamWriter
                using (StreamWriter sw = new StreamWriter(AnimalFileManager.filePath, true))
                {
                    sw.WriteLine(newAnimal.ToFileLine());
                }

                // Show confirmation with calculated status and housing unit
                MessageBox.Show(
                    $"Animal {newAnimal.AnimalID} ({newAnimal.Name}) added successfully!\n\n" +
                    $"Status: {newAnimal.Status}\n" +
                    $"Housing Unit: {newAnimal.HousingUnit}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Refresh the grid and clear input boxes
                RefreshGrid();
                ResetInputBoxes();
            }
            catch (IOException ex)
            {
                MessageBox.Show("File error while saving animal: " + ex.Message, "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Permission error while saving animal: " + ex.Message, "Access Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reloads animal records and updates the DataGridView
        public void RefreshGrid()
        {
            animalList = AnimalFileManager.LoadAnimals();

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
                    RefreshGrid();

                    // If the deleted animal was loaded in the form, clear the form (Section 4.3)
                    if (AnimalID_Tbx.Text.Equals(selectedAnimal.AnimalID, StringComparison.OrdinalIgnoreCase))
                    {
                        ResetInputBoxes();
                    }
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
            if (dgvAnimals.CurrentRow != null && dgvAnimals.CurrentRow.Index >= 0 && dgvAnimals.CurrentRow.Index < animalList.Count)
            {
                LoadAnimalIntoForm(animalList[dgvAnimals.CurrentRow.Index]);
            }
            else
            {
                MessageBox.Show("Please select an animal record from the grid first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateName_Tbx_TextChanged(object sender, EventArgs e)
        {
            string currentText = UpdateName_Tbx.Text;
        }

        private void ClearChanges_Btn_Click(object sender, EventArgs e)
        {
            ResetInputBoxes();
        }

        // Resets the form back to Add Mode with empty boxes (Section 3.2)
        private void ResetInputBoxes()
        {
            ClearChanges_Btn.Visible = false;
            SaveUpdateChanges_Btn.Visible = false;
            UpdateAnimalRecord_Btn.Visible = true;
            AddAnimalRecord_Btn.Enabled = true;

            AnimalID_Tbx.ReadOnly = false;
            AnimalID_Tbx.Text = string.Empty;
            UpdateName_Tbx.Text = string.Empty;
            UpdateSpecies_Tbx.Text = string.Empty;
            UpdateAge_Tbx.Text = string.Empty;
            RecoveryScore_Tbx.Text = string.Empty;
        }

        private void SaveUpdateChanges_Btn_Click(object sender, EventArgs e)
        {
            if (dgvAnimals.CurrentRow == null || dgvAnimals.CurrentRow.Index < 0)
            {
                MessageBox.Show("Please select an animal record to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] fileData = UpdateRecordHandler.DataReadFromTextBoxes(UpdateName_Tbx, UpdateSpecies_Tbx, UpdateAge_Tbx, RecoveryScore_Tbx);
            int rowIndex = dgvAnimals.CurrentRow.Index;
            bool ValidTextBoxFields = UpdateRecordHandler.AllTextBoxesValid(fileData);
            
            if (ValidTextBoxFields)
            {
                Animal updatedAnimalRecord = UpdateRecordHandler.CreateUpdatedAnimalRecord(animalList, rowIndex, fileData);
                UpdateRecordHandler.WriteUpdatedAnimalToFile(updatedAnimalRecord, rowIndex);
            }

            // Refreshes the DGV to display the new data that has been written to the txt file.
            RefreshGrid();

            // Returns to Add mode with cleared fields
            ResetInputBoxes();
        }
    }
}
