namespace Main
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bar_DB_Status = new DevExpress.XtraBars.BarStaticItem();
            this.bar_Message = new DevExpress.XtraBars.BarStaticItem();
            this.bar_UserInfomation = new DevExpress.XtraBars.BarStaticItem();
            this.bar_Datetime = new DevExpress.XtraBars.BarStaticItem();
            this.bar_Rev = new DevExpress.XtraBars.BarStaticItem();
            this.pbtn_Add_Favorite = new DevExpress.XtraBars.BarButtonItem();
            this.pbtn_Rel_Favorite = new DevExpress.XtraBars.BarButtonItem();
            this.bar_Company = new DevExpress.XtraBars.BarStaticItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.clientPanel = new DevExpress.XtraEditors.PanelControl();
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.POPUP_Menu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.SYS = new DevExpress.XtraNavBar.NavBarGroup();
            this.SYS_83 = new DevExpress.XtraNavBar.NavBarItem();
            this.SYS_84 = new DevExpress.XtraNavBar.NavBarItem();
            this.SYS_82 = new DevExpress.XtraNavBar.NavBarItem();
            this.SYS_85 = new DevExpress.XtraNavBar.NavBarItem();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.timerChkUserNotice = new System.Windows.Forms.Timer(this.components);
            this.acUserMsg = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.POPUP_Menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.White;
            this.imageList.Images.SetKeyName(0, "1349953655_gnome-panel-window-menu.png");
            this.imageList.Images.SetKeyName(1, "note_edit.png");
            this.imageList.Images.SetKeyName(2, "note_information.png");
            this.imageList.Images.SetKeyName(3, "paint.png");
            this.imageList.Images.SetKeyName(4, "1363328594_logout.png");
            this.imageList.Images.SetKeyName(5, "1363328518_question.png");
            this.imageList.Images.SetKeyName(6, "1355906578_on-off.png");
            this.imageList.Images.SetKeyName(7, "book_open.png");
            this.imageList.Images.SetKeyName(8, "internet_explorer2.png");
            this.imageList.Images.SetKeyName(9, "folder_yellow_explorer.png");
            this.imageList.Images.SetKeyName(10, "calculator3.png");
            this.imageList.Images.SetKeyName(11, "printer_add.png");
            this.imageList.Images.SetKeyName(12, "gtk_configure.png");
            this.imageList.Images.SetKeyName(13, "simuline_icon.ico");
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.AppearancesDocking.PanelCaption.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.barAndDockingController1.AppearancesDocking.PanelCaption.Options.UseFont = true;
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.barAndDockingController1.AppearancesDocking.PanelCaptionActive.Options.UseFont = true;
            this.barAndDockingController1.LookAndFeel.SkinMaskColor = System.Drawing.Color.DodgerBlue;
            this.barAndDockingController1.LookAndFeel.SkinName = "Visual Studio 2013 Light";
            this.barAndDockingController1.LookAndFeel.UseDefaultLookAndFeel = true;
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // barManager1
            // 
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bar_DB_Status,
            this.bar_Message,
            this.bar_UserInfomation,
            this.bar_Datetime,
            this.bar_Rev,
            this.pbtn_Add_Favorite,
            this.pbtn_Rel_Favorite,
            this.bar_Company});
            this.barManager1.MaxItemId = 15;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1016, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 741);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1016, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 741);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1016, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 741);
            // 
            // bar_DB_Status
            // 
            this.bar_DB_Status.Caption = "DB끊김";
            this.bar_DB_Status.Id = 0;
            this.bar_DB_Status.Name = "bar_DB_Status";
            // 
            // bar_Message
            // 
            this.bar_Message.Caption = "메세지를 표시합니다.";
            this.bar_Message.Id = 1;
            this.bar_Message.Name = "bar_Message";
            // 
            // bar_UserInfomation
            // 
            this.bar_UserInfomation.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bar_UserInfomation.Caption = "사용자정보를 표시";
            this.bar_UserInfomation.Id = 2;
            this.bar_UserInfomation.ItemAppearance.Normal.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar_UserInfomation.ItemAppearance.Normal.Options.UseFont = true;
            this.bar_UserInfomation.Name = "bar_UserInfomation";
            // 
            // bar_Datetime
            // 
            this.bar_Datetime.Caption = "날짜와 시간을 표시합니다.";
            this.bar_Datetime.Id = 3;
            this.bar_Datetime.Name = "bar_Datetime";
            // 
            // bar_Rev
            // 
            this.bar_Rev.Caption = "Rev.표시";
            this.bar_Rev.Id = 4;
            this.bar_Rev.Name = "bar_Rev";
            // 
            // pbtn_Add_Favorite
            // 
            this.pbtn_Add_Favorite.Id = 13;
            this.pbtn_Add_Favorite.Name = "pbtn_Add_Favorite";
            // 
            // pbtn_Rel_Favorite
            // 
            this.pbtn_Rel_Favorite.Id = 14;
            this.pbtn_Rel_Favorite.Name = "pbtn_Rel_Favorite";
            // 
            // bar_Company
            // 
            this.bar_Company.Caption = "회사코드 : 1000";
            this.bar_Company.Id = 12;
            this.bar_Company.Name = "bar_Company";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "App9.ico");
            this.imageList2.Images.SetKeyName(1, "black_board.png");
            this.imageList2.Images.SetKeyName(2, "Chart.png");
            this.imageList2.Images.SetKeyName(3, "Configuration Tools.png");
            this.imageList2.Images.SetKeyName(4, "FindInFiles_16x16.png");
            this.imageList2.Images.SetKeyName(5, "Folder_16x16.png");
            this.imageList2.Images.SetKeyName(6, "Folder_Closed.png");
            this.imageList2.Images.SetKeyName(7, "folder_exe.png");
            this.imageList2.Images.SetKeyName(8, "folderback.png");
            this.imageList2.Images.SetKeyName(9, "Help_16x16.png");
            this.imageList2.Images.SetKeyName(10, "ListBullets_16x16.png");
            this.imageList2.Images.SetKeyName(11, "lock-icon16.png");
            this.imageList2.Images.SetKeyName(12, "logview.png");
            this.imageList2.Images.SetKeyName(13, "Mail16x16.png");
            this.imageList2.Images.SetKeyName(14, "monitor.png");
            this.imageList2.Images.SetKeyName(15, "Options16x16.png");
            this.imageList2.Images.SetKeyName(16, "Order.png");
            this.imageList2.Images.SetKeyName(17, "Password_key.png");
            this.imageList2.Images.SetKeyName(18, "Person_16x16.png");
            this.imageList2.Images.SetKeyName(19, "Preview_16x16.png");
            this.imageList2.Images.SetKeyName(20, "Record.png");
            this.imageList2.Images.SetKeyName(21, "Record_button.png");
            this.imageList2.Images.SetKeyName(22, "Records 1 Check.png");
            this.imageList2.Images.SetKeyName(23, "Refresh.png");
            this.imageList2.Images.SetKeyName(24, "Replace_16x16.png");
            this.imageList2.Images.SetKeyName(25, "Report.png");
            this.imageList2.Images.SetKeyName(26, "save.png");
            this.imageList2.Images.SetKeyName(27, "save16.png");
            this.imageList2.Images.SetKeyName(28, "SaveAll_16x16.png");
            this.imageList2.Images.SetKeyName(29, "SaveAs_16x16.png");
            this.imageList2.Images.SetKeyName(30, "Search-Folders_17.png");
            this.imageList2.Images.SetKeyName(31, "shell32210.png");
            this.imageList2.Images.SetKeyName(32, "Toolbox_16x16.png");
            this.imageList2.Images.SetKeyName(33, "exchange.png");
            this.imageList2.Images.SetKeyName(34, "REPORT.png");
            this.imageList2.Images.SetKeyName(35, "PART.png");
            this.imageList2.Images.SetKeyName(36, "GOODS.png");
            this.imageList2.Images.SetKeyName(37, "BACK.png");
            this.imageList2.Images.SetKeyName(38, "COUNT.png");
            this.imageList2.Images.SetKeyName(39, "BARCODE.png");
            this.imageList2.Images.SetKeyName(40, "STOCK.png");
            this.imageList2.Images.SetKeyName(41, "OUT.png");
            this.imageList2.Images.SetKeyName(42, "IN.png");
            this.imageList2.Images.SetKeyName(43, "APPLICATION03.png");
            this.imageList2.Images.SetKeyName(44, "APPLICATION04.png");
            this.imageList2.Images.SetKeyName(45, "APPLICATION02.png");
            this.imageList2.Images.SetKeyName(46, "APPLICATION01.png");
            this.imageList2.Images.SetKeyName(47, "user_business_chart.png");
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.AppearancePage.Header.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabbedMdiManager1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.White;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseBackColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseBorderColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseForeColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseImage = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseTextOptions = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.ForeColor = System.Drawing.Color.Silver;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseBackColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseBorderColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseForeColor = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseImage = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabbedMdiManager1.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabbedMdiManager1.AppearancePage.PageClient.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtraTabbedMdiManager1.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiManager1.CloseTabOnMiddleClick = DevExpress.XtraTabbedMdi.CloseTabOnMiddleClick.Never;
            this.xtraTabbedMdiManager1.Controller = this.barAndDockingController1;
            this.xtraTabbedMdiManager1.Images = this.imageList2;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.SelectedPageChanged += new System.EventHandler(this.xtraTabbedMdiManager1_SelectedPageChanged);
            this.xtraTabbedMdiManager1.PageAdded += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageAdded);
            this.xtraTabbedMdiManager1.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageRemoved);
            // 
            // clientPanel
            // 
            this.clientPanel.AlwaysScrollActiveControlIntoView = false;
            this.clientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientPanel.Location = new System.Drawing.Point(247, 0);
            this.clientPanel.LookAndFeel.SkinName = "The Asphalt World";
            this.clientPanel.LookAndFeel.UseDefaultLookAndFeel = true;
            this.clientPanel.Name = "clientPanel";
            this.clientPanel.Size = new System.Drawing.Size(769, 741);
            this.clientPanel.TabIndex = 18;
            // 
            // barMenu
            // 
            this.barMenu.BarItemHorzIndent = 6;
            this.barMenu.BarItemVertIndent = 5;
            this.barMenu.BarName = "Main menu";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.FloatLocation = new System.Drawing.Point(353, 278);
            this.barMenu.OptionsBar.AllowQuickCustomization = false;
            this.barMenu.OptionsBar.DisableClose = true;
            this.barMenu.OptionsBar.DrawDragBorder = false;
            this.barMenu.OptionsBar.RotateWhenVertical = false;
            this.barMenu.OptionsBar.UseWholeRow = true;
            this.barMenu.Text = "메뉴바";
            this.barMenu.Visible = false;
            // 
            // POPUP_Menu
            // 
            this.POPUP_Menu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.pbtn_Add_Favorite, false),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.None, false, this.pbtn_Rel_Favorite, false)});
            this.POPUP_Menu.Manager = this.barManager1;
            this.POPUP_Menu.Name = "POPUP_Menu";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.panelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(247, 741);
            this.panelControl3.TabIndex = 36;
            // 
            // panelControl2
            // 
            this.panelControl2.AutoSize = true;
            this.panelControl2.Controls.Add(this.navBarControl1);
            this.panelControl2.Controls.Add(this.dockPanel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.LookAndFeel.SkinMaskColor = System.Drawing.SystemColors.Highlight;
            this.panelControl2.LookAndFeel.SkinName = "Whiteprint";
            this.panelControl2.LookAndFeel.UseDefaultLookAndFeel = true;
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(243, 737);
            this.panelControl2.TabIndex = 31;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.SYS;
            this.navBarControl1.Appearance.Background.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarControl1.Appearance.Background.Options.UseFont = true;
            this.navBarControl1.Appearance.Button.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.Button.Options.UseFont = true;
            this.navBarControl1.Appearance.ButtonDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ButtonDisabled.Options.UseFont = true;
            this.navBarControl1.Appearance.ButtonHotTracked.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ButtonHotTracked.Options.UseFont = true;
            this.navBarControl1.Appearance.ButtonPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ButtonPressed.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupBackground.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.GroupBackground.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupHeader.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.navBarControl1.Appearance.GroupHeader.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupHeaderActive.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.GroupHeaderActive.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupHeaderHotTracked.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.GroupHeaderHotTracked.Options.UseFont = true;
            this.navBarControl1.Appearance.GroupHeaderPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.GroupHeaderPressed.Options.UseFont = true;
            this.navBarControl1.Appearance.Hint.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.Hint.Options.UseFont = true;
            this.navBarControl1.Appearance.Item.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.Item.Options.UseFont = true;
            this.navBarControl1.Appearance.Item.Options.UseImage = true;
            this.navBarControl1.Appearance.ItemActive.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ItemActive.Options.UseFont = true;
            this.navBarControl1.Appearance.ItemActive.Options.UseImage = true;
            this.navBarControl1.Appearance.ItemDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ItemDisabled.Options.UseFont = true;
            this.navBarControl1.Appearance.ItemDisabled.Options.UseImage = true;
            this.navBarControl1.Appearance.ItemHotTracked.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ItemHotTracked.Options.UseFont = true;
            this.navBarControl1.Appearance.ItemPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.ItemPressed.Options.UseFont = true;
            this.navBarControl1.Appearance.LinkDropTarget.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.LinkDropTarget.Options.UseFont = true;
            this.navBarControl1.Appearance.NavigationPaneHeader.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.navBarControl1.Appearance.NavigationPaneHeader.ForeColor = System.Drawing.Color.Black;
            this.navBarControl1.Appearance.NavigationPaneHeader.Options.UseFont = true;
            this.navBarControl1.Appearance.NavigationPaneHeader.Options.UseForeColor = true;
            this.navBarControl1.Appearance.NavPaneContentButton.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.NavPaneContentButton.Options.UseFont = true;
            this.navBarControl1.Appearance.NavPaneContentButtonHotTracked.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.NavPaneContentButtonHotTracked.Options.UseFont = true;
            this.navBarControl1.Appearance.NavPaneContentButtonPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.NavPaneContentButtonPressed.Options.UseFont = true;
            this.navBarControl1.Appearance.NavPaneContentButtonReleased.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.navBarControl1.Appearance.NavPaneContentButtonReleased.Options.UseFont = true;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.DragDropFlags = DevExpress.XtraNavBar.NavBarDragDrop.Default;
            this.navBarControl1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.SYS});
            this.navBarControl1.HideGroupCaptions = true;
            this.navBarControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.SYS_83,
            this.SYS_84,
            this.SYS_82,
            this.SYS_85});
            this.navBarControl1.LargeImages = this.imageList2;
            this.navBarControl1.LinkSelectionMode = DevExpress.XtraNavBar.LinkSelectionModeType.OneInGroup;
            this.navBarControl1.Location = new System.Drawing.Point(2, 2);
            this.navBarControl1.LookAndFeel.SkinMaskColor = System.Drawing.SystemColors.Highlight;
            this.navBarControl1.LookAndFeel.SkinName = "Whiteprint";
            this.navBarControl1.LookAndFeel.UseDefaultLookAndFeel = true;
            this.navBarControl1.MinimumSize = new System.Drawing.Size(0, 500);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.NavigationPaneGroupClientHeight = 200;
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 239;
            this.navBarControl1.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.ExplorerBar;
            this.navBarControl1.Size = new System.Drawing.Size(239, 733);
            this.navBarControl1.SmallImages = this.imageList2;
            this.navBarControl1.StoreDefaultPaintStyleName = true;
            this.navBarControl1.TabIndex = 10;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.navBarControl1_MouseDown);
            this.navBarControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.navBarControl1_MouseDown);
            // 
            // SYS
            // 
            this.SYS.Caption = "시스템관리";
            this.SYS.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.SYS.ImageOptions.LargeImageIndex = 3;
            this.SYS.ImageOptions.SmallImageIndex = 15;
            this.SYS.Name = "SYS";
            this.SYS.SelectedLinkIndex = 0;
            // 
            // SYS_83
            // 
            this.SYS_83.Caption = "프로그램등록";
            this.SYS_83.ImageOptions.LargeImageIndex = 0;
            this.SYS_83.ImageOptions.SmallImageIndex = 0;
            this.SYS_83.Name = "SYS_83";
            this.SYS_83.Tag = "SYS";
            this.SYS_83.Visible = false;
            // 
            // SYS_84
            // 
            this.SYS_84.Caption = "메뉴등록";
            this.SYS_84.ImageOptions.LargeImageIndex = 0;
            this.SYS_84.ImageOptions.SmallImageIndex = 0;
            this.SYS_84.Name = "SYS_84";
            this.SYS_84.Tag = "SYS";
            this.SYS_84.Visible = false;
            // 
            // SYS_82
            // 
            this.SYS_82.Caption = "사용자등록";
            this.SYS_82.ImageOptions.LargeImageIndex = 0;
            this.SYS_82.ImageOptions.SmallImageIndex = 0;
            this.SYS_82.Name = "SYS_82";
            this.SYS_82.Tag = "SYS";
            this.SYS_82.Visible = false;
            // 
            // SYS_85
            // 
            this.SYS_85.Caption = "사용자별권한등록";
            this.SYS_85.ImageOptions.LargeImageIndex = 0;
            this.SYS_85.ImageOptions.SmallImageIndex = 0;
            this.SYS_85.Name = "SYS_85";
            this.SYS_85.Tag = "SYS";
            this.SYS_85.Visible = false;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dockPanel1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dockPanel1.Appearance.Options.UseBackColor = true;
            this.dockPanel1.Appearance.Options.UseFont = true;
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel1.Font = new System.Drawing.Font("맑은 고딕", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.dockPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dockPanel1.ID = new System.Guid("48bac086-f245-4eb2-a3fb-94bff1dccd31");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(250, 200);
            this.dockPanel1.Size = new System.Drawing.Size(250, 900);
            this.dockPanel1.TabsScroll = true;
            this.dockPanel1.Text = "프로그램선택";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.BackColor = System.Drawing.Color.White;
            this.dockPanel1_Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.MinimumSize = new System.Drawing.Size(0, 950);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(250, 950);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // acUserMsg
            // 
            this.acUserMsg.AutoFormDelay = 5000;
            // 
            // frmMain
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.clientPanel);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(1016, 726);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DS글로벌 예산관리시스템 []";
            this.TransparencyKey = System.Drawing.Color.White;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clientPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.POPUP_Menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.PanelControl clientPanel;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Bar barMenu;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarStaticItem bar_DB_Status;
        private DevExpress.XtraBars.BarStaticItem bar_Message;
        private DevExpress.XtraBars.BarStaticItem bar_UserInfomation;
        private DevExpress.XtraBars.BarStaticItem bar_Datetime;
        private DevExpress.XtraBars.BarStaticItem bar_Rev;
        private DevExpress.XtraBars.PopupMenu POPUP_Menu;
        private DevExpress.XtraBars.BarButtonItem pbtn_Add_Favorite;
        private DevExpress.XtraBars.BarButtonItem pbtn_Rel_Favorite;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.BarStaticItem bar_Company;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraNavBar.NavBarGroup SYS;
        private DevExpress.XtraNavBar.NavBarItem SYS_83;
        private DevExpress.XtraNavBar.NavBarItem SYS_82;
        private DevExpress.XtraNavBar.NavBarItem SYS_84;
        private DevExpress.XtraNavBar.NavBarItem SYS_85;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        public DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        public DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private System.Windows.Forms.Timer timerChkUserNotice;
        private DevExpress.XtraBars.Alerter.AlertControl acUserMsg;
    }
}