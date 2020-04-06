/******************************************************************
 * 제    목 : BottonEdit UserControl
 * 작 성 일 : 2018-01-32
 * 작 성 자 : 공동현 (JnKc)
 * 개    요 : DevExpress의 BottonEdit 사용하여 창고/공정 설정
 *            속성값을 선택하여 사용할 쉽게 선택 가능
 * 반 환 값 : GetItem      (string) - CODE 반환
 *            GetItemName  (string) - NAME 반환
 * 이 벤 트 : ButtonClick           - Bedt 버튼을 클릭하면 발생
 * 수정내역 :
*******************************************************************/
using DH_Core;
using DH_Core.CommonPopup;
using MST;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class Jnkc_Bedt_Control : UserControl
    {
        public Jnkc_Bedt_Control()
        {
            InitializeComponent();
        }


        _Environment env = new _Environment();
        public string GetCode { get { return bedt_ITEM.EditValue.ToString().Equals("") ? "%" : bedt_ITEM.EditValue.ToString(); } }            // ITEM 반환
        public string GetName { get { return txt_ITEM_NAME.EditValue.ToString(); } }    // ITEM_NAME 반환
        public string SetCode { set { bedt_ITEM.EditValue = value; } }                      // ITEM 설정
        public string SetName { set { txt_ITEM_NAME.EditValue = value; } }              // ITEM_NAME 설정

        //속성값 설정
        public enum 구분1 { 품목, 거래처 }; // 여기에 추가하면 디자인 속성에 선택할 수 있는 구분값이 추가됨
        private 구분1 _구분1;

        [Category("구분1"), Description("Botton을 눌러 띄울 팝업을 선택")]
        public 구분1 GBN1 { get { return _구분1; } set { _구분1 = value; set_Text(_구분1);} }
        
        //선택한 속성값에 따라 lbl_text변경. 구분 추가시 추가 필요
        private void set_Text(구분1 GBN)
        {
            switch(GBN)
            {
                case 구분1.품목:
                    {
                        lbl_Ledit01.Text = "품      목";
                        break;
                    }
                case 구분1.거래처:
                    {
                        lbl_Ledit01.Text = "거  래  처";
                        break;
                    }
            }
        }


        //선택한 속성값에 따라 버튼 클릭시 팝업
        private void bedt_ITEM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                switch (_구분1)
                {
                    case 구분1.품목:
                        {
                            #region 품목정보 조회
                            //frm_PUP_GET_ITEM_ACCT _frm = new frm_PUP_GET_ITEM_ACCT(bedt_ITEM.Text);
                            //DialogResult ret = _frm.ShowDialog();
                            //if (ret == DialogResult.OK)
                            //{
                            //    if (_frm.ITEM.Equals("")) return;
                            //    bedt_ITEM.Text = _frm.ITEM;
                            //    txt_ITEM_NAME.Text = _frm.ITEM_NAME;
                            //}
                            //else
                            //{
                            //    return;
                            //}
                            #endregion
                        }
                        break;
                    case 구분1.거래처:
                        {
                            #region 거래처조회
                            //frm_PUP_GET_CUST _frm = new frm_PUP_GET_CUST(env, null, null);
                            //DialogResult ret = _frm.ShowDialog();
                            //if (ret == DialogResult.OK)
                            //{
                            //    if (_frm.CUST.Equals("")) return;
                            //    //txt_ITEM_NAME.Text = _frm.CUST_NAME;
                            //    bedt_ITEM.Text = _frm.CUST_NAME;
                            //}
                            //else
                            //{
                            //    return;
                            //}
                            #endregion
                        }
                        break;
                }
            }
            catch (Exception ee)
            {
            }
        }

        private void bedt_ITEM_EditValueChanged(object sender, EventArgs e)
        {
            txt_ITEM_NAME.Text = "";
        }
    }
}
