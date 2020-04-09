﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DH_Core;
using DH_Core.DB;

namespace DH_Core.CommonPopup
{
    public partial class frmPASSWORD_CHG : DevExpress.XtraEditors.XtraForm
    {
        public frmPASSWORD_CHG(_Environment ENV, string[] Param, string type)
        {
            InitializeComponent();
            env = ENV;
            COMP = Param[0];
            FACT = Param[1];
            USR = txt_USER_ID.Text = Param[2];
            p_type = type; //로그인 화면 또는 사용자 관리 화면
        }
        #region Attributes (속성정의 집합)
        ////////////////////////////////
        string[] gParam = null;
        string COMP = "";
        string FACT = "";
        string USR = "";
        string error_msg = null;
        string p_type = null;
        _Environment env;
        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////
        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////

        private void sbtn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void sbtn_Save_Close_Click(object sender, EventArgs e)
        {
            if (txt_USER_ID.Text.Trim() == "")
            {
                MsgBox.MsgInformation("변경 할 사원코드를 입력하세요.", "확인");
                txt_USER_ID.Focus();
                return;
            }
            else if (txt_CURRENT_PASSWORD.Text.Trim() == "" && p_type != null)
            {
                MsgBox.MsgInformation("현재 비밀번호를 입력하세요.", "확인");
                txt_CURRENT_PASSWORD.Focus();
                return;
            }
            else if (txt_NEW_PASSWORD.Text.Trim() == "")
            {
                MsgBox.MsgInformation("신규 비밀번호를 입력하세요.", "확인");
                txt_NEW_PASSWORD.Focus();
                return;
            }
            else if (txt_CHK_PASSWORD.Text.Trim() == "")
            {
                MsgBox.MsgInformation("확인 비밀번호를 입력하세요.", "확인");
                txt_CHK_PASSWORD.Focus();
                return;
            }

            if (txt_NEW_PASSWORD.Text.Equals(txt_CHK_PASSWORD.Text))
            {
                String[] gOut_MSG = new string[] { "", "" };
                gParam = new string[] { txt_CURRENT_PASSWORD.Text.Trim(),
                                        txt_NEW_PASSWORD.Text.Trim(),
                                        txt_USER_ID.Text.Trim()
                };
                DataSet DS = df_Transaction(20, gParam, ref gOut_MSG);
                if(gOut_MSG[0] == "Y")
                {
                    MsgBox.MsgInformation("정상적으로 변경되었습니다.", "알림");
                    this.DialogResult = DialogResult.OK;

                }
            }
            else
            {
                MsgBox.MsgErr("신규 비밀번호와 확인 비밀번호가 틀립니다.", "오류");
                return;
            }
        }

        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////

        private void Form_Load(object sender, EventArgs e)
        {
            if(p_type == null) //화면에서 실행 할 경우
            {
                lbl_CURRENT_PASSWORD.Visible = txt_CURRENT_PASSWORD.Visible = false;
            }
            //txt_CURRENT_PASSWORD.Text = "";



            ////Events List
            //this.txt_CURRENT_PASSWORD.EditValueChanged += new System.EventHandler(this.txt_CURRENT_PASSWORD_EditValueChanged);
            ////this.txt_CURRENT_PASSWORD.Leave += new System.EventHandler(this.txt_CURRENT_PASSWORD_Leave);


        }


        #endregion

        #region DB CRUD(데이터베이스 처리)
        ///////////////////////////////

        /// <summary>
        /// DB Select
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        private DataSet df_Transaction(int Index, object[] Param, ref string[] Out_MSG)
        {
            // Index 1 ~  9  : ComboBox관련 등록관련 쿼리 또는 프로시져
            // Index 10 ~ 19 : Select관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            DataSet DT = null;
            error_msg = string.Empty;
            gConst.DbConn.ClearDB();
            gConst.DbConn.BeginTrans();
            switch (Index)
            {
                case 20://사용자로그인 비밀번호 변경
                    {
                        gConst.DbConn.ProcedureName = "dbo.USP_COM_SET_MES_CHG_PSWD";
                        gConst.DbConn.AddParameter("CURR_PWD", MSSQLAgent.DBFieldType.String, Param[0]);
                        gConst.DbConn.AddParameter("NEW_PWD", MSSQLAgent.DBFieldType.String, Param[1]);
                        gConst.DbConn.AddParameter("USR", MSSQLAgent.DBFieldType.String, Param[2]);
                        gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, p_type == null ? "W" : "L");
                        gConst.DbConn.AddParameter("COMP", MSSQLAgent.DBFieldType.String, COMP);
                        gConst.DbConn.AddParameter("FACT", MSSQLAgent.DBFieldType.String, FACT);
                        gConst.DbConn.AddParameter("RESFLAG", MSSQLAgent.DBFieldType.String, Out_MSG[0], 1, MSSQLAgent.DBDirection.Output);
                        gConst.DbConn.AddParameter("RESULT", MSSQLAgent.DBFieldType.String, Out_MSG[1], 200, MSSQLAgent.DBDirection.Output);
                    }
                    break;
            }
            gConst.DbConn.ExecuteNonQuery(out Out_MSG[0], out Out_MSG[1]);
            if (Out_MSG[0].Equals("N"))
            {
                gConst.DbConn.Rollback();
                MsgBox.MsgInformation(Out_MSG[1], "확인");
            }
            else if (Out_MSG[0].Equals("P")) //처리 없이 끝난 경우
            {
                gConst.DbConn.Commit();
            }
            else
            {
                gConst.DbConn.Commit();
            }
            gConst.DbConn.DBClose();
            return DT;
        }



        #endregion

        private void txt_CURRENT_PASSWORD_EditValueChanged(object sender, EventArgs e)
        {
            //if(txt_CURRENT_PASSWORD.Text.Length==0) return;
            //if(txt_CURRENT_PASSWORD.Text.Length >= PSWD.Length)
            //if(!txt_CURRENT_PASSWORD.Text.Equals(PSWD))
            //{

            //    modMSG.MsgErr("비밀번호가 다릅니다.","오류");
            //txt_CURRENT_PASSWORD.Text = "";
            //}
        }

        private void txt_CHK_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                sbtn_Save_Close_Click(null, null);
            }
        }

        /*
        private void txt_CURRENT_PASSWORD_Leave(object sender, EventArgs e)
        {
            if (!txt_CURRENT_PASSWORD.Text.Equals(PSWD))
            {

                modMSG.MsgErr("비밀번호가 다릅니다.", "오류");
                txt_CURRENT_PASSWORD.Focus();
                //txt_CURRENT_PASSWORD.Text = "";
            }
        }
        */
    }
}