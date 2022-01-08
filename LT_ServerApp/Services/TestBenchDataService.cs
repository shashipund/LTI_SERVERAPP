using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LT_ServerApp.Models;


namespace LT_ServerApp.Services
{
    public class TestBenchDataService
    {
        ServerConnection con = null;
        string _query;
        DataTable dt;

        TestBenchService _service = new TestBenchService();

        public string Create_Remote_Connection(int TestBenchID)
        {
            try
            {
                string DBSource = _service.GenerateConnectionString(TestBenchID);
                return DBSource;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - TestBenchDataService/Create_Remote_Connection", ex.Message, System.DateTime.Now);
                service.LogError();
                return "";
            }
        }

        public ServerDataModel Create_Remote_Datasource(int TestBenchID)
        {
            try
            {
                ServerDataModel DBSource = _service.GenerateRemoteConnectionString(TestBenchID);
                return DBSource;
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - TestBenchDataService/Create_Remote_Connection", ex.Message, System.DateTime.Now);
                service.LogError();
                return null;
            }
        }

        public DataTable GetBBTHVTableData(int  id, string tableName, string fromDate, string toDate)
        {
            try
            {
                string frmDate = "";
                string tDate = "";
                if (!String.IsNullOrEmpty(fromDate))
                {
                    string[] fdt = fromDate.Split('/');
                    frmDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }
                if (!String.IsNullOrEmpty(toDate))
                {
                    string[] fdt = toDate.Split('/');
                    tDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }

                string DBSource = Create_Remote_Connection(id);
                con = new ServerConnection(DBSource);
                //_query = "SELECT [SrNo] ,[Barcode] ,[Date_Time] ,[Project No] AS ProjNo ,[Month Code] AS MOCode ,[Tag No] AS TagNo "+
                //         ",[Stack No] AS StackNo ,[Voltage] ,[Step 1] AS STP1  ,[Step 2] AS STP2  ,[Step 3] AS STP3 "+
                //         " ,[Step 4] AS STP4  ,[Step 5] AS STP5  ,[Step 6] AS STP6  ,[HV Result] AS HV  ,[Date] "+
                //        " FROM  [BBT_HV_Production Report] WHERE [Date_Time] >= '"+frmDate +"' AND [Date_Time] <='"+tDate+"'";
                _query = "GetBBT_HV_Production_Report";
                con.SqlQuerySP(_query, frmDate,tDate);
                dt = con.QueryEx();
                con.closeConnection();
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - TestBenchDataService/GetRemoteTableData", ex.Message, System.DateTime.Now);
                service.LogError();
                dt = null;
            }
            return dt;
        }

        public DataTable GetBBTIRTableData(int id, string tableName, string fromDate, string toDate)
        {
            try
            {
                string frmDate = "";
                string tDate = "";
                if (!String.IsNullOrEmpty(fromDate))
                {
                    string[] fdt = fromDate.Split('/');
                    frmDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }
                if (!String.IsNullOrEmpty(toDate))
                {
                    string[] fdt = toDate.Split('/');
                    tDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }

                string DBSource = Create_Remote_Connection(id);
                con = new ServerConnection(DBSource);
                //_query = "SELECT  [SrNo],[Barcode],[User Name] AS Username,[Date_Time] as Date_Time ,[Project No] AS ProjNo ,[Month Code] AS MOCode "+
                //         " ,[Tag No] AS TagNo ,[Stack No] AS StkNo ,[Lower Limit] AS LowLimit ,[Upper Limit] AS UppLimit "+
                //         " ,[Step 1] AS STP1,[Step 2] AS STP2 ,[Step 3] AS STP3 ,[Step 4] AS STP4 ,[Step 5] AS STP5 ,[Step 6] AS STP6 "+
                //        " ,[Step 7] AS STP7  ,[Step 8] AS STP8 ,[Step 9] AS STP9 ,[Step 10] AS STP10 ,[Step 11] AS STP11 ,[Step 12] AS STP12 "+
                //        " ,[Step 13] AS STP13 ,[Step 14] AS STP14 ,[Step 15] AS STP15 ,[Megger Result] AS result ,[Date] " +
                //        " FROM  [BBT_IR_Production Report] WHERE [Date_Time] >= '" + frmDate + "' AND [Date_Time] <='" + tDate + "'";
                _query = "GetBBT_IR_Production_Report";
                con.SqlQuerySP(_query, frmDate, tDate);
                dt = con.QueryEx();
                con.closeConnection();
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - TestBenchDataService/GetBBTIRTableData", ex.Message, System.DateTime.Now);
                service.LogError();
                dt = null;
            }
            return dt;
        }

        public DataTable GetBBTIRSettings(int id, string tableName, string fromDate, string toDate)
        {
            try
            {
                string frmDate = "";
                string tDate = "";
                if (!String.IsNullOrEmpty(fromDate))
                {
                    string[] fdt = fromDate.Split('/');
                    frmDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }
                if (!String.IsNullOrEmpty(toDate))
                {
                    string[] fdt = toDate.Split('/');
                    tDate = fdt[2] + "-" + fdt[0] + "-" + fdt[1];
                }

                string DBSource = Create_Remote_Connection(id);
                con = new ServerConnection(DBSource);
                _query = "SELECT DISTINCT [Test Selection] AS TestSelection ,[Test Time] AS TestTime,[Ramp Time] AS RampTime "+
                         " ,[Test Voltage] AS TestVoltage   ,[HI Resistance Value] AS HIResistanceValue ,[LO Resistance Value] AS LOResistanceValue " +
                        " FROM  [BBT_IR_Settings]";

                con.SqlQuery(_query);
                dt = con.QueryEx();
                con.closeConnection();
            }
            catch (Exception ex)
            {
                LoggingService service = new LoggingService("WebJob - TestBenchDataService/GetRemoteTableData", ex.Message, System.DateTime.Now);
                service.LogError();
                dt = null;
            }
            return dt;
        }
    }
}