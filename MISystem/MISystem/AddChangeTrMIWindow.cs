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
    public partial class AddChangeTrMIWindow : Form
    {
        private Dictionary<int, MeasInstrFromRef> dictTr;
        public int IdSelectedTypeTr { get; set; }
        public MeasurInstr MeasurInstr { get; set; }
        public int LocationOfMiId { get; set; }
        public int PhaseId { get; set; }
        private bool changeMode = false;

        /// <summary>
        /// Конструктор AddChangeTrMIWindow инициализирует окно с компонентами ввода-вывода
        /// для добавления СИ ТТ или ТН
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="temp">словарь типов СИ</param>
        /// <param name="locationOfMiId">id места установки СИ</param>
        /// <param name="phaseId">id фазы</param>
        /// <param name="newMI">экземпляр нового СИ</param>
        public AddChangeTrMIWindow(string message, Dictionary<int, MeasInstrFromRef> temp, int locationOfMiId, int phaseId, MeasurInstr newMI)
        {
            MeasurInstr = newMI;
            LocationOfMiId = locationOfMiId;
            dictTr = temp;
            PhaseId = phaseId;
            InitializeComponent();
            AddComboBox();
            AddChangeTrWindowLabelMess.Text = message;
            AddChTrTypeTrComboBox.SelectedIndexChanged += AddChTrTypeTrComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Конструктор AddChangeTrMIWindow инициализирует окно с компонентами ввода-вывода
        /// для изменения СИ ТТ или ТН
        /// </summary>
        /// <param name="message">сообщение для пользователя</param>
        /// <param name="temp">словарь типов СИ</param>
        /// <param name="newMI">экземпляр СИ для изменения</param>
        public AddChangeTrMIWindow(string message, Dictionary<int, MeasInstrFromRef> temp, MeasurInstr mI)
        {
            changeMode = true;
            MeasurInstr = mI;
            LocationOfMiId = mI.LocationOfMiId;
            dictTr = temp;
            PhaseId = mI.PhaseId;
            IdSelectedTypeTr = mI.MiFromRefId;
            InitializeComponent();
            AddComboBox(dictTr[MeasurInstr.MiFromRefId], MeasurInstr.AccuracyClass);
            AddChangeTrWindowLabelMess.Text = message;
            AddChTrYearOfManufTrMaskedTextBox.Text = MeasurInstr.YearOfManufacture;
            AddChTrSerialNumbTrTextBox.Text = MeasurInstr.SerialNumber;
            if (MeasurInstr.CoefficientUp.Equals(""))
                AddChTrCoeff1TrNumeric.Value = 0;
            else 
                AddChTrCoeff1TrNumeric.Value = Convert.ToInt32(MeasurInstr.CoefficientUp);
            if (MeasurInstr.CoefficientDown.Equals(""))
                AddChTrCoeff2TrNumeric.Value = 0;
            else
                AddChTrCoeff2TrNumeric.Value = Convert.ToInt32(MeasurInstr.CoefficientDown);
            AddChTrVerifDateTrDateTimePicker.Value = MeasurInstr.VerificationDate;
            AddChTrVerifIntervTrNumeric.Value = MeasurInstr.VerificationInterval;
            AddChTrTypeTrComboBox.SelectedIndexChanged += AddChTrTypeTrComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Событие AddChTrTypeTrComboBox_SelectedIndexChanged запоминает id выбранного типа СИ
        /// </summary>
        void AddChTrTypeTrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdSelectedTypeTr = ((MeasInstrFromRef)AddChTrTypeTrComboBox.SelectedItem).Id;
        }

        /// <summary>
        /// Метод AddComboBox заполняет комбобоксы типами СИ и классами точности
        /// и устанавливает в качестве выбранного первый тип СИ в комбобоксе и класс точности
        /// </summary>
        private void AddComboBox()
        {
            AddChTrTypeTrComboBox.DataSource = dictTr.Values.ToList<MeasInstrFromRef>();
            AddChTrTypeTrComboBox.DisplayMember = "NameMIFromRef";
            AddChTrTypeTrComboBox.ValueMember = "Id";
            IdSelectedTypeTr = ((MeasInstrFromRef)AddChTrTypeTrComboBox.SelectedItem).Id;
            AddChTrTypeTrComboBox.SelectedItem = AddChTrTypeTrComboBox.Items[0];
            AddChTrAccClassTrComboBox.SelectedItem = AddChTrAccClassTrComboBox.Items[0];
        }

        /// <summary>
        /// Метод AddComboBox заполняет комбобоксы типами СИ и классами точности
        /// и устанавливает в качестве выбранного переданный тип СИ в комбобоксе и переданный класс точности
        /// </summary>
        /// <param name="nameMIFromRef">название типа СИ</param>
        /// <param name="accClass">класс точности</param>
        private void AddComboBox(MeasInstrFromRef miFromRef, string accClass)
        {
            AddChTrTypeTrComboBox.DataSource = dictTr.Values.ToList<MeasInstrFromRef>();
            AddChTrTypeTrComboBox.DisplayMember = "NameMIFromRef";
            AddChTrTypeTrComboBox.ValueMember = "Id";
            AddChTrTypeTrComboBox.SelectedItem = miFromRef;
            IdSelectedTypeTr = MeasurInstr.MiFromRefId;
            string accClassSelect = "";
            if (String.IsNullOrEmpty(accClass) || String.IsNullOrWhiteSpace(accClass))
                accClassSelect = "-";
            else
                accClassSelect = MeasurInstr.AccuracyClass;
            AddChTrAccClassTrComboBox.SelectedItem = AddChTrAccClassTrComboBox.Items[AddChTrAccClassTrComboBox.Items.IndexOf(accClassSelect)];
        }

        /// <summary>
        /// Событие AddChTrOkButton_Click заполняет поля экземпляра СИ
        /// и при изменении инфорации о СИ направляет запрос в БД на добавление
        /// записи в историю изменений информации о СИ
        /// </summary>
        private void AddChTrOkButton_Click(object sender, EventArgs e)
        {
            if (VerificationNumCompletedSucc() && CoeffIsSucc() && (VerificationYearCompletedSucc() || Convert.ToInt32(AddChTrYearOfManufTrMaskedTextBox.Text) >= 1900))
            {
                if(changeMode && IsChanged())
                    Tools.AddLog(MeasurInstr, "Изменен");
                MeasurInstr.PhaseId = PhaseId;
                MeasurInstr.MiFromRefId = IdSelectedTypeTr;
                MeasurInstr.LocationOfMiId = LocationOfMiId;
                if (VerificationYearCompletedSucc())
                    MeasurInstr.YearOfManufacture = "";
                else
                    MeasurInstr.YearOfManufacture = (AddChTrYearOfManufTrMaskedTextBox.Text);
                
                if (AddChTrAccClassTrComboBox.SelectedItem == AddChTrAccClassTrComboBox.Items[0])
                    MeasurInstr.AccuracyClass = "";
                else
                    MeasurInstr.AccuracyClass = AddChTrAccClassTrComboBox.SelectedItem.ToString();

                if (String.IsNullOrEmpty(AddChTrSerialNumbTrTextBox.Text) || String.IsNullOrWhiteSpace(AddChTrSerialNumbTrTextBox.Text))
                    MeasurInstr.SerialNumber = "";
                else
                    MeasurInstr.SerialNumber = AddChTrSerialNumbTrTextBox.Text;

                if (AddChTrCoeff1TrNumeric.Value == 0 && AddChTrCoeff2TrNumeric.Value == 0)
                {
                    MeasurInstr.CoefficientUp = "";
                    MeasurInstr.CoefficientDown = "";
                }
                else if (AddChTrCoeff1TrNumeric.Value != 0 && AddChTrCoeff2TrNumeric.Value != 0)
                {
                    MeasurInstr.CoefficientUp = AddChTrCoeff1TrNumeric.Value.ToString();
                    MeasurInstr.CoefficientDown = AddChTrCoeff2TrNumeric.Value.ToString();
                }
                MeasurInstr.VerificationDate = AddChTrVerifDateTrDateTimePicker.Value;
                MeasurInstr.VerificationInterval = (int)AddChTrVerifIntervTrNumeric.Value;
                MeasurInstr.VerificationNextDate = AddChTrVerifDateTrDateTimePicker.Value.AddYears((int)AddChTrVerifIntervTrNumeric.Value);
                this.DialogResult = DialogResult.OK;
            }
            else if (!VerificationYearCompletedSucc() && Convert.ToInt32(AddChTrYearOfManufTrMaskedTextBox.Text)<1900 && Convert.ToInt32(AddChTrYearOfManufTrMaskedTextBox.Text)>0)
                MessageBox.Show("Заполните корректно Год выпуска или оставьте его пустым!");
            else if (!VerificationNumCompletedSucc())
                MessageBox.Show("МПИ должен быть больше 0!");
            else if (!CoeffIsSucc())
                MessageBox.Show("Скорректируйте коэффициенты трансформации!");
        }

        /// <summary>
        /// Метод IsChanged проверяет что информация о СИ изменена
        /// и только после этой проверки делается запись в историю с изменением
        /// </summary>
        private bool IsChanged()
        {
            bool result = false;
            if (MeasurInstr.MiFromRefId != IdSelectedTypeTr || !MeasurInstr.YearOfManufacture.Equals(AddChTrYearOfManufTrMaskedTextBox.Text) ||
                !((MeasurInstr.AccuracyClass.Equals("") && AddChTrAccClassTrComboBox.SelectedItem == AddChTrAccClassTrComboBox.Items[0]) || MeasurInstr.AccuracyClass.Equals(AddChTrAccClassTrComboBox.SelectedItem.ToString())) ||
                !MeasurInstr.SerialNumber.Equals(AddChTrSerialNumbTrTextBox.Text) || 
                !((MeasurInstr.CoefficientUp.Equals("") && AddChTrCoeff1TrNumeric.Value == 0) || MeasurInstr.CoefficientUp.Equals(AddChTrCoeff1TrNumeric.Value.ToString())) || 
                !((MeasurInstr.CoefficientDown.Equals("") && AddChTrCoeff2TrNumeric.Value == 0) || MeasurInstr.CoefficientDown.Equals(AddChTrCoeff2TrNumeric.Value.ToString())) || 
                MeasurInstr.VerificationDate != AddChTrVerifDateTrDateTimePicker.Value || MeasurInstr.VerificationInterval != (int)AddChTrVerifIntervTrNumeric.Value)
                result = true;
            return result;
        }

        /// <summary>
        /// Метод VerificationNumCompletedSucc проверяет указанный МПИ СИ,
        /// он должен быть больше нуля
        /// </summary>
        private bool VerificationNumCompletedSucc()
        {
            bool res = false;
            if (AddChTrVerifIntervTrNumeric.Value != 0)
                res = true;
            return res;
        }

        /// <summary>
        /// Метод VerificationYearCompletedSucc проверяет указанный год выпуска СИ,
        /// если он не указан или указаны пробелы, то проверка пройдена
        /// </summary>
        private bool VerificationYearCompletedSucc()
        {
            bool res = false;
            if (String.IsNullOrEmpty(AddChTrYearOfManufTrMaskedTextBox.Text) || String.IsNullOrWhiteSpace(AddChTrYearOfManufTrMaskedTextBox.Text))
                res = true;
            return res;
        }

        /// <summary>
        /// Метод CoeffIsSucc проверяет указанные коэффициенты трансформации,
        /// они должны быть либо оба 0, либо оба отличные от нуля, при этом
        /// коэф1 должен быть больше коэф2. 
        /// </summary>
        private bool CoeffIsSucc()
        {
            bool res = false;
            if (AddChTrCoeff1TrNumeric.Value == 0 && AddChTrCoeff2TrNumeric.Value == 0)
                res = true;
            else if (AddChTrCoeff1TrNumeric.Value != 0 && AddChTrCoeff2TrNumeric.Value != 0
                && AddChTrCoeff1TrNumeric.Value > AddChTrCoeff2TrNumeric.Value)
                res = true;
            return res;
        }
    }
}
