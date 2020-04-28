/******************************************************************
 * 제    목 : DateEdit UserControl
 * 작 성 일 : 2013-09-26
 * 작 성 자 : 이유림 (JnKc)
 * 개    요 : DevExpress의 DateEdit 두개를 사용하여 시작일/종료일 설정
 *            금일/금월 버튼으로 쉽게 날짜 변경 가능
 *            화살표 버튼으로 일전환/월전환 변경 가능          
 * 반 환 값 : GetStartDate (DateTime) - 시작일 반환
 *            GetEndDate   (DateTime) - 종료일 반환
 * 이 벤 트 : DateEditChanged         - DateEdit의 값이 변경되면 발생
 * 수정내역 : 시작일 값이 종료일보다 클시 종료일 값 시작일로 변경
 *            종료일 값이 시작일보다 작을시 시작일 값 종료일로 변경
*******************************************************************/
using System;
using System.Windows.Forms;

namespace DH_Core
{
    public partial class DatePicker : UserControl
    {
        public DatePicker()
        {
            InitializeComponent();
            InitControl();
            this.Size = new System.Drawing.Size(382, 28);
        }

        ~DatePicker()
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

        public DateTime GetStartDate { get { return dt_START.DateTime; } }  // 시작일 반환
        public DateTime GetEndDate { get { return dt_END.DateTime; } }      // 종료일 반환
        public DateTime SetStartDate { set { dt_START.DateTime = value; } } // 시작일 설정
        public DateTime SetEndDate { set { dt_END.DateTime = value; } }     // 종료일 설정

        private Timer tmr_CHECK_EVENT;              // 이벤트 체크 타이머                      
        private bool bDATEEDIT_CHANGED = false;     // 이벤트 체크용 Flag
        #endregion

        #region Click Events

        private void btn_TODAY_Click(object sender, EventArgs e)
        {
            dt_START.DateTime = DateTime.Today;
            dt_END.DateTime = DateTime.Today;
        }

        private void btn_MONTH_Click(object sender, EventArgs e)
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            dt_START.DateTime = new DateTime(year, month, 1);
            dt_END.DateTime = new DateTime(year, month, DateTime.DaysInMonth(year, month));
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
            dt_START.DateTime = DateTime.Today.AddDays(-7);
            dt_END.DateTime = DateTime.Today;
        }

        private void InitControl()
        {
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;

            //dt_START.DateTime = DateTime.Today.AddDays(-7);
            dt_START.DateTime = new DateTime(year, month, 1);
            dt_END.DateTime = DateTime.Today;

            // DateEdit Changed 체크용 Timer 생성
            tmr_CHECK_EVENT = new Timer();
            tmr_CHECK_EVENT.Interval = 500;
            tmr_CHECK_EVENT.Tick += new EventHandler(tmr_CHECK_EVENT_Tick);
            tmr_CHECK_EVENT.Start();

            // DateEdit Value Changed Event 연결
            dt_START.EditValueChanged += new EventHandler(dt_EditValueChanged);
            dt_END.EditValueChanged += new EventHandler(dt_EditValueChanged);
        }

        /// <summary>
        /// DateEdit의 Display Format을 변경합니다
        /// </summary>
        /// <param name="_format"></param>
        public void DisplayFormat(string _format)
        {
            dt_START.Properties.DisplayFormat.FormatString = _format;
            dt_END.Properties.DisplayFormat.FormatString = _format;
            dt_START.Properties.EditFormat.FormatString = _format;
            dt_END.Properties.EditFormat.FormatString = _format;

        }

        private void SetDay(double value)
        {
            // 날짜를 하루씩 이동
            dt_START.DateTime = dt_START.DateTime.AddDays(value);
            dt_END.DateTime = dt_START.DateTime;
        }

        private void SetMonth(int value)
        {
            // 날짜를 월 1일 ~ 월말 까지 한달로 설정
            int year = dt_START.DateTime.AddMonths(value).Year;
            int month = dt_START.DateTime.AddMonths(value).Month;

            dt_START.DateTime = new DateTime(year, month, 1);
            dt_END.DateTime = new DateTime(year, month, DateTime.DaysInMonth(year, month)); // 매 월 마지막 일 획득
        }

        #endregion


        private void dt_START_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_START.DateTime >= dt_END.DateTime)
                dt_END.DateTime = dt_START.DateTime;
        }

        private void dt_END_EditValueChanged(object sender, EventArgs e)
        {
            if (dt_START.DateTime >= dt_END.DateTime)
                dt_START.DateTime = dt_END.DateTime;
        }

        private void dt_Enter(object sender, EventArgs e) //자동 커서 기능 추가 되면서 에러 방지 메소드
        {
            if (sender == dt_START)
            {
                dt_START.DateTime = dt_START.DateTime;
            }
            else if (sender == dt_END)
            {
                dt_END.DateTime = dt_END.DateTime;
            }
            SendKeys.Send("{Left}");
        }
    }
}