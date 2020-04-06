/******************************************************************
 * 제    목 : DateEdit UserControl
 * 작 성 일 : 2013-09-26
 * 수 정 일 : 2017-11-22 
 * 작 성 자 : 이유림 (JnKc)
 * 수 정 자 : wMES2팀 주태민
 * 개    요 : DevExpress의 DateEdit 두개를 사용하여 시작일/종료일 설정
 *            금일/금월 버튼으로 쉽게 날짜 변경 가능
 *            화살표 버튼으로 일전환/월전환 변경 가능          
 * 반 환 값 : GetStartDate (DateTime) - 시작일 반환
 *            GetEndDate   (DateTime) - 종료일 반환
 * 이 벤 트 : DateEditChanged         - DateEdit의 값이 변경되면 발생
 * 수정내역 : 시작일 값이 종료일보다 클시 종료일 값 시작일로 변경
 *            종료일 값이 시작일보다 작을시 시작일 값 종료일로 변경
 *            프로퍼티 사용으로 변경
*******************************************************************/
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
	public partial class jnkcDatePickerControl : UserControl
	{
		public jnkcDatePickerControl()
		{
			InitializeComponent();
			InitControl();
		}

		~jnkcDatePickerControl()
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

		[Browsable(false)]
		public DateTime GetStartDate { get { return dt_START.DateTime; } }  // 시작일 반환
		[Browsable(false)]
		public DateTime GetDate { get { return dt_START.DateTime; } }  // 시작일 반환
		[Browsable(false)]
		public DateTime GetEndDate { get { return dt_END.DateTime; } }      // 종료일 반환
		[Browsable(false)]


        public Object SetStartEditValue { set { dt_START.EditValue = value; } } // 시작일 null지정
        public Object SetEndEditValue { set { dt_END.EditValue = value; } } // 종료일 null지정
        public DateTime SetStartDate { set { dt_START.DateTime = value; } } // 시작일 설정
		[Browsable(false)]
		public DateTime SetEndDate { set { dt_END.DateTime = value; } }     // 종료일 설정

		private IJNKCDateTime jNKCDate;

		[Description("날짜"), Category("JNKC속성"), DisplayName("날짜")]
		public IJNKCDateTime JNKCDate { get => jNKCDate; set => jNKCDate = value; }

		//	[Description("시작날짜"), Category("JNKC속성"), DisplayName("시작날짜")]
		[Browsable(false)]
		public DateTime StartDate { get => dt_START.DateTime; set => dt_START.DateTime = value; } // 시작일 설정
																																//	[Description("종료날짜"), Category("JNKC속성 날짜"), DisplayName("종료날짜")]
		[Browsable(false)]
		public DateTime EndDate { get => dt_END.DateTime; set => dt_END.DateTime = value; }     // 종료일 설정

		private Timer tmr_CHECK_EVENT;              // 이벤트 체크 타이머                      
		private bool bDATEEDIT_CHANGED = false;     // 이벤트 체크용 Flag
		private bool m_yearVisible = true;
		private bool m_monthVisible = true;
		private bool m_dayVisible = true;
		private bool m_endDateVisible = true;

		[Description("년 보기 / 안보기"), Category("JNKC속성"), DisplayName("년 표시")]
		public bool YearVisible
		{
			get => m_yearVisible; set
			{
				m_yearVisible = value;
				sbtn_YEAR.Visible = value;
				sbtn_MINUS_YEAR.Visible = value;
				sbtn_PLUS_YEAR.Visible = value;
				tableLayoutPanel1.ColumnStyles[8].Width = value == false ? 0 : 20;
				tableLayoutPanel1.ColumnStyles[9].Width = value == false ? 0 : 37;
				tableLayoutPanel1.ColumnStyles[10].Width = value == false ? 0 : 20;

				DisplayFormat();

			}
		}
		[Description("월 보기 / 안보기"), Category("JNKC속성"), DisplayName("월 표시")]
		public bool MonthVisible
		{
			get => m_monthVisible; set
			{
				m_monthVisible = value;
				sbtn_MONTH.Visible = value;
				sbtn_ADD_MONTH.Visible = value;
				sbtn_MINUS_MONTH.Visible = value;
				tableLayoutPanel1.ColumnStyles[4].Width = value == false ? 0 : 20;
				tableLayoutPanel1.ColumnStyles[5].Width = value == false ? 0 : 37;
				tableLayoutPanel1.ColumnStyles[6].Width = value == false ? 0 : 20;
				DisplayFormat();
			}
		}
		[Description("일 보기 / 안보기"), Category("JNKC속성"), DisplayName("일 표시")]
		public bool DayVisible
		{
			get => m_dayVisible; set
			{
				m_dayVisible = value;
				sbtn_ADD_DAY.Visible = value;
				sbtn_MINUS_DAY.Visible = value;
				sbtn_TODAY.Visible = value;
				tableLayoutPanel1.ColumnStyles[0].Width = value == false ? 0 : 20;
				tableLayoutPanel1.ColumnStyles[1].Width = value == false ? 0 : 37;
				tableLayoutPanel1.ColumnStyles[2].Width = value == false ? 0 : 20;
				DisplayFormat();
			}
		}
		[Description("종료날짜 보기 / 안보기"), Category("JNKC속성"), DisplayName("종료날짜 표시")]
		public bool EndPickerVisible
		{
			get => m_endDateVisible; set
			{
				m_endDateVisible = value;
				lbl_wave.Visible = value;
				dt_END.Visible = value;
				tableLayoutPanel1.ColumnStyles[13].Width = value == false ? 0 : 14;
				tableLayoutPanel1.ColumnStyles[14].Width = value == false ? 0 : 50;
				DisplayFormat();
				if (jNKCDate != null)
				{
					DateTime sdt = jNKCDate.START == null ? DateTime.Now : jNKCDate.START;
					DateTime edt = jNKCDate.END == null ? DateTime.Now : jNKCDate.END;
					jNKCDate.DateChanged -= DateTime_DateChanged;

					if (m_endDateVisible == false)
					{										
					jNKCDate = new JNKCDateTimeOneDate(sdt, edt);
					}
					else
					{					
						jNKCDate = new JNKCDateTime(sdt, edt);
					}
					jNKCDate.DateChanged += DateTime_DateChanged;
				}
			}
		}
        
        public bool Set_sdate_null
        {
            get => m_dayVisible; set
            {
                m_dayVisible = value;
                sbtn_ADD_DAY.Visible = value;
                sbtn_MINUS_DAY.Visible = value;
                sbtn_TODAY.Visible = value;
                tableLayoutPanel1.ColumnStyles[0].Width = value == false ? 0 : 20;
                tableLayoutPanel1.ColumnStyles[1].Width = value == false ? 0 : 37;
                tableLayoutPanel1.ColumnStyles[2].Width = value == false ? 0 : 20;
                DisplayFormat();
            }
        }
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

		private void btn_YEAR_Click(object sender, EventArgs e)
		{
			int year = DateTime.Today.Year;
			int month = DateTime.Today.Month;

			dt_START.DateTime = new DateTime(year, 1, 1);
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
		private void btn_MINUS_YEAR_Click(object sender, EventArgs e)
		{
			SetYear(-1);
		}



		private void btn_PLUS_YEAR_Click(object sender, EventArgs e)
		{
			SetYear(1);
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

			jNKCDate = new JNKCDateTime();
			
			jNKCDate.DateChanged += DateTime_DateChanged;
            dt_START.EditValue = DBNull.Value;
            dt_END.EditValue = DBNull.Value;
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
			dt_START.Properties.EditMask = _format;
			dt_END.Properties.EditMask = _format;

		}

		public void DisplayFormat()
		{
			if (m_yearVisible)
			{
				DisplayFormat("yyyy");
				dt_START.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
				dt_START.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
				dt_END.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
				dt_END.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
				//if (this.Width < 20 + 37 + 20 + 2 + 20 + 37 + 20 + 2 + 20 + 37 + 20 + 80) this.Width = 20 + 37 + 20 + 2 + 20 + 37 + 20 + 2 + 20 + 37 + 20 + 100;
			}
			if (m_monthVisible)
			{
				DisplayFormat("yyyy-MM");
				dt_START.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
				dt_START.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
				dt_END.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
				dt_END.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
				//if (this.Width < 20 + 37 + 20 + 2 + 20 + 37 + 20 + 80) this.Width = 20 + 37 + 20 + 2 + 20 + 37 + 20 + 100;
			}
			if (m_dayVisible)
			{
				DisplayFormat("yyyy-MM-dd");
				dt_START.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.MonthView;
				dt_START.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView;
				dt_END.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.MonthView;
				dt_END.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView;

				////if (this.Width < 20 + 37 + 20) this.Width = 20 + 37 + 20 + 100;

			}
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

		private void SetYear(int value)
		{
			// 날짜를 1월 1일 ~ 월말 까지 한달로 설정
			int year = dt_START.DateTime.AddYears(value).Year;

			dt_START.DateTime = new DateTime(year, 1, 1);
			dt_END.DateTime = new DateTime(year, 12 , 31); // 매 년 마지막 일 획득
		}

		#endregion


		private void dt_START_EditValueChanged(object sender, EventArgs e)
		{

            DevExpress.XtraEditors.DateEdit dt = sender as DevExpress.XtraEditors.DateEdit;

            if (dt == null) return;
            if (dt.DateTime == DateTime.MinValue) return;
            if (dt_END.DateTime == DateTime.MinValue) return;
            if (dt_START.DateTime >= dt_END.DateTime)
                dt_END.DateTime = dt_START.DateTime;
           
        }

		private void dt_END_EditValueChanged(object sender, EventArgs e)
		{
			if (dt_START.DateTime >= dt_END.DateTime)
				dt_START.DateTime = dt_END.DateTime;
		}

		private void DateTime_DateChanged(object sender , EventArgs e)
		{
			if (jNKCDate == null) return;
			if (jNKCDate.START == null) jNKCDate.START = DateTime.Now;
			if (jNKCDate.END == null) JNKCDate.END = DateTime.Now;
			dt_END.EditValue = jNKCDate.END;
			dt_START.EditValue = jNKCDate.START;
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

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public interface IJNKCDateTime
	{
		DateTime START { get; set; }
		DateTime END { get; set; }
		event EventHandler DateChanged;

	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JNKCDateTime : IDisposable , IJNKCDateTime
	{
		public event EventHandler DateChanged;
		private DateTime m_start;
		private DateTime m_end;
		[Description("시작일"), DisplayName("시작일")]
		public DateTime START { get => m_start;
		set {
				m_start = value;
		OnDateChanged(m_start, new EventArgs()); 
		}
		}

		[Description("종료일"), DisplayName("종료일")]
		public DateTime END {
			get => m_end; 
		set {
				m_end = value;
		OnDateChanged(m_end, new EventArgs()); 
		}
		}

		public JNKCDateTime(DateTime s , DateTime e)
		{
			m_start = s;
			m_end = e;
		}
		public JNKCDateTime()
		{
			m_start = new DateTime();
			m_end = new DateTime();
		}

		public void OnDateChanged(object sender , EventArgs e)
		{
			if (DateChanged == null) return;
			DateChanged(sender , e);

		}
		
			#region IDisposable Support
			private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.
				if (DateChanged != null) DateChanged = null;
				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		// ~JNKCDateTime() {
		//   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
		//   Dispose(false);
		// }

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		void IDisposable.Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			// GC.SuppressFinalize(this);
		}
		#endregion
        
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class JNKCDateTimeOneDate : IDisposable, IJNKCDateTime
	{
		public event EventHandler DateChanged;
		private DateTime m_start;
		private DateTime m_end;
		[Description("시작일"), DisplayName("시작일")]
		public DateTime START
		{
			get => m_start;
			set
			{
				m_start = value;
				m_end = value;
				OnDateChanged(m_start, new EventArgs());
			}
		}

		[Browsable(false)]
		[Description("종료일"), DisplayName("종료일")]
		public DateTime END
		{
			get => m_end;
			set
			{
				m_end = value;
				OnDateChanged(m_end, new EventArgs());
			}
		}

		public JNKCDateTimeOneDate(DateTime s, DateTime e)
		{
			m_start = s;
			m_end = e;
		}
		public JNKCDateTimeOneDate()
		{
			m_start = DateTime.Now;
			m_end = DateTime.Now;
		}

		public void OnDateChanged(object sender, EventArgs e)
		{
			if (DateChanged == null) return;
			DateChanged(sender, e);

		}

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
				}

				// TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
				// TODO: 큰 필드를 null로 설정합니다.
				if (DateChanged != null) DateChanged = null;
				disposedValue = true;
			}
		}

		// TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
		// ~JNKCDateTime() {
		//   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
		//   Dispose(false);
		// }

		// 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
		void IDisposable.Dispose()
		{
			// 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
			Dispose(true);
			// TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
			// GC.SuppressFinalize(this);
		}
		#endregion


	}
}