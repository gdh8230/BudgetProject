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

namespace STAT
{
    public partial class STAT01 : frmSub_Baseform_Search_STD
    {
        public STAT01()
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
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", bedt_DEPT.Tag.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 4: //프로젝트별
                    {
                        gConst.DbConn.ProcedureName = "REPORT_PJT";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@S_MONTH", ((DateTime)dt_START.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@E_MONTH", ((DateTime)dt_END.EditValue).Month.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_CD", bedt_DEPT.Tag.ToString()));
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
                string title;
                this.Cursor = Cursors.WaitCursor;
                DT_GRD01 = null;
                gridControl1.DataSource = null;
                DT_GRD01 = df_select(radioGroup1.SelectedIndex, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("지출결의서 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                switch (radioGroup1.SelectedIndex)
                {
                    case 0:
                        spreadsheetControl1.LoadDocument("예산대비집행양식.xlsx", DocumentFormat.Xlsx);
                        title = "예산 대비 집행/실적(조직)";

                        Set_Excel_Data(DT_GRD01, title);

                        break;
                    case 1:
                        spreadsheetControl1.LoadDocument("예산대비집행양식.xlsx", DocumentFormat.Xlsx);
                        title = "예산 대비 집행/실적(계정)";

                        Set_Excel_Data(DT_GRD01, title);

                        break;
                    case 2:
                        spreadsheetControl1.LoadDocument("예산대비집행양식.xlsx", DocumentFormat.Xlsx);
                        title = "예산 대비 집행/실적(" + bedt_DEPT.Text + ")";

                        Set_Excel_Data(DT_GRD01, title);

                        break;
                    case 3:
                        spreadsheetControl1.LoadDocument("예산대비집행양식.xlsx", DocumentFormat.Xlsx);
                        title = "예산 대비 집행/실적(프로젝트 - " + bedt_DEPT.Text + ")";

                        Set_Excel_Data(DT_GRD01, title);

                        break;
                    default:
                        break;
                }
                gridControl1.DataSource = DT_GRD01.Tables[0];
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
            sheet.Cells["B2"].Value = title;
            int Start_position = 6;
            int addrow_cnt = 0;
            int EndMonth = ((DateTime)dt_END.EditValue).Month;
            for (int i = 0; i < DT_GRD01.Tables[0].Rows.Count; i++)
            {
                if (DT_GRD01.Tables[0].Rows.Count > addrow_cnt + 10)
                {
                    sheet.Rows.Insert(Start_position + i);
                    sheet.Rows[Start_position + i].CopyFrom(sheet.Rows[Start_position + i + 1]);
                    addrow_cnt++;
                }

                sheet.Cells["B" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["NAME"].ToString();
                if(EndMonth >= 1 )
                {
                    sheet.Cells["C" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["1월 예산"].ToString());
                    sheet.Cells["D" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["1월 집행"].ToString());
                    if(EndMonth >= 2)
                    {
                        sheet.Cells["J" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["2월 예산"].ToString());
                        sheet.Cells["L" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["2월 집행"].ToString());
                        if(EndMonth >= 3)
                        {
                            sheet.Cells["R" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["3월 예산"].ToString());
                            sheet.Cells["T" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["3월 집행"].ToString());
                            if (EndMonth >= 4)
                            {
                                sheet.Cells["Z" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["4월 예산"].ToString());
                                sheet.Cells["AB" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["4월 집행"].ToString());
                                if (EndMonth >= 5)
                                {
                                    sheet.Cells["AH" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["5월 예산"].ToString());
                                    sheet.Cells["AJ" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["5월 집행"].ToString());
                                    if (EndMonth >= 6)
                                    {
                                        sheet.Cells["AP" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["6월 예산"].ToString());
                                        sheet.Cells["AR" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["6월 집행"].ToString());
                                        if (EndMonth >= 7)
                                        {
                                            sheet.Cells["AX" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["7월 예산"].ToString());;
                                            sheet.Cells["AZ" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["7월 집행"].ToString()); ;
                                            if (EndMonth >= 8)
                                            {
                                                sheet.Cells["BF" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["8월 예산"].ToString());
                                                sheet.Cells["BH" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["8월 집행"].ToString());
                                                if (EndMonth >= 9)
                                                {
                                                    sheet.Cells["BN" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["9월 예산"].ToString());
                                                    sheet.Cells["BP" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["9월 집행"].ToString());
                                                    if (EndMonth >= 10)
                                                    {
                                                        sheet.Cells["BV" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["10월 예산"].ToString());
                                                        sheet.Cells["BX" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["10월 집행"].ToString());
                                                        if (EndMonth >= 11)
                                                        {
                                                            sheet.Cells["CD" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["11월 예산"].ToString());
                                                            sheet.Cells["CF" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["11월 집행"].ToString());
                                                            if (EndMonth >= 12)
                                                            {
                                                                sheet.Cells["CL" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["12월 예산"].ToString());
                                                                sheet.Cells["CN" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["12월 집행"].ToString());
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

        }

        private void bedt_DEPT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "본부");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_DEPT.Tag = frm.CODE;
                bedt_DEPT.Text = frm.NAME;
            }
        }
        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            if(radioGroup1.SelectedIndex == 2)
            {
                labelControl5.Visible = true;
                bedt_DEPT.Visible = true;
            }
            else
            {
                labelControl5.Visible = false;
                bedt_DEPT.Visible = false;
            }
        }
    }
}
