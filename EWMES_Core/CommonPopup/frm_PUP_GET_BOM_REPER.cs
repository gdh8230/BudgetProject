using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_BOM_REPER : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_BOM_REPER()
        {
            InitializeComponent();
        }
        public frm_PUP_GET_BOM_REPER(string ITEM)
        {
            InitializeComponent();
            this.P_ITEM = ITEM;
        }

        #region Attributes (속성정의 집합)
        _Environment env;
        private bool AUTO_SEARCH = false;
        public string ITEM { get; set; }
        public string ITEM_NAME { get; set; }
        public string ITEM_UNIT { get; set; }
        public string ITEM_SPEC { get; set; }
        public string LOT_FG { get; set; }
        public string ACCT_NM { get; set; }
        public string DWR_VER { get; set; }
        
        public DataRow SelectRow { get; set; }

        string P_ITEM;

        private string mItem_acct = string.Empty;
        #endregion

        #region Functions 
        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            getData();
            if (this.AUTO_SEARCH)
                getData();
        }

        private void getData()
        {
            this.Cursor = Cursors.WaitCursor;
            string error_msg = string.Empty;
            gridControl_item.DataSource = null;

            try
            {
                DataTable dt = df_select(2, null, out error_msg);
                if (dt == null)
                {
                    MsgBox.MsgErr("품목 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }
                gridControl_item.DataSource = dt;
                gridView_item.Focus();
            }
            catch (Exception ex)
            {

            }
            this.Cursor = Cursors.Default;
        }
        private void selectItem(DataRow dr)
        {
            this.ITEM = dr["ITEM"].ToString();
            this.ITEM_NAME = dr["ITEM_NAME"].ToString();
            this.ITEM_SPEC = dr["ITEM_SPEC"].ToString();
            this.ITEM_UNIT = dr["ITEM_UNIT"].ToString();
            this.SelectRow = dr;
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
        /// <summary>
        /// 엑셀파일로 전환
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Excel_Click(object sender, EventArgs e)
        {
            DH_Core.DXfrmWMES_Baseform.Excel_Print((DataTable)gridControl_item.DataSource,
                                                        gridView_item,
                                                        this.Text + "_" + DateTime.Now.ToShortDateString().Replace("-", "").Replace("/", ""));
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                getData();
        }
        private void btn_Check_Click(object sender, EventArgs e)
        {

            DataRow dr = gridView_item.GetFocusedDataRow();
            if(dr != null)
                selectItem(dr);

        }
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            Point pt = gridView_item.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = gridView_item.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                DataRow dr = gridView_item.GetFocusedDataRow();
                selectItem(dr);
            }
        }
        private void gridView_item_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataRow dr = gridView_item.GetFocusedDataRow();
            if(dr != null)
                selectItem(dr);
        }
        #endregion
        #region DB CRUD(데이터베이스 처리)
        private DataTable df_select(int Index, object[] Param, out string error_msg)
        {
            DataTable dt = null;
            gConst.DbConn_MES.DBClose();
            error_msg = "";
            switch (Index)
            {
                case 0: //계정구분
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WHERE C_ID = 'ACCT_ITEM' ";
                        dt = gConst.DbConn_MES.GetDataTableQuery(query, out error_msg);
                    }
                    break;
                case 2: //
                    {
                        //지시 소요자재정보로 변경
                        gConst.DbConn_MES.ProcedureName = "USP_PUP_GET_WO_REQ_WF";
                        //gConst.DbConn_MES.ProcedureName = "USP_PUP_GET_BOM_REPER_ITEM";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@P_ITEM", P_ITEM));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@ITEM", txt_item.EditValue));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@ITEM_NAME", txt_item_name.EditValue));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@ITEM_SPEC", txtItemSpec.EditValue));
                        dt = gConst.DbConn_MES.GetDataTableQuery(out error_msg);
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
