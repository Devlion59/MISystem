
namespace MISystem
{
    partial class AddChangeMeterMIWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddChangeMeterMIWindow));
            this.AddChangeMeterWindowMessenge = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.AddChangeMeterWindowOkButton = new System.Windows.Forms.Button();
            this.AddChangeMeterWindowCancel = new System.Windows.Forms.Button();
            this.AddChangeMeterWindowComboBoxType = new System.Windows.Forms.ComboBox();
            this.AddChangeMeterWindowMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.AddChangeMeterWindowComboBoxAcc = new System.Windows.Forms.ComboBox();
            this.AddChangeMeterWindowSerialN = new System.Windows.Forms.TextBox();
            this.AddChangeMeterWindowDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.AddChangeMeterWindowNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.AddChangeMeterWindowNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // AddChangeMeterWindowMessenge
            // 
            this.AddChangeMeterWindowMessenge.Location = new System.Drawing.Point(23, 26);
            this.AddChangeMeterWindowMessenge.Name = "AddChangeMeterWindowMessenge";
            this.AddChangeMeterWindowMessenge.Size = new System.Drawing.Size(264, 44);
            this.AddChangeMeterWindowMessenge.TabIndex = 0;
            this.AddChangeMeterWindowMessenge.Text = "Текст для пользователя";
            this.AddChangeMeterWindowMessenge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Год выпуска";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Класс точности";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Заводской номер";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Дата поверки*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "МПИ*";
            // 
            // AddChangeMeterWindowOkButton
            // 
            this.AddChangeMeterWindowOkButton.Image = global::MISystem.Properties.Resources.OK;
            this.AddChangeMeterWindowOkButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddChangeMeterWindowOkButton.Location = new System.Drawing.Point(62, 274);
            this.AddChangeMeterWindowOkButton.Name = "AddChangeMeterWindowOkButton";
            this.AddChangeMeterWindowOkButton.Size = new System.Drawing.Size(75, 23);
            this.AddChangeMeterWindowOkButton.TabIndex = 7;
            this.AddChangeMeterWindowOkButton.Text = "ОК";
            this.AddChangeMeterWindowOkButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddChangeMeterWindowOkButton.UseVisualStyleBackColor = true;
            this.AddChangeMeterWindowOkButton.Click += new System.EventHandler(this.AddChangeMeterWindowOkButton_Click);
            // 
            // AddChangeMeterWindowCancel
            // 
            this.AddChangeMeterWindowCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AddChangeMeterWindowCancel.Image = global::MISystem.Properties.Resources.Cancel;
            this.AddChangeMeterWindowCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddChangeMeterWindowCancel.Location = new System.Drawing.Point(199, 274);
            this.AddChangeMeterWindowCancel.Name = "AddChangeMeterWindowCancel";
            this.AddChangeMeterWindowCancel.Size = new System.Drawing.Size(75, 23);
            this.AddChangeMeterWindowCancel.TabIndex = 8;
            this.AddChangeMeterWindowCancel.Text = "Cancel";
            this.AddChangeMeterWindowCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddChangeMeterWindowCancel.UseVisualStyleBackColor = true;
            // 
            // AddChangeMeterWindowComboBoxType
            // 
            this.AddChangeMeterWindowComboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddChangeMeterWindowComboBoxType.FormattingEnabled = true;
            this.AddChangeMeterWindowComboBoxType.Location = new System.Drawing.Point(166, 95);
            this.AddChangeMeterWindowComboBoxType.Name = "AddChangeMeterWindowComboBoxType";
            this.AddChangeMeterWindowComboBoxType.Size = new System.Drawing.Size(121, 21);
            this.AddChangeMeterWindowComboBoxType.TabIndex = 9;
            this.AddChangeMeterWindowComboBoxType.SelectedIndexChanged += new System.EventHandler(this.AddChangeMeterWindowComboBoxType_SelectedIndexChanged);
            // 
            // AddChangeMeterWindowMaskedTextBox
            // 
            this.AddChangeMeterWindowMaskedTextBox.Location = new System.Drawing.Point(166, 122);
            this.AddChangeMeterWindowMaskedTextBox.Mask = "0000";
            this.AddChangeMeterWindowMaskedTextBox.Name = "AddChangeMeterWindowMaskedTextBox";
            this.AddChangeMeterWindowMaskedTextBox.Size = new System.Drawing.Size(121, 20);
            this.AddChangeMeterWindowMaskedTextBox.TabIndex = 10;
            // 
            // AddChangeMeterWindowComboBoxAcc
            // 
            this.AddChangeMeterWindowComboBoxAcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AddChangeMeterWindowComboBoxAcc.FormattingEnabled = true;
            this.AddChangeMeterWindowComboBoxAcc.Items.AddRange(new object[] {
            "-",
            "0,2",
            "0,2S",
            "0,5",
            "0,5S",
            "1,0",
            "2,0"});
            this.AddChangeMeterWindowComboBoxAcc.Location = new System.Drawing.Point(166, 148);
            this.AddChangeMeterWindowComboBoxAcc.Name = "AddChangeMeterWindowComboBoxAcc";
            this.AddChangeMeterWindowComboBoxAcc.Size = new System.Drawing.Size(121, 21);
            this.AddChangeMeterWindowComboBoxAcc.TabIndex = 11;
            // 
            // AddChangeMeterWindowSerialN
            // 
            this.AddChangeMeterWindowSerialN.Location = new System.Drawing.Point(166, 175);
            this.AddChangeMeterWindowSerialN.Name = "AddChangeMeterWindowSerialN";
            this.AddChangeMeterWindowSerialN.Size = new System.Drawing.Size(121, 20);
            this.AddChangeMeterWindowSerialN.TabIndex = 12;
            // 
            // AddChangeMeterWindowDateTimePicker
            // 
            this.AddChangeMeterWindowDateTimePicker.Location = new System.Drawing.Point(166, 201);
            this.AddChangeMeterWindowDateTimePicker.Name = "AddChangeMeterWindowDateTimePicker";
            this.AddChangeMeterWindowDateTimePicker.Size = new System.Drawing.Size(121, 20);
            this.AddChangeMeterWindowDateTimePicker.TabIndex = 13;
            // 
            // AddChangeMeterWindowNumeric
            // 
            this.AddChangeMeterWindowNumeric.Location = new System.Drawing.Point(166, 229);
            this.AddChangeMeterWindowNumeric.Name = "AddChangeMeterWindowNumeric";
            this.AddChangeMeterWindowNumeric.Size = new System.Drawing.Size(121, 20);
            this.AddChangeMeterWindowNumeric.TabIndex = 14;
            // 
            // AddChangeMeterMIWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 318);
            this.Controls.Add(this.AddChangeMeterWindowNumeric);
            this.Controls.Add(this.AddChangeMeterWindowDateTimePicker);
            this.Controls.Add(this.AddChangeMeterWindowSerialN);
            this.Controls.Add(this.AddChangeMeterWindowComboBoxAcc);
            this.Controls.Add(this.AddChangeMeterWindowMaskedTextBox);
            this.Controls.Add(this.AddChangeMeterWindowComboBoxType);
            this.Controls.Add(this.AddChangeMeterWindowCancel);
            this.Controls.Add(this.AddChangeMeterWindowOkButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AddChangeMeterWindowMessenge);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddChangeMeterMIWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление/изменение счетчика";
            ((System.ComponentModel.ISupportInitialize)(this.AddChangeMeterWindowNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AddChangeMeterWindowMessenge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button AddChangeMeterWindowOkButton;
        private System.Windows.Forms.Button AddChangeMeterWindowCancel;
        private System.Windows.Forms.ComboBox AddChangeMeterWindowComboBoxType;
        private System.Windows.Forms.MaskedTextBox AddChangeMeterWindowMaskedTextBox;
        private System.Windows.Forms.ComboBox AddChangeMeterWindowComboBoxAcc;
        private System.Windows.Forms.TextBox AddChangeMeterWindowSerialN;
        private System.Windows.Forms.DateTimePicker AddChangeMeterWindowDateTimePicker;
        private System.Windows.Forms.NumericUpDown AddChangeMeterWindowNumeric;
    }
}