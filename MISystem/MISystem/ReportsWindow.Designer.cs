
namespace MISystem
{
    partial class ReportsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsWindow));
            this.ReportsMenu = new System.Windows.Forms.MenuStrip();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортВExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportsWindTabContr = new System.Windows.Forms.TabControl();
            this.ListOfMI = new System.Windows.Forms.TabPage();
            this.CreateReport = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ReportsProdFacilComboBox = new System.Windows.Forms.ComboBox();
            this.ReportsOandGPFComboBox = new System.Windows.Forms.ComboBox();
            this.ReportsDistrComboBox = new System.Windows.Forms.ComboBox();
            this.ListOfMIDataGrid = new System.Windows.Forms.DataGridView();
            this.VerifPlan = new System.Windows.Forms.TabPage();
            this.ReportsYear = new System.Windows.Forms.ComboBox();
            this.ReportsOandGPFComboBoxPlan = new System.Windows.Forms.ComboBox();
            this.ReportsDistrComboBoxPlan = new System.Windows.Forms.ComboBox();
            this.CreatePlanReportButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PlanOfMIDataGrid = new System.Windows.Forms.DataGridView();
            this.List = new System.Data.DataSet();
            this.Plan = new System.Data.DataSet();
            this.ReportsMenu.SuspendLayout();
            this.ReportsWindTabContr.SuspendLayout();
            this.ListOfMI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListOfMIDataGrid)).BeginInit();
            this.VerifPlan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlanOfMIDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Plan)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportsMenu
            // 
            this.ReportsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отчетыToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.ReportsMenu.Location = new System.Drawing.Point(0, 0);
            this.ReportsMenu.Name = "ReportsMenu";
            this.ReportsMenu.Size = new System.Drawing.Size(1111, 24);
            this.ReportsMenu.TabIndex = 0;
            this.ReportsMenu.Text = "ReportsMenu";
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортВExcelToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // экспортВExcelToolStripMenuItem
            // 
            this.экспортВExcelToolStripMenuItem.Name = "экспортВExcelToolStripMenuItem";
            this.экспортВExcelToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.экспортВExcelToolStripMenuItem.Text = "Экспорт в Excel";
            this.экспортВExcelToolStripMenuItem.Click += new System.EventHandler(this.экспортВExcelToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // ReportsWindTabContr
            // 
            this.ReportsWindTabContr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportsWindTabContr.Controls.Add(this.ListOfMI);
            this.ReportsWindTabContr.Controls.Add(this.VerifPlan);
            this.ReportsWindTabContr.Location = new System.Drawing.Point(13, 28);
            this.ReportsWindTabContr.Name = "ReportsWindTabContr";
            this.ReportsWindTabContr.SelectedIndex = 0;
            this.ReportsWindTabContr.Size = new System.Drawing.Size(1086, 524);
            this.ReportsWindTabContr.TabIndex = 1;
            // 
            // ListOfMI
            // 
            this.ListOfMI.Controls.Add(this.CreateReport);
            this.ListOfMI.Controls.Add(this.label3);
            this.ListOfMI.Controls.Add(this.label2);
            this.ListOfMI.Controls.Add(this.label1);
            this.ListOfMI.Controls.Add(this.ReportsProdFacilComboBox);
            this.ListOfMI.Controls.Add(this.ReportsOandGPFComboBox);
            this.ListOfMI.Controls.Add(this.ReportsDistrComboBox);
            this.ListOfMI.Controls.Add(this.ListOfMIDataGrid);
            this.ListOfMI.Location = new System.Drawing.Point(4, 22);
            this.ListOfMI.Name = "ListOfMI";
            this.ListOfMI.Padding = new System.Windows.Forms.Padding(3);
            this.ListOfMI.Size = new System.Drawing.Size(1078, 498);
            this.ListOfMI.TabIndex = 0;
            this.ListOfMI.Text = "Перечень СИ";
            this.ListOfMI.UseVisualStyleBackColor = true;
            // 
            // CreateReport
            // 
            this.CreateReport.Location = new System.Drawing.Point(749, 9);
            this.CreateReport.Name = "CreateReport";
            this.CreateReport.Size = new System.Drawing.Size(104, 23);
            this.CreateReport.TabIndex = 8;
            this.CreateReport.Text = "Сформировать";
            this.CreateReport.UseVisualStyleBackColor = true;
            this.CreateReport.Click += new System.EventHandler(this.CreateReport_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Объект";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ЦДНГ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Сетевой район";
            // 
            // ReportsProdFacilComboBox
            // 
            this.ReportsProdFacilComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsProdFacilComboBox.Enabled = false;
            this.ReportsProdFacilComboBox.FormattingEnabled = true;
            this.ReportsProdFacilComboBox.Location = new System.Drawing.Point(549, 11);
            this.ReportsProdFacilComboBox.Name = "ReportsProdFacilComboBox";
            this.ReportsProdFacilComboBox.Size = new System.Drawing.Size(110, 21);
            this.ReportsProdFacilComboBox.TabIndex = 3;
            this.ReportsProdFacilComboBox.SelectedIndexChanged += new System.EventHandler(this.ReportsProdFacilComboBox_SelectedIndexChanged);
            // 
            // ReportsOandGPFComboBox
            // 
            this.ReportsOandGPFComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsOandGPFComboBox.Enabled = false;
            this.ReportsOandGPFComboBox.FormattingEnabled = true;
            this.ReportsOandGPFComboBox.Location = new System.Drawing.Point(312, 11);
            this.ReportsOandGPFComboBox.Name = "ReportsOandGPFComboBox";
            this.ReportsOandGPFComboBox.Size = new System.Drawing.Size(110, 21);
            this.ReportsOandGPFComboBox.TabIndex = 2;
            this.ReportsOandGPFComboBox.SelectedIndexChanged += new System.EventHandler(this.ReportsOandGPFComboBox_SelectedIndexChanged);
            // 
            // ReportsDistrComboBox
            // 
            this.ReportsDistrComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsDistrComboBox.FormattingEnabled = true;
            this.ReportsDistrComboBox.Location = new System.Drawing.Point(94, 11);
            this.ReportsDistrComboBox.Name = "ReportsDistrComboBox";
            this.ReportsDistrComboBox.Size = new System.Drawing.Size(110, 21);
            this.ReportsDistrComboBox.TabIndex = 1;
            this.ReportsDistrComboBox.SelectedIndexChanged += new System.EventHandler(this.ReportsDistrComboBox_SelectedIndexChanged);
            // 
            // ListOfMIDataGrid
            // 
            this.ListOfMIDataGrid.AllowUserToAddRows = false;
            this.ListOfMIDataGrid.AllowUserToDeleteRows = false;
            this.ListOfMIDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListOfMIDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListOfMIDataGrid.Location = new System.Drawing.Point(7, 53);
            this.ListOfMIDataGrid.Name = "ListOfMIDataGrid";
            this.ListOfMIDataGrid.ReadOnly = true;
            this.ListOfMIDataGrid.Size = new System.Drawing.Size(1065, 439);
            this.ListOfMIDataGrid.TabIndex = 0;
            // 
            // VerifPlan
            // 
            this.VerifPlan.Controls.Add(this.ReportsYear);
            this.VerifPlan.Controls.Add(this.ReportsOandGPFComboBoxPlan);
            this.VerifPlan.Controls.Add(this.ReportsDistrComboBoxPlan);
            this.VerifPlan.Controls.Add(this.CreatePlanReportButton);
            this.VerifPlan.Controls.Add(this.label6);
            this.VerifPlan.Controls.Add(this.label5);
            this.VerifPlan.Controls.Add(this.label4);
            this.VerifPlan.Controls.Add(this.PlanOfMIDataGrid);
            this.VerifPlan.Location = new System.Drawing.Point(4, 22);
            this.VerifPlan.Name = "VerifPlan";
            this.VerifPlan.Padding = new System.Windows.Forms.Padding(3);
            this.VerifPlan.Size = new System.Drawing.Size(1078, 498);
            this.VerifPlan.TabIndex = 1;
            this.VerifPlan.Text = "График поверки СИ";
            this.VerifPlan.UseVisualStyleBackColor = true;
            // 
            // ReportsYear
            // 
            this.ReportsYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsYear.FormattingEnabled = true;
            this.ReportsYear.Location = new System.Drawing.Point(540, 13);
            this.ReportsYear.Name = "ReportsYear";
            this.ReportsYear.Size = new System.Drawing.Size(121, 21);
            this.ReportsYear.TabIndex = 10;
            this.ReportsYear.SelectedIndexChanged += new System.EventHandler(this.ReportsYear_SelectedIndexChanged);
            // 
            // ReportsOandGPFComboBoxPlan
            // 
            this.ReportsOandGPFComboBoxPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsOandGPFComboBoxPlan.Enabled = false;
            this.ReportsOandGPFComboBoxPlan.FormattingEnabled = true;
            this.ReportsOandGPFComboBoxPlan.Location = new System.Drawing.Point(313, 12);
            this.ReportsOandGPFComboBoxPlan.Name = "ReportsOandGPFComboBoxPlan";
            this.ReportsOandGPFComboBoxPlan.Size = new System.Drawing.Size(121, 21);
            this.ReportsOandGPFComboBoxPlan.TabIndex = 9;
            this.ReportsOandGPFComboBoxPlan.SelectedIndexChanged += new System.EventHandler(this.ReportsOandGPFComboBoxPlan_SelectedIndexChanged);
            // 
            // ReportsDistrComboBoxPlan
            // 
            this.ReportsDistrComboBoxPlan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReportsDistrComboBoxPlan.FormattingEnabled = true;
            this.ReportsDistrComboBoxPlan.Location = new System.Drawing.Point(95, 12);
            this.ReportsDistrComboBoxPlan.Name = "ReportsDistrComboBoxPlan";
            this.ReportsDistrComboBoxPlan.Size = new System.Drawing.Size(121, 21);
            this.ReportsDistrComboBoxPlan.TabIndex = 8;
            this.ReportsDistrComboBoxPlan.SelectedIndexChanged += new System.EventHandler(this.ReportsDistrComboBoxPlan_SelectedIndexChanged);
            // 
            // CreatePlanReportButton
            // 
            this.CreatePlanReportButton.Location = new System.Drawing.Point(760, 11);
            this.CreatePlanReportButton.Name = "CreatePlanReportButton";
            this.CreatePlanReportButton.Size = new System.Drawing.Size(96, 23);
            this.CreatePlanReportButton.TabIndex = 7;
            this.CreatePlanReportButton.Text = "Сформировать";
            this.CreatePlanReportButton.UseVisualStyleBackColor = true;
            this.CreatePlanReportButton.Click += new System.EventHandler(this.CreatePlanReportButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(509, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Год";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "ЦДНГ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Сетевой район";
            // 
            // PlanOfMIDataGrid
            // 
            this.PlanOfMIDataGrid.AllowUserToAddRows = false;
            this.PlanOfMIDataGrid.AllowUserToDeleteRows = false;
            this.PlanOfMIDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlanOfMIDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlanOfMIDataGrid.Location = new System.Drawing.Point(6, 54);
            this.PlanOfMIDataGrid.Name = "PlanOfMIDataGrid";
            this.PlanOfMIDataGrid.ReadOnly = true;
            this.PlanOfMIDataGrid.Size = new System.Drawing.Size(1066, 438);
            this.PlanOfMIDataGrid.TabIndex = 0;
            // 
            // List
            // 
            this.List.DataSetName = "NewDataSet";
            // 
            // Plan
            // 
            this.Plan.DataSetName = "NewDataSet";
            // 
            // ReportsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 564);
            this.Controls.Add(this.ReportsWindTabContr);
            this.Controls.Add(this.ReportsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.ReportsMenu;
            this.Name = "ReportsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчеты";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ReportsMenu.ResumeLayout(false);
            this.ReportsMenu.PerformLayout();
            this.ReportsWindTabContr.ResumeLayout(false);
            this.ListOfMI.ResumeLayout(false);
            this.ListOfMI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListOfMIDataGrid)).EndInit();
            this.VerifPlan.ResumeLayout(false);
            this.VerifPlan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlanOfMIDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Plan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ReportsMenu;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экспортВExcelToolStripMenuItem;
        private System.Windows.Forms.TabControl ReportsWindTabContr;
        private System.Windows.Forms.TabPage ListOfMI;
        private System.Windows.Forms.ComboBox ReportsProdFacilComboBox;
        private System.Windows.Forms.ComboBox ReportsOandGPFComboBox;
        private System.Windows.Forms.ComboBox ReportsDistrComboBox;
        private System.Windows.Forms.DataGridView ListOfMIDataGrid;
        private System.Windows.Forms.TabPage VerifPlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CreateReport;
        private System.Data.DataSet List;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView PlanOfMIDataGrid;
        private System.Data.DataSet Plan;
        private System.Windows.Forms.Button CreatePlanReportButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ReportsYear;
        private System.Windows.Forms.ComboBox ReportsOandGPFComboBoxPlan;
        private System.Windows.Forms.ComboBox ReportsDistrComboBoxPlan;
    }
}