using System;
using Microsoft.Win32;

namespace DH_Core
{
    public class RegUtil
    {
        /// <summary>
        /// 레지스트리 읽기
        /// </summary>
        /// <param name="root">레지스트리 LocalMachine 하위 루트 노드</param>
        /// <param name="sub">루트 노드 하위 노드</param>
        /// <param name="name">레지스트리 키</param>
        /// <returns>키 값</returns>
        public static String GetReg(String root, String[] sub, String name)
        {
            String rtnValue = "";
            RegistryKey rkSubKey;

            rkSubKey = SearchReg(root, sub, false);

            try
            {
                rtnValue = rkSubKey.GetValue(name, "").ToString();
                rkSubKey.Close();
            }
            catch (Exception)
            {
                rtnValue = "";
            }

            return rtnValue;
        }

        /// <summary>
        /// 하위 노드 조회
        /// </summary>
        /// <param name="root">레지스트리 LocalMachine 하위 루트 노드</param>
        /// <param name="sub">루트 노드 하위 노드</param>
        /// <param name="write">읽기/쓰기 구분</param>
        /// <returns>하위 노드</returns>
        private static RegistryKey SearchReg(String root, String[] sub, bool write)
        {
            RegistryKey rootKey;
            RegistryKey subKey;

            try
            {
                rootKey = Registry.LocalMachine.OpenSubKey(root, write);
                subKey = rootKey.OpenSubKey(sub[0], write);

                for (int i = 1; i < sub.Length; i++)
                {
                    subKey = subKey.OpenSubKey(sub[i], write);
                }

                return subKey;
            }
            catch (Exception)
            {
                return null;
            }
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
            bool rtnValue = false;
            RegistryKey rkSubKey;

            rkSubKey = SearchReg(root, sub, true);

            try
            {
                rkSubKey.SetValue(name, objValue);
                rkSubKey.Close();
                rtnValue = true;
            }
            catch (Exception)
            {
                rtnValue = false;
            }

            return rtnValue;
        }
    }
}
