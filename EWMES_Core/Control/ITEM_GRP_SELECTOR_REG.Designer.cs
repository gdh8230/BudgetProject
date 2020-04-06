namespace DH_Core
{
    partial class ITEM_GRP_SELECTOR_REG
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ITEM_GRP_SELECTOR_REG));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lueScd = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lueMcd = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lueLcd = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lueScd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMcd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLcd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lueScd
            // 
            this.lueScd.EditValue = "<Null>";
            this.lueScd.Location = new System.Drawing.Point(440, 3);
            this.lueScd.Name = "lueScd";
            this.lueScd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lueScd.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueScd.Properties.Appearance.Options.UseBackColor = true;
            this.lueScd.Properties.Appearance.Options.UseFont = true;
            this.lueScd.Properties.Appearance.Options.UseTextOptions = true;
            this.lueScd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lueScd.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lueScd.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueScd.Properties.AppearanceDropDown.Options.UseFont = true;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.lueScd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.lueScd.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("S_NAME", "소분류")});
            this.lueScd.Properties.NullText = "";
            this.lueScd.Properties.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lueScd.Properties.ShowHeader = false;
            this.lueScd.Size = new System.Drawing.Size(149, 22);
            this.lueScd.TabIndex = 627;
            this.lueScd.Tag = "2";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(398, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 15);
            this.labelControl2.TabIndex = 626;
            this.labelControl2.Text = "소분류";
            // 
            // lueMcd
            // 
            this.lueMcd.EditValue = "<Null>";
            this.lueMcd.Location = new System.Drawing.Point(243, 3);
            this.lueMcd.Name = "lueMcd";
            this.lueMcd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lueMcd.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueMcd.Properties.Appearance.Options.UseBackColor = true;
            this.lueMcd.Properties.Appearance.Options.UseFont = true;
            this.lueMcd.Properties.Appearance.Options.UseTextOptions = true;
            this.lueMcd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lueMcd.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lueMcd.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueMcd.Properties.AppearanceDropDown.Options.UseFont = true;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.lueMcd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.lueMcd.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("M_NAME", "중분류")});
            this.lueMcd.Properties.NullText = "";
            this.lueMcd.Properties.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lueMcd.Properties.ShowHeader = false;
            this.lueMcd.Size = new System.Drawing.Size(149, 22);
            this.lueMcd.TabIndex = 625;
            this.lueMcd.Tag = "2";
            this.lueMcd.EditValueChanged += new System.EventHandler(this.lueMcd_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(201, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 15);
            this.labelControl1.TabIndex = 624;
            this.labelControl1.Text = "중분류";
            // 
            // lueLcd
            // 
            this.lueLcd.EditValue = "<Null>";
            this.lueLcd.Location = new System.Drawing.Point(46, 3);
            this.lueLcd.Name = "lueLcd";
            this.lueLcd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lueLcd.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueLcd.Properties.Appearance.Options.UseBackColor = true;
            this.lueLcd.Properties.Appearance.Options.UseFont = true;
            this.lueLcd.Properties.Appearance.Options.UseTextOptions = true;
            this.lueLcd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lueLcd.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lueLcd.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lueLcd.Properties.AppearanceDropDown.Options.UseFont = true;
            editorButtonImageOptions3.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions3.Image")));
            this.lueLcd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.lueLcd.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("L_NAME", "대분류")});
            this.lueLcd.Properties.NullText = "";
            this.lueLcd.Properties.PopupFormMinSize = new System.Drawing.Size(300, 0);
            this.lueLcd.Properties.ShowHeader = false;
            this.lueLcd.Size = new System.Drawing.Size(149, 22);
            this.lueLcd.TabIndex = 623;
            this.lueLcd.Tag = "2";
            this.lueLcd.EditValueChanged += new System.EventHandler(this.lueLcd_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Location = new System.Drawing.Point(4, 7);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 15);
            this.labelControl8.TabIndex = 622;
            this.labelControl8.Text = "대분류";
            // 
            // ITEM_GRP_SELECTOR_REG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lueScd);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lueMcd);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lueLcd);
            this.Name = "ITEM_GRP_SELECTOR_REG";
            this.Size = new System.Drawing.Size(592, 27);
            this.Load += new System.EventHandler(this.ITEM_GRP_SELECTOR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueScd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueMcd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueLcd.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lueScd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lueMcd;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lueLcd;
    }
}
