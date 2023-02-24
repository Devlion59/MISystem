
namespace MISystem
{
    partial class ElementReferenceWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElementReferenceWindow));
            this.AddNewElement = new System.Windows.Forms.Button();
            this.ChangeElement = new System.Windows.Forms.Button();
            this.DeleteElement = new System.Windows.Forms.Button();
            this.RefreshElement = new System.Windows.Forms.Button();
            this.ElementRefWindowOkButton = new System.Windows.Forms.Button();
            this.ListBoxElementReference = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // AddNewElement
            // 
            this.AddNewElement.Location = new System.Drawing.Point(12, 12);
            this.AddNewElement.Name = "AddNewElement";
            this.AddNewElement.Size = new System.Drawing.Size(75, 23);
            this.AddNewElement.TabIndex = 0;
            this.AddNewElement.Text = "Добавить";
            this.AddNewElement.UseVisualStyleBackColor = true;
            this.AddNewElement.Click += new System.EventHandler(this.AddNewElement_Click);
            // 
            // ChangeElement
            // 
            this.ChangeElement.Location = new System.Drawing.Point(93, 12);
            this.ChangeElement.Name = "ChangeElement";
            this.ChangeElement.Size = new System.Drawing.Size(75, 23);
            this.ChangeElement.TabIndex = 1;
            this.ChangeElement.Text = "Изменить";
            this.ChangeElement.UseVisualStyleBackColor = true;
            this.ChangeElement.Click += new System.EventHandler(this.ChangeElement_Click);
            // 
            // DeleteElement
            // 
            this.DeleteElement.Location = new System.Drawing.Point(174, 12);
            this.DeleteElement.Name = "DeleteElement";
            this.DeleteElement.Size = new System.Drawing.Size(75, 23);
            this.DeleteElement.TabIndex = 2;
            this.DeleteElement.Text = "Удалить";
            this.DeleteElement.UseVisualStyleBackColor = true;
            this.DeleteElement.Click += new System.EventHandler(this.DeleteElement_Click);
            // 
            // RefreshElement
            // 
            this.RefreshElement.Location = new System.Drawing.Point(255, 12);
            this.RefreshElement.Name = "RefreshElement";
            this.RefreshElement.Size = new System.Drawing.Size(75, 23);
            this.RefreshElement.TabIndex = 3;
            this.RefreshElement.Text = "Обновить";
            this.RefreshElement.UseVisualStyleBackColor = true;
            this.RefreshElement.Click += new System.EventHandler(this.RefreshElement_Click);
            // 
            // ElementRefWindowOkButton
            // 
            this.ElementRefWindowOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ElementRefWindowOkButton.Image = global::MISystem.Properties.Resources.OK;
            this.ElementRefWindowOkButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ElementRefWindowOkButton.Location = new System.Drawing.Point(255, 308);
            this.ElementRefWindowOkButton.Name = "ElementRefWindowOkButton";
            this.ElementRefWindowOkButton.Size = new System.Drawing.Size(75, 23);
            this.ElementRefWindowOkButton.TabIndex = 5;
            this.ElementRefWindowOkButton.Text = "OK";
            this.ElementRefWindowOkButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ElementRefWindowOkButton.UseVisualStyleBackColor = true;
            // 
            // ListBoxElementReference
            // 
            this.ListBoxElementReference.FormattingEnabled = true;
            this.ListBoxElementReference.Location = new System.Drawing.Point(12, 41);
            this.ListBoxElementReference.Name = "ListBoxElementReference";
            this.ListBoxElementReference.Size = new System.Drawing.Size(318, 264);
            this.ListBoxElementReference.TabIndex = 7;
            // 
            // ElementReferenceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 347);
            this.Controls.Add(this.ListBoxElementReference);
            this.Controls.Add(this.ElementRefWindowOkButton);
            this.Controls.Add(this.RefreshElement);
            this.Controls.Add(this.DeleteElement);
            this.Controls.Add(this.ChangeElement);
            this.Controls.Add(this.AddNewElement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ElementReferenceWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Справочник";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddNewElement;
        private System.Windows.Forms.Button ChangeElement;
        private System.Windows.Forms.Button DeleteElement;
        private System.Windows.Forms.Button RefreshElement;
        private System.Windows.Forms.Button ElementRefWindowOkButton;
        private System.Windows.Forms.ListBox ListBoxElementReference;
    }
}