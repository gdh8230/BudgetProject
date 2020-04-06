using DH_Core;
using System;
using System.Data;
using System.Drawing;

#region Usings
#endregion

namespace MST
{
    public partial class frm_PUP_Item_Get : DXfrmWMES_BasePUP_Get
    {
        public frm_PUP_Item_Get(_Environment Env, object[] Param)
        {
            InitializeComponent();
            env = Env;
            //gParam2 : [0]Title , [1]제품구분 , [2]계정구분 , [3]공정그룹 , [4]모델코드
            gParam2 = Param;
            
            Init_SCR();
            //DB로 부터 데이터를 불러와서 표시한다.
            DataDisplay();
            
        }

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //환경변수 선언
        _Environment env;
        string[] gParam = null;
        object[] gParam2 = null;
        string gCODE = null;
        string gNAME = null;
        string gSPEC = null;
        string gUNIT = null;
        string gCUST = null;
        string gCUST_NAME = null;
        string gPACK_PO_QT = null;
        string gPACK_SO_QT = null;
        string gIN_BASELOC_CD = null;
        string gIN_LOC_CD = null;
        string gOUT_BASELOC_CD = null;
        string gOUT_LOC_CD = null;
        string gLOT_FG = null;
        string gQC_FG = null;
        string gL_CD = null;
        string gM_CD = null;
        string gS_CD = null;
        string gACCT_FG = null;
        string gITEMGRP_CD = null;
        string gITEMGRP_NM = null;
        string ITEM = "";
        string ITEM_NAME = "";
        DataRow gITEM_ROW;
        DataSet DT_GRD01;
        //속성
        public string CODE { get { try { return gCODE; } catch { return ""; } } }
        public string NAME { get { try { return gNAME; } catch { return ""; } } }
        public string SPEC { get { try { return gSPEC; } catch { return ""; } } }
        public string UNIT { get { try { return gUNIT; } catch { return ""; } } }
        public string CUST { get { try { return gCUST; } catch { return ""; } } }
        public string CUST_NAME { get { try { return gCUST_NAME; } catch { return ""; } } }
        public string PACK_PO_QT { get { try { return gPACK_PO_QT; } catch { return ""; } } }
        public string PACK_SO_QT { get { try { return gPACK_SO_QT; } catch { return ""; } } }
        public string IN_BASELOC_CD { get { try { return gIN_BASELOC_CD; } catch { return ""; } } }
        public string IN_LOC_CD { get { try { return gIN_LOC_CD; } catch { return ""; } } }
        public string OUT_BASELOC_CD { get { try { return gOUT_BASELOC_CD; } catch { return ""; } } }
        public string OUT_LOC_CD { get { try { return gOUT_LOC_CD; } catch { return ""; } } }
        public string LOT_FG { get { try { return gLOT_FG; } catch { return ""; } } }
        public string QC_FG { get { try { return gQC_FG; } catch { return ""; } } }
        public string L_CD { get { try { return gL_CD; } catch { return ""; } } }
        public string M_CD { get { try { return gM_CD; } catch { return ""; } } }
        public string S_CD { get { try { return gS_CD; } catch { return ""; } } }
        public string ACCT { get { try { return gACCT_FG; } catch { return ""; } } }
        public string ITEMGRP_CD { get { try { return gITEMGRP_CD; } catch { return ""; } } }
        public string ITEMGRP_NM { get { try { return gITEMGRP_NM; } catch { return ""; } } }
        public DataRow ITEM_ROW { get { try { return gITEM_ROW; } catch { return null; } } }
        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////

        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            this.Size = new System.Drawing.Size(855, 641);

            DataTable DT;
            DT = null;
            //this.Text = "frm_PUP_Item_Get("+gParam2[0].ToString()+"코드조회)"; 
            gParam = new string[] { env.Company, env.Factory, "제품구분", "D", "1" };//List에 전체 있는 조회
            DT = Common_Transaction.df_Transaction(1, gParam).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item01, DT, "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_ORD_FG, DT, "CODE", "NAME", "CODE", "NAME");
                 ledt_Item01.EditValue = gParam2[1].ToString();// "1";//초기 생산품으로 설정
                ledt_Item01.BackColor = Color.PapayaWhip;
            }

            DT = null; //계정구분 조회
            gParam = new string[] { env.Company, env.Factory, "계정구분", "D", "1" };
            //DT1 = df_Transaction(1, gParam).Tables[0];
            DT = Common_Transaction.df_Transaction(1, gParam).Tables[0];

