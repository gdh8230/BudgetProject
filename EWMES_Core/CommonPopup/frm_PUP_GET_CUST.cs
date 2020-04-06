using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_CUST : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_CUST()
        {
            InitializeComponent();
        }
        public frm_PUP_GET_CUST(_Environment env, string CUST, string CUST_NAME)
        {
            InitializeComponent();
            this.env = env;
            txtCust.EditValue = CUST;
            txtCustName.EditValue = CUST_NAME;
        }

        #region Attributes (속성정의 집합)
        _Environment env;

        public string CUST { get; set; }
        public string CUST_NAME { get; set; }
        #endregion
        #region Functions (속성정의 집합)
        private void Init_SCR()
        {
            getData();
        }

        private void getData()
        {
            this.Cursor = Cursors.WaitCursor;
            string error_msg = string.Empty;
            gcCust.DataSource = null;
            gcCust.DataSource = df_select(0, null, out error_msg);
            this.Cursor = Cursors.Default;
        }
        private void selectItem(DataRow dr)
        {
            this.CUST = dr["CUST"].ToString();
            this.CUST_NAME = dr["CUST_NAME"].ToString();
            this.DialogResult = DialogResult.OK;
        }
        public int getDataCount()
        {
            if (this.gcCust.DataSource == null)
                return 0;
            return ((DataTable)this.gcCust.DataSource).Rows.Count;
        }
        public void postItem(int idx)
        {
            DataRow dr = ((DataTable)this.gcCust.DataSource).Rows[idx];
            if (dr != null)
            {
                this.CUST = dr["CUST"].ToString();
                this.CUST_NAME = dr["CUST_NAME"].ToString();
            }
        }
        #endregion

        #region Events
        private void Form_Load(object sender, EventArgs e)
        {
            gvCust.Focus();
            getData();
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gvCust.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gvCust.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                DataRow dr = gvCust.GetFocusedDataRow();
                selectItem(dr);
            }
        }
        private void mKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                getData();
            }
        }
        private void selectAllClick(object sender, EventArgs e)
        {
            try
            {
                if (sender == txtCust)
                {
                    txtCust.SelectAll();
                }
                if (sender == txtCustName)
                {
                    txtCustName.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr(ex.Message, "");
                return;
            }
        }

        #endregion
        #region DB CRUD(데이터베이스 처리)
        private DataTable df_select(int Index, object[] Param, out string error_msg)
        {
            DataTable dt = null;
            gConst.DbConn_MES.DBClose();
            string query = string.Empty;
            error_msg = "";
            switch (Index)
            {
                case 0:
                    {
                        query = "SELECT CUST,CUST_NAME FROM TB_CUST WITH(NOLOCK) WHERE CUST LIKE '%'+@CUST+'%' AND CUST_NAME LIKE '%'+@CUST_NAME+'%' AND COMP = @COMP AND ISNULL(STAT,'') <> 'D'";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@CUST", txtCust.Text.Trim()));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@CUST_NAME", txtCustName.Text.Trim()));
                        dt = gConst.DbConn_MES.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn_MES.DBClose();
            return dt;
        }
        #endregion

    }
}
