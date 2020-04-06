using DevExpress.LookAndFeel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main1
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Check())
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    DevExpress.Skins.SkinManager.EnableFormSkins();
                    DevExpress.Utils.AppearanceObject.DefaultFont = new Font("맑은 고딕", 9);
                    DevExpress.UserSkins.BonusSkins.Register();
                    //UserLookAndFeel.Default.SetSkinStyle("Li");

                    Application.Run(new Login());
                    //Application.Run(new Main());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("실행상의 문제로 종료됩니다. \n이유:\n" + ex.Message);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("이미 실행중입니다.");
                Application.Exit();
            }
        }
        private static bool Check()
        {
            //return true;
            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
            string strProc = process.ProcessName;
            if (System.Diagnostics.Process.GetProcessesByName(strProc).Length > 1) return false;
            else return true;
        }
    }
}