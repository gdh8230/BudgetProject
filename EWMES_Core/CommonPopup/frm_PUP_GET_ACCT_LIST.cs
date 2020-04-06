using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DH_Core;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors;

namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_GET_ACCT_LIST : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_GET_ACCT_LIST(_Environment env, DataTable ACCTS_LIST)
        {
            InitializeComponent();
            this.env = env;
            this.ACCT_LIST = ACCTS_LIST;
        }
        public frm_PUP_GET_ACCT_LIST(_Environment env, string ACCT_CODE)
        {
            InitializeComponent();
            this.env = env;
            this.ACCT_CODE = ACCT_CODE;
        }
        #region Attributes (속성정의 집합)
        _Environment env;
        public DataTable ACCT_LIST;
        public string ACCT_CODE;

        #endregion

        #region Functions 
        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            string error_msg = string.Empty;
            gridControl_ACCT.DataSource = ACCT_LIST;
        }
        
        #endregion

        #region Events
        private void Form_Load(object sender, EventArgs e)
        {
            //환경값 로딩
            //env = new _Environment();
            //화면초기화
            Init_SCR();
        }
        #endregion
        #region DB CRUD(데이터베이스 처리)
        #endregion

    }
} 