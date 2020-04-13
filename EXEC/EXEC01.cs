using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DH_Core;
using DH_Core.DB;
using DevExpress.Spreadsheet;
using DevExpress.XtraCharts;

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

            DataSet ds;

            Workbook workbook = new Workbook();

            //workbook.LoadDocument("C:\\Users\\HJchoi_devP\\Downloads\\지출결의서_20200303 - 복사본(0).xlsx", DocumentFormat.Xlsx);


            spreadsheetControl1.LoadDocument("C:\\Users\\HJchoi_devP\\Downloads\\지출결의서_20200303 - 복사본(0).xlsx", DocumentFormat.Xlsx);
            ////관리구분 lookup
            //ds = df_select(3, null, out error_msg);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    modUTIL.DevLookUpEditorSet(ledt_ADMIN_GBN, ds.Tables[0], "NAME", "CODE");
            //    ledt_ADMIN_GBN.ItemIndex = 0;
            //}

            ////조정구분 lookup
            //ds = df_select(4, null, out error_msg);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    modUTIL.DevLookUpEditorSet(rledt_Gbn, ds.Tables[0], "CODE", "NAME" , "CODE", "NAME");
            //}

            getACCOUNT();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //DataRow dr = gridView1.GetFocusedDataRow();
                ////당기 신청 내역
                //DT_GRD02 = null;
                //gridControl2.DataSource = null;
                //gParam = new string[] { dr["ACT_CD"].ToString(), ((DateTime)dt_PLAN.EditValue).Year.ToString(), "0" };
                //DT_GRD02 = df_select(1, gParam, out error_msg);
                //if (DT_GRD02 == null)
                //{
                //    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                //    this.Cursor = Cursors.Default;
                //    return;
                //}

                //gridControl2.DataSource = DT_GRD02.Tables[0];

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }
        private void getACCOUNT()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DT_GRD01 = null;
                gridControl1.DataSource = null;
                DT_GRD01 = df_select(0, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
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
        #endregion

        #region DB CRUD(데이터베이스 처리)
        private DataSet df_select(int Index, object[] Param, out string error_msg)
        {
            DataSet dt = null;
            gConst.DbConn.DBClose();
            error_msg = "";
            switch (Index)
            {
                case 0: //회계계정 조회
                    {
                        //string query = "SELECT ACT_CD, ACT_NM FROM TB_ACCOUNT WITH(NOLOCK) WHERE ACT_CD LIKE '%' + @ACT_CD + '%' AND ACT_NM LIKE '%' + @ACT_NM + '%' AND CTRL_YN = 'Y' ";
                        //dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //예산 신청 내역
                    {
                        gConst.DbConn.ProcedureName = "USP_STD_GET_BUDGET_REQ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_GBN", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 2: //예산 신청 내역
                    {
                        gConst.DbConn.ProcedureName = "USP_STD_GET_BUDGET_REQ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_GBN", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 3: //부서조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '관리구분'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 4: //조정구분
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '조정구분'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 5: //예산 조정 내역
                    {
                        gConst.DbConn.ProcedureName = "USP_STD_GET_BUDGET_ADJ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        //gConst.DbConn.AddParameter(new SqlParameter("@ACT_GBN", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
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
            getACCOUNT();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;

            Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];

            DataRow dr_grid1 = gridView1.GetFocusedDataRow();
            if (DT_GRD05.HasChanges())
            {
                ds_new = DT_GRD05.GetChanges();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
            if (gridView1.DataSource == null) return;
            select_row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            getData();
        }

        private void dt_YEAR_EditValueChanged(object sender, EventArgs e)
        {
            getACCOUNT();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //DataRow DR;
            //gridView5.AddNewRow();
            //DR = gridView5.GetDataRow(gridView5.FocusedRowHandle);

            //DR["ADJ_DT"] = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy-MM-dd");
            //DR["ACT_CD"] = select_row["ACT_CD"];
            //DR["ADMIN_GBN"] = ledt_ADMIN_GBN.EditValue;
            ////DR["ADJ_MONTH"] = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy-MM");
            //DT_GRD05.Tables[0].Rows.Add(DR);
            //gridControl5.DataSource = null;
            //gridControl5.DataSource = DT_GRD05.Tables[0];
            //gridView5.FocusedRowHandle = gridView1.RowCount - 1;
        }

    }
}
