using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DH_Core;

namespace BASE
{
    public partial class BASE01 : frmSub_Baseform_Search_STD
    {
        public BASE01()
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
        private void USER_Load(object sender, EventArgs e)
        {
            AutoritySet();

               env = new _Environment();
            string error_msg = string.Empty;

            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("NAME");

            //통제유무
            dt.Rows.Add("%", "전체");
            dt.Rows.Add("Y", "사용");
            dt.Rows.Add("N", "미사용"); 
            if (dt != null && dt.Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_CTRL_YN, dt, "NAME", "CODE");
                ledt_CTRL_YN.ItemIndex = 0;
            }


            //대계정 lookup
            DataSet ds;
            ds = df_select(1, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_act_sort, ds.Tables[0], "NAME", "CODE");
                ledt_act_sort.ItemIndex = 0;
            }

            ds = df_select(4, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(rledt_CLASS, ds.Tables[0], "CODE", "NAME", "CODE", "NAME");
            }


            ////계정분류 lookup
            //dt = df_select( 3, null, out error_msg);
            //if(dt != null && dt.Rows.Count > 0)
            //{
            //    modUTIL.DevLookUpEditorSet(ledt_act_sort, dt, "NAME", "CODE");
            //    ledt_act_sort.ItemIndex = 0;
            //}

            getData();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                gridControl1.DataSource = null;
                DT_GRD01 = df_select(0, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("회계 계정 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl1.DataSource = DT_GRD01.Tables[0];
                DT_GRD02 = DT_GRD01;
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
                case 0: //회계 계정 조회
                    {
                        string query = "SELECT ACT_CD, ACT_NM, ACT_GRP_NM, BUDGET_NM, CTRL_YN, CLASS FROM TB_ACCOUNT WITH(NOLOCK) WHERE ACT_CD LIKE '%' + @ACT_CD + '%' AND ACT_NM LIKE '%' + @ACT_NM + '%' AND ISNULL(CTRL_YN,'N') LIKE @CTRL_YN  ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", txt_act_cd.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_NM", txt_act_nm.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@CTRL_YN", ledt_CTRL_YN.EditValue.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //사원구분 조회
                    {
                        string query = "SELECT '%' CODE, '전체' NAME UNION ALL SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정' ";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 2: //그리드
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_USER";
                        gConst.DbConn.AddParameter(new SqlParameter("@USER", txt_act_cd.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@NAME", txt_act_nm.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", ledt_act_sort.EditValue.ToString() ));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 3: //부서조회
                    {
                        string query = "SELECT '%' CODE, '전체' NAME UNION ALL  SELECT DEPT AS CODE, DEPT_NAME AS NAME FROM TS_DEPT";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 4: //사원구분 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정' ";
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
                string query = "UPDATE TB_ACCOUNT SET BUDGET_NM = @BUDGET_NM, CTRL_YN = @CTRL_YN, MODIFY_DT = GETDATE(), MODIFY_ID = @USR WHERE ACT_CD = @ACT_CD ";
                gConst.DbConn.AddParameter(new SqlParameter("@BUDGET_NM", dr["BUDGET_NM"]));
                gConst.DbConn.AddParameter(new SqlParameter("@CTRL_YN", dr["CTRL_YN"]));
                gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", dr["ACT_CD"]));
                gConst.DbConn.AddParameter(new SqlParameter("@USR", env.EmpCode));
                result = gConst.DbConn.ExecuteSQLQuery(query, out error_msg);

                gConst.DbConn.ClearDB();
            }
            catch
            {
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
                gConst.DbConn.Rollback();
            }

            gConst.DbConn.Commit();
            return result;
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;
            if (DT_GRD01.HasChanges())
            {
                ds_new = DT_GRD01.GetChanges();
                foreach (DataRow dr in ds_new.Tables[0].Rows)
                {
                    gParam = new string[] { "U" };
                    df_Transaction(20, gParam, dr, out gOut_MSG);
                }
                MsgBox.MsgInformation("저장 완료", "확인");
                btn_Search_Click(null, null);
                return;
            }
        }
    }
}
