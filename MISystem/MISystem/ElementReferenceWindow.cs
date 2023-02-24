using MISystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISystem
{
    public partial class ElementReferenceWindow : Form
    {
        public int IdTypeOfMIFromRef { get; set; }
        public string TypeOfMiForMessenge { get; set; }
        private bool changeElem;
        public bool ChangeElem
        {
            get
            {
                return changeElem;
            }
            set
            {
                changeElem = value;
            }
        }

        private Dictionary<int, MeasInstrFromRef> dictMIFromRef = new Dictionary<int, MeasInstrFromRef>();

        /// <summary>
        /// Конструктор ElementReferenceWindow инициализирует окно справочника
        /// для определенного вида СИ
        /// </summary>
        /// <param name="idTypeOfMIFromRef">id вида СИ</param>
        public ElementReferenceWindow(int idTypeOfMIFromRef)
        {
            ChangeElem = false;
            IdTypeOfMIFromRef = idTypeOfMIFromRef;
            SetTypeOfMiForMessenge(IdTypeOfMIFromRef);
            InitializeComponent();
            RefreshListElem();
        }

        /// <summary>
        /// Событие AddNewElement_Click нажатия кнопки добавить 
        /// инициирует новое окно для ввода информации о типе СИ
        /// и направляет запрос в БД на создание записи о типе СИ
        /// </summary>
        private void AddNewElement_Click(object sender, EventArgs e)
        {
            AddChangeRefMIWindow addChMIWin = new AddChangeRefMIWindow($"Введите название {TypeOfMiForMessenge}");
            addChMIWin.Text = $"Добавление {TypeOfMiForMessenge}";
            addChMIWin.ShowDialog();
            if(addChMIWin.DialogResult==DialogResult.OK)
            {
                Tools.AddRefElem(IdTypeOfMIFromRef, addChMIWin.NewNameOfMiInRef);
                RefreshListElem();
            }
        }

        /// <summary>
        /// Событие ChangeElement_Click нажатия кнопки изменить 
        /// инициирует новое окно для изменения информации о выбранном типе СИ
        /// и направляет запрос в БД на изменение записи о выбранном типе СИ
        /// </summary>
        private void ChangeElement_Click(object sender, EventArgs e)
        {
            if(ListBoxElementReference.SelectedItems.Count != 0)
            {
                int idMi = ((MeasInstrFromRef)ListBoxElementReference.SelectedItem).Id;
                string nameMI = ((MeasInstrFromRef)ListBoxElementReference.SelectedItem).NameMIFromRef;
                AddChangeRefMIWindow addChMIWin = new AddChangeRefMIWindow($"Измените название {TypeOfMiForMessenge}",nameMI);
                addChMIWin.Text = $"Изменение названия {TypeOfMiForMessenge}";
                addChMIWin.ShowDialog();
                if (addChMIWin.DialogResult == DialogResult.OK)
                {
                    Tools.ChangeRefElem(idMi, addChMIWin.NewNameOfMiInRef);
                    ChangeElem = true;
                    dictMIFromRef[idMi].NameMIFromRef = addChMIWin.NewNameOfMiInRef;
                    ListBoxElementReference.DataSource = dictMIFromRef.Values.ToList<MeasInstrFromRef>();
                    ListBoxElementReference.DisplayMember = "NameMIFromRef";
                    ListBoxElementReference.ValueMember = "Id";
                }
            } 
            else
                MessageBox.Show("Добавьте тип СИ!");
        }

        /// <summary>
        /// Событие DeleteElement_Click нажатия кнопки удалить 
        /// проверяет используется ли выбранный тип СИ в качестве информации о СИ и в истории,
        /// если нет, то инициирует новое окно для подтверждения удаления информации о выбранном типе СИ
        /// и направляет запрос в БД на удаление записи о выбранном типе СИ,
        /// если тип СИ используется в БД и в истории, то предупреждает что удалить невозможно
        /// </summary>
        private void DeleteElement_Click(object sender, EventArgs e)
        {
            if (ListBoxElementReference.SelectedItems.Count != 0)
            {
                int idMi = ((MeasInstrFromRef)ListBoxElementReference.SelectedItem).Id;
                string nameMI = ((MeasInstrFromRef)ListBoxElementReference.SelectedItem).NameMIFromRef;
                if (Tools.GetCountMIWithRefId(idMi) == 0 && Tools.GetCountMiHistoryWithRefId(idMi) == 0)
                {
                    VerificationDelWindow verificationMIWindow = new VerificationDelWindow(nameMI, IdTypeOfMIFromRef);
                    verificationMIWindow.Text = "Подтверждение";
                    verificationMIWindow.ShowDialog();
                    if (verificationMIWindow.DialogResult == DialogResult.OK)
                    {
                        Tools.DeleteRefElem(idMi);
                        dictMIFromRef.Remove(idMi);
                        ListBoxElementReference.DataSource = dictMIFromRef.Values.ToList<MeasInstrFromRef>();
                        ListBoxElementReference.DisplayMember = "NameMIFromRef";
                        ListBoxElementReference.ValueMember = "Id";
                    }
                }
                else if (Tools.GetCountMIWithRefId(idMi) != 0 && Tools.GetCountMiHistoryWithRefId(idMi) == 0)
                    MessageBox.Show("Данный тип используется в базе данных!");
                else if(Tools.GetCountMiHistoryWithRefId(idMi) != 0 && Tools.GetCountMIWithRefId(idMi) == 0)
                    MessageBox.Show("Данный тип используется в истории изменений!");
                else if(Tools.GetCountMIWithRefId(idMi) != 0 && Tools.GetCountMiHistoryWithRefId(idMi) != 0)
                    MessageBox.Show("Данный тип используется в базе данных и истории изменений!");
            }
            else
                MessageBox.Show("Добавьте тип СИ!");
        }

        /// <summary>
        /// Событие RefreshListElem нажатия кнопки обновить 
        /// обновляет перечень типов СИ
        /// </summary>
        private void RefreshListElem()
        {
            dictMIFromRef.Clear();
            Tools.RefreshRefElem(dictMIFromRef, IdTypeOfMIFromRef);
            ListBoxElementReference.DataSource = dictMIFromRef.Values.ToList<MeasInstrFromRef>();
            ListBoxElementReference.DisplayMember = "NameMIFromRef";
            ListBoxElementReference.ValueMember = "Id";
        }

        /// <summary>
        /// Метод SetTypeOfMiForMessenge определяет вид СИ
        /// </summary>
        /// <param name="idTypeOfMIFromRef">id вида СИ</param>
        private void SetTypeOfMiForMessenge(int idTypeOfMIFromRef)
        {
            switch (idTypeOfMIFromRef)
            {
                case 1:
                    TypeOfMiForMessenge = "трансформатора тока";
                    break;
                case 2:
                    TypeOfMiForMessenge = "трансформатора напряжения";
                    break;
                case 3:
                    TypeOfMiForMessenge = "счетчика";
                    break;
            }
        }

        private void RefreshElement_Click(object sender, EventArgs e)
        {
            RefreshListElem();
        }
    }
}
