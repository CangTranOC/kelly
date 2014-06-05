using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;


namespace QuanLyCamDo
{
    class Connection
    {
        private string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + System.Windows.Forms.Application.StartupPath + "/akajiro.mdb";
        private OleDbConnection db;
        public void connect()
        {
            db = new OleDbConnection(connectionString);
            try
            {
                db.Open();
            }
            catch (Exception ex)
            {
                Log.writeLog("Connection error [" + ex.ToString() + "]", Log.CRASH);
                return;
            }
        }
        public void disConnect()
        {
            if (db.State == ConnectionState.Open)
            {
                db.Close();
                db.Dispose();
            }
        }
        public DataTable fillData(string sql)
        {
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter;
            try
            {
                adapter = new OleDbDataAdapter(sql, db);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Log.writeLog("Can't fill data [" + ex.ToString() + "]", Log.CRITIC);
            }
            return dt;
        }
        public void fillData(string sql, ref DataTable dt)
        {
            dt = fillData(sql);
        }
        public bool executeCmd(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, db);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Log.writeLog("Execute cmd failed! [" + ex.ToString() + "]", Log.ERROR);
                return false;
            }
        }

    }
}