            //if (DT1 != null)
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item02, DT, "NAME", "CODE");
                ledt_Item02.EditValue = "%";
                ledt_Item02.BackColor = Color.PapayaWhip;
            }

            // 품목군 조회
            gParam = new string[] { env.Company, "1" }; 
            DT = Common_Transaction.df_Transaction( 8, gParam ).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item03, DT, "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_ITEMGRP_CD, DT, "CODE", "NAME", "CODE", "NAME");
                ledt_Item03.EditValue = "%";
            }

            //대분류 LookupEdit에 표시할 데이터 조회
            gParam = new string[] { env.Company, env.Factory, "1", "Z1" };
            DT = Common_Transaction.df_Transaction( 4, gParam ).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item04, DT, "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_L_CD, DT, "CODE", "NAME", "CODE", "NAME");
                ledt_Item04.EditValue = "%";
            }

            //중분류 LookupEdit에 표시할 데이터 조회
            gParam = new string[] { env.Company, env.Factory, "1", "Z2" };
            DT = Common_Transaction.df_Transaction(4, gParam).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item05, DT, "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_M_CD, DT, "CODE", "NAME", "CODE", "NAME");
                ledt_Item05.EditValue = "%";
            }

            //소분류 LookupEdit에 표시할 데이터 조회        
            gParam = new string[] { env.Company, env.Factory, "1", "Z3" };
            DT = Common_Transaction.df_Transaction(4, gParam).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_Item06, DT, "NAME", "CODE");
                modUTIL.DevLookUpEditorSet(ledt_S_CD, DT, "CODE", "NAME", "CODE", "NAME");
                ledt_Item06.EditValue = "%";
            }

            this.ledt_Item01.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
            this.ledt_Item02.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
            this.ledt_Item06.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
            this.ledt_Item04.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
            this.ledt_Item05.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);
            this.ledt_Item03.EditValueChanged += new System.EventHandler(this.ledt_Item01_EditValueChanged);

            this.gview_Data01.CustomRowFilter += new DevExpress.XtraGrid.Views.Base.RowFilterEventHandler( gview_Data01_CustomRowFilter );
        }


        /// <summary>
        /// DB로 부터 데이터를 불러와서 표시한다.
        /// </summary>
        private void DataDisplay()
        {
            try
            {
                gParam = new string[] { ledt_Item01.EditValue.ToString(), 
                                        ledt_Item02.EditValue.ToString(),
                                        ledt_Item03.EditValue.ToString(),
                                        ledt_Item04.EditValue.ToString(),
                                        ledt_Item05.EditValue.ToString(),
                                        ledt_Item06.EditValue.ToString(),
                                        gParam2[3].ToString().Equals("")?"%":gParam2[3].ToString(),
                                        gParam2[4].ToString().Equals("")?"%":gParam2[4].ToString()};
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

            gCODE = DR["CODE"].ToString();
            gNAME = DR["NAME"].ToString();
            gSPEC = DR["SPEC"].ToString();
            gUNIT = DR["UNIT"].ToString();
            gITEMGRP_CD = DR["ITEMGRP_CD"].ToString();
            gITEMGRP_NM = DR["ITEMGRP_NM"].ToString();
            gCUST = DR["TRMAIN_CD"].ToString();
            gCUST_NAME = DR["CUST_NAME"].ToString();
            gPACK_PO_QT = DR["PACK_PO_QT"].ToString();
            gPACK_SO_QT = DR["PACK_SO_QT"].ToString();
            gLOT_FG = DR["LOT_FG"].ToString();
            gQC_FG = DR["QC_FG"].ToString();
            gL_CD = DR["L_CD"].ToString();
            gM_CD = DR["M_CD"].ToString();
            gS_CD = DR["S_CD"].ToString();
            gACCT_FG = DR["ACCT_FG"].ToString();
            gITEM_ROW = DR;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        
        private void grd_Data01_DoubleClick(object sender, EventArgs e)
        {
            if (gview_Data01.RowCount <= 0) return;
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            gCODE = DR["CODE"].ToString();
            gNAME = DR["NAME"].ToString();
            gSPEC = DR["SPEC"].ToString();
            gUNIT = DR["UNIT"].ToString();
            gITEMGRP_CD = DR["ITEMGRP_CD"].ToString();
            gITEMGRP_NM = DR["ITEMGRP_NM"].ToString();
            gCUST = DR["TRMAIN_CD"].ToString();
            gCUST_NAME = DR["CUST_NAME"].ToString();
            gPACK_PO_QT = DR["PACK_PO_QT"].ToString();
            gPACK_SO_QT = DR["PACK_SO_QT"].ToString();
            gLOT_FG = DR["LOT_FG"].ToString();
            gQC_FG = DR["QC_FG"].ToString();
            gL_CD = DR["L_CD"].ToString();
            gM_CD = DR["M_CD"].ToString();
            gS_CD = DR["S_CD"].ToString();
            gACCT_FG = DR["ACCT_FG"].ToString();
            gITEM_ROW = DR;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //그리드와 Text박스를 비고하여 Rowfocus를 찾는다.
            string chkstring = txt_Item01.Text;
            string TextBoxPosition = radio01.SelectedIndex.ToString();
            //DataTable DT = (DataTable)grd_Data01.DataSource;
            //DataRow DR;

            if (TextBoxPosition.Equals("0"))
            {
                ITEM_NAME = "";
                ITEM = txt_Item01.Text.ToUpper(); 
            }
            else if (TextBoxPosition.Equals("1"))
            {
                ITEM = "";
                ITEM_NAME = txt_Item02.Text.ToUpper();
            }
            grd_Data01.DataSource = null;
            grd_Data01.DataSource = DT_GRD01.Tables[0];
            grd_Data01.Refresh();
        }
        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////

        private void ledt_Item01_EditValueChanged(object sender, EventArgs e)
        {
            DataDisplay();
        }
        
        /// <summary>
        /// 라디오버튼클릭 시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio01_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void gview_Data01_CustomRowFilter( object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e )
        {
            //DataGridView view = sender as DataGridView;
            DataView dv = gview_Data01.DataSource as DataView;
            try
            {

                if ( dv[e.ListSourceRow]["CODE"].ToString().IndexOf( ITEM ) < 0
                    || dv[e.ListSourceRow]["NAME"].ToString().IndexOf( ITEM_NAME ) < 0
                    )
                {

                    e.Visible = false;
                    e.Handled = true;
                }
                else
                {
                    e.Visible = true;
                }
            }
            catch ( Exception ex )
            {
                modUTIL.WriteLog( ex.Message );
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
                case 10: //품목 정보를 조회
                    {
                        string Query = "SELECT          A.ITEM CODE, " +
                                        "               A.ITEM_NAME NAME, " +
                                        "               A.ITEM_SPEC SPEC, " +
                                        "               A.UNIT_DC UNIT," +
                                        "               A.ACCT_FG," +
                                        "               A.ACCT_FG_NM," +
                                        "               A.ODR_FG," +
                                        "               A.ODR_FG_NM," +
                                        "               A.UNITMANG_DC," +
                                        "               A.UNITCHNG_NB," +
                                        "               A.ITEMGRP_CD," +
                                        "               A.ITEMGRP_NM," +
                                        "               A.L_CD      ," +
                                        "               A.L_NM," +
                                        "               A.M_CD," +
                                        "               A.M_NM," +
                                        "               A.S_CD," +
                                        "               A.S_NM," +
                                        "               A.LOT_FG," +
                                        "               A.USEYN USE_YN," +
                                        "               A.TRMAIN_CD," +
                                        "               B.CUST_NAME," +
                                        "               A.PACK_PO_QT," +
                                        "               A.PACK_PO_RT," +
                                        "               A.PACK_SO_QT," +
                                        "               A.PACK_SO_RT," +
                                        "               A.QC_FG," +
                                        "               A.NOTE BARCODE_DC " +
                                        "   FROM        tb_item A WITH(NOLOCK) " +
                                        "   LEFT JOIN   tb_CUST B WITH(NOLOCK) " +
                                        "   ON          B.COMP = A.COMP " +
                                        "   AND         B.FACT = A.FACT " +
                                        "   AND         B.CUST = A.TRMAIN_CD " +
                                        "   AND         B.STAT <> 'D' " +
                                        "   WHERE       A.COMP ='" + env.Company + "' " +
                                        "   AND         A.FACT ='" + env.Factory + "' " +
                                        "   AND         ISNULL(A.ACCT_FG,'')    LIKE '" + gParam[1] + "' " +
                                        "   AND         ISNULL(A.ITEMGRP_CD,'') LIKE '" + gParam[2] + "' " +
                                        "   AND         ISNULL(A.L_CD,'') LIKE '" + gParam[3] + "' " +
                                        "   AND         ISNULL(A.M_CD,'') LIKE '" + gParam[4] + "' " +
                                        "   AND         ISNULL(A.S_CD,'') LIKE '" + gParam[5] + "' " +
                                        "   AND         A.USEYN     = '1' " +
                                        "   AND         A.STAT      <> 'D' " +
                                        "   ORDER BY ITEM, ITEM_NAME";
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
            }
            return DT;
        }
        #endregion 


   


    }
}
