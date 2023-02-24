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
    public partial class ReportsWindow : Form
    {
        Dictionary<int, District> dicDistr = new Dictionary<int, District>();
        int IdDistr { get; set; }
        int IdOandGPF { get; set; }
        int IdPF { get; set; }
        int IdDistrPlan { get; set; }
        int IdOandGPFPlan { get; set; }
        string Year { get; set; }

        public ReportsWindow()
        {
            IdDistr = 0;
            IdOandGPF = 0;
            IdPF = 0;
            IdDistrPlan = 0;
            IdOandGPFPlan = 0;
            Year = "";
            InitializeComponent();
            ComboboxDistrAdd();
            ComboboxDistrAddPlan();
            ComboboxYear();
            ReportsDistrComboBox.SelectedIndexChanged += ReportsDistrComboBox_SelectedIndexChanged;
            ReportsOandGPFComboBox.SelectedIndexChanged += ReportsOandGPFComboBox_SelectedIndexChanged;
            ReportsProdFacilComboBox.SelectedIndexChanged += ReportsProdFacilComboBox_SelectedIndexChanged;
            ReportsDistrComboBoxPlan.SelectedIndexChanged += ReportsDistrComboBoxPlan_SelectedIndexChanged;
            ReportsOandGPFComboBoxPlan.SelectedIndexChanged += ReportsOandGPFComboBoxPlan_SelectedIndexChanged;
        }

        /// <summary>
        /// Метод ComboboxDistrAdd добавляет в словарь сетевых районов с ключем = 0 элемент "Все"
        /// и заполняет компонент комбобокс на вкладке "Перечень СИ" перечнем сетевых районов
        /// и устанавливает в качестве выбранного сетевого района "Все"
        /// </summary>
        private void ComboboxDistrAdd()
        {
            dicDistr.Add(0, new District { Id = 0, NameDistrict = "Все" });
            Tools.GetDistricts(dicDistr);
            ReportsDistrComboBox.DataSource = dicDistr.Values.ToList<District>();
            ReportsDistrComboBox.DisplayMember = "NameDistrict";
            ReportsDistrComboBox.ValueMember = "Id";
            ReportsDistrComboBox.SelectedItem = ReportsDistrComboBox.Items[0];
        }

        /// <summary>
        /// Метод ComboboxOandGPFAdd заполняет компонент комбобокс на вкладке 
        /// "Перечень СИ" перечнем ЦДНГ, относящиеся к выбранному сетевому району
        /// и устанавливает в качестве выбранного ЦДНГ "Все"
        /// </summary>
        private void ComboboxOandGPFAdd()
        {
            if (IdDistr != 0)
            {
                ReportsOandGPFComboBox.DataSource = dicDistr[IdDistr].OandGPFs.Values.ToList<OandGPF>();
                ReportsOandGPFComboBox.DisplayMember = "NameOandGPF";
                ReportsOandGPFComboBox.ValueMember = "Id";
                ReportsOandGPFComboBox.SelectedItem = ReportsOandGPFComboBox.Items[0];
            }
        }

        /// <summary>
        /// Метод ComboboxPrFacilAdd заполняет компонент комбобокс на вкладке 
        /// "Перечень СИ" перечнем пр.объектов, относящиеся к выбранному ЦДНГ
        /// и устанавливает в качестве выбранного пр.объекта "Все"
        /// </summary>
        private void ComboboxPrFacilAdd()
        {
            if (IdOandGPF != 0)
            {
                ReportsProdFacilComboBox.DataSource = dicDistr[IdDistr].OandGPFs[IdOandGPF].ProductionFacilities.Values.ToList<ProductionFacility>();
                ReportsProdFacilComboBox.DisplayMember = "NameProductFacility";
                ReportsProdFacilComboBox.ValueMember = "Id";
                ReportsProdFacilComboBox.SelectedItem = ReportsProdFacilComboBox.Items[0];
            }
        }

        /// <summary>
        /// Событие ReportsDistrComboBox_SelectedIndexChanged отображает выбранный пользователем сетевой район
        /// на вкладке "Перечень СИ",
        /// если у выбранного сетевого района в словаре ЦДНг нет элементов, то выгружает и добавляет
        /// и отображает в комбобоксе ЦДНГ только те ЦДНГ, которые относятся к выбранному сетевому району
        /// </summary>
        private void ReportsDistrComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdDistr = ((District)ReportsDistrComboBox.SelectedItem).Id;
            if (dicDistr[IdDistr].OandGPFs.Count == 0)
            {
                dicDistr[IdDistr].OandGPFs.Add(0, new OandGPF { Id = 0, NameOandGPF = "Все", DistrictId = IdDistr });
                Tools.GetOandGPFs(dicDistr[IdDistr]);
            }
            else if (IdDistr == 0)
            {
                ReportsOandGPFComboBox.SelectedValue = 0;
                ReportsOandGPFComboBox.Enabled = false;
                ReportsProdFacilComboBox.SelectedValue = 0;
                ReportsProdFacilComboBox.Enabled = false;
                IdPF = 0;
                IdOandGPF = 0;
            }
            else if (IdDistr !=0)
            {
                ReportsOandGPFComboBox.Enabled = true;
                ReportsProdFacilComboBox.Enabled = true;
            }
            ComboboxOandGPFAdd();
        }

        /// <summary>
        /// Событие ReportsOandGPFComboBox_SelectedIndexChanged отображает выбранный пользователем ЦДНГ
        /// на вкладке "Перечень СИ",
        /// если у выбранного ЦДНГ в словаре пр.объектов нет элементов, то выгружает и добавляет
        /// и отображает в комбобоксе пр.объектов только те пр.объекты, которые относятся к выбранному ЦДНГ
        /// </summary>
        private void ReportsOandGPFComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdOandGPF = ((OandGPF)ReportsOandGPFComboBox.SelectedItem).Id;
            if (dicDistr[IdDistr].OandGPFs[IdOandGPF].ProductionFacilities.Count == 0)
            {
                dicDistr[IdDistr].OandGPFs[IdOandGPF].ProductionFacilities.Add(0, new ProductionFacility { Id = 0, NameProductFacility = "Все", OandGPFId = IdOandGPF });
                Tools.GetProductionFacilities(dicDistr[IdDistr].OandGPFs[IdOandGPF]);
            }
            else if(IdOandGPF == 0)
            {
                ReportsProdFacilComboBox.Enabled = false;
                IdPF = 0;
            }
            else if(IdOandGPF!=0)
            {
                ReportsProdFacilComboBox.Enabled = true;
            }
            ComboboxPrFacilAdd();
        }

        /// <summary>
        /// Событие ReportsProdFacilComboBox_SelectedIndexChanged отображает выбранный пользователем пр.объект
        /// на вкладке "Перечень СИ"
        /// </summary>
        private void ReportsProdFacilComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdPF = ((ProductionFacility)ReportsProdFacilComboBox.SelectedItem).Id;
        }

        /// <summary>
        /// Событие экспортВExcelToolStripMenuItem_Click выгружает отчет в Excel при нажатии на соответствующую кнопку
        /// проверяет на какой вкладке находится пользователь, чтобы выгрузить в эксель соответствующий вид отчета,
        /// т.к. у каждого отчета разные столбцы
        /// </summary>
        private void экспортВExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            //Приложение
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
                //Книга.
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                //Таблица.
                ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

                if(ReportsWindTabContr.SelectedTab.Text.Equals("Перечень СИ"))
                {
                    ExcelWorkSheet.Cells[1, 1] = "Сетевой район";
                    ExcelWorkSheet.Cells[1, 2] = "ЦДНГ";
                    ExcelWorkSheet.Cells[1, 3] = "Производственный объект";
                    ExcelWorkSheet.Cells[1, 4] = "Место установки СИ";
                    ExcelWorkSheet.Cells[1, 5] = "Тип СИ";
                    ExcelWorkSheet.Cells[1, 6] = "Название СИ";
                    ExcelWorkSheet.Cells[1, 7] = "Год выпуска";
                    ExcelWorkSheet.Cells[1, 8] = "Класс точности";
                    ExcelWorkSheet.Cells[1, 9] = "Заводской номер";
                    ExcelWorkSheet.Cells[1, 10] = "Коэффициент 1";
                    ExcelWorkSheet.Cells[1, 11] = "Коэффициент 2";
                    ExcelWorkSheet.Cells[1, 12] = "Дата поверки";
                    ExcelWorkSheet.Cells[1, 13] = "МПИ";
                    ExcelWorkSheet.Cells[1, 14] = "Дата следующей поверки";

                    for (int i = 0; i < ListOfMIDataGrid.Rows.Count; i++)
                        for (int j = 0; j < ListOfMIDataGrid.ColumnCount; j++)
                            ExcelWorkSheet.Cells[i + 2, j + 1] = ListOfMIDataGrid.Rows[i].Cells[j].Value;
                }
                else
                {
                    ExcelWorkSheet.Cells[1, 1] = "Сетевой район";
                    ExcelWorkSheet.Cells[1, 2] = "ЦДНГ";
                    ExcelWorkSheet.Cells[1, 3] = "Производственный объект";
                    ExcelWorkSheet.Cells[1, 4] = "Место установки СИ";
                    ExcelWorkSheet.Cells[1, 5] = "Тип СИ";
                    ExcelWorkSheet.Cells[1, 6] = "Название СИ";
                    ExcelWorkSheet.Cells[1, 7] = "Класс точности";
                    ExcelWorkSheet.Cells[1, 8] = "Заводской номер";
                    ExcelWorkSheet.Cells[1, 9] = "Дата следующей поверки";

                    for (int i = 0; i < PlanOfMIDataGrid.Rows.Count; i++)
                        for (int j = 0; j < PlanOfMIDataGrid.ColumnCount; j++)
                            ExcelWorkSheet.Cells[i + 2, j + 1] = PlanOfMIDataGrid.Rows[i].Cells[j].Value;
                }
                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблемы с MS Office Excel. Проверьте что он установлен!");
            }
        }

        /// <summary>
        /// Метод ComboboxDistrAdd заполняет компонент комбобокс на вкладке "График поверки СИ" перечнем сетевых районов
        /// и устанавливает в качестве выбранного сетевого района "Все"
        /// </summary>
        private void ComboboxDistrAddPlan()
        {
            ReportsDistrComboBoxPlan.DataSource = dicDistr.Values.ToList<District>();
            ReportsDistrComboBoxPlan.DisplayMember = "NameDistrict";
            ReportsDistrComboBoxPlan.ValueMember = "Id";
            ReportsDistrComboBoxPlan.SelectedItem = ReportsDistrComboBoxPlan.Items[0];
        }

        /// <summary>
        /// Метод ComboboxOandGPFAddPlan заполняет компонент комбобокс на вкладке "График поверки СИ" перечнем ЦДНГ,
        /// относящиейся к выбранному сетевому району
        /// и устанавливает в качестве выбранного ЦДНГ "Все"
        /// </summary>
        private void ComboboxOandGPFAddPlan()
        {
            if (IdDistrPlan != 0)
            {
                ReportsOandGPFComboBoxPlan.DataSource = dicDistr[IdDistrPlan].OandGPFs.Values.ToList<OandGPF>();
                ReportsOandGPFComboBoxPlan.DisplayMember = "NameOandGPF";
                ReportsOandGPFComboBoxPlan.ValueMember = "Id";
                ReportsOandGPFComboBoxPlan.SelectedItem = ReportsOandGPFComboBoxPlan.Items[0];
            }
        }

        /// <summary>
        /// Метод ComboboxOandGPFAddPlan заполняет компонент комбобокс на вкладке "График поверки СИ" перечнем годов
        /// базовый год - текущий. отображение 20 лет до и 20 лет после текущего года
        /// и устанавливает в качестве выбранного года текущий год
        /// </summary>
        private void ComboboxYear()
        {
            ReportsYear.Items.Clear();
            int year = DateTime.Now.Year;
            int startYear = year + 20;
            for (int i = 0; i < 40; i++)
            {
                ReportsYear.Items.Add(startYear.ToString());
                startYear--;
            }
            ReportsYear.SelectedItem = ReportsYear.Items[20];
        }

        /// <summary>
        /// Событие ReportsDistrComboBoxPlan_SelectedIndexChanged отображает выбранный пользователем сетевой район
        /// на вкладке "График поверки СИ",
        /// если у выбранного сетевого района в словаре ЦДНГ нет элементов, то выгружает и добавляет
        /// и отображает в комбобоксе ЦДНГ только те ЦДНГ, которые относятся к выбранному сетевому району
        /// </summary>
        private void ReportsDistrComboBoxPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdDistrPlan = ((District)ReportsDistrComboBoxPlan.SelectedItem).Id;
            if (dicDistr[IdDistrPlan].OandGPFs.Count == 0)
            {
                dicDistr[IdDistrPlan].OandGPFs.Add(0, new OandGPF { Id = 0, NameOandGPF = "Все", DistrictId = IdDistrPlan });
                Tools.GetOandGPFs(dicDistr[IdDistrPlan]);
            }
            else if (IdDistrPlan == 0)
            {
                ReportsOandGPFComboBoxPlan.SelectedValue = 0;
                ReportsOandGPFComboBoxPlan.Enabled = false;
                IdOandGPFPlan = 0;
            }
            else if (IdDistrPlan != 0)
            {
                ReportsOandGPFComboBoxPlan.Enabled = true;
            }
            ComboboxOandGPFAddPlan();
        }

        /// <summary>
        /// Событие ReportsOandGPFComboBoxPlan_SelectedIndexChanged отображает выбранный пользователем ЦДНГ
        /// на вкладке "График поверки СИ"
        /// </summary>
        private void ReportsOandGPFComboBoxPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdOandGPFPlan = ((OandGPF)ReportsOandGPFComboBoxPlan.SelectedItem).Id;
        }

        /// <summary>
        /// Событие ReportsYear_SelectedIndexChanged запоминает выбранный год
        /// на вкладке "График поверки СИ"
        /// </summary>
        private void ReportsYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Year = ReportsYear.SelectedItem.ToString();
        }

        /// <summary>
        /// Метод CreateReport_Click выгружает отчет на вкладке "Перечень СИ"
        /// по выбранным пользователям параметрам - Сетевой район, ЦДНГ, пр.объект,
        /// формирует столбцы в таблице и присваивает им названия
        /// </summary>
        private void CreateReport_Click(object sender, EventArgs e)
        {
            string parameters = "";
            if (IdDistr != 0 && IdOandGPF == 0 && IdPF == 0)
                parameters = $" WHERE idDistrict = {IdDistr}";
            else if (IdDistr != 0 && IdOandGPF != 0 && IdPF == 0)
                parameters = $" WHERE idDistrict = {IdDistr} AND idOandGPF = {IdOandGPF}";
            else if (IdDistr != 0 && IdOandGPF != 0 && IdPF != 0)
                parameters = $" WHERE idDistrict = {IdDistr} AND idOandGPF = {IdOandGPF} AND idProductFacility = {IdPF}";

            SqlRequest(parameters);

            ListOfMIDataGrid.Columns[0].HeaderText = "Сетевой район";
            ListOfMIDataGrid.Columns[1].HeaderText = "ЦДНГ";
            ListOfMIDataGrid.Columns[2].HeaderText = "Производственный объект";
            ListOfMIDataGrid.Columns[3].HeaderText = "Место установки СИ";
            ListOfMIDataGrid.Columns[4].HeaderText = "Тип СИ";
            ListOfMIDataGrid.Columns[5].HeaderText = "Название СИ";
            ListOfMIDataGrid.Columns[6].HeaderText = "Год выпуска";
            ListOfMIDataGrid.Columns[7].HeaderText = "Класс точности";
            ListOfMIDataGrid.Columns[8].HeaderText = "Заводской номер";
            ListOfMIDataGrid.Columns[9].HeaderText = "Коэффициент 1";
            ListOfMIDataGrid.Columns[10].HeaderText = "Коэффициент 2";
            ListOfMIDataGrid.Columns[11].HeaderText = "Дата поверки";
            ListOfMIDataGrid.Columns[12].HeaderText = "МПИ";
            ListOfMIDataGrid.Columns[13].HeaderText = "Дата следующей поверки";
        }

        /// <summary>
        /// Метод CreatePlanReportButton_Click выгружает отчет на вкладке "График поверки СИ"
        /// по выбранным пользователям параметрам - Сетевой район, ЦДНГ, год,
        /// формирует столбцы в таблице и присваивает им названия
        /// </summary>
        private void CreatePlanReportButton_Click(object sender, EventArgs e)
        {
            string parameters = " WHERE";
            if (IdDistrPlan != 0 && IdOandGPFPlan == 0)
                parameters = $" WHERE idDistrict = {IdDistrPlan} AND";
            else if (IdDistrPlan != 0 && IdOandGPFPlan != 0)
                parameters = $" WHERE idDistrict = {IdDistrPlan} AND idOandGPF = {IdDistrPlan} AND";

            SqlRequest(parameters, Year);

            PlanOfMIDataGrid.Columns[0].HeaderText = "Сетевой район";
            PlanOfMIDataGrid.Columns[1].HeaderText = "ЦДНГ";
            PlanOfMIDataGrid.Columns[2].HeaderText = "Производственный объект";
            PlanOfMIDataGrid.Columns[3].HeaderText = "Место установки СИ";
            PlanOfMIDataGrid.Columns[4].HeaderText = "Тип СИ";
            PlanOfMIDataGrid.Columns[5].HeaderText = "Название СИ";
            PlanOfMIDataGrid.Columns[6].HeaderText = "Класс точности";
            PlanOfMIDataGrid.Columns[7].HeaderText = "Заводской номер";
            PlanOfMIDataGrid.Columns[8].HeaderText = "Дата следующей поверки";
        }

        /// <summary>
        /// Метод SqlRequest направляет SQL запрос и 
        /// по выбранным пользователям параметрам - Сетевой район, ЦДНГ, пр.объект
        /// и отображает в таблице "Перечень СИ"
        /// </summary>
        /// <param name="parameters">список параметров для запроса</param>
        private void SqlRequest(string parameters)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = $"select nameDistrict, nameOandGPF, nameProductFacility, nameLocationOfMI, nameTypeOfMI, nameMIFromRef, yearOfManufacture, accuracyClass, serialNumber, coefficientUp, coefficientDown," +
                $" verificationDate, verificationInterval, verificationNextDate from MeasurInstr INNER JOIN RefOfMI ON miFromRefId = idMIFromRef INNER JOIN TypeOfMI ON idTypeOfMI = typeOfMiId " +
                $"INNER JOIN LocationOfMI ON locationOfMiId = idLocationOfMI INNER JOIN ProductionFacilities ON productionFacilityId = idProductFacility" +
                $" INNER JOIN OandGPF ON OandGPFId = idOandGPF INNER JOIN Districts ON districtId = idDistrict{parameters}";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                List = new DataSet();
                adapter.Fill(List);
                ListOfMIDataGrid.DataSource = List.Tables[0];
            }
        }

        /// <summary>
        /// Метод SqlRequest направляет SQL запрос и 
        /// по выбранным пользователям параметрам - Сетевой район, ЦДНГ
        /// и отображает в таблице "График поверки СИ"
        /// </summary>
        /// <param name="parameters">список параметров для запроса</param>
        /// <param name="year">год, в котором должна быть проведена поверка</param>
        private void SqlRequest(string parameters, string year)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = $"select nameDistrict, nameOandGPF, nameProductFacility, nameLocationOfMI, nameTypeOfMI, nameMIFromRef, accuracyClass, serialNumber," +
                $" verificationNextDate from MeasurInstr INNER JOIN RefOfMI ON miFromRefId = idMIFromRef INNER JOIN TypeOfMI ON idTypeOfMI = typeOfMiId " +
                $"INNER JOIN LocationOfMI ON locationOfMiId = idLocationOfMI INNER JOIN ProductionFacilities ON productionFacilityId = idProductFacility" +
                $" INNER JOIN OandGPF ON OandGPFId = idOandGPF INNER JOIN Districts ON districtId = idDistrict{parameters} verificationNextDate >= '01.01.{year}' AND verificationNextDate <= '31.12.{year}'";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                Plan = new DataSet();
                adapter.Fill(Plan);
                PlanOfMIDataGrid.DataSource = Plan.Tables[0];
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
