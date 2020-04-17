using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DH_Core;
using DH_Core.DB;
//using SYST.Popup;
using System.Drawing;
using SYST.Popup;
using System.Data.SqlClient;

namespace SYST
{
    public partial class AUTH : frmSub_Baseform_Search_STD
    {
        public AUTH()
        {
            InitializeComponent(); 
        }
        
        #region Attributes (속성정의 집합)
        ////////////////////////////////
        string[] gParam = null;
        string[] gOut_MSG = null;
        string[] Filter =new string[]{"","",""};
        static DataSet DT_GRD01 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD02 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD03 = new DataSet();    //FOR GIRD2
        static DataSet DT_GRD04 = new DataSet();    //FOR GRID2
        static DataSet DT_GRD05 = new DataSet();    //FOR GRID3
        static DataSet DT_GRD06 = new DataSet();    //FOR GRID3
        _Environment env;

        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////

        /// <summary>
        /// 기능버튼초기화
        /// </summary>
        private void AutoritySet()
        {
            DataRow DR = (DataRow)this.Tag;// btnAuthority.pf_btnAuthority(env.EmpCode, this.Name);
            if (DR != null)
            {
                try
                {
                    pnl_Search.Enabled = DR[0].Equals("Y") ? true : false;
                    pnl_Add.Enabled = DR[1].Equals("Y") ? true : false;
                    btn_PRG_ADD.Enabled = DR[1].Equals("Y") ? true : false;
                    pnl_Delete.Enabled = DR[3].Equals("Y") ? true : false;
                    btn_PRG_DELETE.Enabled = DR[3].Equals("Y") ? true : false;
                    pnl_Save.Enabled = DR[4].Equals("Y") ? true : false;
                    pnl_Print.Enabled = DR[5].Equals("Y") ? true : false;
                    pnl_Excel.Enabled = DR[6].Equals("Y") ? true : false;
                }
                catch
                {

                }

            }
        }

        /// <summary>
        /// 화면표시를 초기화 한다.
        /// </summary>
        private void Init_SCR()
        {
            DataTable DT;
            grd_Data01.DataSource = null;
            //부서정보조회
            DT = null;
            DT = df_Transaction(1, gParam).Tables[0];
            if (DT != null)
            {
                modUTIL.DevLookUpEditorSet(ledt_dept, DT, "NAME", "CODE");
                ledt_dept.ItemIndex = 0;
            }
        }

          

        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////

        /// <summary>
        /// 검색클릭처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Search_Click(object sender, EventArgs e)
        {
            //if (gview_Data01.RowCount <= 0) return;
            DevExpress.XtraEditors.SimpleButton btnchk = (DevExpress.XtraEditors.SimpleButton)sender;
            if (sender == null || btnchk.Tag.Equals("1"))
            {
                gParam = new string[] { ledt_dept.EditValue.ToString() == ""? "%" : ledt_dept.EditValue.ToString()
                                        ,txt_emp_cd.Text
                                        ,txt_emp_nm.Text
                                        };
                DT_GRD01 = null;
                DT_GRD01 = df_Transaction(10, gParam);
                if (DT_GRD01.Tables.Count>0)
                {
                    grd_Data01.DataSource = DT_GRD01.Tables[0];
                }
                grd_Data01.Focus();
                gParam = null;
                grd_Data02.DataSource = null;

                if (gview_Data01.RowCount > 0)
                {
                    Filter[1] = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["CODE"].ToString();
                    DT_GRD03 = null;
                    DT_GRD03 = df_Transaction(11, null);
                    if (DT_GRD03 != null)
                    {
                        grd_Data02.DataSource = DT_GRD03.Tables[0];
                    }
                }
                else
                { 
                
                }
            }
            gParam = null;
            grd_Data03.DataSource = null;
            //if (gview_Data02.RowCount > 0)
            //{
                Filter[2] = "0";
                gParam = new string[] { "업무구분", Filter[1] };
                DT_GRD05 = null;
                DT_GRD05 = df_Transaction(12, gParam);
                if (DT_GRD05 != null)
                {
                    grd_Data03.DataSource = DT_GRD05.Tables[0];
                }
                DT_GRD02 = DT_GRD01;
                DT_GRD04 = DT_GRD03;
            //}
        }

