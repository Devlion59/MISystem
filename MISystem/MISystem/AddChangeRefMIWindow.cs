using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MISystem
{
    public partial class AddChangeRefMIWindow : Form
    {
        public string NewNameOfMiInRef { get; set; }
        public string OldNameOfMiInRef { get; set; }

        /// <summary>
        /// Конструктор AddChangeRefMIWindow инициализирует новое окно для ввода названия типа СИ
        /// </summary>
        /// <param name="messageForPerson">собщение для пользователя</param>
        public AddChangeRefMIWindow(string messageForPerson)
        {
            InitializeComponent();
            AddChangeWindowLabel.Text = messageForPerson;
        }

        /// <summary>
        /// Конструктор AddChangeRefMIWindow инициализирует новое окно для изменения названия типа СИ
        /// </summary>
        /// <param name="messageForPerson">собщение для пользователя</param>
        /// <param name="nameMiPressInList">название существущего типа СИ для изменения</param>
        public AddChangeRefMIWindow(string messageForPerson, string nameMiPressInList)
        {
            InitializeComponent();
            AddChangeWindowLabel.Text = messageForPerson;
            OldNameOfMiInRef = nameMiPressInList;
            AddChangeMIWindowTextBox.Text = nameMiPressInList;
        }

        public void AddChangeMIWindowCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Событие AddChangeMIWindowOKButton_Click нажатия кнопки ок после ввода названия типа
        /// система проверяет что название введено, что оно отлично от того, что было ранее при изменении
        /// </summary>
        private void AddChangeMIWindowOKButton_Click(object sender, EventArgs e)
        {
            NewNameOfMiInRef = AddChangeMIWindowTextBox.Text;
            if (String.IsNullOrWhiteSpace(NewNameOfMiInRef))
            {
                MessageBox.Show("Введите название средства измерения!");
                this.DialogResult = DialogResult.Cancel;
            }
            if (NewNameOfMiInRef.Equals(OldNameOfMiInRef))
            {
                MessageBox.Show("Вы не поменяли название СИ. Изменения не будут сохранены!");
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
