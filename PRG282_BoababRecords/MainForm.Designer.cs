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
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).BeginInit();
            SuspendLayout();
            // 
            // dgvAnimals
            // 
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnimals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnimals.Location = new Point(17, 20);
            dgvAnimals.Margin = new Padding(4, 5, 4, 5);
            dgvAnimals.MultiSelect = false;
            dgvAnimals.Name = "dgvAnimals";
            dgvAnimals.ReadOnly = true;
            dgvAnimals.RowHeadersWidth = 62;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.Size = new Size(1109, 500);
            dgvAnimals.TabIndex = 0;
            dgvAnimals.CellContentClick += dgvAnimals_CellContentClick;
            // 
            // btnDeleteAnimal
            // 
            btnDeleteAnimal.Location = new Point(518, 547);
            btnDeleteAnimal.Name = "btnDeleteAnimal";
            btnDeleteAnimal.Size = new Size(246, 69);
            btnDeleteAnimal.TabIndex = 1;
            btnDeleteAnimal.Text = "Delete Animal";
            btnDeleteAnimal.UseVisualStyleBackColor = true;
            btnDeleteAnimal.Click += btnDeleteAnimal_Click;
            // 
            // btnSummaryReport
            // 
            btnSummaryReport.Location = new Point(243, 547);
            btnSummaryReport.Name = "btnSummaryReport";
            btnSummaryReport.Size = new Size(246, 69);
            btnSummaryReport.TabIndex = 2;
            btnSummaryReport.Text = "Generate Summary Report";
            btnSummaryReport.UseVisualStyleBackColor = true;
            btnSummaryReport.Click += btnSummaryReport_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(btnSummaryReport);
            Controls.Add(btnDeleteAnimal);
            Controls.Add(dgvAnimals);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Baobab Ridge Wildlife Rehabilitation Records";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAnimals).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnimals;
        private Button btnDeleteAnimal;
        private Button btnSummaryReport;
    }
}
