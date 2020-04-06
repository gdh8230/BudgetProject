using System;
using System.IO;

namespace Core
{
    public class LogUtil
    {
        private const String LOG_PREFIX = ".\\LOG\\";       //Log 폴더
        private const String LOG_EXT = ".LOG";              //Log 파일 확장자

        /// <summary>
        /// 로그파일 쓰기
        /// </summary>
        /// <param name="strMsg">Log 내용</param>
        public static void WriteLog(String strMsg)
        {
            DateTime dtNow = DateTime.Now;

            string strPath = String.Format("{0}{1}{2}", LOG_PREFIX, dtNow.ToString("yyyyMMdd"), LOG_EXT);
            string strDir  = Path.GetDirectoryName(strPath);

            DirectoryInfo diDir = new DirectoryInfo(strDir);

            string strLog = String.Format("[{0}] {1}", dtNow.ToString("yyyy/MM/dd HH:mm:ss"), strMsg);

            try
            {
                if (!diDir.Exists)
                {
                    diDir.Create();
                    diDir = new DirectoryInfo(strDir);  
                }

                if (diDir.Exists)
                {
                    System.IO.StreamWriter swStream = File.AppendText(strPath);
                    swStream.WriteLine(strLog);
                    swStream.Close();
                }
            }
            catch (IOException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }
        }

    }
}
