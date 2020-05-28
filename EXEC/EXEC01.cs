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
        double drowup = 0;
        double use = 0;
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

            //DateEdit 초기화
            dt_PLAN.DateTime = DateTime.Today;
            dt_PAY.DateTime = DateTime.Today;
            dt_BILL.DateTime = DateTime.Today;
            ////화면 Claer
            //ClearForm();

            //로그인 사용자
            bedt_DEPT.Tag = env.Dept;
            bedt_DEPT.Text = env.DeptName;
            bedt_PLAN_USER.Tag = env.EmpCode;
            bedt_PLAN_USER.Text = env.EmpName;


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
            getHeaderData();
            getGridData();
        }

        private void ClearForm()
        {
            ADMIN_NO = null;
            DT_GRD01.Clear(); ;
            gridControl1.DataSource = DT_GRD01.Tables[0];

            dt_PLAN.DateTime = DateTime.Today;
            dt_PAY.DateTime = DateTime.Today;
            dt_BILL.DateTime = DateTime.Today;
            txt_ACCT_HOLDER.Text = "";
            txt_COMP_ACCT.Text = "";
            txt_COMP_BANK.Text = "";
            txt_COMP_MNG.Text = "";
            txt_COMP_MNG_PHONE.Text = "";
            txt_COMP_NAME.Text = "";
            txt_DCMNT1_NM.Text = "";
            txt_DCMNT1.Text = "";
            txt_DCMNT1.Tag = "";
            txt_DCMNT2_NM.Text = "";
            txt_DCMNT2.Text = "";
            txt_DCMNT2.Tag = "";
            txt_DCMNT3_NM.Text = "";
            txt_DCMNT3.Text = "";
            txt_DCMNT3.Tag = "";
            medt_PLAN_CONTENT.Text = "";
            txt_PLAN_TITLE.Text = "";
            bedt_PJT.Text = "";
            bedt_PJT.Tag = "";
            bedt_USER.Text = "";
            bedt_USER.Tag = "";
            txt_ADMIN_NO.Text = "";
            txt_GW_NO.Text = "";

            EnableControl(true);
        }

        private void EnableControl(bool tp)
        {
            dt_PLAN.Enabled =tp;
            dt_PAY.Enabled = tp;
            dt_BILL.Enabled = tp;
            txt_ACCT_HOLDER.Enabled = tp;
            txt_COMP_ACCT.Enabled = tp;
            txt_COMP_BANK.Enabled = tp;
            txt_COMP_MNG.Enabled = tp;
            txt_COMP_MNG_PHONE.Enabled = tp;
            txt_COMP_NAME.Enabled = tp;
            txt_DCMNT1.Enabled = tp;
            txt_DCMNT1_NM.Enabled = tp;
            txt_DCMNT2.Enabled = tp;
            txt_DCMNT2_NM.Enabled = tp;
            txt_DCMNT3.Enabled = tp;
            txt_DCMNT3_NM.Enabled = tp;
            btn_DCMNT1.Enabled = tp;
            btn_DCMNT2.Enabled = tp;
            btn_DCMNT3.Enabled = tp;
            medt_PLAN_CONTENT.Enabled = tp;
            txt_PLAN_TITLE.Enabled = tp;
            ledt_BUSSINESS_GBN.Enabled = tp;
            bedt_PJT.Enabled = tp;
            bedt_USER.Enabled = tp;
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

                if(ds.Tables[0].Rows.Count > 0)
                {
                    txt_ADMIN_NO.Text = ADMIN_NO;
                    dt_PLAN.EditValue = DateTime.Parse(ds.Tables[0].Rows[0]["PLAN_DT"].ToString());
                    dt_PAY.EditValue = DateTime.Parse(ds.Tables[0].Rows[0]["PAY_DT"].ToString());
                    dt_BILL.EditValue = DateTime.Parse(ds.Tables[0].Rows[0]["BILL_DT"].ToString());
                    txt_ACCT_HOLDER.Text = ds.Tables[0].Rows[0]["ACCT_HOLDER"].ToString();
                    txt_COMP_ACCT.Text = ds.Tables[0].Rows[0]["COMP_ACCT"].ToString();
                    txt_COMP_BANK.Text = ds.Tables[0].Rows[0]["COMP_BANK"].ToString();
                    txt_COMP_MNG.Text = ds.Tables[0].Rows[0]["COMP_MNG"].ToString();
                    txt_COMP_MNG_PHONE.Text = ds.Tables[0].Rows[0]["COMP_MNG_PHONE"].ToString();
                    txt_COMP_NAME.Text = ds.Tables[0].Rows[0]["COMP_NAME"].ToString();
                    txt_DCMNT1_NM.Text = ds.Tables[0].Rows[0]["DCMNT1_NM"].ToString();
                    txt_DCMNT2_NM.Text = ds.Tables[0].Rows[0]["DCMNT2_NM"].ToString();
                    txt_DCMNT3_NM.Text = ds.Tables[0].Rows[0]["DCMNT3_NM"].ToString();
                    txt_DCMNT1.Text = ds.Tables[0].Rows[0]["DCMNT1"].ToString(); //? null : ds.Tables[0].Rows[0]["DCMNT1"].ToString();
                    txt_DCMNT2.Text = ds.Tables[0].Rows[0]["DCMNT2"].ToString();
                    txt_DCMNT3.Text = ds.Tables[0].Rows[0]["DCMNT3"].ToString();
                    medt_PLAN_CONTENT.Text = ds.Tables[0].Rows[0]["PLAN_CONTENT"].ToString();
                    txt_PLAN_TITLE.Text = ds.Tables[0].Rows[0]["PLAN_TITLE"].ToString();
                    bedt_PJT.Text = ds.Tables[0].Rows[0]["PJT_NM"].ToString();
                    bedt_PJT.Tag = ds.Tables[0].Rows[0]["PJT_CD"].ToString();
                    bedt_USER.Text = ds.Tables[0].Rows[0]["UNAM"].ToString();
                    bedt_USER.Tag = ds.Tables[0].Rows[0]["USER"].ToString();
                    txt_GW_NO.Text = ds.Tables[0].Rows[0]["GW_NO"].ToString();
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
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_NO", ADMIN_NO));
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
                case 6: //부서별 예산 편성/집행 금액 조회
                    {
                        string query = string.Empty;
                        query += "DECLARE @SECT NVARCHAR(20) ";
                        query += "SELECT @SECT = SECT_CD FROM TS_DEPT WHERE DEPT = '" + Param[2] + "' ";
                        query += "SELECT SUM(DROWUP) FROM( ";
                        query += "SELECT ISNULL(SUM(A.DROWUP_MONEY),0) + ISNULL(SUM(B.ADJ_MONEY),0) AS DROWUP ";
                        query += "FROM BUDGET_CTRL A WITH(NOLOCK) ";
                        query += "LEFT JOIN (SELECT ADJ_YEAR, ADJ_MONTH, ADMIN_CD, SUM(ADJ_MONEY) as ADJ_MONEY   FROM BUDGET_ADJ WITH(NOLOCK) WHERE ADMIN_GBN = 0 GROUP BY ADJ_YEAR, ADJ_MONTH, ADMIN_CD) B ";
                        query += "ON A.YEAR = B.ADJ_YEAR ";
                        query += "AND A.MONTH = B.ADJ_MONTH ";
                        query += "AND	A.ADMIN_CD = B.ADMIN_CD ";
                        query += "WHERE ACT_GBN = 1 AND A.ADMIN_GBN = 0 AND YEAR = '" + Param[0] + "'  AND MONTH <= '" + Param[1] + "' AND A.ADMIN_CD in (SELECT DEPT FROM TS_DEPT WHERE SECT_CD = @SECT) AND A.STAT <> 'D' ";
                        query += "GROUP BY A.YEAR, A.MONTH ) A ";

                        query += "SELECT	ISNULL(SUM(B.TOTAL),0) ";
                        query += "FROM	SPND_RSLT_H	A WITH(NOLOCK) ";
                        query += "JOIN	SPND_RSLT_D B WITH(NOLOCK) ";
                        query += "ON		A.ADMIN_NO = B.ADMIN_NO ";
                        query += "WHERE	LEFT(PLAN_DT,4) = '" + Param[0] + "' AND LEFT(PLAN_DT,6) <= '" + Param[0] + "'+'" + Param[1] + "' AND DEPT in (SELECT DEPT FROM TS_DEPT WHERE SECT_CD = @SECT) AND A.STAT<> 'D' AND B.STAT <> 'D'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
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
                    gConst.DbConn.AddParameter("COMP_NAME", MSSQLAgent.DBFieldType.String, dr["COMP_NAME"].ToString());
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
                    gConst.DbConn.AddParameter("COMP_NAME", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);

                }
                if (!gConst.DbConn.ExecuteNonQuery(out error_msg))
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
                gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "I");
                gConst.DbConn.AddParameter("ADMIN_NO", MSSQLAgent.DBFieldType.String, ADMIN_NO);
                gConst.DbConn.AddParameter("PLAN_DT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dt_PLAN.EditValue.ToString()).ToString("yyyyMMdd"));
                gConst.DbConn.AddParameter("DEPT", MSSQLAgent.DBFieldType.String, bedt_DEPT.Tag.ToString());
                gConst.DbConn.AddParameter("DEPT_NAME", MSSQLAgent.DBFieldType.String, bedt_DEPT.Text);
                gConst.DbConn.AddParameter("PLAN_USER", MSSQLAgent.DBFieldType.String, env.EmpCode);
                gConst.DbConn.AddParameter("BUSINESS_GBN", MSSQLAgent.DBFieldType.String, ledt_BUSSINESS_GBN.EditValue);
                gConst.DbConn.AddParameter("PJT_CD", MSSQLAgent.DBFieldType.String, bedt_PJT.Tag);
                gConst.DbConn.AddParameter("BILL_DT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dt_BILL.EditValue.ToString()).ToString("yyyyMMdd"));
                gConst.DbConn.AddParameter("PLAN_TITLE", MSSQLAgent.DBFieldType.String, txt_PLAN_TITLE.Text);
                gConst.DbConn.AddParameter("PLAN_CONTENT", MSSQLAgent.DBFieldType.String, medt_PLAN_CONTENT.Text);
                //gConst.DbConn.AddParameter("COMP_NAME", MSSQLAgent.DBFieldType.String, txt_COMP_NAME.Text);
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
            popup_Get_AdminNo _frm = new popup_Get_AdminNo(env, null);
            if(_frm.ShowDialog() == DialogResult.OK)
            {
                ADMIN_NO = _frm.ADMIN_NO;
                EnableControl(false);
                getHeaderData();
                getGridData();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (!txt_GW_NO.Text.Equals("") && !env.EmpCode.Equals("suser"))
            {
                MsgBox.MsgInformation("결재 완료되어 수정/삭제가 불가능합니다.", "확인");
                return;
            }
            if (!txt_DCMNT1.Text.Equals("") && txt_DCMNT1_NM.Text.Equals(""))
            {
                MsgBox.MsgInformation("첨부문서명을 입력하세요.", "확인");
                return;
            }
            if (!txt_DCMNT2.Text.Equals("") && txt_DCMNT2_NM.Text.Equals(""))
            {
                MsgBox.MsgInformation("첨부문서명을 입력하세요.", "확인");
                return;
            }
            if (!txt_DCMNT3.Text.Equals("") && txt_DCMNT3_NM.Text.Equals(""))
            {
                MsgBox.MsgInformation("첨부문서명을 입력하세요.", "확인");
                return;
            }

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
            EnableControl(true);
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
            if (!txt_GW_NO.Text.Equals(""))
            {
                MsgBox.MsgInformation("결재 완료되어 수정 불가능합니다.", "확인");
                return;
            }
            DataRow DR;
            gridView1.AddNewRow();
            DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            DR["SEQ"] = gridView1.RowCount;
            DR["TOTAL"] = 0;
            DR["EXCH_CD"] = "001"; 
            gParam = new string[] { DR["EXCH_CD"].ToString(), ((DateTime)dt_PLAN.EditValue).Year.ToString(), ((DateTime)dt_PLAN.EditValue).ToString("MM") };
            DataSet ds = df_select(4, gParam, out error_msg);
            if (ds != null)
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
            if (ADMIN_NO == null || ADMIN_NO == "") return;

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
                using (FileStream stream = new FileStream(saveFileDialog.FileName + "_" + DateTime.Now.ToShortDateString().Replace("-", "").Replace("/", "") + ".xlsx",
                FileMode.Create, FileAccess.ReadWrite))
                {
                    workbook.SaveDocument(stream, DocumentFormat.Xlsx);
                }
            }
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
            DataRow dr = gridView1.GetDataRow(e.RowHandle);
            if (e.Column.FieldName == "EXCH_RATE" || e.Column.FieldName == "PRICE" || e.Column.FieldName == "AMOUNT")
            {
                if (dr["EXCH_RATE"].ToString() != "" && dr["PRICE"].ToString() != "" && dr["AMOUNT"].ToString() != "")
                {
                    dr["TOTAL"] = double.Parse(dr["EXCH_RATE"].ToString()) * double.Parse(dr["PRICE"].ToString()) * double.Parse(dr["AMOUNT"].ToString());
                }
            }
            if (e.Column.FieldName == "EXCH_RATE" || e.Column.FieldName == "PRICE" || e.Column.FieldName == "AMOUNT" || e.Column.FieldName == "ACT_CD")
            {
                if (dr["EXCH_RATE"].ToString() != "" && dr["PRICE"].ToString() != "" && dr["AMOUNT"].ToString() != "" && dr["ACT_CD"].ToString() != "")
                {
                    DataSet ds;
                    gParam = new string[] { bedt_DEPT.Tag.ToString(), bedt_PJT.Tag == null ? "" : bedt_PJT.Tag.ToString(), dr["CLASS"].ToString(), ((DateTime)dt_PLAN.EditValue).Year.ToString(), ((DateTime)dt_PLAN.EditValue).ToString("MM") };
                    ds = df_select(5, gParam, out gOut_MSG);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dr["BUDGET_MONEY_DEPT"] = ds.Tables[0].Rows[0][0];
                            dr["REMAIN_MONEY_DEPT"] = double.Parse(ds.Tables[0].Rows[0][0].ToString()) - double.Parse(ds.Tables[0].Rows[0][1].ToString()) - double.Parse(dr["TOTAL"].ToString());
                        }
                        else
                        {
                            dr["BUDGET_MONEY_DEPT"] = 0;
                            dr["REMAIN_MONEY_DEPT"] = 0 - double.Parse(dr["TOTAL"].ToString());
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            dr["BUDGET_MONEY_PJT"] = ds.Tables[1].Rows[0][0];
                            dr["REMAIN_MONEY_PJT"] = double.Parse(ds.Tables[1].Rows[0][0].ToString()) - double.Parse(ds.Tables[1].Rows[0][1].ToString()) - double.Parse(dr["TOTAL"].ToString());
                        }
                        else
                        {
                            dr["BUDGET_MONEY_PJT"] = 0;
                            dr["REMAIN_MONEY_PJT"] = 0 - double.Parse(dr["TOTAL"].ToString());
                        }
                    }
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
                DR["CLASS"] = frm.CLASS;
                DR["CLASS_NM"] = frm.CLASS_NM;
            }
        }

        private void bedt_USER_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "사원", "%");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_USER.Tag = frm.CODE;
                bedt_USER.Text = frm.NAME;
            }
        }

        private void btn_excel_make_Click(object sender, EventArgs e)
        {
            try
            {
                if(ADMIN_NO == null || ADMIN_NO == "")
                {
                    MsgBox.MsgInformation("저장 후 생성가능합니다.", "확인");
                    return;
                }
                spreadsheetControl1.LoadDocument("지출결의서_양식.xlsx", DocumentFormat.Xlsx);
                Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];
                sheet.ActiveView.ShowGridlines = false;
                sheet.ActiveView.ShowFormulas = false;

                sheet.Cells["AW3"].Value = DateTime.Parse(dt_PLAN.EditValue.ToString()).ToString("yyyy-MM-dd");
                sheet.Cells["BJ3"].Value = DateTime.Parse(dt_BILL.EditValue.ToString()).ToString("yyyy-MM-dd");
                sheet.Cells["AW4"].Value = bedt_DEPT.Text;
                sheet.Cells["BJ4"].Value = bedt_PLAN_USER.Text;
                sheet.Cells["AW5"].Value = ledt_BUSSINESS_GBN.Text;
                sheet.Cells["BJ5"].Value = bedt_PJT.Text;
                sheet.Cells["AW6"].Value = txt_PLAN_TITLE.Text;
                sheet.Cells["AQ12"].Value = medt_PLAN_CONTENT.Text;


                int Start_position = 18;
                int addrow_cnt = 0;

                for (int i = 0; i < DT_GRD01.Tables[0].Rows.Count; i++)
                {
                    if (DT_GRD01.Tables[0].Rows.Count > addrow_cnt + 5)
                    {
                        sheet.Rows.Insert(Start_position + i);
                        sheet.Rows[Start_position + i].CopyFrom(sheet.Rows[Start_position + i + 1]);
                        addrow_cnt++;
                    }

                    sheet.Cells["AQ" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["SEQ"].ToString();    //순번
                    sheet.Cells["AR" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["ITEM_NM"].ToString();    //품명
                    sheet.Cells["AV" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["COMP_NAME"].ToString();    //공급업체명
                    sheet.Cells["AZ" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["PRICE"].ToString());    //단가
                    sheet.Cells["BB" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["AMOUNT"].ToString());    //수량
                    sheet.Cells["BD" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["UNIT"].ToString();    //단위
                    sheet.Cells["BF" + (Start_position + i)].Value = double.Parse(DT_GRD01.Tables[0].Rows[i]["TOTAL"].ToString());    //소계
                    sheet.Cells["BI" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["CLASS_NM"].ToString();    //대계정명
                    sheet.Cells["BL" + (Start_position + i)].Value = DT_GRD01.Tables[0].Rows[i]["ACT_NM"].ToString();    //소계정명

                }


                sheet.Cells["AQ" + (28 + addrow_cnt)].Value = "o 공급업체명: " + DT_GRD01.Tables[0].Rows[0]["COMP_NAME"].ToString();   //공급업체명
                sheet.Cells["AQ" + (29 + addrow_cnt)].Value = "o 계좌번호 : " + txt_COMP_ACCT.Text + " / " + txt_COMP_BANK.Text + " / " + txt_ACCT_HOLDER.Text;   //계좌정보
                sheet.Cells["AQ" + (30 + addrow_cnt)].Value = "o 지급일자 : " + DateTime.Parse(dt_PAY.EditValue.ToString()).ToString("yyyy-MM-dd");    //지급일자
                sheet.Cells["AQ" + (31 + addrow_cnt)].Value = "o 공급업체 담당자 : " + txt_COMP_MNG.Text + " / " + txt_COMP_MNG_PHONE.Text;   //공급업체 담당자
                sheet.Cells["AQ" + (35 + addrow_cnt)].Value = txt_DCMNT1_NM.Text;   //첨부파일1
                if (txt_DCMNT2_NM.Text != "")
                {
                    sheet.Rows.Insert(35 + addrow_cnt);
                    sheet.Rows[35 + addrow_cnt].CopyFrom(sheet.Rows[35 + addrow_cnt + 1]);
                    sheet.Cells["AQ" + (35 + addrow_cnt + 1)].Value = txt_DCMNT2_NM.Text;   //첨부파일2
                }
                if (txt_DCMNT3_NM.Text != "")
                {
                    sheet.Rows.Insert(36 + addrow_cnt);
                    sheet.Rows[36 + addrow_cnt].CopyFrom(sheet.Rows[36 + addrow_cnt + 1]);
                    sheet.Cells["AQ" + (36 + addrow_cnt + 1)].Value = txt_DCMNT3_NM.Text;   //첨부파일3
                }

                sheet.Cells["BF" + (31 + addrow_cnt)].Value = drowup;
                sheet.Cells["BS" + (31 + addrow_cnt)].Value = use;

                //FileStream stream = new FileStream()
                //this.spreadsheetControl1.SaveDocument("123", DocumentFormat.Xlsx);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btn_DCMNT1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.DefaultExt = "jpg";
            openFile.Filter = "Image Files(All files (*.*)|*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] dcmnt = new byte[fs.Length]; 
                    fs.Read(dcmnt, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    this.txt_DCMNT1.Text = filename;
                    this.txt_DCMNT1.Tag = dcmnt;
                }
            }
        }

        private void btn_DCMNT2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.DefaultExt = "jpg";
            openFile.Filter = "Image Files(All files (*.*)|*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] dcmnt = new byte[fs.Length];
                    fs.Read(dcmnt, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    this.txt_DCMNT2.Text = filename;
                    this.txt_DCMNT2.Tag = dcmnt;
                }
            }
        }

        private void btn_DCMNT3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.DefaultExt = "jpg";
            openFile.Filter = "Image Files(All files (*.*)|*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|*.*";
            openFile.ShowDialog();
            if (openFile.FileNames.Length > 0)
            {
                foreach (string filename in openFile.FileNames)
                {
                    FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
                    byte[] dcmnt = new byte[fs.Length];
                    fs.Read(dcmnt, 0, System.Convert.ToInt32(fs.Length));
                    fs.Close();
                    this.txt_DCMNT3.Text = filename;
                    this.txt_DCMNT3.Tag = dcmnt;
                }
            }
        }

        private void txt_DEPT_EditValueChanged(object sender, EventArgs e)
        {
            gParam = new string[] { ((DateTime)dt_PLAN.EditValue).Year.ToString(), ((DateTime)dt_PLAN.EditValue).ToString("MM"), bedt_DEPT.Tag.ToString() };
            DataSet ds = df_select(6, gParam, out error_msg);
            if(ds != null)
            {
                if(ds.Tables[0].Rows.Count >0)
                {
                    drowup = double.Parse(ds.Tables[0].Rows[0][0].ToString());
                    txt_DROWUP.Text = double.Parse(ds.Tables[0].Rows[0][0].ToString()).ToString("#,##0");
                }
                else
                {

                }
                if(ds.Tables[1].Rows.Count > 0)
                {
                    use = double.Parse(ds.Tables[1].Rows[0][0].ToString());
                    txt_USE.Text = double.Parse(ds.Tables[1].Rows[0][0].ToString()).ToString("#,##0");
                }
                textEdit1.Text = double.Parse((drowup - use).ToString()).ToString("#,##0");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        private void bedt_DEPT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "부서");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_DEPT.Tag = frm.CODE;
                bedt_DEPT.Text = frm.NAME;
            }
        }

        private void bedt_PLAN_USER_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, "사원", "%");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_PLAN_USER.Tag = frm.CODE;
                bedt_PLAN_USER.Text = frm.NAME;
            }
        }
    }
}
