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
    public partial class GradingSheetForm : Form
    {
        String Grade = "%";//or 1 to 10
        String quarter = "1";//1, 2, 3, 4
        String Subject = "Math";//`subject`_total for total
        String teacher = "";
      /*Math
        English
        Science
        Filipino
        S.S.
        M.A.P.E.H
        T.L.E*/

        public GradingSheetForm(String Grade, String quarter, String Subject, String teacher)
        {
            this.Grade = Grade;
            this.quarter = quarter;
            this.Subject = Subject;
            this.teacher = teacher;
            InitializeComponent();
            lbl_grade.Text = "Grade " + Grade;
            subjectCmBx.DataSource = new String[] { Subject };
            subjectCmBx.Text = Subject;
            lblGradelvl.Text = "TeacherName: " + teacher;
        }
        
        private void GradingSheet_Load(object sender, EventArgs e)
        {
            loadMale();
        }

        private void resizeDGV(DataGridView v, int defSize = 50, int lastSize = 100)
        {
            int i;
            for (i = 0; i < v.ColumnCount; i++)
            {
                v.Columns[i].Width = defSize;
                if (i + 1 == v.ColumnCount)
                {
                    v.Columns[i].Width = lastSize;
                }
            }
        }

        private void resizeRow(DataGridView v, int num)
        {
            if (v.Rows.Count > 0)
            {
                DataGridViewRow row = v.Rows[0];
                //dgvBName.Height = row.Height * dgvBName.Rows.Count;

                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = row.Height * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
            else
            {
                TableLayoutRowStyleCollection styles = tableLayoutPanel6.RowStyles;
                int x = 0;
                foreach (RowStyle style in styles)
                {
                    if (x == num)
                    {
                        style.SizeType = SizeType.Absolute;
                        style.Height = 40 * (v.Rows.Count + 1);
                    }
                    x++;
                }
            }
        }

        private void loadMale()
        {
            dgvBName.DataSource = GetSNameMale();
            resizeDGV(dgvBName,100);
            resizeRow(dgvBName, 0);

            dgvBWW.DataSource = GetPScoreMale();
            resizeDGV(dgvBWW);
        }

        DataTable GetSNameMale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` "+
                "FROM `student_profile` "+
                "WHERE `Student_Sex` like 'Male' "+
                "AND `student_Level` like 'Grade "+Grade+"'");
        }

        DataTable GetPScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' "+
            "AND `student_Level` like 'Grade " + Grade + "' "+
            "AND `subject` like '" + Subject + "'"+
            "AND `quarter_ID` = '"+quarter+"'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinalRatingForm frf = new FinalRatingForm(Grade, quarter, Subject, teacher);
            frf.Show();
        }

        static public float transmutate(float grade)
        {
            if (grade >= 0.00 && grade <= 3.99) return 60;
            if (grade >= 4.00 && grade <= 7.99) return 61;
            if (grade >= 8.00 && grade <= 11.99) return 62;
            if (grade >= 12.00 && grade <= 15.99) return 63;
            if (grade >= 16.00 && grade <= 19.99) return 64;
            if (grade >= 20.00 && grade <= 23.99) return 65;
            if (grade >= 24.00 && grade <= 27.99) return 66;
            if (grade >= 28.00 && grade <= 31.99) return 67;
            if (grade >= 32.00 && grade <= 35.99) return 68;
            if (grade >= 36.00 && grade <= 39.99) return 69;
            if (grade >= 40.00 && grade <= 43.99) return 70;
            if (grade >= 44.00 && grade <= 47.99) return 71;
            if (grade >= 48.00 && grade <= 51.99) return 72;
            if (grade >= 52.00 && grade <= 55.99) return 73;
            if (grade >= 56.00 && grade <= 59.99) return 74;
            if (grade >= 60.00 && grade <= 61.99) return 75;
            if (grade >= 61.60 && grade <= 63.19) return 76;
            if (grade >= 63.20 && grade <= 64.79) return 77;
            if (grade >= 64.80 && grade <= 66.39) return 78;
            if (grade >= 66.40 && grade <= 67.99) return 79;
            if (grade >= 68.00 && grade <= 69.59) return 80;
            if (grade >= 69.60 && grade <= 71.19) return 81;
            if (grade >= 71.20 && grade <= 72.79) return 82;
            if (grade >= 72.80 && grade <= 74.39) return 83;
            if (grade >= 74.40 && grade <= 75.99) return 84;
            if (grade >= 76.00 && grade <= 77.59) return 85;
            if (grade >= 77.60 && grade <= 79.19) return 86;
            if (grade >= 79.20 && grade <= 80.79) return 87;
            if (grade >= 80.80 && grade <= 82.39) return 88;
            if (grade >= 82.40 && grade <= 83.99) return 89;
            if (grade >= 84.00 && grade <= 85.59) return 90;
            if (grade >= 85.60 && grade <= 87.19) return 91;
            if (grade >= 87.20 && grade <= 88.79) return 92;
            if (grade >= 88.80 && grade <= 90.39) return 93;
            if (grade >= 90.40 && grade <= 91.99) return 94;
            if (grade >= 92.00 && grade <= 93.59) return 95;
            if (grade >= 93.60 && grade <= 95.19) return 96;
            if (grade >= 95.20 && grade <= 96.79) return 97;
            if (grade >= 96.80 && grade <= 98.39) return 98;
            if (grade >= 98.40 && grade <= 99.99) return 99;
            if (grade >= 100.00 && grade <= 100.00) return 100;
            return 0;
        }
        /*
         */
    }
}
/*
lbl_written_work	:
WRITTEN WORK (30%)

lbl_performance_task	:
PERFORMANCE TASK (50%)

lbl_quarter	:
QUARTERLY
ASSESSMENT
        (20%)

subjectCmBx	:

lblGradelvl	:
TeacherName

lbl_grade	:
Grade 0

txtbx_w1	:
txtbx_w2	:
txtbx_w3	:
txtbx_w4	:
txtbx_w5	:
txtbx_w6	:
txtbx_w7	:
txtbx_w8	:
txtbx_wT	:

txtbx_p1	:
txtbx_p2	:
txtbx_p3	:
txtbx_p4	:
txtbx_p5	:
txtbx_p6	:
txtbx_p7	:
txtbx_p8	:
txtbx_pT	:

txtbx_qT	:

dgvBName	:
dgvBWW		:
dgvBWWPS	:
dgvBWWWS	:

dgvBPT		:
dgvBPTPS	:
dgvBPTWS	:

dgvBQA		:
dgvBQAPS	:
dgvBQAWS	:

dgvBIG		:
dgvBQG		:

dgvGName	:
dgvGWW		:
dgvGWWPS	:
dgvGWWWS	:

dgvGPT		:
dgvGPTPS	:
dgvGPTWS	:

dgvGQA		:
dgvGQAPS	:
dgvGQAWS	:

dgvGIG		:
dgvGQG		:
*/
