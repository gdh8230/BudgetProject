namespace Core.BaseForm
{
    partial class BaseForm
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
            this.pnl_Close = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.pnl_Excel = new System.Windows.Forms.Panel();
            this.btn_Excel = new System.Windows.Forms.Button();
            this.pnl_Print = new System.Windows.Forms.Panel();
            this.btn_Print = new System.Windows.Forms.Button();
            this.pnl_Save = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.pnl_Del = new System.Windows.Forms.Panel();
            this.btn_Del = new System.Windows.Forms.Button();
            this.pnl_Add = new System.Windows.Forms.Panel();
            this.btn_Add = new System.Windows.Forms.Button();
            this.pnl_Search = new System.Windows.Forms.Panel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnl_Close.SuspendLayout();
            this.pnl_Excel.SuspendLayout();
            this.pnl_Print.SuspendLayout();
            this.pnl_Save.SuspendLayout();
            this.pnl_Del.SuspendLayout();
            this.pnl_Add.SuspendLayout();
            this.pnl_Search.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnl_Close);
            this.panel1.Controls.Add(this.pnl_Excel);
            this.panel1.Controls.Add(this.pnl_Print);
            this.panel1.Controls.Add(this.pnl_Save);
            this.panel1.Controls.Add(this.pnl_Del);
            this.panel1.Controls.Add(this.pnl_Add);
            this.panel1.Controls.Add(this.pnl_Search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1268, 38);
            this.panel1.TabIndex = 0;
            // 
            // pnl_Close
            // 
            this.pnl_Close.Controls.Add(this.btn_Close);
            this.pnl_Close.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Close.Location = new System.Drawing.Point(228, 0);
            this.pnl_Close.Name = "pnl_Close";
            this.pnl_Close.Size = new System.Drawing.Size(38, 38);
            this.pnl_Close.TabIndex = 6;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Close.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Close.Image = global::Core.Properties.Resources.outline_clear_black_18dp;
            this.btn_Close.Location = new System.Drawing.Point(0, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(38, 38);
            this.btn_Close.TabIndex = 2;
            this.btn_Close.Tag = "닫기";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // pnl_Excel
            // 
            this.pnl_Excel.Controls.Add(this.btn_Excel);
            this.pnl_Excel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Excel.Location = new System.Drawing.Point(190, 0);
            this.pnl_Excel.Name = "pnl_Excel";
            this.pnl_Excel.Size = new System.Drawing.Size(38, 38);
            this.pnl_Excel.TabIndex = 5;
            // 
            // btn_Excel
            // 
            this.btn_Excel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Excel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Excel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Excel.Image = global::Core.Properties.Resources.outline_explicit_black_18dp;
            this.btn_Excel.Location = new System.Drawing.Point(0, 0);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Size = new System.Drawing.Size(38, 38);
            this.btn_Excel.TabIndex = 2;
            this.btn_Excel.Tag = "엑셀";
            this.btn_Excel.UseVisualStyleBackColor = false;
            // 
            // pnl_Print
            // 
            this.pnl_Print.Controls.Add(this.btn_Print);
            this.pnl_Print.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Print.Location = new System.Drawing.Point(152, 0);
            this.pnl_Print.Name = "pnl_Print";
            this.pnl_Print.Size = new System.Drawing.Size(38, 38);
            this.pnl_Print.TabIndex = 4;
            // 
            // btn_Print
            // 
            this.btn_Print.BackColor = System.Drawing.Color.Transparent;
            this.btn_Print.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Print.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Print.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Print.Image = global::Core.Properties.Resources.outline_print_black_18dp;
            this.btn_Print.Location = new System.Drawing.Point(0, 0);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(38, 38);
            this.btn_Print.TabIndex = 2;
            this.btn_Print.Tag = "출력";
            this.btn_Print.UseVisualStyleBackColor = false;
            // 
            // pnl_Save
            // 
            this.pnl_Save.Controls.Add(this.btn_Save);
            this.pnl_Save.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Save.Location = new System.Drawing.Point(114, 0);
            this.pnl_Save.Name = "pnl_Save";
            this.pnl_Save.Size = new System.Drawing.Size(38, 38);
            this.pnl_Save.TabIndex = 3;
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.Transparent;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Save.Image = global::Core.Properties.Resources.outline_save_black_18dp;
            this.btn_Save.Location = new System.Drawing.Point(0, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(38, 38);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Tag = "저장";
            this.btn_Save.UseVisualStyleBackColor = false;
            // 
            // pnl_Del
            // 
            this.pnl_Del.Controls.Add(this.btn_Del);
            this.pnl_Del.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Del.Location = new System.Drawing.Point(76, 0);
            this.pnl_Del.Name = "pnl_Del";
            this.pnl_Del.Size = new System.Drawing.Size(38, 38);
            this.pnl_Del.TabIndex = 2;
            // 
            // btn_Del
            // 
            this.btn_Del.BackColor = System.Drawing.Color.Transparent;
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Del.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Del.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Del.Image = global::Core.Properties.Resources.outline_remove_black_18dp;
            this.btn_Del.Location = new System.Drawing.Point(0, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(38, 38);
            this.btn_Del.TabIndex = 2;
            this.btn_Del.Tag = "삭제";
            this.btn_Del.UseVisualStyleBackColor = false;
            // 
            // pnl_Add
            // 
            this.pnl_Add.Controls.Add(this.btn_Add);
            this.pnl_Add.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Add.Location = new System.Drawing.Point(38, 0);
            this.pnl_Add.Name = "pnl_Add";
            this.pnl_Add.Size = new System.Drawing.Size(38, 38);
            this.pnl_Add.TabIndex = 1;
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.Transparent;
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Add.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Add.Image = global::Core.Properties.Resources.outline_add_black_18dp;
            this.btn_Add.Location = new System.Drawing.Point(0, 0);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(38, 38);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Tag = "추가";
            this.btn_Add.UseVisualStyleBackColor = false;
            // 
            // pnl_Search
            // 
            this.pnl_Search.Controls.Add(this.btn_Search);
            this.pnl_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Search.Location = new System.Drawing.Point(0, 0);
            this.pnl_Search.Name = "pnl_Search";
            this.pnl_Search.Size = new System.Drawing.Size(38, 38);
            this.pnl_Search.TabIndex = 0;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.Transparent;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Search.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Search.Image = global::Core.Properties.Resources.outline_search_black_18dp;
            this.btn_Search.Location = new System.Drawing.Point(0, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(38, 38);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Tag = "조회";
            this.btn_Search.UseVisualStyleBackColor = false;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 638);
            this.Controls.Add(this.panel1);
            this.Name = "BaseForm";
            this.Text = "BaseForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BaseForm_FormClosing);
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnl_Close.ResumeLayout(false);
            this.pnl_Excel.ResumeLayout(false);
            this.pnl_Print.ResumeLayout(false);
            this.pnl_Save.ResumeLayout(false);
            this.pnl_Del.ResumeLayout(false);
            this.pnl_Add.ResumeLayout(false);
            this.pnl_Search.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Close;
        private System.Windows.Forms.Panel pnl_Excel;
        private System.Windows.Forms.Panel pnl_Print;
        private System.Windows.Forms.Panel pnl_Save;
        private System.Windows.Forms.Panel pnl_Del;
        private System.Windows.Forms.Panel pnl_Add;
        private System.Windows.Forms.Panel pnl_Search;
        public System.Windows.Forms.Button btn_Close;
        public System.Windows.Forms.Button btn_Excel;
        public System.Windows.Forms.Button btn_Print;
        public System.Windows.Forms.Button btn_Save;
        public System.Windows.Forms.Button btn_Del;
        public System.Windows.Forms.Button btn_Add;
        public System.Windows.Forms.Button btn_Search;
    }
}