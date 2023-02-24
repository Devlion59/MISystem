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
    public partial class FindMIWindow : Form
    {
        public FindMIWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Событие FindButton_Click нажатия кнопки "Найти" выгружает количество СИ 
        /// с указанным номером из основной таблицы СИ, если оно больше 0,
        /// то выгружает информацию по СИ и отображает в таблице,
        /// если равно 0, то проверяет есть ли запись в таблице история по указанному серийному номеру,
        /// если есть, то выгружает информацию и отображает в таблице, если нет то выдает сообщение,
        /// что СИ по серийному номеру не найдено в БД
        /// </summary>
        private void FindButton_Click(object sender, EventArgs e)
        {
            string serialNumber;
            if (String.IsNullOrEmpty(FindMISerialNumber.Text) || String.IsNullOrWhiteSpace(FindMISerialNumber.Text))
                serialNumber = "";
            else
                serialNumber = FindMISerialNumber.Text;
            if (Tools.GetCountMIWithSerial(FindMISerialNumber.Text) !=0)
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string sql = $"select nameDistrict, nameOandGPF, nameProductFacility, nameLocationOfMI, nameTypeOfMI, nameMIFromRef, serialNumber" +
                    $" from MeasurInstr INNER JOIN RefOfMI ON miFromRefId = idMIFromRef INNER JOIN TypeOfMI ON idTypeOfMI = typeOfMiId " +
                    $"INNER JOIN LocationOfMI ON locationOfMiId = idLocationOfMI INNER JOIN ProductionFacilities ON productionFacilityId = idProductFacility" +
                    $" INNER JOIN OandGPF ON OandGPFId = idOandGPF INNER JOIN Districts ON districtId = idDistrict WHERE serialNumber = '{serialNumber}'";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    FindDataSet = new DataSet();
                    adapter.Fill(FindDataSet);
                    FindDataGridView.DataSource = FindDataSet.Tables[0];
                }
                FindDataGridView.Columns[0].HeaderText = "Сетевой район";
                FindDataGridView.Columns[1].HeaderText = "ЦДНГ";
                FindDataGridView.Columns[2].HeaderText = "Производственный объект";
                FindDataGridView.Columns[3].HeaderText = "Место установки СИ";
                FindDataGridView.Columns[4].HeaderText = "Тип СИ";
                FindDataGridView.Columns[5].HeaderText = "Название СИ";
                FindDataGridView.Columns[6].HeaderText = "Заводской номер";
            }
            else if(Tools.GetCountMiHistoryWithSerial(FindMISerialNumber.Text) !=0)
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string sql = $"select nameDistrict, nameOandGPF, nameProductFacility, nameLocationOfMI, nameTypeOfMI, nameMIFromRef, logSerialNumber, nameReason" +
                    $" from Log INNER JOIN RefOfMI ON LogMiFromRefId = idMIFromRef INNER JOIN TypeOfMI ON idTypeOfMI = typeOfMiId " +
                    $"INNER JOIN LocationOfMI ON LogLocationOfMiId = idLocationOfMI INNER JOIN ProductionFacilities ON productionFacilityId = idProductFacility" +
                    $" INNER JOIN OandGPF ON OandGPFId = idOandGPF INNER JOIN Districts ON districtId = idDistrict WHERE LogSerialNumber = '{serialNumber}'";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    FindDataSet = new DataSet();
                    adapter.Fill(FindDataSet);
                    FindDataGridView.DataSource = FindDataSet.Tables[0];
                }
                FindDataGridView.Columns[0].HeaderText = "Сетевой район";
                FindDataGridView.Columns[1].HeaderText = "ЦДНГ";
                FindDataGridView.Columns[2].HeaderText = "Производственный объект";
                FindDataGridView.Columns[3].HeaderText = "Место установки СИ";
                FindDataGridView.Columns[4].HeaderText = "Тип СИ";
                FindDataGridView.Columns[5].HeaderText = "Название СИ";
                FindDataGridView.Columns[6].HeaderText = "Заводской номер";
                FindDataGridView.Columns[7].HeaderText = "Статус";
            }
            else if(Tools.GetCountMIWithSerial(FindMISerialNumber.Text) == 0 && Tools.GetCountMiHistoryWithSerial(FindMISerialNumber.Text) == 0)
                MessageBox.Show("СИ по серийному номеру не найдено! Проверьте корректность введенного серийного номера!");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
