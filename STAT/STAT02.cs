using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DH_Core;
using DH_Core.DB;
using DH_Core.CommonPopup;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.Spreadsheet;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;

namespace STAT
{
    public partial class STAT02 : frmSub_Baseform_Search_STD
    {
        public STAT02()
        {
            InitializeComponent();
        }

        #region Attributes (속성정의 집합)
        _Environment env;
        string[] gParam = null;
        string gOut_MSG = null;
        static DataSet DT_GRD01 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD02 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD03 = new DataSet();    //FOR GIRD2
        static DataSet DT_GRD04 = new DataSet();    //FOR GRID2
        static DataSet DT_GRD05 = new DataSet();    //FOR GRID2
        static DataSet DT_GRD06 = new DataSet();    //FOR GRID2
        static DataRow select_row;
        string ADMIN_NO = null;
        string error_msg = "";
        #endregion

        #region Functions 
        /// <summary>
        /// 기능버튼초기화
        /// </summary>
        private void AutoritySet()
        {
            DataRow DR = (DataRow)this.Tag;
            if (DR != null)
            {
                try
                {
                    //pnl_Search.Enabled = DR[0].Equals("Y") ? true : false;
                    pnl_Add.Enabled = DR[1].Equals("Y") ? true : false;
                    pnl_Delete.Enabled = DR[3].Equals("Y") ? true : false;
                    pnl_Save.Enabled = DR[4].Equals("Y") ? true : false;
                    pnl_Print.Enabled = DR[5].Equals("Y") ? true : false;
                    pnl_Excel.Enabled = DR[6].Equals("Y") ? true : false;
                }
                catch (Exception ex)
                {
                    MsgBox.MsgErr("" + ex, "");
                }
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            AutoritySet();

            env = new _Environment();

            dt_YEAR.EditValue = DateTime.Today;
            dt_END.EditValue = DateTime.Today;
        }

        #endregion

        #region DB CRUD(데이터베이스 처리)
        private DataSet df_select(int Index, object[] Param, out string error_msg)
        {
            DataSet dt = null;
            gConst.DbConn.DBClose();
            error_msg = "";
            switch (Index)
            {
                case 0: //조직별
                    {
                        gConst.DbConn.ProcedureName = "REPORT_SECT";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@S_MONTH", ((DateTime)dt_START.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@E_MONTH", ((DateTime)dt_END.EditValue).Month.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 1: //계정별
                    {
                        gConst.DbConn.ProcedureName = "REPORT_ACT";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@S_MONTH", ((DateTime)dt_START.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@E_MONTH", ((DateTime)dt_END.EditValue).Month.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 2: //부서별
                    {
                        gConst.DbConn.ProcedureName = "REPORT_DEPT";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@S_MONTH", ((DateTime)dt_START.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@E_MONTH", ((DateTime)dt_END.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", bedt_PJT.Tag.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 4: //프로젝트별
                    {
                        gConst.DbConn.ProcedureName = "REPORT_PJT";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@S_MONTH", ((DateTime)dt_START.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@E_MONTH", ((DateTime)dt_END.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_CD", bedt_PJT.Tag.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return dt;
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if(bedt_PJT.Tag.ToString() == "")
                {
                    MsgBox.MsgErr("프로젝트를 선택해주세요.\r\n" + error_msg, "에러");
                    return;
                }
                string title;
                this.Cursor = Cursors.WaitCursor;
                DT_GRD01 = null;
                DT_GRD01 = df_select(4, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("지출결의서 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                spreadsheetControl1.LoadDocument("예산대비집행양식_프로젝트.xlsx", DocumentFormat.Xlsx);
                title = "예산 대비 집행/실적(프로젝트 - " + bedt_PJT.Text + ")";

                Set_Excel_Data(DT_GRD01, title);

                //DT_GRD02 = DT_GRD01.Copy();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }

        private void Set_Excel_Data(DataSet ds, string title)
        {
            Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];
            sheet.Cells["B6"].Value = DT_GRD01.Tables[0].Rows[0][0].ToString();
            int Start_position = 6;
            int addrow_cnt = 0;
            int EndMonth = ((DateTime)dt_END.EditValue).Month;

            for (int i = 0; i < DT_GRD01.Tables[1].Rows.Count; i++)
            {
                if ( i < DT_GRD01.Tables[1].Rows.Count-1)
                {
                    sheet.Rows.Insert(Start_position + 20 + addrow_cnt, 21);
                    for (int k = 0; k < 21; k++)
                    {
                        sheet.Rows[Start_position + 21 + addrow_cnt + k - 1].CopyFrom(sheet.Rows[Start_position + k - 1]);
                    }
                }
                sheet.Cells["C" + (Start_position + addrow_cnt)].Value = DT_GRD01.Tables[1].Rows[i]["SECT_NAME"].ToString();

                DataRow[] rows = DT_GRD01.Tables[0].Select("SECT_CD = '" + DT_GRD01.Tables[1].Rows[i]["SECT_CD"] + "'");
                for (int j = 0; j < rows.Length; j++)
                    {
                    sheet.Cells["D" + (Start_position + addrow_cnt + j)].Value = rows[j]["예산계정명"].ToString();
                    if (EndMonth >= 1)
                    {
                        sheet.Cells["E" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["1월 예산"].ToString());
                        sheet.Cells["F" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["1월 집행"].ToString());
                        if (EndMonth >= 2)
                        {
                            sheet.Cells["L" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["2월 예산"].ToString());
                            sheet.Cells["N" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["2월 집행"].ToString());
                            if (EndMonth >= 3)
                            {
                                sheet.Cells["T" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["3월 예산"].ToString());
                                sheet.Cells["V" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["3월 집행"].ToString());
                                if (EndMonth >= 4)
                                {
                                    sheet.Cells["AB" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["4월 예산"].ToString());
                                    sheet.Cells["AD" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["4월 집행"].ToString());
                                    if (EndMonth >= 5)
                                    {
                                        sheet.Cells["AJ" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["5월 예산"].ToString());
                                        sheet.Cells["AL" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["5월 집행"].ToString());
                                        if (EndMonth >= 6)
                                        {
                                            sheet.Cells["AR" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["6월 예산"].ToString());
                                            sheet.Cells["AT" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["6월 집행"].ToString());
                                            if (EndMonth >= 7)
                                            {
                                                sheet.Cells["AZ" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["7월 예산"].ToString()); ;
                                                sheet.Cells["BB" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["7월 집행"].ToString()); ;
                                                if (EndMonth >= 8)
                                                {
                                                    sheet.Cells["BH" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["8월 예산"].ToString());
                                                    sheet.Cells["BJ" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["8월 집행"].ToString());
                                                    if (EndMonth >= 9)
                                                    {
                                                        sheet.Cells["BP" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["9월 예산"].ToString());
                                                        sheet.Cells["BR" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["9월 집행"].ToString());
                                                        if (EndMonth >= 10)
                                                        {
                                                            sheet.Cells["BX" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["10월 예산"].ToString());
                                                            sheet.Cells["BZ" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["10월 집행"].ToString());
                                                            if (EndMonth >= 11)
                                                            {
                                                                sheet.Cells["CF" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["11월 예산"].ToString());
                                                                sheet.Cells["CH" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["11월 집행"].ToString());
                                                                if (EndMonth >= 12)
                                                                {
                                                                    sheet.Cells["CN" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["12월 예산"].ToString());
                                                                    sheet.Cells["CP" + (Start_position + addrow_cnt + j)].Value = double.Parse(rows[j]["12월 집행"].ToString());
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                addrow_cnt = addrow_cnt + 21;
                sheet.MergeCells(sheet.Range["C" + (Start_position + addrow_cnt) + ":" + "C" + (Start_position + addrow_cnt + 19)]);
            }
        }

        private void bedt_DEPT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "프로젝트");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_PJT.Tag = frm.CODE;
                bedt_PJT.Text = frm.NAME;
            }
        }
        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            //if(radioGroup1.SelectedIndex == 2)
            //{
            //    labelControl5.Visible = true;
            //    bedt_PJT.Visible = true;
            //}
            //else
            //{
            //    labelControl5.Visible = false;
            //    bedt_PJT.Visible = false;
            //}
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            if (!MsgBox.MsgQuestion("엑셀 저장하시겠습니까?", "알림"))
            {
                return;
            }

            IWorkbook workbook = spreadsheetControl1.Document;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = this.Text;
            saveFileDialog.Title = "다른 경로로 저장";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx | Excel Files(*.xlsm) | *.xlsm";
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName + "_" + DateTime.Now.ToShortDateString().Replace("-", "").Replace("/", "")+".xlsx",
                FileMode.Create, FileAccess.ReadWrite))
                {
                    workbook.SaveDocument(stream, DocumentFormat.Xlsx);
                }
            }
        }

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "프로젝트코드")
            {
                int iValue1 = Convert.ToInt32(e.CellValue1);
                int iValue2 = Convert.ToInt32(e.CellValue2);

                e.Merge = true;
                e.Handled = true;
            }
            if (e.Column.FieldName == "부문")
            {
                int iValue1 = Convert.ToInt32(e.CellValue1);
                int iValue2 = Convert.ToInt32(e.CellValue2);

                e.Merge = true;
                e.Handled = true;
            }
            if (e.Column.FieldName == "예산계정명")
            {
                int iValue1 = Convert.ToInt32(e.CellValue1);
                int iValue2 = Convert.ToInt32(e.CellValue2);

                e.Merge = true;
                e.Handled = true;
            }
        }
    }
}
