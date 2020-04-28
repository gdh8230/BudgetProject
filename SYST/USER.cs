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

namespace SYST
{
    public partial class USER : frmSub_Baseform_Search_STD
    {
        public USER()
        {
            InitializeComponent();
        }

        #region Attributes (속성정의 집합)
        _Environment env;
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
                    btn_pwd_reset.Enabled = DR[4].Equals("Y") ? true : false;
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

            DataTable dt;

            //부서 lookup
            dt = df_select( 3, null, out error_msg);
            if(dt != null && dt.Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_dept, dt, "NAME", "CODE");
                ledt_dept.ItemIndex = 0;
            }

            getData();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                gridControl1.DataSource = null;
                DataTable dt = df_select(2, null, out error_msg);
                if (dt == null)
                {
                    MsgBox.MsgErr("사원 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl1.DataSource = dt;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }
        #endregion


        #region DB CRUD(데이터베이스 처리)
        private DataTable df_select(int Index, object[] Param, out string error_msg)
        {
            DataTable dt = null;
            gConst.DbConn.DBClose();
            error_msg = "";
            switch (Index)
            {
                case 1: //사원구분 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = 'EMP_TP' ";
                        dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                case 2: //그리드
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_USER";
                        gConst.DbConn.AddParameter(new SqlParameter("@USER", txt_emp_cd.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@NAME", txt_emp_nm.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", ledt_dept.EditValue.ToString() ));
                        dt = gConst.DbConn.GetDataTableQuery(out error_msg);
                    }
                    break;
                case 3: //부서조회
                    {
                        string query = "SELECT '%' CODE, '전체' NAME UNION ALL  SELECT DEPT AS CODE, DEPT_NAME AS NAME FROM TS_DEPT";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
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
                gConst.DbConn.ProcedureName = "USP_SYS_SET_PWD_RESET";
                gConst.DbConn.AddParameter(new SqlParameter("@EMP_CD", dr["EMP_CD"]));
                gConst.DbConn.AddParameter(new SqlParameter("@PWD", "1"));
                result = gConst.DbConn.ExecuteNonQuery(out error_msg);

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

        private void btn_pwd_reset_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string error_msg = string.Empty;

            if(df_Transaction(0, null, dr, out error_msg))
            {
                MsgBox.MsgInformation("사용자 "+dr["EMP_CD"].ToString()+"의  비밀번호가 초기화 되었습니다.", "확인");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            string error_msg = string.Empty;

            if (df_Transaction(0, null, dr, out error_msg))
            {
                MsgBox.MsgInformation("사용자 " + dr["EMP_CD"].ToString() + "의  비밀번호가 변경 되었습니다.", "확인");
            }
        }
    }
}
