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
    public partial class MainWindow : Form
    {
        Dictionary<int, District> dictOfDistrict = new Dictionary<int, District>();
        Element elem;

        public MainWindow()
        {
            InitializeComponent();
            Tools.GetDistricts(dictOfDistrict);
            MainWindowTreeView.BeforeExpand += MainWindowTreeView_BeforeExpand;
            MainWindowTreeView.BeforeSelect += MainWindowTreeView_BeforeSelect;
            FillMiNodes();
        }

        /// <summary>
        /// Событие токаToolStripMenuItem_Click создает окно справочника
        /// с указанным id вида СИ - трансформаторы тока
        /// </summary>

        private void токаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementReferenceWindow windowER = new ElementReferenceWindow(1);
            windowER.Text = "Типы трансформаторов тока";
            windowER.ShowDialog();
            if (windowER.ChangeElem)
                RefreshMainWindow();
        }

        /// <summary>
        /// Событие напряженияToolStripMenuItem_Click создает окно справочника
        /// с указанным id вида СИ - трансформаторы напряжения
        /// </summary>
        private void напряженияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementReferenceWindow windowER = new ElementReferenceWindow(2);
            windowER.Text = "Типы трансформаторов напряжения";
            windowER.ShowDialog();
            if (windowER.ChangeElem)
                RefreshMainWindow();
        }

        /// <summary>
        /// Событие счетчикиToolStripMenuItem_Click создает окно справочника
        /// с указанным id вида СИ - счетчики
        /// </summary>
        private void счетчикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ElementReferenceWindow windowER = new ElementReferenceWindow(3);
            windowER.Text = "Типы счетчиков";
            windowER.ShowDialog();
            if (windowER.ChangeElem)
                RefreshMainWindow();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Событие редактированиеToolStripMenuItem_Click создает окно редактирования
        /// для доб/редакт/удал элементов
        /// </summary>
        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServicesWindow sw = new ServicesWindow(dictOfDistrict);
            sw.ShowDialog();
            if(sw.AddChangeDelElem)
                RefreshMainWindow();
        }

        /// <summary>
        /// Событие отчетыToolStripMenuItem_Click создает окно формирования отчетов
        /// </summary>
        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportsWindow rpw = new ReportsWindow();
            rpw.Text = "Отчеты";
            rpw.ShowDialog();
        }

        /// <summary>
        /// Событие MainWindowTreeView_BeforeExpand перед раскрытием элемента в дереве отображения элементов
        /// запрашивает перечень элементов, вхдящих в иерархию выбранного элемента и отображает их в дереве
        /// </summary>
        void MainWindowTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            try
            {
                if (e.Node.Tag is District)
                {
                    District d = e.Node.Tag as District;
                    var keys = d.OandGPFs.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode(d.OandGPFs[k].NameOandGPF);
                        dirNode.Tag = d.OandGPFs[k];
                        FillTreeNodes(dirNode, d.OandGPFs[k]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
                else if (e.Node.Tag is OandGPF)
                {
                    OandGPF oAndGPF = e.Node.Tag as OandGPF;
                    var keys = oAndGPF.ProductionFacilities.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode(oAndGPF.ProductionFacilities[k].NameProductFacility);
                        dirNode.Tag = oAndGPF.ProductionFacilities[k];
                        FillTreeNodes(dirNode, oAndGPF.ProductionFacilities[k]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
                else if (e.Node.Tag is ProductionFacility)
                {
                    ProductionFacility pf = e.Node.Tag as ProductionFacility;
                    var keys = pf.LocationOfMIs.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode(pf.LocationOfMIs[k].NameLocationOfMI);
                        dirNode.Tag = pf.LocationOfMIs[k];
                        FillTreeNodes(dirNode, pf.LocationOfMIs[k]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Событие MainWindowTreeView_BeforeSelect перед выбором элемента в дереве отображения элементов
        /// отображает информацию об элементе в компонентах отображения информации
        /// </summary>
        void MainWindowTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            elem = (Element)e.Node.Tag;
            try
            {
                if (e.Node.Tag is District)
                {
                    MainWindowTabControlMI.Visible = false;
                    TabControlPassportLabelNumber.Visible = false;
                    LabelTabControlPassportForNumber.Visible = false;
                    District d = e.Node.Tag as District;
                    LabelUpTabСontrolForName.Text = d.NameDistrict;
                }
                else if (e.Node.Tag is OandGPF)
                {
                    MainWindowTabControlMI.Visible = false;
                    TabControlPassportLabelNumber.Visible = false;
                    LabelTabControlPassportForNumber.Visible = false;
                    OandGPF oAndGPF = e.Node.Tag as OandGPF;
                    LabelUpTabСontrolForName.Text = oAndGPF.NameOandGPF;
                }
                else if (e.Node.Tag is ProductionFacility)
                {
                    MainWindowTabControlMI.Visible = false;
                    TabControlPassportLabelNumber.Visible = false;
                    LabelTabControlPassportForNumber.Visible = false;
                    ProductionFacility pf = e.Node.Tag as ProductionFacility;
                    LabelUpTabСontrolForName.Text = pf.NameProductFacility;
                }
                else if (e.Node.Tag is LocationOfMI)
                {
                    MainWindowTabControlMI.Visible = true;
                    TabControlPassportLabelNumber.Visible = true;
                    LabelTabControlPassportForNumber.Visible = true;
                    LocationOfMI locOfMi = e.Node.Tag as LocationOfMI;
                    LabelUpTabСontrolForName.Text = locOfMi.NameLocationOfMI;
                    LabelTabControlPassportForNumber.Text = locOfMi.NumberLocationOfMI.ToString();
                    if (locOfMi.MeasurInstrs.Count == 0)
                        Tools.GetMIs(locOfMi);
                    ShowMI(locOfMi);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Метод FillMiNodes добавляет сетевые районы в дерево отображения элементов
        /// и запрашивает запрашивает элементы, входящие в каждый сетевой район для дальнешего
        /// добавления в дерево отображения элементов
        /// </summary>
        private void FillMiNodes()
        {
            try
            {
                var keys = dictOfDistrict.Keys;
                foreach (var k in keys)
                {
                    TreeNode distNode = new TreeNode();
                    distNode.Tag = dictOfDistrict[k];
                    distNode.Text = dictOfDistrict[k].NameDistrict;
                    FillTreeNodes(distNode, dictOfDistrict[k]);
                    MainWindowTreeView.Nodes.Add(distNode);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Метод FillTreeNodes добавляет элементы в дерево отображения элементов
        /// для выбранного родительского узла.
        /// получает элементы для выбранного узла из базы данных
        /// </summary>
        /// <param name="distNode">родительский узел</param>
        /// <param name="o">тип выбранного элемента для которого нужно получить зависимые элементы</param>
        private void FillTreeNodes(TreeNode distNode, Element o)
        {
            try
            {
                if (o is District)
                {
                    District d = o as District;
                    if (d.OandGPFs.Count == 0)
                        Tools.GetOandGPFs(d);
                    var keys = d.OandGPFs.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode();
                        dirNode.Tag = d.OandGPFs[k];
                        dirNode.Text = d.OandGPFs[k].NameOandGPF;
                        distNode.Nodes.Add(dirNode);
                    }
                }
                else if (o is OandGPF)
                {
                    OandGPF oAndGPF = o as OandGPF;
                    if (oAndGPF.ProductionFacilities.Count == 0)
                        Tools.GetProductionFacilities(oAndGPF);
                    var keys = oAndGPF.ProductionFacilities.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode();
                        dirNode.Tag = oAndGPF.ProductionFacilities[k];
                        dirNode.Text = oAndGPF.ProductionFacilities[k].NameProductFacility;
                        distNode.Nodes.Add(dirNode);
                    }
                }
                else if (o is ProductionFacility)
                {
                    ProductionFacility pf = o as ProductionFacility;
                    if (pf.LocationOfMIs.Count == 0)
                        Tools.GetLocationOfMis(pf);
                    var keys = pf.LocationOfMIs.Keys;
                    foreach (var k in keys)
                    {
                        TreeNode dirNode = new TreeNode();
                        dirNode.Tag = pf.LocationOfMIs[k];
                        dirNode.Text = pf.LocationOfMIs[k].NameLocationOfMI;
                        distNode.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Метод ShowMI заполняет компоненты отображения информации
        /// о СИ информацией из экземпляра СИ
        /// </summary>
        /// <param name="locOfMi">экземпляр места установки СИ</param>
        private void ShowMI(LocationOfMI locOfMi)
        {
            ClearAllLableInMainWindownDownTabControl();
            var keys = locOfMi.MeasurInstrs.Keys;
            foreach (var k in keys)
            {
                if (locOfMi.MeasurInstrs[k].PhaseId == 1)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId)==1)
                    {
                        LabelTypeCurrTrPhaseA.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeffUpCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeffDownCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        LabelVerifDateCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        LabelTypeVoltTrPhaseA.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeff1VoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeff2VoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientDown?? "";
                        LabelVerifDateVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 2)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 1)
                    {
                        LabelTypeCurrTrPhaseB.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeffUpCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeffDownCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        LabelVerifDateCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        LabelTypeVoltTrPhaseB.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeff1VoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeff2VoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        LabelVerifDateVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 3)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId)  == 1)
                    {
                        LabelTypeCurrTrPhaseC.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeff1CurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeff2CurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        LabelVerifDateCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        LabelTypeVoltTrPhaseC.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        LabelAccClassVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        LabelSerialNumbVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        LabelCoeff1VoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        LabelCoeff2VoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientDown?? "";
                        LabelVerifDateVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 8)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 3)
                    {
                        LabelTypeMeter.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        LabelYearOfManufMeter.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ??"";
                        LabelAccClassMeter.Text = locOfMi.MeasurInstrs[k].AccuracyClass??"";
                        LabelSerialNumbMeter.Text = locOfMi.MeasurInstrs[k].SerialNumber??"";
                        LabelVerifDateMeter.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        LabelVerifIntervMeter.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        LabelVerifNextDateMeter.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
            }
        }

        /// <summary>
        /// Метод ClearAllLableInMainWindownDownTabControl обновляет информацию в 
        /// компонентах отображения информации о СИ
        /// </summary>
        private void ClearAllLableInMainWindownDownTabControl()
        {
            LabelTypeCurrTrPhaseA.ResetText();
            LabelYearOfManufCurrTrPhaseA.ResetText();
            LabelAccClassCurrTrPhaseA.ResetText();
            LabelSerialNumbCurrTrPhaseA.ResetText();
            LabelCoeffUpCurrTrPhaseA.ResetText();
            LabelCoeffDownCurrTrPhaseA.ResetText();
            LabelVerifDateCurrTrPhaseA.ResetText();
            LabelVerifIntervCurrTrPhaseA.ResetText();
            LabelVerifNextDateCurrTrPhaseA.ResetText();

            LabelTypeVoltTrPhaseA.ResetText();
            LabelYearOfManufVoltTrPhaseA.ResetText();
            LabelAccClassVoltTrPhaseA.ResetText();
            LabelSerialNumbVoltTrPhaseA.ResetText();
            LabelCoeff1VoltTrPhaseA.ResetText();
            LabelCoeff2VoltTrPhaseA.ResetText();
            LabelVerifDateVoltTrPhaseA.ResetText();
            LabelVerifIntervVoltTrPhaseA.ResetText();
            LabelVerifNextDateVoltTrPhaseA.ResetText();

            LabelTypeCurrTrPhaseB.ResetText();
            LabelYearOfManufCurrTrPhaseB.ResetText();
            LabelAccClassCurrTrPhaseB.ResetText();
            LabelSerialNumbCurrTrPhaseB.ResetText();
            LabelCoeffUpCurrTrPhaseB.ResetText();
            LabelCoeffDownCurrTrPhaseB.ResetText();
            LabelVerifDateCurrTrPhaseB.ResetText();
            LabelVerifIntervCurrTrPhaseB.ResetText();
            LabelVerifNextDateCurrTrPhaseB.ResetText();

            LabelTypeVoltTrPhaseB.ResetText();
            LabelYearOfManufVoltTrPhaseB.ResetText();
            LabelAccClassVoltTrPhaseB.ResetText();
            LabelSerialNumbVoltTrPhaseB.ResetText();
            LabelCoeff1VoltTrPhaseB.ResetText();
            LabelCoeff2VoltTrPhaseB.ResetText();
            LabelVerifDateVoltTrPhaseB.ResetText();
            LabelVerifIntervVoltTrPhaseB.ResetText();
            LabelVerifNextDateVoltTrPhaseB.ResetText();

            LabelTypeCurrTrPhaseC.ResetText();
            LabelYearOfManufCurrTrPhaseC.ResetText();
            LabelAccClassCurrTrPhaseC.ResetText();
            LabelSerialNumbCurrTrPhaseC.ResetText();
            LabelCoeff1CurrTrPhaseC.ResetText();
            LabelCoeff2CurrTrPhaseC.ResetText();
            LabelVerifDateCurrTrPhaseC.ResetText();
            LabelVerifIntervCurrTrPhaseC.ResetText();
            LabelVerifNextDateCurrTrPhaseC.ResetText();

            LabelTypeVoltTrPhaseC.ResetText();
            LabelYearOfManufVoltTrPhaseC.ResetText();
            LabelAccClassVoltTrPhaseC.ResetText();
            LabelSerialNumbVoltTrPhaseC.ResetText();
            LabelCoeff1VoltTrPhaseC.ResetText();
            LabelCoeff2VoltTrPhaseC.ResetText();
            LabelVerifDateVoltTrPhaseC.ResetText();
            LabelVerifIntervVoltTrPhaseC.ResetText();
            LabelVerifNextDateVoltTrPhaseC.ResetText();

            LabelTypeMeter.ResetText();
            LabelYearOfManufMeter.ResetText();
            LabelAccClassMeter.ResetText();
            LabelSerialNumbMeter.ResetText();
            LabelVerifDateMeter.ResetText();
            LabelVerifIntervMeter.ResetText();
            LabelVerifNextDateMeter.ResetText();
        }

        /// <summary>
        /// Метод RefreshMainWindow обновляет информацию в дереве отображения элементов
        /// </summary>
        private void RefreshMainWindow()
        {
            MainWindowTabControlMI.Visible = false;
            TabControlPassportLabelNumber.Visible = false;
            LabelTabControlPassportForNumber.Visible = false;
            dictOfDistrict.Clear();
            MainWindowTreeView.Nodes.Clear();

            Tools.GetDistricts(dictOfDistrict);
            FillMiNodes();
            if (dictOfDistrict.Count != 0)
                MainWindowTreeView.SelectedNode = MainWindowTreeView.Nodes[0];
            else
                LabelUpTabСontrolForName.Text = "";
        }

        /// <summary>
        /// Событие HistoryCurrTrPhaseA_Click создает окно истории изменений инфо о тр.тока фазы А
        /// </summary>
        private void HistoryCurrTrPhaseA_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 1);
            hW.Text = "История изменений/удалений трансформаторов тока фазы А";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryCurrTrPhaseB_Click создает окно истории изменений инфо о тр.тока фазы В
        /// </summary>
        private void HistoryCurrTrPhaseB_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 2);
            hW.Text = "История изменений/удалений трансформаторов тока фазы В";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryCurrTrPhaseC_Click создает окно истории изменений инфо о тр.тока фазы С
        /// </summary>
        private void HistoryCurrTrPhaseC_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 3);
            hW.Text = "История изменений/удалений трансформаторов тока фазы С";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryVoltTrPhaseA_Click создает окно истории изменений инфо о тр.напряжения фазы А
        /// </summary>
        private void HistoryVoltTrPhaseA_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 1);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы А";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryVoltTrPhaseB_Click создает окно истории изменений инфо о тр.напряжения фазы В
        /// </summary>
        private void HistoryVoltTrPhaseB_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 2);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы В";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryVoltTrPhaseC_Click создает окно истории изменений инфо о тр.напряжения фазы С
        /// </summary>
        private void HistoryVoltTrPhaseC_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 3);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы С";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistoryMeter_Click создает окно истории изменений инфо о счетчике
        /// </summary>
        private void HistoryMeter_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 3, 8);
            hW.Text = "История изменений/удалений счетчиков";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие оПрограммеToolStripMenuItem_Click создает окно о программе
        /// </summary>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram ap = new AboutProgram();
            ap.ShowDialog();
        }

        /// <summary>
        /// Событие поискToolStripMenuItem_Click создает окно поиска СИ
        /// </summary>
        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindMIWindow fMI = new FindMIWindow();
            fMI.ShowDialog();
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administration adm = new Administration();
            adm.ShowDialog();
        }
    }
}
