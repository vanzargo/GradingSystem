using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GradingSystem
{
    public partial class AddTeacherForm : Form
    {
        public AddTeacherForm()
        {
            InitializeComponent();
        }

        private string conn;
        private MySqlConnection connect;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlCommand cmd;
        
        private void db_connection()
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

        private void display_teachers()
        {
            db_connection();

            mySqlDataAdapter = new MySqlDataAdapter("SELECT User.User_ID, User_Username, User_Status, Teacher_FirstName, Teacher_LastName, Teacher_Sex, Teacher_Position  from user, teacher where User.User_ID = teacher.User_ID  ", conn);
            DataSet DS = new DataSet();
            mySqlDataAdapter.Fill(DS);
            TeacherTable.DataSource = DS.Tables[0];

            connect.Close();
        }

        private void AddTeacherForm_Load(object sender, EventArgs e)
        {
            display_teachers();
            panelAdd.Hide();
        }

        private void TeacherTable_SelectionChanged(object sender, EventArgs e)
        {

            db_connection();
            //view
            int rowindex = TeacherTable.CurrentCell.RowIndex;
            var getTeacherID = TeacherTable.Rows[rowindex].Cells[0].Value.ToString();
      //      var getUser = TeacherTable.Rows[rowindex].Cells[1].Value.ToString();
       //     var getPass = TeacherTable.Rows[rowindex].Cells[2].Value.ToString();
            var getStatus = TeacherTable.Rows[rowindex].Cells[3].Value.ToString();

            txtIDno.Text = getTeacherID;
            v_IDno.Text = getTeacherID;

            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM user, teacher where User.User_ID='" + getTeacherID + "'";
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                //view
                v_FName.Text = reader.GetString(reader.GetOrdinal("Teacher_FirstName"));
                v_LName.Text = reader.GetString(reader.GetOrdinal("Teacher_LastName"));
                v_Sex.Text = reader.GetString(reader.GetOrdinal("Teacher_Sex"));
                v_Position.Text = reader.GetString(reader.GetOrdinal("Teacher_Position"));

                //Add
                txtIDno.Text = reader.GetString(reader.GetOrdinal("User_ID"));
                txtUsername.Text = reader.GetString(reader.GetOrdinal("User_Username"));
                getStatus = reader.GetString(reader.GetOrdinal("User_Status"));
                
                if(getStatus == "Active")
                {
                    rbtnActive.Checked = true;
                }
                else {
                    rbtnInactive.Checked = true;
                }

            }
        }

        private void AddTeacherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            panelView.Hide();
            panelAdd.Show();
        }
    }
}
