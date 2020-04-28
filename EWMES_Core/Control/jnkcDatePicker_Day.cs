using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class DatePicker_Day : UserControl
    {
        public DatePicker_Day()
        {
            InitializeComponent();
            InitControl();
        }

        ~DatePicker_Day()
        {
            // Timer 해제
            tmr_CHECK_EVENT.Stop();
            tmr_CHECK_EVENT.Dispose();
        }

        #region Attributes

        public delegate void DateEditChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// 시작일/종료일이 변경되었을 경우 발생하는 이벤트입니다.
        /// </summary>
        public event DateEditChangedEventHandler DateEditChanged;

        public DateTime GetDate { get { return dt_day.DateTime; } }  // 일 반환
        public DateTime SetDate { set { dt_day.DateTime = value; } } // 일 설정

        private Timer tmr_CHECK_EVENT;              // 이벤트 체크 타이머                      
        private bool bDATEEDIT_CHANGED = false;     // 이벤트 체크용 Flag
        #endregion

        #region Click Events
        private void btn_TODAY_Click(object sender, EventArgs e)
        {
            dt_day.DateTime = DateTime.Today;
        }

        private void btn_MONTH_Click(object sender, EventArgs e)
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            dt_day.DateTime = new DateTime(year, month, 1);
        }

        private void btn_MINUS_DAY_Click(object sender, EventArgs e)
        {
            SetDay(-1);
        }

        private void btn_ADD_DAY_Click(object sender, EventArgs e)
        {
            SetDay(1);
        }

        private void btn_MINUS_MONTH_Click(object sender, EventArgs e)
        {
            SetMonth(-1);
        }

        private void btn_ADD_MONTH_Click(object sender, EventArgs e)
        {
            SetMonth(1);
        }
        #endregion

        #region Events

        // DateEdit Changed 체크용 Timer Event
        private void tmr_CHECK_EVENT_Tick(object sender, EventArgs e)
        {
            // 값이 변경 되었을 때만 실행
            if (bDATEEDIT_CHANGED)
            {
                tmr_CHECK_EVENT.Stop();
                SendEvent(null, null);
                bDATEEDIT_CHANGED = false;
                tmr_CHECK_EVENT.Start();
            }
        }

        protected void dt_EditValueChanged(object sender, EventArgs e)
        {
            // DateEdit에 Event를 두번 타지 않도록 처리
            bDATEEDIT_CHANGED = true;
        }

        private void SendEvent(object sender, EventArgs e)
        {
            if (DateEditChanged == null) return;

            // EventHandler에 전달
            DateEditChanged(this, e);
        }

        #endregion

        #region Functions

        /// <summary>
        /// 오늘 날짜로 변경합니다.
        /// </summary>
        public void SetToday()
        {
            btn_TODAY_Click(null, null);
        }

        /// <summary>
        /// 해당 달로 변경합니다.
        /// </summary>
        public void SetMonth()
        {
            btn_MONTH_Click(null, null);
        }

        /// <summary>
        /// 어제~오늘로 변경합니다
        /// </summary>
        public void SetYesterdayAndToday()
        {
            dt_day.DateTime = DateTime.Today;
        }

        private void InitControl()
        {
            dt_day.DateTime = DateTime.Today;

            // DateEdit Changed 체크용 Timer 생성
            tmr_CHECK_EVENT = new Timer();
            tmr_CHECK_EVENT.Interval = 500;
            tmr_CHECK_EVENT.Tick += new EventHandler(tmr_CHECK_EVENT_Tick);
            tmr_CHECK_EVENT.Start();

            // DateEdit Value Changed Event 연결
            dt_day.EditValueChanged += new EventHandler(dt_EditValueChanged);
        }

        /// <summary>
        /// DateEdit의 Display Format을 변경합니다
        /// </summary>
        /// <param name="_format"></param>
        public void DisplayFormat(string _format)
        {
            dt_day.Properties.DisplayFormat.FormatString = _format;
            dt_day.Properties.EditFormat.FormatString = _format;
        }

        private void SetDay(double value)
        {
            // 날짜를 하루씩 이동
            dt_day.DateTime = dt_day.DateTime.AddDays(value);
        }

        private void SetMonth(int value)
        {
            // 날짜를 월 1일 ~ 월말 까지 한달로 설정
            int year = dt_day.DateTime.AddMonths(value).Year;
            int month = dt_day.DateTime.AddMonths(value).Month;

            dt_day.DateTime = new DateTime(year, month, 1);
        }

        public void Clear()
        {
            dt_day.Text = string.Empty;
        }

        #endregion
    }
}
