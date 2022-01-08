using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LT_ServerApp
{
    public class ServerConnection
    {
        private SqlConnection _con;
        public SqlCommand Cmd;
        private SqlDataAdapter _da;
        private DataTable _dt;
        public SqlDataReader _dr;
        public SqlParameter _param;
        //Data Source="192.168.1.5, 1433";Initial Catalog=TestBench-1;User ID=sa;Password=***********
        //string connectio_string = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\PrintingPress\\press.mdb;Jet OLEDB:Database Password=admin;Persist Security Info=True";
        public ServerConnection(string source)
        {
            //string str = "";
            _con = new SqlConnection(source);
            //_con = new SqlConnection(@"Data Source=OMSAI1\SQLEXPRESS;Initial Catalog=TestDb;User ID=sa;Password=123;Integrated Security=True");
            if ((_con != null) && (_con.State.Equals(ConnectionState.Open)))
                _con.Close();
            //if((_con!=null) || (_con.State.Equals(ConnectionState.Closed)))
            _con.Open();
        }

        public ServerConnection()
        {
            // TODO: Complete member initialization
        }

        public void SqlQuery(string queryText)
        {
            Cmd = new SqlCommand(queryText, _con);
        }
        public void SqlQuerySP(string queryText,string fromDate, string toDate)
        {
            Cmd = new SqlCommand(queryText, _con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@FromDate", SqlDbType.VarChar).Value = fromDate;
            Cmd.Parameters.AddWithValue("@ToDate", SqlDbType.VarChar).Value = toDate;
        }
        public DataTable QueryEx()
        {
            _da = new SqlDataAdapter(Cmd);
            _dt = new DataTable();
            _da.Fill(_dt);
            return _dt;
        }

        public bool NonQueryEx()
        {

            if (Cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
                return false;
        }

        public SqlDataReader DataReader()
        {
            _dr = Cmd.ExecuteReader();
            return _dr;
        }

        public void closeReader()
        {
            _dr.Close();
        }

        public void closeConnection()
        {
            _con.Close();
        }
    }
}