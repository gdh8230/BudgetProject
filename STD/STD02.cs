﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DH_Core;
using DH_Core.CommonPopup;
using DH_Core.DB;

namespace STD
{
    public partial class STD02 : frmSub_Baseform_Search_STD
    {
        public STD02()
        {
            InitializeComponent();
        }

        #region Attributes (속성정의 집합)
        _Environment env;
        string[] gParam = null;
        string gOut_MSG = null;
        static DataSet DT_GRD01 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD02 = new DataSet();    //FOR GRID1
        static DataSet DT_GRD03 = new DataSet();    //FOR GIRD2
        static DataSet DT_GRD04 = new DataSet();    //FOR GRID2
        static DataRow select_row;
        string error_msg = "";
        #endregion

        #region Functions 
        /// <summary>
        /// 기능버튼초기화
        /// </summary>
        private void AutoritySet()
        {
            DataRow DR = (DataRow)this.Tag;
            if (DR != null)
            {
                try
                {
                    //pnl_Search.Enabled = DR[0].Equals("Y") ? true : false;
                    pnl_Add.Enabled = DR[1].Equals("Y") ? true : false;
                    pnl_Delete.Enabled = DR[3].Equals("Y") ? true : false;
                    pnl_Save.Enabled = DR[4].Equals("Y") ? true : false;
                    pnl_Print.Enabled = DR[5].Equals("Y") ? true : false;
                    pnl_Excel.Enabled = DR[6].Equals("Y") ? true : false;
                }
                catch (Exception ex)
                {
                    MsgBox.MsgErr("" + ex, "");
                }
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            AutoritySet();

               env = new _Environment();
            string error_msg = string.Empty;

            DataSet ds;

            //관리구분 lookup
            ds = df_select(3, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_ADMIN_GBN, ds.Tables[0], "NAME", "CODE");
                ledt_ADMIN_GBN.ItemIndex = 0;
            }

            //대계정 lookup
            ds = df_select(2, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_CLASS, ds.Tables[0], "NAME", "CODE");
                ledt_CLASS.ItemIndex = 0;
            }

            gridView2.OptionsView.ShowGroupPanel = false;

            getACCOUNT();
        }

