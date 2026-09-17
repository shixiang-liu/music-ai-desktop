using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UI.Controls
{
    public class RoundPictureBox : PictureBox
    {
        private float _angle = 0;
        public float RotationAngle
        {
            get { return _angle; }
            set { _angle = value % 360; Invalidate(); }
        }

        public RoundPictureBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int diameter = Math.Min(Width, Height);
            int x = (Width - diameter) / 2;
            int y = (Height - diameter) / 2;

            // 1. 先剪裁成圆形
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(x, y, diameter, diameter);
                pe.Graphics.SetClip(path);
            }

            // 2. 旋转图片
            if (this.Image != null)
            {
                pe.Graphics.TranslateTransform(Width / 2f, Height / 2f);
                pe.Graphics.RotateTransform(_angle);
                pe.Graphics.TranslateTransform(-Width / 2f, -Height / 2f);

                pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                pe.Graphics.DrawImage(this.Image, x, y, diameter, diameter);
            }

            // 3. 恢复旋转，绘制一圈高质量白色描边
            pe.Graphics.ResetTransform();
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(x, y, diameter - 1, diameter - 1); // -1是为了避免右下有锯齿
                using (Pen pen = new Pen(Color.White, 3)) // 白色描边，宽度可调
                {
                    pe.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
