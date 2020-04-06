using System;
using System.Windows.Forms;
//using DevExpress.XtraEditors;

namespace Core
{
    public class MsgBox
    {
        /// <summary>
        /// ���� �޼���
        /// </summary>
        public struct COMMON_MSG
        {
            public static readonly String MSG_QUESTION_EXIT  = "���α׷��� �����Ͻðڽ��ϱ�?";
            public static readonly String MSG_ROLE_ERROR     = "������ �����ϴ�!\n�����ڿ��� �����ϼ���!";
            public static readonly String MSG_BTN_SAVE       = "���� �Ͻðڽ��ϱ�?";
            public static readonly String MSG_BTN_DELETE     = "���� �Ͻðڽ��ϱ�?";
            public static readonly String MSG_SAVE_DATA_NULL = "������ �����Ͱ� �����ϴ�.";
            public static readonly String MSG_SAVE_ERR       = "���忡 �����߽��ϴ�!";
            public static readonly String MSG_DEL_ERR        = "������ �����߽��ϴ�!";
            public static readonly String MSG_SELECT_ERR     = "��ȸ�� �����߽��ϴ�!";            
        }

        /// <summary>
        /// �޼��� �ڽ� Infomation
        /// </summary>
        /// <param name="msg">�޼��� ����</param>
        /// <param name="title">�޼��� Ÿ��Ʋ</param>
        public static void MsgInformation(String msg, String title)
        {
            //XtraMessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// �޼��� �ڽ� ����
        /// </summary>
        /// <param name="msg">�޼��� ����</param>
        /// <param name="title">�޼��� Ÿ��Ʋ</param>
        public static void MsgErr(String msg, String title)
        {
            //XtraMessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// �޼��� �ڽ� ����
        /// </summary>
        /// <param name="msg">�޼��� ����</param>
        /// <param name="title">�޼��� Ÿ��Ʋ</param>
        /// <returns>Ok : true, Cancel : false</returns>
        public static bool MsgQuestion(String msg, String title)
        {
            //if (XtraMessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            if (MessageBox.Show(msg, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                return true;
                
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �޼��� �ڽ� ���� (2010-09-08 �߰�)
        /// </summary>
        /// <param name="msg">�޼��� ����</param>
        /// <param name="title">�޼��� Ÿ��Ʋ</param>
        /// <returns>Yes:1, No:2, Cancel:3</returns>
        public static int MsgQuestionYesNoCancel(String msg, String title)
        {
            //DialogResult ds = XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            DialogResult ds = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (ds.Equals(DialogResult.Yes))
            {
                return 1;
            }
            else if (ds.Equals(DialogResult.No))
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        public static void MsgDataGridErr(int i, String errMsg)
        {
            //XtraMessageBox.Show(i + "��° �� �����Ͱ� �߸� �Ǿ����ϴ�!"
            //                  + "\nERR : " + errMsg
            //                  + "\n��� �ϽǷ��� ESC Ű�� �����ּ���!", "List Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(i + "��° �� �����Ͱ� �߸� �Ǿ����ϴ�!"
                              + "\nERR : " + errMsg
                              + "\n��� �ϽǷ��� ESC Ű�� �����ּ���!", "List Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
