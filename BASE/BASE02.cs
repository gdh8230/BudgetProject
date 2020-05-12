using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Spreadsheet;
using DH_Core;
using static DH_Core.DB.MSSQLAgent;

namespace BASE
{
    public partial class BASE02 : frmSub_Baseform_Search_STD
    {
        public BASE02()
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
        static DataTable dt = new DataTable();
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
        }

        private void getData()
        {
            try
            {
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
                case 0: //회계 계정 조회
                    {
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
            string query = string.Empty;

            gConst.DbConn.BeginTrans();

            try
            {
                query += "DELETE FROM SPND_RSLT_D WHERE ADMIN_NO in (SELECT ADMIN_NO FROM SPND_RSLT_H WHERE PLAN_DT LIKE '" + ((DateTime)dt_YEAR.EditValue).ToString("yyyyMM") + "' +'%') ";
                query += "DELETE FROM SPND_RSLT_H WHERE PLAN_DT LIKE '" + ((DateTime)dt_YEAR.EditValue).ToString("yyyyMM") + "' +'%' ";
                gConst.DbConn.ExecuteSQLQuery(query, out error_msg);
                foreach (DataRow dr1 in dt.Rows)
                {
                    query = string.Empty;
                    //query += "  USP_BASE_SET_RESULT2 '"+ DateTime.Parse(dr1["PLAN_DT"].ToString()).ToString("yyyyMMdd") + "', '" + dr1["DEPT"] + "', '" + dr1["DEPT_NAME"] + "', '" + dr1["PLAN_USER"] + "', '" + dr1["BUSINESS_GBN"] + "', '" + dr1["PJT_CD"] + "', '" + (dr1["BILL_DT"].ToString().Equals("") ? DateTime.Parse(dr1["PLAN_DT"].ToString()).ToString("yyyyMMdd") : DateTime.Parse(dr1["BILL_DT"].ToString()).ToString("yyyyMMdd")) + "', '" + dr1["PLAN_TITLE"] + "', ";
                    //query += "   '" + dr1["PLAN_CONTENT"] + "', '" + dr1["COMP_NAME"] + "', '" + dr1["COMP_ACCT"] + "', '" + dr1["COMP_BANK"] + "', '" + dr1["ACCT_HOLDER"] + "', '" + DateTime.Parse(dr1["PAY_DT"].ToString()).ToString("yyyyMMdd") + "', '" + dr1["COMP_MNG"] + "',   ";
                    //query += "   '" + dr1["COMP_MNG_PHONE"] + "', '" + dr1["USER"] + "', '" + dr1["GW_NO"] + "', '" + dr1["ITEM_NM"] + "', '" + (dr1["EXCH_CD"].Equals("") ? "001" : dr1["EXCH_CD"].ToString()) + "', " + dr1["PRICE"] + ", " + dr1["AMOUNT"] + ",   ";
                    //query += "   '" + dr1["UNIT"] + "', " + dr1["TOTAL"] + ", '" + dr1["ACT_CD"] + "', '" + dr1["CLASS"] + "', '" + ((DateTime)dt_YEAR.EditValue).Year.ToString() + "', '" + ((DateTime)dt_YEAR.EditValue).ToString("MM") + "', '" + env.EmpCode + "'   ";
                    gConst.DbConn.ProcedureName = "dbo.USP_BASE_SET_RESULT2";
                    //gConst.DbConn.AddParameter("@BASE", 6, dt.Rows[i].Table);
                    if (dr1["PLAN_DT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PLAN_DT", DateTime.Parse(dr1["PLAN_DT"].ToString()).ToString("yyyyMMdd")));
                    if (dr1["DEPT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@DEPT", dr1["DEPT"].ToString()));
                    if (dr1["DEPT_NAME"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@DEPT_NAME", dr1["DEPT_NAME"].ToString()));
                    if (dr1["PLAN_USER"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PLAN_USER", dr1["PLAN_USER"].ToString()));
                    if (dr1["BUSINESS_GBN"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@BUSINESS_GBN", dr1["BUSINESS_GBN"].ToString()));
                    if (dr1["PJT_CD"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PJT_CD", dr1["PJT_CD"].ToString()));
                    if (dr1["BILL_DT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@BILL_DT", (dr1["BILL_DT"].ToString().Equals("") ? DateTime.Parse(dr1["PLAN_DT"].ToString()).ToString("yyyyMMdd") : DateTime.Parse(dr1["BILL_DT"].ToString()).ToString("yyyyMMdd"))));
                    if (dr1["PLAN_TITLE"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PLAN_TITLE", dr1["PLAN_TITLE"].ToString()));
                    if (dr1["PLAN_CONTENT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PLAN_CONTENT", dr1["PLAN_CONTENT"].ToString()));
                    if (dr1["COMP_NAME"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@COMP_NAME", dr1["COMP_NAME"].ToString()));
                    if (dr1["COMP_ACCT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@COMP_ACCT", dr1["COMP_ACCT"].ToString()));
                    if (dr1["COMP_BANK"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@COMP_BANK", dr1["COMP_BANK"].ToString()));
                    if (dr1["ACCT_HOLDER"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@ACCT_HOLDER", dr1["ACCT_HOLDER"].ToString()));
                    if (dr1["PAY_DT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PAY_DT", (dr1["PAY_DT"].ToString().Equals("") ? DateTime.Parse(dr1["PLAN_DT"].ToString()).ToString("yyyyMMdd") : DateTime.Parse(dr1["PAY_DT"].ToString()).ToString("yyyyMMdd"))));
                    if (dr1["COMP_MNG"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@COMP_MNG", dr1["COMP_MNG"].ToString()));
                    if (dr1["COMP_MNG_PHONE"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@COMP_MNG_PHONE", dr1["COMP_MNG_PHONE"].ToString()));
                    if (dr1["USER"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@USER", dr1["USER"].ToString()));
                    if (dr1["GW_NO"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@GW_NO", dr1["GW_NO"].ToString()));
                    if (dr1["ITEM_NM"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@ITEM_NM", dr1["ITEM_NM"].ToString()));
                    if (dr1["EXCH_CD"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@EXCH_CD", dr1["EXCH_CD"].ToString()));
                    if (dr1["PRICE"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@PRICE", dr1["PRICE"].ToString()));
                    if (dr1["AMOUNT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@AMOUNT", dr1["AMOUNT"].ToString()));
                    if (dr1["UNIT"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@UNIT", dr1["UNIT"].ToString()));
                    if (dr1["TOTAL"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@TOTAL", dr1["TOTAL"].ToString()));
                    if (dr1["ACT_CD"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@ACT_CD", dr1["ACT_CD"].ToString()));
                    if (dr1["CLASS"].ToString() != "") gConst.DbConn.AddParameter(new SqlParameter("@CLASS", dr1["CLASS"].ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ((DateTime)dt_YEAR.EditValue).Year.ToString()));
                    gConst.DbConn.AddParameter(new SqlParameter("@MONTH", ((DateTime)dt_YEAR.EditValue).ToString("MM")));
                    gConst.DbConn.AddParameter(new SqlParameter("@MODIFY_ID", env.EmpCode));
                    gConst.DbConn.ExecuteNonQuery(out error_msg);
                    gConst.DbConn.ClearDB();
                    //query += "   DECLARE @ADMIN_NO	NVARCHAR(20), @EXCH_RATE NUMERIC(17,6)   ";
                    //query += "   SET @EXCH_RATE = (SELECT EXCH_RATE FROM TB_EXCHANGE WHERE [YEAR] = '"+ ((DateTime)dt_YEAR.EditValue).Year + "'  AND [MONTH] = '" + ((DateTime)dt_YEAR.EditValue).ToString("MM") + "' AND EXCH_CD = '" + (dr1["EXCH_CD"].Equals("") ? "001" : dr1["EXCH_CD"].ToString()) + "')  ";
                    //query += "   SET @ADMIN_NO = @DEPT + CONVERT(NVARCHAR(6),GETDATE(),12) +(SELECT ISNULL(RIGHT(CAST(RIGHT(MAX(ADMIN_NO),4) AS INT)+10001,4),'0001') FROM SPND_RSLT_H WITH(NOLOCK) WHERE LEFT(ADMIN_NO,10) = @DEPT + CONVERT(NVARCHAR(6), GETDATE(), 12)) ";
                    //query += "                                                  ";
                    //query += "                                                  ";
                    //gConst.DbConn.ExecuteSQLQuery(query, out error_msg);
                }
                if (error_msg != "")
                {
                    MsgBox.MsgErr("저장에 실패했습니다." + error_msg, "에러");
                    gConst.DbConn.Rollback();
                }

            }
            catch(Exception ee)
            {
                MsgBox.MsgErr("실패했습니다." + ee.ToString(), "에러");
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
            if (!MsgBox.MsgQuestion( ((DateTime)dt_YEAR.EditValue).ToString("yyyy-MM") + "데이터가 전부 삭제된 후 저장됩니다. 진행하시겠습니까?", "알림"))
            {
                return;
            }
            if (gridControl1.DataSource != null)
            {
                df_Transaction(20, gParam, null, out gOut_MSG);
                MsgBox.MsgInformation("저장 완료", "확인");
                return;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            spreadsheetControl1.LoadDocument("지출결의 기초 자료 양식.xls", DocumentFormat.Xls);
            Worksheet sheet = this.spreadsheetControl1.Document.Worksheets[0];

            IWorkbook workbook = spreadsheetControl1.Document;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "지출결의 기초 자료 양식";
            saveFileDialog.Title = "다른 경로로 저장";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "Excel Files(.xls)|*.xls| Excel Files(.xlsx)| *.xlsx | Excel Files(*.xlsm) | *.xlsm";
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName,
                FileMode.Create, FileAccess.ReadWrite))
                {
                    workbook.SaveDocument(stream, DocumentFormat.Xls);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OleDbConnection excel_con = null;
            string xls_filename;

            try
            {
                //엑셀 불러오기
                FileDialog file_dlg = new OpenFileDialog();
                file_dlg.InitialDirectory = "c\\";
                file_dlg.Filter = "모든 파일 (*.*)|*.*";
                file_dlg.RestoreDirectory = true;

                //엑셀문서를 보여주기
                if (file_dlg.ShowDialog() == DialogResult.OK)
                {
                    xls_filename = file_dlg.FileName;

                    string str_con = "Provider = Microsoft.ACE.OLEDB.12.0.0;Data Source=" + xls_filename + ";Extended Properties='Excel 12.0;HDR=YES'";
                    excel_con = new OleDbConnection(str_con);

                    excel_con.Open();
                    string excel_sql = @"select * from[Sheet1$]";

                    OleDbDataAdapter excel_adapter = new OleDbDataAdapter(excel_sql, excel_con);
                    DataSet excel_DS = new DataSet();
                    excel_adapter.Fill(excel_DS);
                    excel_DS.Tables[0].Rows.RemoveAt(0);
                    dt = excel_DS.Tables[0];

                    gridControl1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 가져오기 실패 : " + ex.Message);
            }
        }
    }
}
