
namespace MISystem
{
    partial class VerificationDelWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerificationDelWindow));
            this.VerificationDelLebel = new System.Windows.Forms.Label();
            this.VerificationDelOkButton = new System.Windows.Forms.Button();
            this.VerificationDelCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // VerificationDelLebel
            // 
            this.VerificationDelLebel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VerificationDelLebel.Location = new System.Drawing.Point(59, 34);
            this.VerificationDelLebel.Name = "VerificationDelLebel";
            this.VerificationDelLebel.Size = new System.Drawing.Size(256, 68);
            this.VerificationDelLebel.TabIndex = 0;
            this.VerificationDelLebel.Text = "Текст для пользователя";
            this.VerificationDelLebel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VerificationDelOkButton
            // 
            this.VerificationDelOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.VerificationDelOkButton.Image = global::MISystem.Properties.Resources.OK;
            this.VerificationDelOkButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VerificationDelOkButton.Location = new System.Drawing.Point(59, 144);
            this.VerificationDelOkButton.Name = "VerificationDelOkButton";
            this.VerificationDelOkButton.Size = new System.Drawing.Size(75, 23);
            this.VerificationDelOkButton.TabIndex = 1;
            this.VerificationDelOkButton.Text = "Да";
            this.VerificationDelOkButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.VerificationDelOkButton.UseVisualStyleBackColor = true;
            // 
            // VerificationDelCancelButton
            // 
            this.VerificationDelCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.VerificationDelCancelButton.Image = global::MISystem.Properties.Resources.Cancel;
            this.VerificationDelCancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VerificationDelCancelButton.Location = new System.Drawing.Point(240, 144);
            this.VerificationDelCancelButton.Name = "VerificationDelCancelButton";
            this.VerificationDelCancelButton.Size = new System.Drawing.Size(75, 23);
            this.VerificationDelCancelButton.TabIndex = 2;
            this.VerificationDelCancelButton.Text = "Нет";
            this.VerificationDelCancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.VerificationDelCancelButton.UseVisualStyleBackColor = true;
            // 
            // VerificationDelWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 205);
            this.Controls.Add(this.VerificationDelCancelButton);
            this.Controls.Add(this.VerificationDelOkButton);
            this.Controls.Add(this.VerificationDelLebel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerificationDelWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подтверждение";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label VerificationDelLebel;
        private System.Windows.Forms.Button VerificationDelOkButton;
        private System.Windows.Forms.Button VerificationDelCancelButton;
    }
}