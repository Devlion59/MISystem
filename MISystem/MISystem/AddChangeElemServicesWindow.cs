using MISystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISystem
{
    public partial class AddChangeElemServicesWindow : Form
    {
        public string NewNameOfElem { get; set; }
        public string OldNameOfElem { get; set; }
        public int OldNumberLocationOfMi { get; set; }
        public int NewNumberLocationOfElem { get; set; }
        public int PrFId { get; set; }
                
        private ProductionFacility pf;

        /// <summary>
        /// Конструктор AddChangeElemServicesWindow инициализирует новое окно для добавления элемента
        /// </summary>
        /// <param name="messageForPerson">сообщение для пользователя</param>
        public AddChangeElemServicesWindow(string messageForPerson)
        {
            InitializeComponent();
            AddChangeElemServicesWindowLabel.Text = messageForPerson;
        }

        /// <summary>
        /// Конструктор AddChangeElemServicesWindow инициализирует новое окно для изменения элемента
        /// </summary>
        /// <param name="messageForPerson">сообщение для пользователя</param>
        /// <param name="nameObjectInTreeView">название элемента для изменения</param>
        public AddChangeElemServicesWindow(string messageForPerson, string nameObjectInTreeView)
        {
            InitializeComponent();
            OldNameOfElem = nameObjectInTreeView;
            AddChangeElemServicesWindowLabel.Text = messageForPerson;
            AddChangeElemServicesWindowTextBox.Text = nameObjectInTreeView;
        }

        /// <summary>
        /// Конструктор AddChangeElemServicesWindow инициализирует новое окно для изменения места установки СИ
        /// </summary>
        /// <param name="messageForPerson">сообщение для пользователя</param>
        /// <param name="nameObjectInTreeView">название места установки СИ для изменения</param>
        /// <param name="numberLocationOfMI">номер места установки СИ для изменения</param>
        /// <param name="nameObjectInTreeView">id пр.объекта, где изменяется место установки СИ</param>
        public AddChangeElemServicesWindow(string messageForPerson, string nameObjectInTreeView, int numberLocationOfMI, int prFId)
        {
            InitializeComponent();
            AddChangeElemServicesWindowLabelNumber.Visible = true;
            AddChangeElemServicesWindowNumeric.Visible = true;
            OldNameOfElem = nameObjectInTreeView;
            OldNumberLocationOfMi = numberLocationOfMI;
            PrFId = prFId;
            AddChangeElemServicesWindowLabel.Text = messageForPerson;
            AddChangeElemServicesWindowTextBox.Text = nameObjectInTreeView;
            AddChangeElemServicesWindowNumeric.Value = numberLocationOfMI;
        }

        /// <summary>
        /// Конструктор AddChangeElemServicesWindow инициализирует новое окно для добавления места установки СИ
        /// </summary>
        /// <param name="messageForPerson">сообщение для пользователя</param>
        /// <param name="o">элемент, для которого добавляется новый элемент в иерархию</param>
        public AddChangeElemServicesWindow(string messageForPerson, Element o)
        {
            InitializeComponent();

            if(o is ProductionFacility)
            {
                AddChangeElemServicesWindowLabelNumber.Visible = true;
                AddChangeElemServicesWindowNumeric.Visible = true;
                pf = o as ProductionFacility;
            }
            AddChangeElemServicesWindowLabel.Text = messageForPerson;
        }

        private void AddChangeElemServicesWindowCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Событие AddChangeElemServicesWindowOKButton_Click нажатия кнопки ОК после добавления/изменения информации
        /// </summary>
        private void AddChangeElemServicesWindowOKButton_Click(object sender, EventArgs e)
        {
            NewNameOfElem = AddChangeElemServicesWindowTextBox.Text;
            NewNumberLocationOfElem = (int)AddChangeElemServicesWindowNumeric.Value;
            if (String.IsNullOrWhiteSpace(NewNameOfElem))
            {
                MessageBox.Show("Введите название!");
                this.DialogResult = DialogResult.Cancel;
            }
            if(NewNameOfElem.Equals(OldNameOfElem) && NewNumberLocationOfElem == OldNumberLocationOfMi)
            {
                MessageBox.Show("Вы не поменяли название. Изменения не будут сохранены!");
                this.DialogResult = DialogResult.Cancel;
            }
            if (pf is ProductionFacility)
            {
                var keys = pf.LocationOfMIs.Keys;
                foreach (var k in keys)
                {
                    if (pf.LocationOfMIs[k].NumberLocationOfMI == NewNumberLocationOfElem)
                    {
                        MessageBox.Show("Данное место установки СИ уже существует!");
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    }
                }
            }
            if(PrFId>0 && IsAlreadyExcist(PrFId, NewNumberLocationOfElem) && OldNumberLocationOfMi != NewNumberLocationOfElem)
            {
                MessageBox.Show("Данное место установки СИ уже существует!");
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// Метод IsAlreadyExcist проверки существования номера места установки СИ в БД
        /// </summary>
        /// <param name="prFId">id производственного объекта</param>
        /// <param name="nLofMi">номер места установки СИ</param>
        private bool IsAlreadyExcist(int prFId, int nLofMi)
        {
            bool res = false;
            List<int> lst = new List<int>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdLocOfMi = new SqlCommand($"select numberLocationOfMi from LocationOfMI WHERE productionFacilityId = {prFId}", con);
                var drLocOfMi = cmdLocOfMi.ExecuteReader();
                while (drLocOfMi.Read())
                {
                    int i = (int)drLocOfMi["numberLocationOfMI"];
                    lst.Add(i);
                }
                drLocOfMi.Close();
                con.Close();
            }
            if (lst.Contains(nLofMi))
                res = true;
            return res;
        }
    }
}
