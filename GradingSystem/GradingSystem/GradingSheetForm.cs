﻿using System;
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
        String Subject = "Math";//`subject`_total for total
        String teacher = "";
        String section = "%";
        String quarter = "1";//1, 2, 3, 4
        /*Math
          English
          Science
          Filipino
          S.S.
          M.A.P.E.H
          T.L.E*/

        public GradingSheetForm(String Grade, String quarter, String Subject, String teacher, String section)
        {
            this.Grade = Grade;
            this.quarter = quarter;
            this.Subject = Subject;
            this.teacher = teacher;
            this.section = section;
            InitializeComponent();
            lblGradelvl.Text = "Grade " + Grade;
            lbl_grade.Text = section;
            lblSubject.Text = Subject;
            quarterCmBx.SelectedIndex = Int32.Parse(quarter) - 1;
            lblGradelvl.Text = "TeacherName: " + teacher;
        }
        
        private void GradingSheet_Load(object sender, EventArgs e)
        {
            loadMale();
            loadFemale();
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

        private void loadFemale()
        {
            dgvBName.DataSource = GetSNameMale();
            resizeDGV(dgvBName, 100);
            resizeRow(dgvBName, 0);

            dgvBWW.DataSource = GetPScoreMale();
            resizeDGV(dgvBWW);
        }

        DataTable GetSNameMale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` " +
                "FROM `student_profile` " +
                "WHERE `Student_Sex` like 'Male' " +
                "AND `student_Level` like 'Grade " + Grade + "' AND `student_Section` = '" + section + "' ");
        }

        DataTable GetPScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `subject` like '" + Subject + "'" +
            "AND `quarter_ID` = '" + quarter + "' AND `student_Section` = '" + section + "' ");
        }

        DataTable GetSNameFemale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` "+
                "FROM `student_profile` "+
                "WHERE `Student_Sex` like 'Female' " +
                "AND `student_Level` like 'Grade "+Grade+"' AND `student_Section` = '"+ section + "' ");
        }

        DataTable GetPScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' "+
            "AND `subject` like '" + Subject + "'"+
            "AND `quarter_ID` = '"+quarter+ "' AND `student_Section` = '" + section + "' ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinalRatingForm frf = new FinalRatingForm(Grade, quarter, Subject, teacher);
            frf.Show();
        }

        private void quarterCmBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            GradingSheet_Load(sender,e);
        }

        private void manageCompute() {
            float[] nums = new float[8];
            nums[0] = Program.safeParse(txtbx_w1.Text);
            nums[1] = Program.safeParse(txtbx_w2.Text);
            nums[2] = Program.safeParse(txtbx_w3.Text);
            nums[3] = Program.safeParse(txtbx_w4.Text);
            nums[4] = Program.safeParse(txtbx_w5.Text);
            nums[5] = Program.safeParse(txtbx_w6.Text);
            nums[6] = Program.safeParse(txtbx_w7.Text);
            nums[7] = Program.safeParse(txtbx_w8.Text);
            txtbx_wT.Text = Program.sum(nums).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            manageCompute();
        }
    }
}
/*

*/
