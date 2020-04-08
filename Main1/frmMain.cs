using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Reflection;
using DevExpress.XtraNavBar;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Editors;
using DH_Core;
using DH_Core.DB;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Main
{
    public partial class frmMain : XtraForm
    {
        #region Attributes (속성정의 집함)
        ////////////////////////////////
        //환경선언
        _Environment env;
        public static string SESSION_ID = "";
        //DB agent
        MSSQLAgent DBConMES = new MSSQLAgent();
        //즐겨 찾기 기능 추가//
        DataTable Menu_dt;
        NavBarHitInfo info;
        WebBrowser web;
        string errorMSg;
        Task<int> taskGetUserNoticeCnt = null;
        //////////////////////
        #endregion

        #region Functions (각 사용자정의 평션)
        private void InitSCR()
        {
            UserLookAndFeel.Default.SetSkinStyle("iMaginary");
            bar_Datetime.Caption = DateTime.Today.ToString("yyyy-MM-dd");
        }

        public frmMain()
        {
            InitializeComponent();
            env = new _Environment();

            MenuInit();

        }
        
        private void form_Clear()
        {
            //사용자정보초기화
            env.Save();
            //bar_UserInfomation.Caption = env.Database + " " + clientIP + " " + env.Factory;
            bar_DB_Status.Caption = "DB끊김";
            bar_DB_Status.ImageIndex = 6;
        }

        private void form_Close()
        {
            if (!MsgBox.MsgQuestion("프로그램을 완전히 종료하시겠습니까?", ""))
                return;
            Close();
        }
        private bool DBConnect(string DB)
        {
            bool Ret = false;
            string error_msg = string.Empty;
            try
            {
                switch (DB)
                {
                    case "MES":
                        {
                            gConst.DbConn = DBConMES;
                            if (gConst.DbConn.DBConnect(out error_msg))
                            {
                                bar_Message.Caption = " Database와 연결되었습니다.";
                                bar_DB_Status.Caption = "DB연결됨";
                                bar_DB_Status.ImageIndex = 5;
                                Ret = true;
                            }
                            else
                            {
                                MessageBox.Show(DB + " DB 연결에 실패하였습니다. \n인터넷 연결을 확인하십시오.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                bar_Message.Caption = "Database와 연결이 끊어졌습니다.";
                                bar_DB_Status.Caption = "DB끊어짐";
                                bar_DB_Status.ImageIndex = 6;
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Ret;
        }

        public void SetWebBack()
        {
            try
            {

                if (clientPanel.Controls.Count == 0)
                {
                    web = new WebBrowser();
                    clientPanel.Controls.Add(web);
                    web.Navigate(web.StatusText);
                    web.Dock = DockStyle.Fill;
                    web.ScrollBarsEnabled = true;
                    web.ScriptErrorsSuppressed = true; //2016.10.26 스크립트 에러 메시지 보완
                    //web.Url = new Uri(@"http://www.youngchemical.co.kr/");
                    clientPanel.Visible = true;
                    //(web.ActiveXInstance as SHDocVw.WebBrowser).NewWindow3 += new SHDocVw.DWebBrowserEvents2_NewWindow3EventHandler(webBlockpopupWindow); //팝업 차단 메소드

                }
            }
            catch (Exception ex)
            { MsgBox.MsgErr(ex.Message, ""); }
        }

        //팝업 차단 메소드
        private void webBlockpopupWindow(ref object ppDisp, ref bool Cancel, uint dwFlags, string bstrUrlContext, string bstrUrl)
        {
            Cancel = true;
        }

        private void MenuInit()
        {
            string clientIP = AppUtil.Client_IP;
            env.MyIP = clientIP;

            string error_msg = string.Empty;
            Menu_dt = dbSelect(0, null, out error_msg);

            if (Menu_dt == null || Menu_dt.Rows.Count == 0)
            {
                this.SYS_82.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                this.SYS_83.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                this.SYS_84.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                this.SYS_85.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                return;
            }
            navBarControl1.Groups.Clear();
            //좌측 트리메뉴 만들기

            DevExpress.XtraNavBar.NavBarGroup group = new DevExpress.XtraNavBar.NavBarGroup();
            group.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;

            foreach (DataRow dr in Menu_dt.Rows)
            {
                group = null;
                foreach (DevExpress.XtraNavBar.NavBarGroup grp in navBarControl1.Groups)
                {
                    if (grp.Name == dr["P_CODE"].ToString())
                    {
                        group = grp;
                        break;
                    }
                }
                if (group == null)
                {
                    group = new DevExpress.XtraNavBar.NavBarGroup();
                    group.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
                    group.Name = dr["P_CODE"].ToString();
                    group.Caption = dr["P_NAME"].ToString();

                    navBarControl1.Groups.Add(group);
                }
                DevExpress.XtraNavBar.NavBarItem item = new DevExpress.XtraNavBar.NavBarItem();
                item.Tag = dr["P_CODE"].ToString();
                item.Name = dr["PROGRAM_ID"].ToString();
                item.Caption = dr["PROGRAM_NM"].ToString();
                item.AppearancePressed.ForeColor = Color.Navy;
                try
                {
                    item.AppearanceDisabled.Image = imageList2.Images[5];
                    item.Appearance.Image = imageList2.Images[6];
                    item.SmallImage = imageList2.Images[34];
                }
                catch
                {

                }
                item.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(item_LinkClicked);
                group.ItemLinks.Add(item);
                group.SelectedLinkIndex = 0;
                //group.Expanded = true;
            }

        }
        #endregion

        #region event
        void ProgramMenuButtonEvent(string Namespace, string MenuName, string MenuCode)
        {
            //열린화면 중복체크
            foreach (Form frmchild in MdiChildren)
            {
                if (frmchild.Name == MenuCode)
                {
                    frmchild.BringToFront();
                    frmchild.Show();
                    this.ActivateMdiChild(frmchild);
                    return;
                }
            }
            string error_msg = string.Empty;
            if (System.IO.File.Exists(Application.StartupPath + "\\" + Namespace + ".dll"))
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(Application.StartupPath + "\\" + Namespace + ".dll");
                    Type t = asm.GetType(Namespace + "." + MenuCode);
                    object obj = Activator.CreateInstance(t);
                    DXfrmWMES_Baseform frm = (DXfrmWMES_Baseform)obj;
                    try
                    {
                        DataRow PanelDR = dbSelect(11, new object[] { MenuCode },out errorMSg).Rows[0];
                        frm.SetPanels(PanelDR);
                        DataRow DR = dbSelect(1, new object[] { MenuCode }, out error_msg).Rows[0];
                        frm.Tag = DR;
                    }
                    catch
                    {
                        if (!env.EmpCode.Equals("SUSER"))
                        {
                            MsgBox.MsgErr("네트워크 연결이 끊어졌거나, \n또는 로그인 사용자가 존재하지 않거나, \n프로그램정보를 조회하는데 실패하였습니다.", "알림");
                            return;
                        }
                    }
                    frm.Name = MenuCode;
                    frm.Text = MenuName;
                    frm.MdiParent = this;

                    //실행파일명조회
                    frm.Show();
                    xtraTabbedMdiManager1.Pages[frm].Text = MenuName;
                    xtraTabbedMdiManager1.Pages[frm].Appearance.HeaderDisabled.BackColor = Color.White;
                    xtraTabbedMdiManager1.Pages[frm].Appearance.HeaderDisabled.BackColor2 = Color.Blue;

                    this.Text = "DS글로벌" + " 예산관리시스템 [ " + MenuName + " ]";
                }
                catch (Exception ex)
                {
                    MsgBox.MsgErr("파일 또는 메뉴에 해당하는 정보를 찾을 수 없습니다. \n 세부내용 : \n" + ex.Message, "알림");
                    modUTIL.WriteLog(ex.Source + " : " + ex.Message + " (" + ex.StackTrace + ")");
                }
            }
            else
            {
                MsgBox.MsgErr("어셈블리 파일이 존재하지 않습니다.", "알림");
            }
        }
        void item_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            DevExpress.XtraNavBar.NavBarItem item = sender as DevExpress.XtraNavBar.NavBarItem;
            this.Cursor = Cursors.WaitCursor;
            ProgramMenuButtonEvent(item.Tag.ToString(), item.Caption, item.Name);
            this.Cursor = Cursors.Default;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            form_Close();
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (xtraTabbedMdiManager1.Pages.Count > 0)
                clientPanel.Visible = false;
            else
                SetWebBack();
        }

        private void xtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            try
            {
                if (xtraTabbedMdiManager1.Pages.Count > 0)
                {
                    clientPanel.Controls.Clear();
                    clientPanel.Visible = false;
                }
            }
            catch (Exception ee)
            {
                MsgBox.MsgInformation(ee.Message, "확인");
                return;
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitSCR();

            bar_UserInfomation.Caption = env.Database + " " + env.MyIP + " " + env.FactoryName;
            bar_UserInfomation.Caption += "[" + env.EmpName + "]";
            bar_Company.Caption = "회사코드 : " + env.CompanyName;

        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gConst.DbConn == null)
                return;
            string error_msg = string.Empty;


        }

        private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        {
            try
            {
                if (xtraTabbedMdiManager1.Pages.Count > 0)
                {
                    this.Text = "DS글로벌" + " 예산관리시스템 [ " + ((DevExpress.XtraTabbedMdi.XtraTabbedMdiManager)(sender)).SelectedPage.Text + " ]";
                }
                else
                {
                    this.Text = "DS글로벌" + " 예산관리시스템 []";
                }
            }
            catch
            { }
        }
        private void navBarControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NavBarControl navBar = sender as NavBarControl;
                NavBarHitInfo hitInfo = navBar.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.InGroupCaption && !hitInfo.InGroupButton)
                    hitInfo.Group.Expanded = !hitInfo.Group.Expanded;
            }
        }

        #endregion

        #region DB
        private DataTable dbSelect(int Index, object[] param, out string errorMsg)
        {
            DataTable dt = null;
            gConst.DbConn.ClearDB();
            errorMsg = "";
            string query = string.Empty;
            switch (Index)
            {
                case 0:
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_MENU";
                        gConst.DbConn.AddParameter(new SqlParameter("@USER", env.EmpCode));
                        dt = gConst.DbConn.GetDataTableQuery(out errorMsg);
                    }
                    break;
                case 1:
                    {
                        query = "SELECT USEYN, AUTH_NEW, AUTH_UPDATE, AUTH_DELETE, AUTH_SAVE, AUTH_PRINT, AUTH_EXCEL FROM TS_ACCESS WITH(NOLOCK) "
                            + " WHERE USR = @USR AND P_ID = @WIND";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        gConst.DbConn.AddParameter(new SqlParameter("@USR", env.EmpCode));
                        gConst.DbConn.AddParameter(new SqlParameter("@WIND", param[0]));
                        dt = gConst.DbConn.GetDataTableQuery(query, out errorMsg);
                    }
                    break;
                case 2:
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_SEARCH_MENU";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        gConst.DbConn.AddParameter(new SqlParameter("@MENU_ID", "eWMES"));
                        gConst.DbConn.AddParameter(new SqlParameter("@USER_ID", env.EmpCode));
                        dt = gConst.DbConn.GetDataTableQuery(out errorMsg);
                    }
                    break;
                case 11: //화면 기능 유무 조회(Panel Visible 값 조회)
                    {
                        query = "SELECT AUTH_NEW," +
                            "                   AUTH_DELETE," +
                            "                   AUTH_SAVE," +
                            "                   AUTH_PRINT," +
                            "                   AUTH_EXCEL" +
                            " FROM DBO.TS_PROGRAM" +
                            " WHERE P_ID = '" + param[0] + "'";
                        dt = gConst.DbConn.GetDataTableQuery(query, out errorMsg);
                    }
                    break;
                default: break;
            }
            return dt;
        }

        #endregion

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}