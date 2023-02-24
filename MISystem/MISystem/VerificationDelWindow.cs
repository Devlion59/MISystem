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
    public partial class VerificationDelWindow : Form
    {
        public VerificationDelWindow(string nameMiDelet, int idTypeOfMIFromRef)
        {
            InitializeComponent();
            VerificationDelLebel.Text = $"Вы действительно хотите удалить средство измерения {nameMiDelet} из справочника {TypeOfMI(idTypeOfMIFromRef)}?";
        }

        public VerificationDelWindow(string nameMiDelet, Element o)
        {
            InitializeComponent();
            VerificationDelLebel.Text = $"Вы действительно хотите удалить{TypeOfObject(o)} {nameMiDelet} из базы данных?";
        }

        /// <summary>
        /// Метод TypeOfMI возвращает название вида СИ по переданному id
        /// </summary>
        /// <param name="idTypeOfMIFromRef">id вида СИ</param>
        private string TypeOfMI(int idTypeOfMIFromRef)
        {
            string tempS = "";
            switch(idTypeOfMIFromRef)
            {
                case 1:
                    tempS = "Трансформаторов тока";
                    break;
                case 2:
                    tempS = "Трансформаторов напряжения";
                    break;
                case 3:
                    tempS = "Счетчиков";
                    break;
            }
            return tempS;
        }

        /// <summary>
        /// Метод TypeOfMI возвращает название тип переданного элемента
        /// </summary>
        /// <param name="o">экземпляр переданного элемента</param>
        private string TypeOfObject(Element o)
        {
            string res = "";
            if (o is District)
                res = " Район";
            else if (o is OandGPF)
                res = " ЦДНГ";
            else if (o is ProductionFacility)
                res = " Объект";
            else if (o is LocationOfMI)
                res = " место установки СИ";
            else if(o is MeasurInstr)
                res = "";
            return res;
        }
    }
}
