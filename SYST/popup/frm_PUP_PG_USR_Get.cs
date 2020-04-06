using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DH_Core;
using System.Data.SqlClient;

namespace SYST.Popup
{
    public partial class frm_PUP_PG_USR_Get : DXfrmWMES_BasePUP_Get
    {
        
        public frm_PUP_PG_USR_Get(_Environment Env, string Param)
        {
            InitializeComponent();
            env = Env;
            //  gSYS_GBN = Param; //프로그램 사용자 등록인지 ? 작업자 등록인지 여부 
            DataSet DS;
            this.Size = new System.Drawing.Size(459, 497);
            DS = null;
            DS = df_Transaction(10, null);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                grd_Data01.DataSource = DS.Tables[0];
                DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
                txt_Item01.Text = DR["U_ID"].ToString();
                txt_Item02.Text = DR["U_NM"].ToString();
            }

            DS = null;
            gParam = new string[] { "사용자그룹" };
            DS = df_Transaction(1, gParam);
            if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                this.ledt_GRUP.DataSource = DS.Tables[0];
                modUTIL.DevLookUpEditorSet(ledt_GRUP, DS.Tables[0], "CODE", "NAME", "코드", "명칭");

            }
        }

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        string[] gParam = null;
        string gU_ID = null;
        string gU_NM = null;
        string gGRUP = null;
       // string gSYS_GBN = "";
        //ArrayList cboKeyList = null;
        //필수요소
        _Environment env;

        public string USER { get { try { return gU_ID; } catch { return ""; } } }
        public string USERNAME { get { try { return gU_NM; } catch { return ""; } } }
        public string GROUP { get { try { return gGRUP; } catch { return ""; } } }

        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////



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

        private void grd_Data01_Click(object sender, EventArgs e)
        {
            //DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            //txt_Item01.Text = DR["U_ID"].ToString();
            //txt_Item02.Text = DR["U_NM"].ToString();

        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            gU_ID = DR["U_ID"].ToString();
            gU_NM = DR["U_NM"].ToString();
            gGRUP = DR["GRUP"].ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.Dispose();
        }

        private void grd_Data01_DoubleClick(object sender, EventArgs e)
        {
            if (gview_Data01.RowCount == 0 || gview_Data01.RowCount < 0) return;
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            gU_ID = DR["U_ID"].ToString();
            gU_NM = DR["U_NM"].ToString();
            gGRUP = DR["GRUP"].ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }






        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////
        /// <summary>
        /// 라디오버튼클릭 시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio01_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  radio01.Properties.Items[0].
            int RCheck = ((DevExpress.XtraEditors.RadioGroup)(sender)).SelectedIndex;
            if (RCheck.Equals(0))
            {
                txt_Item01.Enabled = true;
                txt_Item02.Enabled = false;
            }
            else
            {
                txt_Item01.Enabled = false;
                txt_Item02.Enabled = true;
            }
        }


        private void txt_Item01_EditValueChanged(object sender, EventArgs e)
        {
            //그리드와 Text박스를 비고하여 Rowfocus를 찾는다.
            string chkstring = ((DevExpress.XtraEditors.TextEdit)(sender)).Text;
            string TextBoxPosition = ((DevExpress.XtraEditors.TextEdit)(sender)).Tag.ToString();
            DataTable DT = (DataTable)grd_Data01.DataSource;
            //foreach (DataRow DR in DT.Rows)
            //{
            DataRow DR;
            for (int rows = 0; rows < gview_Data01.RowCount; rows++)
            {
                DR = DT.Rows[rows];
                if (TextBoxPosition.Equals("1"))
                {
                    if (DR["U_ID"].ToString().IndexOf(chkstring) >= 0)
                    {
                        gview_Data01.FocusedRowHandle = rows;
                        break;
                    }
                }
                else if (TextBoxPosition.Equals("2"))
                {
                    if (DR["U_NM"].ToString().IndexOf(chkstring) >= 0)
                    {
                        gview_Data01.FocusedRowHandle = rows;
                        break;
                    }
                }
            }
        }


        private void gview_Data01_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gview_Data01.RowCount.Equals(0)) return;
            try
            {
                DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);

                txt_Item01.Text = DR["U_ID"].ToString();
                txt_Item02.Text = DR["U_NM"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            string Errchk;
            gConst.DbConn.ClearDB();
            switch (Index)
            {
                case 1: //그룹정보 콤보
                    {
                        //SELECT RTRIM(CODE) CODE, RTRIM(NAME) NAME
                        //FROM tb_code
                        //WHERE    C_ID = '사용자그룹'
                        //AND STAT <> 'D'
                        //ORDER BY CODE


                        string Query = "SELECT      RTRIM(CODE) CODE, RTRIM(NAME) NAME " +
                                       "FROM        tb_code WITH(NOLOCK) " +
                                       "WHERE       C_ID Like '" + gParam[0] + "%' " +
                                       "AND STAT <> 'D' " +
                                       "ORDER BY CODE ASC ";
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                    }
                    break;

                case 10: //사원정보를 조회
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_USER_GROUP";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        DT = gConst.DbConn.GetDataSetQuery(out Errchk);

                        //string Query = "SELECT RTRIM(USR) U_ID, RTRIM(UNAM) U_NM, RTRIM(GRUP) GRUP FROM TS_USER WITH(NOLOCK) WHERE COMP='"
                        //        +env.Company+"' AND FACT = '"+env.Factory
                        //        +"' AND USR <> 'SUSER' "
                        //        +" AND STAT <> 'D' ORDER BY U_ID ";
                        //    DT = gConst.DbConn_MES.GetDataSetQuery(Query, out Errchk);
                    }
                    break;

            }
            return DT;
        }


        #endregion










    }
}
