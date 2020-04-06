using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Net;

namespace Core
{
    public class AppUtil
    {
        private const String FMT_DATE = "yyyy-MM-dd";
        private const String FMT_TIME = "HH:mm:ss";
        private const String FMT_DATE_TIME = "yyyy-MM-dd HH:mm:ss";

        

//-----------------------------------------------------------------------------------------------------
//숫자 및 계산
//-----------------------------------------------------------------------------------------------------
        #region 숫자 및 계산
        /// <summary>
        /// 정수형 여부 검사
        /// </summary>
        /// <param name="number">확인할 문자</param>
        /// <returns>정수형 : Ture</returns>
        public static bool IsNumber(String number)
        {
            if (String.IsNullOrEmpty(number)) return false;

            try
            {
                Convert.ToDouble(number);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 몫 구하기.(Long)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">몫</param>
        public static void Divide(long dividend, long divisor, out long rtnValue)
        {
            rtnValue = 0;

            if (dividend == 0) return;
            if (divisor == 0)
            {
                rtnValue = dividend;
                return;
            }

            rtnValue = dividend / divisor;
        }

        /// <summary>
        /// 몫 구하기.(INT)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">몫</param>
        public static void Divide(int dividend, int divisor, out int rtnValue)
        {
            rtnValue = 0;

            if (dividend == 0) return;
            if (divisor == 0)
            {
                rtnValue = dividend;
                return;
            }

            rtnValue = dividend / divisor;
        }

        /// <summary>
        /// 반올림(Double)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">반올림할 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Round(double value, int num, out double rtnValue)
        {
            rtnValue = 0;

            try
            {
                rtnValue = System.Math.Round(value, num);
            }
            catch (Exception)
            {
                rtnValue = value;
            }
        }

        /// <summary>
        /// 반올림(Float)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">반올림할 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Round(float value, int num, out float rtnValue)
        {
            rtnValue = 0;

            try
            {
                rtnValue = (float)System.Math.Round(value, num);
            }
            catch (Exception)
            {
                rtnValue = value;
            }
        }

        /// <summary>
        /// 소수점절사(Double)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">나타낼 소수점 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void DecimalCut(double value, int num, out double rtnValue)
        {
            rtnValue = 0;
            String strTmp = "";

            try
            {
                strTmp = String.Format("{0:F" + num + "}", value);

                rtnValue = Convert.ToDouble(strTmp);
            }
            catch (Exception)
            {
                rtnValue = value;
            }
        }

        /// <summary>
        /// 소수점절사(Float)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">나타낼 소수점 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void DecimalCut(float value, int num, out float rtnValue)
        {
            rtnValue = 0;
            String strTmp = "";

            try
            {
                strTmp = String.Format("{0:F" + num + "}", value);

                rtnValue = Convert.ToSingle(strTmp);
            }
            catch (Exception)
            {
                rtnValue = value;
            }
        }

        /// <summary>
        /// 나머지 구하기.(Long)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">나머지</param>
        public static void DivRem(long dividend, long divisor, out long rtnValue)
        {
            rtnValue = 0;

            System.Math.DivRem(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// 나머지 구하기.(INT)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">나머지</param>
        public static void DivRem(int dividend, int divisor, out int rtnValue)
        {
            rtnValue = 0;

            System.Math.DivRem(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// 숫자 문자열을 원하는 숫자 형식으로 변환
        /// </summary>
        /// <param name="strNumber">변환할 숫자 문자열</param>
        /// <param name="format">변환 형식(ex : "C")</param>
        /// <param name="len">자리수</param>
        /// <returns>변환된 숫자 문자열</returns>
        public static String FormatNumber(String strNumber, String format, int len)
        {
            String rtnValue = "";

            try
            {
                if (!IsNumber(strNumber)) return strNumber;

                if (strNumber.Contains("."))
                {
                    rtnValue = String.Format("{0:" + format + len + "}", Convert.ToDouble(strNumber));
                }
                else
                {
                    rtnValue = String.Format("{0:" + format + len + "}", Convert.ToInt64(strNumber));
                }
            }
            catch (Exception)
            {
                rtnValue = strNumber;
            }

            return rtnValue;
        }

        /// <summary>
        /// 문자열을 소수형으로 변환. (Float)
        /// </summary>
        /// <param name="str">소수 문자열</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Str2Number(String str, out float rtnValue)
        {
            rtnValue = (float)StrCastNumber(str, "Float");
        }

        /// <summary>
        /// 문자열을 소수형으로 변환. (Double)
        /// </summary>
        /// <param name="str">소수 문자열</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Str2Number(String str, out double rtnValue)
        {
            rtnValue = (double)StrCastNumber(str, "Double");
        }

        /// <summary>
        /// 문자열을 정수형으로 변환.(INT)
        /// </summary>
        /// <param name="str">숫자 문자열</param>
        /// <param name="rtnValue">변환된 정수</param>
        public static void Str2Number(String str, out int rtnValue)
        {
            rtnValue = (int)StrCastNumber(str, "INT");
        }

        /// <summary>
        /// 문자열을 긴 정수형으로 변환.(LONG)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rtnValue">변환된 정수</param>
        public static void Str2Number(String str, out long rtnValue)
        {
            rtnValue = (long)StrCastNumber(str, "LONG");
        }

        /// <summary>
        /// 숫자 타입으로 문자열을 변환
        /// </summary>
        /// <param name="str">숫자 문자열</param>
        /// <param name="convertType">변환할 타입</param>
        /// <returns>변환된 숫자</returns>
        public static object StrCastNumber(String str, String convertType)
        {
            object rtnValue;

            if (String.IsNullOrEmpty(str)) str = "0";
            
            try
            {
                switch (convertType.ToUpper())
                {
                    case "INT":
                        rtnValue = Convert.ToInt32(str);
                        break;
                    case "LONG":
                        rtnValue = Convert.ToInt64(str);
                        break;
                    case "DOUBLE":
                        rtnValue = Convert.ToDouble(str);
                        break;
                    case "FLOAT" :
                        rtnValue = Convert.ToSingle(str);
                        break;
                    case "DECIMAL":
                        rtnValue = Convert.ToDecimal(str);
                        break;
                    default:
                        rtnValue = 0;
                        break;
                }
            }
            catch (Exception)
            {
                rtnValue = 0;
            }

            return rtnValue;
        }
        #endregion

//-----------------------------------------------------------------------------------------------------
//문자
//-----------------------------------------------------------------------------------------------------

        #region 문자
        /// <summary>
        /// 널 검사
        /// </summary>
        /// <param name="str">확인할 문자</param>
        /// <returns>NULL : True</returns>
        public static bool IsNull(String str)
        {
            bool rtnValue = false;

            if (str == null)
            {
                return true;
            }

            return rtnValue;
        }

        /// <summary>
        /// 널이면 빈문자열, 그렇지 않으면 원래 문자열로 변환.
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환값</returns>
        public static String Null2Str(String str)
        {
            if (str == null) str = "";
            return str;
        }

        /// <summary>
        /// 한글 문자열 길이.
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>문자열 바이트 크기</returns>
        public static int StrHLen(String str)
        {
            int rtnValue = 0;

            if (IsNull(str)) str = "";

            try
            {
                rtnValue = Encoding.Default.GetByteCount(str);
            }
            catch (Exception)
            {
                rtnValue = 0;
            }

            return rtnValue;
        }

        /// <summary>
        /// 길이 구하기.       
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>문자열 길이</returns>
        public static int StrLen(String str)
        {
            int rtnValue = 0;

            if (IsNull(str)) str = "";

            try
            {
                rtnValue = str.Length;
            }
            catch (Exception)
            {
                rtnValue = 0;
            }

            return rtnValue;
        }

        /// <summary>
        /// Trim. 앞/뒤 공백 제거
        /// </summary>
        /// <param name="str">공백 제거 할 문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrTrim(String str)
        {
            String tmp;

            tmp = Null2Str(str);

            return tmp.Trim();
        }

        /// <summary>
        /// 문자열 에서 원하는 문자를 변환
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="old">변환할 문자</param>
        /// <param name="replacement">변환될 문자</param>
        /// <returns>변환된 문자열</returns>
        public static String StrReplace(String str, String old, String replacement)
        {
            String tmp = "";

            if (IsNull(str) || IsNull(old) || IsNull(replacement)) return str;

            tmp = str.Replace(old, replacement);

            return tmp;
        }

        /// <summary>
        /// Split기능을 사용한 문자 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="delim">자를 문자열</param>
        /// <returns>변환된 문자열 배열</returns>
        public static String[] StrSplit(String str, String delim)
        {
            String[] strTmp;

            if (IsNull(str) || IsNull(delim)) return null;

            strTmp = str.Split(delim.ToCharArray());

            return strTmp;
        }

        /// <summary>
        /// 특정문자열 존재유무
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="fine">검색할 문자열</param>
        /// <returns>존재유무</returns>
        public static bool InStr(String str, String fine)
        {

            if (String.IsNullOrEmpty(str) || String.IsNullOrEmpty(fine)) return false;

            if (str.IndexOf(fine) > -1) return true;
            else return false;
        }

        /// <summary>
        /// 문자열 왼쪽에서 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="len">잘라낼 길이</param>
        /// <returns>변환된 문자열</returns>
        public static String StrLeftDel(String str, int len)
        {
            String rtnValue = "";

            rtnValue = Null2Str(str);

            if (rtnValue.Length > len) rtnValue = str.Substring(len, str.Length - len);
            else if (rtnValue.Length <= len) rtnValue = "";

            return rtnValue;
        }

        /// <summary>
        /// 문자열 오른쪽에서 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="len">잘라낼 길이</param>
        /// <returns>변환된 문자열</returns>
        public static String StrRightDel(String str, int len)
        {
            String rtnValue = "";

            rtnValue = Null2Str(str);

            if (rtnValue.Length > len) rtnValue = str.Substring(0, str.Length - len);
            else if (rtnValue.Length <= len) rtnValue = "";

            return rtnValue;
        }

        /// <summary>
        /// 문자열 원하는 자리에서 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="num">잘라낼 문자열 시작 위치</param>
        /// <param name="len">잘라낼 문자열 길이</param>
        /// <returns>변환된 문자열</returns>
        public static String SubStr(String str, int num, int len)
        {
            String rtnValue = "";

            if(String.IsNullOrEmpty(str)) return "";

            if (str.Length > num)
                if (str.Length >= (num + len)) rtnValue = str.Substring(num, len);
                else
                    rtnValue = str.Substring(num, str.Length - num);
            else
                rtnValue = str;

            return rtnValue;
        }

        /// <summary>
        /// 대문자로 변환
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrToUpperd(String str)
        {
            String rtnValue = "";

            if (String.IsNullOrEmpty(str)) return "";

            rtnValue = str.ToUpper();

            return rtnValue;
        }

        /// <summary>
        /// 소문자로 변환
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrToLower(String str)
        {
            String rtnValue = "";

            if (String.IsNullOrEmpty(str)) return "";

            rtnValue = str.ToLower();

            return rtnValue;
        }

        /// <summary>
        /// 문자열비교
        /// </summary>
        /// <param name="str1">비교할 문자열1</param>
        /// <param name="str2">비교할 문자열2</param>
        /// <returns>일치여부</returns>
        public static bool StrCompareTo(String str1, String str2)
        {
            str1 = Null2Str(str1);
            str2 = Null2Str(str2);

            return str1.Equals(str2);
        }
        #endregion

//-----------------------------------------------------------------------------------------------------
//Date Time...
//-----------------------------------------------------------------------------------------------------
        #region 날짜
        /// <summary>
        /// 날짜 형식 검사.
        /// </summary>
        /// <param name="strDate">문자열</param>
        /// <returns>날짜 형식 여부</returns>
        public static bool IsDate(String strDate)
        {
            DateTime date;
            bool rtnValue = false;

            try
            {
                date = Convert.ToDateTime(strDate);
                rtnValue = true;
            }
            catch (Exception)
            {
                rtnValue = false;
            }

            return rtnValue;
        }
        

        /// <summary>
        /// 날짜 증감 연산
        /// </summary>
        /// <param name="strDate">증가 시킬 날짜</param>
        /// <param name="addDay">증가할 일 수</param>
        /// <param name="format">반환할 문자형식</param>
        /// <returns></returns>
        public static String AddDate(String strDate, int addDay, String format)
        {
            DateTime tmpDate;

            tmpDate = Str2Date(strDate);

            tmpDate = tmpDate.AddDays(addDay);

            //return tmpDate.ToString(DEFAULT_FORMAT, DateTimeFormatInfo.InvariantInfo);
            return FormatDate(tmpDate.ToString(), format);
        }

        /// <summary>
        /// 날짜 형식 변환
        /// </summary>
        /// <param name="strDate">변환할 날짜 형식 문자열</param>
        /// <param name="format">형식</param>
        /// <returns>변환된 날짜</returns>
        public static String FormatDate(String strDate, String format)
        {
            DateTime tmpDate;

            tmpDate = Str2Date(strDate);

            return tmpDate.ToString(IsNull(format) ? FMT_DATE : format, DateTimeFormatInfo.InvariantInfo);
        }


        /// <summary>
        /// 문자열을 날짜형으로 변환
        /// </summary>
        /// <param name="str">변환할 문자열</param>
        /// <returns>변환된 날짜</returns>
        public static DateTime Str2Date(String str)
        {
            DateTime date;

            if (str.Length == 8) str = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);

            try
            {
                date = Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                date = DateTime.Now;
            }

            return date;
        }

        /// <summary>
        /// 문자열을 날짜형으로 변환2
        /// </summary>
        /// <param name="str">변환할 문자열</param>
        /// <returns>변환된 날짜</returns>
        public static TimeSpan DateDiff(DateTime str1, DateTime str2)
        {
            TimeSpan time;
            time = str2.Subtract(str1);
            return time;
        }




        /// <summary>
        /// 시간 차이 계산
        /// </summary>
        /// <param name="val1">비교할 시간1</param>
        /// <param name="val2">비교할 시간2</param>
        /// <returns>시간 차이 : 초단위</returns>
        public static long DiffTime(String val1, String val2)
        {
            DateTime date1 = Str2Date(val1);
            DateTime date2 = Str2Date(val2);
            TimeSpan ts = date2 - date1;

            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// 해당월 마지막 일자 구하기.
        /// </summary>
        /// <param name="strDate">날짜 문자열</param>
        /// <returns>마지막 일자</returns>
        public static int LastDateOfMon(String strDate)
        {
            DateTime date1 = Str2Date(strDate);
            DateTime date2 = new DateTime(date1.Year, date1.Month, 1);

            date2 = date2.AddMonths(1);

            date2 = date2.AddDays(-1);

            return date2.Day;
        }

        /// <summary>
        /// 해당날짜에 대한 월,화,수,목,금,토,일 반환.
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static String GetWeek(String strDate)
        {
            DateTime date = Str2Date(strDate);
            String rtnValue = "";
            rtnValue = date.ToString("ddd", new CultureInfo("ko-KR"));

            return rtnValue;
        }

        /// <summary>
        /// 시스템 날짜 구하기.
        /// </summary>
        /// <returns>시스템 현재 시간</returns>
        public static String SysDate()
        {
            return DateTime.Now.ToString(FMT_DATE_TIME);
        }

        public static int WeekDay(DateTime dateTime)
        {
            int tempWeekDay = 0;
            var dt = dateTime.DayOfWeek;
            switch (dt)
            {
                case DayOfWeek.Monday: //월
                    tempWeekDay = 1;
                    break;
                case DayOfWeek.Tuesday: //화
                    tempWeekDay = 2;
                    break;
                case DayOfWeek.Wednesday: //수
                    tempWeekDay = 3;
                    break;
                case DayOfWeek.Thursday: //목
                    tempWeekDay = 4;
                    break;
                case DayOfWeek.Friday: //금
                    tempWeekDay = 5;
                    break;
                case DayOfWeek.Saturday: //토
                    tempWeekDay = 6;
                    break;
                case DayOfWeek.Sunday: //일
                    tempWeekDay = 7;
                    break;
            }
            return tempWeekDay;
        }
        #endregion
//-----------------------------------------------------------------------------------------------------
//Form
//-----------------------------------------------------------------------------------------------------
        #region 폼
        /// <summary>
        /// 윈도우실행유무(MDI폼에서 동일한 Child폼 실행)
        /// </summary>
        /// <param name="mdiForm">MDI폼</param>
        /// <param name="childForm">Child폼</param>
        /// <returns>폼 실행 여부</returns>
        public static bool ActiveForm(Form mdiForm, Form childForm)
        {
            bool rtnValue = false;

            foreach (Form form in mdiForm.MdiChildren)
            {
                if (childForm.GetType() == form.GetType())
                {
                    rtnValue = true;
                    childForm.Dispose();
                    return rtnValue;
                }
            }            

            return rtnValue;
        }

        /// <summary>
        /// MDI Main 폼에서 서브폼 실행
        /// </summary>
        /// <param name="mdiForm">MDI Main 폼</param>
        /// <param name="childForm">실행 시킬 자식 폼</param>
        public static void SubFormLoad(Form mdiForm, Form childForm)
        {
            try
            {
                if ((mdiForm != null) && (childForm != null))
                {
                    if (!AppUtil.ActiveForm(mdiForm, childForm))
                    {
                        childForm.MdiParent = mdiForm;
                        childForm.TopMost = true;
                        childForm.StartPosition = FormStartPosition.Manual;
                        childForm.Show();
                    }
                    else
                    {
                        foreach (Form form in mdiForm.MdiChildren)
                        {
                            if (childForm.GetType() == form.GetType())
                            {
                                form.Activate();
                                if (!form.Visible)
                                {
                                    form.Visible = true;
                                }
                            }
                        }
                        childForm.Dispose();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// MDI Main 폼에서 서브폼 실행 ( 새창띄우기 임시)
        /// </summary>
        /// <param name="mdiForm">MDI Main 폼</param>
        /// <param name="childForm">실행 시킬 자식 폼</param>
        public static void SubFormLoad(Form mdiForm, Form childForm, bool WindowOpenType)
        {
            try
            {
                if (WindowOpenType)
                {
                    if ((mdiForm != null) && (childForm != null))
                    {
                        if (!AppUtil.ActiveForm(mdiForm, childForm))
                        {
                            //childForm.MdiParent = mdiForm;                            
                            childForm.TopMost = true;
                            childForm.StartPosition = FormStartPosition.Manual;
                            childForm.Show();
                        }
                        else
                        {
                            foreach (Form form in mdiForm.MdiChildren)
                            {
                                if (childForm.GetType() == form.GetType())
                                {
                                    form.Activate();
                                    if (!form.Visible)
                                    {
                                        form.Visible = true;
                                    }
                                }
                            }
                            childForm.Dispose();
                        }
                    }
                }
                else
                {
                    if ((mdiForm != null) && (childForm != null))
                    {
                        if (!AppUtil.ActiveForm(mdiForm, childForm))
                        {
                            childForm.MdiParent = mdiForm;
                            childForm.TopMost = true;
                            childForm.StartPosition = FormStartPosition.Manual;
                            childForm.Show();
                        }
                        else
                        {
                            foreach (Form form in mdiForm.MdiChildren)
                            {
                                if (childForm.GetType() == form.GetType())
                                {
                                    form.Activate();
                                    if (!form.Visible)
                                    {
                                        form.Visible = true;
                                    }
                                }
                            }
                            childForm.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// MDI Main 폼에 열려져있는 자식 폼들을 모두 닫는다.
        /// </summary>
        /// <param name="mdiForm"></param>
        public static void SubFormClose(Form mdiForm)
        {
            foreach (Form form in mdiForm.MdiChildren)
            {
                form.Close();
            }
        }

        /// <summary>
        /// 폼명으로 해당 폼에 Instance를 구한다.
        /// </summary>
        /// <param name="nameSpace">폼이 속한 Name Space</param>
        /// <param name="formName">폼명</param>
        /// <param name="constructor">생성자 변수</param>
        /// <returns>폼</returns>
        public static Form GetForm(String assemName, String nameSpace, String formName, object[] constructor)
        {
            try
            {
                //string fullName = nameSpace + "." + formName;
                //Type frmType = Type.GetType(fullName);
                //object o = Activator.CreateInstance(frmType, constructor);
                string fullName = nameSpace + "." + formName;
                Assembly alib = Assembly.Load(assemName);
                Type frmType = alib.GetType(fullName);
                object o = Activator.CreateInstance(frmType, null);

                Form frm = o as Form;

                return frm;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
//-----------------------------------------------------------------------------------------------------
//기타
//-----------------------------------------------------------------------------------------------------
        #region 기타
        /// <summary>
        /// 프로그램 실행 여부 확인
        /// </summary>
        public static bool AppState()
        {
            System.Diagnostics.Process[] myProc = System.Diagnostics.Process.GetProcessesByName(Application.ProductName);

            if (myProc.Length < 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// List 데이터를 확인한다
        /// </summary>
        /// <returns>List 데이터 상태</returns>
        public static bool CheckList<T>(List<T> list)
        {
            if (list == null) return false;
            if (list.Count < 1) return false;
            return true;
        }

        /// <summary>
        /// 판넬에 Title을 설정한다.
        /// </summary>
        /// <param name="strTitle">Title 내용</param>
        /// <param name="pnlTitle">Title이 설정될 판넬</param>
        /// <param name="size">Title 크기</param>
        public static void SetTitle(String strTitle, Panel pnlTitle, float size)
        {
            Label title = new Label();
            title.Dock = DockStyle.Fill;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("굴림", size, System.Drawing.FontStyle.Bold);
            title.Text = strTitle;
            title.Parent = pnlTitle;
        }

        /// <summary>
        /// ComboBox List 구성
        /// </summary>
        /// <param name="cboBox"   >셋팅할 콤보박스</param>
        /// <param name="keyValues">콤보박스 리스트에 대입할 리스트객체KeyValuePair</param>
        /// <param name="key"      >각 항목에 등록된 값</param>
        /// <param name="value"    >화면에 보이는 항목의 텍스트</param>
        public static void SetComboBox(ComboBox cboBox, List<KeyValuePair<string, string>> keyValues, String key, String value)
        {
            cboBox.DataSource = keyValues;
            cboBox.DisplayMember = value;
            cboBox.ValueMember = key;
        }


        /// <summary>
        /// ComboBox List 구성
        /// </summary>
        /// <typeparam name="T"    >List 타입</typeparam>
        /// <param name="cboBox"   >셋팅할 콤보박스</param>
        /// <param name="keyValues">콤보박스 리스트에 대입할 리스트객체</param>
        /// <param name="key"      >각 항목에 등록된 값</param>
        /// <param name="value"    >화면에 보이는 항목의 텍스트</param>
        public static void SetComboBox<T>(ComboBox cboBox, List<T> keyValues, String key, String value)
        {
            cboBox.DataSource    = keyValues;
            cboBox.DisplayMember = value;
            cboBox.ValueMember   = key;
        }

        /// <summary>
        /// Text 박스에 숫자만 입력
        /// </summary>
        /// <param name="key">TextBox Key</param>
        public static void CheckTextNumber(KeyPressEventArgs key)
        {
            if (!(Char.IsDigit(key.KeyChar)) && key.KeyChar != 8)
            {
                key.Handled = true;
            }
        }

        /// <summary>
        /// IP 입력 값인 0~255 사이에 값만 입력되도록 한다
        /// </summary>
        /// <param name="txt">값을 처리할 TextBox</param>
        public static void CheckTextIP(TextBox txt)
        {
            if (IsNumber(txt.Text))
            {
                int key = 0;

                Str2Number(txt.Text, out key);

                if (key > 255)
                {
                    txt.Text = "255";
                }
            }
            else
            {
                txt.Text = "0";
            }
        }

        /// <summary>
        /// 이미지를 바이트어레이로 변환 (2010-04-15)
        /// </summary>
        /// <param name="imageToConvert">이미지</param>
        /// <param name="formatOfImage">이미지형식</param>
        /// <returns>byte[] 형식으로 변환된 값</returns>
        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Value;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Value = ms.ToArray();
                }               
            }
            catch (Exception) { return null; }
            return Value;
        }

        /// <summary>
        /// 현재Host의 IP를 가져온다. (2010-05-03)
        /// </summary>
        /// <returns>스트링형식의 IP값</returns>
        public static String GetHostIP()
        {
            String Value = "";;
            IPAddress ip = null;
            IPHostEntry host = null;

            try
            {                
                host = Dns.GetHostEntry(Dns.GetHostName());
                for (int i = 0; i < host.AddressList.Length; i++)
                {
                    ip = host.AddressList[i];

                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork       // InterNetWork 4 인것만 조회
                        && (host.AddressList[i].ToString().Substring(0,2) == "59"))                  // 59. 으로시작하는 IP만 보임 (TMC)
                    {
                        Value = ip.ToString();
                    }
                }                
            }
            catch 
            {
                return null;
            }

            return Value;
        }


        /// <summary>/// 클라이언트 IP 주소 얻어오기...
        /// </summary>
        public static string Client_IP
        {    
            get    
            {        
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());        
                string ClientIP = string.Empty;        
                for (int i = 0; i < host.AddressList.Length; i++)        
                {            
                    if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)            
                    {                
                        ClientIP = host.AddressList[i].ToString();            
                    }        
                }        
                return ClientIP;    
            }
        }
        /// <summary>
        /// 파일크기(Byte) 를 반환
        /// </summary>
        /// <param name="filePath">파일경로</param>
        /// <returns>파일크기(Byte)</returns>
        public static long GetFileSize(string filePath){return new FileInfo(filePath).Length;}

        /// <summary>
        /// 마우스WaitCursor
        /// </summary>
        public static void MouseWaitCursor()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        /// <summary>
        /// 마우스DefaultCursor
        /// </summary>
        public static void MouseDefaultCursor()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        public static string getUTF8String(string oriString)
        {
            byte[] stringByte = null;
            if (string.IsNullOrWhiteSpace(oriString))
                return string.Empty;
            else
            {
                stringByte = Encoding.Default.GetBytes(oriString);
                return Encoding.UTF8.GetString(stringByte);
            }
        }

        ///// <summary>
        ///// Loading화면 팝업
        ///// </summary>
        //public static void showLoading()
        //{
        //    SplashScreenManager.ShowForm(typeof(LoadingForm));
        //}
        ///// <summary>
        ///// Loading화면 닫기
        ///// </summary>
        //public static void closeLoading()
        //{
        //    SplashScreenManager.CloseForm(false);
        //}
        #endregion
    }
}