        private void getData()
        {
            try
            {
                if (bedt_CODE.Tag == null || bedt_CODE.Tag.Equals(""))
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                DataRow dr = gridView1.GetFocusedDataRow();
                //당기 신청 내역
                DT_GRD02 = null;
                gridControl2.DataSource = null;
                gParam = new string[] { dr["CODE"].ToString(), ((DateTime)dt_YEAR.EditValue).Year.ToString(), "0" };
                DT_GRD02 = df_select(1, gParam, out error_msg);
                if (DT_GRD02 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl2.DataSource = DT_GRD02.Tables[0];

                //당기 편성 내역
                DT_GRD03 = null;
                gridControl3.DataSource = null;
                gParam = new string[] { dr["CODE"].ToString(), ((DateTime)dt_YEAR.EditValue).Year.ToString(), "1" };
                DT_GRD03 = df_select(1, gParam, out error_msg);
                if (DT_GRD03 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl3.DataSource = DT_GRD03.Tables[0];
                //DT_GRD02 = DT_GRD01.Copy();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }
        private void getACCOUNT()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DT_GRD01 = null;
                gridControl1.DataSource = null;
                DT_GRD01 = df_select(0, null, out error_msg);
                if (DT_GRD01 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl1.DataSource = DT_GRD01.Tables[0];
                //DT_GRD02 = DT_GRD01.Copy();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MsgBox.MsgErr("" + ex, "");
            }
        }
        #endregion


        #region DB CRUD(데이터베이스 처리)
        private DataSet df_select(int Index, object[] Param, out string error_msg)
        {
            DataSet dt = null;
            gConst.DbConn.DBClose();
            error_msg = "";
            switch (Index)
            {
                case 0: //대계정 조회 전체미포함
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정' AND CODE LIKE @CODE ";
                        gConst.DbConn.AddParameter(new SqlParameter("@CODE", ledt_CLASS.EditValue));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //예산 신청/편성 내역
                    {
                        gConst.DbConn.ProcedureName = "USP_STD_GET_BUDGET_REQ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_GBN", ledt_ADMIN_GBN.EditValue.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_CD", bedt_CODE.Tag.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_GBN", Param[2]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 2: //대계정 조회 전체포함
                    {
                        string query = "SELECT '%' CODE, '전체' NAME UNION ALL SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '대계정'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 3: //관리구분 조회
                    {
                        string query = "SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '관리구분'";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return dt;
        }

        private bool df_Transaction(int Index, object[] Param, DataRow dr, out string error_msg)
        {

            bool result = false;
            gConst.DbConn.ClearDB();
            error_msg = "";

            gConst.DbConn.BeginTrans();

            try
            {
                #region 입력/수정/삭제
                if (dr.RowState != DataRowState.Deleted)
                {

                    gConst.DbConn.ProcedureName = "dbo.USP_STD_SET_BUDGET";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "I");
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, Param[0]);
                    gConst.DbConn.AddParameter("ACT_GBN", MSSQLAgent.DBFieldType.String, "1"); //편성
                    gConst.DbConn.AddParameter("YEAR", MSSQLAgent.DBFieldType.String, Param[1]);
                    gConst.DbConn.AddParameter("MONTH", MSSQLAgent.DBFieldType.String, dr["MONTH"].ToString());
                    gConst.DbConn.AddParameter("ADMIN_GBN", MSSQLAgent.DBFieldType.String, ledt_ADMIN_GBN.EditValue.ToString());
                    gConst.DbConn.AddParameter("ADMIN_CD", MSSQLAgent.DBFieldType.String, bedt_CODE.Tag.ToString());
                    gConst.DbConn.AddParameter("QUARTER", MSSQLAgent.DBFieldType.String, dr["QUARTER"].ToString());
                    gConst.DbConn.AddParameter("DROWUP_MONEY", MSSQLAgent.DBFieldType.String, dr["MONEY"].ToString().Equals("") ? 0 : Convert.ToDecimal(dr["MONEY"].ToString()));
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                }
                else
                {
                    gConst.DbConn.ProcedureName = "dbo.USP_STD_SET_BUDGET";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "D");
                    gConst.DbConn.AddParameter("ACT_CD", MSSQLAgent.DBFieldType.String, dr["PJT_CD", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("ACT_GBN", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("YEAR", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("MONTH", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ADMIN_GBN", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("ADMIN_CD", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("QUARTER", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("REQ_MONEY", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, null);

                }
                gConst.DbConn.ExecuteNonQuery(out error_msg);
                if (!error_msg.Equals(""))
                {
                    MsgBox.MsgErr("다음 사유로 인하여 처리되지 않았습니다.\n" + error_msg  , "저장오류");
                }
                #endregion
                gConst.DbConn.ClearDB();
            }
            catch(Exception ee)
            {
                Console.WriteLine(ee.ToString());
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
                gConst.DbConn.Rollback();
            }

            gConst.DbConn.Commit();
            return result;
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (bedt_CODE.Tag == null || bedt_CODE.Tag.Equals(""))
            {
                MsgBox.MsgInformation(labelControl8.Tag.ToString() + "를 선택해주세요.", "확인");
                return;
            }
            getACCOUNT();
        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;
            DataRow dr_grid1 = gridView1.GetFocusedDataRow();
            if (DT_GRD03.HasChanges())
            {
                ds_new = DT_GRD03.GetChanges();
                foreach (DataRow dr in ds_new.Tables[0].Rows)
                {
                    gParam = new string[] { dr_grid1["CODE"].ToString(), ((DateTime)dt_YEAR.EditValue).Year.ToString() };
                    df_Transaction(20, gParam, dr, out gOut_MSG);
                }
                MsgBox.MsgInformation("저장 완료", "확인");
                btn_Search_Click(null, null);
                return;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (DT_GRD01 == null) return;
            if (DT_GRD01.HasChanges())
            {
                if (!MsgBox.MsgQuestion("변경사항이 있습니다. 저장하신 후 진행하시겠습니까?", "알림"))
                {
                    return;
                }
                else
                {
                    btn_Save_Click(null, null);
                }
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.DataSource == null) return;
            getData();
        }

        private void gridView2_CustomColumnGroup(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (e.Column == gridColumn4)
            {
                DateTime value1 = (DateTime)e.Value1;
                DateTime value2 = (DateTime)e.Value2;
                //if (GetSeason(value1) == GetSeason(value2)) e.Result = 0;
                //else e.Result = 1;
                e.Handled = true;
            }
        }

        private void gridControl3_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (gridControl3.FocusedView as ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
        }

        private void dt_YEAR_EditValueChanged(object sender, EventArgs e)
        {
            getACCOUNT();
        }

        private void btn_COPY_Click(object sender, EventArgs e)
        {
            DT_GRD03 = DT_GRD02.Copy();
            for(int i = 0; i<DT_GRD03.Tables[0].Rows.Count; i++)
            {
                DT_GRD03.Tables[0].Rows[i].SetAdded();
            }
            gridControl3.DataSource = DT_GRD03.Tables[0];
        }

        private void ledt_ADMIN_GBN_EditValueChanged(object sender, EventArgs e)
        {
            if (ledt_ADMIN_GBN.EditValue.Equals("0"))
            {
                labelControl8.Text = "부      서";
                labelControl8.Tag = "부서";
            }
            else if (ledt_ADMIN_GBN.EditValue.Equals("1"))
            {
                labelControl8.Text = "프로젝트";
                labelControl8.Tag = "프로젝트";
            }
        }

        private void bedt_CODE_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            frm_PUP_GET_CODE frm = new frm_PUP_GET_CODE(env, labelControl8.Tag.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bedt_CODE.Tag = frm.CODE;
                bedt_CODE.Text = frm.NAME;
            }
        }
    }
}
