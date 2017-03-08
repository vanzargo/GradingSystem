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
        String Subject = "Math";//`subject`_total for total
        /*Math
          English
          Science
          Filipino
          S.S.
          M.A.P.E.H
          T.L.E*/
        String teacher = "";
        String section = "%";
        String quarter = "1";//1, 2, 3, 4

        float WrittenWorkPercent = 30;
        float PerformancePercent = 50;
        float QuarterlyPercent = 20;

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
            switch (Subject)
            {
                default:break;
                case "English":
                case "Filipino":
                case "S.S.":
                    WrittenWorkPercent = 30;
                    PerformancePercent = 50;
                    QuarterlyPercent = 20;
                    break;
                case "Math":
                case "Science":
                    WrittenWorkPercent = 40;
                    PerformancePercent = 40;
                    QuarterlyPercent = 20;
                    break;
                case "M.A.P.E.H":
                case "T.L.E":
                    WrittenWorkPercent = 20;
                    PerformancePercent = 60;
                    QuarterlyPercent = 20;
                    break;
            }
            lbl_written_work.Text = "WRITTEN WORK(" + WrittenWorkPercent + " %)";
            lbl_performance_task.Text = "PERFORMANCE TASK (" + PerformancePercent + "%)";
            lbl_quarter.Text = "QUARTERLY\nASSESSMENT\n        (" + QuarterlyPercent + " %)";
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

            dgvBWW.DataSource = GetWScoreMale();
            resizeDGV(dgvBWW);

            dgvBPT.DataSource = GetPScoreMale();
            resizeDGV(dgvBPT);

            dgvBQA.DataSource = GetQScoreMale();
            resizeDGV(dgvBQA);
        }

        private void loadFemale()
        {
            dgvGName.DataSource = GetSNameFemale();
            resizeDGV(dgvGName, 100);
            resizeRow(dgvGName, 2);

            dgvGWW.DataSource = GetWScoreFemale();
            resizeDGV(dgvGWW);

            dgvGPT.DataSource = GetPScoreFemale();
            resizeDGV(dgvGPT);

            dgvGQA.DataSource = GetQScoreFemale();
            resizeDGV(dgvGQA);
        }

        DataTable GetSNameMale()
        {
            return Program.GetDataFromQuery("SELECT `Student_LastName`,`Student_FirstName`, `Student_MI` " +
                "FROM `student_profile` " +
                "WHERE `Student_Sex` like 'Male' " +
                "AND `student_Level` like 'Grade " + Grade + "' AND `student_Section` = '" + section + "' ");
        }

        DataTable GetWScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `subject` like '" + Subject + "'" +
            "AND `quarter_ID` = '" + quarter + "' AND `student_Section` = '" + section + "' ");
        }
        DataTable GetPScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, (`PTS1` + `PTS2` + `PTS3` + `PTS4` + `PTS5` + `PTS6` + `PTS7` + `PTS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_perf` ON `student_perf`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Male' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `subject` like '" + Subject + "'" +
            "AND `quarter_ID` = '" + quarter + "' AND `student_Section` = '" + section + "' ");
        }
        DataTable GetQScoreMale()
        {
            return Program.GetDataFromQuery("SELECT `quarterly_score` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_qa` ON `student_qa`.`student_ID` = `student_profile`.`student_ID`" +
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

        DataTable GetWScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `WWS1`, `WWS2`, `WWS3`, `WWS4`, `WWS5`, `WWS6`, `WWS7`, `WWS8`, (`WWS1` + `WWS2` + `WWS3` + `WWS4` + `WWS5` + `WWS6` + `WWS7` + `WWS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_ww` ON `student_ww`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' "+
            "AND `subject` like '" + Subject + "'"+
            "AND `quarter_ID` = '"+quarter+ "' AND `student_Section` = '" + section + "' ");
        }
        DataTable GetPScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `PTS1`, `PTS2`, `PTS3`, `PTS4`, `PTS5`, `PTS6`, `PTS7`, `PTS8`, (`PTS1` + `PTS2` + `PTS3` + `PTS4` + `PTS5` + `PTS6` + `PTS7` + `PTS8`) as `TOTAL`" +
            "FROM `student_profile` " +
            "LEFT JOIN `student_perf` ON `student_perf`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `subject` like '" + Subject + "'" +
            "AND `quarter_ID` = '" + quarter + "' AND `student_Section` = '" + section + "' ");
        }
        DataTable GetQScoreFemale()
        {
            return Program.GetDataFromQuery("SELECT `quarterly_score` " +
            "FROM `student_profile` " +
            "LEFT JOIN `student_qa` ON `student_qa`.`student_ID` = `student_profile`.`student_ID`" +
            "WHERE `Student_Sex` like 'Female' " +
            "AND `student_Level` like 'Grade " + Grade + "' " +
            "AND `subject` like '" + Subject + "'" +
            "AND `quarter_ID` = '" + quarter + "' AND `student_Section` = '" + section + "' ");
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

        private void manageCompute(object sender, EventArgs e) {
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

            nums[0] = Program.safeParse(txtbx_p1.Text);
            nums[1] = Program.safeParse(txtbx_p2.Text);
            nums[2] = Program.safeParse(txtbx_p3.Text);
            nums[3] = Program.safeParse(txtbx_p4.Text);
            nums[4] = Program.safeParse(txtbx_p5.Text);
            nums[5] = Program.safeParse(txtbx_p6.Text);
            nums[6] = Program.safeParse(txtbx_p7.Text);
            nums[7] = Program.safeParse(txtbx_p8.Text);
            txtbx_pT.Text = Program.sum(nums).ToString();
            int i,x;
            float sum = 0;
            float[,] arrBWW = Program.getFloat2dArray(dgvBWW);
            float[,] arrBWWPS = new float[arrBWW.GetLength(0), 1];//Program.getFloat2dArray(dgvBWWPS);
            float[,] arrBWWWS = new float[arrBWW.GetLength(0), 1];//Program.getFloat2dArray(dgvBWWWS);
            for (x = 0; x < arrBWW.GetLength(0); x++)
            {
                if (x == 0)
                {
                    Console.Write("[");
                }
                for (i = 0; i < arrBWW.GetLength(1); i++)
                {
                    if (i == 0)
                    {
                        sum = 0;
                        Console.Write("[");
                    }
                    if (i+1>= arrBWW.GetLength(1)) {
                        arrBWW[x, i] = sum;
                        arrBWWPS[x, 0] = arrBWW[x, i] / Program.safeParse(txtbx_wT.Text) *100;
                        arrBWWWS[x, 0] = arrBWWPS[x, 0] * WrittenWorkPercent / 100;
                        Console.Write(arrBWW[x, i].ToString());
                    } else {
                        sum += arrBWW[x, i];
                        Console.Write(arrBWW[x, i].ToString() + ',');
                    }
                    if (i + 1 >= arrBWW.GetLength(1))
                    {
                        Console.Write("]");
                    }
                }
                if (x+1 >= arrBWW.GetLength(0))
                {
                    Console.Write("]");
                }else
                {
                    Console.Write(",");
                }
            }
            Console.Write(txtbx_wT.Text);
            setDataToGridView(ref dgvBWW, arrBWW);
            setDataToGridView(ref dgvBWWPS, arrBWWPS);
            setDataToGridView(ref dgvBWWWS, arrBWWWS);

            float[,] arrBPT = Program.getFloat2dArray(dgvBPT);
            float[,] arrBPTPS = Program.getFloat2dArray(dgvBPTPS);
            float[,] arrBPTWS = Program.getFloat2dArray(dgvBPTWS);

            float[,] arrBQA = Program.getFloat2dArray(dgvBQA);
            float[,] arrBQAPS = Program.getFloat2dArray(dgvBQAPS);
            float[,] arrBQAWS = Program.getFloat2dArray(dgvBQAWS);
            
            float[,] arrBIG = Program.getFloat2dArray(dgvBIG);
            float[,] arrBQG = Program.getFloat2dArray(dgvBQG);

        }

        public void setDataToGridView(ref DataGridView v, float[,] data)
        {
            v.DataSource = new float[data.GetLength(0), data.GetLength(1)];
            v.DataSource = null;
            var rowCount = data.GetLength(0);
            var rowLength = data.GetLength(1);
            v.ColumnCount = rowLength;
            for (int rowIndex = 0; rowIndex < rowCount; ++rowIndex)
            {
                var row = new DataGridViewRow();

                for (int columnIndex = 0; columnIndex < rowLength; ++columnIndex)
                {
                    row.Cells.Add(new DataGridViewTextBoxCell()
                    {
                        Value = data[rowIndex, columnIndex]
                    });
                }
                v.Rows.Add(row);
            }
            resizeDGV(v);
        }

        private void dgvGPT_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            manageCompute(sender, e);
        }
    }
}
/*

*/
