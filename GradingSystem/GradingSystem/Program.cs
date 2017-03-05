using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradingSystem
{
    static class Program
    {
        static public String position = "";
        static public String user_id = "";
        static public string connectionString = "server=localhost;database=gradingsystem;uid=root;pwd=;";
        static public MySqlCommand cm = new MySqlCommand();
        static public MySqlConnection cn = new MySqlConnection();
        static private string conn;
        static private MySqlConnection connect;
        static private MySqlDataAdapter mySqlDataAdapter;
        static private MySqlCommand cmd;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        static public void db_connection()
        {
            try
            {
                conn = "Server=localhost;Database=gradingsystem;Uid=root;Pwd=;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Database not connected!");
                throw e;
            }
        }

        static public DataTable GetDataFromQuery(String sql)
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }

        static public MySqlDataReader GetReaderFromQuery(String sql)
        {
            db_connection();
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
