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
            AnimalsFromFile();
        }

        private void AnimalsFromFile()
        {
            string filePath = "C:\\Users\\Keaan\\Documents\\GitHub\\PRG282_Project\\PRG282_BoababRecords\\animals.txt";

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

                    if (parts.Length != 7)
                    {
                        skippedLineCount++;
                        continue;
                    }

                    if (!int.TryParse(parts[3].Trim(), out int age) || !int.TryParse(parts[4].Trim(), out int score))
                    {
                        skippedLineCount++;
                        continue;
                    }

                    Animal a = new Animal();
                    a.AnimalID = parts[0].Trim().ToUpper();
                    a.Name = parts[1].Trim();
                    a.Species = parts[2].Trim();
                    a.Age = age;
                    a.RecoveryScore = score;
                    a.Status = parts[5].Trim();
                    a.HousingUnit = parts[6].Trim();

                    animalList.Add(a);
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

        private void dgvAnimals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
