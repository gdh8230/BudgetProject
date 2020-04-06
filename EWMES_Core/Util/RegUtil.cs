using System;
using Microsoft.Win32;

namespace DH_Core
{
    public class RegUtil
    {
        /// <summary>
        /// ������Ʈ�� �б�
        /// </summary>
        /// <param name="root">������Ʈ�� LocalMachine ���� ��Ʈ ���</param>
        /// <param name="sub">��Ʈ ��� ���� ���</param>
        /// <param name="name">������Ʈ�� Ű</param>
        /// <returns>Ű ��</returns>
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
        /// ���� ��� ��ȸ
        /// </summary>
        /// <param name="root">������Ʈ�� LocalMachine ���� ��Ʈ ���</param>
        /// <param name="sub">��Ʈ ��� ���� ���</param>
        /// <param name="write">�б�/���� ����</param>
        /// <returns>���� ���</returns>
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
        /// ������Ʈ�� ����
        /// </summary>
        /// <param name="root">������Ʈ�� LocalMachine ���� ��Ʈ ���</param>
        /// <param name="sub">��Ʈ ��� ���� ���</param>
        /// <param name="name">������Ʈ�� Ű</param>
        /// <param name="objValue">Ű ��</param>
        /// <returns>���� ���� ����</returns>
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
