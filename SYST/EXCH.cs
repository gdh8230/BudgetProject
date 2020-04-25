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
using DH_Core;
using DH_Core.CommonPopup;

namespace SYST
{
    public partial class EXCH : frmSub_Baseform_Search_STD
    {
        public EXCH()
        {
            InitializeComponent();
        }

        #region Attributes (속성정의 집합)
        _Environment env;
        string[] gParam = null;
        string[] gOut_MSG = null;
        string error_msg = "";
        DataSet ds_001;
        DataSet ds_002;
        DataSet ds_003;
        DataSet ds_004;
        DataTable dt_h;
        DataTable dt_d;
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

            getData();
        }

        private void getData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                gridControl1.DataSource = null;
                ds_001 = df_select(0, null, out error_msg);
                if (ds_001 == null)
                {
                    MsgBox.MsgErr("코드 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                    this.Cursor = Cursors.Default;
                    return;
                }
                gridControl1.DataSource = ds_001.Tables[0];
                ds_002 = ds_001;
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
                case 0: //선택월 조회
                    {
                        string query = "select DATEPART(mm, [MONTH]) as [MONTH]  from [MONTH]";
                        dt = gConst.DbConn.GetDataSetQuery(query, out error_msg);
                    }
                    break;
                case 1: //선택월 환율정보 조회
                    {
                        string query = "SELECT* FROM[MONTH] A CROSS JOIN(SELECT CODE, NAME FROM TS_CODE WHERE C_ID = '환종') B LEFT JOIN TB_EXCHANGE C ON  B.CODE = C.EXCH_CD AND C.YEAR = '" + Param[0] + "'  AND DATEPART(m, A.[MONTH]) = C.MONTH WHERE DATEPART(mm, A.[MONTH]) = '" + Param[1] +"'";
                        //string query = "SELECT C_ID, CODE, NAME FROM TS_CODE WITH(NOLOCK)";
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
                if(dr.RowState != DataRowState.Deleted)
                {
                    gConst.DbConn.ProcedureName = "USP_SYS_SET_EXCH";
                    gConst.DbConn.AddParameter(new SqlParameter("@STAT", dr.RowState.Equals(DataRowState.Added) ? "I" : "U" ));
                    gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[0]));
                    gConst.DbConn.AddParameter(new SqlParameter("@MONTH", Param[1]));
                    gConst.DbConn.AddParameter(new SqlParameter("@CODE", dr["CODE"].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@NAME", dr["NAME"].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@EXCH", dr["EXCH_RATE"].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@USER", env.EmpCode));
                }
                else
                {
                    gConst.DbConn.ProcedureName = "USP_SYS_SET_EXCH";
                    gConst.DbConn.AddParameter(new SqlParameter("@STAT", "D"));
                    gConst.DbConn.AddParameter(new SqlParameter("@YEAR", Param[0]));
                    gConst.DbConn.AddParameter(new SqlParameter("@MONTH", Param[1]));
                    gConst.DbConn.AddParameter(new SqlParameter("@CODE", dr["CODE", DataRowVersion.Original].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@NAME", dr["NAME", DataRowVersion.Original].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@EXCH", dr["EXCH_RATE", DataRowVersion.Original].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@USER", env.EmpCode));
                }
                result = gConst.DbConn.ExecuteNonQuery(out error_msg);

                gConst.DbConn.ClearDB();
            }
            catch
            {
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
                gConst.DbConn.Rollback();
                return result;
            }

            gConst.DbConn.Commit();
            return result;
        }

        #endregion

        #region Event
        private void btn_Search_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Code_Set frm = new Code_Set(null, null);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.CODE.Equals("")) return;
                for (int count = 0; count < gridView2.RowCount; count++)
                {
                    if (gridView2.GetDataRow(count)["CODE"].Equals(frm.CODE))
                    {
                        gridView2.FocusedRowHandle = count;
                        MsgBox.MsgErr("동일한 코드가 존재합니다.", "오류");
                        return;
                    }
                }
                DataRow DR_ADD;
                DataRow dr = gridView1.GetFocusedDataRow();

                gridView2.AddNewRow();
                DR_ADD = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                DR_ADD["C_ID"] = dr["C_ID"];
                DR_ADD["CODE"] = frm.CODE;
                DR_ADD["NAME"] = frm.NAME;

                ds_003.Tables[0].Rows.Add(DR_ADD);

            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            gridView2.DeleteRow(gridView2.FocusedRowHandle);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds_new;
                DataRow dr_h = gridView1.GetFocusedDataRow();
                gridView2.UpdateCurrentRow();
                gridView2.PostEditor();
                if (ds_004.HasChanges())
                {
                    ds_new = ds_004.GetChanges();
                    foreach (DataRow dr in ds_new.Tables[0].Rows)
                    {
                        gParam = new string[] { ((DateTime)dt_YEAR.EditValue).Year.ToString(), ((DateTime)dr["MONTH"]).ToString("MM") };
                        df_Transaction(20, gParam, dr, out error_msg);
                    }

                }
            }
            catch(Exception ee)
            {

            }
            getData();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.RowCount == 0) return;
            DataRow dr = gridView1.GetFocusedDataRow();
            //var drs = ds_001.Tables[0].Select("C_ID = '" + dr["C_ID"].ToString() + "'");

            //gridControl2.DataSource = drs.CopyToDataTable();

            ds_003 = df_select(1, new string[] { ((DateTime)dt_YEAR.EditValue).Year.ToString(), dr["MONTH"].ToString() }, out error_msg );
            if (ds_003 == null)
            {
                MsgBox.MsgErr("코드 정보를 가져오는데 실패 했습니다.\r\n" + error_msg, "에러");
                this.Cursor = Cursors.Default;
                return;
            }
            gridControl2.DataSource = ds_003.Tables[0];
            ds_004 = ds_003;
        }
        #endregion
    }
}
