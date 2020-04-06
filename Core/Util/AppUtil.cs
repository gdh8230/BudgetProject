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
//���� �� ���
//-----------------------------------------------------------------------------------------------------
        #region ���� �� ���
        /// <summary>
        /// ������ ���� �˻�
        /// </summary>
        /// <param name="number">Ȯ���� ����</param>
        /// <returns>������ : Ture</returns>
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
        /// �� ���ϱ�.(Long)
        /// </summary>
        /// <param name="dividend">������</param>
        /// <param name="divisor">����</param>
        /// <param name="rtnValue">��</param>
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
        /// �� ���ϱ�.(INT)
        /// </summary>
        /// <param name="dividend">������</param>
        /// <param name="divisor">����</param>
        /// <param name="rtnValue">��</param>
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
        /// �ݿø�(Double)
        /// </summary>
        /// <param name="value">�Ҽ�</param>
        /// <param name="num">�ݿø��� �ڸ���</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
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
        /// �ݿø�(Float)
        /// </summary>
        /// <param name="value">�Ҽ�</param>
        /// <param name="num">�ݿø��� �ڸ���</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
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
        /// �Ҽ�������(Double)
        /// </summary>
        /// <param name="value">�Ҽ�</param>
        /// <param name="num">��Ÿ�� �Ҽ��� �ڸ���</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
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
        /// �Ҽ�������(Float)
        /// </summary>
        /// <param name="value">�Ҽ�</param>
        /// <param name="num">��Ÿ�� �Ҽ��� �ڸ���</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
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
        /// ������ ���ϱ�.(Long)
        /// </summary>
        /// <param name="dividend">������</param>
        /// <param name="divisor">����</param>
        /// <param name="rtnValue">������</param>
        public static void DivRem(long dividend, long divisor, out long rtnValue)
        {
            rtnValue = 0;

            System.Math.DivRem(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// ������ ���ϱ�.(INT)
        /// </summary>
        /// <param name="dividend">������</param>
        /// <param name="divisor">����</param>
        /// <param name="rtnValue">������</param>
        public static void DivRem(int dividend, int divisor, out int rtnValue)
        {
            rtnValue = 0;

            System.Math.DivRem(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// ���� ���ڿ��� ���ϴ� ���� �������� ��ȯ
        /// </summary>
        /// <param name="strNumber">��ȯ�� ���� ���ڿ�</param>
        /// <param name="format">��ȯ ����(ex : "C")</param>
        /// <param name="len">�ڸ���</param>
        /// <returns>��ȯ�� ���� ���ڿ�</returns>
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
        /// ���ڿ��� �Ҽ������� ��ȯ. (Float)
        /// </summary>
        /// <param name="str">�Ҽ� ���ڿ�</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
        public static void Str2Number(String str, out float rtnValue)
        {
            rtnValue = (float)StrCastNumber(str, "Float");
        }

        /// <summary>
        /// ���ڿ��� �Ҽ������� ��ȯ. (Double)
        /// </summary>
        /// <param name="str">�Ҽ� ���ڿ�</param>
        /// <param name="rtnValue">��ȯ�� �Ҽ�</param>
        public static void Str2Number(String str, out double rtnValue)
        {
            rtnValue = (double)StrCastNumber(str, "Double");
        }

        /// <summary>
        /// ���ڿ��� ���������� ��ȯ.(INT)
        /// </summary>
        /// <param name="str">���� ���ڿ�</param>
        /// <param name="rtnValue">��ȯ�� ����</param>
        public static void Str2Number(String str, out int rtnValue)
        {
            rtnValue = (int)StrCastNumber(str, "INT");
        }

        /// <summary>
        /// ���ڿ��� �� ���������� ��ȯ.(LONG)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rtnValue">��ȯ�� ����</param>
        public static void Str2Number(String str, out long rtnValue)
        {
            rtnValue = (long)StrCastNumber(str, "LONG");
        }

        /// <summary>
        /// ���� Ÿ������ ���ڿ��� ��ȯ
        /// </summary>
        /// <param name="str">���� ���ڿ�</param>
        /// <param name="convertType">��ȯ�� Ÿ��</param>
        /// <returns>��ȯ�� ����</returns>
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
//����
//-----------------------------------------------------------------------------------------------------

        #region ����
        /// <summary>
        /// �� �˻�
        /// </summary>
        /// <param name="str">Ȯ���� ����</param>
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
        /// ���̸� ���ڿ�, �׷��� ������ ���� ���ڿ��� ��ȯ.
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <returns>��ȯ��</returns>
        public static String Null2Str(String str)
        {
            if (str == null) str = "";
            return str;
        }

        /// <summary>
        /// �ѱ� ���ڿ� ����.
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <returns>���ڿ� ����Ʈ ũ��</returns>
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
        /// ���� ���ϱ�.       
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <returns>���ڿ� ����</returns>
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
        /// Trim. ��/�� ���� ����
        /// </summary>
        /// <param name="str">���� ���� �� ���ڿ�</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrTrim(String str)
        {
            String tmp;

            tmp = Null2Str(str);

            return tmp.Trim();
        }

        /// <summary>
        /// ���ڿ� ���� ���ϴ� ���ڸ� ��ȯ
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="old">��ȯ�� ����</param>
        /// <param name="replacement">��ȯ�� ����</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrReplace(String str, String old, String replacement)
        {
            String tmp = "";

            if (IsNull(str) || IsNull(old) || IsNull(replacement)) return str;

            tmp = str.Replace(old, replacement);

            return tmp;
        }

        /// <summary>
        /// Split����� ����� ���� �ڸ���
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="delim">�ڸ� ���ڿ�</param>
        /// <returns>��ȯ�� ���ڿ� �迭</returns>
        public static String[] StrSplit(String str, String delim)
        {
            String[] strTmp;

            if (IsNull(str) || IsNull(delim)) return null;

            strTmp = str.Split(delim.ToCharArray());

            return strTmp;
        }

        /// <summary>
        /// Ư�����ڿ� ��������
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="fine">�˻��� ���ڿ�</param>
        /// <returns>��������</returns>
        public static bool InStr(String str, String fine)
        {

            if (String.IsNullOrEmpty(str) || String.IsNullOrEmpty(fine)) return false;

            if (str.IndexOf(fine) > -1) return true;
            else return false;
        }

        /// <summary>
        /// ���ڿ� ���ʿ��� �ڸ���
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="len">�߶� ����</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrLeftDel(String str, int len)
        {
            String rtnValue = "";

            rtnValue = Null2Str(str);

            if (rtnValue.Length > len) rtnValue = str.Substring(len, str.Length - len);
            else if (rtnValue.Length <= len) rtnValue = "";

            return rtnValue;
        }

        /// <summary>
        /// ���ڿ� �����ʿ��� �ڸ���
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="len">�߶� ����</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrRightDel(String str, int len)
        {
            String rtnValue = "";

            rtnValue = Null2Str(str);

            if (rtnValue.Length > len) rtnValue = str.Substring(0, str.Length - len);
            else if (rtnValue.Length <= len) rtnValue = "";

            return rtnValue;
        }

        /// <summary>
        /// ���ڿ� ���ϴ� �ڸ����� �ڸ���
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <param name="num">�߶� ���ڿ� ���� ��ġ</param>
        /// <param name="len">�߶� ���ڿ� ����</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
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
        /// �빮�ڷ� ��ȯ
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrToUpperd(String str)
        {
            String rtnValue = "";

            if (String.IsNullOrEmpty(str)) return "";

            rtnValue = str.ToUpper();

            return rtnValue;
        }

        /// <summary>
        /// �ҹ��ڷ� ��ȯ
        /// </summary>
        /// <param name="str">���ڿ�</param>
        /// <returns>��ȯ�� ���ڿ�</returns>
        public static String StrToLower(String str)
        {
            String rtnValue = "";

            if (String.IsNullOrEmpty(str)) return "";

            rtnValue = str.ToLower();

            return rtnValue;
        }

        /// <summary>
        /// ���ڿ���
        /// </summary>
        /// <param name="str1">���� ���ڿ�1</param>
        /// <param name="str2">���� ���ڿ�2</param>
        /// <returns>��ġ����</returns>
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
        #region ��¥
        /// <summary>
        /// ��¥ ���� �˻�.
        /// </summary>
        /// <param name="strDate">���ڿ�</param>
        /// <returns>��¥ ���� ����</returns>
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
        /// ��¥ ���� ����
        /// </summary>
        /// <param name="strDate">���� ��ų ��¥</param>
        /// <param name="addDay">������ �� ��</param>
        /// <param name="format">��ȯ�� ��������</param>
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
        /// ��¥ ���� ��ȯ
        /// </summary>
        /// <param name="strDate">��ȯ�� ��¥ ���� ���ڿ�</param>
        /// <param name="format">����</param>
        /// <returns>��ȯ�� ��¥</returns>
        public static String FormatDate(String strDate, String format)
        {
            DateTime tmpDate;

            tmpDate = Str2Date(strDate);

            return tmpDate.ToString(IsNull(format) ? FMT_DATE : format, DateTimeFormatInfo.InvariantInfo);
        }


        /// <summary>
        /// ���ڿ��� ��¥������ ��ȯ
        /// </summary>
        /// <param name="str">��ȯ�� ���ڿ�</param>
        /// <returns>��ȯ�� ��¥</returns>
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
        /// ���ڿ��� ��¥������ ��ȯ2
        /// </summary>
        /// <param name="str">��ȯ�� ���ڿ�</param>
        /// <returns>��ȯ�� ��¥</returns>
        public static TimeSpan DateDiff(DateTime str1, DateTime str2)
        {
            TimeSpan time;
            time = str2.Subtract(str1);
            return time;
        }




        /// <summary>
        /// �ð� ���� ���
        /// </summary>
        /// <param name="val1">���� �ð�1</param>
        /// <param name="val2">���� �ð�2</param>
        /// <returns>�ð� ���� : �ʴ���</returns>
        public static long DiffTime(String val1, String val2)
        {
            DateTime date1 = Str2Date(val1);
            DateTime date2 = Str2Date(val2);
            TimeSpan ts = date2 - date1;

            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// �ش�� ������ ���� ���ϱ�.
        /// </summary>
        /// <param name="strDate">��¥ ���ڿ�</param>
        /// <returns>������ ����</returns>
        public static int LastDateOfMon(String strDate)
        {
            DateTime date1 = Str2Date(strDate);
            DateTime date2 = new DateTime(date1.Year, date1.Month, 1);

            date2 = date2.AddMonths(1);

            date2 = date2.AddDays(-1);

            return date2.Day;
        }

        /// <summary>
        /// �ش糯¥�� ���� ��,ȭ,��,��,��,��,�� ��ȯ.
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
        /// �ý��� ��¥ ���ϱ�.
        /// </summary>
        /// <returns>�ý��� ���� �ð�</returns>
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
                case DayOfWeek.Monday: //��
                    tempWeekDay = 1;
                    break;
                case DayOfWeek.Tuesday: //ȭ
                    tempWeekDay = 2;
                    break;
                case DayOfWeek.Wednesday: //��
                    tempWeekDay = 3;
                    break;
                case DayOfWeek.Thursday: //��
                    tempWeekDay = 4;
                    break;
                case DayOfWeek.Friday: //��
                    tempWeekDay = 5;
                    break;
                case DayOfWeek.Saturday: //��
                    tempWeekDay = 6;
                    break;
                case DayOfWeek.Sunday: //��
                    tempWeekDay = 7;
                    break;
            }
            return tempWeekDay;
        }
        #endregion
//-----------------------------------------------------------------------------------------------------
//Form
//-----------------------------------------------------------------------------------------------------
        #region ��
        /// <summary>
        /// �������������(MDI������ ������ Child�� ����)
        /// </summary>
        /// <param name="mdiForm">MDI��</param>
        /// <param name="childForm">Child��</param>
        /// <returns>�� ���� ����</returns>
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
        /// MDI Main ������ ������ ����
        /// </summary>
        /// <param name="mdiForm">MDI Main ��</param>
        /// <param name="childForm">���� ��ų �ڽ� ��</param>
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
        /// MDI Main ������ ������ ���� ( ��â���� �ӽ�)
        /// </summary>
        /// <param name="mdiForm">MDI Main ��</param>
        /// <param name="childForm">���� ��ų �ڽ� ��</param>
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
        /// MDI Main ���� �������ִ� �ڽ� ������ ��� �ݴ´�.
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
        /// �������� �ش� ���� Instance�� ���Ѵ�.
        /// </summary>
        /// <param name="nameSpace">���� ���� Name Space</param>
        /// <param name="formName">����</param>
        /// <param name="constructor">������ ����</param>
        /// <returns>��</returns>
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
//��Ÿ
//-----------------------------------------------------------------------------------------------------
        #region ��Ÿ
        /// <summary>
        /// ���α׷� ���� ���� Ȯ��
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
        /// List �����͸� Ȯ���Ѵ�
        /// </summary>
        /// <returns>List ������ ����</returns>
        public static bool CheckList<T>(List<T> list)
        {
            if (list == null) return false;
            if (list.Count < 1) return false;
            return true;
        }

        /// <summary>
        /// �ǳڿ� Title�� �����Ѵ�.
        /// </summary>
        /// <param name="strTitle">Title ����</param>
        /// <param name="pnlTitle">Title�� ������ �ǳ�</param>
        /// <param name="size">Title ũ��</param>
        public static void SetTitle(String strTitle, Panel pnlTitle, float size)
        {
            Label title = new Label();
            title.Dock = DockStyle.Fill;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("����", size, System.Drawing.FontStyle.Bold);
            title.Text = strTitle;
            title.Parent = pnlTitle;
        }

        /// <summary>
        /// ComboBox List ����
        /// </summary>
        /// <param name="cboBox"   >������ �޺��ڽ�</param>
        /// <param name="keyValues">�޺��ڽ� ����Ʈ�� ������ ����Ʈ��üKeyValuePair</param>
        /// <param name="key"      >�� �׸� ��ϵ� ��</param>
        /// <param name="value"    >ȭ�鿡 ���̴� �׸��� �ؽ�Ʈ</param>
        public static void SetComboBox(ComboBox cboBox, List<KeyValuePair<string, string>> keyValues, String key, String value)
        {
            cboBox.DataSource = keyValues;
            cboBox.DisplayMember = value;
            cboBox.ValueMember = key;
        }


        /// <summary>
        /// ComboBox List ����
        /// </summary>
        /// <typeparam name="T"    >List Ÿ��</typeparam>
        /// <param name="cboBox"   >������ �޺��ڽ�</param>
        /// <param name="keyValues">�޺��ڽ� ����Ʈ�� ������ ����Ʈ��ü</param>
        /// <param name="key"      >�� �׸� ��ϵ� ��</param>
        /// <param name="value"    >ȭ�鿡 ���̴� �׸��� �ؽ�Ʈ</param>
        public static void SetComboBox<T>(ComboBox cboBox, List<T> keyValues, String key, String value)
        {
            cboBox.DataSource    = keyValues;
            cboBox.DisplayMember = value;
            cboBox.ValueMember   = key;
        }

        /// <summary>
        /// Text �ڽ��� ���ڸ� �Է�
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
        /// IP �Է� ���� 0~255 ���̿� ���� �Էµǵ��� �Ѵ�
        /// </summary>
        /// <param name="txt">���� ó���� TextBox</param>
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
        /// �̹����� ����Ʈ��̷� ��ȯ (2010-04-15)
        /// </summary>
        /// <param name="imageToConvert">�̹���</param>
        /// <param name="formatOfImage">�̹�������</param>
        /// <returns>byte[] �������� ��ȯ�� ��</returns>
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
        /// ����Host�� IP�� �����´�. (2010-05-03)
        /// </summary>
        /// <returns>��Ʈ�������� IP��</returns>
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

                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork       // InterNetWork 4 �ΰ͸� ��ȸ
                        && (host.AddressList[i].ToString().Substring(0,2) == "59"))                  // 59. ���ν����ϴ� IP�� ���� (TMC)
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


        /// <summary>/// Ŭ���̾�Ʈ IP �ּ� ������...
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
        /// ����ũ��(Byte) �� ��ȯ
        /// </summary>
        /// <param name="filePath">���ϰ��</param>
        /// <returns>����ũ��(Byte)</returns>
        public static long GetFileSize(string filePath){return new FileInfo(filePath).Length;}

        /// <summary>
        /// ���콺WaitCursor
        /// </summary>
        public static void MouseWaitCursor()
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
        }

        /// <summary>
        /// ���콺DefaultCursor
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
        ///// Loadingȭ�� �˾�
        ///// </summary>
        //public static void showLoading()
        //{
        //    SplashScreenManager.ShowForm(typeof(LoadingForm));
        //}
        ///// <summary>
        ///// Loadingȭ�� �ݱ�
        ///// </summary>
        //public static void closeLoading()
        //{
        //    SplashScreenManager.CloseForm(false);
        //}
        #endregion
    }
}
