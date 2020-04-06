using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_PJT_CD : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_PJT_CD(_Environment env, string pjtCd, string pjtName)
        {
            InitializeComponent();
            this.env = env;
            txtPjtCd.EditValue = pjtCd;
            txtPjtName.EditValue = pjtName;
            getData();
        }

        #region Attributes (속성정의 집합)
        _Environment env;

        public string PJT_CD { get; set; }
        public string PJT_NAME { get; set; }
        #endregion

        #region Functions 
        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            //getData();
        }
        
        private void getData()
        {
            this.Cursor = Cursors.WaitCursor;
            string error_msg = string.Empty;
            gcPjt.DataSource = null;
            gcPjt.DataSource = df_select(0,null,out error_msg);
            this.Cursor = Cursors.Default;
        }
        private void selectItem(DataRow dr)
        {
            this.PJT_CD = dr["PJT_CD"].ToString();
            this.PJT_NAME = dr["PJT_NAME"].ToString();
            this.DialogResult = DialogResult.OK;
        }
        public int getDataCount()
        {
            if (this.gcPjt.DataSource == null)
                return 0;
            return ((DataTable)this.gcPjt.DataSource).Rows.Count;
        }
        public void postItem(int idx)
        {
            DataRow dr = ((DataTable)this.gcPjt.DataSource).Rows[idx];
            if (dr != null)
            {
                this.PJT_CD = dr["PJT_CD"].ToString();
                this.PJT_NAME = dr["PJT_NAME"].ToString();
            }
        }
        #endregion

        #region Events
        private void Form_Load(object sender, EventArgs e)
        {
            gvPjt.Focus();
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }
        private void mKeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (sender == txtPjtCd)
                    getData();
                else if (sender == txtPjtName)
                {
                    getData();
                }
                else if (sender == gvPjt)
                {
                    if (gcPjt.DataSource == null || gvPjt.RowCount == 0)
                        return;
                    DataRow dr = gvPjt.GetFocusedDataRow();
                    if (dr == null)
                        return;
                    selectItem(dr);
                }
            }
        }
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gvPjt.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gvPjt.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                DataRow dr = gvPjt.GetFocusedDataRow();
                selectItem(dr);
            }
        }
        #endregion
        #region DB CRUD(데이터베이스 처리)
        private DataTable df_select(int Index, object[] Param, out string error_msg)
        {
            DataTable dt = null;
            gConst.DbConn.DBClose();
            string query = string.Empty;
            error_msg = "";
            switch (Index)
            {
                case 0: //PJT 조회 
                    {
                        query = "SELECT PRJ_CODE AS PJT_CD, PRJ_NAME AS PJT_NAME FROM SUNGJIN_ERP.DBO.TMPROJECT WITH(NOLOCK) WHERE ISNULL(PRJ_CODE,'%') LIKE @PJT_CD+'%' AND ISNULL(PRJ_NAME,'%') LIKE @PJT_NAME+'%'";
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_CD", txtPjtCd.Text.Trim()));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_NAME", txtPjtName.Text.Trim()));
                        dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return dt;
        }
        #endregion
    }
}
