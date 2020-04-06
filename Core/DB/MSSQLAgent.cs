using System;

//신규추가
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Core.DB
{
    #region SQLParameterCollection
    /// <summary>
    /// DBParameterCollection의 요약 설명입니다.
    /// </summary>
    public class SQLParameterCollection : CollectionBase
    {
        // 인덱스를 재정의.
        public SqlParameter this[int index]
        {
            get
            {
                return ((SqlParameter)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }
        public int Add(SqlParameter value)
        {
            return (List.Add(value));
        }

        public int IndexOf(SqlParameter value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, SqlParameter value)
        {
            List.Insert(index, value);
        }

        public void Remove(SqlParameter value)
        {
            List.Remove(value);
        }

        public bool Contains(SqlParameter value)
        {
            // 만약 value가 SqlParameter타입이 아니면, false가 리턴된다.
            return (List.Contains(value));
        }
    }
    #endregion
    #region MSSQLAgent
    /// <summary>
    /// SQLAgent의 요약 설명입니다.
    /// </summary>
    public class MSSQLAgent
    {
        /// <summary>
        /// DBFieldType
        /// </summary>
        public struct DBFieldType
        {
            public const byte String = 0;
            public const byte Number = 1;
            public const byte Date = 2;
            public const byte LongRaw = 3;
            public const byte Blob = 4;
            public const byte Image = 5; //2010-04-12
            public const byte Structure = 6;
        }

        /// <summary>
        /// DBDirection
        /// </summary>
        public struct DBDirection
        {
            public const byte Input = 0;
            public const byte Output = 1;
            public const byte InputOutput = 2;
        }

        private static readonly String DEFAULT_DATASET_NAME = "VirtualDataSet";
        private static readonly String DEFAULT_TABLE_NAME = "VirtualTable";

        private static SqlConnection CONN;

        private SqlDataAdapter DA;             //DataAdapter
        private SqlTransaction TA;             //트렌젝션
        private SqlCommand CMD;            //데이터베이스에 대해 실행할 Transact-SQL 문이나 저장 프로시저 
        private DataSet DS;             //데이터
        private string PROCNAME;       //프로시저명

        public SQLParameterCollection Parameters = new SQLParameterCollection();     //파라미터

        private static String conStr;         //DB 연결 정보

        public static String ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }

        public static String DEFAULT_TABLE_NAME1
        {
            get { return DEFAULT_TABLE_NAME; }
        }

        public SqlConnection Connection
        {
            get { return CONN; }
            set { CONN = value; }
        }
        public string ProcedureName
        {
            get { return PROCNAME; }
            set { PROCNAME = value; }
        }
        /// <summary>
        /// DB 연결 상태를 반환
        /// </summary>
        /// <returns>연결상태</returns>
        public bool DBConnectState()
        {
            bool rtnValue = false;

            if (CONN != null)
            {
                switch (CONN.State)
                {
                    case ConnectionState.Open:
                        rtnValue = true;
                        break;
                    case ConnectionState.Fetching:
                        rtnValue = true;
                        break;
                    case ConnectionState.Executing:
                        rtnValue = true;
                        break;
                    case ConnectionState.Connecting:
                        rtnValue = true;
                        break;
                    default:
                        rtnValue = false;
                        break;
                }
            }
                                         
            return rtnValue;
        }

        /// <summary>
        /// DB 연결
        /// </summary>
        /// <param name="constr">Connect 정보</param>
        public bool DBConnect(out string error_msg)
        {
            error_msg = string.Empty;
            bool result = false;
            try
            {
                if(CONN == null)
                    CONN = new SqlConnection(conStr);
                CONN.Open();

                CMD = new SqlCommand();
                CMD.CommandTimeout = 600;
                result = true;
            }
            catch (SqlException e)
            {
                error_msg = e.Message;
                LogUtil.WriteLog("DB Connection Err : " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                error_msg = e.Message;
                LogUtil.WriteLog("DB Connection Err : " + e.Message);
                return false;
            }
            return result;
        }
        /// <summary>
        /// 반환값 없는 프로지서 실행
        /// </summary>
        public bool ExecuteNonQuery()
        {
            bool rtnValue = false;

            if (CONN == null)
                CONN = new SqlConnection(conStr);
            CONN.Open();
            try
            {
                CMD.Connection = CONN;
                CMD.CommandText = PROCNAME;
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Clear();

                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }

                CMD.ExecuteNonQuery();
                rtnValue = true;
            }
            catch (SqlException e)
            {
                LogUtil.WriteLog("ExecuteNonQuery Err : " + e.Message);
                rtnValue = false;
            }
            catch (Exception e)
            {
                LogUtil.WriteLog("ExecuteNonQuery Err : " + e.Message);
                rtnValue = false;
            }
            //progressForm.processThread.Close();
            return rtnValue;
        }
        /// <summary>
        /// 반환값 없는 프로지서 실행
        /// </summary>
        /// <param name="out_msg">에러 메시지를 담을 변수</param>
        public void ExecuteNonQuery(out string out_msg1, out string out_msg2)
        {
            out_msg1 = String.Empty;
            out_msg2 = String.Empty;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandText = PROCNAME;
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Clear();

                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }

                CMD.ExecuteNonQuery();

                if (CONN == null) return;
                try
                {
                    out_msg1 = CMD.Parameters["RESFLAG"].Value.ToString();
                    out_msg2 = CMD.Parameters["RESULT"].Value.ToString();
                }
                catch
                { }
            }
            catch (SqlException e)
            {
                LogUtil.WriteLog("ExecuteNonQuery Err : " + e.Message);
            }
            catch (Exception e)
            {
                LogUtil.WriteLog("ExecuteNonQuery Err : " + e.Message);
            }
            if (String.IsNullOrEmpty(out_msg1))
            {
                out_msg1 = String.Empty;
            }
            if (String.IsNullOrEmpty(out_msg2))
            {
                out_msg2 = String.Empty;
            }
        }
        /// <summary>
        /// DB 연결 해체
        /// </summary>
        public void DBClose()
        {
            Parameters.Clear();
            CMD = new SqlCommand();
            CMD.CommandTimeout = 600;
            PROCNAME = String.Empty;
            CONN.Close();
        }

        /// <summary>
        /// 쿼리 작동을 위한 초기화
        /// </summary>
        public void ClearDB()
        {
            PROCNAME = String.Empty;
            ClearParameter();
        }

        /// <summary>
        /// 파라미터 초기화
        /// </summary>
        public void ClearParameter()
        {
            if (Parameters != null) Parameters.Clear();
        }

        /// <summary>
        /// 파라미터 추가
        /// </summary>
        /// <param name="Para">파라미터</param>
        public void AddParameter(SqlParameter Para)
        {
            Parameters.Add(Para);
        }

        /// <summary>
        /// 파라미터 추가
        /// </summary>
        /// <param name="ParameterName">파라미터 명</param>
        /// <param name="ParameterValue">파티미터 값</param>
        public void AddParameter(string ParameterName, string ParameterValue)
        {
            AddParameter(ParameterName, DBFieldType.String, ParameterValue, 0, DBDirection.Input);
        }

        /// <summary>
        /// 파라미터 추가
        /// </summary>
        /// <param name="ParameterName">파라미터명</param>
        /// <param name="ParameterType">파라미터 타입</param>
        /// <param name="ParameterValue">파라미터 값</param>
        public void AddParameter(string ParameterName, byte ParameterType, object ParameterValue)
        {
            AddParameter(ParameterName, ParameterType, ParameterValue, 0, DBDirection.Input);
        }

        /// <summary>
        /// 파라미터 추가
        /// </summary>
        /// <param name="ParameterName">파라미터명</param>
        /// <param name="ParameterType">파라미터 타입</param>
        /// <param name="ParameterValue">파라미터 값</param>
        /// <param name="Size">파라미터 크기</param>
        /// <param name="ParaDirection">DB 실행 타입</param>
        public void AddParameter(string ParameterName, byte ParameterType, object ParameterValue, int Size, byte ParaDirection)
        {
            SqlDbType dbType = SqlDbType.VarChar;

            //if (!DBConnectState()) DBConnect();

            switch (ParameterType)
            {
                case DBFieldType.String:
                    dbType = SqlDbType.VarChar;
                    break;
                case DBFieldType.Number:
                    dbType = SqlDbType.Decimal;
                    break;
                case DBFieldType.Date:
                    dbType = SqlDbType.DateTime;
                    break;
                case DBFieldType.LongRaw:
                    dbType = SqlDbType.Binary;
                    break;
                case DBFieldType.Blob:
                    dbType = SqlDbType.Binary;
                    break;
                case DBFieldType.Image:         //2010-04-12
                    dbType = SqlDbType.Image;
                    break;
                case DBFieldType.Structure:
                    dbType = SqlDbType.Structured;
                    break;
                default:
                    break;
            }

            try
            {
                if (Size == 0)
                {
                    SqlParameter Para = new SqlParameter(ParameterName, dbType);
                    if (ParameterValue == null || ParameterValue.ToString() == "")
                        Para.Value = null;
                    else
                        Para.Value = ParameterValue;
                    switch (ParaDirection)
                    {
                        case DBDirection.Input:
                            Para.Direction = ParameterDirection.Input;
                            break;

                        case DBDirection.Output:
                            Para.Direction = ParameterDirection.Output;
                            break;

                        case DBDirection.InputOutput:
                            Para.Direction = ParameterDirection.InputOutput;
                            break;
                    }
                    Parameters.Add(Para);
                }
                else
                {
                    SqlParameter Para = new SqlParameter(ParameterName, dbType);
                    switch (ParaDirection)
                    {
                        case DBDirection.Input:
                            Para.Direction = ParameterDirection.Input;
                            Para.Size = Size;
                            break;

                        case DBDirection.Output:
                            Para.Direction = ParameterDirection.Output;
                            Para.Size = Size;
                            break;

                        case DBDirection.InputOutput:
                            Para.Direction = ParameterDirection.InputOutput;
                            Para.Size = Size;
                            break;
                    }
                    Parameters.Add(Para);
                }
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                throw;
            }
        }

        public DataSet GetDataSetQuery(out String error_msg)
        {
            error_msg = String.Empty;
            DS = null;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandText = PROCNAME;
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Clear();
                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }
                DA = new SqlDataAdapter(CMD);
                DS = new DataSet(DEFAULT_DATASET_NAME);
                DA.Fill(DS, DEFAULT_TABLE_NAME);
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                DS = null;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                DS = null;
            }
            return DS;
        }

        public DataSet GetDataSetQuery(string query,out String error_msg)
        {
            error_msg = String.Empty;
            DS = null;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandText = query;
                CMD.CommandType = CommandType.Text;

                CMD.Parameters.Clear();

                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }

                DA = new SqlDataAdapter(CMD);
                DS = new DataSet(DEFAULT_DATASET_NAME);
                DA.Fill(DS, DEFAULT_TABLE_NAME);
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                DS = null;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                DS = null;
            }
            return DS;
        }
        /// <summary>
        /// 프로시저 쿼리 수행후 리턴값을 DataSet로 반환
        /// </summary>
        /// <returns>조회된 결과</returns>
   
        /// <summary>
        /// 프로시저 조회 결과를 DataTable로 반환
        /// </summary>
        /// <returns>조회된 결과</returns>
        public DataTable GetDataTableQuery(out string error_msg)
        {
            error_msg = String.Empty;
            DataTable dt = null;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandText = PROCNAME;
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Clear();

                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }

                DA = new SqlDataAdapter(CMD);
                dt = new DataTable(DEFAULT_TABLE_NAME);
                DA.Fill(dt);
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                dt = null;
                error_msg = ex.Message;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                dt = null;
                error_msg = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// 일반 Sql 조회 결과를 DataTable로 반환
        /// </summary>
        /// <param name="query">쿼리</param>
        /// <param name="Err">에러 메시지를 담을 변수</param>
        /// <returns>조회 결과</returns>
        public DataTable GetDataTableQuery(String query, out String error_msg)
        {
            error_msg = String.Empty;
            DataTable dt = null;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = query;

                CMD.Parameters.Clear();
                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }
                DA = new SqlDataAdapter(CMD);
                dt = new DataTable(DEFAULT_TABLE_NAME);
                DA.Fill(dt);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                dt = null;
            }
            return dt;
        }
        
        /// <summary>
        /// 프로시저 실행
        /// </summary>
        /// <param name="Index">connection index</param>
        /// <param name="error_msg">error message</param>
        /// <returns>성공여부</returns>
        public bool ExecuteNonQuery(out string error_msg)
        {
            bool result = false;
            error_msg = String.Empty;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandText = PROCNAME;
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Clear();

                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }

                CMD.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                result = false;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 성공여부반환 일반 쿼리문
        /// </summary>
        /// <param name="pSql">쿼리</param>
        /// <param name="out_msg">에러 메시지를 담을 변수</param>
        public bool ExecuteSQLQuery(string pSql, out string error_msg)
        {
            bool result = false;
            error_msg = String.Empty;
            try
            {
                if (!DBConnectState())
                    CONN.Open();
                CMD.Connection = CONN;
                CMD.CommandType = CommandType.Text;
                CMD.CommandText = pSql;

                CMD.Parameters.Clear();
                foreach (SqlParameter Para in Parameters)
                {
                    CMD.Parameters.Add(Para);
                }
                CMD.ExecuteNonQuery();

                error_msg = String.Empty;
                result = true;
            }
            catch (SqlException ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog(DateTime.Now.ToString("yy-MM-dd hh:mm:ss") + "\r\n[stacktrace] : " + ex.StackTrace + "\r\n[message] : " + ex.Message);
                error_msg = ex.Message;
                return false;
            }
            return result;
        }
        public void BeginTrans()
        {
            try
            {
                TA = CONN.BeginTransaction();
                CMD.Transaction = TA;
            }
            catch
            {
            }
        }

        /// <summary>
        /// DB RollBack
        /// </summary>
        public void Rollback()
        {
            try
            {
                TA.Rollback();
                TA = null;
            }
            catch
            {
            }
        }

        /// <summary>
        /// DB Commit
        /// </summary>
        public void Commit()
        {
            try
            {
                TA.Commit();
                TA = null;
            }
            catch
            {
            }
        }
        public SqlCommand getSqlCommand()
        {
            return this.CMD;
        }
    }
    #endregion
}//End Of Namespace
