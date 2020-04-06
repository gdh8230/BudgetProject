using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


namespace DH_Core.CommonPopup
{
    public partial class frm_PUP_CMD_CODE_Set : frmSub_Baseform_System_NullPanel
    {
        public frm_PUP_CMD_CODE_Set(_Environment _env, object[] _param)
        {
            InitializeComponent();
            env = _env;
            // 0: Title, 1: C_ID
            gParam2 = _param;
            DataDisplay();
            InitSCR();
        }
        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //환경변수 선언
        _Environment env;
        string[] gParam;
        object[] gParam2;
        string[] gOut_MSG = null;

        DataSet DT_GRD01 = null;
        DataSet DT_GRD02 = null;
        //string[] gParam = null;
        string gCODE = null;
        string gNAME = null;
        string gFNAM = null;
        //속성
        public string CODE { get { try { return gCODE; } catch { return ""; } } }
        public string NAME { get { try { return gNAME; } catch { return ""; } } }
        public string DETAIL { get { try { return gFNAM; } catch { return ""; } } }

        #endregion

        #region Funcitons

        private void DataDisplay()
        {
            int FocusedRow = gvCode.FocusedRowHandle;
            gcCode.DataSource = null;
            DT_GRD01 = df_Transaction(10, new string[] { gParam2[1].ToString() }); // C_ID
            if (DT_GRD01 != null && DT_GRD01.Tables.Count > 0)
            {
                gcCode.DataSource = DT_GRD01.Tables[0];
                gvCode.FocusedRowHandle = FocusedRow;
            }
            DT_GRD02 = DT_GRD01;
        }

