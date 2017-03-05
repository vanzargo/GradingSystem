using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GradingSystem
{
    public partial class TeacherLoadingForm : Form
    {

        public MySqlCommand cm = new MySqlCommand();
        public MySqlConnection cn = new MySqlConnection();
        List<String> teacherName = new List<String>();
        List<String> teacherId = new List<String>();
        private string conn;
        private MySqlConnection connect;
        private MySqlDataAdapter mySqlDataAdapter;
        private MySqlCommand cmd;

        public TeacherLoadingForm()
        {
            InitializeComponent();
            Ini();
        }

        private void Ini()
        {
            dataGridView1.DataSource = GetSchedules();

            MySqlConnection cn;
            cn = new MySqlConnection(Program.connectionString);
            try
            {
                cn.Open();

            }
            catch (Exception)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            cm.Connection = cn;

            String query = "SELECT `User_ID`, CONCAT(`Teacher_FirstName`, ' ',`Teacher_LastName`) as `Name` FROM `teacher`";
            cm = new MySqlCommand(query, cn);
            MySqlDataReader reader = cm.ExecuteReader();
            while (reader.Read())
            {
                teacherName.Add(reader.GetString(reader.GetOrdinal("Name")));
                teacherId.Add(reader.GetString(reader.GetOrdinal("User_ID")));
            }
            comboBox15.DataSource = teacherName;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            String name = comboBox15.Text;
            int index = teacherName.FindIndex(a => a == name);
            String id = teacherId[index];
            db_connection();
            cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM teacher_schedule where User_ID='" + id + "'";
            cmd.Connection = connect;
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                connect.Close();
                db_connection();
                cmd.CommandText = "UPDATE `teacher_schedule` SET"+
                      " `7_subject`='"+s7to8.Text
                    +"',`7_GradeLevel`='" +gl7to8.Text
                    +"',`8_subject`='" + s8to9.Text
                    + "',`8_GradeLevel`='" + gl8to9.Text
                    + "',`9_subject`='" + s9to10.Text
                    + "',`9_GradeLevel`='" + gl9to10.Text
                    + "',`10_subject`='" + s10to11.Text
                    + "',`10_GradeLevel`='" + gl10to11.Text
                    + "',`12_subject`='" + s12to1.Text
                    + "',`12_GradeLevel`='" + gl12to1.Text
                    + "',`1_subject`='" + s1to2.Text
                    + "',`1_GradeLevel`='" + gl1to2.Text
                    + "',`2_subject`='" + s2to3.Text
                    + "',`2_GradeLevel`='" + gl2to3.Text
                    + "',`advisory_class`='" +comboBox16.Text
                    +"' WHERE `User_ID`='" +id
                    +"'";
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            else
            {
                db_connection();
                cmd.CommandText = "INSERT INTO `teacher_schedule`(`User_ID`, `7_subject`, `7_GradeLevel`, `8_subject`, `8_GradeLevel`, `9_subject`, `9_GradeLevel`, `10_subject`, `10_GradeLevel`, `12_subject`, `12_GradeLevel`, `1_subject`, `1_GradeLevel`, `2_subject`, `2_GradeLevel`, `advisory_class`) "+
                    "VALUES ('"+id+ "','" + s7to8.Text + "','" + gl7to8.Text + "','" + s8to9.Text + "','" + gl8to9.Text + "','" + s9to10.Text + "','" + gl9to10.Text + "','" + s10to11.Text + "','" + gl10to11.Text + "','" + s12to1.Text + "','" + gl12to1.Text + "','" + s1to2.Text + "','" + gl1to2.Text + "','" + s2to3.Text + "','" + gl2to3.Text + "','" + comboBox16.Text + "')";
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            dataGridView1.DataSource = GetSchedules();
        }

        DataTable GetSchedules()
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            String sql = "SELECT `teacher_schedule_id` as `Schedule id`, CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as Name, `7_subject`, `7_GradeLevel`, `8_subject`, `8_GradeLevel`, `9_subject`, `9_GradeLevel`, `10_subject`, `10_GradeLevel`, `12_subject`, `12_GradeLevel`, `1_subject`, `1_GradeLevel`, `2_subject`, `2_GradeLevel`, `advisory_class` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID`";

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }

        DataTable GetSchedules(String search)
        {
            DataTable dt = new DataTable();
            MySqlConnection con;
            con = new MySqlConnection(Program.connectionString);
            con.Open();
            String sql = "SELECT `teacher_schedule_id` as `Schedule id`, CONCAT(`Teacher_FirstName`,' ',`Teacher_LastName`) as `Name`, `7_subject`, `7_GradeLevel`, `8_subject`, `8_GradeLevel`, `9_subject`, `9_GradeLevel`, `10_subject`, `10_GradeLevel`, `12_subject`, `12_GradeLevel`, `1_subject`, `1_GradeLevel`, `2_subject`, `2_GradeLevel`, `advisory_class` FROM `teacher` LEFT JOIN `teacher_schedule` ON `teacher`.`User_ID` = `teacher_schedule`.`User_ID` WHERE "+
                "`7_subject`='"+ search
                + "' OR `7_GradeLevel`='" + search
                + "' OR `8_subject`='" + search
                + "' OR `8_GradeLevel`='" + search
                + "' OR `9_subject`='" + search
                + "' OR `9_GradeLevel`='" + search
                + "' OR `10_subject`='" + search
                + "' OR `10_GradeLevel`='" + search
                + "' OR `12_subject`='" + search
                + "' OR `12_GradeLevel`='" + search
                + "' OR `1_subject`='" + search
                + "' OR `1_GradeLevel`='" + search
                + "' OR `2_subject`='" + search
                + "' OR `2_GradeLevel`='" + search
                + "' OR `advisory_class`='" + search
                + "' OR  `Name` ='" + search;

            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {

                MySqlDataAdapter adpt = new MySqlDataAdapter(cmd);
                adpt.Fill(dt);
            }


            return dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                String id = row.Cells[0].Value.ToString();
                db_connection();
                cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM `teacher_schedule` WHERE `teacher_schedule_id` = '" + id + "'";
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                connect.Close();
            }
            dataGridView1.DataSource = GetSchedules();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
