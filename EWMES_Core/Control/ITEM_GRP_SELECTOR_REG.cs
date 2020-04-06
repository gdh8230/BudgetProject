using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class ITEM_GRP_SELECTOR_REG : UserControl
    {
        public ITEM_GRP_SELECTOR_REG()
        {
            InitializeComponent();
            env = new _Environment();
        }

        public object L_CD
        {
            get
            {
                return lueLcd.EditValue == null || string.IsNullOrWhiteSpace(lueLcd.EditValue.ToString()) ? DBNull.Value : lueLcd.EditValue;
            }
            set
            {
                lueLcd.EditValue = value;
            }
        }
        public object M_CD
        {
            get
            {
                return lueMcd.EditValue == null || string.IsNullOrWhiteSpace(lueMcd.EditValue.ToString()) ? DBNull.Value : lueMcd.EditValue;
            }
            set
            {
                lueMcd.EditValue = value;
            }
        }
        public object S_CD
        {
            get
            {
                return lueScd.EditValue == null || string.IsNullOrWhiteSpace(lueScd.EditValue.ToString()) ? DBNull.Value : lueScd.EditValue;
            }
            set
            {
                lueScd.EditValue = value;
            }
        }
        public void setAcct(ITEM_ACCT itemAcct)
        {
            switch (itemAcct)
            {
                case ITEM_ACCT.MATL:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD = '41'";
                        lueLcd.EditValue = "41";
                    }
                    break;
                case ITEM_ACCT.SUB_MATL:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD = '42'";
                        lueLcd.EditValue = "42";
                    }
                    break;
                case ITEM_ACCT.PRODUCT:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD IN ('1','2','3')";
                        lueLcd.EditValue = DBNull.Value;
                    }
                    break;
                case ITEM_ACCT.SUB_PRODUCT:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD IN ('1','2','3')";
                        lueLcd.EditValue = DBNull.Value;
                    }
                    break;
                case ITEM_ACCT.GOOD:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD IN ('1','2','3')";
                        lueLcd.EditValue = DBNull.Value;
                    }
                    break;
                case ITEM_ACCT.OUT_SOURCING:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD = '43'";
                        lueLcd.EditValue = "43";
                    }
                    break;
                case ITEM_ACCT.CONSUMABLES:
                    {
                        ((DataView)lueLcd.Properties.DataSource).RowFilter = "L_CD = '51'";
                        lueLcd.EditValue = "51";
                    }
                    break;
            }
        }
        private _Environment env;
        private string errorMsg;
        private void setDataBind()
        {
            if (this.DesignMode)
                return;
            this.lueLcd.EditValueChanged -= new System.EventHandler(this.lueLcd_EditValueChanged);
            this.lueMcd.EditValueChanged -= new System.EventHandler(this.lueMcd_EditValueChanged);
            DataTable dt = dfSelect(0, out errorMsg);
            if (dt == null)
                return;
            modUTIL.DevLookUpEditorSet(lueLcd, dt.AsDataView(), "L_NAME", "L_CD");
            lueLcd.EditValue = DBNull.Value;
            dt = dfSelect(1, out errorMsg);
            if (dt == null)
                return;
            modUTIL.DevLookUpEditorSet(lueMcd, dt.AsDataView(), "M_NAME", "M_CD");
            lueMcd.EditValue = DBNull.Value;
            dt = dfSelect(2, out errorMsg);
            if (dt == null)
                return;
            modUTIL.DevLookUpEditorSet(lueScd, dt.AsDataView(), "S_NAME", "S_CD");
            lueScd.EditValue = DBNull.Value;
            this.lueLcd.EditValueChanged += new System.EventHandler(this.lueLcd_EditValueChanged);
            this.lueMcd.EditValueChanged += new System.EventHandler(this.lueMcd_EditValueChanged);
        }
        private void ITEM_GRP_SELECTOR_Load(object sender, System.EventArgs e)
        {
            setDataBind();
        }
        private void lueLcd_EditValueChanged(object sender, System.EventArgs e)
        {
            mCdFilterSet();
        }
        private void lueMcd_EditValueChanged(object sender, System.EventArgs e)
        {
            sCdFilterSet();
        }
        private void mCdFilterSet()
        {
            string selectedLcd = lueLcd.EditValue.ToString().Trim();
            lueMcd.EditValue = DBNull.Value;
            //if (!selectedLcd.Equals(""))
                ((DataView)lueMcd.Properties.DataSource).RowFilter = "L_CD = '" + selectedLcd + "'";
            sCdFilterSet();
        }
        private void sCdFilterSet()
        {
            lueScd.EditValue = DBNull.Value;
            if (lueMcd.EditValue == null || lueMcd.EditValue == DBNull.Value)
                return;
            DataRow dr = ((DataRowView)lueMcd.GetSelectedDataRow()).Row;
            string selectedLcd = dr["L_CD"].ToString().Trim();
            string selectedMcd = dr["M_CD"].ToString().Trim();
            //if (!selectedMcd.Equals(""))
                ((DataView)lueScd.Properties.DataSource).RowFilter = "(L_CD LIKE '" + selectedLcd + "' AND M_CD LIKE '" + selectedMcd + "')";
        }
        private DataTable dfSelect(int Index, out string errorMsg)
        {
            DataTable dt = null;
            gConst.DbConn.DBClose();
            string query = string.Empty;
            errorMsg = "";
            switch (Index)
            {
                case 0:
                    {
                        query = "SELECT DISTINCT L_CD,L_NAME FROM TB_ITEM_GRP WITH(NOLOCK) WHERE COMP = @COMP AND FACT = @FACT AND ISNULL(STAT,'') <> 'D'";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataTableQuery(query, out errorMsg);
                    }
                    break;
                case 1:
                    {
                        query = "SELECT DISTINCT L_CD,M_CD,M_NAME FROM TB_ITEM_GRP WITH(NOLOCK) WHERE COMP = @COMP AND FACT = @FACT AND ISNULL(STAT,'') <> 'D'";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataTableQuery(query, out errorMsg);
                    }
                    break;
                case 2:
                    {
                        query = "SELECT DISTINCT L_CD,M_CD,S_CD,S_NAME FROM TB_ITEM_GRP WITH(NOLOCK) WHERE COMP = @COMP AND FACT = @FACT AND ISNULL(STAT,'') <> 'D'";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        dt = gConst.DbConn.GetDataTableQuery(query, out errorMsg);
                    }
                    break;
                default: break;
            }
            return dt;
        }
    }
}
