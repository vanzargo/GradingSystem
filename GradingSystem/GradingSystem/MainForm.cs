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
    public partial class MainForm : Form
    {

        public MainForm()
        {
            if (Program.position=="") {
                Program.position = "";
            }
            InitializeComponent();
            if (Program.position == "Admin")
            {
                panelAdmin.Show();
                panelTeacher.Hide();
            }
            else
            {
                panelAdmin.Hide();
                panelTeacher.Show();
            }
        }

        public MainForm(String Position)
        {
            if (Program.position == "")
            {
                Program.position = Position;
            }
            InitializeComponent();
            if (Program.position == "Admin")
            {
                panelAdmin.Show();
                panelTeacher.Hide();
            }
            else
            {
                panelAdmin.Hide();
                panelTeacher.Show();
            }
        }

        private void addStudent_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            add.Show();
            this.Hide();
        }

        private void addTeacher_Click(object sender, EventArgs e)
        {
            AddTeacherForm teacher = new AddTeacherForm();
            teacher.Show();
            this.Hide();
        }

  

        private void addGradelvl_Click(object sender, EventArgs e)
        {
            GradingSheetForm sheet = new GradingSheetForm();
            sheet.Show();
        }



        private void addSchoolyear_Click(object sender, EventArgs e)
        {
            GradingSheetForm add = new GradingSheetForm();
            add.Show();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

        private void AdminMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void addTimeSchedule_Click(object sender, EventArgs e)
        {
            ClassSchedForm form = new ClassSchedForm();
            form.Show();
        }



        private void btnFacultyLoad_Click(object sender, EventArgs e)
        {
            TeacherLoadingForm teach = new TeacherLoadingForm();
            teach.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.position = "";
            Close();
        }
    }
}
