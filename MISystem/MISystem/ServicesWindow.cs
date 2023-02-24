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
using MISystem.Classes;

namespace MISystem
{
    public partial class ServicesWindow : Form
    {
        Dictionary<int, District> servicesDictOfDistrict = new Dictionary<int, District>();
        Dictionary<int, MeasInstrFromRef> servicesDictOfMeasInstrFromRef = new Dictionary<int, MeasInstrFromRef>();

        private bool addChangeDelElem;

        public bool AddChangeDelElem
        {
            get
            {
                return addChangeDelElem;
            }
            set
            {
                addChangeDelElem = value;
            }
        }

        Element elem;

        public ServicesWindow(Dictionary<int, District> servicesDictOfDistrict)
        {
            this.servicesDictOfDistrict = servicesDictOfDistrict;
            AddChangeDelElem = false;
            InitializeComponent();

            ServicesTreeView.BeforeSelect += ServicesTreeView_BeforeSelect;
            ServicesTreeView.BeforeExpand += ServicesTreeView_BeforeExpand;

            ServicesFillMiNodes();
        }

        private void ServicesExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Событие MainWindowTreeView_BeforeExpand перед раскрытием элемента в дереве отображения элементов
        /// запрашивает перечень элементов, вхдящих в иерархию выбранного элемента и отображает их в дереве
        /// </summary>
        void ServicesTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
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
                        ServicesFillTreeNodes(dirNode, d.OandGPFs[k]);
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
                        ServicesFillTreeNodes(dirNode, oAndGPF.ProductionFacilities[k]);
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
                        ServicesFillTreeNodes(dirNode, pf.LocationOfMIs[k]);
                        e.Node.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Событие ServicesTreeView_BeforeSelect перед выбором элемента в дереве отображения элементов
        /// отображает информацию об элементе в компонентах отображения информации
        /// </summary>
        void ServicesTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            elem = (Element)e.Node.Tag;
            try
            {
                if (e.Node.Tag is District)
                {
                    ServicesTabControlMI.Visible = false;
                    ServicesTabControlPassportLabelNumber.Visible = false;
                    ServicesLabelTabControlPassportForNumber.Visible = false;
                    District d = e.Node.Tag as District;
                    ServicesLabelTabControlPassportForName.Text = d.NameDistrict;
                    ServicesAddNewElemButton.Visible = true;
                    ServicesAddNewDistrButton.Visible = true;
                    ServicesAddNewElemButton.Enabled = true;
                    ServicesGroupBoxButton.Text = $"Действия с сетевым районом {d.NameDistrict}";
                }
                else if (e.Node.Tag is OandGPF)
                {
                    ServicesTabControlMI.Visible = false;
                    ServicesTabControlPassportLabelNumber.Visible = false;
                    ServicesLabelTabControlPassportForNumber.Visible = false;
                    OandGPF oAndGPF = e.Node.Tag as OandGPF;
                    ServicesLabelTabControlPassportForName.Text = oAndGPF.NameOandGPF;
                    ServicesAddNewElemButton.Visible = true;
                    ServicesAddNewDistrButton.Visible = false;
                    ServicesAddNewElemButton.Enabled = true;
                    ServicesGroupBoxButton.Text = $"Действия с {oAndGPF.NameOandGPF}";
                }
                else if (e.Node.Tag is ProductionFacility)
                {
                    ServicesTabControlMI.Visible = false;
                    ServicesTabControlPassportLabelNumber.Visible = false;
                    ServicesLabelTabControlPassportForNumber.Visible = false;
                    ProductionFacility pf = e.Node.Tag as ProductionFacility;
                    ServicesLabelTabControlPassportForName.Text = pf.NameProductFacility;
                    ServicesAddNewElemButton.Visible = true;
                    ServicesAddNewDistrButton.Visible = false;
                    ServicesAddNewElemButton.Enabled = true;
                    ServicesGroupBoxButton.Text = $"Действия с объектом {pf.NameProductFacility}";
                }
                else if (e.Node.Tag is LocationOfMI)
                {
                    ServicesTabControlMI.Visible = true;
                    ServicesTabControlPassportLabelNumber.Visible = true;
                    ServicesLabelTabControlPassportForNumber.Visible = true;
                    LocationOfMI locOfMi = e.Node.Tag as LocationOfMI;
                    ServicesLabelTabControlPassportForName.Text = locOfMi.NameLocationOfMI;
                    ServicesLabelTabControlPassportForNumber.Text = locOfMi.NumberLocationOfMI.ToString();
                    ServicesAddNewDistrButton.Visible = false;
                    ServicesAddNewElemButton.Enabled = false;
                    ServicesGroupBoxButton.Text = $"Действия с местом установки СИ {locOfMi.NameLocationOfMI}";
                    if (locOfMi.MeasurInstrs.Count == 0)
                        Tools.GetMIs(locOfMi);
                    ServicesShowMI(locOfMi);
                    if (servicesDictOfMeasInstrFromRef.Count == 0)
                        Tools.GetAllRefElem(servicesDictOfMeasInstrFromRef);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Метод ServicesFillMiNodes добавляет сетевые районы в дерево отображения элементов
        /// и запрашивает запрашивает элементы, входящие в каждый сетевой район для дальнешего
        /// добавления в дерево отображения элементов
        /// </summary>
        private void ServicesFillMiNodes()
        {
            try
            {
                var keys = servicesDictOfDistrict.Keys;
                foreach (var k in keys)
                {
                    TreeNode distNode = new TreeNode();
                    distNode.Tag = servicesDictOfDistrict[k];
                    distNode.Text = servicesDictOfDistrict[k].NameDistrict;
                    ServicesFillTreeNodes(distNode, servicesDictOfDistrict[k]);
                    ServicesTreeView.Nodes.Add(distNode);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Метод ServicesFillTreeNodes добавляет элементы в дерево отображения элементов
        /// для выбранного родительского узла.
        /// получает элементы для выбранного узла из базы данных
        /// </summary>
        /// <param name="distNode">родительский узел</param>
        /// <param name="o">тип выбранного элемента для которого нужно получить зависимые элементы</param>
        private void ServicesFillTreeNodes(TreeNode distNode, Element o)
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
        /// Метод ServicesShowMI заполняет компоненты отображения информации
        /// о СИ информацией из экземпляра СИ
        /// </summary>
        /// <param name="locOfMi">экземпляр места установки СИ</param>
        private void ServicesShowMI(LocationOfMI locOfMi)
        {
            ClearAllLableInMainWindownDownTabControl();
            var keys = locOfMi.MeasurInstrs.Keys;
            foreach (var k in keys)
            {
                if (locOfMi.MeasurInstrs[k].PhaseId == 1)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 1)
                    {
                        ServicesLabelTypeCurrTrPhaseA.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeffUpCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeffDownCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateCurrTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        ServicesLabelTypeVoltTrPhaseA.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeff1VoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeff2VoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateVoltTrPhaseA.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 2)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 1)
                    {
                        ServicesLabelTypeCurrTrPhaseB.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeffUpCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeffDownCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateCurrTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        ServicesLabelTypeVoltTrPhaseB.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeff1VoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeff2VoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateVoltTrPhaseB.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 3)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 1)
                    {
                        ServicesLabelTypeCurrTrPhaseC.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeff1CurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeff2CurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateCurrTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                    else if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 2)
                    {
                        ServicesLabelTypeVoltTrPhaseC.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelCoeff1VoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientUp ?? "";
                        ServicesLabelCoeff2VoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].CoefficientDown ?? "";
                        ServicesLabelVerifDateVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateVoltTrPhaseC.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
                    }
                }
                if (locOfMi.MeasurInstrs[k].PhaseId == 8)
                {
                    if (Tools.GetIdTypeRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId) == 3)
                    {
                        ServicesLabelTypeMeter.Text = Tools.GetNameRefOfMi(locOfMi.MeasurInstrs[k].MiFromRefId);
                        ServicesLabelYearOfManufMeter.Text = locOfMi.MeasurInstrs[k].YearOfManufacture ?? "";
                        ServicesLabelAccClassMeter.Text = locOfMi.MeasurInstrs[k].AccuracyClass ?? "";
                        ServicesLabelSerialNumbMeter.Text = locOfMi.MeasurInstrs[k].SerialNumber ?? "";
                        ServicesLabelVerifDateMeter.Text = locOfMi.MeasurInstrs[k].VerificationDate.ToShortDateString();
                        ServicesLabelVerifIntervMeter.Text = locOfMi.MeasurInstrs[k].VerificationInterval.ToString();
                        ServicesLabelVerifNextDateMeter.Text = locOfMi.MeasurInstrs[k].VerificationNextDate.ToShortDateString();
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
            ServicesLabelTypeCurrTrPhaseA.ResetText();
            ServicesLabelYearOfManufCurrTrPhaseA.ResetText();
            ServicesLabelAccClassCurrTrPhaseA.ResetText();
            ServicesLabelSerialNumbCurrTrPhaseA.ResetText();
            ServicesLabelCoeffUpCurrTrPhaseA.ResetText();
            ServicesLabelCoeffDownCurrTrPhaseA.ResetText();
            ServicesLabelVerifDateCurrTrPhaseA.ResetText();
            ServicesLabelVerifIntervCurrTrPhaseA.ResetText();
            ServicesLabelVerifNextDateCurrTrPhaseA.ResetText();

            ServicesLabelTypeVoltTrPhaseA.ResetText();
            ServicesLabelYearOfManufVoltTrPhaseA.ResetText();
            ServicesLabelAccClassVoltTrPhaseA.ResetText();
            ServicesLabelSerialNumbVoltTrPhaseA.ResetText();
            ServicesLabelCoeff1VoltTrPhaseA.ResetText();
            ServicesLabelCoeff2VoltTrPhaseA.ResetText();
            ServicesLabelVerifDateVoltTrPhaseA.ResetText();
            ServicesLabelVerifIntervVoltTrPhaseA.ResetText();
            ServicesLabelVerifNextDateVoltTrPhaseA.ResetText();

            ServicesLabelTypeCurrTrPhaseB.ResetText();
            ServicesLabelYearOfManufCurrTrPhaseB.ResetText();
            ServicesLabelAccClassCurrTrPhaseB.ResetText();
            ServicesLabelSerialNumbCurrTrPhaseB.ResetText();
            ServicesLabelCoeffUpCurrTrPhaseB.ResetText();
            ServicesLabelCoeffDownCurrTrPhaseB.ResetText();
            ServicesLabelVerifDateCurrTrPhaseB.ResetText();
            ServicesLabelVerifIntervCurrTrPhaseB.ResetText();
            ServicesLabelVerifNextDateCurrTrPhaseB.ResetText();

            ServicesLabelTypeVoltTrPhaseB.ResetText();
            ServicesLabelYearOfManufVoltTrPhaseB.ResetText();
            ServicesLabelAccClassVoltTrPhaseB.ResetText();
            ServicesLabelSerialNumbVoltTrPhaseB.ResetText();
            ServicesLabelCoeff1VoltTrPhaseB.ResetText();
            ServicesLabelCoeff2VoltTrPhaseB.ResetText();
            ServicesLabelVerifDateVoltTrPhaseB.ResetText();
            ServicesLabelVerifIntervVoltTrPhaseB.ResetText();
            ServicesLabelVerifNextDateVoltTrPhaseB.ResetText();

            ServicesLabelTypeCurrTrPhaseC.ResetText();
            ServicesLabelYearOfManufCurrTrPhaseC.ResetText();
            ServicesLabelAccClassCurrTrPhaseC.ResetText();
            ServicesLabelSerialNumbCurrTrPhaseC.ResetText();
            ServicesLabelCoeff1CurrTrPhaseC.ResetText();
            ServicesLabelCoeff2CurrTrPhaseC.ResetText();
            ServicesLabelVerifDateCurrTrPhaseC.ResetText();
            ServicesLabelVerifIntervCurrTrPhaseC.ResetText();
            ServicesLabelVerifNextDateCurrTrPhaseC.ResetText();

            ServicesLabelTypeVoltTrPhaseC.ResetText();
            ServicesLabelYearOfManufVoltTrPhaseC.ResetText();
            ServicesLabelAccClassVoltTrPhaseC.ResetText();
            ServicesLabelSerialNumbVoltTrPhaseC.ResetText();
            ServicesLabelCoeff1VoltTrPhaseC.ResetText();
            ServicesLabelCoeff2VoltTrPhaseC.ResetText();
            ServicesLabelVerifDateVoltTrPhaseC.ResetText();
            ServicesLabelVerifIntervVoltTrPhaseC.ResetText();
            ServicesLabelVerifNextDateVoltTrPhaseC.ResetText();

            ServicesLabelTypeMeter.ResetText();
            ServicesLabelYearOfManufMeter.ResetText();
            ServicesLabelAccClassMeter.ResetText();
            ServicesLabelSerialNumbMeter.ResetText();
            ServicesLabelVerifDateMeter.ResetText();
            ServicesLabelVerifIntervMeter.ResetText();
            ServicesLabelVerifNextDateMeter.ResetText();
        }

        /// <summary>
        /// Событие ServicesAddNewDistrButton_Click добавления сетевого района
        /// создает окно для добавления сетевого района
        /// </summary>
        private void ServicesAddNewDistrButton_Click(object sender, EventArgs e)
        {
            AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow("Введите название сетевого района");
            addChangeElemServW.Text = $"Добавление сетевого района";
            addChangeElemServW.ShowDialog();

            if (addChangeElemServW.DialogResult == DialogResult.OK && !String.IsNullOrWhiteSpace(addChangeElemServW.NewNameOfElem))
            {
                Tools.AddNewDistrict(addChangeElemServW.NewNameOfElem);
                AddChangeDelElem = true;
                ServicesRefreshAfterAdd();
            }
            else
                ServicesTreeView.Select();
        }

        /// <summary>
        /// Событие ServicesAddNewElemButton_Click добавления элемента
        /// создает окно для добавления элемента
        /// </summary>
        private void ServicesAddNewElemButton_Click(object sender, EventArgs e)
        {
            if(elem !=null)
            {
                AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow($"Введите название {Tools.TypeOfNewElem(elem)} в {Tools.NameOfSelectedElem(elem)}", elem);
                addChangeElemServW.Text = $"Добавление {Tools.TypeOfNewElem(elem)}";
                addChangeElemServW.ShowDialog();

                if (addChangeElemServW.DialogResult == DialogResult.OK && !String.IsNullOrWhiteSpace(addChangeElemServW.NewNameOfElem))
                {
                    if (elem is District)
                    {
                        District d = elem as District;
                        Tools.AddNewOandGPF(d.Id, addChangeElemServW.NewNameOfElem);
                    }
                    else if (elem is OandGPF)
                    {
                        OandGPF o = elem as OandGPF;
                        Tools.AddNewProductionFacility(o.Id, addChangeElemServW.NewNameOfElem);
                    }
                    else if (elem is ProductionFacility)
                    {
                        ProductionFacility pf = elem as ProductionFacility;
                        Tools.AddNewLocationOfMi(addChangeElemServW.NewNumberLocationOfElem, addChangeElemServW.NewNameOfElem, pf.Id);
                    }
                    ServicesRefreshAfterAdd(elem);
                    AddChangeDelElem = true;
                }
                else
                    ServicesTreeView.Select();
            }
            else
                MessageBox.Show("Создайте сетевой район!");
        }

        /// <summary>
        /// Событие ServicesChangeElemButton_Click изменения элемента
        /// создает окно для изменения информации о выбранном элементе
        /// </summary>
        private void ServicesChangeElemButton_Click(object sender, EventArgs e)
        {
            if (elem != null)
            {
                if (elem is District)
                {
                    District d = elem as District;
                    AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow($"Измените название {Tools.TypeOfChangeElem(elem)}", d.NameDistrict);
                    addChangeElemServW.Text = $"Изменение {Tools.TypeOfChangeElem(elem)}";
                    addChangeElemServW.ShowDialog();

                    if (addChangeElemServW.DialogResult == DialogResult.OK)
                    {
                        Tools.ChangeDisrict(d.Id, addChangeElemServW.NewNameOfElem);
                        AddChangeDelElem = true;
                        d.NameDistrict = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.SelectedNode.Text = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.Select();
                        ServicesLabelTabControlPassportForName.Text = addChangeElemServW.NewNameOfElem;
                    }
                    else
                        ServicesTreeView.Select();
                }
                else if (elem is OandGPF)
                {
                    OandGPF o = elem as OandGPF;
                    AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow($"Измените название {Tools.TypeOfChangeElem(elem)}", o.NameOandGPF);
                    addChangeElemServW.Text = $"Изменение {Tools.TypeOfChangeElem(elem)}";
                    addChangeElemServW.ShowDialog();

                    if (addChangeElemServW.DialogResult == DialogResult.OK)
                    {
                        Tools.ChangeOandGPF(o.Id, addChangeElemServW.NewNameOfElem);
                        AddChangeDelElem = true;
                        o.NameOandGPF = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.SelectedNode.Text = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.Select();
                        ServicesLabelTabControlPassportForName.Text = addChangeElemServW.NewNameOfElem;
                    }
                    else
                        ServicesTreeView.Select();
                }
                else if (elem is ProductionFacility)
                {
                    ProductionFacility pf = elem as ProductionFacility;
                    AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow($"Измените название {Tools.TypeOfChangeElem(elem)}", pf.NameProductFacility);
                    addChangeElemServW.Text = $"Изменение {Tools.TypeOfChangeElem(elem)}";
                    addChangeElemServW.ShowDialog();

                    if (addChangeElemServW.DialogResult == DialogResult.OK)
                    {
                        Tools.ChangeProductionFacility(pf.Id, addChangeElemServW.NewNameOfElem);
                        AddChangeDelElem = true;
                        pf.NameProductFacility = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.SelectedNode.Text = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.Select();
                        ServicesLabelTabControlPassportForName.Text = addChangeElemServW.NewNameOfElem;
                    }
                    else
                        ServicesTreeView.Select();
                }
                else if (elem is LocationOfMI)
                {
                    LocationOfMI l = elem as LocationOfMI;
                    AddChangeElemServicesWindow addChangeElemServW = new AddChangeElemServicesWindow($"Измените название {Tools.TypeOfChangeElem(elem)}", l.NameLocationOfMI, l.NumberLocationOfMI, l.ProductionFacilityId);
                    addChangeElemServW.Text = $"Изменение {Tools.TypeOfChangeElem(elem)}";
                    addChangeElemServW.ShowDialog();

                    if (addChangeElemServW.DialogResult == DialogResult.OK)
                    {
                        Tools.ChangeLocationOfMi(l.Id, addChangeElemServW.NewNameOfElem, addChangeElemServW.NewNumberLocationOfElem);
                        AddChangeDelElem = true;
                        l.NameLocationOfMI = addChangeElemServW.NewNameOfElem;
                        l.NumberLocationOfMI = addChangeElemServW.NewNumberLocationOfElem;
                        ServicesTreeView.SelectedNode.Text = addChangeElemServW.NewNameOfElem;
                        ServicesTreeView.Select();
                        ServicesLabelTabControlPassportForName.Text = addChangeElemServW.NewNameOfElem;
                        ServicesLabelTabControlPassportForNumber.Text = addChangeElemServW.NewNumberLocationOfElem.ToString();
                    }
                    else
                        ServicesTreeView.Select();
                }
            }
            else
                MessageBox.Show("Выбирете элемент для изменения!");
        }

        /// <summary>
        /// Событие ServicesDeleteElemButton_Click удаления элемента
        /// проверяе тест ьли зависымые элементы у удаляемого элемента
        /// если нет, создает окно для подтверждения удаления элемента
        /// если да, то предупреждает и отменяет удаление 
        /// </summary>
        private void ServicesDeleteElemButton_Click(object sender, EventArgs e)
        {
            if (elem != null)
            {
                if (Tools.GetCountOfElemInDict(elem) == 0)
                {
                    if (elem is District)
                    {
                        District d = elem as District;
                        VerificationDelWindow vDW = new VerificationDelWindow(d.NameDistrict, d);
                        vDW.Text = "Подтверждение";
                        vDW.ShowDialog();
                        if (vDW.DialogResult == DialogResult.OK)
                        {
                            Tools.DeleteDisrict(d.Id);
                            AddChangeDelElem = true;
                            servicesDictOfDistrict.Remove(d.Id);
                            ServicesTreeView.Nodes.Clear();
                            ServicesFillMiNodes();
                            if(servicesDictOfDistrict.Count!=0)
                            {
                                ServicesTreeView.SelectedNode = ServicesTreeView.Nodes[0];
                                ServicesTreeView.Select();
                            }
                            else
                            {
                                elem = null;
                                ServicesLabelTabControlPassportForName.Text = "";
                                ServicesGroupBoxButton.Text = "Действия";
                            }
                        }
                        else
                            ServicesTreeView.Select();
                    }
                    else if (elem is OandGPF)
                    {
                        OandGPF o = elem as OandGPF;
                        VerificationDelWindow vDW = new VerificationDelWindow(o.NameOandGPF, o);
                        vDW.Text = "Подтверждение";
                        vDW.ShowDialog();
                        if (vDW.DialogResult == DialogResult.OK)
                        {
                            Tools.DeleteOandGPF(o.Id);
                            AddChangeDelElem = true;
                            elem = (Element)ServicesTreeView.SelectedNode.Parent.Tag;
                            ServicesRefreshAfterDel(elem);
                        }
                        else
                            ServicesTreeView.Select();
                    }
                    else if (elem is ProductionFacility)
                    {
                        ProductionFacility pf = elem as ProductionFacility;
                        VerificationDelWindow vDW = new VerificationDelWindow(pf.NameProductFacility, pf);
                        vDW.Text = "Подтверждение";
                        vDW.ShowDialog();
                        if (vDW.DialogResult == DialogResult.OK)
                        {
                            Tools.DeleteProductionFacilility(pf.Id);
                            AddChangeDelElem = true;
                            elem = (Element)ServicesTreeView.SelectedNode.Parent.Tag;
                            ServicesRefreshAfterDel(elem);
                        }
                        else
                            ServicesTreeView.Select();
                    }
                    else if (elem is LocationOfMI)
                    {
                        LocationOfMI l = elem as LocationOfMI;
                        VerificationDelWindow vDW = new VerificationDelWindow(l.NameLocationOfMI, l);
                        vDW.Text = "Подтверждение";
                        vDW.ShowDialog();
                        if (vDW.DialogResult == DialogResult.OK)
                        {
                            Tools.DelLog(l.Id);
                            Tools.DeleteLocationOfMi(l.Id);
                            AddChangeDelElem = true;
                            elem = (Element)ServicesTreeView.SelectedNode.Parent.Tag;
                            ServicesRefreshAfterDel(elem);
                        }
                        else
                            ServicesTreeView.Select();
                    }
                }
                else
                {
                    MessageBox.Show("У данного элемента имеются зависимые элементы! Удалите сначала их!");
                    ServicesTreeView.Select();
                }
            }
            else
                MessageBox.Show("Выбирете элемент для удаления!");
        }

        /// <summary>
        /// Метод ServicesRefreshAfterAdd обновляет информацию после добавления сетевого района
        /// </summary>
        private void ServicesRefreshAfterAdd()
        {
            ServicesTabControlMI.Visible = false;
            servicesDictOfDistrict.Clear();
            ServicesTreeView.Nodes.Clear();

            Tools.GetDistricts(servicesDictOfDistrict);
            ServicesFillMiNodes();
            if(servicesDictOfDistrict.Count!=0)
            {
                ServicesTreeView.SelectedNode = ServicesTreeView.Nodes[0];
                ServicesTreeView.Select();
            }
            else
                MessageBox.Show("Создайте сетевой район!");
        }

        /// <summary>
        /// Метод ServicesRefreshAfterAdd обновляет информацию после добавления элемента
        /// </summary>
        /// <param name="elem">экземпляр объекта, для которого необходимо обновить зависимые элементы</param>
        private void ServicesRefreshAfterAdd(Element elem)
        {
            if (!(elem is LocationOfMI))
            {
                ServicesTreeView.SelectedNode.Nodes.Clear();
                if (elem is District)
                {
                    District d = elem as District;
                    d.OandGPFs.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, d);
                }
                if (elem is OandGPF)
                {
                    OandGPF o = elem as OandGPF;
                    o.ProductionFacilities.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, o);
                }
                if (elem is ProductionFacility)
                {
                    ProductionFacility pf = elem as ProductionFacility;
                    pf.LocationOfMIs.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, pf);
                }
                ServicesTreeView.SelectedNode.Toggle();
                ServicesTreeView.SelectedNode.Expand();
                ServicesTreeView.Select();
            }
            else if (elem is LocationOfMI)
            {
                LocationOfMI l = elem as LocationOfMI;
                l.MeasurInstrs.Clear();
                Tools.GetMIs(l);
                ServicesShowMI(l);
            }
        }

