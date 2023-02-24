
namespace MISystem
{
    partial class Administration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administration));
            this.AdminDataGridView = new System.Windows.Forms.DataGridView();
            this.AdminDataSet = new System.Data.DataSet();
            this.AddPersonButton = new System.Windows.Forms.Button();
            this.ChangePersonButton = new System.Windows.Forms.Button();
            this.DelPersonButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AdminDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdminDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // AdminDataGridView
            // 
            this.AdminDataGridView.AllowUserToAddRows = false;
            this.AdminDataGridView.AllowUserToDeleteRows = false;
            this.AdminDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AdminDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AdminDataGridView.Location = new System.Drawing.Point(12, 41);
            this.AdminDataGridView.Name = "AdminDataGridView";
            this.AdminDataGridView.ReadOnly = true;
            this.AdminDataGridView.Size = new System.Drawing.Size(882, 510);
            this.AdminDataGridView.TabIndex = 0;
            // 
            // AdminDataSet
            // 
            this.AdminDataSet.DataSetName = "NewDataSet";
            // 
            // AddPersonButton
            // 
            this.AddPersonButton.Location = new System.Drawing.Point(12, 12);
            this.AddPersonButton.Name = "AddPersonButton";
            this.AddPersonButton.Size = new System.Drawing.Size(75, 23);
            this.AddPersonButton.TabIndex = 1;
            this.AddPersonButton.Text = "Добавить";
            this.AddPersonButton.UseVisualStyleBackColor = true;
            this.AddPersonButton.Click += new System.EventHandler(this.AddPersonButton_Click);
            // 
            // ChangePersonButton
            // 
            this.ChangePersonButton.Location = new System.Drawing.Point(93, 12);
            this.ChangePersonButton.Name = "ChangePersonButton";
            this.ChangePersonButton.Size = new System.Drawing.Size(75, 23);
            this.ChangePersonButton.TabIndex = 2;
            this.ChangePersonButton.Text = "Изменить";
            this.ChangePersonButton.UseVisualStyleBackColor = true;
            // 
            // DelPersonButton
            // 
            this.DelPersonButton.Location = new System.Drawing.Point(174, 12);
            this.DelPersonButton.Name = "DelPersonButton";
            this.DelPersonButton.Size = new System.Drawing.Size(75, 23);
            this.DelPersonButton.TabIndex = 3;
            this.DelPersonButton.Text = "Удалить";
            this.DelPersonButton.UseVisualStyleBackColor = true;
            // 
            // Administration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 563);
            this.Controls.Add(this.DelPersonButton);
            this.Controls.Add(this.ChangePersonButton);
            this.Controls.Add(this.AddPersonButton);
            this.Controls.Add(this.AdminDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Administration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Администрирование";
            ((System.ComponentModel.ISupportInitialize)(this.AdminDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdminDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AdminDataGridView;
        private System.Data.DataSet AdminDataSet;
        private System.Windows.Forms.Button AddPersonButton;
        private System.Windows.Forms.Button ChangePersonButton;
        private System.Windows.Forms.Button DelPersonButton;
    }
}