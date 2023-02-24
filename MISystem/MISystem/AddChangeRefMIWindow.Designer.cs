
namespace MISystem
{
    partial class AddChangeRefMIWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddChangeRefMIWindow));
            this.AddChangeMIWindowOKButton = new System.Windows.Forms.Button();
            this.AddChangeMIWindowCancelButton = new System.Windows.Forms.Button();
            this.AddChangeMIWindowTextBox = new System.Windows.Forms.TextBox();
            this.AddChangeWindowLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddChangeMIWindowOKButton
            // 
            this.AddChangeMIWindowOKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddChangeMIWindowOKButton.Image = global::MISystem.Properties.Resources.OK;
            this.AddChangeMIWindowOKButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddChangeMIWindowOKButton.Location = new System.Drawing.Point(63, 134);
            this.AddChangeMIWindowOKButton.Name = "AddChangeMIWindowOKButton";
            this.AddChangeMIWindowOKButton.Size = new System.Drawing.Size(75, 23);
            this.AddChangeMIWindowOKButton.TabIndex = 0;
            this.AddChangeMIWindowOKButton.Text = "OK";
            this.AddChangeMIWindowOKButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddChangeMIWindowOKButton.UseVisualStyleBackColor = true;
            this.AddChangeMIWindowOKButton.Click += new System.EventHandler(this.AddChangeMIWindowOKButton_Click);
            // 
            // AddChangeMIWindowCancelButton
            // 
            this.AddChangeMIWindowCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AddChangeMIWindowCancelButton.Image = global::MISystem.Properties.Resources.Cancel;
            this.AddChangeMIWindowCancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddChangeMIWindowCancelButton.Location = new System.Drawing.Point(166, 134);
            this.AddChangeMIWindowCancelButton.Name = "AddChangeMIWindowCancelButton";
            this.AddChangeMIWindowCancelButton.Size = new System.Drawing.Size(75, 23);
            this.AddChangeMIWindowCancelButton.TabIndex = 1;
            this.AddChangeMIWindowCancelButton.Text = "Cancel";
            this.AddChangeMIWindowCancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddChangeMIWindowCancelButton.UseVisualStyleBackColor = true;
            this.AddChangeMIWindowCancelButton.Click += new System.EventHandler(this.AddChangeMIWindowCancelButton_Click);
            // 
            // AddChangeMIWindowTextBox
            // 
            this.AddChangeMIWindowTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddChangeMIWindowTextBox.Location = new System.Drawing.Point(63, 89);
            this.AddChangeMIWindowTextBox.Name = "AddChangeMIWindowTextBox";
            this.AddChangeMIWindowTextBox.Size = new System.Drawing.Size(178, 20);
            this.AddChangeMIWindowTextBox.TabIndex = 2;
            // 
            // AddChangeWindowLabel
            // 
            this.AddChangeWindowLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddChangeWindowLabel.Location = new System.Drawing.Point(63, 33);
            this.AddChangeWindowLabel.Name = "AddChangeWindowLabel";
            this.AddChangeWindowLabel.Size = new System.Drawing.Size(178, 44);
            this.AddChangeWindowLabel.TabIndex = 3;
            this.AddChangeWindowLabel.Text = "Текст для пользователя";
            this.AddChangeWindowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddChangeRefMIWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 191);
            this.Controls.Add(this.AddChangeWindowLabel);
            this.Controls.Add(this.AddChangeMIWindowTextBox);
            this.Controls.Add(this.AddChangeMIWindowCancelButton);
            this.Controls.Add(this.AddChangeMIWindowOKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddChangeRefMIWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление/изменение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddChangeMIWindowOKButton;
        private System.Windows.Forms.Button AddChangeMIWindowCancelButton;
        private System.Windows.Forms.TextBox AddChangeMIWindowTextBox;
        private System.Windows.Forms.Label AddChangeWindowLabel;
    }
}