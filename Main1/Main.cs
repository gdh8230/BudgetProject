using DH_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main1
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        DataSet Menu_dt;
        _Environment env;
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            env = new _Environment();
            MenuInit();
        }

        private void MenuInit()
        {
            string error_msg = string.Empty;
            string group;
            Menu_dt = dbSelect(0, null, out error_msg);

            //좌측 트리메뉴 권한별 Visible만들기
            foreach (DataRow dr in Menu_dt.Tables[0].Rows) //대메뉴
            {
                if(dr != null)
                {
                    try
                    {
                        switch (dr[0])
                        {
                            case "SYST": { menu_panel1.Visible = true; } break;
                            case "BASE": { menu_panel2.Visible = true; } break;
                            case "STD" : { menu_panel3.Visible = true; } break;
                            case "EXEC": { menu_panel4.Visible = true; } break;
                            case "STAT": { menu_panel5.Visible = true;  } break;
                        }
                    }catch(Exception e)
                    {

                    }
                }
            }
            foreach (DataRow dr in Menu_dt.Tables[1].Rows) //소메뉴
            {
                if (dr != null)
                {
                    try
                    {
                        switch (dr[0])
                        {
                            case "ACCOUNT_CODE": { btn_ACCODE.Visible = true; } break;
                            case "DEPT": { btn_DEPT.Visible = true; } break;
                            case "USER": { btn_USER.Visible = true; } break;
                            case "CODE": { btn_CODE.Visible = true; } break;
                            case "AUTH": { btn_AUTH.Visible = true; } break;
                            case "BASE01": { btn_BASE01.Visible = true; } break;
                            case "BASE02": { btn_BASE02.Visible = true; } break;
                            case "BASE03": { btn_BASE03.Visible = true; } break;
                            case "STD01": { btn_STD01.Visible = true; } break;
                            case "STD02": { btn_STD02.Visible = true; } break;
                            case "STD03": { btn_STD03.Visible = true; } break;
                            case "EXEC01": { btn_EXEC01.Visible = true; } break;
                            case "EXEC02": { btn_EXEC02.Visible = true; } break;
                            case "STAT01": { btn_STAT01.Visible = true; } break;
                            case "STAT02": { btn_STAT02.Visible = true; } break;
                            case "STAT03": { btn_STAT03.Visible = true; } break;
                            case "STAT04": { btn_STAT04.Visible = true; } break;
                            case "STAT05": { btn_STAT05.Visible = true; } break;
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

        }



        //추후 버튼이벤트시 효과를 위해 사용
        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (isCollapsed.Equals("Y"))
            //{
            //    menu_panel1.AutoSize = true;
            //    menu_panel1.Height += 10;
            //    if(menu_panel1.Size.Height == menu_panel1.MaximumSize.Height)
            //    {
            //        timer1.Stop();
            //        button.Tag = "N";
            //    }

            //}
            //else
            //{
            //    menu_panel1.AutoSize = false;
            //    menu_panel1.Height -= 10;
            //    if(menu_panel1.Size.Height == menu_panel1.MinimumSize.Height)
            //    {
            //        timer1.Stop();
            //        button.Tag = "Y";
            //    }
            //}

        }

        private void P_MENU_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            bool isYN = button.UseMnemonic;
            string isCollapsed = button.Tag.ToString();
            if (!isYN)
            {
                switch (button.Tag)
                {
                    case "SYST":
                        menu_panel1.AutoSize = true;
                        break;
                    case "BASE":
                        menu_panel2.AutoSize = true;
                        break;
                    case "STD":
                        menu_panel3.AutoSize = true;
                        break;
                    case "EXEC":
                        menu_panel4.AutoSize = true;
                        break;
                    case "STAT":
                        menu_panel5.AutoSize = true;
                        break;
                }
            }
            else
            {
                switch (button.Tag)
                {
                    case "SYST":
                        menu_panel1.AutoSize = false;
                        menu_panel1.Height = menu_panel1.MinimumSize.Height;
                        break;
                    case "BASE":
                        menu_panel2.AutoSize = false;
                        menu_panel2.Height = menu_panel2.MinimumSize.Height;
                        break;
                    case "STD":
                        menu_panel3.AutoSize = false;
                        menu_panel3.Height = menu_panel3.MinimumSize.Height;
                        break;
                    case "EXEC":
                        menu_panel4.AutoSize = false;
                        menu_panel4.Height = menu_panel4.MinimumSize.Height;
                        break;
                    case "STAT":
                        menu_panel5.AutoSize = false;
                        menu_panel5.Height = menu_panel5.MinimumSize.Height;
                        break;
                }
            }
            button.UseMnemonic = !isYN;
        }

        private void C_MENU_Click(object sender, EventArgs e)
        {
            Button menu_item = sender as Button;
            Panel panel = sender as Panel;
            this.Cursor = Cursors.WaitCursor;
            ProgramMenuButtonEvent(menu_item.AccessibleName.ToString(), menu_item.Text, menu_item.Tag.ToString());
            this.Cursor = Cursors.Default;
        }
        
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
                    frmSub_Baseform_System_NullPanel frm = (frmSub_Baseform_System_NullPanel)obj;
                    //try
                    //{
                    //    DataRow PanelDR = dbSelect(11, new object[] { MenuCode },out error_msg).Tables[0].Rows[0];
                    //    frm.SetPanels(PanelDR);
                    //    DataRow DR = dbSelect(1, new object[] { MenuCode }, out error_msg).Tables[0].Rows[0];
                    //    frm.Tag = DR;
                    //}
                    //catch
                    //{
                    //    if (!env.EmpCode.Equals("SUSER"))
                    //    {
                    //        MsgBox.MsgErr("네트워크 연결이 끊어졌거나, \n또는 로그인 사용자가 존재하지 않거나, \n프로그램정보를 조회하는데 실패하였습니다.", "알림");
                    //        return;
                    //    }
                    //}
                    frm.TopLevel = false;
                    TabPage tp = new TabPage(frm.Text);
                    metroTabControl1.TabPages.Add(tp);
                    //metroTabControl1.TabPages[tp.Text].Controls.Add(frm);
                    frm.Name = MenuCode;
                    frm.Text = MenuName;
                    frm.Parent = tp;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    //this.ActiveMdiChild.Tag = tp;
                    //this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);


                    //실행파일명조회
                    frm.Show();
                    frm.Tag = tp;
                    frm.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);
                    //metroTabControl1.TabPages[frm].Text = MenuName;
                    //metroTabControl1 .Pages[frm].Text = MenuName;
                    //metroTabControl1.Pages[frm].Appearance.HeaderDisabled.BackColor = Color.White;
                    //metroTabControl1.Pages[frm].Appearance.HeaderDisabled.BackColor2 = Color.Blue;
                    //switch (Namespace)
                    //{
                    //    case "SYS": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[32]; } break;//시스템관리
                    //    case "MST": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[7]; } break;//기준정보
                    //    case "MAT": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[16]; } break;//자재관리
                    //    case "PRC": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[16]; } break;//생산관리
                    //    case "QCA": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[24]; } break;//품질관리
                    //    case "WMS": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[19]; } break;//제품관리
                    //    case "MON": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[14]; } break;//모니터링관리
                    //    case "DRW": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[7]; } break;//도면관리
                    //    case "BCD": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[39]; } break;//바코드관리
                    //    case "LBL": { xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[39]; } break;//라벨관리
                    //    default:
                    //        {
                    //            xtraTabbedMdiManager1.Pages[frm].Image = imageList2.Images[0];// PROJ_MES_Main.Properties.Resources.Open_16x16;
                    //        }
                    //        break;
                    //}
                    this.Text = "(주)영케미칼" + " eWMES [ " + MenuName + " ]";
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
        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }
        #region DB
        private DataSet dbSelect(int Index, object[] param, out string errorMsg)
        {
            DataSet dt = null;
            gConst.DbConn.ClearDB();
            errorMsg = "";
            string query = string.Empty;
            switch (Index)
            {
                case 0:
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_MENU";
                        gConst.DbConn.AddParameter(new SqlParameter("@USER", env.EmpCode));
                        dt = gConst.DbConn.GetDataSetQuery(out errorMsg);
                    }
                    break;
                case 1:
                    {
                        query = "SELECT AUTH_QUERY, AUTH_NEW, AUTH_UPDATE, AUTH_DELETE, AUTH_SAVE, AUTH_PRINT, AUTH_EXCEL FROM TS_ACCESS WITH(NOLOCK) "
                            + " WHERE COMP = @COMP AND FACT = @FACT AND USR = @USR AND WIND = @WIND";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        gConst.DbConn.AddParameter(new SqlParameter("@USR", env.EmpCode));
                        gConst.DbConn.AddParameter(new SqlParameter("@WIND", param[0]));
                        dt = gConst.DbConn.GetDataSetQuery(query, out errorMsg);
                    }
                    break;
                case 2:
                    {
                        gConst.DbConn.ProcedureName = "USP_SYS_GET_SEARCH_MENU";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        gConst.DbConn.AddParameter(new SqlParameter("@MENU_ID", "eWMES"));
                        gConst.DbConn.AddParameter(new SqlParameter("@USER_ID", env.EmpCode));
                        dt = gConst.DbConn.GetDataSetQuery(out errorMsg);
                    }
                    break;
                case 11: //화면 기능 유무 조회(Panel Visible 값 조회)
                    {
                        query = "SELECT AUTH_QUERY," +
                            "                   AUTH_NEW," +
                            "                   AUTH_DELETE," +
                            "                   AUTH_SAVE," +
                            "                   AUTH_PRINT," +
                            "                   AUTH_EXCEL" +
                            " FROM DBO.TS_PROGRAM" +
                            " WHERE WIND = '" + param[0] + "'";
                        dt = gConst.DbConn.GetDataSetQuery(query, out errorMsg);
                    }
                    break;
                default: break;
            }
            return dt;
        }
        #endregion

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
