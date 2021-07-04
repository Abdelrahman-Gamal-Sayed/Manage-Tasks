using System;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using System.Windows;
namespace ONA_Tasks
{
    class DB
    {//171.0.1.96 171.0.1.96 

        private static readonly string ConnectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=171.0.1.96)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=12369";
        public OracleCommand cmd = new OracleCommand();
#pragma warning disable IDE0044 // Add readonly modifier

       // private static readonly string ConnectionStr => connectionStr;
        public bool RunNonQuery(string SQLStatement, string Message = "")
        {

            using (OracleConnection conn = new OracleConnection(ConnectionStr))
            {
                bool test = false;
                try
                {
                    cmd.Connection = conn;
                    cmd.CommandText = SQLStatement;
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    else if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    //conn.Close();
                    //conn.Open();
                    cmd.ExecuteNonQuery();
                    if (Message != "")
                    {
                        MessageBox.Show(Message);
                        test = true;
                    }
                    return test;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                    return test;
                }
                finally { conn.Close(); }
            }
        }

        public async Task<DataTable> RunReader(string SQLStatement)
        {

            using (OracleConnection conn = new OracleConnection(ConnectionStr))
            {
                DataTable tbl = new DataTable();
                try
                {

                    cmd.Connection = conn;
                    cmd.CommandText = SQLStatement;
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                        conn.Open();
                    }
                    else if (conn.State == ConnectionState.Closed) conn.Open();

                    tbl.Load(await cmd.ExecuteReaderAsync());
                    conn.Close();
                    return tbl;

                }
                catch (OracleException ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.ToString());
                    return tbl;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                    return tbl;
                }
                finally { conn.Close(); }
            }
        }



    }
}
