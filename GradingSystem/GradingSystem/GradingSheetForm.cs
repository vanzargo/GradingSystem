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
