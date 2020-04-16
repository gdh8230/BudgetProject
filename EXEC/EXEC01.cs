using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DH_Core;
using DH_Core.DB;
using DevExpress.Spreadsheet;
using DevExpress.XtraCharts;
using DH_Core.CommonPopup;
using DevExpress.XtraEditors;

namespace EXEC
{
    public partial class EXEC01 : frmSub_Baseform_Search_STD
    {
        public EXEC01()
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
            string error_msg = string.Empty;

            //로그인 사용자
            txt_DEPT.Tag = env.Dept;
            txt_DEPT.Text = env.DeptName;
            txt_PLAN_USER.Tag = env.EmpCode;
            txt_PLAN_USER.Text = env.EmpName;


            DataSet ds;

            spreadsheetControl1.LoadDocument("지출결의서_양식.xlsx", DocumentFormat.Xlsx);
            //사업구분 lookup
            ds = df_select(0, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_BUSSINESS_GBN, ds.Tables[0], "NAME", "CODE");
                ledt_BUSSINESS_GBN.ItemIndex = 0;
            }

            //환종 lookup
            ds = df_select(1, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(rledt_EXCH, ds.Tables[0], "CODE", "NAME", "CODE", "NAME");
            }

            getGridData();
        }

        private void getGridData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DT_GRD01 = null;
                gridControl1.DataSource = null;
                //지출결의서 디테일 조회
                gParam = new string[] { ADMIN_NO };
                DT_GRD01 = df_select(3, gParam, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl1.DataSource = DT_GRD01.Tables[0];

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
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
                case 0: //사업구분 lookup 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '사업구분'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //환종 lookup 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '환종'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 2: //지출결의서 헤더
                    {
                        gConst.DbConn.ProcedureName = "USP_EXEC_GET_SPND_RSLT_H";
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_NO", Param[0]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 3: //지출결의서 디테일
                    {
                        gConst.DbConn.ProcedureName = "USP_EXEC_GET_SPND_RSLT_D";
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_NO", Param[0]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 4: //지출결의서 디테일
                    {
                        string query = "SELECT EXCH_RATE FROM TB_EXCHANGE WITH(NOLOCK) WHERE YEAR = @YEAR AND MONTH = @MONTH AND EXCH_CD = @EXCH_CD";
                        gConst.DbConn.AddParameter(new SqlParameter("@EXCH_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@MONTH", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return dt;
        }

        private bool df_Transaction(int Index, object[] Param, DataRow dr, out string error_msg)
        {

            bool result = false;
            gConst.DbConn.ClearDB();
            error_msg = "";

            gConst.DbConn.BeginTrans();

            try
            {
                #region 입력/수정/삭제
                if (dr.RowState != DataRowState.Deleted)
                {

                    gConst.DbConn.ProcedureName = "dbo.USP_STD_SET_BUDGET_ADJ";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, dr.RowState.Equals(DataRowState.Added) ? "I" : "U");
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, dr["ACT_CD"].ToString());
                    gConst.DbConn.AddParameter("ADMIN_GBN", MSSQLAgent.DBFieldType.String, dr["ADMIN_GBN"].ToString());
                    gConst.DbConn.AddParameter("ADJ_GBN", MSSQLAgent.DBFieldType.String, dr["ADJ_GBN"].ToString()); //조정구분
                    gConst.DbConn.AddParameter("ADJ_DT", MSSQLAgent.DBFieldType.String, dr["ADJ_DT"].ToString());
                    gConst.DbConn.AddParameter("ADJ_MONTH", MSSQLAgent.DBFieldType.String, dr["ADJ_MONTH"].ToString());
                    gConst.DbConn.AddParameter("ADJ_MONEY", MSSQLAgent.DBFieldType.String, dr["ADJ_MONEY"].ToString());
                    gConst.DbConn.AddParameter("ADJ_NOTE", MSSQLAgent.DBFieldType.String, dr["ADJ_NOTE"].ToString());
                    gConst.DbConn.AddParameter("SEQ", MSSQLAgent.DBFieldType.String, dr["SEQ"].ToString());
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                }
                else
                {
                    gConst.DbConn.ProcedureName = "dbo.USP_STD_SET_BUDGET_ADJ";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "D");
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, dr["ACT_CD", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("ADMIN_GBN", MSSQLAgent.DBFieldType.String, dr["ADMIN_GBN", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("ADJ_GBN", MSSQLAgent.DBFieldType.String, null); //조정구분
                    gConst.DbConn.AddParameter("ADJ_DT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ADJ_MONTH", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ADJ_MONEY", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ADJ_NOTE", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("SEQ", MSSQLAgent.DBFieldType.String, dr["SEQ", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);

                }
                gConst.DbConn.ExecuteNonQuery(out error_msg);
                if (!error_msg.Equals(""))
                {
                    MsgBox.MsgErr("다음 사유로 인하여 처리되지 않았습니다.\n" + error_msg  , "저장오류");
                }
                #endregion
                gConst.DbConn.ClearDB();
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
                gConst.DbConn.Rollback();
            }

            gConst.DbConn.Commit();
            return result;
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            getGridData();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;

            Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];

            DataRow dr_grid1 = gridView1.GetFocusedDataRow();
            if (DT_GRD01.HasChanges())
            {
                ds_new = DT_GRD01.GetChanges();
                foreach (DataRow dr in ds_new.Tables[0].Rows)
                {
                    gParam = new string[] { dr_grid1["ACT_CD"].ToString(), ((DateTime)dt_PLAN.EditValue).Year.ToString() };
                    df_Transaction(20, gParam, dr, out gOut_MSG);
                }
                MsgBox.MsgInformation("저장 완료", "확인");
                btn_Search_Click(null, null);
                return;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (DT_GRD01.HasChanges())
            {
                if (!MsgBox.MsgQuestion("변경사항이 있습니다. 저장하신 후 진행하시겠습니까?", "알림"))
                {
                    return;
                }
                else
                {
                    btn_Save_Click(null, null);
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

            DataRow DR;
            gridView1.AddNewRow();
            DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            DR["SEQ"] = gridView1.RowCount;
            //DR["ACT_CD"] = select_row["ACT_CD"];
            //DR["ADMIN_GBN"] = ledt_ADMIN_GBN.EditValue;
            ////DR["ADJ_MONTH"] = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy-MM");
            DT_GRD01.Tables[0].Rows.Add(DR);
            gridControl1.DataSource = null;
            gridControl1.DataSource = DT_GRD01.Tables[0];
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];

            //sheet.InsertCells(sheet.Cells["AW3"], InsertCellsMode.ShiftCellsDown);
            sheet.Cells["AW3"].Value = DateTime.Parse(dt_PLAN.EditValue.ToString()).ToString("yyyy-MM-dd");
            sheet.Cells["BJ3"].Value = DateTime.Parse(dt_BILL.EditValue.ToString()).ToString("yyyy-MM-dd");
            sheet.Cells["AW4"].Value = txt_DEPT.Text;
            sheet.Cells["BJ4"].Value = txt_PLAN_USER.Text;
            sheet.Cells["AW5"].Value = ledt_BUSSINESS_GBN.Text;
            sheet.Cells["BJ5"].Value = bedt_PJT.Text;
            sheet.Cells["AW6"].Value = txt_PLAN_TITLE.Text;
            sheet.Cells["AQ12"].Value = txt_PLAN_CONTENT.Text;
        }

        private void rledt_EXCH_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookupEdit = sender as LookUpEdit;

            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            gParam = new string[] { lookupEdit.EditValue.ToString(), ((DateTime)dt_PLAN.EditValue).Year.ToString(), ((DateTime)dt_PLAN.EditValue).ToString("MM")};
            DataSet ds = df_select(4, gParam, out error_msg );
            if(ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DR["EXCH_RATE"] = ds.Tables[0].Rows[0]["EXCH_RATE"];
                }
                else
                {
                    DR["EXCH_RATE"] = 0;
                }
            }
        }

        private void bedt_PJT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_PJT_CD frm = new frm_PUP_GET_PJT_CD(env, null, null);

            if(frm.ShowDialog() == DialogResult.OK)
            {
                bedt_PJT.Tag = frm.PJT_CD;
                bedt_PJT.Text = frm.PJT_NAME;
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "EXCH_RATE" || e.Column.FieldName == "PRICE" || e.Column.FieldName == "AMOUNT")
            {
                DataRow dr = gridView1.GetDataRow(e.RowHandle);
                if (dr["EXCH_RATE"].ToString() != "" && dr["PRICE"].ToString() != "" && dr["AMOUNT"].ToString() != "")
                {
                    dr["TOTAL"] = double.Parse(dr["EXCH_RATE"].ToString()) * double.Parse(dr["PRICE"].ToString()) * double.Parse(dr["AMOUNT"].ToString());
                }
            }

        }

        private void rtedt_ACT_DoubleClick(object sender, EventArgs e)
        {
            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "계정");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DR["ACT_CD"] = frm.CODE;
                DR["ACT_NM"] = frm.NAME;
            }
        }
    }
}
