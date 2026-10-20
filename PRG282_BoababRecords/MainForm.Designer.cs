namespace PRG282_BoababRecords
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvAnimals = new DataGridView();
            btnDeleteAnimal = new Button();
            btnSummaryReport = new Button();
            UpdateAnimalRecord_Btn = new Button();
            label1 = new Label();
            UpdateName_Tbx = new TextBox();
            label2 = new Label();
            UpdateSpecies_Tbx = new TextBox();
            label3 = new Label();
            UpdateAge_Tbx = new TextBox();
            label4 = new Label();
            RecoveryScore_Tbx = new TextBox();
            SaveUpdateChanges_Btn = new Button();
            ClearChanges_Btn = new Button();
            SearchBox_Tbx = new TextBox();
            Search_Btn = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).BeginInit();
            SuspendLayout();
            // 
            // dgvAnimals
            // 
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnimals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimals.Location = new Point(12, 12);
            dgvAnimals.MultiSelect = false;
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersWidth = 62;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.Size = new Size(1024, 300);
            dgvAnimals.TabIndex = 0;
            dgvAnimals.CellContentClick += dgvAnimals_CellContentClick;
            // 
            // btnDeleteAnimal
            // 
            btnDeleteAnimal.Location = new Point(864, 507);
            btnDeleteAnimal.Margin = new Padding(2);
            btnDeleteAnimal.Name = "btnDeleteAnimal";
            btnDeleteAnimal.Size = new Size(172, 41);
            btnDeleteAnimal.TabIndex = 1;
            btnDeleteAnimal.Text = "Delete Animal";
            btnDeleteAnimal.UseVisualStyleBackColor = true;
            btnDeleteAnimal.Click += btnDeleteAnimal_Click;
            // 
            // btnSummaryReport
            // 
            btnSummaryReport.Location = new Point(865, 462);
            btnSummaryReport.Margin = new Padding(2);
            btnSummaryReport.Name = "btnSummaryReport";
            btnSummaryReport.Size = new Size(172, 41);
            btnSummaryReport.TabIndex = 2;
            btnSummaryReport.Text = "Generate Summary Report";
            btnSummaryReport.UseVisualStyleBackColor = true;
            btnSummaryReport.Click += btnSummaryReport_Click;
            // 
            // UpdateAnimalRecord_Btn
            // 
            UpdateAnimalRecord_Btn.Location = new Point(305, 334);
            UpdateAnimalRecord_Btn.Name = "UpdateAnimalRecord_Btn";
            UpdateAnimalRecord_Btn.Size = new Size(172, 41);
            UpdateAnimalRecord_Btn.TabIndex = 3;
            UpdateAnimalRecord_Btn.Text = "Update Animal";
            UpdateAnimalRecord_Btn.UseVisualStyleBackColor = true;
            UpdateAnimalRecord_Btn.Click += UpdateAnimalRecord_Btn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(15, 338);
            label1.Name = "label1";
            label1.Size = new Size(53, 19);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // UpdateName_Tbx
            // 
            UpdateName_Tbx.Location = new Point(84, 334);
            UpdateName_Tbx.Name = "UpdateName_Tbx";
            UpdateName_Tbx.Size = new Size(156, 23);
            UpdateName_Tbx.TabIndex = 5;
            UpdateName_Tbx.TextChanged += UpdateName_Tbx_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(15, 364);
            label2.Name = "label2";
            label2.Size = new Size(63, 19);
            label2.TabIndex = 6;
            label2.Text = "Species:";
            // 
            // UpdateSpecies_Tbx
            // 
            UpdateSpecies_Tbx.Location = new Point(84, 364);
            UpdateSpecies_Tbx.Name = "UpdateSpecies_Tbx";
            UpdateSpecies_Tbx.Size = new Size(156, 23);
            UpdateSpecies_Tbx.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(15, 396);
            label3.Name = "label3";
            label3.Size = new Size(40, 19);
            label3.TabIndex = 8;
            label3.Text = "Age:";
            // 
            // UpdateAge_Tbx
            // 
            UpdateAge_Tbx.Location = new Point(84, 395);
            UpdateAge_Tbx.Name = "UpdateAge_Tbx";
            UpdateAge_Tbx.Size = new Size(156, 23);
            UpdateAge_Tbx.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.Location = new Point(15, 425);
            label4.Name = "label4";
            label4.Size = new Size(119, 19);
            label4.TabIndex = 10;
            label4.Text = "Recovery Score:";
            // 
            // RecoveryScore_Tbx
            // 
            RecoveryScore_Tbx.Location = new Point(140, 425);
            RecoveryScore_Tbx.Name = "RecoveryScore_Tbx";
            RecoveryScore_Tbx.Size = new Size(100, 23);
            RecoveryScore_Tbx.TabIndex = 11;
            // 
            // SaveUpdateChanges_Btn
            // 
            SaveUpdateChanges_Btn.BackColor = Color.GreenYellow;
            SaveUpdateChanges_Btn.Location = new Point(305, 381);
            SaveUpdateChanges_Btn.Name = "SaveUpdateChanges_Btn";
            SaveUpdateChanges_Btn.Size = new Size(172, 41);
            SaveUpdateChanges_Btn.TabIndex = 12;
            SaveUpdateChanges_Btn.Text = "Save Changes";
            SaveUpdateChanges_Btn.UseVisualStyleBackColor = false;
            SaveUpdateChanges_Btn.Visible = false;
            SaveUpdateChanges_Btn.Click += SaveUpdateChanges_Btn_Click;
            // 
            // ClearChanges_Btn
            // 
            ClearChanges_Btn.BackColor = Color.RosyBrown;
            ClearChanges_Btn.Location = new Point(305, 428);
            ClearChanges_Btn.Name = "ClearChanges_Btn";
            ClearChanges_Btn.Size = new Size(172, 41);
            ClearChanges_Btn.TabIndex = 13;
            ClearChanges_Btn.Text = "Clear Changes";
            ClearChanges_Btn.UseVisualStyleBackColor = false;
            ClearChanges_Btn.Visible = false;
            ClearChanges_Btn.Click += ClearChanges_Btn_Click;
            // 
            // SearchBox_Tbx
            // 
            SearchBox_Tbx.Location = new Point(737, 338);
            SearchBox_Tbx.Name = "SearchBox_Tbx";
            SearchBox_Tbx.Size = new Size(205, 23);
            SearchBox_Tbx.TabIndex = 14;
            SearchBox_Tbx.Text = "ID:";
            SearchBox_Tbx.Click += SearchBox_Tbx_Click;
            // 
            // Search_Btn
            // 
            Search_Btn.BackColor = Color.Khaki;
            Search_Btn.FlatAppearance.BorderColor = Color.FromArgb(192, 64, 0);
            Search_Btn.FlatAppearance.BorderSize = 2;
            Search_Btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            Search_Btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            Search_Btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Search_Btn.Location = new Point(936, 336);
            Search_Btn.Name = "Search_Btn";
            Search_Btn.Size = new Size(100, 25);
            Search_Btn.TabIndex = 15;
            Search_Btn.Text = "Search";
            Search_Btn.UseVisualStyleBackColor = false;
            Search_Btn.Click += Search_Btn_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 559);
            Controls.Add(Search_Btn);
            Controls.Add(SearchBox_Tbx);
            Controls.Add(ClearChanges_Btn);
            Controls.Add(SaveUpdateChanges_Btn);
            Controls.Add(RecoveryScore_Tbx);
            Controls.Add(label4);
            Controls.Add(UpdateAge_Tbx);
            Controls.Add(label3);
            Controls.Add(UpdateSpecies_Tbx);
            Controls.Add(label2);
            Controls.Add(UpdateName_Tbx);
            Controls.Add(label1);
            Controls.Add(UpdateAnimalRecord_Btn);
            Controls.Add(btnSummaryReport);
            Controls.Add(btnDeleteAnimal);
            Controls.Add(dgvAnimals);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Baobab Ridge Wildlife Rehabilitation Records";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnimals;
        private Button btnDeleteAnimal;
        private Button btnSummaryReport;
        private Button UpdateAnimalRecord_Btn;
        private Label label1;
        private TextBox UpdateName_Tbx;
        private Label label2;
        private TextBox UpdateSpecies_Tbx;
        private Label label3;
        private TextBox UpdateAge_Tbx;
        private Label label4;
        private TextBox RecoveryScore_Tbx;
        private Button SaveUpdateChanges_Btn;
        private Button ClearChanges_Btn;
        private TextBox SearchBox_Tbx;
        private Button Search_Btn;
    }
}