        private void grd_Data01_Click(object sender, EventArgs e)
        {
            if (gview_Data01.RowCount <= 0)
            {
                grd_Data02.DataSource = null;
                return;
            }
            DataRow DR = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle);
            if (!DR["CODE"].ToString().Equals(""))
            {
                Filter[1] = DR["CODE"].ToString();
            }
            else
            {
                Filter[1] = "알_수_없_는_값";
            }
            object grdbuf = grd_Data02.DataSource;
            grd_Data02.DataSource = null;
            grd_Data02.DataSource = grdbuf;

            gParam = null;
            grd_Data03.DataSource = null;
            Filter[2] = "0";
            gParam = new string[] { "업무구분", Filter[1] };
            DT_GRD05 = null;
            DT_GRD05 = df_Transaction(12, gParam);
            if (DT_GRD05 != null)
            {
                grd_Data03.DataSource = DT_GRD05.Tables[0];
            }
        }

        /// <summary>
        /// 선택한 프로그램목록을 추가한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PRG_ADD_Click(object sender, EventArgs e)
        {
            if(gview_Data03.RowCount <= 0) return;
            int[] Rows = gview_Data03.GetSelectedRows();
            if (!MsgBox.MsgQuestion("선택한 " + (Rows.Length > 1 ? "영역의 프로그램들" : "프로그램(" +
                                    gview_Data03.GetDataRow(gview_Data03.FocusedRowHandle)["P_NAME"] + ")") +
                                    "을 추가하시겠습니까? \n 추가프내용이 DB에 반영됩니다.", "프로그램추가")) return;
            DataRow DR;
            for (int count = Rows.Length - 1; count >= 0; count--)
            {
                gview_Data02.AddNewRow();
                DR = gview_Data02.GetDataRow(gview_Data02.FocusedRowHandle);
                if (DR == null)
                {
                    string[] Params1 = new string[]{ 
                                                    "USR",
                                                    "USR_NAME",
                                                    "P_ID",
                                                    "P_NAME",
                                                    "SYSTEM",
                                                    "AUTH_NEW",
                                                    "AUTH_DELETE",
                                                    "AUTH_SAVE",
                                                    "AUTH_PRINT",
                                                    "AUTH_EXCEL"
                                                    };
                    object[] Params2 = new object[]{ 
                                                    gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["CODE"],
                                                    gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["NAME"],
                                                    gview_Data03.GetDataRow(Rows[count])["P_ID"],
                                                    gview_Data03.GetDataRow(Rows[count])["P_NAME"],
                                                    gview_Data03.GetDataRow(Rows[count])["SYSTEM"],
                                                    "0",
                                                    "0",
                                                    "0",
                                                    "0",
                                                    "0"
                                                    };

                    DT_GRD03 = null;
                    DataSet NewDS = new DataSet();
                    grd_Data02.DataSource = null;
                    NewDS.Tables.Add("Main");
                    DT_GRD03 = NewDS;
                    for(int col = 0; col < Params1.Length; col++)
                    {
                        DT_GRD03.Tables["Main"].Columns.Add(Params1[col]);
                    }

                    DT_GRD03.Tables["Main"].Rows.Add(
                                                    gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["CODE"],
                                                    gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["NAME"],
                                                    gview_Data03.GetDataRow(Rows[count])["P_ID"],
                                                    gview_Data03.GetDataRow(Rows[count])["P_NAME"],
                                                    gview_Data03.GetDataRow(Rows[count])["SYSTEM"],
                                                    "0",
                                                    "0",
                                                    "0",
                                                    "0",
                                                    "0"
                                                    );
                    grd_Data02.DataSource = null;
                    grd_Data02.DataSource = DT_GRD03.Tables["Main"];
                }
                else
                {
                    DR["USR"] = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["CODE"];
                    DR["USR_NAME"] = gview_Data01.GetDataRow(gview_Data01.FocusedRowHandle)["NAME"];
                    DR["P_ID"] = gview_Data03.GetDataRow(Rows[count])["P_ID"];
                    DR["P_NAME"] = gview_Data03.GetDataRow(Rows[count])["P_NAME"];
                    DR["SYSTEM"] = gview_Data03.GetDataRow(Rows[count])["SYSTEM"];
                    DR["AUTH_NEW"] = "0";
                    DR["AUTH_DELETE"] = "0";
                    DR["AUTH_SAVE"] = "0";
                    DR["AUTH_PRINT"] = "0";
                    DR["AUTH_EXCEL"] = "0";
                    DT_GRD03.Tables[0].Rows.Add(DR);
                    grd_Data02.DataSource = null;
                    grd_Data02.DataSource = DT_GRD03.Tables[0];
                }
                gview_Data03.FocusedRowHandle = gview_Data03.RowCount - 1;
                gview_Data03.DeleteRow(Rows[count]);
            }
            btn_Save_Click(null, null);
            grd_Data01_Click(null, null);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (gview_Data02.RowCount <= 0) return;
            int[] Rows = gview_Data02.GetSelectedRows();
            if (!MsgBox.MsgQuestion("선택한 " + (Rows.Length > 1 ? "영역의 프로그램들" : "프로그램(" + 
                                    gview_Data02.GetDataRow(gview_Data02.FocusedRowHandle)["P_NAME"] + ")") + 
                                    "을 삭재하시겠습니까? \n 삭제내용이 DB에 반영됩니다.", "삭제처리")) return;

            for (int count = Rows.Length-1 ; count >= 0 ; count--)
            {
                gview_Data02.DeleteRow(Rows[count]);
            
            }
            btn_Save_Click(null, null);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;
            if (DT_GRD03.HasChanges())
            {
                ds_new = DT_GRD03.GetChanges();
                foreach (DataRow dr in ds_new.Tables[0].Rows)
                {
                    switch (dr.RowState.ToString().Trim())
                    {
                        case "Added":
                            {
                                gParam = new string[] { "I" };
                                gOut_MSG = new string[] { "", "" };
                                df_Transaction(20, gParam, dr, ref gOut_MSG);
                            }
                            break;
                        case "Modified":
                            {
                                gParam = new string[] { "U" };
                                gOut_MSG = new string[] { "", "" };
                                df_Transaction(20, gParam, dr, ref gOut_MSG);
                            }
                            break;
                        case "Deleted":
                            {
                                gParam = new string[] { "D" };
                                gOut_MSG = new string[] { "", "" };
                                df_Transaction(20, gParam, dr, ref gOut_MSG);
                            }
                            break;
                    }
                }
                MsgBox.MsgInformation("저장 완료", "확인");
                grd_Data01_Click(null, null);
                int focusgrid = gview_Data01.FocusedRowHandle;
                btn_Search_Click(null, null);
                gview_Data01.FocusedRowHandle = focusgrid;
                return;
            }

        }

        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////
        private void Form_Load(object sender, EventArgs e)
        {
            //환경값 로딩
            env = new _Environment();
            //버튼사용권한조회 및 설정
            AutoritySet();
            //화면초기회
             Init_SCR();
             this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_closing);
             //전체내용 조회
             btn_Search_Click(null, null);

        }

        /// <summary>
        /// 폼이 닫힐때 발생하는 이벤트처리.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (DT_GRD02.HasChanges() || DT_GRD04.HasChanges() || DT_GRD06.HasChanges())
                {
                    if (MsgBox.MsgQuestion("저장되지 않은 데이터가 있습니다. \n 저장 (OK) / 무시 (Cancel)", "알림"))
                    {
                        btn_Save_Click(null, null);
                        this.Dispose();
                    }
                    else
                    {
                        DT_GRD01 = null;
                        DT_GRD02 = null;
                        DT_GRD03 = null;
                        DT_GRD04 = null;
                        DT_GRD05 = null;
                        DT_GRD06 = null;
                        this.Dispose();
                    }
                }
            }
            catch { }
        }

        private void ledt_Item02_EditValueChanged(object sender, EventArgs e)
        {
            //해당 사용자로 포커스 이동.
            if (gview_Data01.RowCount <= 0) return;
            string chkstring = ((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue.ToString();
            DataRow DR;
            for (int rows = 0; rows < gview_Data01.RowCount; rows++)
            {
                DR = gview_Data01.GetDataRow(rows);
                if (DR["CODE"].ToString().IndexOf(chkstring) >= 0 && chkstring != "")
                {
                    gview_Data01.FocusedRowHandle = rows;
                    break;
                }
            }

        }

        private void gview_Data01_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;
            //위/아래이동중
            grd_Data01_Click(null, null);
        }

        /// 해당 컬럼조건에 만족하는 필터링을 수행한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gview_Data01_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //    if (gview_Data01.FocusedRowHandle < 0) return;
            DataGridView view = sender as DataGridView;
            DataView dv = gview_Data01.DataSource as DataView;
            try
            {
                if (dv[e.ListSourceRow]["P_MENU"].ToString().IndexOf("eWMES") != 0)
                {
                    // 현재 행을 표시합니다.
                    e.Visible = false;
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 해당 컬럼조건에 만족하는 필터링을 수행한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gview_Data02_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            DataView dv = gview_Data02.DataSource as DataView;
            try
            {
                if (dv[e.ListSourceRow]["USR"].ToString().IndexOf(Filter[1]) != 0)
                {
                    // 현재 행을 표시합니다.
                    e.Visible = false;
                    e.Handled = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
        
        private void gview_Data02_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                DataGridView view = sender as DataGridView;
                DataView dv = gview_Data02.DataSource as DataView;

                if (dv[e.RowHandle]["ACC_NEW"].ToString().Equals("Y") && e.Column.FieldName.Equals("AUTH_NEW"))
                {
                    e.Appearance.BackColor = Color.PeachPuff;
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
                else if (dv[e.RowHandle]["ACC_DELETE"].ToString().Equals("Y") && e.Column.FieldName.Equals("AUTH_DELETE"))
                {
                    e.Appearance.BackColor = Color.PeachPuff;
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
                else if (dv[e.RowHandle]["ACC_SAVE"].ToString().Equals("Y") && e.Column.FieldName.Equals("AUTH_SAVE"))
                {
                    e.Appearance.BackColor = Color.PeachPuff;
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
                else if (dv[e.RowHandle]["ACC_PRINT"].ToString().Equals("Y") && e.Column.FieldName.Equals("AUTH_PRINT"))
                {
                    e.Appearance.BackColor = Color.PeachPuff;
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
                else if (dv[e.RowHandle]["ACC_EXCEL"].ToString().Equals("Y") && e.Column.FieldName.Equals("AUTH_EXCEL"))
                {
                    e.Appearance.BackColor = Color.PeachPuff;
                    e.Column.OptionsColumn.AllowEdit = true;
                    e.Column.OptionsColumn.AllowFocus = true;
                }
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr(ex.Message,"");
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
            // Index 10 ~ 19 : 일반 조회관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            DataSet DT = null;
            string Errchk;
            gConst.DbConn.ClearDB();
            switch (Index)
            {
                case 1:
                    {
                        string Query = "SELECT '%' CODE, '전체' NAME UNION ALL  SELECT DEPT AS CODE, DEPT_NAME AS NAME FROM TS_DEPT";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 10:
                    {

                        #region 사용자 조회
                        string Query = "SELECT  RTRIM(A.USR) CODE,        " +
                                       "        RTRIM(A.UNAM) NAME,       " +
                                       "        RTRIM(DEPT_NAME) DEPT_NAME " +
                                       "FROM        TS_USER A WITH(NOLOCK)" +
                                       "JOIN   TS_DEPT B WITH(NOLOCK)" +
                                       "ON A.DEPT = B.DEPT                " +
                                       "AND A.COMP = B.COMP               " +
                                       "AND A.FACT = B.FACT               " +
                                       "AND A.USEYN = 1              " +
                                       "WHERE A.DEPT LIKE @DEPT " +
                                       "AND A.USR LIKE @USR + '%' " +
                                       "AND A.UNAM LIKE '%' + @UNAM + '%' " +
                                       "GROUP BY        A.USR, A.UNAM, DEPT_NAME " +
                                       "ORDER BY A.USR ASC";
                        gConst.DbConn.AddParameter(new SqlParameter("@DEPT", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@USR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@UNAM", Param[2]));
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                        #endregion
                    }
                    break;
                case 11: 
                    {
                        #region 상단 그리드 조회
                        string Query = "    SELECT A.USR ,                       " +
                                       "            C.UNAM AS USR_NAME,          " +
                                       "            A.P_ID,                      " +
                                       "            A.P_NAME,                    " +
                                       "            A.SYSTEM,                    " +
                                       "            A.AUTH_NEW,                  " +
                                       "            A.AUTH_DELETE,               " +
                                       "            A.AUTH_SAVE,                 " +
                                       "            A.AUTH_PRINT,                " +
                                       "            A.AUTH_EXCEL,                " +
                                       "            B.AUTH_NEW ACC_NEW,          " +
                                       "            B.AUTH_DELETE ACC_DELETE,    " +
                                       "            B.AUTH_SAVE ACC_SAVE,        " +
                                       "            B.AUTH_PRINT ACC_PRINT,      " +
                                       "            B.AUTH_EXCEL ACC_EXCEL       " +
                                       "    FROM TS_access A WITH(NOLOCK)        " +
                                       "    INNER JOIN  TS_PROGRAM B WITH(NOLOCK)" +
                                       "    ON A.P_ID = B.P_ID                   " +
                                       "    JOIN TS_USER C WITH(NOLOCK)          " +
                                       "    ON A.USR = C.USR                     " +
                                       "    AND C.COMP = '" + env.Company + "'   " +
                                       "    AND C.FACT = '" + env.Factory + "'   " +
                                       "    ORDER BY A.USR, A.SYSTEM             ";
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                        #endregion
                    }
                    break;
                case 12: 
                    {
                        #region 하단 그리드 조회
                        string Query = "SELECT A.P_ID,                                               " +
                                       "A.P_NAME,                                                    " +
                                       "A.SYSTEM,                                                    " +
                                       "'0' ACCESS                                                   " +
                                       "FROM       TS_program A WITH(NOLOCK)                         " +
                                       "INNER JOIN TS_menu B WITH(NOLOCK)                            " +
                                       "ON B.C_MENU = A.P_ID                                         " +
                                       "AND B.P_MENU = A.SYSTEM                                      " +
                                       "WHERE A.P_ID NOT IN(SELECT P_ID FROM TS_access WITH(NOLOCK)  " +
                                       "                                                             " +
                                       "                    WHERE usr = '" + gParam[1] + "')         " +
                                       "ORDER BY SYSTEM                                              ";

                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                        #endregion
                    }
                    break;
            }
            return DT;
        }




        /// <summary>
        /// DB Transaction
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        private void df_Transaction(int Index, object[] Param, DataRow row, ref string[] Out_MSG)
        {
            // Index 1 ~  9  : ComboBox관련 등록관련 쿼리 또는 프로시져
            // Index 10 ~ 19 : Select관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            gConst.DbConn.DBClose();
            switch (Index)
            {

                case 20:
                    {
                        #region 입력/수정/삭제
                        if (row.RowState != DataRowState.Deleted)
                        {

                            gConst.DbConn.ProcedureName = "dbo.USP_SYS_SET_TS_ACCESS";
                            gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, Param[0]);
                            gConst.DbConn.AddParameter("USR", MSSQLAgent.DBFieldType.String, row["USR"].ToString());
                            gConst.DbConn.AddParameter("P_ID", MSSQLAgent.DBFieldType.String, row["P_ID"].ToString());
                            gConst.DbConn.AddParameter("P_NAME", MSSQLAgent.DBFieldType.String, row["P_NAME"].ToString());
                            gConst.DbConn.AddParameter("SYSTEM", MSSQLAgent.DBFieldType.String, row["SYSTEM"].ToString());
                            gConst.DbConn.AddParameter("AUTH_NEW", MSSQLAgent.DBFieldType.String, row["AUTH_NEW"].ToString());
                            gConst.DbConn.AddParameter("AUTH_DELETE", MSSQLAgent.DBFieldType.String, row["AUTH_DELETE"].ToString());
                            gConst.DbConn.AddParameter("AUTH_SAVE", MSSQLAgent.DBFieldType.String, row["AUTH_SAVE"].ToString());
                            gConst.DbConn.AddParameter("AUTH_PRINT", MSSQLAgent.DBFieldType.String, row["AUTH_PRINT"].ToString());
                            gConst.DbConn.AddParameter("AUTH_EXCEL", MSSQLAgent.DBFieldType.String, row["AUTH_EXCEL"].ToString());
                            gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                            gConst.DbConn.AddParameter("RESFLAG", MSSQLAgent.DBFieldType.String, Out_MSG[0], 1, MSSQLAgent.DBDirection.Output);
                            gConst.DbConn.AddParameter("RESULT", MSSQLAgent.DBFieldType.String, Out_MSG[1], 100, MSSQLAgent.DBDirection.Output);
                        }
                        else
                        {
                            gConst.DbConn.ProcedureName = "dbo.USP_SYS_SET_TS_ACCESS";
                            gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, Param[0]);
                            gConst.DbConn.AddParameter("USR", MSSQLAgent.DBFieldType.String, row["USR", DataRowVersion.Original].ToString());
                            gConst.DbConn.AddParameter("P_ID", MSSQLAgent.DBFieldType.String, row["P_ID", DataRowVersion.Original].ToString());
                            gConst.DbConn.AddParameter("P_NAME", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("SYSTEM", MSSQLAgent.DBFieldType.String, row["SYSTEM", DataRowVersion.Original].ToString());
                            gConst.DbConn.AddParameter("AUTH_NEW", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("AUTH_DELETE", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("AUTH_SAVE", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("AUTH_PRINT", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("AUTH_EXCEL", MSSQLAgent.DBFieldType.String, null);
                            gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                            gConst.DbConn.AddParameter("RESFLAG", MSSQLAgent.DBFieldType.String, Out_MSG[0], 1, MSSQLAgent.DBDirection.Output);
                            gConst.DbConn.AddParameter("RESULT", MSSQLAgent.DBFieldType.String, Out_MSG[1], 100, MSSQLAgent.DBDirection.Output);

                        }
                        gConst.DbConn.ExecuteNonQuery(out Out_MSG[0], out Out_MSG[1]);
                        if (!Out_MSG[0].Equals("Y"))
                        {
                            MsgBox.MsgErr("다음 사유로 인하여 처리되지 않았습니다.\n 코드(" + row["P_ID"].ToString() + ") \n" + Out_MSG[1], "저장오류");
                        }
                        #endregion
                    }
                    break;
            }
            gConst.DbConn.DBClose();
        }

        #endregion 

        private void gview_Data01_CustomRowFilter_1(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            DataGridView view = sender as DataGridView;
            DataView dv = gview_Data01.DataSource as DataView;
            try
            {
                if (dv[e.ListSourceRow]["CODE"].ToString().ToUpper().IndexOf("SUSER") == 0)
                {
                    // 현재 행을 표시합니다.
                    if (!env.EmpCode.ToUpper().Equals("SUSER"))
                    {
                        e.Visible = false;
                        e.Handled = true;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr(ex.Message,"");
            }
        }

    }
}