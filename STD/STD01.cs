using System;
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
using DH_Core;
using DH_Core.CommonPopup;
using DH_Core.DB;

namespace STD
{
    public partial class STD01 : frmSub_Baseform_Search_STD
    {
        public STD01()
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
                modUTIL.DevLookUpEditorSet(ledt_ADMIN_ITEM, ds.Tables[0], "NAME", "CODE");
                ledt_ADMIN_ITEM.ItemIndex = 0;
            }

            gridView2.OptionsView.ShowGroupPanel = false;

            getACCOUNT();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DT_GRD02 = null;
                gridControl2.DataSource = null;
                DataRow dr = gridView1.GetFocusedDataRow();
                gParam = new string[] { dr["ACT_CD"].ToString() };
                DT_GRD02 = df_select(1, gParam, out error_msg);
                if (DT_GRD02 == null)
                {
                    MsgBox.MsgErr("프로젝트 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }

                gridControl2.DataSource = DT_GRD02.Tables[0];
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
                case 0: //회계계정 조회
                    {
                        string query = "SELECT ACT_CD, ACT_NM FROM TB_ACCOUNT WITH(NOLOCK) WHERE ACT_CD LIKE '%' + @ACT_CD + '%' AND ACT_NM LIKE '%' + @ACT_NM + '%' AND CTRL_YN = 'Y' ";
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", txt_act_cd.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_NM", txt_act_nm.Text));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //전기 예산 신청 내역
                    {
                        gConst.DbConn.ProcedureName = "USP_STD_GET_BUDGET_REQ";
                        gConst.DbConn.AddParameter(new SqlParameter("@YEAR", dt_YEAR.EditValue));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_GBN", "0"));
                        gConst.DbConn.AddParameter(new SqlParameter("@ADMIN_ITEM", ledt_ADMIN_ITEM.EditValue.ToString()));
                        gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", Param[0]));
                        dt = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 3: //부서조회
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

                    gConst.DbConn.ProcedureName = "dbo.USP_BASE_SET_PJT";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, dr.RowState.Equals(DataRowState.Added) ? "I" : "U" );
                    gConst.DbConn.AddParameter("PJT_CD", MSSQLAgent.DBFieldType.String, dr["PJT_CD"].ToString());
                    gConst.DbConn.AddParameter("PJT_NM", MSSQLAgent.DBFieldType.String, dr["PJT_NM"].ToString());
                    gConst.DbConn.AddParameter("APRV_DT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dr["APRV_DT"].ToString()).ToString("yyyyMMdd"));
                    gConst.DbConn.AddParameter("PJT_SDT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dr["PJT_SDT"].ToString()).ToString("yyyyMMdd"));
                    gConst.DbConn.AddParameter("PJT_EDT", MSSQLAgent.DBFieldType.String, DateTime.Parse(dr["PJT_EDT"].ToString()).ToString("yyyyMMdd"));
                    gConst.DbConn.AddParameter("PJT_STAT", MSSQLAgent.DBFieldType.String, dr["PJT_STAT"].ToString());
                    gConst.DbConn.AddParameter("DEPT", MSSQLAgent.DBFieldType.String, dr["DEPT"].ToString());
                    gConst.DbConn.AddParameter("EMP", MSSQLAgent.DBFieldType.String, dr["EMP"].ToString());
                    gConst.DbConn.AddParameter("CLIENT", MSSQLAgent.DBFieldType.String, dr["CLIENT"].ToString());
                    gConst.DbConn.AddParameter("PJT_MONEY", MSSQLAgent.DBFieldType.String, dr["PJT_MONEY"].ToString());
                    gConst.DbConn.AddParameter("PJT_PLACE", MSSQLAgent.DBFieldType.String, dr["PJT_PLACE"].ToString());
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);
                }
                else
                {
                    gConst.DbConn.ProcedureName = "dbo.USP_BASE_SET_PJT";
                    gConst.DbConn.AddParameter("ACCTYPE", MSSQLAgent.DBFieldType.String, "D");
                    gConst.DbConn.AddParameter("PJT_CD", MSSQLAgent.DBFieldType.String, dr["PJT_CD", DataRowVersion.Original].ToString());
                    gConst.DbConn.AddParameter("PJT_NM", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("APRV_DT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PJT_SDT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PJT_EDT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PJT_STAT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("DEPT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("EMP", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("CLIENT", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PJT_MONEY", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("PJT_PLACE", MSSQLAgent.DBFieldType.String, null);
                    gConst.DbConn.AddParameter("MODIFY_ID", MSSQLAgent.DBFieldType.String, env.EmpCode);

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
            getACCOUNT();
        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            DataSet ds_new;
            if (DT_GRD01.HasChanges())
            {
                ds_new = DT_GRD01.GetChanges();
                foreach (DataRow dr in ds_new.Tables[0].Rows)
                {
                    df_Transaction(20, null, dr, out gOut_MSG);
                }
                MsgBox.MsgInformation("저장 완료", "확인");
                btn_Search_Click(null, null);
                return;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
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
    }
}
