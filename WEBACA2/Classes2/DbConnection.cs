using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WEBACA2.Classes2
{
    public class DbConnection
    {
        
            SqlConnection conn;
            SqlCommand cmd;
            SqlDataAdapter da;
            DataTable dt;
            public DbConnection()
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString());
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();
                cmd.Connection = conn;
                da.SelectCommand = cmd;
            }

            public void Fill()
            {

                conn.Open();
                da.Fill(dt);
                conn.Close();
            }
            public DataTable Dt
            {
                get { return dt; }
                set { this.dt = value; }
            }

            public SqlCommand Cmd
            {
                get { return cmd; }
                set { this.cmd = value; }
            }

            public SqlConnection Conn
            {
                get { return conn; }

            }
        }
    }
