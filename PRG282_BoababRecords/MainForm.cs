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

    }

}




