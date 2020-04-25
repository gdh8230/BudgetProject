namespace Main1
{
    partial class Login
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.txt_PWD = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txt_NAME = new MetroFramework.Controls.MetroTextBox();
            this.bedt_ID = new DevExpress.XtraEditors.ButtonEdit();
            this.btn_Login = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Config = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Exit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.bedt_ID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(261, 309);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(72, 19);
            this.metroLabel3.TabIndex = 18;
            this.metroLabel3.Text = "비밀번호 :";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(263, 244);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(70, 19);
            this.metroLabel4.TabIndex = 17;
            this.metroLabel4.Text = "사용자ID :";
            // 
            // txt_PWD
            // 
            // 
            // 
            // 
            this.txt_PWD.CustomButton.Image = null;
            this.txt_PWD.CustomButton.Location = new System.Drawing.Point(127, 2);
            this.txt_PWD.CustomButton.Name = "";
            this.txt_PWD.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_PWD.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_PWD.CustomButton.TabIndex = 1;
            this.txt_PWD.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_PWD.CustomButton.UseSelectable = true;
            this.txt_PWD.CustomButton.Visible = false;
            this.txt_PWD.Lines = new string[0];
            this.txt_PWD.Location = new System.Drawing.Point(339, 306);
            this.txt_PWD.MaxLength = 32767;
            this.txt_PWD.Name = "txt_PWD";
            this.txt_PWD.PasswordChar = '*';
            this.txt_PWD.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_PWD.SelectedText = "";
            this.txt_PWD.SelectionLength = 0;
            this.txt_PWD.SelectionStart = 0;
            this.txt_PWD.ShortcutsEnabled = true;
            this.txt_PWD.Size = new System.Drawing.Size(151, 26);
            this.txt_PWD.TabIndex = 1;
            this.txt_PWD.UseSelectable = true;
            this.txt_PWD.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_PWD.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txt_PWD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_PWD_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(261, 276);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(72, 19);
            this.metroLabel1.TabIndex = 22;
            this.metroLabel1.Text = "사용자명 :";
            // 
            // txt_NAME
            // 
            // 
            // 
            // 
            this.txt_NAME.CustomButton.Image = null;
            this.txt_NAME.CustomButton.Location = new System.Drawing.Point(127, 2);
            this.txt_NAME.CustomButton.Name = "";
            this.txt_NAME.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txt_NAME.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_NAME.CustomButton.TabIndex = 1;
            this.txt_NAME.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_NAME.CustomButton.UseSelectable = true;
            this.txt_NAME.CustomButton.Visible = false;
            this.txt_NAME.Enabled = false;
            this.txt_NAME.Lines = new string[0];
            this.txt_NAME.Location = new System.Drawing.Point(339, 273);
            this.txt_NAME.MaxLength = 32767;
            this.txt_NAME.Name = "txt_NAME";
            this.txt_NAME.PasswordChar = '\0';
            this.txt_NAME.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_NAME.SelectedText = "";
            this.txt_NAME.SelectionLength = 0;
            this.txt_NAME.SelectionStart = 0;
            this.txt_NAME.ShortcutsEnabled = true;
            this.txt_NAME.Size = new System.Drawing.Size(151, 26);
            this.txt_NAME.TabIndex = 21;
            this.txt_NAME.TabStop = false;
            this.txt_NAME.UseSelectable = true;
            this.txt_NAME.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_NAME.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // bedt_ID
            // 
            this.bedt_ID.Location = new System.Drawing.Point(339, 241);
            this.bedt_ID.Name = "bedt_ID";
            editorButtonImageOptions1.Image = global::Budgeting_DSG.Properties.Resources.outline_search_black_18dp1;
            this.bedt_ID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bedt_ID.Size = new System.Drawing.Size(151, 26);
            this.bedt_ID.TabIndex = 0;
            this.bedt_ID.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bedt_ID_ButtonClick);
            this.bedt_ID.Leave += new System.EventHandler(this.txt_ID_Leave);
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(497, 240);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 92);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "접속";
            this.btn_Login.Click += new System.EventHandler(this.btn_LOGIN_Click);
            // 
            // btn_Config
            // 
            this.btn_Config.Location = new System.Drawing.Point(415, 338);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(75, 27);
            this.btn_Config.TabIndex = 3;
            this.btn_Config.Text = "환경설정";
            this.btn_Config.Click += new System.EventHandler(this.btn_CONFIG_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(497, 338);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 27);
            this.btn_Exit.TabIndex = 4;
            this.btn_Exit.Text = "종료";
            this.btn_Exit.Click += new System.EventHandler(this.btn_EXIT_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 409);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.bedt_ID);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txt_NAME);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.txt_PWD);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.btn_Config);
            this.Name = "Login";
            this.Text = "예산관리시스템";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.bedt_ID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox txt_PWD;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txt_NAME;
        private DevExpress.XtraEditors.ButtonEdit bedt_ID;
        private DevExpress.XtraEditors.SimpleButton btn_Login;
        private DevExpress.XtraEditors.SimpleButton btn_Config;
        private DevExpress.XtraEditors.SimpleButton btn_Exit;
    }
}

