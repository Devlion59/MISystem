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
    public partial class HistoryWindow : Form
    {

        /// <summary>
        /// Конструктор HistoryWindow вызывает метод для выгрузки информации 
        /// по истории изменения информации о СИ
        /// </summary>
        /// <param name="logLocationOfMiId">id места установки СИ</param>
        /// <param name="idTypeOfMi">id вида СИ</param>
        /// <param name="logPhaseId">id фазы, на которой установлено СИ</param>
        public HistoryWindow(int logLocationOfMiId, int idTypeOfMi, int logPhaseId)
        {
            InitializeComponent();
            GetHistory(logLocationOfMiId, idTypeOfMi, logPhaseId);
        }

        /// <summary>
        /// Метод GetHistory направляет запрос в БД на выгрузку записей по истории изменений информации о СИ
        /// и заносит полученную информацию в таблицу,
        /// формирует столбцы и устанавливает им названия
        /// </summary>
        /// <param name="logLocationOfMiId">id места установки СИ</param>
        /// <param name="idTypeOfMi">id вида СИ</param>
        /// <param name="logPhaseId">id фазы, на которой установлено СИ</param>
        private void GetHistory(int logLocationOfMiId, int idTypeOfMi, int logPhaseId)
        {
            if (idTypeOfMi != 3)
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string sql = $"select nameMIFromRef, logYearOfManufacture, logAccuracyClass, logSerialNumber, logCoefficientUp, logCoefficientDown," +
                    $" logVerificationDate, logVerificationInterval, logVerificationNextDate, dateOfLogEntry, nameReason from Log " +
                    $"INNER JOIN RefOfMI ON logMiFromRefId = idMIFromRef WHERE logLocationOfMiId = {logLocationOfMiId} AND logPhaseId = {logPhaseId} AND typeOfMiId = {idTypeOfMi}";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    HistoryWindowDataSet = new DataSet();
                    adapter.Fill(HistoryWindowDataSet);
                    HistoryWindowDataGrid.DataSource = HistoryWindowDataSet.Tables[0];
                }
                HistoryWindowDataGrid.Columns[0].HeaderText = "Тип СИ";
                HistoryWindowDataGrid.Columns[1].HeaderText = "Год выпуска";
                HistoryWindowDataGrid.Columns[2].HeaderText = "Класс точности";
                HistoryWindowDataGrid.Columns[3].HeaderText = "Заводской номер";
                HistoryWindowDataGrid.Columns[4].HeaderText = "Коэффициент 1";
                HistoryWindowDataGrid.Columns[5].HeaderText = "Коэффициент 2";
                HistoryWindowDataGrid.Columns[6].HeaderText = "Дата поверки";
                HistoryWindowDataGrid.Columns[7].HeaderText = "МПИ";
                HistoryWindowDataGrid.Columns[8].HeaderText = "Дата следующей поверки";
                HistoryWindowDataGrid.Columns[9].HeaderText = "Дата изменения/удаления";
                HistoryWindowDataGrid.Columns[10].HeaderText = "Статус";
            }
            else
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string sql = $"select nameMIFromRef, logYearOfManufacture, logAccuracyClass, logSerialNumber," +
                    $" logVerificationDate, logVerificationInterval, logVerificationNextDate, dateOfLogEntry, nameReason from Log " +
                    $"INNER JOIN RefOfMI ON logMiFromRefId = idMIFromRef WHERE logLocationOfMiId = {logLocationOfMiId} AND logPhaseId = {logPhaseId} AND typeOfMiId = {idTypeOfMi}";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    HistoryWindowDataSet = new DataSet();
                    adapter.Fill(HistoryWindowDataSet);
                    HistoryWindowDataGrid.DataSource = HistoryWindowDataSet.Tables[0];
                }
                HistoryWindowDataGrid.Columns[0].HeaderText = "Тип СИ";
                HistoryWindowDataGrid.Columns[1].HeaderText = "Год выпуска";
                HistoryWindowDataGrid.Columns[2].HeaderText = "Класс точности";
                HistoryWindowDataGrid.Columns[3].HeaderText = "Заводской номер";
                HistoryWindowDataGrid.Columns[4].HeaderText = "Дата поверки";
                HistoryWindowDataGrid.Columns[5].HeaderText = "МПИ";
                HistoryWindowDataGrid.Columns[6].HeaderText = "Дата следующей поверки";
                HistoryWindowDataGrid.Columns[7].HeaderText = "Дата изменения/удаления";
                HistoryWindowDataGrid.Columns[8].HeaderText = "Статус";
            }
        }
    }
}