        private void InitSCR()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            // 시스템관리자인지 확인
            DataSet DS = df_Transaction(0, null);
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                this.Text = gParam2[0].ToString() + " 코드 등록/조회";
                btn_Add.Enabled = true;
                btn_Delete.Enabled = true;
                btn_Save.Enabled = true;
                gvCode.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                this.Text = gParam2[0].ToString() + " 코드 조회";
                btn_Add.Enabled = false;
                btn_Delete.Enabled = false;
                btn_Save.Enabled = false;
                gvCode.OptionsBehavior.ReadOnly = true;
            }
        }

        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////

        /// <summary>
        /// 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Code_Set _frm = new Code_Set(env, gParam2[0].ToString());
            if (_frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 중복 체크
                for (int i = 0; i < DT_GRD01.Tables[0].Rows.Count; i++)
                {
                    if (_frm.CODE.Equals(DT_GRD01.Tables[0].Rows[i]["CODE"].ToString()))
                    {
                        MsgBox.MsgErr("중복된 코드가 존재합니다.", "확인");
                        return;
                    }
                }

                DataRow DR;
                gvCode.AddNewRow();
                DR = gvCode.GetDataRow(gvCode.FocusedRowHandle);
                DT_GRD01.Tables[0].Rows.Add(DR);
                DR["C_ID"] = gParam2[1];
                DR["CODE"] = _frm.CODE;
                DR["NAME"] = _frm.NAME;
                gcCode.DataSource = null;
                gcCode.DataSource = DT_GRD01.Tables[0];
                gvCode.FocusedRowHandle = gvCode.RowCount - 1;
            }
        }

        /// <summary>
        /// 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            gvCode.DeleteRow(gvCode.FocusedRowHandle);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            gvCode.UpdateCurrentRow();
            gvCode.PostEditor();
            if (!DT_GRD02.HasChanges()) return;
            DataSet ds_new = DT_GRD02.GetChanges();
            foreach (DataRow dr in ds_new.Tables[0].Rows)
            {
                switch (dr.RowState.ToString().Trim())
                {
                    case "Added":
                        {
                            gParam = new string[] { "I", null };
                            gOut_MSG = new string[] { "", "" };
                            string Result = df_Transaction(20, gParam, dr, ref gOut_MSG);
                            if (!Result.Equals(""))
                            {
                                MsgBox.MsgInformation("다음사유로저장되지 않았습니다. \n" + Result, "확인");
                                return;
                            }
                        }
                        break;
                    case "Modified":
                        {
                            gParam = new string[] { "U", null };
                            gOut_MSG = new string[] { "", "" };
                            string Result = df_Transaction(20, gParam, dr, ref gOut_MSG);
                            if (!Result.Equals(""))
                            {
                                MsgBox.MsgInformation("다음사유로저장되지 않았습니다. \n" + Result, "확인");
                                return;
                            }
                        }
                        break;
                    case "Deleted":
                        {
                            gParam = new string[] { "D", null };
                            gOut_MSG = new string[] { "", "" };
                            string Result = df_Transaction(20, gParam, dr, ref gOut_MSG);
                            if (!Result.Equals(""))
                            {
                                MsgBox.MsgInformation("다음사유로저장되지 않았습니다. \n" + Result, "확인");
                                return;
                            }
                        }
                        break;
                }

            }
            MsgBox.MsgInformation("저장 완료", "확인");
            DT_GRD01 = null;
            DT_GRD02 = null;
            DataDisplay();
        }

        /// <summary>
        /// 화면을 닫습니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (btn_Save.Enabled && DT_GRD01 != null && DT_GRD01.HasChanges())
            {
                if (MsgBox.MsgQuestion("저장되지 않은 데이터가 있습니다. 저장하시겠습니까?", "확인"))
                {
                    btn_Save_Click(null, null);
                }
            }
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
            if (btn_Save.Enabled && DT_GRD01 != null && DT_GRD01.HasChanges())
            {
                MsgBox.MsgErr("데이터 저장 후 반영이 가능합니다. 저장 후 진행해주세요.", "확인");
                return;
            }

            DataRow DR = gvCode.GetDataRow(gvCode.FocusedRowHandle);
            gCODE = DR["CODE"].ToString();
            gNAME = DR["NAME"].ToString();
            gFNAM = DR["FNAM"].ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void grd_Data01_DoubleClick(object sender, EventArgs e)
        {
            btn_Check_Click(null, null);
        }

        private void tedt_TEXT_DoubleClick(object sender, EventArgs e)
        {
            btn_Check_Click(null, null);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            //그리드와 Text박스를 비고하여 Rowfocus를 찾는다.
            string chkstring = txtItem.Text;
            string TextBoxPosition = radio01.SelectedIndex.ToString();
            DataTable DT = (DataTable)gcCode.DataSource;
            DataRow DR;
            for (int rows = 0; rows < gvCode.RowCount; rows++)
            {
                DR = DT.Rows[rows];
                if (TextBoxPosition.Equals("0"))
                {
                    if (DR["CODE"].ToString().IndexOf(chkstring) >= 0)
                    {
                        gvCode.FocusedRowHandle = rows;
                        break;
                    }
                }
                else if (TextBoxPosition.Equals("1"))
                {
                    if (DR["NAME"].ToString().IndexOf(chkstring) >= 0)
                    {
                        gvCode.FocusedRowHandle = rows;
                        break;
                    }
                }
            }
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
            int RCheck = ((DevExpress.XtraEditors.RadioGroup)(sender)).SelectedIndex;
            if (RCheck.Equals(0))
            {
                txtItem.Enabled = true;
                txtItemName.Enabled = false;
            }
            else
            {
                txtItem.Enabled = false;
                txtItemName.Enabled = true;
            }
        }


        private void txt_Item01_EditValueChanged(object sender, EventArgs e)
        {

        }


        private void gview_Data01_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gvCode.RowCount.Equals(0)) return;
            try
            {
                DataRow DR = gvCode.GetDataRow(gvCode.FocusedRowHandle);

                txtItem.Text = DR["CODE"].ToString();
                txtItemName.Text = DR["NAME"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void tedt_CODE_Leave(object sender, EventArgs e)
        {
            // 중복 체크
            DataRow DR = gvCode.GetFocusedDataRow();
            for (int i = 0; i < DT_GRD01.Tables[0].Rows.Count; i++)
            {
                if (i == gvCode.FocusedRowHandle) continue;

                if (DR["CODE"].ToString().Equals(DT_GRD01.Tables[0].Rows[i]["CODE"].ToString()))
                {
                    // 중복 발생
                    MsgBox.MsgErr("동일한 코드가 존재합니다.", "확인");
                    return;
                }
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
                case 0: // 시스템 관리자 여부 체크
                    {
                        string Query = "SELECT  USR " +
                                       "FROM    TS_USER WITH(NOLOCK) " +
                                       "WHERE   COMP = '" + env.Company + "' " +
                                       "AND     FACT = '" + env.Factory + "' " +
                                       "AND     USR = '" + env.EmpCode + "' " +
                                       "AND     GRUP LIKE 'A%' " +
                                       "AND     STAT <> 'D' ";

                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 10: //기준정보의 공통코드 정보를 조회
                    {
                        string Query = "SELECT  RTRIM(CODE) CODE, " +
                                       "        RTRIM(NAME) NAME, " +
                                       "        RTRIM(FNAM) FNAM, " +
                                       "        C_ID, " +
                                       "        NOTE " +
                                       "FROM    TB_CODE WITH(NOLOCK) " +
                                       "WHERE   COMP = '" + env.Company + "' " +
                                       "AND     FACT = '" + env.Factory + "' " +
                                       "AND     C_ID = '" + Param[0] + "' " +
                                       "AND     STAT <> 'D' " +
                                       "ORDER BY CODE ASC ";
                        DT = gConst.DbConn.GetDataSetQuery(Query, out Errchk);
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
        private string df_Transaction(int Index, object[] Param, DataRow row, ref string[] Out_MSG)
        {
            // Index 1 ~  9  : ComboBox관련 등록관련 쿼리 또는 프로시져
            // Index 10 ~ 19 : Select관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            gConst.DbConn.ClearDB();
            switch (Index)
            {

                case 20://그리드상의 콤보데이터 표시용
                    {
                        gConst.DbConn.ProcedureName = "dbo.USP_COM_SET_TB_CODE";
                        if (row.RowState != DataRowState.Deleted)
                        {

                            gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                            gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                            gConst.DbConn.AddParameter(new SqlParameter("@ACCTYPE", Param[0]));
                            gConst.DbConn.AddParameter(new SqlParameter("@C_ID", row["C_ID"].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@CODE", row["CODE"].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@NAME", row["NAME"].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@FNAM", row["FNAM"].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@NOTE", row["NOTE"].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@M_ID", env.Company));
                        }
                        else
                        {
                            gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                            gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                            gConst.DbConn.AddParameter(new SqlParameter("@ACCTYPE", Param[0]));
                            gConst.DbConn.AddParameter(new SqlParameter("@C_ID", row["C_ID", DataRowVersion.Original].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@CODE", row["CODE", DataRowVersion.Original].ToString()));
                            gConst.DbConn.AddParameter(new SqlParameter("@NAME", null));
                            gConst.DbConn.AddParameter(new SqlParameter("@FNAM", null));
                            gConst.DbConn.AddParameter(new SqlParameter("@NOTE", null));
                            gConst.DbConn.AddParameter(new SqlParameter("@M_ID", env.Company));

                        }
                        gConst.DbConn.ExecuteNonQuery(out Out_MSG[0], out Out_MSG[1]);

                        if (!Out_MSG[0].Equals("Y"))
                        {
                            return Out_MSG[1];
                        }
                    }
                    break;
            }
            return "";
        }
        #endregion

    }
}
