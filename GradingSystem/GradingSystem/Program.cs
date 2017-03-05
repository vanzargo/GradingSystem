using System;
using System.Collections.Generic;
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
    }
}
