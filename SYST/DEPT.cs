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
    public partial class DEPT : frmSub_Baseform_Search_STD
    {
        public DEPT()
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

            getData();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                gridControl1.DataSource = null;
                string[] param = new string[] { txt_dept_cd.Text.Equals("") ? "%":txt_dept_cd.Text
                                                ,txt_dept_nm.Text.Equals("") ? "%":txt_dept_nm.Text
                                                };
                DataTable dt = df_select(0, param, out error_msg);
                if (dt == null)
                {
                    MsgBox.MsgErr("부서 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
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
                case 0: //부서조회
                    {
                        string query = "SELECT DEPT, DEPT_NAME, SECT_CD, SECT_NAME FROM TS_DEPT WITH(NOLOCK) WHERE DEPT LIKE '"+ Param[0] +"' AND DEPT_NAME LIKE '"+ Param[1] + "' AND COMP = @COMP AND FACT = @FACT";
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
    }
}
