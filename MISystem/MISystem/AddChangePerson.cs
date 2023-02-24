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
    public partial class AddChangePerson : Form
    {
        protected List<Role> listRole = new List<Role>();
        protected List<Person> listPerson = new List<Person>();
        protected Dictionary<int, District> dicDistr = new Dictionary<int, District>();
        public bool Change { get; set; }

        public AddChangePerson()
        {
            InitializeComponent();
            AddRoleCombobox();
            AddDistrCombobox();
        }


        private void AddRoleCombobox()
        {
            Tools.GetRole(listRole);
            AddChangePersonRoleComboBox.DataSource = listRole;
            AddChangePersonRoleComboBox.DisplayMember = "NameRole";
            AddChangePersonRoleComboBox.ValueMember = "IdRole";
        }

        private void AddDistrCombobox()
        {
            Tools.GetDistricts(dicDistr);
            AddChangePersonDistrComboBox.DataSource = dicDistr.Values.ToList<District>();
            AddChangePersonDistrComboBox.DisplayMember = "NameDistrict";
            AddChangePersonDistrComboBox.ValueMember = "Id";
        }
    }


}



