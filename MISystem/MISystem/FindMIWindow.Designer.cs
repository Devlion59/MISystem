
namespace MISystem
{
    partial class FindMIWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindMIWindow));
            this.FindMISerialNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FindDataGridView = new System.Windows.Forms.DataGridView();
            this.FindDataSet = new System.Data.DataSet();
            this.FindButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.FindDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // FindMISerialNumber
            // 
            this.FindMISerialNumber.Location = new System.Drawing.Point(139, 12);
            this.FindMISerialNumber.Name = "FindMISerialNumber";
            this.FindMISerialNumber.Size = new System.Drawing.Size(100, 20);
            this.FindMISerialNumber.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Серийный номер СИ";
            // 
            // FindDataGridView
            // 
            this.FindDataGridView.AllowUserToAddRows = false;
            this.FindDataGridView.AllowUserToDeleteRows = false;
            this.FindDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FindDataGridView.Location = new System.Drawing.Point(13, 38);
            this.FindDataGridView.Name = "FindDataGridView";
            this.FindDataGridView.ReadOnly = true;
            this.FindDataGridView.Size = new System.Drawing.Size(674, 394);
            this.FindDataGridView.TabIndex = 2;
            // 
            // FindDataSet
            // 
            this.FindDataSet.DataSetName = "NewDataSet";
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(245, 10);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(48, 23);
            this.FindButton.TabIndex = 3;
            this.FindButton.Text = "Найти";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(612, 10);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Выход";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FindMIWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 444);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.FindDataGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FindMISerialNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindMIWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск";
            ((System.ComponentModel.ISupportInitialize)(this.FindDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FindDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FindMISerialNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView FindDataGridView;
        private System.Data.DataSet FindDataSet;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Button CloseButton;
    }
}