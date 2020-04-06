namespace DH_Core
{
    partial class Jnkc_Bedt_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Jnkc_Bedt_Control));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lbl_Ledit01 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bedt_ITEM = new DevExpress.XtraEditors.ButtonEdit();
            this.txt_ITEM_NAME = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bedt_ITEM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ITEM_NAME.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Ledit01
            // 
            this.lbl_Ledit01.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Ledit01.Appearance.Options.UseFont = true;
            this.lbl_Ledit01.Location = new System.Drawing.Point(1, 6);
            this.lbl_Ledit01.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbl_Ledit01.Name = "lbl_Ledit01";
            this.lbl_Ledit01.Size = new System.Drawing.Size(48, 15);
            this.lbl_Ledit01.TabIndex = 503;
            this.lbl_Ledit01.Text = "품      목";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(413, 8);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(0, 15);
            this.labelControl1.TabIndex = 649;
            this.labelControl1.Visible = false;
            // 
            // bedt_ITEM
            // 
            this.bedt_ITEM.EditValue = "";
            this.bedt_ITEM.Location = new System.Drawing.Point(55, 3);
            this.bedt_ITEM.Name = "bedt_ITEM";
            this.bedt_ITEM.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.bedt_ITEM.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.bedt_ITEM.Properties.Appearance.Options.UseBackColor = true;
            this.bedt_ITEM.Properties.Appearance.Options.UseFont = true;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.bedt_ITEM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bedt_ITEM.Size = new System.Drawing.Size(140, 22);
            this.bedt_ITEM.TabIndex = 650;
            this.bedt_ITEM.Tag = "1";
            this.bedt_ITEM.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bedt_ITEM_ButtonClick);
            this.bedt_ITEM.EditValueChanged += new System.EventHandler(this.bedt_ITEM_EditValueChanged);
            // 
            // txt_ITEM_NAME
            // 
            this.txt_ITEM_NAME.EditValue = "";
            this.txt_ITEM_NAME.Enabled = false;
            this.txt_ITEM_NAME.Location = new System.Drawing.Point(202, 3);
            this.txt_ITEM_NAME.Name = "txt_ITEM_NAME";
            this.txt_ITEM_NAME.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.txt_ITEM_NAME.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.txt_ITEM_NAME.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.txt_ITEM_NAME.Properties.Appearance.Options.UseBackColor = true;
            this.txt_ITEM_NAME.Properties.Appearance.Options.UseFont = true;
            this.txt_ITEM_NAME.Properties.Appearance.Options.UseForeColor = true;
            this.txt_ITEM_NAME.Size = new System.Drawing.Size(205, 22);
            this.txt_ITEM_NAME.TabIndex = 651;
            this.txt_ITEM_NAME.Tag = "1";
            this.txt_ITEM_NAME.Visible = false;
            // 
            // Jnkc_Bedt_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.txt_ITEM_NAME);
            this.Controls.Add(this.bedt_ITEM);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lbl_Ledit01);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Jnkc_Bedt_Control";
            this.Size = new System.Drawing.Size(489, 30);
            ((System.ComponentModel.ISupportInitialize)(this.bedt_ITEM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ITEM_NAME.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lbl_Ledit01;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit bedt_ITEM;
        private DevExpress.XtraEditors.TextEdit txt_ITEM_NAME;
    }
}
