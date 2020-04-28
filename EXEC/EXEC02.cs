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
using System.IO;

namespace EXEC
{
    public partial class EXEC02 : frmSub_Baseform_Search_STD
    {
        public EXEC02()
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

            ////화면 Claer
            //ClearForm();
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("NAME");

            dt.Rows.Add("%", "전체");
            dt.Rows.Add("0", "미결");
            dt.Rows.Add("1", "결재");

            modUTIL.DevLookUpEditorSet(ledt_GW_YN, dt, "NAME", "CODE");
            ledt_GW_YN.ItemIndex = 0;


            DataSet ds;

            //사업구분 lookup
            ds = df_select(0, new string[] { "0" }, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_BUSSINESS_GBN, ds.Tables[0], "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_BUSSINESS_GBN2, ds.Tables[0], "NAME", "CODE");
                ledt_BUSSINESS_GBN.ItemIndex = 0;
            }

            //환종 lookup
            ds = df_select(1, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(rledt_EXCH, ds.Tables[0], "CODE", "NAME", "CODE", "NAME");
            }

            //대계정 lookup
            ds = df_select(2, new string[] { "0" }, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_CLASS, ds.Tables[0], "NAME", "CODE");
                ledt_CLASS.ItemIndex = 0;
            }

            //getHeaderData();
            //getGridData();
        }

        private void getHeaderData()
        {
            try 
            { 
                this.Cursor = Cursors.WaitCursor;

                DataSet ds = new DataSet();
                //지출결의서 헤더 조회
                ds = df_select(2, null, out error_msg);
                if (ds == null)
                {
                    MsgBox.MsgErr("지출결의서 헤더 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }

        private void getGridData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DT_GRD01 = null;
                gridControl3.DataSource = null;
                //지출결의서 디테일 조회
                gParam = new string[] { ADMIN_NO };
                DT_GRD01 = df_select(3, gParam, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl3.DataSource = DT_GRD01.Tables[0];

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
                case 0: //지출결의서 헤더 조회
                    {
                        string query = string.Empty;
                        query += "SELECT A.*, B.UNAM as PLANER , C.UNAM as [USER], CASE ISNULL(GW_NO,'') WHEN '' THEN '미결' ELSE '결재' END as GW_YN, D.PJT_NM  ";
                        query += "FROM SPND_RSLT_H A WITH(NOLOCK)  ";
                        query += "LEFT JOIN TS_USER B WITH(NOLOCK)  ";
                        query += "ON		A.PLAN_USER= B.USR  ";
                        query += "LEFT JOIn TS_USER C WITH(NOLOCK)  ";
                        query += "ON		A.[USER] = C.USR  ";
                        query += "LEFT JOIN TB_PJT D WITH(NOLOCK)  ";
                        query += "ON		A.PJT_CD = D.PJT_CD  ";
                        query += "WHERE	PLAN_DT BETWEEN '" + DatePicker1.GetStartDate.ToString("yyyyMMdd") + "' AND '" + DatePicker1.GetEndDate.ToString("yyyyMMdd") + "'  ";
                        query += "AND		PLAN_USER LIKE '" + bedt_PLAN_USER.Tag + "' + '%'  ";
                        query += "AND		A.DEPT LIKE '" + bedt_DEPT.Tag + "' + '%'  ";
                        query += "AND		[USER] LIKE '" + bedt_USER.Tag + "' + '%'  ";
                        query += "AND		A.PJT_CD LIKE '" + bedt_PJT.Tag + "' + '%'  ";
                        query += "AND		BUSINESS_GBN LIKE '" + ledt_BUSSINESS_GBN.EditValue + "' + '%'  ";
                        query += "AND		CASE ISNULL(GW_NO,'') WHEN '' THEN '0' ELSE '1' END  LIKE '" + ledt_GW_YN.EditValue + "'  ";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //환종 lookup 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '환종'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 2: //대계정 조회
                    {
                        string query = string.Empty;
                        if (Param[0].Equals("0")) query += "SELECT '%' CODE, '전체' NAME UNION ALL "; // param[0] 값이 0일경우 전체포함 1일경우 전체미포함
                        query += "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 3: //소계정 조회
                    {
                        string query = string.Empty;
                        if (Param[0].Equals("0")) query += "SELECT '%' CODE, '전체' NAME UNION ALL "; // param[0] 값이 0일경우 전체포함 1일경우 전체미포함
                        query += "SELECT	ACT_CD AS CODE ,BUDGET_NM AS NAME FROM	TB_ACCOUNT WHERE	CLASS LIKE  '" + ledt_CLASS.EditValue.ToString() + "'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 4: //환율 정보
                    {
                        string query = "SELECT EXCH_RATE FROM TB_EXCHANGE WITH(NOLOCK) WHERE YEAR = @YEAR AND MONTH = @MONTH AND EXCH_CD = @EXCH_CD";
                        gConst.DbConn.AddParameter(new SqlParameter("@EXCH_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@MONTH", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 5: //예산
                    {
                        gConst.DbConn.ProcedureName = "USP_EXEC_GET_BUDGET";
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_CD", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@CLASS", Param[2]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[3]));
                        gConst.DbConn.AddParameter(new SqlParameter("@MONTH", Param[4]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return dt;
        }

        private bool Detail_Transaction(object[] Param, DataRow dr, out string error_msg)
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

                    gConst.DbConn.ProcedureName = "dbo.USP_EXEC_SET_SPND_RSLT_D";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, dr.RowState.Equals(DataRowState.Added) ? "I" : "U");
                    gConst.DbConn.AddParameter("SEQ", MSSQLAgent.DBFieldType.String, dr["SEQ"].ToString());
                    gConst.DbConn.AddParameter("ADMIN_NO", MSSQLAgent.DBFieldType.String,ADMIN_NO);
                    gConst.DbConn.AddParameter("ITEM_NM", MSSQLAgent.DBFieldType.String, dr["ITEM_NM"].ToString()); //조정구분
                    gConst.DbConn.AddParameter("EXCH_CD", MSSQLAgent.DBFieldType.String, dr["EXCH_CD"].ToString());
                    gConst.DbConn.AddParameter("EXCH_RATE", MSSQLAgent.DBFieldType.String, dr["EXCH_RATE"].ToString());
                    gConst.DbConn.AddParameter("PRICE", MSSQLAgent.DBFieldType.String, dr["PRICE"].ToString());
                    gConst.DbConn.AddParameter("AMOUNT", MSSQLAgent.DBFieldType.String, dr["AMOUNT"].ToString());
                    gConst.DbConn.AddParameter("UNIT", MSSQLAgent.DBFieldType.String, dr["UNIT"].ToString());
                    gConst.DbConn.AddParameter("TOTAL", MSSQLAgent.DBFieldType.String, dr["TOTAL"].ToString());
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, dr["ACT_CD"].ToString());
                    gConst.DbConn.AddParameter("CLASS", MSSQLAgent.DBFieldType.String, dr["CLASS"].ToString());
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                }
                else
                {
                    gConst.DbConn.ProcedureName = "dbo.USP_EXEC_SET_SPND_RSLT_D";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "D");
                    gConst.DbConn.AddParameter("SEQ", MSSQLAgent.DBFieldType.String, dr["SEQ", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("ADMIN_NO", MSSQLAgent.DBFieldType.String, ADMIN_NO);
                    gConst.DbConn.AddParameter("ITEM_NM", MSSQLAgent.DBFieldType.String, null); //조정구분
                    gConst.DbConn.AddParameter("EXCH_CD", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("EXCH_RATE", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PRICE", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("AMOUNT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("UNIT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("TOTAL", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("CLASS", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);

                }
                if(!gConst.DbConn.ExecuteNonQuery(out error_msg))
                {
                    MsgBox.MsgErr("다음 사유로 인하여 처리되지 않았습니다.\n" + error_msg, "저장오류");
                    gConst.DbConn.Rollback();
                }
                else
                {

                }
                #endregion
                gConst.DbConn.ClearDB();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
                gConst.DbConn.Rollback();
            }

            gConst.DbConn.Commit();
            return result;
        }

        private bool Header_Transaction(object[] Param, DataRow dr, out string error_msg)
        {

            bool result = false;
            gConst.DbConn.ClearDB();
            error_msg = "";

            gConst.DbConn.BeginTrans();

            try
            {
                #region 입력/수정/삭제
                gConst.DbConn.ProcedureName = "dbo.USP_EXEC_SET_SPND_RSLT_H";
                gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, Param[0]);
                gConst.DbConn.AddParameter("ADMIN_NO", MSSQLAgent.DBFieldType.String, ADMIN_NO);
                gConst.DbConn.AddParameter("COMP_NAME", MSSQLAgent.DBFieldType.String, txt_COMP_NAME.Text);
                gConst.DbConn.AddParameter("COMP_ACCT", MSSQLAgent.DBFieldType.String, txt_COMP_ACCT.Text);
                gConst.DbConn.AddParameter("COMP_BANK", MSSQLAgent.DBFieldType.String, txt_COMP_BANK.Text);
                gConst.DbConn.AddParameter("ACCT_HOLDER", MSSQLAgent.DBFieldType.String, txt_ACCT_HOLDER.Text);
                gConst.DbConn.AddParameter("PAY_DT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dt_PAY.EditValue.ToString()).ToString("yyyyMMdd"));
                gConst.DbConn.AddParameter("COMP_MNG", MSSQLAgent.DBFieldType.String, txt_COMP_MNG.Text);
                gConst.DbConn.AddParameter("COMP_MNG_PHONE", MSSQLAgent.DBFieldType.String, txt_COMP_MNG_PHONE.Text);
                gConst.DbConn.AddParameter("DCMNT1", MSSQLAgent.DBFieldType.Image, txt_DCMNT1.Tag);
                gConst.DbConn.AddParameter("DCMNT1_NM", MSSQLAgent.DBFieldType.String, txt_DCMNT1_NM.Text);
                gConst.DbConn.AddParameter("DCMNT2", MSSQLAgent.DBFieldType.Image, txt_DCMNT2.Tag);
                gConst.DbConn.AddParameter("DCMNT2_NM", MSSQLAgent.DBFieldType.String, txt_DCMNT2_NM.Text);
                gConst.DbConn.AddParameter("DCMNT3", MSSQLAgent.DBFieldType.Image, txt_DCMNT3.Tag) ;
                gConst.DbConn.AddParameter("DCMNT3_NM", MSSQLAgent.DBFieldType.String, txt_DCMNT3_NM.Text);
                //gConst.DbConn.AddParameter("USER", MSSQLAgent.DBFieldType.String, txt_USER.Tag.ToString());
                gConst.DbConn.AddParameter("USER", MSSQLAgent.DBFieldType.String, bedt_USER.Tag);
                gConst.DbConn.AddParameter("GW_NO", MSSQLAgent.DBFieldType.String, null);
                gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                gConst.DbConn.AddParameter("OUTPUT", MSSQLAgent.DBFieldType.String, error_msg, 20, MSSQLAgent.DBDirection.Output);
                if(!gConst.DbConn.ExecuteNonQuery(out error_msg, ""))
                {
                    MsgBox.MsgErr("다음 사유로 인하여 처리되지 않았습니다.\n" + error_msg, "저장오류");
                    gConst.DbConn.Rollback();
                }
                else
                {
                    ADMIN_NO = error_msg;
                    result = true;
                }
                #endregion
                gConst.DbConn.ClearDB();
            }
            catch (Exception ee)
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DT_GRD01 = null;
                gridControl1.DataSource = null;
                DT_GRD01 = df_select(0, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("지출결의서 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;

            //헤더저장
            if(Header_Transaction(gParam, null, out error_msg))
            {
                //디테일 저장
                if (DT_GRD01.HasChanges())
                {
                    ds_new = DT_GRD01.GetChanges();
                    foreach (DataRow dr in ds_new.Tables[0].Rows)
                    {
                        gParam = new string[] { ADMIN_NO };
                        Detail_Transaction(gParam, dr, out gOut_MSG);
                    }
                    MsgBox.MsgInformation("저장 완료", "확인");
                    //btn_Search_Click(null, null);
                    return;
                }
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
            DR["TOTAL"] = 0;
            //DR["ACT_CD"] = select_row["ACT_CD"];
            //DR["ADMIN_GBN"] = ledt_ADMIN_GBN.EditValue;
            ////DR["ADJ_MONTH"] = DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy-MM");
            DT_GRD01.Tables[0].Rows.Add(DR);
            gridControl3.DataSource = null;
            gridControl3.DataSource = DT_GRD01.Tables[0];
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }

        private void bedt_DEPT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "부서");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_DEPT.Tag = frm.CODE;
                bedt_DEPT.Text = frm.NAME;
            }
        }

        private void bedt_PJT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "프로젝트");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_USER.Tag = frm.CODE;
                bedt_USER.Text = frm.NAME;
            }
        }

        private void bedt_PLAN_USER_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "사원", bedt_DEPT.Tag.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_PLAN_USER.Tag = frm.CODE;
                bedt_PLAN_USER.Text = frm.NAME;
            }
        }

        private void bedt_USER_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "사원");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_USER.Tag = frm.CODE;
                bedt_USER.Text = frm.NAME;
            }
        }

        private void ledt_CLASS_EditValueChanged(object sender, EventArgs e)
        {
            DataSet ds = df_select(3, new string[] { "0" }, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_ACT, ds.Tables[0], "NAME", "CODE");
                ledt_ACT.ItemIndex = 0;
            }

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView2.GetFocusedDataRow();

            if(dr == null)
            {
                return;
            }
            dt_PAY.EditValue = dr["PAY_DT"];
            dt_BILL.EditValue = dr["BILL_DT"];
            txt_ACCT_HOLDER.Text = dr["ACCT_HOLDER"].ToString();
            txt_COMP_ACCT.Text = dr["COMP_ACCT"].ToString();
            txt_COMP_BANK.Text = dr["COMP_BANK"].ToString();
            txt_COMP_MNG.Text = dr["COMP_MNG"].ToString();
            txt_COMP_MNG_PHONE.Text = dr["COMP_MNG_PHONE"].ToString();
            txt_COMP_NAME.Text = dr["COMP_NAME"].ToString();
            txt_DCMNT1_NM.Text = dr["DCMNT1_NM"].ToString();
            txt_DCMNT2_NM.Text = dr["DCMNT2_NM"].ToString();
            txt_DCMNT3_NM.Text = dr["DCMNT3_NM"].ToString();
            txt_PLAN_CONTENT.Text = dr["PLAN_CONTENT"].ToString();
            txt_PLAN_TITLE.Text = dr["PLAN_TITLE"].ToString();
            bedt_PJT2.Text = dr["PJT_NM"].ToString();
            bedt_PJT2.Tag = dr["PJT_CD"].ToString();
            bedt_USER2.Text = dr["USER1"].ToString();
            bedt_USER2.Tag = dr["USER"].ToString();
            ledt_BUSSINESS_GBN2.EditValue = dr["BUSINESS_GBN"];

        }
    }
}
