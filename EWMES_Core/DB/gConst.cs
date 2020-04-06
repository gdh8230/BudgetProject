using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using DH_Core.DB;
namespace DH_Core
{
    public class gConst
    {
        private static MSSQLAgent dbConnMES = null;                              //DB
        #region Get/Set

        /// <summary>
        /// DB Connection(sms)
        /// </summary>           
        public static MSSQLAgent DbConn
        {
            get { return gConst.dbConnMES; }
            set { gConst.dbConnMES = value; }
        }
        #endregion
    }
}
