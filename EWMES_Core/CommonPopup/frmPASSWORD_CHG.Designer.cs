namespace DH_Core.CommonPopup
{
    partial class frmPASSWORD_CHG
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.sbtn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sbtn_Save_Close = new DevExpress.XtraEditors.SimpleButton();
            this.txt_USER_ID = new DevExpress.XtraEditors.TextEdit();
            this.lbl_CURRENT_PASSWORD = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_CURRENT_PASSWORD = new DevExpress.XtraEditors.TextEdit();
            this.txt_NEW_PASSWORD = new DevExpress.XtraEditors.TextEdit();
            this.txt_CHK_PASSWORD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Ledit02 = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_USER_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CURRENT_PASSWORD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NEW_PASSWORD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CHK_PASSWORD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 177);
            this.panel1.TabIndex = 1007;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.sbtn_Cancel);
            this.panelControl1.Controls.Add(this.sbtn_Save_Close);
            this.panelControl1.Controls.Add(this.txt_USER_ID);
            this.panelControl1.Controls.Add(this.lbl_CURRENT_PASSWORD);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txt_CURRENT_PASSWORD);
            this.panelControl1.Controls.Add(this.txt_NEW_PASSWORD);
            this.panelControl1.Controls.Add(this.txt_CHK_PASSWORD);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lbl_Ledit02);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.LookAndFeel.SkinMaskColor = System.Drawing.SystemColors.WindowFrame;
            this.panelControl1.LookAndFeel.SkinName = "Whiteprint";
            this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = true;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(292, 177);
            this.panelControl1.TabIndex = 1010;
            // 
            // sbtn_Cancel
            // 
            this.sbtn_Cancel.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.sbtn_Cancel.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.sbtn_Cancel.Appearance.Options.UseFont = true;
            this.sbtn_Cancel.Appearance.Options.UseForeColor = true;
            this.sbtn_Cancel.Appearance.Options.UseTextOptions = true;
            this.sbtn_Cancel.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.sbtn_Cancel.Location = new System.Drawing.Point(157, 136);
            this.sbtn_Cancel.LookAndFeel.SkinMaskColor = System.Drawing.Color.SkyBlue;
            this.sbtn_Cancel.LookAndFeel.SkinName = "Whiteprint";
            this.sbtn_Cancel.LookAndFeel.UseDefaultLookAndFeel = true;
            this.sbtn_Cancel.Name = "sbtn_Cancel";
            this.sbtn_Cancel.Size = new System.Drawing.Size(102, 24);
            this.sbtn_Cancel.TabIndex = 1020;
            this.sbtn_Cancel.TabStop = false;
            this.sbtn_Cancel.Text = "취소";
            this.sbtn_Cancel.Click += new System.EventHandler(this.sbtn_Cancel_Click);
            // 
            // sbtn_Save_Close
            // 
            this.sbtn_Save_Close.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.sbtn_Save_Close.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.sbtn_Save_Close.Appearance.Options.UseFont = true;
            this.sbtn_Save_Close.Appearance.Options.UseForeColor = true;
            this.sbtn_Save_Close.Appearance.Options.UseTextOptions = true;
            this.sbtn_Save_Close.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.sbtn_Save_Close.Location = new System.Drawing.Point(24, 136);
            this.sbtn_Save_Close.LookAndFeel.SkinMaskColor = System.Drawing.Color.SkyBlue;
            this.sbtn_Save_Close.LookAndFeel.SkinName = "Whiteprint";
            this.sbtn_Save_Close.LookAndFeel.UseDefaultLookAndFeel = true;
            this.sbtn_Save_Close.Name = "sbtn_Save_Close";
            this.sbtn_Save_Close.Size = new System.Drawing.Size(102, 24);
            this.sbtn_Save_Close.TabIndex = 1019;
            this.sbtn_Save_Close.TabStop = false;
            this.sbtn_Save_Close.Text = "저장(닫기)";
            this.sbtn_Save_Close.Click += new System.EventHandler(this.sbtn_Save_Close_Click);
            // 
            // txt_USER_ID
            // 
            this.txt_USER_ID.EditValue = "";
            this.txt_USER_ID.Location = new System.Drawing.Point(105, 22);
            this.txt_USER_ID.Name = "txt_USER_ID";
            this.txt_USER_ID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txt_USER_ID.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_USER_ID.Properties.Appearance.Options.UseBackColor = true;
            this.txt_USER_ID.Properties.Appearance.Options.UseFont = true;
            this.txt_USER_ID.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txt_USER_ID.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txt_USER_ID.Size = new System.Drawing.Size(164, 22);
            this.txt_USER_ID.TabIndex = 1015;
            this.txt_USER_ID.TabStop = false;
            this.txt_USER_ID.Tag = "1";
            // 
            // lbl_CURRENT_PASSWORD
            // 
            this.lbl_CURRENT_PASSWORD.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CURRENT_PASSWORD.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_CURRENT_PASSWORD.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_CURRENT_PASSWORD.Appearance.Options.UseBackColor = true;
            this.lbl_CURRENT_PASSWORD.Appearance.Options.UseFont = true;
            this.lbl_CURRENT_PASSWORD.Appearance.Options.UseForeColor = true;
            this.lbl_CURRENT_PASSWORD.Location = new System.Drawing.Point(22, 50);
            this.lbl_CURRENT_PASSWORD.Name = "lbl_CURRENT_PASSWORD";
            this.lbl_CURRENT_PASSWORD.Size = new System.Drawing.Size(72, 15);
            this.lbl_CURRENT_PASSWORD.TabIndex = 1013;
            this.lbl_CURRENT_PASSWORD.Text = "현재비밀번호";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(22, 76);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 15);
            this.labelControl1.TabIndex = 1011;
            this.labelControl1.Text = "신규비밀번호";
            // 
            // txt_CURRENT_PASSWORD
            // 
            this.txt_CURRENT_PASSWORD.EditValue = "";
            this.txt_CURRENT_PASSWORD.Location = new System.Drawing.Point(105, 48);
            this.txt_CURRENT_PASSWORD.Name = "txt_CURRENT_PASSWORD";
            this.txt_CURRENT_PASSWORD.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.txt_CURRENT_PASSWORD.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_CURRENT_PASSWORD.Properties.Appearance.Options.UseBackColor = true;
            this.txt_CURRENT_PASSWORD.Properties.Appearance.Options.UseFont = true;
            this.txt_CURRENT_PASSWORD.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txt_CURRENT_PASSWORD.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txt_CURRENT_PASSWORD.Properties.PasswordChar = '*';
            this.txt_CURRENT_PASSWORD.Size = new System.Drawing.Size(164, 22);
            this.txt_CURRENT_PASSWORD.TabIndex = 1016;
            this.txt_CURRENT_PASSWORD.Tag = "1";
            this.txt_CURRENT_PASSWORD.EditValueChanged += new System.EventHandler(this.txt_CURRENT_PASSWORD_EditValueChanged);
            // 
            // txt_NEW_PASSWORD
            // 
            this.txt_NEW_PASSWORD.EditValue = "";
            this.txt_NEW_PASSWORD.Location = new System.Drawing.Point(105, 74);
            this.txt_NEW_PASSWORD.Name = "txt_NEW_PASSWORD";
            this.txt_NEW_PASSWORD.Properties.Appearance.BackColor = System.Drawing.Color.PeachPuff;
            this.txt_NEW_PASSWORD.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_NEW_PASSWORD.Properties.Appearance.Options.UseBackColor = true;
            this.txt_NEW_PASSWORD.Properties.Appearance.Options.UseFont = true;
            this.txt_NEW_PASSWORD.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txt_NEW_PASSWORD.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txt_NEW_PASSWORD.Properties.PasswordChar = '*';
            this.txt_NEW_PASSWORD.Size = new System.Drawing.Size(164, 22);
            this.txt_NEW_PASSWORD.TabIndex = 1017;
            this.txt_NEW_PASSWORD.Tag = "1";
            // 
            // txt_CHK_PASSWORD
            // 
            this.txt_CHK_PASSWORD.EditValue = "";
            this.txt_CHK_PASSWORD.Location = new System.Drawing.Point(105, 100);
            this.txt_CHK_PASSWORD.Name = "txt_CHK_PASSWORD";
            this.txt_CHK_PASSWORD.Properties.Appearance.BackColor = System.Drawing.Color.PeachPuff;
            this.txt_CHK_PASSWORD.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_CHK_PASSWORD.Properties.Appearance.Options.UseBackColor = true;
            this.txt_CHK_PASSWORD.Properties.Appearance.Options.UseFont = true;
            this.txt_CHK_PASSWORD.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txt_CHK_PASSWORD.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txt_CHK_PASSWORD.Properties.PasswordChar = '*';
            this.txt_CHK_PASSWORD.Size = new System.Drawing.Size(164, 22);
            this.txt_CHK_PASSWORD.TabIndex = 1018;
            this.txt_CHK_PASSWORD.Tag = "1";
            this.txt_CHK_PASSWORD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_CHK_PASSWORD_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(22, 102);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 15);
            this.labelControl2.TabIndex = 1012;
            this.labelControl2.Text = "확인비밀번호";
            // 
            // lbl_Ledit02
            // 
            this.lbl_Ledit02.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Ledit02.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_Ledit02.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lbl_Ledit02.Appearance.Options.UseBackColor = true;
            this.lbl_Ledit02.Appearance.Options.UseFont = true;
            this.lbl_Ledit02.Appearance.Options.UseForeColor = true;
            this.lbl_Ledit02.Location = new System.Drawing.Point(46, 24);
            this.lbl_Ledit02.Name = "lbl_Ledit02";
            this.lbl_Ledit02.Size = new System.Drawing.Size(48, 15);
            this.lbl_Ledit02.TabIndex = 1010;
            this.lbl_Ledit02.Text = "사원코드";
            // 
            // frmPASSWORD_CHG
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 177);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.LookAndFeel.SkinName = "Whiteprint";
            this.LookAndFeel.UseDefaultLookAndFeel = true;
            this.Name = "frmPASSWORD_CHG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "비밀번호변경";
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_USER_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CURRENT_PASSWORD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_NEW_PASSWORD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_CHK_PASSWORD.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtn_Cancel;
        private DevExpress.XtraEditors.SimpleButton sbtn_Save_Close;
        private DevExpress.XtraEditors.TextEdit txt_USER_ID;
        private DevExpress.XtraEditors.LabelControl lbl_CURRENT_PASSWORD;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txt_CURRENT_PASSWORD;
        private DevExpress.XtraEditors.TextEdit txt_NEW_PASSWORD;
        private DevExpress.XtraEditors.TextEdit txt_CHK_PASSWORD;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbl_Ledit02;
    }
}