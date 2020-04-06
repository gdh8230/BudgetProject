using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;

namespace DH_Core//PrinterControl
{
    public partial class jnkcPrinterControl : UserControl
    {
        public jnkcPrinterControl()
        {
            InitializeComponent();
        }

        XtraReport REPORT = null;

        /// <summary>
        /// PrintControl을 반환합니다.
        /// DevExpress.XtraPrinting 이 참조에 추가되어야 사용가능합니다.
        /// using 절에 DevExpress.XtraPrinting.Control 을 추가해야합니다.
        /// </summary>
        public DevExpress.XtraPrinting.Control.PrintControl PrintControl
        {
            get
            {
                return printControl1;
            }
        }
        /// <summary>
        /// PrintBarManager를 반환합니다.
        /// DevExpress.XtraPrinting 이 참조에 추가되어야 사용가능합니다.
        /// using 절에 DevExpress.XtraPrinting.Preview 를 추가해야합니다.
        /// </summary>
        public DevExpress.XtraPrinting.Preview.PrintBarManager PrintBarManager
        {
            get
            {
                return printBarManager1;
            }
        }
        /// <summary>
        /// PrintControl의 PrintingSystem을 넘기거나 받아옵니다.
        /// Report의 PrintingSystem을 넘겨주면 미리보기로 볼 수 있습니다.
        /// </summary>
        public DevExpress.XtraPrinting.PrintingSystemBase PrintingSystem
        {
            get
            {
                return printControl1.PrintingSystem;
            }
            set
            {
                printControl1.PrintingSystem = value;
            }
        }

        /// <summary>
        /// XtraReport 를 넘기면 미리보기로 보여줍니다
        /// </summary>
        /// <param name="_rpt"> 미리보기 보여줄 Report </param>
        public void CreatePreviw( DevExpress.XtraReports.UI.XtraReport _rpt )
        {
            REPORT = _rpt;
            _rpt.CreateDocument();
            printControl1.PrintingSystem = _rpt.PrintingSystem;
        }
        public void Print()
        {
            if (REPORT != null)
            {
                REPORT.Print();
            }
        }
    }
}
