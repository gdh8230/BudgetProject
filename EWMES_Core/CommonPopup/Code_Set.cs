using System;
using System.Data;
using System.Collections;

namespace DH_Core.CommonPopup
{
    public partial class Code_Set : DXfrmWMES_BasePUP_Set
    {

        public Code_Set(_Environment Env, string Param)
        {
            InitializeComponent();
            //this.Size = new System.Drawing.Size(460, 170);
            //화면초기회
            Init_SCR();

            env = Env;
            grp_Top01.Text = Param + "코드입력";
            lbl_Title01.Text = Param + "코드";
            lbl_Title02.Text = Param + "명";
         }

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        string gCode = null;
        string gName = null;

        //필수요소
        _Environment env;

        public string CODE { get { try { return gCode; } catch { return ""; } } }
        public string NAME { get { try { return gName; } catch { return ""; } } }

        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////

        private void frm_PUPMenuSet_Load(object sender, EventArgs e)
        {
            env = new _Environment();
        }

        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        /// 
        private void Init_SCR()
        {
        }

        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////

        /// <summary>
        /// 화면을 닫습니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            //this.Dispose();
        }

        /// <summary>
        /// 선택항목저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            gCode = txt_Code.Text.ToUpper();
            gName = txt_Name.Text.ToUpper();
            if (gCode.Equals("") || gName.Equals(""))
            {
                MsgBox.MsgErr("코드 또는 명칭을 등록하지 않았습니다.", "알림");
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancel_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void grd_Data01_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////
        
        /// <summary>
        /// Enter키 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Name_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (Convert.ToInt32(e.KeyChar))
            {
                case 13:
                    {
                        btn_Save.PerformClick();
                    } break;
            }
        }

        /// <summary>
        /// Enter키 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Code_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (Convert.ToInt32(e.KeyChar))
            {
                case 13:
                    {
                        txt_Name.Focus();
                    } break;
            }
        }
        /// <summary>
        /// 라디오버튼클릭 시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio01_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RCheck = ((DevExpress.XtraEditors.RadioGroup)(sender)).SelectedIndex;
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
        private DataSet df_Transaction(int Index, object[] Param)
        {
            // Index 1 ~  9  : ComboBox관련 등록관련 쿼리 또는 프로시져
            // Index 10 ~ 19 : Select관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            DataSet DT = null;
            gConst.DbConn.ClearDB();
            switch (Index)
            {
                case 0: //현재화면 사용가능여부 조회
                    {
                        //string Query = "SELECT        ACCE " +
                        //               "FROM        TB_ACCESS WITH(NOLOCK) " +
                        //               "WHERE        USR ='" + gParam[0] + "' " +
                        //               "AND            WIND ='" + gParam[1] + "' ";
                        //DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;

                case 1: //구분콤보/서버콤보/속성콤보 표시내용 조회
                    {
                        //string Query = "SELECT      CODE, NAME " +
                        //               "FROM        tb_code WITH(NOLOCK) " +
                        //               "WHERE        C_ID = '" + gParam[0] + "' " +
                        //               "ORDER BY    NAME ASC ";
                        //DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;

                case 2: //구분콤보와 상위 그리드에 표시할 데이터조회 //하위 그리드에 표시할 데이터조회
                    {
                    //    string Query = "SELECT        C_MENU, MENU, R_ID, UPIL, P_MENU, " +
                    //                   "MGBN, " +
                    //                   "(CASE MGBN    WHEN 'E' THEN '실행파일' " +
                    //                   "            WHEN 'B' THEN '기타' " +
                    //                   "            WHEN 'F' THEN '메뉴폴더' " +
                    //                   "            WHEN 'S' THEN '구분자' " +
                    //                   "            WHEN 'W' THEN '프로그램' " +
                    //                   "            ELSE '구분자없음' END) NAME, " +
                    //                   "            WIND, " +
                    //                   "            [SITE], " +
                    //                   "            (CASE [SITE]    WHEN 'A' THEN '전체' " +
                    //                   "                            WHEN 'R' THEN 'RND' " +
                    //                   "                            WHEN 'U' THEN 'UAT' " +
                    //                   "                            WHEN 'P' THEN 'PRD' ELSE '알수없없음' END) SITENAME , " +
                    //                   "            VISIBLE, " +
                    //                   "            (CASE VISIBLE   WHEN 'Y' THEN '보이기' " +
                    //                   "                            WHEN 'N' THEN '숨기기' ELSE '숨기기' END) VISIBLENAME , " +
                    //                   "            NOTE, STAT, M_ID, M_IL, U_ID, U_IL " +
                    //                   "FROM        tb_menu " +
                    //                   "WHERE        P_MENU LIKE '" + gParam[0] + "%' " +
                    //                   "ORDER BY    P_MENU ASC, C_MENU ASC  ";
                    //    DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 5: //업무구분을 조회
                    {
                        //string Query = "SELECT      '%' CODE,'전체' NAME " +
                        //               "UNION ALL " +
                        //               "SELECT      RTRIM(CODE) CODE, RTRIM(NAME) NAME " +
                        //               "FROM        tb_code " +
                        //               "WHERE       C_ID = '업무구분' " +
                        //               "AND         STAT <> 'D' " +
                        //               "ORDER BY    CODE";
                        //DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 6: //그리드에표시할 업무구분조회
                    {
                        //string Query = "SELECT      RTRIM(CODE) CODE, RTRIM(NAME) NAME " +
                        //               "FROM        tb_code " +
                        //               "WHERE       C_ID = '업무구분' " +
                        //               "AND         CODE LIKE '%" + Param[0] + "' " +
                        //               "AND         STAT <> 'D' " +
                        //               "ORDER BY    CODE";
                        //DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;

                case 10: //프로그램등록목록을 조회
                    {
                        //string Query = "     SELECT        A.R_ID, " +
                        //               "                A.UPIL, " +
                        //               "                B.CODE, " +
                        //               "                B.NAME, " +
                        //               "                A.WIND, " +
                        //               "                A.WNAM, " +
                        //               "                A.NOTE, " +
                        //               "                A.STAT, " +
                        //               "                A.M_IL, " +
                        //               "                A.M_ID, " +
                        //               "                A.U_IL, " +
                        //               "                A.U_ID " +
                        //               "    FROM        TB_PROGRAM A " +
                        //               "    INNER JOIN    tb_code B " +
                        //               "    ON            B.CODE = A.SYST " +
                        //               "    WHERE       A.SYST LIKE '" + gParam[0] + "' " +
                        //               "    ORDER BY    A.WIND  ASC";
                        //DT = gConst.DbConn_SMS.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
            }
            return DT;
        }

        #endregion
    }
}
