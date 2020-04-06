using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core.BaseForm
{
    public partial class BaseForm : Form
    {

        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //static _Environment Baseenv;

        #endregion

        public BaseForm()
        {
            InitializeComponent();
        }

        #region
        private void BaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                AutoritySet();
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog("DXfrmWMES_Baseform_Load Err : " + ex.Message + "\r\n" + ex.StackTrace);
                return;
            }
        }
        private void AutoritySet()
        {
            DataRow DR = (DataRow)this.Tag;
            if (DR != null)
            {
                try
                {
                    pnl_Search.Enabled = DR[0].Equals("1") ? true : false;
                    pnl_Add.Enabled = DR[1].Equals("1") ? true : false;
                    pnl_Del.Enabled = DR[2].Equals("1") ? true : false;
                    pnl_Save.Enabled = DR[3].Equals("1") ? true : false;
                    pnl_Print.Enabled = DR[4].Equals("1") ? true : false;
                    pnl_Excel.Enabled = DR[5].Equals("1") ? true : false;
                }
                catch
                {

                }

            }
        }
        public void SetPanels(DataRow dr)
        {
            pnl_Search.Visible = dr["AUTH_QUERY"].Equals("1") ? true : false;
            pnl_Add.Visible = dr["AUTH_NEW"].Equals("1") ? true : false;
            pnl_Del.Visible = dr["AUTH_DELETE"].Equals("1") ? true : false;
            //pnl_Edit.Visible = dr["AUTH_QUERY"].Equals("1") ? true : false;
            //pnl_Edit.Visible = false;
            pnl_Save.Visible = dr["AUTH_SAVE"].Equals("1") ? true : false;
            pnl_Print.Visible = dr["AUTH_PRINT"].Equals("1") ? true : false;
            pnl_Excel.Visible = dr["AUTH_RUN"].Equals("1") ? true : false;
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
