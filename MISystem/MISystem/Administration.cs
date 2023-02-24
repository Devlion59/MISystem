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
    public partial class Administration : Form
    {
        public Administration()
        {
            InitializeComponent();
            FillPerson();
        }






        private void FillPerson()
        {
            SqlRequest();
            AdminDataGridView.Columns[0].HeaderText = "ФИО пользователя";
            AdminDataGridView.Columns[1].HeaderText = "Логин";
            AdminDataGridView.Columns[2].HeaderText = "Роль";
            AdminDataGridView.Columns[3].HeaderText = "Сетевой район";
        }

        private void SqlRequest()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sql = $"select namePerson, loginPerson, nameRole, nameDistrict FROM Person INNER JOIN Role ON roleId = idRole INNER JOIN Districts ON districtId = idDistrict";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                AdminDataSet = new DataSet();
                adapter.Fill(AdminDataSet);
                AdminDataGridView.DataSource = AdminDataSet.Tables[0];
            }
        }

        private void AddPersonButton_Click(object sender, EventArgs e)
        {
            AddChangePerson adChP = new AddChangePerson();
            adChP.ShowDialog();
        }
    }
}
