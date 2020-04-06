using System.Drawing;
using System.Windows.Forms;

#region Usings
using System.Drawing.Drawing2D;
#endregion 
namespace DH_Core
{
    public partial class DXfrmMDI_Baseform : DevExpress.XtraEditors.XtraForm
    {

        public DXfrmMDI_Baseform()
        {
            InitializeComponent();
            Design_Setting();
        }


        #region Attributes (속성정의 집합)
        ////////////////////////////////
        //반투명 알파값
        private int percentAlpha = 70;
        //반투명 색상
        private Color pb = new Color();
        //그라데이션 브러쉬
        private LinearGradientBrush lineGBrush;
        //마우스로 폼드래그 하기
        private Point mCurrentPosition = new Point(0, 0);
        //마우스 드래그시 상단 타이틀인지 체크
        private bool titleMove = false;


        #endregion

        #region Functions (각 사용자정의 평션)
        ///////////////////////////////////


        /// <summary>
        /// 컨트롤박스 제거, 키프리뷰 설정, 하단그립 제거, 폼테두리 변경(Fixedsingle), 뎌블버퍼링 설정
        /// </summary>
        private void Design_Setting()
        {
            //스타일 변경
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            //색상설정
            pb = Color.FromArgb(percentAlpha * 255 / 100, Color.FromArgb(0, 128, 255));

            //그라데이션 설정
            lineGBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(this.Width, 0), Color.SkyBlue, pb);

            //라인브러쉬 블랜드 적용
            float[] relativelntensities = { 0f, 0.5f, 1.0f };
            float[] reltivePositions = { 0.0f, 0.1f, 1.0f };
            Blend blend = new Blend();
            blend.Factors = relativelntensities;
            blend.Positions = reltivePositions;
            lineGBrush.Blend = blend;
        }


        #endregion

        #region Button & ETC Click Events (모든클릭 이벤트처리)
        ////////////////////////////////////////////////////
        #endregion

        #region Events (클릭을 제외한 이벤트처리)
        /////////////////////////////////////


        private void DXfrmMDI_Baseform_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y < 20)
            {
                mCurrentPosition = new Point(-e.X, -e.Y);
                this.titleMove = true;
            }
        }

        private void DXfrmMDI_Baseform_Paint(object sender, PaintEventArgs e)
        {
            //폼 배경을 흰색으로 설정
            e.Graphics.Clear(Color.Snow);

            //브러쉬 설정
            SolidBrush sBrush = new SolidBrush(Color.FromArgb(127, 50, 0));
            sBrush.Color = Color.White;

            //화면 그리기
            Image memimage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(memimage);

            string title = "http://www.jnkc.kr";
            g.FillRectangle(this.lineGBrush, 0, 0, this.Width, 20);
            g.DrawString(title, this.Font, sBrush, 5f, 5f);

            //화면으로 표현
            e.Graphics.DrawImage(memimage, ClientRectangle);

            g.Dispose();
            memimage.Dispose();
            sBrush.Dispose();
        }

        private void DXfrmMDI_Baseform_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.titleMove == true)
            {
                this.Location = new Point(
                    this.Location.X + (mCurrentPosition.X + e.X),
                    this.Location.Y + (mCurrentPosition.Y + e.Y));//마우스 이동치를 Form Location에 반영한다.
            }
        }

        private void DXfrmMDI_Baseform_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.titleMove == true)
            {
                this.Location = new Point(
                    this.Location.X + (mCurrentPosition.X + e.X),
                    this.Location.Y + (mCurrentPosition.Y + e.Y));//마우스 이동치를 Form Location에 반영한다.
            }
            this.titleMove = false;
        }


        #endregion

        #region DB CRUD(데이터베이스 처리)
        ///////////////////////////////
        #endregion 


    }
}