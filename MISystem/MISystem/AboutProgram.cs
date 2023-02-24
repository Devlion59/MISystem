using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MISystem
{
    public partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
            AboutProgramLinkLabelWebsite.LinkClicked += AboutProgramLinkLabelWebsite_LinkClicked;
            AboutProgramLinkLabelMailTo.LinkClicked += AboutProgramLinkLabelMailTo_LinkClicked;
            UsersGuideLinkLabel.LinkClicked += UsersGuideLinkLabel_LinkClicked;
        }

        /// <summary>
        /// Событие AboutProgramLinkLabelWebsite_LinkClicked 
        /// открывает ссылку на сайт предприятия в браузере, отмеченным по умолчанию
        /// </summary>
        private void AboutProgramLinkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://perm.lukoil.ru/ru");
        }

        /// <summary>
        /// Событие AboutProgramLinkLabelMailTo_LinkClicked 
        /// открывает почтовый сервис по умолчанию и создает новое электронное сообщение на почту
        /// </summary>
        private void AboutProgramLinkLabelMailTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:cheremnyhdn@gmail.com");
        }

        private void UsersGuideLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(@"C:\\Users\\User\\source\\repos\\MISystem\\MISystem\\Properties\\Users Guide.doc"))
            {
                System.Diagnostics.Process.Start("C:\\Users\\User\\source\\repos\\MISystem\\MISystem\\Properties\\Users Guide.doc");
            }
            else
                MessageBox.Show("Руководство по эксплуатации не существует!");
        }
    }
}
