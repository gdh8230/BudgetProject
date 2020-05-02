namespace TIMS_PM
{
    partial class PM_030
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.SS1 = new DevExpress.XtraGrid.GridControl();
            this.SS1_View = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chk_Visible = new DevExpress.XtraEditors.CheckEdit();
            this.dtp_WorkDate_To = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtp_WorkDate_From = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SS1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS1_View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Visible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_To.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_To.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_From.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_From.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupControl2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 729);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.groupControl1, 2);
            this.groupControl1.Controls.Add(this.SS1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(1, 61);
            this.groupControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Margin = new System.Windows.Forms.Padding(1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1006, 667);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "종합 생산성";
            // 
            // SS1
            // 
            this.SS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS1.Location = new System.Drawing.Point(2, 19);
            this.SS1.MainView = this.SS1_View;
            this.SS1.Margin = new System.Windows.Forms.Padding(0);
            this.SS1.Name = "SS1";
            this.SS1.Size = new System.Drawing.Size(1002, 646);
            this.SS1.TabIndex = 1;
            this.SS1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.SS1_View});
            // 
            // SS1_View
            // 
            this.SS1_View.GridControl = this.SS1;
            this.SS1_View.IndicatorWidth = 50;
            this.SS1_View.Name = "SS1_View";
            this.SS1_View.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.SS1_View.OptionsSelection.MultiSelect = true;
            this.SS1_View.OptionsView.ColumnAutoWidth = false;
            this.SS1_View.OptionsView.ShowColumnHeaders = false;
            this.SS1_View.OptionsView.ShowGroupPanel = false;
            this.SS1_View.OptionsView.ShowIndicator = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.groupControl2.Appearance.Options.UseBackColor = true;
            this.groupControl2.Appearance.Options.UseFont = true;
            this.groupControl2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.groupControl2, 2);
            this.groupControl2.Controls.Add(this.chk_Visible);
            this.groupControl2.Controls.Add(this.dtp_WorkDate_To);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.dtp_WorkDate_From);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(1, 1);
            this.groupControl2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.groupControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl2.Margin = new System.Windows.Forms.Padding(1);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1006, 58);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "조회 조건";
            // 
            // chk_Visible
            // 
            this.chk_Visible.Location = new System.Drawing.Point(296, 26);
            this.chk_Visible.Name = "chk_Visible";
            this.chk_Visible.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
            this.chk_Visible.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.chk_Visible.Properties.Appearance.Options.UseFont = true;
            this.chk_Visible.Properties.Appearance.Options.UseForeColor = true;
            this.chk_Visible.Properties.Caption = "기존 항목 제외";
            this.chk_Visible.Size = new System.Drawing.Size(113, 19);
            this.chk_Visible.TabIndex = 11;
            // 
            // dtp_WorkDate_To
            // 
            this.dtp_WorkDate_To.EditValue = new System.DateTime(2014, 4, 9, 20, 57, 9, 0);
            this.dtp_WorkDate_To.Location = new System.Drawing.Point(186, 26);
            this.dtp_WorkDate_To.Name = "dtp_WorkDate_To";
            this.dtp_WorkDate_To.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtp_WorkDate_To.Properties.Appearance.Options.UseFont = true;
            this.dtp_WorkDate_To.Properties.Appearance.Options.UseTextOptions = true;
            this.dtp_WorkDate_To.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtp_WorkDate_To.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dtp_WorkDate_To.Properties.AutoHeight = false;
            this.dtp_WorkDate_To.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtp_WorkDate_To.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtp_WorkDate_To.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtp_WorkDate_To.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtp_WorkDate_To.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtp_WorkDate_To.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtp_WorkDate_To.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dtp_WorkDate_To.Size = new System.Drawing.Size(95, 23);
            this.dtp_WorkDate_To.TabIndex = 10;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl2.Location = new System.Drawing.Point(166, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 30);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "∽";
            // 
            // dtp_WorkDate_From
            // 
            this.dtp_WorkDate_From.EditValue = new System.DateTime(2014, 4, 9, 20, 57, 9, 0);
            this.dtp_WorkDate_From.Location = new System.Drawing.Point(65, 26);
            this.dtp_WorkDate_From.Name = "dtp_WorkDate_From";
            this.dtp_WorkDate_From.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dtp_WorkDate_From.Properties.Appearance.Options.UseFont = true;
            this.dtp_WorkDate_From.Properties.Appearance.Options.UseTextOptions = true;
            this.dtp_WorkDate_From.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.dtp_WorkDate_From.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.dtp_WorkDate_From.Properties.AutoHeight = false;
            this.dtp_WorkDate_From.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtp_WorkDate_From.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtp_WorkDate_From.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtp_WorkDate_From.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtp_WorkDate_From.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtp_WorkDate_From.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtp_WorkDate_From.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dtp_WorkDate_From.Size = new System.Drawing.Size(95, 23);
            this.dtp_WorkDate_From.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelControl1.Location = new System.Drawing.Point(11, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 15);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "작업일자";
            // 
            // PM_030
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.Name = "PM_030";
            this.Text = "PM_030";
            this.Load += new System.EventHandler(this.PM_030_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SS1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS1_View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chk_Visible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_To.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_To.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_From.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_WorkDate_From.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.DateEdit dtp_WorkDate_To;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtp_WorkDate_From;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl SS1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView SS1_View;
        private DevExpress.XtraEditors.CheckEdit chk_Visible;
    }
}