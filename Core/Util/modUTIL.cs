using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
//using DevExpress.XtraEditors;
//using DevExpress.XtraEditors.Repository;
using System.Data;
//using DevExpress.XtraGrid.Views.Grid;
using System.Data.OleDb;

namespace Core
{
    public class modUTIL
    {
        #region AppUtil 구현
        /// <summary>
        /// 정수형 여부 검사
        /// </summary>
        /// <param name="number">확인할 문자</param>
        /// <returns>정수형 : Ture</returns>
        public static bool IsNumber(String number)
        {
            return AppUtil.IsNumber(number);
        }

        /// <summary>
        /// 몫 구하기.(Long)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">몫</param>
        public static void Divide(long dividend, long divisor, out long rtnValue)
        {
            AppUtil.Divide(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// 몫 구하기.(INT)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">몫</param>
        public static void Divide(int dividend, int divisor, out int rtnValue)
        {
            AppUtil.Divide(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// 반올림(Double)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">반올림할 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Round(double value, int num, out double rtnValue)
        {
            AppUtil.Round(value, num, out rtnValue);
        }

        /// <summary>
        /// 반올림(Float)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">반올림할 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Round(float value, int num, out float rtnValue)
        {
            AppUtil.Round(value, num, out rtnValue);
        }

        /// <summary>
        /// 소수점절사(Double)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">나타낼 소수점 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void DecimalCut(double value, int num, out double rtnValue)
        {
            AppUtil.DecimalCut(value, num, out rtnValue);
        }

        /// <summary>
        /// 소수점절사(Float)
        /// </summary>
        /// <param name="value">소수</param>
        /// <param name="num">나타낼 소수점 자리수</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void DecimalCut(float value, int num, out float rtnValue)
        {
            AppUtil.DecimalCut(value, num, out rtnValue);
        }

        /// <summary>
        /// 나머지 구하기.(Long)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">나머지</param>
        public static void DivRem(long dividend, long divisor, out long rtnValue)
        {
            AppUtil.DivRem(dividend, divisor, out rtnValue);
        }

        /// <summary>
        /// 나머지 구하기.(INT)
        /// </summary>
        /// <param name="dividend">피제수</param>
        /// <param name="divisor">제수</param>
        /// <param name="rtnValue">나머지</param>
        public static void DivRem(int dividend, int divisor, out int rtnValue)
        {
            AppUtil.DivRem(dividend, divisor, out rtnValue);
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
            return AppUtil.FormatNumber(strNumber, format, len);
        }

        /// <summary>
        /// 문자열을 소수형으로 변환. (Float)
        /// </summary>
        /// <param name="str">소수 문자열</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Str2Number(String str, out float rtnValue)
        {
            AppUtil.Str2Number(str, out rtnValue);
        }

        /// <summary>
        /// 문자열을 소수형으로 변환. (Double)
        /// </summary>
        /// <param name="str">소수 문자열</param>
        /// <param name="rtnValue">변환된 소수</param>
        public static void Str2Number(String str, out double rtnValue)
        {
            AppUtil.Str2Number(str, out rtnValue);
        }

        /// <summary>
        /// 문자열을 정수형으로 변환.(INT)
        /// </summary>
        /// <param name="str">숫자 문자열</param>
        /// <param name="rtnValue">변환된 정수</param>
        public static void Str2Number(String str, out int rtnValue)
        {
            AppUtil.Str2Number(str, out rtnValue);
        }

        /// <summary>
        /// 문자열을 긴 정수형으로 변환.(LONG)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rtnValue">변환된 정수</param>
        public static void Str2Number(String str, out long rtnValue)
        {
            AppUtil.Str2Number(str, out rtnValue);
        }

        /// <summary>
        /// 널 검사
        /// </summary>
        /// <param name="str">확인할 문자</param>
        /// <returns>NULL : True</returns>
        public static bool IsNull(String str)
        {
            return AppUtil.IsNull(str);
        }

        /// <summary>
        /// 널이면 빈문자열, 그렇지 않으면 원래 문자열로 변환.
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환값</returns>
        public static String Null2Str(String str)
        {
            return AppUtil.Null2Str(str);
        }

        /// <summary>
        /// 한글 문자열 길이.
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>문자열 바이트 크기</returns>
        public static int StrHLen(String str)
        {
            return AppUtil.StrHLen(str);
        }

        /// <summary>
        /// 길이 구하기.       
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>문자열 길이</returns>
        public static int StrLen(String str)
        {
            return AppUtil.StrLen(str);
        }

        /// <summary>
        /// Trim. 앞/뒤 공백 제거
        /// </summary>
        /// <param name="str">공백 제거 할 문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrTrim(String str)
        {
            return AppUtil.StrTrim(str);
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
            return AppUtil.StrReplace(str, old, replacement);
        }

        /// <summary>
        /// Split기능을 사용한 문자 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="delim">자를 문자열</param>
        /// <returns>변환된 문자열 배열</returns>
        public static String[] StrSplit(String str, String delim)
        {
            return AppUtil.StrSplit(str, delim);
        }

        /// <summary>
        /// 특정문자열 존재유무
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="fine">검색할 문자열</param>
        /// <returns>존재유무</returns>
        public static bool InStr(String str, String fine)
        {
            return AppUtil.InStr(str, fine);
        }

        /// <summary>
        /// 문자열 왼쪽에서 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="len">잘라낼 길이</param>
        /// <returns>변환된 문자열</returns>
        public static String StrLeftDel(String str, int len)
        {
            return AppUtil.StrLeftDel(str, len);
        }

        /// <summary>
        /// 문자열 오른쪽에서 자르기
        /// </summary>
        /// <param name="str">문자열</param>
        /// <param name="len">잘라낼 길이</param>
        /// <returns>변환된 문자열</returns>
        public static String StrRightDel(String str, int len)
        {
            return AppUtil.StrRightDel(str, len);
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
            return AppUtil.SubStr(str, num, len);
        }

        /// <summary>
        /// 대문자로 변환
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrToUpperd(String str)
        {
            return AppUtil.StrToUpperd(str);
        }

        /// <summary>
        /// 소문자로 변환
        /// </summary>
        /// <param name="str">문자열</param>
        /// <returns>변환된 문자열</returns>
        public static String StrToLower(String str)
        {
            return AppUtil.StrToLower(str);
        }

        /// <summary>
        /// 문자열비교
        /// </summary>
        /// <param name="str1">비교할 문자열1</param>
        /// <param name="str2">비교할 문자열2</param>
        /// <returns>일치여부</returns>
        public static bool StrCompareTo(String str1, String str2)
        {
            return AppUtil.StrCompareTo(str1, str2);
        }

        /// <summary>
        /// 날짜 형식 검사.
        /// </summary>
        /// <param name="strDate">문자열</param>
        /// <returns>날짜 형식 여부</returns>
        public static bool IsDate(String strDate)
        {
            return AppUtil.IsDate(strDate);
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
            return AppUtil.AddDate(strDate, addDay, format);
        }

        /// <summary>
        /// 날짜 형식 변환
        /// </summary>
        /// <param name="strDate">변환할 날짜 형식 문자열</param>
        /// <param name="format">형식</param>
        /// <returns>변환된 날짜</returns>
        public static String FormatDate(String strDate, String format)
        {
            return AppUtil.FormatDate(strDate, format);
        }

        /// <summary>
        /// 문자열을 날짜형으로 변환
        /// </summary>
        /// <param name="str">변환할 문자열</param>
        /// <returns>변환된 날짜</returns>
        public static DateTime Str2Date(String str)
        {
            return AppUtil.Str2Date(str);
        }

        /// <summary>
        /// 시간 차이 계산
        /// </summary>
        /// <param name="val1">비교할 시간1</param>
        /// <param name="val2">비교할 시간2</param>
        /// <returns>시간 차이 : 초단위</returns>
        public static long DiffTime(String val1, String val2)
        {
            return AppUtil.DiffTime(val1, val2);
        }

        /// <summary>
        /// 해당월 마지막 일자 구하기.
        /// </summary>
        /// <param name="strDate">날짜 문자열</param>
        /// <returns>마지막 일자</returns>
        public static int LastDateOfMon(String strDate)
        {
            return AppUtil.LastDateOfMon(strDate);
        }

        /// <summary>
        /// 해당날짜에 대한 월,화,수,목,금,토,일 반환.
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static String GetWeek(String strDate)
        {
            return AppUtil.GetWeek(strDate);
        }

        /// <summary>
        /// 시스템 날짜 구하기.
        /// </summary>
        /// <returns>시스템 현재 시간</returns>
        public static String SysDate()
        {
            return AppUtil.SysDate();
        }

        /// <summary>
        /// 윈도우실행유무(MDI폼에서 동일한 Child폼 실행)
        /// </summary>
        /// <param name="mdiForm">MDI폼</param>
        /// <param name="childForm">Child폼</param>
        /// <returns>폼 실행 여부</returns>
        public static bool ActiveForm(Form mdiForm, Form childForm)
        {
            return AppUtil.ActiveForm(mdiForm, childForm);
        }

        /// <summary>
        /// MDI Main 폼에서 서브폼 실행
        /// </summary>
        /// <param name="mdiForm">MDI Main 폼</param>
        /// <param name="childForm">실행 시킬 자식 폼</param>
        public static void SubFormLoad(Form mdiForm, Form childForm)
        {
            AppUtil.SubFormLoad(mdiForm, childForm);
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
            return AppUtil.GetForm(assemName, nameSpace, formName, constructor);
        }

        /// <summary>
        /// MDI Main 폼에 열려져있는 자식 폼들을 모두 닫는다.
        /// </summary>
        /// <param name="mdiForm"></param>
        public static void SubFormClose(Form mdiForm)
        {
            AppUtil.SubFormClose(mdiForm);
        }

        /// <summary>
        /// 프로그램 실행 여부 확인
        /// </summary>
        public static bool AppState()
        {
            return AppUtil.AppState();
        }

        /// <summary>
        /// 판넬에 Title을 설정한다.
        /// </summary>
        /// <param name="strTitle">Title 내용</param>
        /// <param name="pnlTitle">Title이 설정될 판넬</param>
        /// <param name="size">Title 크기</param>
        public static void SetTitle(String strTitle, Panel pnlTitle, float size)
        {
            AppUtil.SetTitle(strTitle, pnlTitle, size);
        }

        /// <summary>
        /// ComboBox List 구성
        /// </summary>
        /// <param name="cboBox"   >셋팅할 콤보박스</param>
        /// <param name="keyValues">콤보박스 리스트에 대입할 리스트객체KeyValuePair</param>
        /// <param name="key"      >화면에 보이는 항목의 텍스트</param>
        /// <param name="value"    >각 항목에 등록된 값</param>
        public static void SetComboBox(System.Windows.Forms.ComboBox cboBox, List<KeyValuePair<string, string>> keyValues, String key, String value)
        {
            AppUtil.SetComboBox(cboBox, keyValues, key, value);
        }

        /// <summary>
        /// ComboBox List 구성
        /// </summary>
        /// <typeparam name="T"    >List 타입</typeparam>
        /// <param name="cboBox"   >셋팅할 콤보박스</param>
        /// <param name="keyValues">콤보박스 리스트에 대입할 리스트객체</param>
        /// <param name="key"      >각 항목에 등록된 값</param>
        /// <param name="value"    >화면에 보이는 항목의 텍스트</param>
        public static void SetComboBox<T>(System.Windows.Forms.ComboBox cboBox, List<T> keyValues, String key, String value)
        {
            AppUtil.SetComboBox<T>(cboBox, keyValues, key, value);
        }

        /// <summary>
        /// Text 박스에 숫자만 입력
        /// </summary>
        /// <param name="key">TextBox Key</param>
        public static void CheckTextNumber(KeyPressEventArgs key)
        {
            AppUtil.CheckTextNumber(key);
        }

        /// <summary>
        /// IP 입력 값인 0~255 사이에 값만 입력되도록 한다
        /// </summary>
        /// <param name="txt">값을 처리할 TextBox</param>
        public static void CheckTextIP(TextBox txt)
        {
            AppUtil.CheckTextIP(txt);
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
            return AppUtil.ConvertImageToByteArray(imageToConvert, formatOfImage);
        }

        /// <summary>
        /// 현재Host의 IP를 가져온다. (2010-05-03)
        /// </summary>
        /// <returns>스트링형식의 IP값</returns>
        public static String GetHostIP()
        {
            return AppUtil.GetHostIP();
        }

        /// <summary>
        /// 마우스WaitCursor
        /// </summary>
        public static void MouseWaitCursor()
        {
            AppUtil.MouseWaitCursor();
        }

        /// <summary>
        /// 마우스DefaultCursor
        /// </summary>
        public static void MouseDefaultCursor()
        {
            AppUtil.MouseDefaultCursor();
        }

        /// <summary>
        /// out_msg check
        /// </summary>
        /// <param name="out_msg">out_msg</param>
        /// <returns>1 is true, else false</returns>
        public static bool checkOutMsg(String out_msg)
        {
            //1이면 true, 1이 아니면 false
            if (out_msg.Equals("1")) return true;
            else
                return false;
        }
        #endregion

        #region RegUtil 구현
        /// <summary>
        /// 레지스트리 읽기
        /// </summary>
        /// <param name="root">레지스트리 LocalMachine 하위 루트 노드</param>
        /// <param name="sub">루트 노드 하위 노드</param>
        /// <param name="name">레지스트리 키</param>
        /// <returns>키 값</returns>
        public static String GetReg(String root, String[] sub, String name)
        {
            return RegUtil.GetReg(root, sub, name);
        }

        /// <summary>
        /// 레지스트리 쓰기
        /// </summary>
        /// <param name="root">레지스트리 LocalMachine 하위 루트 노드</param>
        /// <param name="sub">루트 노드 하위 노드</param>
        /// <param name="name">레지스트리 키</param>
        /// <param name="objValue">키 값</param>
        /// <returns>쓰기 성공 여부</returns>
        public static bool SetReg(String root, String[] sub, String name, String objValue)
        {
            return SetReg(root, sub, name, objValue);
        }
        #endregion

        #region LogUtil 구현
        /// <summary>
        /// 로그파일 쓰기
        /// </summary>
        /// <param name="strMsg">Log 내용</param>
        public static void WriteLog(String strMsg)
        {
            LogUtil.WriteLog(strMsg);
        }
        #endregion

        ///// <summary>
        ///// DevExpress Editor SearchlookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dt">데이터테이블</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //#region DevEditorUtil
        //public static void DevLookUpEditorSet(SearchLookUpEdit lue, DataTable dt, String key, String value)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, dt, key, value);
        //}

        ///// <summary>
        ///// DevExpress Editor lookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dt">데이터테이블</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //public static void DevLookUpEditorSet(LookUpEdit lue, DataTable dt, String key, String value)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, dt, key, value);
        //}
        ///// <summary>
        ///// DevExpress Editor lookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dt">데이터테이블</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //public static void DevLookUpEditorSet(LookUpEdit lue, DataView dv, String key, String value)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, dv, key, value);
        //}

        ///// <summary>
        ///// DevExpress Editor lookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dt">데이터테이블</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //public static void DevLookUpEditorSet(RepositoryItemLookUpEdit lue, DataTable dt, String key, String value, String keytext, String valuetext)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, dt, key, value, keytext, valuetext);
        //}
        ///// <summary>
        ///// DevExpress Editor lookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dv">데이터뷰</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //public static void DevLookUpEditorSet(RepositoryItemLookUpEdit lue, DataView dv, String key, String value, String keytext, String valuetext)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, dv, key, value, keytext, valuetext);
        //}

        ///// <summary>
        ///// DevExpress Editor lookUpEditor 세팅
        ///// </summary>
        ///// <param name="lue">룩업에디트</param>
        ///// <param name="dt">데이터테이블</param>
        ///// <param name="key">키 이름</param>
        ///// <param name="value">값 이름</param>
        //public static void DevLookUpEditorSet(LookUpEdit lue, List<KeyValuePair<String, String>> listKeyValuePair)
        //{
        //    DevEditorUtil.DevLookUpEditorSet(lue, listKeyValuePair);
        //}

        ///// <summary>
        ///// XtraEditors.Repository.RepositoryItemLookUpEdit 의 리스트 삽입
        ///// </summary>
        ///// <param name="LookupEdit">컨트롤</param>
        ///// <param name="list">KeyValuePair리스트</param>
        ///// <param name="isGridIn"> </param>
        //public static void DevRepositoryLookUpEditorSet(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit RepLookupEdit, List<KeyValuePair<String, String>> list)
        //{
        //    DevEditorUtil.DevRepositoryLookUpEditorSet(RepLookupEdit, list);
        //}
        //#endregion

        //#region DevGridUtil
        ///// <summary>
        ///// Excel 파일 생성
        ///// </summary>
        ///// <param name="table">그리드 데이타</param>
        ///// <param name="gv">그리드 뷰</param>
        //public static void DevCreateExcel(GridView gv)
        //{
        //    DevGridUtil.DevCreateExcel(gv);
        //}

        ///// <summary>
        ///// Excel 파일 생성
        ///// </summary>
        ///// <param name="data">그리드 데이타</param>
        ///// <param name="gv  ">그리드 뷰</param>
        //public static void DevCreateExcel(object data, GridView gv, string Fname)
        //{
        //    DevGridUtil.DevCreateExcel((DataTable)data, gv, Fname);
        //}

        ///// <summary>
        ///// Excel 파일 생성
        ///// </summary>
        ///// <param name="table">그리드 데이타</param>
        ///// <param name="gv">그리드 뷰</param>
        //public static void DevCreateExcel(DataTable table, GridView gv)
        //{
        //    DevGridUtil.DevCreateExcel(table, gv);
        //}
        //#endregion

        #region ExcelUtil 구현

        /// <summary>
        /// Excel File Open 첫 Sheet 획득
        /// </summary>
        /// <param name="strFilePath"> File 경로 </param>
        /// <param name="sheetNo"> Sheet 위치 </param>
        /// <returns> File DataTable </returns>
        public static DataTable ReadExcel(string strFilePath, int sheetNo)
        {
            // 엑셀 문서 내용 추출
            object missing = System.Reflection.Missing.Value;

            string strProvider = string.Empty;
            strProvider = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strFilePath + @";Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""";

            OleDbConnection excelConnection = new OleDbConnection(strProvider);
            excelConnection.Open();

            var dtSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheet = Convert.ToString(dtSchema.Rows[sheetNo].Field<object>("TABLE_NAME"));
            //OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [{0}]", sheet1), conn);

            string strQuery = String.Format("SELECT * FROM [{0}]", sheet); //"SELECT * FROM [Sheet1$]";

            OleDbCommand dbCommand = new OleDbCommand(strQuery, excelConnection);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(dbCommand);

            DataTable dTable = new DataTable();
            dataAdapter.Fill(dTable);

            dataAdapter.Dispose();
            dbCommand.Dispose();

            excelConnection.Close();
            excelConnection.Dispose();

            return dTable;
        }
        #endregion


    }
}
