using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MISystem.Classes
{
    public static class Tools
    {
        /// <summary>
        /// Метод ChangeRefElem обновляет название типа СИ в таблице справочник
        /// с указанным id
        /// </summary>
        /// <param name="idMIFromRef">id тип СИ</param>
        /// <param name="newNameMIFromRef">новое имя типа СИ</param>
        public static void ChangeRefElem(int idMIFromRef, string newNameMIFromRef)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE RefOfMI SET nameMIFromRef = '{newNameMIFromRef}' WHERE idMIFromRef = {idMIFromRef}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddRefElem добавляет новый тип СИ в таблицу справочник
        /// </summary>
        /// <param name="typeOfMiId">id вида СИ</param>
        /// <param name="nameMIFromRef">имя нового типа СИ</param>
        public static void AddRefElem(int typeOfMiId, string nameMIFromRef)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO RefOfMI (nameMIFromRef, typeOfMiId) VALUES ('{nameMIFromRef}', {typeOfMiId})";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DeleteRefElem удаляет тип СИ из таблицы справочника с указанным id
        /// </summary>
        /// <param name="idMIFromRef">id типа СИ</param>
        public static void DeleteRefElem(int idMIFromRef)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM RefOfMI WHERE idMIFromRef = {idMIFromRef}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод RefreshRefElem выгружает перечень типов СИ из таблицы справочника
        /// RefOfMI указанного вида, создает экземпляр типа СИ и заносит его в словарь
        /// </summary>
        /// <param name="dictMIRef">словарь типов СИ</param>
        /// <param name="typeOfMiId">id вида СИ</param>
        public static void RefreshRefElem(Dictionary<int, MeasInstrFromRef> dictMIRef, int typeOfMiId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select * from RefOfMI WHERE typeOfMiId = {typeOfMiId}", con);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MeasInstrFromRef m = new MeasInstrFromRef();
                    m.Id = (int)dr[$"idMIFromRef"];
                    m.NameMIFromRef = dr[$"nameMIFromRef"].ToString();
                    m.TypeOfMiId = (int)dr[$"typeOfMiId"];
                    dictMIRef.Add(m.Id, m);
                }
                dr.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetCountMIWithRefId подсчитывает количесво записей о СИ
        /// в таблице СИ с указанным типом СИ
        /// </summary>
        /// <param name="miFromRefId">тип СИ</param>
        public static int GetCountMIWithRefId(int miFromRefId)
        {
            int count = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select COUNT(*) from MeasurInstr WHERE miFromRefId = {miFromRefId}", con);
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return count;
        }

        /// <summary>
        /// Метод GetCountMiHistoryWithRefId подсчитывает количесво записей о СИ
        /// в таблице истории СИ с указанным типом СИ
        /// </summary>
        /// <param name="miFromRefId">тип СИ</param>
        public static int GetCountMiHistoryWithRefId(int miFromRefId)
        {
            int count = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select COUNT(*) from Log WHERE logMiFromRefId = {miFromRefId}", con);
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return count;
        }

        /// <summary>
        /// Метод GetCountMIWithSerial подсчитывает количесво СИ
        /// в таблице СИ с указанным серийным номером
        /// </summary>
        /// <param name="serialNumber">серийный номер</param>
        public static int GetCountMIWithSerial(string serialNumber)
        {
            int count = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select COUNT(*) from MeasurInstr WHERE serialNumber = '{serialNumber}'", con);
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return count;
        }

        /// <summary>
        /// Метод GetCountMiHistoryWithSerial подсчитывает количесво СИ
        /// в таблице истории СИ с указанным серийным номером
        /// </summary>
        /// <param name="serialNumber">серийный номер</param>
        public static int GetCountMiHistoryWithSerial(string serialNumber)
        {
            int count = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select COUNT(*) from Log WHERE logSerialNumber = '{serialNumber}'", con);
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return count;
        }

        /// <summary>
        /// Метод GetDistricts выгружает сетевые районы
        /// из таблицы сетевых районов, создает экземпляр сетевонр района и добавляет его в словарь
        /// </summary>
        /// <param name="dictOfDistrict">словарь сетевых районов</param>
        public static void GetDistricts(Dictionary<int, District> dictOfDistrict)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdDistr = new SqlCommand("select * from Districts", con);
                var drDistr = cmdDistr.ExecuteReader();
                while (drDistr.Read())
                {
                    District d = new District();
                    d.Id = (int)drDistr["idDistrict"];
                    d.NameDistrict = drDistr["nameDistrict"].ToString();
                    dictOfDistrict.Add(d.Id, d);
                }
                drDistr.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddNewDistrict добавляет новый сетевой район
        /// в таблицу сетевых районов
        /// </summary>
        /// <param name="nameDistrict">название сетевого района</param>
        public static void AddNewDistrict(string nameDistrict)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO Districts (nameDistrict) VALUES ('{nameDistrict}')";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод ChangeDisrict изменяет название существующего сетевого района
        /// в таблице сетевых районов по id
        /// </summary>
        /// <param name="idDistrict">id сетевого района</param>
        /// <param name="newNameDistrict">новое название сетевого района</param>
        public static void ChangeDisrict(int idDistrict, string newNameDistrict)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE Districts SET nameDistrict = '{newNameDistrict}' WHERE idDistrict = {idDistrict}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DeleteDisrict удаляет запись о сетевом районе
        /// из таблицы сетевых районов по id
        /// </summary>
        /// <param name="idDistrict">id сетевого района</param>
        public static void DeleteDisrict(int idDistrict)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM Districts WHERE idDistrict = {idDistrict}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetOandGPFs выгружает ЦДНГ для определенного сетевого района
        /// из таблицы ЦДНГ, создает экземпляр ЦДНГ и добавляет его в словарь переданного сетевого района
        /// </summary>
        /// <param name="distr">экземпляр сетевого района, для которого выгружают перечнь ЦДНГ</param>
        public static void GetOandGPFs(District distr)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdOandGPF = new SqlCommand($"select * from OandGPF WHERE districtId = {distr.Id} ", con);
                var drOandGPF = cmdOandGPF.ExecuteReader();
                while (drOandGPF.Read())
                {
                    OandGPF o = new OandGPF();
                    o.Id = (int)drOandGPF["idOandGPF"];
                    o.NameOandGPF = drOandGPF["nameOandGPF"].ToString();
                    o.DistrictId = (int)drOandGPF["districtId"];
                    distr.OandGPFs.Add(o.Id, o);
                }
                drOandGPF.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddNewOandGPF добавляет новое ЦДНГ
        /// в таблицу ЦДНГ
        /// </summary>
        /// <param name="districtId">id сетевого района, к которому относится ЦДНГ</param>
        /// <param name="nameOandGPF">название ЦДНГ</param>
        public static void AddNewOandGPF(int districtId, string nameOandGPF)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO OandGPF (nameOandGPF, districtId) VALUES ('{nameOandGPF}', {districtId})";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод ChangeOandGPF изменяет название существующего ЦДНГ
        /// в таблице ЦДНГ по id
        /// </summary>
        /// <param name="idOandGPF">id ЦДНГ</param>
        /// <param name="newNameOandGPF">новое название ЦДНГ</param>
        public static void ChangeOandGPF(int idOandGPF, string newNameOandGPF)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE OandGPF SET nameOandGPF = '{newNameOandGPF}' WHERE idOandGPF = {idOandGPF}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DeleteOandGPF удаляет запись о ЦДНГ
        /// из таблицы ЦДНГ по id
        /// </summary>
        /// <param name="idOandGPF">id сетевого района</param>
        public static void DeleteOandGPF(int idOandGPF)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM OandGPF WHERE idOandGPF = {idOandGPF}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetProductionFacilities выгружает производственные объекты для определенного ЦДНГ
        /// из таблицы производственных объектов, создает экземпляр производственного объекта 
        /// и добавляет его в словарь переданного ЦДНГ
        /// </summary>
        /// <param name="o">экземпляр ЦДНГ, для которого выгружают перечнь производственных объектов</param>
        public static void GetProductionFacilities(OandGPF o)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdPF = new SqlCommand($"select * from ProductionFacilities WHERE OandGPFId = {o.Id}", con);
                var drPF = cmdPF.ExecuteReader();
                while (drPF.Read())
                {
                    ProductionFacility pf = new ProductionFacility();
                    pf.Id = (int)drPF["idProductFacility"];
                    pf.NameProductFacility = drPF["nameProductFacility"].ToString();
                    pf.OandGPFId = (int)drPF[$"OandGPFId"];
                    o.ProductionFacilities.Add(pf.Id, pf);
                }
                drPF.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddNewProductionFacility добавляет новый производственный объект
        /// в таблицу производственных объектов
        /// </summary>
        /// <param name="oAndGPFId">id ЦДНГ, к которому относится производственный объект</param>
        /// <param name="nameProductFacility">название произв.объекта</param>
        public static void AddNewProductionFacility(int oAndGPFId, string nameProductFacility)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO ProductionFacilities (nameProductFacility, OandGPFId) VALUES ('{nameProductFacility}', {oAndGPFId})";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод ChangeProductionFacility изменяет название существующего произв.объекта
        /// в таблице произв.объектов по id
        /// </summary>
        /// <param name="idProductFacility">id произв.объекта</param>
        /// <param name="newNamePrF">новое название произв.объекта</param>
        public static void ChangeProductionFacility(int idProductFacility, string newNamePrF)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE ProductionFacilities SET nameProductFacility = '{newNamePrF}' WHERE idProductFacility = {idProductFacility}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DeleteProductionFacilility удаляет запись о произв.объекте
        /// из таблицы произв.объектов по id
        /// </summary>
        /// <param name="idProductFacility">id произв.объекта</param>
        public static void DeleteProductionFacilility(int idProductFacility)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM ProductionFacilities WHERE idProductFacility = {idProductFacility}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetLocationOfMis выгружает места установки СИ для определенного произв.объекта
        /// из таблицы мест установки СИ, создает экземпляр места установки СИ 
        /// и добавляет его в словарь переданного произв.объекта
        /// </summary>
        /// <param name="pf">экземпляр произв.объекта, для которого выгружают перечнь мест установки СИ</param>
        public static void GetLocationOfMis(ProductionFacility pf)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdLocOfMi = new SqlCommand($"select * from LocationOfMI WHERE productionFacilityId = {pf.Id}", con);
                var drLocOfMi = cmdLocOfMi.ExecuteReader();
                while (drLocOfMi.Read())
                {
                    LocationOfMI lOfMI = new LocationOfMI();
                    lOfMI.Id = (int)drLocOfMi["idLocationOfMI"];
                    lOfMI.NumberLocationOfMI = (int)drLocOfMi["numberLocationOfMI"];
                    lOfMI.NameLocationOfMI = drLocOfMi["nameLocationOfMI"].ToString();
                    lOfMI.ProductionFacilityId = (int)drLocOfMi["productionFacilityId"];
                    pf.LocationOfMIs.Add(lOfMI.Id, lOfMI);
                }
                drLocOfMi.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddNewLocationOfMi добавляет новое место установки СИ
        /// в таблицу мест установки СИ
        /// </summary>
        /// <param name="numberLocationOfMI">номер места установки СИ</param>
        /// <param name="nameLocationOfMI">название места установки СИ</param>
        /// <param name="productionFacilityId">id произв.объекта, к которому относится место установки СИ</param>
        public static void AddNewLocationOfMi(int numberLocationOfMI, string nameLocationOfMI, int productionFacilityId)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO LocationOfMI (numberLocationOfMI, nameLocationOfMI, productionFacilityId) " +
                $"VALUES ({numberLocationOfMI}, '{nameLocationOfMI}', {productionFacilityId})";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод ChangeLocationOfMi изменяет название и номер существующего места установки СИ
        /// в таблице мест установки СИ по id
        /// </summary>
        /// <param name="idLocationOfMI">id места установки СИ</param>
        /// <param name="newNameLocOfMi">новое название места установки СИ</param>
        /// <param name="newNumberLocOfMi">новый номер места установки СИ</param>
        public static void ChangeLocationOfMi(int idLocationOfMI, string newNameLocOfMi, int newNumberLocOfMi)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE LocationOfMI SET nameLocationOfMI = '{newNameLocOfMi}', numberLocationOfMI = {newNumberLocOfMi} " +
                $"WHERE idLocationOfMI = {idLocationOfMI}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DeleteLocationOfMi удаляет запись о месте установки СИ
        /// из таблицы мест установки СИ по id
        /// </summary>
        /// <param name="idLocationOfMI">id места установки СИ</param>
        public static void DeleteLocationOfMi(int idLocationOfMI)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM LocationOfMI WHERE idLocationOfMI = {idLocationOfMI}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetMIs выгружает СИ для определенного места установки СИ
        /// из таблицы СИ, создает экземпляр СИ 
        /// и добавляет его в словарь переданного места установки СИ
        /// </summary>
        /// <param name="l">экземпляр места установки СИ, для которого выгружают перечнь СИ</param>
        public static void GetMIs(LocationOfMI l)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdMI = new SqlCommand($"select * from MeasurInstr WHERE locationOfMiId = {l.Id}", con);
                var drMI = cmdMI.ExecuteReader();
                while (drMI.Read())
                {
                    MeasurInstr mI = new MeasurInstr();
                    mI.Id = (int)drMI["idMeasuringInstr"];
                    mI.PhaseId = (int)drMI["phaseId"];
                    mI.MiFromRefId = (int)drMI["miFromRefId"];
                    mI.LocationOfMiId = (int)drMI["locationOfMiId"];
                    mI.YearOfManufacture = drMI["yearOfManufacture"].ToString()??"";
                    mI.AccuracyClass = drMI["accuracyClass"].ToString()??"";
                    mI.SerialNumber = drMI["serialNumber"].ToString()??"";
                    mI.CoefficientUp = drMI["coefficientUp"].ToString()??"";
                    mI.CoefficientDown = drMI["coefficientDown"].ToString()??"";
                    mI.VerificationDate = (DateTime)drMI["verificationDate"];
                    mI.VerificationInterval = (int)drMI["verificationInterval"];
                    mI.VerificationNextDate = (DateTime)drMI["verificationNextDate"];
                    l.MeasurInstrs.Add(mI.Id, mI);
                }
                drMI.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddNewMi добавляет новое СИ в таблицу СИ
        /// </summary>
        /// <param name="newMi">экземпляр нового СИ</param>
        public static void AddNewMi(MeasurInstr newMi)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO MeasurInstr (phaseId, miFromRefId, locationOfMiId, yearOfManufacture, accuracyClass, " +
                $"serialNumber, coefficientUp, coefficientDown, verificationDate, verificationInterval, verificationNextDate) " +
                $"VALUES ({newMi.PhaseId}, {newMi.MiFromRefId}, {newMi.LocationOfMiId}, '{newMi.YearOfManufacture}', '{newMi.AccuracyClass}', " +
                $"'{newMi.SerialNumber}', '{newMi.CoefficientUp}', '{newMi.CoefficientDown}', '{newMi.VerificationDate}', {newMi.VerificationInterval}, " +
                $"'{newMi.VerificationNextDate}')";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DelMi удаляет запись о СИ из таблицы СИ по id
        /// </summary>
        /// <param name="idMeasuringInstr">id СИ</param>
        public static void DelMi(int idMeasuringInstr)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM MeasurInstr WHERE idMeasuringInstr = {idMeasuringInstr}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод ChangeMi изменяет имнформацию о существующем СИ в таблице СИ по id
        /// </summary>
        /// <param name="mI">экземпляр нового СИ</param>
        public static void ChangeMi(MeasurInstr mI)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"UPDATE MeasurInstr SET miFromRefId = {mI.MiFromRefId}, yearOfManufacture = '{mI.YearOfManufacture}', " +
                $"accuracyClass = '{mI.AccuracyClass}', serialNumber = '{mI.SerialNumber}', coefficientUp = '{mI.CoefficientUp}', " +
                $"coefficientDown = '{mI.CoefficientDown}', verificationDate = '{mI.VerificationDate}', verificationInterval = {mI.VerificationInterval}, " +
                $"verificationNextDate = '{mI.VerificationNextDate}' WHERE idMeasuringInstr = {mI.Id}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод AddLog добавляет запись о СИ в таблицу истории изменений информации о СИ
        /// </summary>
        /// <param name="oldMi">экземпляр старого СИ</param>
        /// <param name="nameReason">причина внесения записи в историю</param>
        public static void AddLog(MeasurInstr oldMi, string nameReason)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"INSERT INTO Log (logPhaseId, logMiFromRefId, logLocationOfMiId, logYearOfManufacture, " +
                $"logAccuracyClass, logSerialNumber, logCoefficientUp, logCoefficientDown, logVerificationDate, logVerificationInterval, " +
                $"logVerificationNextDate, dateOfLogEntry, nameReason) " +
                $"VALUES ({oldMi.PhaseId}, {oldMi.MiFromRefId}, {oldMi.LocationOfMiId}, '{oldMi.YearOfManufacture}', '{oldMi.AccuracyClass}', " +
                $"'{oldMi.SerialNumber}', '{oldMi.CoefficientUp}', '{oldMi.CoefficientDown}', '{oldMi.VerificationDate}', {oldMi.VerificationInterval}, " +
                $"'{oldMi.VerificationNextDate}', '{DateTime.Now}', '{nameReason}')";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод DelLog удаляет запись о СИ из таблицы историй изменений 
        /// информации по id места установки СИ
        /// </summary>
        /// <param name="locationOfMiId">id места установки СИ</param>
        public static void DelLog(int locationOfMiId)
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string sqlCom = $"DELETE FROM Log WHERE logLocationOfMiId = {locationOfMiId}";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                SqlCommand dr = new SqlCommand(sqlCom, con);
                dr.ExecuteNonQuery();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetAllRefElem выгружает перечень типов СИ из таблицы справочника
        /// RefOfMI, создает экземпляр типа СИ и заносит его в словарь
        /// </summary>
        /// <param name="dictMIRef">словарь типов СИ</param>
        public static void GetAllRefElem(Dictionary<int, MeasInstrFromRef> dictMIRef)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select * from RefOfMI", con);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    MeasInstrFromRef m = new MeasInstrFromRef();
                    m.Id = (int)dr[$"idMIFromRef"];
                    m.NameMIFromRef = dr[$"nameMIFromRef"].ToString();
                    m.TypeOfMiId = (int)dr[$"typeOfMiId"];
                    dictMIRef.Add(m.Id, m);
                }
                dr.Close();
                con.Close();
            }
        }

        /// <summary>
        /// Метод GetNameRefOfMi выгружает название типа СИ из таблицы справочника
        /// RefOfMI по id типа СИ
        /// </summary>
        /// <param name="idMIFromRef">id типа СИ</param>
        public static string GetNameRefOfMi(int idMIFromRef)
        {
            string s = "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdRefOfMI = new SqlCommand($"select nameMIFromRef from RefOfMI WHERE idMIFromRef = {idMIFromRef}", con);
                var drRefOfMI = cmdRefOfMI.ExecuteReader();
                while (drRefOfMI.Read())
                {
                    s = drRefOfMI[$"nameMIFromRef"].ToString();
                }
                drRefOfMI.Close();
                con.Close();
            }
            return s;
        }

        /// <summary>
        /// Метод GetIdTypeRefOfMi выгружает id вида СИ из таблицы справочника
        /// RefOfMI по id типа СИ
        /// </summary>
        /// <param name="idMIFromRef">id типа СИ</param>
        public static int GetIdTypeRefOfMi(int idMIFromRef)
        {
            int i = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmdRefOfMI = new SqlCommand($"select typeOfMiId from RefOfMI WHERE idMIFromRef = {idMIFromRef}", con);
                var drRefOfMI = cmdRefOfMI.ExecuteReader();
                while (drRefOfMI.Read())
                {
                    i = (int)drRefOfMI[$"typeOfMiId"];
                }
                drRefOfMI.Close();
                con.Close();
            }
            return i;
        }

        /// <summary>
        /// Метод TypeOfNewElem возвращает название типов элементов, входящих в иерархию переданного элемента
        /// </summary>
        /// <param name="o">экземпляр переданного элемента</param>
        public static string TypeOfNewElem(Element o)
        {
            string type = "";
            if (o is District)
                type = "ЦДНГ";
            else if (o is OandGPF)
                type = "Объекта";
            else if (o is ProductionFacility)
                type = "Места установки СИ";
            return type;
        }

        /// <summary>
        /// Метод TypeOfNewElem возвращает название типа переданного элемента
        /// </summary>
        /// <param name="o">экземпляр переданного элемента</param>
        public static string TypeOfChangeElem(Element o)
        {
            string type = "";
            if (o is District)
                type = "Района";
            else if (o is OandGPF)
                type = "ЦДНГ";
            else if (o is ProductionFacility)
                type = "Объекта";
            else if (o is LocationOfMI)
                type = "Места установки СИ";
            return type;
        }

        /// <summary>
        /// Метод TypeOfNewElem возвращает название переданного элемента
        /// </summary>
        /// <param name="o">экземпляр переданного элемента</param>
        public static string NameOfSelectedElem(Element o)
        {
            string name = "";
            if (o is District)
            {
                District d = o as District;
                name = "районе " + d.NameDistrict;
            }
            else if (o is OandGPF)
            {
                OandGPF aAndGPF = o as OandGPF;
                name = aAndGPF.NameOandGPF;
            }
            else if (o is ProductionFacility)
            {
                ProductionFacility pf = o as ProductionFacility;
                name = pf.NameProductFacility;
            }
            return name;
        }

        /// <summary>
        /// Метод TypeOfNewElem возвращает количество элементов в словаре переданного элемента
        /// </summary>
        /// <param name="o">экземпляр переданного элемента</param>
        public static int GetCountOfElemInDict(Element o)
        {
            int result = 0;
            if (o is District)
            {
                District d = o as District;
                result = d.OandGPFs.Count();
            }
            else if (o is OandGPF)
            {
                OandGPF oaGPF = o as OandGPF;
                result = oaGPF.ProductionFacilities.Count();
            }
            else if (o is ProductionFacility)
            {
                ProductionFacility pf = o as ProductionFacility;
                result = pf.LocationOfMIs.Count();
            }
            else if (o is LocationOfMI)
            {
                LocationOfMI l = o as LocationOfMI;
                result = l.MeasurInstrs.Count();
            }
            return result;
        }

        public static void GetRole(List<Role> list)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                var cmd = new SqlCommand($"select * from Role", con);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Role r = new Role();
                    r.IdRole = (int)dr[$"idRole"];
                    r.NameRole = dr[$"nameRole"].ToString();
                    list.Add(r);
                }
                dr.Close();
                con.Close();
            }
        }


    }
}
