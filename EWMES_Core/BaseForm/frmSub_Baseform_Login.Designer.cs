namespace DH_Core
{
    partial class frmSub_Baseform_Login
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
            this.btn_Connect = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Config = new DevExpress.XtraEditors.SimpleButton();
            this.txt_Password = new DevExpress.XtraEditors.TextEdit();
            this.lbl_Password = new DevExpress.XtraEditors.LabelControl();
            this.lbl_UserID = new DevExpress.XtraEditors.LabelControl();
            this.txt_ID = new DevExpress.XtraEditors.TextEdit();
            this.lbl_Factory = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Company = new DevExpress.XtraEditors.LabelControl();
            this.lbl_Name = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ledt_Company = new DevExpress.XtraEditors.LookUpEdit();
            this.ledt_Fact = new DevExpress.XtraEditors.LookUpEdit();
            this.btn_PASSWORD_CHANGE = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledt_Company.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledt_Fact.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(458, 337);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(72, 24);
            this.btn_Connect.TabIndex = 1;
            this.btn_Connect.Text = "접속";
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(536, 337);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(72, 24);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Text = "종료";
            // 
            // btn_Config
            // 
            this.btn_Config.Location = new System.Drawing.Point(380, 337);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(72, 24);
            this.btn_Config.TabIndex = 3;
            this.btn_Config.Text = "환경설정";
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(458, 308);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.txt_Password.Properties.Appearance.Options.UseBackColor = true;
            this.txt_Password.Size = new System.Drawing.Size(150, 20);
            this.txt_Password.TabIndex = 4;
            this.txt_Password.EditValueChanged += new System.EventHandler(this.txt_Password_EditValueChanged);
            // 
            // lbl_Password
            // 
            this.lbl_Password.Appearance.Options.UseTextOptions = true;
            this.lbl_Password.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Password.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Password.Location = new System.Drawing.Point(402, 311);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(48, 14);
            this.lbl_Password.TabIndex = 5;
            this.lbl_Password.Text = "비밀번호";
            this.lbl_Password.Visible = false;
            // 
            // lbl_UserID
            // 
            this.lbl_UserID.Appearance.Options.UseTextOptions = true;
            this.lbl_UserID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_UserID.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_UserID.Location = new System.Drawing.Point(404, 261);
            this.lbl_UserID.Name = "lbl_UserID";
            this.lbl_UserID.Size = new System.Drawing.Size(46, 14);
            this.lbl_UserID.TabIndex = 6;
            this.lbl_UserID.Text = "사용자ID";
            this.lbl_UserID.Visible = false;
            // 
            // txt_ID
            // 
            this.txt_ID.Location = new System.Drawing.Point(458, 258);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.txt_ID.Properties.Appearance.Options.UseBackColor = true;
            this.txt_ID.Size = new System.Drawing.Size(150, 20);
            this.txt_ID.TabIndex = 7;
            this.txt_ID.EditValueChanged += new System.EventHandler(this.txt_ID_EditValueChanged);
            // 
            // lbl_Factory
            // 
            this.lbl_Factory.Appearance.Options.UseTextOptions = true;
            this.lbl_Factory.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Factory.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbl_Factory.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Factory.Location = new System.Drawing.Point(392, 235);
            this.lbl_Factory.Name = "lbl_Factory";
            this.lbl_Factory.Size = new System.Drawing.Size(58, 14);
            this.lbl_Factory.TabIndex = 10;
            this.lbl_Factory.Text = "사업장";
            this.lbl_Factory.Visible = false;
            // 
            // lbl_Company
            // 
            this.lbl_Company.Appearance.Options.UseTextOptions = true;
            this.lbl_Company.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbl_Company.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lbl_Company.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Company.Location = new System.Drawing.Point(392, 208);
            this.lbl_Company.Name = "lbl_Company";
            this.lbl_Company.Size = new System.Drawing.Size(58, 14);
            this.lbl_Company.TabIndex = 12;
            this.lbl_Company.Text = "회사명";
            this.lbl_Company.Visible = false;
            // 
            // lbl_Name
            // 
            this.lbl_Name.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_Name.Appearance.Options.UseBackColor = true;
            this.lbl_Name.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_Name.Location = new System.Drawing.Point(458, 283);
            this.lbl_Name.Name = "lbl_Name";
            this.lbl_Name.Size = new System.Drawing.Size(150, 20);
            this.lbl_Name.TabIndex = 13;
            this.lbl_Name.Text = "labelControl1";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(404, 286);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "사용자명";
            this.labelControl1.Visible = false;
            // 
            // ledt_Company
            // 
            this.ledt_Company.EditValue = "<Null>";
            this.ledt_Company.Location = new System.Drawing.Point(458, 204);
            this.ledt_Company.Margin = new System.Windows.Forms.Padding(0);
            this.ledt_Company.Name = "ledt_Company";
            this.ledt_Company.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.ledt_Company.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.ledt_Company.Properties.Appearance.Options.UseBackColor = true;
            this.ledt_Company.Properties.Appearance.Options.UseFont = true;
            this.ledt_Company.Properties.Appearance.Options.UseTextOptions = true;
            this.ledt_Company.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ledt_Company.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ledt_Company.Properties.AutoHeight = false;
            this.ledt_Company.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.ledt_Company.Properties.NullText = "";
            this.ledt_Company.Size = new System.Drawing.Size(150, 23);
            this.ledt_Company.TabIndex = 540;
            this.ledt_Company.Tag = "2";
            // 
            // ledt_Fact
            // 
            this.ledt_Fact.EditValue = "<Null>";
            this.ledt_Fact.Location = new System.Drawing.Point(458, 231);
            this.ledt_Fact.Margin = new System.Windows.Forms.Padding(0);
            this.ledt_Fact.Name = "ledt_Fact";
            this.ledt_Fact.Properties.Appearance.BackColor = System.Drawing.Color.PapayaWhip;
            this.ledt_Fact.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.ledt_Fact.Properties.Appearance.Options.UseBackColor = true;
            this.ledt_Fact.Properties.Appearance.Options.UseFont = true;
            this.ledt_Fact.Properties.Appearance.Options.UseTextOptions = true;
            this.ledt_Fact.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ledt_Fact.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.ledt_Fact.Properties.AutoHeight = false;
            this.ledt_Fact.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.ledt_Fact.Properties.NullText = "";
            this.ledt_Fact.Size = new System.Drawing.Size(150, 23);
            this.ledt_Fact.TabIndex = 541;
            this.ledt_Fact.Tag = "2";
            // 
            // btn_PASSWORD_CHANGE
            // 
            this.btn_PASSWORD_CHANGE.Location = new System.Drawing.Point(534, 174);
            this.btn_PASSWORD_CHANGE.Name = "btn_PASSWORD_CHANGE";
            this.btn_PASSWORD_CHANGE.Size = new System.Drawing.Size(72, 24);
            this.btn_PASSWORD_CHANGE.TabIndex = 543;
            this.btn_PASSWORD_CHANGE.Text = "비밀번호변경";
            this.btn_PASSWORD_CHANGE.Click += new System.EventHandler(this.btn_PASSWORD_CHANGE_Click);
            // 
            // frmSub_Baseform_Login
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 377);
            this.Controls.Add(this.btn_PASSWORD_CHANGE);
            this.Controls.Add(this.ledt_Fact);
            this.Controls.Add(this.ledt_Company);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lbl_Name);
            this.Controls.Add(this.lbl_Company);
            this.Controls.Add(this.lbl_Factory);
            this.Controls.Add(this.txt_ID);
            this.Controls.Add(this.lbl_UserID);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.btn_Config);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_Connect);
            this.Name = "frmSub_Baseform_Login";
            this.Text = "frmSub_Baseform_Login";
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledt_Company.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledt_Fact.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected DevExpress.XtraEditors.SimpleButton btn_Connect;
        protected DevExpress.XtraEditors.SimpleButton btn_Close;
        protected DevExpress.XtraEditors.SimpleButton btn_Config;
        protected DevExpress.XtraEditors.TextEdit txt_Password;
        protected DevExpress.XtraEditors.TextEdit txt_ID;
        protected DevExpress.XtraEditors.LabelControl lbl_Password;
        protected DevExpress.XtraEditors.LabelControl lbl_UserID;
        protected DevExpress.XtraEditors.LabelControl lbl_Factory;
        protected DevExpress.XtraEditors.LabelControl lbl_Company;
        protected DevExpress.XtraEditors.LabelControl lbl_Name;
        protected DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.LookUpEdit ledt_Company;
        protected DevExpress.XtraEditors.LookUpEdit ledt_Fact;
        protected DevExpress.XtraEditors.SimpleButton btn_PASSWORD_CHANGE;
    }
}