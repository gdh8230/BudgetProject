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

            gConst.DbConn.BeginTrans();

            try
            {
                gConst.DbConn.ProcedureName = "dbo.USP_EXEC_SET_SPND_RSLT_D";
                gConst.DbConn.AddParameter(new SqlParameter("@BASE", dt));
                gConst.DbConn.AddParameter(new SqlParameter("@YEAR", ""));
                gConst.DbConn.AddParameter(new SqlParameter("@MONTH", ""));
                gConst.DbConn.ExecuteNonQuery(out error_msg);

                gConst.DbConn.ClearDB();
            }
            catch
            {
                MsgBox.MsgErr("초기화에 실패했습니다." + error_msg, "에러");
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
            if(gridControl1.DataSource != null)
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
                    excel_DS.Tables[0].Rows[0].Delete();
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
