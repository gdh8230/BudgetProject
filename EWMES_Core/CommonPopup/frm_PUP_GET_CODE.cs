using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_CODE : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_CODE(_Environment env, string title)
        {
            InitializeComponent();
            this.env = env;

            this.dept = env.Dept;
            this.Text = title + " 정보";
            this.title = title;
            getData();
        }
        public frm_PUP_GET_CODE(_Environment env, string title, string dept)
        {
            InitializeComponent();
            this.env = env;

            this.dept = dept;
            this.Text = title + " 정보";
            this.title = title;
            getData();
        }

        #region Attributes (속성정의 집합)
        _Environment env;

        public string CODE { get; set; }
        public string NAME { get; set; }
        public string CLASS { get; set; }
        public string CLASS_NM { get; set; }
        string title;
        string dept;
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
            gridControl1.DataSource = null;
            gridControl1.DataSource = df_select(null,out error_msg);
            this.Cursor = Cursors.Default;
        }
        private void selectItem(DataRow dr)
        {
            this.CODE = dr["CODE"].ToString();
            this.NAME = dr["NAME"].ToString();
            if (title.Equals("계정"))
            {
                this.CLASS = dr["CLASS"].ToString();
                this.CLASS_NM = dr["CLASS_NM"].ToString();
            }
            this.DialogResult = DialogResult.OK;
        }
        public int getDataCount()
        {
            if (this.gridControl1.DataSource == null)
                return 0;
            return ((DataTable)this.gridControl1.DataSource).Rows.Count;
        }
        public void postItem(int idx)
        {
            DataRow dr = ((DataTable)this.gridControl1.DataSource).Rows[idx];
            if (dr != null)
            {
                this.CODE = dr["CODE"].ToString();
                this.NAME = dr["NAME"].ToString();
                if (title.Equals("계정"))
                {
                    this.CLASS = dr["CLASS"].ToString();
                    this.CLASS_NM = dr["CLASS_NM"].ToString();
                }
            }
        }
        #endregion

        #region Events
        private void Form_Load(object sender, EventArgs e)
        {
            gridView1.Focus();
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }
        private void mKeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (sender == txt_code)
                    getData();
                else if (sender == txt_name)
                {
                    getData();
                }
                else if (sender == gridView1)
                {
                    if (gridControl1.DataSource == null || gridView1.RowCount == 0)
                        return;
                    DataRow dr = gridView1.GetFocusedDataRow();
                    if (dr == null)
                        return;
                    selectItem(dr);
                }
            }
        }
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gridView1.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridView1.CalcHitInfo(pt);
            if (info.InRow || info.InRowCell)
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                selectItem(dr);
            }
        }
        #endregion
        #region DB CRUD(데이터베이스 처리)
        private DataTable df_select(object[] Param, out string error_msg)
        {
            DataTable dt = null;
            gConst.DbConn.DBClose();
            string query = string.Empty;
            error_msg = "";
            switch (title)
            {
                case "프로젝트":
                    {
                        query = "SELECT PJT_CD AS CODE, PJT_NM AS NAME FROM TB_PJT WITH(NOLOCK) WHERE PJT_CD LIKE @CODE+'%' AND PJT_NM LIKE @NAME+'%'";
                    }
                    break;
                case "부서":
                    {
                        query = "SELECT DEPT AS CODE, DEPT_NAME AS NAME FROM TS_DEPT WITH(NOLOCK) WHERE DEPT LIKE @CODE+'%' AND DEPT_NAME LIKE @NAME+'%'";
                    }
                    break;
                case "계정":
                    {
                        query = "SELECT ACT_CD AS CODE, ACT_NM AS NAME, CLASS, B.NAME AS CLASS_NM FROM TB_ACCOUNT A WITH(NOLOCK) " +
                                "JOIN TS_CODE B WITH(NOLOCK) " +
                                "ON A.CLASS = B.CODE " +
                                "AND B.C_ID = '대계정' " +
                                "WHERE ACT_CD LIKE @CODE+'%' AND ACT_NM LIKE @NAME+'%'";
                    }
                    break;
                case "사원":
                    {
                        query = "SELECT USR AS CODE, UNAM AS NAME FROM TS_USER A WITH(NOLOCK) " +
                                "WHERE USR LIKE @CODE+'%' AND UNAM LIKE @NAME+'%' AND DEPT LIKE '" + dept + "' ";
                    }
                    break;
                default: break;
            }
            gConst.DbConn.AddParameter(new SqlParameter("@CODE", txt_code.Text.Trim()));
            gConst.DbConn.AddParameter(new SqlParameter("@NAME", txt_name.Text.Trim()));
            dt = gConst.DbConn.GetDataTableQuery(query, out error_msg);
            gConst.DbConn.DBClose();
            return dt;
        }
        #endregion

        private void btn_Add_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            selectItem(dr);
        }
    }
}
