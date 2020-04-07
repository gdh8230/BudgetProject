using DH_Core;
using DH_Core.DB;
using Main;
using System;
using System.Data;
using System.Windows.Forms;

namespace Main1
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        _Environment env;
        MSSQLAgent DBCon = new MSSQLAgent();
        private string dept = null;
        private void Login_Load(object sender, EventArgs e)
        {
            env = new _Environment();

            MSSQLAgent.ConStr = "server=" + env.Server + ";database=" + env.Database + ";uid=" + env.Dbid
                + ";pwd=" + env.Password + ";";

            DBConnect("MES");
            bedt_ID.EditValue = env.EmpCode;
            txt_NAME.Tag = env.EmpName;
        }

        private bool DBConnect(string DB)
        {
            bool Ret = false;
            string error_msg = string.Empty;
            try
            {
                switch (DB)
                {
                    case "MES":
                        {
                            gConst.DbConn = DBCon;
                            if (gConst.DbConn.DBConnect(out error_msg))
                            {
                                //bar_Message.Caption = " Database와 연결되었습니다.";
                                //bar_DB_Status.Caption = "DB연결됨";
                                //bar_DB_Status.ImageIndex = 5;
                                Ret = true;
                            }
                            else
                            {
                                MessageBox.Show(DB + " DB 연결에 실패하였습니다. \n인터넷 연결을 확인하십시오.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //bar_Message.Caption = "Database와 연결이 끊어졌습니다.";
                                //bar_DB_Status.Caption = "DB끊어짐";
                                //bar_DB_Status.ImageIndex = 6;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Ret;
        }

        private void btn_LOGIN_Click(object sender, EventArgs e)
        {
            //입력된 로그인 정보 체크
            try
            {
                string error_msg = string.Empty;
                DataTable dt = df_Select(11, null, out error_msg);
                if (dt != null)
                {
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        MsgBox.MsgErr("비밀번호가 틀렸습니다", "확인");
                        return;
                    }
                }
                env.EmpCode = bedt_ID.EditValue.ToString();
                env.EmpName = txt_NAME.Text;
                env.Dept = dept;
                env.Save();
                this.Hide();
                frmMain frm = new frmMain();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr(ex.Message, "");
                Application.Exit();
                return;
            }
        }

        private void bedt_ID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btn_CONFIG_Click(object sender, EventArgs e)
        {

        }

        private void btn_EXIT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txt_ID_Leave(object sender, EventArgs e)
        {
            try
            {
                string error_msg = string.Empty;
                DataTable dt = df_Select(4, new string[] { bedt_ID.EditValue.ToString() }, out error_msg);
                if (dt != null && dt.Rows.Count > 0)
                {
                    txt_NAME.Text = dt.Rows[0]["UNAM"].ToString();
                    dept = dt.Rows[0]["DEPT"].ToString();
                }
                else
                {
                    bedt_ID.Text = "";
                    txt_NAME.Text = "";
                }
            }
            catch(Exception ee)
            {

            }
        }


        /// <summary>
        /// DB Select
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        private DataTable df_Select(int Index, object[] Param, out string error_msg)
        {
            error_msg = string.Empty;
            DataTable dt = null;
            gConst.DbConn.ClearDB();
            switch (Index)
            {
                case 1: //회사명을 조회
                    {
                        string query = " SELECT COMP CODE, COMP_NAME NAME " +
                                        " FROM TB_COMP WITH(NOLOCK) ";
                        dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                case 2://사업장을 조회
                    {
                        string query = " SELECT FACT CODE, FACT_NAME AS NAME " +
                                        " FROM TB_FACT A WITH(NOLOCK) "
                                        + " WHERE A.COMP ='" + env.Company + "' ";
                        dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                case 4://사용자명을 조회
                    {
                        dt = gConst.DbConn.GetDataTableQuery(" SELECT    UNAM, DEPT " +
                                                                " FROM      TS_USER WITH(NOLOCK)  " +
                                                                " WHERE     COMP = '" + env.Company + "' " +
                                                                " AND       USR = '" + Param[0] + "' " +
                                                                " AND       STAT <> 'D' " +
                                                                " AND       USEYN = 1 "
                                                                , out error_msg);
                    }
                    break;
                case 10://사용자정보조회
                    {
                        dt = gConst.DbConn.GetDataTableQuery(" SELECT PSWD , UNAM, DEPT FROM TS_USER WITH(NOLOCK) " +
                                                                " WHERE COMP = '" + env.Company + "' " +
                                                                " AND   USR ='" + Param[0] + "'", out error_msg);
                    }
                    break;
                case 11://사용자로그인정보조회
                    {
                        gConst.DbConn.ProcedureName = "dbo.USP_GET_LOGIN";
                        gConst.DbConn.AddParameter("COMP", MSSQLAgent.DBFieldType.String, env.Company);
                        gConst.DbConn.AddParameter("FACT", MSSQLAgent.DBFieldType.String, env.Factory);
                        gConst.DbConn.AddParameter("USER", MSSQLAgent.DBFieldType.String, bedt_ID.EditValue.ToString());
                        gConst.DbConn.AddParameter("PSWD", MSSQLAgent.DBFieldType.String, txt_PWD.Text);
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg).Tables[1];
                    }
                    break;


            }
            gConst.DbConn.DBClose();
            return dt;
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
