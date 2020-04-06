using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_EMP : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_EMP()
        {
            InitializeComponent();
        }
        public frm_PUP_GET_EMP(string EMP_CD)
        {
            InitializeComponent();
            txt_emp_cd.EditValue = EMP_CD;
        }
        public frm_PUP_GET_EMP(string EMP_CD, string EMP_NAME)
        {
            InitializeComponent();
            txt_emp_cd.EditValue = EMP_CD;
            txt_emp_name.EditValue = EMP_NAME;
        }

        #region Attributes (속성정의 집합)
        _Environment env;

        public string EMP_CD { get; set; }
        public string EMP_NAME { get; set; }
        #endregion

        #region Functions 
        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            getData();
        }
        
        private void getData()
        {
            this.Cursor = Cursors.WaitCursor;
            string error_msg = string.Empty;
            gridControl_emp.DataSource = null;
            DataTable dt = df_select(0,null,out error_msg);
            if (dt == null)
            {
                MsgBox.MsgErr("사원 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                this.Cursor = Cursors.Default;
                return;
            }
            gridControl_emp.DataSource = dt;
            this.Cursor = Cursors.Default;
        }
        private void selectItem(DataRow dr)
        {
            this.EMP_CD = dr["EMP_CD"].ToString();
            this.EMP_NAME = dr["EMP_NAME"].ToString();
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region Events
        private void Form_Load(object sender, EventArgs e)
        {
            //환경값 로딩
            env = new _Environment();
            //화면초기회
            Init_SCR();
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                getData();
        }
        private void btn_Check_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView_emp.GetFocusedDataRow();
            if (dr == null)
                return;
            selectItem(dr);
        }
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gridView_emp.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridView_emp.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                DataRow dr = gridView_emp.GetFocusedDataRow();
                selectItem(dr);
            }
        }
        private void selectAllClick(object sender, EventArgs e)
        {
            try
            {
                if (sender == txt_emp_cd)
                {
                    txt_emp_cd.SelectAll();
                }
                if (sender == txt_emp_name)
                {
                    txt_emp_name.SelectAll();
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
            gConst.DbConn.DBClose();
            string query = string.Empty;
            error_msg = "";
            switch (Index)
            {
                case 0: //사원조회
                    {
                        query = "SELECT EMP_CD, EMP_NAME FROM TB_EMPLOYEE WHERE COMP = @COMP AND FACT = @FACT AND EMP_NAME LIKE @EMP_NAME +'%' AND USEYN = 1";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@EMP_CD", txt_emp_cd.Text.Trim()));
                        gConst.DbConn.AddParameter(new SqlParameter("@EMP_NAME", txt_emp_name.Text.Trim()));
                        dt = gConst.DbConn.GetDataTableQuery(query,out error_msg);
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
