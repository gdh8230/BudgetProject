/******************************************************************
 * 제    목 : LookUpEdit UserControl
 * 작 성 일 : 2018-01-02
 * 작 성 자 : 공동현 (JnKc)
 * 개    요 : DevExpress의 LookUpEdit 사용하여 창고/공정 설정
 *            속성값을 선택하여 사용할 쉽게 선택 가능      
 * 반 환 값 : GetLedtItem01 - 1번 LookUp Editvalue 반환
 *            GetLedtItem02 - 2번 LookUp Editvalue 반환
 * 이 벤 트 : EditValueChanged  - LookUpEdit의 값이 변경되면 발생
 * 수정내역 :
*******************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class Jnkc_Ledt_Control : UserControl
    {
        public Jnkc_Ledt_Control()
        {
            InitializeComponent();
        }


        _Environment env = new _Environment();
        CultureInfo c_info = CultureInfo.CurrentCulture;
        public string GetLedtItem01 { get { return ledt_Item01.EditValue.ToString(); } }    // 1번 Ledt 반환
        public string GetLedtItem02 { get { return ledt_Item02.EditValue.ToString(); } }    // 2번 Ledt 반환
        public string GetLedtItemText01 { get { return ledt_Item01.Text; } }                // 1번 Ledt Text 반환
        public string GetLedtItemText02 { get { return ledt_Item02.Text; } }                // 2번 Ledt Text 반환
        public string GetOperGBN { get { return rdo_Item01.EditValue.ToString(); } }    // 2번 Ledt Text 반환
        public string SetLedtItem01 { set { ledt_Item01.EditValue = value; } }              // 1번 Ledt 설정
        public string SetLedtItem02 { set { ledt_Item02.EditValue = value; } }              // 2번 Ldet 설정
        public string SetRadioItem01 { set { rdo_Item01.EditValue = value; } }              // 2번 Ldet 설정
        public string SetLabel01 { set { lbl_Ledit01.Text = value; } }              // 1번 Label 설정
        public string SetLabel02 { set { lbl_Ledit02.Text = value; } }              // 2번 Label 설정

        //속성값 설정.
        public enum All_USE { 전체포함, 전체제외 };
        private All_USE _구분1;

        [Category("구분"), Description("전체 포함/미포함 여부 선택")]
        public All_USE GBN1 { get { return _구분1; } set { _구분1 = value; } }

        public enum COUNT { 둘다, 하나만 };
        private COUNT _구분2 = 0;

        [Category("구분"), Description("사용할 Ledt개수 선택")]
        public COUNT GBN2 { get { return _구분2; } set { _구분2 = value; set_DisPlay(_구분2); } }

        public enum GROUP { 창고, 장소, 공정, 작업장 }; // 여기에 추가하면 디자인 속성에 선택할 수 있는 구분값이 추가됨
        private GROUP _구분3;

        [Category("구분"), Description("창고, 장소, 공정, 작업장 선택")]

        public GROUP GBN3 { get { return _구분3; } set { _구분3 = value; set_Text(_구분3); set_Ledt(_구분3, _구분1); } }

        public enum orientation { Horizontal, Vertical };
        private orientation _orientation;

        [Category("구분"), Description("가로, 세로 방향 설정")]
        public orientation 방향 { get { return _orientation; } set { _orientation = value; set_Position(_orientation); } }

        private bool radio_Visable = true;

        [Category(""), Description("사용할 Ledt개수 선택")]

        public bool Radio_Visable { get => radio_Visable; set { radio_Visable = value; set_Radio(value); } }

        private void set_Radio(bool bo)
        {
            rdo_Item01.Visible = bo;
            rdo_Item02.Visible = false;
        }

        private void set_Position(orientation orientation)
        {
            if(_구분2 == COUNT.둘다)
            {
                switch (orientation)
                {
                    case orientation.Vertical:
                        {
                            Size = new System.Drawing.Size(200, 57);
                            lbl_Ledit02.Location = new System.Drawing.Point(1, 34);
                            ledt_Item02.Location = new System.Drawing.Point(55, 31);
                        }
                        break;
                    case orientation.Horizontal:
                        {
                            Size = new System.Drawing.Size(416, 30);
                            lbl_Ledit02.Location = new System.Drawing.Point(212, 8);
                            ledt_Item02.Location = new System.Drawing.Point(266, 3);
                        }
                        break;
                }

            }
        }

        //선택한 속성값에 따라 lbl_text변경
        private void set_Text(GROUP GBN)
        {
            switch(GBN)
            {
                case GROUP.창고:
                    {
                        rdo_Item01.EditValue = 0;
                        rdo_Item01.SelectedIndex = 0;
                        rdo_Item02.EditValue = 0;
                        rdo_Item02.SelectedIndex = 0;
                        lbl_Ledit01.Text = "창      고";
                        lbl_Ledit02.Text = "장      소";
                        break;
                    }
                case GROUP.장소:
                    {
                        rdo_Item01.EditValue = 0;
                        rdo_Item01.SelectedIndex = 0;
                        rdo_Item02.EditValue = 0;
                        rdo_Item02.SelectedIndex = 0;
                        lbl_Ledit01.Text = "장      소";
                        lbl_Ledit02.Visible = false;
                        ledt_Item02.Visible = false;
                        break;
                    }
                case GROUP.공정:
                    {
                        rdo_Item01.EditValue = 1;
                        rdo_Item01.SelectedIndex = 1;
                        rdo_Item02.EditValue = 1;
                        rdo_Item02.SelectedIndex = 1;
                        lbl_Ledit01.Text = "공      정";
                        lbl_Ledit02.Text = "작  업  장";
                        break;
                    }
                case GROUP.작업장:
                    {
                        rdo_Item01.EditValue = 1;
                        rdo_Item01.SelectedIndex = 1;
                        rdo_Item02.EditValue = 1;
                        rdo_Item02.SelectedIndex = 1;
                        lbl_Ledit01.Text = "작  업  장";
                        lbl_Ledit02.Visible = false;
                        ledt_Item02.Visible = false;
                        break;
                    }
            }
        }
        
        //선택한 속성에 따라 LookUpEdit값을 채워줌
        private void set_Ledt(GROUP GBN, All_USE GBN1)
        {
            try
            {
                if (GBN.ToString() == "장소" || GBN.ToString() == "작업장")
                {
                    _구분2 = COUNT.하나만;
                    //if (GBN.ToString() == "장소" || GBN.ToString() == "작업장")
                    //{
                    //    modMSG.MsgInformation("창고 또는 공정을 선택하세요","확인");

                    //    return;
                    //}
                }

                DataTable dt;

                dt = Common_Transaction.df_Transaction(3, new string[] { env.Company,
                                                                        env.Factory,
                                                                        GBN1.ToString().Equals("전체포함") ? "0" : GBN1.ToString().Equals("전체제외") ? "1" : "%",            // 전체 포함 / 미포함
                                                                        rdo_Item01.EditValue.ToString(),  // 창고 / 공정
                                                                        (GBN.ToString().Equals("창고") || GBN.ToString().Equals("공정")) ? "0" : (GBN.ToString().Equals("장소") || GBN.ToString().Equals("작업장")) ? "1" : "%",  // OPER / LOC
                                                                        "%"                                                                                                           
                                                                        }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    modUTIL.DevLookUpEditorSet(ledt_Item01, dt, "NAME", "CODE");
                    ledt_Item01.EditValue = dt.Rows[0]["CODE"].ToString();
                }
            }
            catch(Exception e)
            {
            }
        }

        //Ledt사용 개수에 따라 디자인에 표시되는 LookUp개수 변경
        private void set_DisPlay(COUNT GBN2)
        {
            switch(GBN2)
            {
                case COUNT.하나만:
                    {
                        ledt_Item02.Visible = false;
                        lbl_Ledit02.Visible = false;
                        this.ledt_Item01.EditValueChanged -= new System.EventHandler(this.ledt_Item01_EditValueChanged);
                        Size = new System.Drawing.Size();
                        break;
                    }
                case COUNT.둘다:
                    {
                        ledt_Item02.Visible = true;
                        lbl_Ledit02.Visible = true;
                        this.ledt_Item01.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
                        set_Position(_orientation);
                        break;
                    }
            }
        }

        //1번 LookUp이 변경될때 이벤트 발생
        private void ledt_Item01_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                dt = Common_Transaction.df_Transaction(3, new string[] { env.Company,
                                                                        env.Factory,
                                                                        GBN1.ToString().Equals("전체포함") ? "0" : GBN1.ToString().Equals("전체제외") ? "1" : "%",            // 전체 포함 / 미포함
                                                                        rdo_Item01.EditValue.ToString(),  // 창고 / 공정
                                                                        "1",  // OPER / LOC
                                                                        ledt_Item01.EditValue.ToString()
                                                                        }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    ledt_Item02.Properties.ForceInitialize();
                    modUTIL.DevLookUpEditorSet(ledt_Item02, dt, "NAME", "CODE");
                    ledt_Item02.EditValue = dt.Rows[0]["CODE"].ToString();
                }
            }
            catch (Exception ee)
            {

            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                dt = Common_Transaction.df_Transaction(3, new string[] { env.Company,
                                                                        env.Factory,
                                                                        GBN1.ToString().Equals("전체포함") ? "0" : GBN1.ToString().Equals("전체제외") ? "1" : "%",            // 전체 포함 / 미포함
                                                                        rdo_Item01.EditValue.ToString(),  // 창고 / 공정
                                                                        (GBN3.ToString().Equals("창고") || GBN3.ToString().Equals("공정")) ? "0" : (GBN3.ToString().Equals("장소") || GBN3.ToString().Equals("작업장")) ? "1" : "%",  // OPER / LOC
                                                                        "%"
                                                                        }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    modUTIL.DevLookUpEditorSet(ledt_Item01, dt, "NAME", "CODE");
                    ledt_Item01.EditValue = dt.Rows[0]["CODE"].ToString();
                }
            }
            catch(Exception ee)
            {

            }
        }

        private void rdo_Item02_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt;

                dt = Common_Transaction.df_Transaction(3, new string[] { env.Company,
                                                                        env.Factory,
                                                                        GBN1.ToString().Equals("전체포함") ? "0" : GBN1.ToString().Equals("전체제외") ? "1" : "%",            // 전체 포함 / 미포함
                                                                        rdo_Item02.EditValue.ToString(),  // 창고 / 공정
                                                                        (GBN3.ToString().Equals("창고") || GBN3.ToString().Equals("공정")) ? "0" : (GBN3.ToString().Equals("장소") || GBN3.ToString().Equals("작업장")) ? "1" : "%",  // OPER / LOC
                                                                        "%"
                                                                        }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    modUTIL.DevLookUpEditorSet(ledt_Item01, dt, "NAME", "CODE");
                    ledt_Item01.EditValue = dt.Rows[0]["CODE"].ToString();
                }
            }
            catch (Exception ee)
            {

            }
        }
    }
}
