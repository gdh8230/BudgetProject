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

namespace BASE
{
    public partial class BASE03 : frmSub_Baseform_Search_STD
    {
        public BASE03()
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
        private void USER_Load(object sender, EventArgs e)
        {
            AutoritySet();

               env = new _Environment();
            string error_msg = string.Empty;

            DataSet ds;

            //부서 lookup
            ds = df_select(3, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_DEPT, ds.Tables[0], "NAME", "CODE");
                ledt_DEPT.ItemIndex = 0;
            }
            //프로젝트 상태 lookup
            ds = df_select(1, null, out error_msg);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                modUTIL.DevLookUpEditorSet(ledt_get_PJT_STAT, ds.Tables[0], "NAME", "CODE");
                ledt_get_PJT_STAT.ItemIndex = 0;
                DataSet ds2 = new DataSet();
                ds2 = ds.Copy();
                ds2.Tables[0].Rows[0].Delete();
                modUTIL.DevLookUpEditorSet(ledt_set_PJT_STAT, ds2.Tables[0], "NAME", "CODE");
                ledt_set_PJT_STAT.ItemIndex = 0;
            }

            getData();
        }

        private void getData()
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
                DT_GRD02 = DT_GRD01.Copy();
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
                case 0: //프로젝트 조회
                    {
                        string query = "SELECT PJT_CD, PJT_NM, dbo.f_get_STR2DATE(APRV_DT, '-') AS APRV_DT, dbo.f_get_STR2DATE(PJT_SDT, '-') AS PJT_SDT, dbo.f_get_STR2DATE(PJT_EDT, '-') AS PJT_EDT, DEPT, PJT_STAT, EMP, CLIENT, PJT_MONEY, PJT_PLACE" +
                                        " FROM TB_PJT WITH(NOLOCK) WHERE PJT_CD LIKE '%' + @PJD_CD + '%' AND PJT_NM LIKE '%' + @PJT_NM + '%' " +
                                        "AND ISNULL(PJT_STAT,'') LIKE @PJT_STAT AND STAT <> 'D'";
                        gConst.DbConn.AddParameter(new SqlParameter("@PJD_CD", txt_PJT_CD.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_NM", txt_PJT_NM2.Text));
                        gConst.DbConn.AddParameter(new SqlParameter("@PJT_STAT", ledt_get_PJT_STAT.EditValue.ToString()));
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //프로젝트상태 코드
                    {
                        string query = "SELECT '%' CODE, '전체' NAME UNION ALL  SELECT CODE, NAME FROM TS_CODE WITH(NOLOCK) WHERE C_ID = '프로젝트상태'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 3: //부서조회
                    {
                        string query = "SELECT DEPT AS CODE, DEPT_NAME AS NAME FROM TS_DEPT WITH(NOLOCK)";
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
                    gConst.DbConn.AddParameter("APRV_DT", MSSQLAgent.DBFieldType.String, dr["APRV_DT"].ToString().Equals("") ? null : DateTime.Parse(dr["APRV_DT"].ToString()).ToString("yyyyMMdd"));
                    gConst.DbConn.AddParameter("PJT_SDT", MSSQLAgent.DBFieldType.String, dr["PJT_SDT"].ToString().Equals("") ? null : DateTime.Parse(dr["PJT_SDT"].ToString()).ToString("yyyyMMdd"));
                    gConst.DbConn.AddParameter("PJT_EDT", MSSQLAgent.DBFieldType.String, dr["PJT_EDT"].ToString().Equals("") ? null : DateTime.Parse(dr["PJT_EDT"].ToString()).ToString("yyyyMMdd"));
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
                MsgBox.MsgErr(ee.ToString() + error_msg, "에러");
                gConst.DbConn.Rollback();
            }

            gConst.DbConn.Commit();
            return result;
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Code_Set frm = new Code_Set(env, "프로젝트");
            if(frm.ShowDialog() == DialogResult.OK)
            {
                EditorReset();

                DataRow DR;
                gridView1.AddNewRow();
                DR = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                DR["PJT_CD"] = frm.CODE;
                DR["PJT_NM"] = frm.NAME;
                txt_PJT_NM.EditValue = frm.NAME;
                DT_GRD01.Tables[0].Rows.Add(DR);
                gridControl1.DataSource = null;
                gridControl1.DataSource = DT_GRD01.Tables[0];
                gridView1.FocusedRowHandle = gridView1.RowCount - 1;
            }

        }

        private void EditorReset()
        {
            txt_PJT_NM.EditValue = null;
            dt_APRV.EditValue = null;
            dedt_SDATE.EditValue = null;
            dedt_EDATE.EditValue = null;
            ledt_DEPT.EditValue = null;
            ledt_set_PJT_STAT.EditValue = null;
            txt_EMP.EditValue = null;
            txt_CLIENT.EditValue = null;
            txt_PJT_MONEY.EditValue = null;
            txt_PJT_PLACE.EditValue = null;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            select_row = gridView1.GetFocusedDataRow();
            if (select_row != null)
            {
                txt_PJT_NM.EditValue = select_row["PJT_NM"];
                dt_APRV.EditValue = select_row["APRV_DT"];
                dedt_SDATE.EditValue = select_row["PJT_SDT"];
                dedt_EDATE.EditValue = select_row["PJT_EDT"];
                ledt_DEPT.EditValue = select_row["DEPT"];
                ledt_set_PJT_STAT.EditValue = select_row["PJT_STAT"];
                txt_EMP.EditValue = select_row["EMP"];
                txt_CLIENT.EditValue = select_row["CLIENT"];
                txt_PJT_MONEY.EditValue = select_row["PJT_MONEY"];
                txt_PJT_PLACE.EditValue = select_row["PJT_PLACE"];
            }
        }

        private void textEdit_Leave(object sender, EventArgs e)
        {
            TextEdit textEdit = sender as TextEdit;
            if (textEdit.EditValue.Equals("") && textEdit.Tag.Equals("PJT_MONEY"))
            {
                textEdit.EditValue = 0;
            }
            if (!select_row[textEdit.Tag.ToString()].Equals(textEdit.EditValue))
            {
                if (textEdit.Tag.ToString().Equals("PJT_MONEY")){
                    textEdit.EditValue = 0;
                }
                select_row[textEdit.Tag.ToString()] = textEdit.EditValue;
            }

        }

        private void loolupEdit_Leave(object sender, EventArgs e)
        {
            LookUpEdit lookupEdit = sender as LookUpEdit;
            if (!select_row[lookupEdit.Tag.ToString()].Equals(lookupEdit.EditValue))
            {
                select_row[lookupEdit.Tag.ToString()] = lookupEdit.EditValue;
            }
        }
        private void dateEdit_Leave(object sender, EventArgs e)
        {
            DateEdit dateEdit = sender as DateEdit;
            if(dateEdit.EditValue == null)
            {
                select_row[dateEdit.Tag.ToString()] = "";
            }
            else if (!select_row[dateEdit.Tag.ToString()].Equals(dateEdit.EditValue))
            {
                select_row[dateEdit.Tag.ToString()] = DateTime.Parse(dateEdit.EditValue.ToString()).ToString("yyyy-MM-dd");
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

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
    }
}
