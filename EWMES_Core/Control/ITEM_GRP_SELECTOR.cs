using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class ITEM_GRP_SELECTOR : UserControl
    {
        public ITEM_GRP_SELECTOR()
        {
            InitializeComponent();
            env = new _Environment();
        }

        public string L_CD
        {
            get
            {
                return lueLcd.EditValue == null || string.IsNullOrWhiteSpace(lueLcd.EditValue.ToString()) ? "%" : lueLcd.EditValue.ToString();
            }
        }
        public string M_CD
        {
            get
            {
                return lueMcd.EditValue == null || string.IsNullOrWhiteSpace(lueMcd.EditValue.ToString()) ? "%" : lueMcd.EditValue.ToString();
            }
        }
        public string S_CD
        {
            get
            {
                return lueScd.EditValue == null || string.IsNullOrWhiteSpace(lueScd.EditValue.ToString()) ? "%" : lueScd.EditValue.ToString();
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
            DataRow dr = null;
            if (dt == null)
                return;
            dr = dt.NewRow();
            dr["L_CD"] = "%";
            dr["L_NAME"] = "전체";
            dt.Rows.InsertAt(dr,0);
            modUTIL.DevLookUpEditorSet(lueLcd, dt.AsDataView(), "L_NAME", "L_CD");
            lueLcd.ItemIndex = 0;
            dt = dfSelect(1, out errorMsg);
            if (dt == null)
                return;
            dr = dt.NewRow();
            dr["L_CD"] = "%";
            dr["M_CD"] = "%";
            dr["M_NAME"] = "전체";
            dt.Rows.InsertAt(dr, 0);
            modUTIL.DevLookUpEditorSet(lueMcd, dt.AsDataView(), "M_NAME", "M_CD");
            ((DataView)lueMcd.Properties.DataSource).RowFilter = "M_CD = '%'";
            lueMcd.ItemIndex = 0;
            dt = dfSelect(2, out errorMsg);
            if (dt == null)
                return;
            dr = dt.NewRow();
            dr["L_CD"] = "%";
            dr["M_CD"] = "%";
            dr["S_CD"] = "%";
            dr["S_NAME"] = "전체";
            dt.Rows.InsertAt(dr, 0);
            modUTIL.DevLookUpEditorSet(lueScd, dt.AsDataView(), "S_NAME", "S_CD");
            ((DataView)lueScd.Properties.DataSource).RowFilter = "S_CD = '%'";
            lueScd.ItemIndex = 0;
            this.lueLcd.EditValueChanged += new System.EventHandler(this.lueLcd_EditValueChanged);
            this.lueMcd.EditValueChanged += new System.EventHandler(this.lueMcd_EditValueChanged);
        }

        public void setClear()
        {
            this.lueLcd.EditValueChanged -= new System.EventHandler(this.lueLcd_EditValueChanged);
            this.lueMcd.EditValueChanged -= new System.EventHandler(this.lueMcd_EditValueChanged);
            lueLcd.ItemIndex = 0;
            lueMcd.ItemIndex = 0;
            lueScd.ItemIndex = 0;
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
            if(selectedLcd.Equals("%"))
                ((DataView)lueMcd.Properties.DataSource).RowFilter = "M_CD = '%'";
            else
                ((DataView)lueMcd.Properties.DataSource).RowFilter = "M_CD = '%' OR L_CD = '"+ selectedLcd + "'";
            lueMcd.EditValue = "%";
            sCdFilterSet();
        }
        private void sCdFilterSet()
        {
            DataRow dr = ((DataRowView)lueMcd.GetSelectedDataRow()).Row;
            string selectedLcd = dr["L_CD"].ToString().Trim();
            string selectedMcd = dr["M_CD"].ToString().Trim();
            if (selectedMcd.Equals("%"))
                ((DataView)lueScd.Properties.DataSource).RowFilter = "S_CD = '%'";
            else
                ((DataView)lueScd.Properties.DataSource).RowFilter = "(L_CD LIKE '"+ selectedLcd + "' AND M_CD LIKE '" + selectedMcd + "') OR S_CD = '%'";
            lueScd.EditValue = "%";
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
                        dt = gConst.DbConn.GetDataTableQuery(query,out errorMsg);
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
