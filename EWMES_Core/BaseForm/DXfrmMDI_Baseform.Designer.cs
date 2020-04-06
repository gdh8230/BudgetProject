namespace DH_Core
{
    partial class DXfrmMDI_Baseform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DXfrmMDI_Baseform));
            this.SuspendLayout();
            // 
            // DXfrmMDI_Baseform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 662);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.LookAndFeel.SkinName = "Whiteprint";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "DXfrmMDI_Baseform";
            this.Text = "DXfrmMDI_Baseform";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DXfrmMDI_Baseform_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DXfrmMDI_Baseform_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DXfrmMDI_Baseform_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DXfrmMDI_Baseform_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}