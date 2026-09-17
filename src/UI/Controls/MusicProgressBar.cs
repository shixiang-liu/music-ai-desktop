using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace UI.Controls
{
    public class MusicProgressBar : Control
    {
        public event EventHandler<int> ValueChanged;
        private int value = 0;
        private int maximum = 100;
        private bool isDragging = false;

        public int Maximum
        {
            get => maximum;
            set { maximum = value; Invalidate(); }
        }

        public int Value
        {
            get => value;
            set
            {
                if (value < 0) value = 0;
                if (value > maximum) value = maximum;
                this.value = value;
                Invalidate();
            }
        }

        public Color GradientStartColor { get; set; } = Color.FromArgb(0, 162, 212);
        public Color GradientEndColor { get; set; } = Color.FromArgb(55, 216, 255);

        public Color BackColorCustom { get; set; } = Color.FromArgb(230, 233, 245);
        public Color ThumbColor { get; set; } = Color.White;

        public MusicProgressBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Height = 16;
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 背景
            using (var bg = new SolidBrush(BackColorCustom))
                g.FillRectangle(bg, 0, this.Height / 2 - 3, this.Width, 6);

            // 进度（渐变主色）
            float percent = maximum == 0 ? 0 : (float)value / maximum;
            int barWidth = (int)(percent * this.Width);
            if (barWidth > 0)
            {
                using (var gradientBrush = new LinearGradientBrush(
                    new Rectangle(0, this.Height / 2 - 3, barWidth, 6),
                    GradientStartColor,
                    GradientEndColor,
                    LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(gradientBrush, 0, this.Height / 2 - 3, barWidth, 6);
                }
            }

            // 圆形滑块
            int thumbSize = 14;
            int thumbX = barWidth - thumbSize / 2;
            if (thumbX < 0) thumbX = 0;
            if (thumbX > this.Width - thumbSize) thumbX = this.Width - thumbSize;

            using (var thumbBrush = new SolidBrush(ThumbColor))
            {
                g.FillEllipse(thumbBrush, thumbX, this.Height / 2 - thumbSize / 2, thumbSize, thumbSize);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isDragging = true;
            UpdateValue(e.X);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                UpdateValue(e.X);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
        }

        private void UpdateValue(int mouseX)
        {
            int newValue = (int)((mouseX / (float)this.Width) * maximum);
            Value = newValue;
            ValueChanged?.Invoke(this, newValue);
        }
    }
}
