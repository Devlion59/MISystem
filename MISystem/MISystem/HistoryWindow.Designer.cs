
namespace MISystem
{
    partial class HistoryWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryWindow));
            this.HistoryWindowDataGrid = new System.Windows.Forms.DataGridView();
            this.HistoryWindowDataSet = new System.Data.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryWindowDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryWindowDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // HistoryWindowDataGrid
            // 
            this.HistoryWindowDataGrid.AllowUserToAddRows = false;
            this.HistoryWindowDataGrid.AllowUserToDeleteRows = false;
            this.HistoryWindowDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryWindowDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistoryWindowDataGrid.Location = new System.Drawing.Point(12, 12);
            this.HistoryWindowDataGrid.Name = "HistoryWindowDataGrid";
            this.HistoryWindowDataGrid.ReadOnly = true;
            this.HistoryWindowDataGrid.Size = new System.Drawing.Size(820, 426);
            this.HistoryWindowDataGrid.TabIndex = 0;
            // 
            // HistoryWindowDataSet
            // 
            this.HistoryWindowDataSet.DataSetName = "NewDataSet";
            // 
            // HistoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(844, 450);
            this.Controls.Add(this.HistoryWindowDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "История";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.HistoryWindowDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryWindowDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView HistoryWindowDataGrid;
        private System.Data.DataSet HistoryWindowDataSet;
    }
}