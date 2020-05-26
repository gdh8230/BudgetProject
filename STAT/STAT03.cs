using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DH_Core;
using DH_Core.DB;
using DH_Core.CommonPopup;
using DevExpress.XtraEditors;
using System.IO;

namespace STAT
{
    public partial class STAT03 : frmSub_Baseform_Search_STD
    {
        public STAT03()
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

            DataSet ds;

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

            ////세부계정 lookup
            //ds = df_select(3, new string[] { "0" }, out error_msg);
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    modUTIL.DevLookUpEditorSet(ledt_ACT, ds.Tables[0], "NAME", "CODE");
            //    ledt_ACT.ItemIndex = 0;
            //}
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
                case 0: //예산 사용 내역 조회
                    {
                        string query = string.Empty;
                        query += "SELECT A.ADMIN_NO, B.CLASS, C.NAME as CLASS_NM, B.ACT_CD, D.BUDGET_NM as ACT_NM, ";
                        query += "		dbo.f_get_STR2DATE(PLAN_DT, '-') as PLAN_DT, dbo.f_get_STR2DATE(BILL_DT, '-') as BILL_DT, dbo.f_get_STR2DATE(PAY_DT, '-') as PAY_DT, PLAN_TITLE, ITEM_NM, TOTAL, ";
                        query += "		A.DEPT_NAME as DEPT_NM, E.SECT_NAME, PJT_NM, B.COMP_NAME ";
                        query += "FROM SPND_RSLT_H A WiTH(NOLOCK) ";
                        query += "JOIN SPND_RSLT_D B WITH(NOLOCK) ON	A.ADMIN_NO = B.ADMIN_NO ";
                        query += "JOIN TS_CODE C WITH(NOLOCK) ON	B.CLASS = C.CODE AND	C.C_ID = '대계정' ";
                        query += "JOIN TB_ACCOUNT D WITH(NOLOCK) ON	B.ACT_CD = D.ACT_CD ";
                        query += "JOIN TS_DEPT E WITH(NOLOCK) ON	A.DEPT = E.DEPT ";
                        query += "LEFT JOIN TB_PJT F WITH(NOLOCK) ON	A.PJT_CD = F.PJT_CD ";
                        query += "WHERE (PLAN_DT BETWEEN '" + DatePicker1.GetStartDate.ToString("yyyyMMdd") + "' AND '" + DatePicker1.GetEndDate.ToString("yyyyMMdd") + "') AND A.DEPT like '" + bedt_DEPT.Tag.ToString() + "' + '%' AND PLAN_USER LIKE '" + bedt_PLAN_USER.Tag.ToString() + "' + '%' ";
                        query += "AND B.CLASS LIKE '" + ledt_CLASS.EditValue + "' AND B.ACT_CD LIKE '" + ledt_ACT.EditValue + "' AND ISNULL(A.PJT_CD,'') LIKE '" + bedt_PJT.Tag.ToString() + "' AND A.STAT <>'D' AND B.STAT <> 'D'  AND ISNULL(A.[USER],'') LIKE '" + bedt_USER.Tag.ToString() + "' ";
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
                case 6: //사업구분 lookup 조회
                    {
                        string query = string.Empty;
                        if (Param[0].Equals("0")) query += "SELECT '%' CODE, '전체' NAME UNION ALL "; // param[0] 값이 0일경우 전체포함 1일경우 전체미포함
                        query += "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '사업구분'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 7:  //지출결의서 디테일
                    {
                        gConst.DbConn.ProcedureName = "USP_EXEC_GET_SPND_RSLT_D";
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_NO", Param[0]));
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
                bedt_PJT.Tag = frm.CODE;
                bedt_PJT.Text = frm.NAME;
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

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            Excel_Print((DataTable)gridControl1.DataSource,
                                           gridView1,
                                           this.Text + "_" + DateTime.Now.ToShortDateString().Replace("-", "").Replace("/", ""));
        }
    }
}
