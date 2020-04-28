namespace DH_Core
{
    partial class DatePicker_Day
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatePicker_Day));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dt_day = new DevExpress.XtraEditors.DateEdit();
            this.btn_TODAY = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_MINUS_DAY = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_ADD_DAY = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dt_day.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_day.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dt_day
            // 
            this.dt_day.EditValue = null;
            this.dt_day.Location = new System.Drawing.Point(85, 3);
            this.dt_day.Name = "dt_day";
            this.dt_day.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.dt_day.Properties.Appearance.Options.UseBackColor = true;
            this.dt_day.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dt_day.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.dt_day.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dt_day.Size = new System.Drawing.Size(97, 22);
            this.dt_day.TabIndex = 1;
            // 
            // btn_TODAY
            // 
            this.btn_TODAY.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TODAY.Appearance.Options.UseFont = true;
            this.btn_TODAY.Location = new System.Drawing.Point(22, 3);
            this.btn_TODAY.Name = "btn_TODAY";
            this.btn_TODAY.Size = new System.Drawing.Size(37, 20);
            this.btn_TODAY.TabIndex = 16;
            this.btn_TODAY.Text = "금일";
            this.btn_TODAY.Click += new System.EventHandler(this.btn_TODAY_Click);
            // 
            // sbtn_MINUS_DAY
            // 
            this.sbtn_MINUS_DAY.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_MINUS_DAY.Image")));
            this.sbtn_MINUS_DAY.Location = new System.Drawing.Point(2, 3);
            this.sbtn_MINUS_DAY.Name = "sbtn_MINUS_DAY";
            this.sbtn_MINUS_DAY.Size = new System.Drawing.Size(20, 20);
            this.sbtn_MINUS_DAY.TabIndex = 14;
            this.sbtn_MINUS_DAY.Click += new System.EventHandler(this.btn_MINUS_DAY_Click);
            // 
            // sbtn_ADD_DAY
            // 
            this.sbtn_ADD_DAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sbtn_ADD_DAY.Image = ((System.Drawing.Image)(resources.GetObject("sbtn_ADD_DAY.Image")));
            this.sbtn_ADD_DAY.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.sbtn_ADD_DAY.Location = new System.Drawing.Point(59, 3);
            this.sbtn_ADD_DAY.Name = "sbtn_ADD_DAY";
            this.sbtn_ADD_DAY.Size = new System.Drawing.Size(20, 20);
            this.sbtn_ADD_DAY.TabIndex = 17;
            this.sbtn_ADD_DAY.Click += new System.EventHandler(this.btn_ADD_DAY_Click);
            // 
            // DatePicker_Day
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.sbtn_ADD_DAY);
            this.Controls.Add(this.btn_TODAY);
            this.Controls.Add(this.sbtn_MINUS_DAY);
            this.Controls.Add(this.dt_day);
            this.Name = "DatePicker_Day";
            this.Size = new System.Drawing.Size(185, 27);
            ((System.ComponentModel.ISupportInitialize)(this.dt_day.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_day.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dt_day;
        private DevExpress.XtraEditors.SimpleButton btn_TODAY;
        private DevExpress.XtraEditors.SimpleButton sbtn_MINUS_DAY;
        private DevExpress.XtraEditors.SimpleButton sbtn_ADD_DAY;
    }
}
