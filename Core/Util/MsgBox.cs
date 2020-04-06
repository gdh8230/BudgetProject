using System;
using System.Windows.Forms;
//using DevExpress.XtraEditors;

namespace Core
{
    public class MsgBox
    {
        /// <summary>
        /// 공통 메세지
        /// </summary>
        public struct COMMON_MSG
        {
            public static readonly String MSG_QUESTION_EXIT  = "프로그램을 종료하시겠습니까?";
            public static readonly String MSG_ROLE_ERROR     = "권한이 없습니다!\n관리자에게 문의하세요!";
            public static readonly String MSG_BTN_SAVE       = "저장 하시겠습니까?";
            public static readonly String MSG_BTN_DELETE     = "삭제 하시겠습니까?";
            public static readonly String MSG_SAVE_DATA_NULL = "저장할 데이터가 없습니다.";
            public static readonly String MSG_SAVE_ERR       = "저장에 실패했습니다!";
            public static readonly String MSG_DEL_ERR        = "삭제에 실패했습니다!";
            public static readonly String MSG_SELECT_ERR     = "조회에 실패했습니다!";            
        }

        /// <summary>
        /// 메세지 박스 Infomation
        /// </summary>
        /// <param name="msg">메세지 내용</param>
        /// <param name="title">메세지 타이틀</param>
        public static void MsgInformation(String msg, String title)
        {
            //XtraMessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 메세지 박스 에러
        /// </summary>
        /// <param name="msg">메세지 내용</param>
        /// <param name="title">메세지 타이틀</param>
        public static void MsgErr(String msg, String title)
        {
            //XtraMessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 메세지 박스 질문
        /// </summary>
        /// <param name="msg">메세지 내용</param>
        /// <param name="title">메세지 타이틀</param>
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
        /// 메세지 박스 질문 (2010-09-08 추가)
        /// </summary>
        /// <param name="msg">메세지 내용</param>
        /// <param name="title">메세지 타이틀</param>
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
            //XtraMessageBox.Show(i + "번째 행 데이터가 잘못 되었습니다!"
            //                  + "\nERR : " + errMsg
            //                  + "\n취소 하실려면 ESC 키를 눌러주세요!", "List Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(i + "번째 행 데이터가 잘못 되었습니다!"
                              + "\nERR : " + errMsg
                              + "\n취소 하실려면 ESC 키를 눌러주세요!", "List Err", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
