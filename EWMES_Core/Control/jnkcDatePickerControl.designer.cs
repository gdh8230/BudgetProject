namespace DH_Core
{
	partial class DatePickerControl
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatePickerControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dt_START = new DevExpress.XtraEditors.DateEdit();
            this.lbl_wave = new System.Windows.Forms.Label();
            this.sbtn_TODAY = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_MONTH = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sbtn_MINUS_DAY = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_ADD_MONTH = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_ADD_DAY = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_MINUS_MONTH = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_MINUS_YEAR = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_YEAR = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_PLUS_YEAR = new DevExpress.XtraEditors.SimpleButton();
            this.dt_END = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_START.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_START.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_END.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_END.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dt_START
            // 
            this.dt_START.CausesValidation = false;
            this.dt_START.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dt_START.EditValue = null;
            this.dt_START.EnterMoveNextControl = true;
            this.dt_START.Location = new System.Drawing.Point(240, 0);
            this.dt_START.Margin = new System.Windows.Forms.Padding(0);
            this.dt_START.Name = "dt_START";
            this.dt_START.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dt_START.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.Appearance.Options.UseBackColor = true;
            this.dt_START.Properties.Appearance.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.Button.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.Button.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.ButtonHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.ButtonHighlighted.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.ButtonPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.ButtonPressed.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.CalendarHeader.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.CalendarHeader.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCell.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCell.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellDisabled.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellHoliday.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellHoliday.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellInactive.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellInactive.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellPressed.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellSelected.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellSelected.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecial.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecial.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialPressed.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialSelected.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellSpecialSelected.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.DayCellToday.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.DayCellToday.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.HeaderPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.HeaderPressed.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.WeekDay.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.WeekDay.Options.UseFont = true;
            this.dt_START.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dt_START.Properties.AppearanceFocused.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_START.Properties.AppearanceFocused.Options.UseFont = true;
            this.dt_START.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dt_START.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.dt_START.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dt_START.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dt_START.Properties.MaxValue = new System.DateTime(((long)(0)));
            this.dt_START.Size = new System.Drawing.Size(91, 22);
            this.dt_START.TabIndex = 0;
            this.dt_START.EditValueChanged += new System.EventHandler(this.dt_START_EditValueChanged);
            // 
            // lbl_wave
            // 
            this.lbl_wave.Location = new System.Drawing.Point(331, 0);
            this.lbl_wave.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_wave.Name = "lbl_wave";
            this.lbl_wave.Size = new System.Drawing.Size(14, 22);
            this.lbl_wave.TabIndex = 2;
            this.lbl_wave.Text = "~";
            this.lbl_wave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sbtn_TODAY
            // 
            this.sbtn_TODAY.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.sbtn_TODAY.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.sbtn_TODAY.Appearance.Options.UseFont = true;
            this.sbtn_TODAY.Appearance.Options.UseForeColor = true;
            this.sbtn_TODAY.Location = new System.Drawing.Point(20, 0);
            this.sbtn_TODAY.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_TODAY.Name = "sbtn_TODAY";
            this.sbtn_TODAY.Size = new System.Drawing.Size(37, 22);
            this.sbtn_TODAY.TabIndex = 13;
            this.sbtn_TODAY.Text = "금일";
            this.sbtn_TODAY.Click += new System.EventHandler(this.btn_TODAY_Click);
            // 
            // sbtn_MONTH
            // 
            this.sbtn_MONTH.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.sbtn_MONTH.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.sbtn_MONTH.Appearance.Options.UseFont = true;
            this.sbtn_MONTH.Appearance.Options.UseForeColor = true;
            this.sbtn_MONTH.Location = new System.Drawing.Point(99, 0);
            this.sbtn_MONTH.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_MONTH.Name = "sbtn_MONTH";
            this.sbtn_MONTH.Size = new System.Drawing.Size(37, 22);
            this.sbtn_MONTH.TabIndex = 14;
            this.sbtn_MONTH.Text = "금월";
            this.sbtn_MONTH.Click += new System.EventHandler(this.btn_MONTH_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 15;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.sbtn_MINUS_DAY, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_TODAY, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_ADD_MONTH, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_ADD_DAY, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_MONTH, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_MINUS_MONTH, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_wave, 13, 0);
            this.tableLayoutPanel1.Controls.Add(this.dt_START, 12, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_MINUS_YEAR, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_YEAR, 9, 0);
            this.tableLayoutPanel1.Controls.Add(this.sbtn_PLUS_YEAR, 10, 0);
            this.tableLayoutPanel1.Controls.Add(this.dt_END, 14, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 22);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // sbtn_MINUS_DAY
            // 
            this.sbtn_MINUS_DAY.Dock = System.Windows.Forms.DockStyle.Left;
            this.sbtn_MINUS_DAY.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_MINUS_DAY.Image")));
            this.sbtn_MINUS_DAY.Location = new System.Drawing.Point(0, 0);
            this.sbtn_MINUS_DAY.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_MINUS_DAY.Name = "sbtn_MINUS_DAY";
            this.sbtn_MINUS_DAY.Size = new System.Drawing.Size(20, 22);
            this.sbtn_MINUS_DAY.TabIndex = 9;
            this.sbtn_MINUS_DAY.Click += new System.EventHandler(this.btn_MINUS_DAY_Click);
            // 
            // sbtn_ADD_MONTH
            // 
            this.sbtn_ADD_MONTH.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbtn_ADD_MONTH.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_ADD_MONTH.Image")));
            this.sbtn_ADD_MONTH.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtn_ADD_MONTH.Location = new System.Drawing.Point(136, 0);
            this.sbtn_ADD_MONTH.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_ADD_MONTH.Name = "sbtn_ADD_MONTH";
            this.sbtn_ADD_MONTH.Size = new System.Drawing.Size(20, 22);
            this.sbtn_ADD_MONTH.TabIndex = 16;
            this.sbtn_ADD_MONTH.Click += new System.EventHandler(this.btn_ADD_MONTH_Click);
            // 
            // sbtn_ADD_DAY
            // 
            this.sbtn_ADD_DAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbtn_ADD_DAY.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_ADD_DAY.Image")));
            this.sbtn_ADD_DAY.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtn_ADD_DAY.Location = new System.Drawing.Point(57, 0);
            this.sbtn_ADD_DAY.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_ADD_DAY.Name = "sbtn_ADD_DAY";
            this.sbtn_ADD_DAY.Size = new System.Drawing.Size(20, 22);
            this.sbtn_ADD_DAY.TabIndex = 10;
            this.sbtn_ADD_DAY.Click += new System.EventHandler(this.btn_ADD_DAY_Click);
            // 
            // sbtn_MINUS_MONTH
            // 
            this.sbtn_MINUS_MONTH.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_MINUS_MONTH.Image")));
            this.sbtn_MINUS_MONTH.Location = new System.Drawing.Point(79, 0);
            this.sbtn_MINUS_MONTH.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_MINUS_MONTH.Name = "sbtn_MINUS_MONTH";
            this.sbtn_MINUS_MONTH.Size = new System.Drawing.Size(20, 22);
            this.sbtn_MINUS_MONTH.TabIndex = 15;
            this.sbtn_MINUS_MONTH.Click += new System.EventHandler(this.btn_MINUS_MONTH_Click);
            // 
            // sbtn_MINUS_YEAR
            // 
            this.sbtn_MINUS_YEAR.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_MINUS_YEAR.Image")));
            this.sbtn_MINUS_YEAR.Location = new System.Drawing.Point(158, 0);
            this.sbtn_MINUS_YEAR.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_MINUS_YEAR.Name = "sbtn_MINUS_YEAR";
            this.sbtn_MINUS_YEAR.Size = new System.Drawing.Size(20, 22);
            this.sbtn_MINUS_YEAR.TabIndex = 15;
            this.sbtn_MINUS_YEAR.Click += new System.EventHandler(this.btn_MINUS_YEAR_Click);
            // 
            // sbtn_YEAR
            // 
            this.sbtn_YEAR.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.sbtn_YEAR.Appearance.ForeColor = System.Drawing.Color.Navy;
            this.sbtn_YEAR.Appearance.Options.UseFont = true;
            this.sbtn_YEAR.Appearance.Options.UseForeColor = true;
            this.sbtn_YEAR.Location = new System.Drawing.Point(178, 0);
            this.sbtn_YEAR.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_YEAR.Name = "sbtn_YEAR";
            this.sbtn_YEAR.Size = new System.Drawing.Size(37, 22);
            this.sbtn_YEAR.TabIndex = 14;
            this.sbtn_YEAR.Text = "금년";
            this.sbtn_YEAR.Click += new System.EventHandler(this.btn_YEAR_Click);
            // 
            // sbtn_PLUS_YEAR
            // 
            this.sbtn_PLUS_YEAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbtn_PLUS_YEAR.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_PLUS_YEAR.Image")));
            this.sbtn_PLUS_YEAR.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtn_PLUS_YEAR.Location = new System.Drawing.Point(215, 0);
            this.sbtn_PLUS_YEAR.Margin = new System.Windows.Forms.Padding(0);
            this.sbtn_PLUS_YEAR.Name = "sbtn_PLUS_YEAR";
            this.sbtn_PLUS_YEAR.Size = new System.Drawing.Size(20, 22);
            this.sbtn_PLUS_YEAR.TabIndex = 16;
            this.sbtn_PLUS_YEAR.Click += new System.EventHandler(this.btn_PLUS_YEAR_Click);
            // 
            // dt_END
            // 
            this.dt_END.CausesValidation = false;
            this.dt_END.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dt_END.EditValue = null;
            this.dt_END.EnterMoveNextControl = true;
            this.dt_END.Location = new System.Drawing.Point(345, 0);
            this.dt_END.Margin = new System.Windows.Forms.Padding(0);
            this.dt_END.Name = "dt_END";
            this.dt_END.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dt_END.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.Appearance.Options.UseBackColor = true;
            this.dt_END.Properties.Appearance.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.Button.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.Button.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.ButtonHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.ButtonHighlighted.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.ButtonPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.ButtonPressed.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.CalendarHeader.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.CalendarHeader.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCell.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCell.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellDisabled.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellDisabled.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellHighlighted.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellHoliday.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellHoliday.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellInactive.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellInactive.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellPressed.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellSelected.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellSelected.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecial.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecial.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialHighlighted.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialPressed.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialSelected.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellSpecialSelected.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.DayCellToday.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.DayCellToday.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.Header.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.Header.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.HeaderHighlighted.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.HeaderHighlighted.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.HeaderPressed.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.HeaderPressed.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.WeekDay.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.WeekDay.Options.UseFont = true;
            this.dt_END.Properties.AppearanceCalendar.WeekNumber.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceCalendar.WeekNumber.Options.UseFont = true;
            this.dt_END.Properties.AppearanceFocused.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dt_END.Properties.AppearanceFocused.Options.UseFont = true;
            this.dt_END.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dt_END.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.dt_END.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dt_END.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dt_END.Properties.MaxValue = new System.DateTime(((long)(0)));
            this.dt_END.Size = new System.Drawing.Size(91, 22);
            this.dt_END.TabIndex = 1;
            // 
            // DatePickerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "DatePickerControl";
            this.Size = new System.Drawing.Size(436, 22);
            ((System.ComponentModel.ISupportInitialize)(this.dt_START.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_START.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dt_END.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_END.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.DateEdit dt_START;
		private System.Windows.Forms.Label lbl_wave;
		private DevExpress.XtraEditors.SimpleButton sbtn_MINUS_DAY;
		private DevExpress.XtraEditors.SimpleButton sbtn_ADD_DAY;
		private DevExpress.XtraEditors.SimpleButton sbtn_TODAY;
		private DevExpress.XtraEditors.SimpleButton sbtn_MONTH;
		private DevExpress.XtraEditors.SimpleButton sbtn_ADD_MONTH;
		private DevExpress.XtraEditors.SimpleButton sbtn_MINUS_MONTH;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private DevExpress.XtraEditors.SimpleButton sbtn_MINUS_YEAR;
		private DevExpress.XtraEditors.SimpleButton sbtn_YEAR;
		private DevExpress.XtraEditors.SimpleButton sbtn_PLUS_YEAR;
        private DevExpress.XtraEditors.DateEdit dt_END;
    }
}
