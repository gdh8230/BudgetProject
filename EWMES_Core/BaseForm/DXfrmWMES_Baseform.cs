using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraTreeList;
using System.Data;

namespace DH_Core
{
    public partial class DXfrmWMES_Baseform : DevExpress.XtraEditors.XtraForm
    {

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //static _Environment Baseenv;
        private List<GridView> arr = new List<GridView>();
        private List<TreeList> arrTree = new List<TreeList>();
        private List<SplitContainerControl> arr_sc = new List<SplitContainerControl>();
        string folderName = "grid_layouts";
        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////
        public DXfrmWMES_Baseform()
        {
            InitializeComponent();
        }
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
                catch
                {

                }

            }
        }
        public void getAllGridViewControlInForm(Control.ControlCollection conColl)
        {
            //foreach (Control con in conColl)
            //{
            //    if (con.Controls.Count > 0)
            //        getAllGridViewControlInForm(con.Controls);
            //    if (con.GetType() == typeof(GridControl))
            //    {
            //        ((GridControl)con).ForceInitialize();
            //        foreach (GridView gv in ((GridControl)con).Views)
            //        {
            //            this.arr.Add(gv);
            //        }
            //    }
            //    else if (con.GetType() == typeof(TreeList))
            //    {
            //        ((TreeList)con).ForceInitialize();
            //        this.arrTree.Add((TreeList)con);
            //    }
            //    else if (con.GetType() == typeof(SplitContainerControl))
            //    {
            //        this.arr_sc.Add((SplitContainerControl)con);
            //    }
            //}
        }
        public void SetPanels(DataRow dr)
        {
            //pnl_Search.Visible = dr["AUTH_QUERY"].Equals("1") ? true : false;
            pnl_Add.Visible = dr["AUTH_NEW"].Equals("Y") ? true : false;
            pnl_Delete.Visible = dr["AUTH_DELETE"].Equals("Y") ? true : false;
            //pnl_Edit.Visible = dr["AUTH_QUERY"].Equals("1") ? true : false;
            pnl_Save.Visible = dr["AUTH_SAVE"].Equals("Y") ? true : false;
            pnl_Print.Visible = dr["AUTH_PRINT"].Equals("Y") ? true : false;
            pnl_Excel.Visible = dr["AUTH_EXCEL"].Equals("Y") ? true : false;
        }
        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static bool Excel_Print(object _DataSource, DevExpress.XtraGrid.Views.Grid.GridView _GridView, string Fname)
        {
            try
            {
                modUTIL.DevCreateExcel(_DataSource, _GridView, Fname);
                return true;
            }
            catch
            {
                return false;
            }
            //  return false;
        }
        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////
        private void DXfrmWMES_Baseform_Load(object sender, EventArgs e)
        {
            try
            {
                AutoritySet();

                getAllGridViewControlInForm(this.Controls);
                string filename = string.Empty;
                System.IO.FileInfo fi = null;
                if (arr.Count > 0)
                {
                    foreach (GridView gv in this.arr)
                    {
                        filename = folderName + "\\" + this.GetType().Name + "_" + gv.Name + ".xml";
                        fi = new System.IO.FileInfo(filename);
                        if (fi.Exists)
                            gv.RestoreLayoutFromXml(filename);
                        //Dev 그리드 뷰 복사 기능 추가
                        gv.KeyDown += new System.Windows.Forms.KeyEventHandler(DevGridUtil.Gview_KeyDown);
                    }
                }
                if (arrTree.Count > 0)
                {
                    foreach (TreeList treeList in this.arrTree)
                    {
                        filename = folderName + "\\" + this.GetType().Name + "_" + treeList.Name + ".xml";
                        fi = new System.IO.FileInfo(filename);
                        if (fi.Exists)
                            treeList.RestoreLayoutFromXml(filename);
                        //Dev 그리드 뷰 복사 기능 추가
                        treeList.KeyDown += new System.Windows.Forms.KeyEventHandler(DevGridUtil.Gview_KeyDown);
                    }
                }
                if (arr_sc.Count > 0)
                {
                    foreach (SplitContainerControl sc in this.arr_sc)
                    {
                        filename = folderName + "\\" + this.GetType().Name + "_" + sc.Name + ".xml";
                        fi = new System.IO.FileInfo(filename);
                        if (fi.Exists)
                            sc.RestoreLayoutFromXml(filename);
                    }
                }
            }
            catch(Exception ex)
            {
                LogUtil.WriteLog("DXfrmWMES_Baseform_Load Err : " + ex.Message + "\r\n" + ex.StackTrace);
                return;
            }
        }
        private void DXfrmWMES_Baseform_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch(Exception ex)
            {
                LogUtil.WriteLog("DXfrmWMES_Baseform_FormClosing Err : " + ex.Message + "\r\n" + ex.StackTrace);
                return;
            }
        }
        /////////////////////////////////////
        /// <summary>
        /// //////////2017.06.05 이보현 단축키 설정 이벤트//////////////////////
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!base.ProcessCmdKey(ref msg, keyData))
            {
                Keys key = keyData & ~(Keys.Shift | Keys.Control);
                if (keyData.Equals(Keys.F2)) /////조회
                {
                    btn_Search.PerformClick();
                    return true;
                }
                else if (keyData.Equals(Keys.F3)) /////추가
                {
                    btn_Add.PerformClick();
                    return true;
                }
                else if (keyData.Equals(Keys.F4)) /////저장
                {
                    btn_Save.PerformClick();
                    return true;
                }
                else if (keyData.Equals(Keys.F6)) /////삭제
                {
                    btn_Delete.PerformClick();
                    return true;
                }
                else if (keyData.Equals(Keys.F10)) /////인쇄물 출력
                {
                    btn_Print.PerformClick();
                    return true; 
                }
                else if (keyData.Equals(Keys.F11)) /////엑셀
                {
                    btn_Excel.PerformClick();
                    return true;
                }
                //else if (keyData.Equals(Keys.Escape)) /////닫기
                //{
                //    btn_Close.PerformClick();
                //    btn_Cancel.PerformClick();
                //    return true;
                //}
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            //return base.ProcessCmdKey(ref msg, keyData);
        }
        /////////////////////////////////////////////////////////////////////
        #endregion
        #region DB CRUD(데이터베이스 처리)
        ///////////////////////////////
        #endregion 
    }
}