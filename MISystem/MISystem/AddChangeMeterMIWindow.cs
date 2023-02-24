using MISystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISystem
{
    public partial class AddChangeMeterMIWindow : Form
    {
        private Dictionary<int, MeasInstrFromRef> dictMet;
        public int IdSelectedTypeMet { get; set; }
        public MeasurInstr MeasurInstr { get; set; }
        public int LocationOfMiId { get; set; }
        private bool changeMode = false;

        /// <summary>
        /// Конструктор AddChangeMeterMIWindow инициализирует окно с компонентами ввода-вывода
        /// для добавления СИ счетчика
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="temp">словарь типов СИ</param>
        /// <param name="locationOfMiId">id места установки СИ</param>
        /// <param name="newMI">экземпляр нового СИ</param>
        public AddChangeMeterMIWindow(string message, Dictionary<int, MeasInstrFromRef> temp, int locationOfMiId, MeasurInstr newMI)
        {
            MeasurInstr = newMI;
            LocationOfMiId = locationOfMiId;
            dictMet = temp;
            InitializeComponent();
            AddChangeMeterWindowMessenge.Text = message;
            AddComboBoxMeter();
            AddChangeMeterWindowComboBoxType.SelectedIndexChanged += AddChangeMeterWindowComboBoxType_SelectedIndexChanged;
        }

        /// <summary>
        /// Конструктор AddChangeMeterMIWindow инициализирует окно с компонентами ввода-вывода
        /// для изменения СИ счетчика
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="temp">словарь типов СИ</param>
        /// <param name="newMI">экземпляр СИ для изменения</param>
        public AddChangeMeterMIWindow(string message, Dictionary<int, MeasInstrFromRef> temp, MeasurInstr mI)
        {
            changeMode = true;
            MeasurInstr = mI;
            LocationOfMiId = mI.LocationOfMiId;
            dictMet = temp;
            IdSelectedTypeMet = mI.MiFromRefId;
            InitializeComponent();
            AddComboBoxMeter();
            AddChangeMeterWindowMessenge.Text = message;
            AddComboBoxMeter(dictMet[MeasurInstr.MiFromRefId], MeasurInstr.AccuracyClass);
            AddChangeMeterWindowMaskedTextBox.Text = MeasurInstr.YearOfManufacture;
            AddChangeMeterWindowSerialN.Text = MeasurInstr.SerialNumber;
            AddChangeMeterWindowDateTimePicker.Value = MeasurInstr.VerificationDate;
            AddChangeMeterWindowNumeric.Value = MeasurInstr.VerificationInterval;
            AddChangeMeterWindowComboBoxType.SelectedIndexChanged += AddChangeMeterWindowComboBoxType_SelectedIndexChanged;
        }

        /// <summary>
        /// Метод AddComboBoxMeter заполняет комбобоксы типами СИ и классами точности
        /// и устанавливает в качестве выбранного первый тип СИ в комбобоксе и класс точности
        /// </summary>
        private void AddComboBoxMeter()
        {
            AddChangeMeterWindowComboBoxType.DataSource = dictMet.Values.ToList<MeasInstrFromRef>();
            AddChangeMeterWindowComboBoxType.DisplayMember = "NameMIFromRef";
            AddChangeMeterWindowComboBoxType.ValueMember = "Id";
            IdSelectedTypeMet = ((MeasInstrFromRef)AddChangeMeterWindowComboBoxType.SelectedItem).Id;
            AddChangeMeterWindowComboBoxAcc.SelectedItem = AddChangeMeterWindowComboBoxAcc.Items[0];
        }

        /// <summary>
        /// Метод AddComboBoxMeter заполняет комбобоксы типами СИ и классами точности
        /// и устанавливает в качестве выбранного переданный тип СИ в комбобоксе и переданный класс точности
        /// </summary>
        /// <param name="nameMIFromRef">название типа СИ</param>
        /// <param name="accClass">класс точности</param>
        private void AddComboBoxMeter(MeasInstrFromRef miFromRef, string accClass)
        {
            AddChangeMeterWindowComboBoxType.DataSource = dictMet.Values.ToList<MeasInstrFromRef>();
            AddChangeMeterWindowComboBoxType.DisplayMember = "NameMIFromRef";
            AddChangeMeterWindowComboBoxType.ValueMember = "Id";
            AddChangeMeterWindowComboBoxType.SelectedItem = miFromRef;
            IdSelectedTypeMet = MeasurInstr.MiFromRefId;
            string accClassSelect = "";
            if (String.IsNullOrEmpty(accClass) || String.IsNullOrWhiteSpace(accClass))
                accClassSelect = "-";
            else
                accClassSelect = MeasurInstr.AccuracyClass;
            AddChangeMeterWindowComboBoxAcc.SelectedItem = AddChangeMeterWindowComboBoxAcc.Items[AddChangeMeterWindowComboBoxAcc.Items.IndexOf(accClassSelect)];
        }

        /// <summary>
        /// Событие AddChangeMeterWindowComboBoxType_SelectedIndexChanged запоминает id выбранного типа СИ
        /// </summary>
        private void AddChangeMeterWindowComboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdSelectedTypeMet = ((MeasInstrFromRef)AddChangeMeterWindowComboBoxType.SelectedItem).Id;
        }

        /// <summary>
        /// Метод VerificationNumCompletedSuccMeter проверяет указанный МПИ СИ,
        /// он должен быть больше нуля
        /// </summary>
        private bool VerificationNumCompletedSuccMeter()
        {
            bool res = false;
            if (AddChangeMeterWindowNumeric.Value != 0)
                res = true;
            return res;
        }

        /// <summary>
        /// Метод VerificationYearCompletedSuccMeter проверяет указанный год выпуска СИ,
        /// он должен быть не указан
        /// </summary>
        private bool VerificationYearCompletedSuccMeter()
        {
            bool res = false;
            if (String.IsNullOrEmpty(AddChangeMeterWindowMaskedTextBox.Text) || String.IsNullOrWhiteSpace(AddChangeMeterWindowMaskedTextBox.Text))
                res = true;
            return res;
        }

        /// <summary>
        /// Событие AddChangeMeterWindowOkButton_Click заполняет поля экземпляра СИ
        /// и при изменении инфорации о СИ направляет запрос в БД на добавление
        /// записи в историю изменений информации о СИ
        /// </summary>
        private void AddChangeMeterWindowOkButton_Click(object sender, EventArgs e)
        {
            if (VerificationNumCompletedSuccMeter() && (VerificationYearCompletedSuccMeter() || Convert.ToInt32(AddChangeMeterWindowMaskedTextBox.Text)>1000))
            {
                if (changeMode && IsChanged())
                    Tools.AddLog(MeasurInstr, "Изменен");
                MeasurInstr.PhaseId = 8;
                MeasurInstr.MiFromRefId = IdSelectedTypeMet;
                MeasurInstr.LocationOfMiId = LocationOfMiId;

                if (VerificationYearCompletedSuccMeter())
                    MeasurInstr.YearOfManufacture = "";
                else
                    MeasurInstr.YearOfManufacture = (AddChangeMeterWindowMaskedTextBox.Text);

                if (AddChangeMeterWindowComboBoxAcc.SelectedItem == AddChangeMeterWindowComboBoxAcc.Items[0])
                    MeasurInstr.AccuracyClass = "";
                else
                    MeasurInstr.AccuracyClass = AddChangeMeterWindowComboBoxAcc.SelectedItem.ToString();

                if (String.IsNullOrEmpty(AddChangeMeterWindowSerialN.Text) || String.IsNullOrWhiteSpace(AddChangeMeterWindowSerialN.Text))
                    MeasurInstr.SerialNumber = "";
                else
                    MeasurInstr.SerialNumber = AddChangeMeterWindowSerialN.Text;

                MeasurInstr.VerificationDate = AddChangeMeterWindowDateTimePicker.Value;

                MeasurInstr.VerificationInterval = (int)AddChangeMeterWindowNumeric.Value;

                MeasurInstr.VerificationNextDate = AddChangeMeterWindowDateTimePicker.Value.AddYears((int)AddChangeMeterWindowNumeric.Value);

                this.DialogResult = DialogResult.OK;
            }
            else if (!VerificationYearCompletedSuccMeter() && Convert.ToInt32(AddChangeMeterWindowMaskedTextBox.Text) < 1000 && Convert.ToInt32(AddChangeMeterWindowMaskedTextBox.Text)>0)
                MessageBox.Show("Заполните корректно Год выпуска или оставьте его пустым!");
            else if(!VerificationNumCompletedSuccMeter())
                MessageBox.Show("МПИ должен быть больше 0!");
        }

        /// <summary>
        /// Метод IsChanged проверяет что информация о СИ изменена
        /// и только после этой проверки делается запись в историю с изменением
        /// </summary>
        private bool IsChanged()
        {
            bool result = false;
            if (MeasurInstr.MiFromRefId != IdSelectedTypeMet || !MeasurInstr.YearOfManufacture.Equals(AddChangeMeterWindowMaskedTextBox.Text) ||
                !((MeasurInstr.AccuracyClass.Equals("") && AddChangeMeterWindowComboBoxAcc.SelectedItem == AddChangeMeterWindowComboBoxAcc.Items[0]) || MeasurInstr.AccuracyClass.Equals(AddChangeMeterWindowComboBoxAcc.SelectedItem.ToString())) || 
                !MeasurInstr.SerialNumber.Equals(AddChangeMeterWindowSerialN.Text) ||
                MeasurInstr.VerificationDate != AddChangeMeterWindowDateTimePicker.Value || MeasurInstr.VerificationInterval != (int)AddChangeMeterWindowNumeric.Value)
                result = true;  
            return result;
        }
    }
}
