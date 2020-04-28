using DH_Core;
using DH_Core.DB;
using System;
using System.Data;
using System.Drawing;

#region Usings
#endregion

namespace EXEC
{
    public partial class popup_Get_AdminNo : DXfrmWMES_BasePUP_Get
    {
        public popup_Get_AdminNo(_Environment Env, object[] Param)
        {
            InitializeComponent();
            env = Env;
            
            Init_SCR();
            //DB로 부터 데이터를 불러와서 표시한다.
            DataDisplay();
            
        }

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //환경변수 선언
        _Environment env;
        string[] gParam = null;
        string gADMIN_NO = null;
        DataRow gITEM_ROW;
        DataSet DT_GRD01;
        //속성
        public string ADMIN_NO { get { try { return gADMIN_NO; } catch { return ""; } } }
        public DataRow ITEM_ROW { get { try { return gITEM_ROW; } catch { return null; } } }
        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////

        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {

        }


        /// <summary>
        /// DB로 부터 데이터를 불러와서 표시한다.
        /// </summary>
        private void DataDisplay()
        {
            try
            {
                gParam = new string[] { DatePicker1.GetStartDate.ToString()
                                        ,DatePicker1.GetEndDate.ToString()
                                        ,txt_ADMIN_NO.Text.Equals("") ? "%" : txt_ADMIN_NO.Text};
                DT_GRD01 = df_Transaction( 10, gParam );
                if ( DT_GRD01 != null )
                {
                    grd_Data01.DataSource = DT_GRD01.Tables[0];
                    DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
                }
            }
            catch
            { 
            
            }

        }
        
        
        //////////
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
            //this.Dispose();
            this.DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            if (gview_Data01.RowCount <= 0) return;

            gADMIN_NO = DR["ADMIN_NO"].ToString();
            gITEM_ROW = DR;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        
        private void grd_Data01_DoubleClick(object sender, EventArgs e)
        {
            if (gview_Data01.RowCount <= 0) return;
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            gADMIN_NO = DR["ADMIN_NO"].ToString();
            gITEM_ROW = DR;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            grd_Data01.DataSource = null;
            grd_Data01.DataSource = DT_GRD01.Tables[0];
            grd_Data01.Refresh();
        }
        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////

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
            string Errchk;
            gConst.DbConn.ClearDB();
            switch (Index)
            {
                case 10: //품목 정보를 조회
                    {
                        string Query = "SELECT ADMIN_NO " +
                                        "		,PLAN_DT " +
                                        "		,PLAN_TITLE " +
                                        "		,A.BUSINESS_GBN " +
                                        "		,B.NAME " +
                                        "		,A.PJT_CD " +
                                        "		,C.PJT_NM " +
                                        "FROM	SPND_RSLT_H A WITH(NOLOCK) " +
                                        "JOIN	TS_CODE B WITH(NOLOCK) " +
                                        "ON		A.BUSINESS_GBN = B.CODE " +
                                        "AND		B.C_ID = '사업구분' " +
                                        "LEFT JOIN TB_PJT C WITH(NOLOCK) " +
                                        "ON		A.PJT_CD = C.PJT_CD " +
                                        "WHERE	PLAN_DT BETWEEN '" + Param[0] + "' AND '" + Param[1] + "' " +
                                        "AND		ADMIN_NO LIKE '" + Param[2] + "' + '%' " +
                                        "AND		A.PLAN_USER LIKE '" + env.EmpCode + "' + '%' " +
                                        "AND		A.STAT <> 'D' " +
                                        "AND		A.DEPT = '" + env.Dept + "' ";
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
            }
            return DT;
        }
        #endregion 


   


    }
}
