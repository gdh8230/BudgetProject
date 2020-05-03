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
                case 1: //부서별
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '환종'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 2: //계정별
                    {
                        string query = string.Empty;
                        if (Param[0].Equals("0")) query += "SELECT '%' CODE, '전체' NAME UNION ALL "; // param[0] 값이 0일경우 전체포함 1일경우 전체미포함
                        query += "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 4: //프로젝트별
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

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
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
