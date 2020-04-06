using System;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraTreeList;

namespace EWMES_Core
{
    public class DevGridUtil
    {
        /// <summary>
        /// DevExpress Grid 데이터 세팅
        /// </summary>
        /// <param name="grid     ">Grid</param>
        /// <param name="data     ">Data</param>
        /// <param name="tableName">테이블명</param>
        public static void DevGridSetData(GridControl grid, DataSet data, String tableName)
        {
            grid.DataSource = null;

            try
            {
                grid.DataSource = data;
                grid.DataMember = tableName;
            }
            catch
            {
                grid.DataSource = null;
            }
        }

        /// <summary>
        /// DevExpress Grid 데이터 세팅
        /// </summary>
        /// <param name="grid     ">Grid</param>
        /// <param name="data     ">Data</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="errMsg   ">에러 메세지</param>
        public static void DevGridSetData(GridControl grid, DataSet data, String tableName, String errMsg)
        {
            try
            {
                if (data.Tables[tableName] != null)
                {
                    DevGridSetData(grid, data, tableName);
                    return;
                }
            }
            catch
            {
                grid.DataSource = null;
            }

            MsgBox.MsgErr(errMsg, "");
        }

        /// <summary>
        /// DevExpress Grid 데이터 Read
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <returns>Grid 데이터</returns>
        public static DataSet DevGridGetDataSet(GridControl grid)
        {
            DataSet data;

            try
            {
                data = (DataSet)grid.DataSource;
            }
            catch
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// DevExpress Grid 데이터 테이블 Read
        /// </summary>
        /// <param name="grid">Grid</param>
        /// <returns>Grid 테이블</returns>
        public static DataTable DevGridGetDataTable(GridControl grid, String tableName)
        {
            DataTable data;

            try
            {
                data = DevGridGetDataSet(grid).Tables[tableName];
            }
            catch
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// DevExpress Grid 선택된 Row 반환
        /// </summary>
        /// <param name="view">GridView</param>
        /// <returns>선택된 Row</returns>
        public static DataRow DevGridSelectRow(GridView view)
        {
            DataRow row = null;

            try
            {
                foreach (int i in view.GetSelectedRows())
                {
                    row = view.GetDataRow(i);
                    break;
                }
            }
            catch
            {
                row = null;
            }

            return row;
        }

        /// <summary>
        /// DevExpress Grid 변경된 Row Table 반환
        /// </summary>
        /// <param name="grid     ">Grid</param>
        /// <param name="tableName">테이블명</param>
        /// <returns>변경된 Row Table</returns>
        public static DataTable DevGridGetChange(GridControl grid, String tableName)
        {
            DataTable data;

            try
            {
                data = DevGridGetDataTable(grid, tableName).GetChanges();
            }
            catch
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// DevExpress Grid 변경된 Row Table 반환
        /// </summary>
        /// <param name="grid     ">Grid</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="state    ">반환할 Row 상태(등록/수정/삭제)</param>
        /// <returns>변경된 Row Table</returns>
        public static DataTable DevGridGetChange(GridControl grid, String tableName, DataRowState state)
        {
            DataTable data;

            try
            {
                data = DevGridGetDataTable(grid, tableName).GetChanges(state);
            }
            catch
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// DevExpress Grid 변경된 Row 데이터를 삭제 한다.
        /// </summary>
        /// <param name="grid     ">Grid</param>
        /// <param name="tableName">테이블명</param>
        public static void DevGridClearChangeRow(GridControl grid, String tableName)
        {
            try
            {
                DataSet data = DevGridGetDataSet(grid);

                data.RejectChanges();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Excel 파일 생성
        /// </summary>
        /// <param name="table">그리드 데이타</param>
        /// <param name="gv">그리드 뷰</param>
        public static void DevCreateExcel(GridView gv)
        {
            try
            {
                //그리드 데이터 확인
                if (gv.GridControl.DataSource != null)
                {
                    if (gv.RowCount > 0)
                    {

                        //파일 저장 창
                        SaveFileDialog file = new SaveFileDialog();
                        file.Tag = gv;

                        file.AddExtension = true;
                        file.DefaultExt = "xls";
                        file.Filter = "Excel files (*.xlsx)|*.xlsx";

                        file.FileOk += new CancelEventHandler(file_FileOk);

                        file.ShowDialog();

                        return;
                    }
                }

                MsgBox.MsgInformation("Excel을 저장할 데이터가 없습니다!", "");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Excel 파일 생성
        /// </summary>
        /// <param name="data">그리드 데이타</param>
        /// <param name="gv  ">그리드 뷰</param>
        public static void DevCreateExcel(object data, GridView gv)
        {
            try
            {
                if (data != null)
                {
                    DataSet ds = data as DataSet;

                    DevCreateExcel(ds.Tables[0], gv);
                    return;
                }

                MsgBox.MsgInformation("Excel을 저장할 데이터가 없습니다!", "");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Excel 파일 생성
        /// </summary>
        /// <param name="table">그리드 데이타</param>
        /// <param name="gv">그리드 뷰</param>
        public static void DevCreateExcel(DataTable table, GridView gv, string Fname)
        {
            try
            {
                //그리드 데이터 확인
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        //파일 저장 창
                        SaveFileDialog file = new SaveFileDialog();
                        file.Tag = gv;
                        file.FileName = Fname;
                        file.AddExtension = true;
                        file.DefaultExt = "xls";
                        file.Filter = "Excel files (*.xls)|*.xls";

                        file.FileOk += new CancelEventHandler(file_FileOk);

                        file.ShowDialog();

                        return;
                    }
                }
                MsgBox.MsgInformation("Excel을 저장할 데이터가 없습니다!", "");
            }
            catch {}
        }
        public static void gridViewExportExcelWYSIWYG(GridView gv, string FileName)
        {
            string fileName = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = FileName;
            sfd.DefaultExt = "xlsx";
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.AddExtension = true;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    AppUtil.MouseWaitCursor();
                    XlsxExportOptionsEx option = new XlsxExportOptionsEx();
                    option.ShowColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
                    option.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                    gv.ExportToXlsx(sfd.FileName, option);
                    AppUtil.MouseDefaultCursor();
                }
            }
            catch (Exception ex)
            {
                AppUtil.MouseDefaultCursor();
                MsgBox.MsgErr(ex.Message, "");
            }
            finally
            {
                AppUtil.MouseDefaultCursor();
            }
        }
        public static void gridViewExportExcelXlsx(GridView gv, string FileName)
        {
            string fileName = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = FileName;
            sfd.DefaultExt = "xlsx";
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.AddExtension = true;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    AppUtil.MouseWaitCursor();
                    gv.ExportToXlsx(sfd.FileName);
                    AppUtil.MouseDefaultCursor();
                }
            }
            catch (Exception ex)
            {
                AppUtil.MouseDefaultCursor();
                MsgBox.MsgErr(ex.Message, "");
            }
            finally
            {
                AppUtil.MouseDefaultCursor();
            }
        }
        public static void gridViewExportExcelXlsx(TreeList treeList, string FileName)
        {
            string fileName = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = FileName;
            sfd.DefaultExt = "xlsx";
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.AddExtension = true;
            try
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    AppUtil.MouseWaitCursor();
                    treeList.ExportToXlsx(sfd.FileName);
                    AppUtil.MouseDefaultCursor();
                }
            }
            catch (Exception ex)
            {
                AppUtil.MouseDefaultCursor();
                MsgBox.MsgErr(ex.Message, "");
            }
            finally
            {
                AppUtil.MouseDefaultCursor();
            }
        }
        /// <summary>
        /// 2017.11.15 이보현
        /// Dev 그리드 뷰에 셀 복사 처리 될 수 있도록 함.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Gview_KeyDown(object sender, KeyEventArgs e)
        {
            string controlName = sender.GetType().Name;
            GridView conView = null;
            TreeList conList = null;
            string value = null;
            if (controlName.Equals("GridView"))
                conView = (GridView)sender;
            else if (controlName.Equals("TreeList"))
                conList = (TreeList)sender;
            else if (controlName.Equals("BandedGridView"))
                conView = (GridView)sender;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (controlName.Equals("GridView"))
                    value = conView.GetFocusedDisplayText();
                else if (controlName.Equals("TreeList"))
                    value = conList.GetFocusedDisplayText();
                else if (controlName.Equals("BandedGridView"))
                    value = conView.GetFocusedDisplayText();
                if (string.IsNullOrWhiteSpace(value))
                    value = " ";
                Clipboard.SetText(value);
                e.Handled = true;
            }
        }
        /// <summary>
        /// 파일 저장 버튼 클릭
        /// </summary>
        private static void file_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                SaveFileDialog file = (SaveFileDialog)sender;
                GridView gv = (GridView)file.Tag;

                if (!String.IsNullOrEmpty(file.FileName))
                {
                    if (file.FileName.Substring(file.FileName.Length - 4, 4) != ".xls")
                    {
                        file.FileName = file.FileName;
                    }

                    gv.ExportToXls(file.FileName.ToString());
                }
                else
                {
                    MsgBox.MsgInformation("파일명을 입력하세요!", "");
                }
            }
            catch(Exception ee)
            {
                MsgBox.MsgInformation("파일생성에 실패했습니다!" + ee.ToString(), "");
            }
        }

        /// <summary>
        /// 그리드뷰에 데이터 존재여부 반환
        /// </summary>
        /// <param name="GV"></param>
        /// <returns></returns>
        public static bool RowcountStatus(DevExpress.XtraGrid.Views.Grid.GridView GV)
        {
            if (GV.RowCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
