using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DH_Core.CommonPopup;

namespace DH_Core
{
    public partial class frmSub_Baseform_Login : DevExpress.XtraEditors.XtraForm
    {
        public frmSub_Baseform_Login()
        {
            InitializeComponent();
            this.Name = "사용자 로그인";
            env = new _Environment();
        }

        _Environment env;

        #region Events (버튼클릭을 제외한 이벤트처리)
        ////////////////////////////////////////

        /// <summary>
        /// 회사명을 변경했을때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbo_Company_SelectedIndexChanged(object sender, EventArgs e)
        {
           // cbo_Factory.BackColor = Color.Gold;
        }

        /// <summary>
        /// 사용자계정을 변경했을때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_ID_EditValueChanged(object sender, EventArgs e)
        {
            txt_ID.BackColor = Color.White;// Color.Gold;
        }

        /// <summary>
        /// 비밀번호를 변경했을때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Password_EditValueChanged(object sender, EventArgs e)
        {
            txt_Password.BackColor = Color.White;// Color.Gold;
        }

        private void btn_PASSWORD_CHANGE_Click(object sender, EventArgs e)
        {
            //frmPASSWORD_CHG frm = new frmPASSWORD_CHG(env, txt_ID.Text);
            //frm.ShowDialog();
        }
        ///////////////////////////////////////////
        #endregion Events (버튼클릭을 제외한 이벤트처리)


    }
}