        /// <summary>
        /// Метод ServicesRefreshAfterDel обновляет информацию после удаления элемента
        /// </summary>
        /// <param name="elem">экземпляр объекта, для которого необходимо обновить зависимые элементы</param>
        private void ServicesRefreshAfterDel(Element elem)
        {
            if (!(elem is LocationOfMI))
            {
                ServicesTreeView.SelectedNode.Parent.Nodes.Clear();
                if (elem is District)
                {
                    District d = elem as District;
                    d.OandGPFs.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, d);
                }
                if (elem is OandGPF)
                {
                    OandGPF o = elem as OandGPF;
                    o.ProductionFacilities.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, o);
                }
                if (elem is ProductionFacility)
                {
                    ProductionFacility pf = elem as ProductionFacility;
                    pf.LocationOfMIs.Clear();
                    ServicesFillTreeNodes(ServicesTreeView.SelectedNode, pf);
                }
                ServicesTreeView.SelectedNode.Toggle();
                ServicesTreeView.SelectedNode.Expand();
                ServicesTreeView.Select();
            }
            else if (elem is LocationOfMI)
            {
                LocationOfMI l = elem as LocationOfMI;
                ServicesShowMI(l);
            }
        }

        /// <summary>
        /// Событие ServicesRefreshElemButton_Click обновление информации
        /// </summary>
        private void ServicesRefreshElemButton_Click(object sender, EventArgs e)
        {
            ServicesRefreshAfterAdd();
        }

        /// <summary>
        /// Событие ServicesAddTrPhaseAButton_Click добавления трансформатора тока фазы А
        /// </summary>
        private void ServicesAddTrPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(1, 1, "Трансформатор тока уже существует!", "Введите информацию по трансформатору тока фазы А", "Добавить трансформатор тока фазы А");
        }

        /// <summary>
        /// Событие ServicesAddTrPhaseBButton_Click добавления трансформатора тока фазы В
        /// </summary>
        private void ServicesAddTrPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(1, 2, "Трансформатор тока уже существует!", "Введите информацию по трансформатору тока фазы B", "Добавить трансформатор тока фазы B");
        }

        /// <summary>
        /// Событие ServicesAddTrPhaseCButton_Click добавления трансформатора тока фазы С
        /// </summary>
        private void ServicesAddTrPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(1, 3, "Трансформатор тока уже существует!", "Введите информацию по трансформатору тока фазы C", "Добавить трансформатор тока фазы C");
        }

        /// <summary>
        /// Событие ServicesAddVoltPhaseAButton_Click добавления трансформатора напряжения фазы А
        /// </summary>
        private void ServicesAddVoltPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(2, 1, "Трансформатор напряжения уже существует!", "Введите информацию по трансформатору напряжения фазы А", "Добавить трансформатор напряжения фазы А");
        }

        /// <summary>
        /// Событие ServicesAddVoltPhaseBButton_Click добавления трансформатора напряжения фазы В
        /// </summary>
        private void ServicesAddVoltPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(2, 2, "Трансформатор напряжения уже существует!", "Введите информацию по трансформатору напряжения фазы B", "Добавить трансформатор напряжения фазы B");
        }

        /// <summary>
        /// Событие ServicesAddVoltPhaseCButton_Click добавления трансформатора напряжения фазы С
        /// </summary>
        private void ServicesAddVoltPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesAddTrMI(2, 3, "Трансформатор напряжения уже существует!", "Введите информацию по трансформатору напряжения фазы C", "Добавить трансформатор напряжения фазы C");
        }

        /// <summary>
        /// Событие ServicesDeleteTrPhaseAButton_Click удаления трансформатора тока фазы А
        /// </summary>
        private void ServicesDeleteTrPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(1, 1, "Трансформатора тока на фазе А нет! Сначала добавьте!", "на фазе А трансформатор тока");
        }

        /// <summary>
        /// Событие ServicesDeleteTrPhaseBButton_Click удаления трансформатора тока фазы В
        /// </summary>
        private void ServicesDeleteTrPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(1, 2, "Трансформатора тока на фазе B нет! Сначала добавьте!", "на фазе B трансформатор тока");
        }

        /// <summary>
        /// Событие ServicesDeleteTrPhaseCButton_Click удаления трансформатора тока фазы С
        /// </summary>
        private void ServicesDeleteTrPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(1, 3, "Трансформатора тока на фазе C нет! Сначала добавьте!", "на фазе C трансформатор тока");
        }

        /// <summary>
        /// Событие ServicesDeleteVoltPhaseAButton_Click удаления трансформатора напряжения фазы А
        /// </summary>
        private void ServicesDeleteVoltPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(2, 1, "Трансформатора напряжения на фазе А нет! Сначала добавьте!", "на фазе А трансформатор напряжения");
        }

        /// <summary>
        /// Событие ServicesDeleteVoltPhaseBButton_Click удаления трансформатора напряжения фазы В
        /// </summary>
        private void ServicesDeleteVoltPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(2, 2, "Трансформатора напряжения на фазе В нет! Сначала добавьте!", "на фазе В трансформатор напряжения");
        }

        /// <summary>
        /// Событие ServicesDeleteVoltPhaseCButton_Click удаления трансформатора напряжения фазы С
        /// </summary>
        private void ServicesDeleteVoltPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(2, 3, "Трансформатора напряжения на фазе С нет! Сначала добавьте!", "на фазе С трансформатор напряжения");
        }

        /// <summary>
        /// Событие ServicesDeleteMeterButton_Click удаления счетчика
        /// </summary>
        private void ServicesDeleteMeterButton_Click(object sender, EventArgs e)
        {
            ServicesDelMI(3, 8, "Счетчика нет! Сначала добавьте!", "счетчик");
        }

        /// <summary>
        /// Событие ServicesChangeTrPhaseAButton_Click изменения трансформатора тока фазы А
        /// </summary>
        private void ServicesChangeTrPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(1, 1, "Трансформатор тока не существует!", "Измените информацию по трансформатору тока фазы А", "Изменить трансформатор тока фазы А");
        }

        /// <summary>
        /// Событие ServicesChangeCurrTrPhaseBButton_Click изменения трансформатора тока фазы В
        /// </summary>
        private void ServicesChangeCurrTrPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(1, 2, "Трансформатор тока не существует!", "Измените информацию по трансформатору тока фазы B", "Изменить трансформатор тока фазы B");
        }

        /// <summary>
        /// Событие ServicesChangeCurrTrPhaseCButton_Click изменения трансформатора тока фазы С
        /// </summary>
        private void ServicesChangeCurrTrPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(1, 3, "Трансформатор тока не существует!", "Измените информацию по трансформатору тока фазы C", "Изменить трансформатор тока фазы C");
        }

        /// <summary>
        /// Событие ServicesChangeVoltPhaseAButton_Click изменения трансформатора напряжения фазы А
        /// </summary>
        private void ServicesChangeVoltPhaseAButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(2, 1, "Трансформатор напряжения не существует!", "Измените информацию по трансформатору напряжения фазы А", "Изменить трансформатор напряжения фазы А");
        }

        /// <summary>
        /// Событие ServicesChangeVoltPhaseBButton_Click изменения трансформатора напряжения фазы В
        /// </summary>
        private void ServicesChangeVoltPhaseBButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(2, 2, "Трансформатор напряжения не существует!", "Измените информацию по трансформатору напряжения фазы В", "Изменить трансформатор напряжения фазы В");
        }

        /// <summary>
        /// Событие ServicesChangeVoltPhaseCButton_Click изменения трансформатора напряжения фазы С
        /// </summary>
        private void ServicesChangeVoltPhaseCButton_Click(object sender, EventArgs e)
        {
            ServicesChangeTrMI(2, 3, "Трансформатор напряжения не существует!", "Измените информацию по трансформатору напряжения фазы С", "Изменить трансформатор напряжения фазы С");
        }

        /// <summary>
        /// Событие ServicesChangeMeterButton_Click проверяет есть ли счетчик на данном месте и
        /// создает окно для добавления счетчика
        /// </summary>
        private void ServicesAddMeterButton_Click(object sender, EventArgs e)
        {
            bool isExcist = false;
            LocationOfMI l = elem as LocationOfMI;
            Dictionary<int, MeasInstrFromRef> temp = new Dictionary<int, MeasInstrFromRef>();
            var keys = servicesDictOfMeasInstrFromRef.Keys;
            foreach (var k in keys)
                if (servicesDictOfMeasInstrFromRef[k].TypeOfMiId == 3)
                    temp.Add(k, servicesDictOfMeasInstrFromRef[k]);
            var keysMi = l.MeasurInstrs.Keys;
            foreach (var k in keysMi)
                if (temp.ContainsKey(l.MeasurInstrs[k].MiFromRefId) && l.MeasurInstrs[k].PhaseId == 8)
                    isExcist = true;
            if (isExcist)
                MessageBox.Show("Счетчик уже существует!");
            else if(temp.Count!=0)
            {
                MeasurInstr newMI = new MeasurInstr();
                AddChangeMeterMIWindow addWind = new AddChangeMeterMIWindow("Введите информацию по счетчику", temp, l.Id, newMI);
                addWind.Text = "Добавить счетчик";
                addWind.ShowDialog();
                if (addWind.DialogResult == DialogResult.OK)
                {
                    Tools.AddNewMi(newMI);
                    AddChangeDelElem = true;
                    ServicesRefreshAfterAdd(l);
                }
                ServicesTreeView.Select();
            }
            else
                MessageBox.Show("Заполните справочник счетчиков!");
        }

        /// <summary>
        /// Событие ServicesChangeMeterButton_Click проверяет есть ли счетчик на данном месте и
        /// создает окно для изменения счетчика
        /// </summary>
        private void ServicesChangeMeterButton_Click(object sender, EventArgs e)
        {
            bool isExcist = false;
            LocationOfMI l = elem as LocationOfMI;
            Dictionary<int, MeasInstrFromRef> temp = new Dictionary<int, MeasInstrFromRef>();
            var keys = servicesDictOfMeasInstrFromRef.Keys;
            foreach (var k in keys)
                if (servicesDictOfMeasInstrFromRef[k].TypeOfMiId == 3)
                    temp.Add(k, servicesDictOfMeasInstrFromRef[k]);
            MeasurInstr MI = new MeasurInstr();
            var keysMi = l.MeasurInstrs.Keys;
            foreach (var k in keysMi)
            {
                if (temp.ContainsKey(l.MeasurInstrs[k].MiFromRefId) && l.MeasurInstrs[k].PhaseId == 8)
                {
                    isExcist = true;
                    MI = l.MeasurInstrs[k];
                    break;
                }
            }
            if (!isExcist)
                MessageBox.Show("Счетчик не существует!");
            else
            {
                AddChangeMeterMIWindow addWind = new AddChangeMeterMIWindow("Измените информацию по счетчику", temp, MI);
                addWind.Text = "Изменить счетчик";
                addWind.ShowDialog();
                if (addWind.DialogResult == DialogResult.OK)
                {
                    l.MeasurInstrs.Remove(MI.Id);
                    Tools.ChangeMi(MI);
                    AddChangeDelElem = true;
                    l.MeasurInstrs.Add(MI.Id, MI);
                    ServicesRefreshAfterDel(l);
                }
                ServicesTreeView.Select();
            }
        }

        /// <summary>
        /// Метод ServicesAddTrMI проверяет установлен ли трансформатор и
        /// создает окно для добавления трансформаторов
        /// </summary>
        /// <param name="idTypeMI">id вида СИ из справочника</param>
        /// <param name="phaseId">id фазы на который добавялем СИ</param>
        /// <param name="MessErr">сообщение для пользователя в случае ошибки</param>
        /// <param name="MessForPers">сообщение для пользователя при удалении СИ</param>
        /// <param name="windText">текст названия окна</param>
        private void ServicesAddTrMI(int idTypeMI, int phaseId, string MessErr, string MessForPers, string windText)
        {
            bool isExcist = false;
            LocationOfMI l = elem as LocationOfMI;
            Dictionary<int, MeasInstrFromRef> temp = new Dictionary<int, MeasInstrFromRef>();
            var keys = servicesDictOfMeasInstrFromRef.Keys;
            foreach (var k in keys)
                if (servicesDictOfMeasInstrFromRef[k].TypeOfMiId == idTypeMI)
                    temp.Add(k, servicesDictOfMeasInstrFromRef[k]);
            var keysMi = l.MeasurInstrs.Keys;
            foreach (var k in keysMi)
                if (temp.ContainsKey(l.MeasurInstrs[k].MiFromRefId) && l.MeasurInstrs[k].PhaseId == phaseId)
                    isExcist = true;
            if (isExcist)
                MessageBox.Show(MessErr);
            else if(temp.Count!=0)
            {
                MeasurInstr newMI = new MeasurInstr();
                AddChangeTrMIWindow addWind = new AddChangeTrMIWindow(MessForPers, temp, l.Id, phaseId, newMI);
                addWind.Text = windText;
                addWind.ShowDialog();
                if (addWind.DialogResult == DialogResult.OK)
                {
                    Tools.AddNewMi(newMI);
                    AddChangeDelElem = true;
                    ServicesRefreshAfterAdd(l);
                }
                ServicesTreeView.Select();
            }
            else
                MessageBox.Show("Заполните справочник трансформаторов!");
        }

        /// <summary>
        /// Метод ServicesDelMI проверяет установлено ли СИ и 
        /// создает окно для подтверждения удаления средства измерения
        /// </summary>
        /// <param name="idTypeMI">id вида СИ из справочника</param>
        /// <param name="phaseId">id фазы на которой установлено СИ для удаления</param>
        /// <param name="MessErr">сообщение для пользователя в случае ошибки</param>
        /// <param name="MessForPers">сообщение для пользователя при удалении СИ</param>
        private void ServicesDelMI(int idTypeMI, int phaseId, string MessErr, string MessForPers)
        {
            string tempSerialNumMI = "";
            string tempDelTypeMU="";
            bool isExcist = false;
            LocationOfMI l = elem as LocationOfMI;
            MeasurInstr mI = new MeasurInstr();
            var temp = from item in servicesDictOfMeasInstrFromRef where item.Value.TypeOfMiId == idTypeMI select item.Value.Id;
            var keysMi = l.MeasurInstrs.Keys;
            foreach (var k in keysMi)
            {
                if (temp.Contains(l.MeasurInstrs[k].MiFromRefId) && l.MeasurInstrs[k].PhaseId == phaseId)
                {
                    isExcist = true;
                    mI = l.MeasurInstrs[k];
                    tempSerialNumMI = mI.SerialNumber;
                    tempDelTypeMU = servicesDictOfMeasInstrFromRef[mI.MiFromRefId].NameMIFromRef;
                }
            }
            if (!isExcist)
                MessageBox.Show(MessErr);
            else
            {
                VerificationDelWindow delWind = new VerificationDelWindow($"{MessForPers} {tempDelTypeMU} с заводским номером {tempSerialNumMI}", new MeasurInstr());
                delWind.Text = "Подтверждение";
                delWind.ShowDialog();
                if (delWind.DialogResult == DialogResult.OK)
                {
                    Tools.AddLog(mI, "Удален");
                    Tools.DelMi(mI.Id);
                    AddChangeDelElem = true;
                    l.MeasurInstrs.Remove(mI.Id);
                    ServicesRefreshAfterDel(l);
                }
                ServicesTreeView.Select();
            }
        }

        /// <summary>
        /// Метод ServicesChangeTrMI проверяет установлен ли трансформатор и
        /// создает окно для изменения информации о трансформаторах
        /// </summary>
        /// <param name="idTypeMI">id вида СИ из справочника</param>
        /// <param name="phaseId">id фазы на которой установлено СИ для изменения</param>
        /// <param name="MessErr">сообщение для пользователя в случае ошибки</param>
        /// <param name="MessForPers">сообщение для пользователя при изменени информации</param>
        /// <param name="windText">текст названия окна</param>
        private void ServicesChangeTrMI(int idTypeMI, int phaseId, string MessErr, string MessForPers, string windText)
        {
            bool isExcist = false;
            LocationOfMI l = elem as LocationOfMI;
            Dictionary<int, MeasInstrFromRef> temp = new Dictionary<int, MeasInstrFromRef>();
            var keys = servicesDictOfMeasInstrFromRef.Keys;
            foreach (var k in keys)
                if (servicesDictOfMeasInstrFromRef[k].TypeOfMiId == idTypeMI)
                    temp.Add(k, servicesDictOfMeasInstrFromRef[k]);
            MeasurInstr MI = new MeasurInstr();
            var keysMi = l.MeasurInstrs.Keys;
            foreach (var k in keysMi)
            {
                if (temp.ContainsKey(l.MeasurInstrs[k].MiFromRefId) && l.MeasurInstrs[k].PhaseId == phaseId)
                {
                    isExcist = true;
                    MI = l.MeasurInstrs[k];
                    break;
                }
            }
            if (!isExcist)
                MessageBox.Show(MessErr);
            else
            {
                AddChangeTrMIWindow addWind = new AddChangeTrMIWindow(MessForPers, temp, MI);
                addWind.Text = windText;
                addWind.ShowDialog();
                if (addWind.DialogResult == DialogResult.OK)
                {
                    l.MeasurInstrs.Remove(MI.Id);
                    Tools.ChangeMi(MI);
                    AddChangeDelElem = true;
                    l.MeasurInstrs.Add(MI.Id, MI);
                    ServicesRefreshAfterDel(l);
                }
                ServicesTreeView.Select();
            }
        }

        /// <summary>
        /// Событие HistiryWindCurrTrPhaseA_Click создает окно истории изменений инфо о тр.тока фазы А
        /// </summary>
        private void HistiryWindCurrTrPhaseA_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 1);
            hW.Text = "История изменений/удалений трансформаторов тока фазы А";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindCurrTrPhaseB_Click создает окно истории изменений инфо о тр.тока фазы В
        /// </summary>
        private void HistiryWindCurrTrPhaseB_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 2);
            hW.Text = "История изменений/удалений трансформаторов тока фазы В";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindCurrTrPhaseC_Click создает окно истории изменений инфо о тр.тока фазы С
        /// </summary>
        private void HistiryWindCurrTrPhaseC_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 1, 3);
            hW.Text = "История изменений/удалений трансформаторов тока фазы С";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindVoltTrPhaseA_Click создает окно истории изменений инфо о тр.напряжения фазы А
        /// </summary>
        private void HistiryWindVoltTrPhaseA_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 1);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы А";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindVoltTrPhaseB_Click создает окно истории изменений инфо о тр.напряжения фазы В
        /// </summary>
        private void HistiryWindVoltTrPhaseB_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 2);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы В";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindVoltTrPhaseC_Click создает окно истории изменений инфо о тр.напряжения фазы С
        /// </summary>
        private void HistiryWindVoltTrPhaseC_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 2, 3);
            hW.Text = "История изменений/удалений трансформаторов напряжения фазы С";
            hW.ShowDialog();
        }

        /// <summary>
        /// Событие HistiryWindMeter_Click создает окно истории изменений инфо о счетчике
        /// </summary>
        private void HistiryWindMeter_Click(object sender, EventArgs e)
        {
            LocationOfMI l = elem as LocationOfMI;
            HistoryWindow hW = new HistoryWindow(l.Id, 3, 8);
            hW.Text = "История изменений/удалений счетчиков";
            hW.ShowDialog();
        }
    }
}
