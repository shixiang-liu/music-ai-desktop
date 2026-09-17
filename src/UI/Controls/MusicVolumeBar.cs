using System.Drawing;
using System.Windows.Forms;
using System;
using System.Drawing.Drawing2D;

public class MusicVolumeBar : Control
{
    public event EventHandler<int> VolumeChanged;
    private int volume = 50;

    public int Volume
    {
        get => volume;
        set
        {
            if (value < 0) value = 0;
            if (value > 100) value = 100;
            volume = value;
            Invalidate();
        }
    }

    // 渐变主色
    public Color GradientStartColor { get; set; } = Color.FromArgb(0, 162, 212);
    public Color GradientEndColor { get; set; } = Color.FromArgb(55, 216, 255);

    public Color BackColorCustom { get; set; } = Color.FromArgb(230, 233, 245);
    public Color ThumbColor { get; set; } = Color.White;

    public MusicVolumeBar()
    {
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        this.Width = 107;
        this.Height = 26;
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

        // 进度渐变
        float percent = volume / 100f;
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
        SetVolumeByMouse(e.X);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (e.Button == MouseButtons.Left)
        {
            SetVolumeByMouse(e.X);
        }
    }

    private void SetVolumeByMouse(int mouseX)
    {
        int v = (int)((mouseX / (float)this.Width) * 100);
        Volume = v;
        VolumeChanged?.Invoke(this, v);
    }
}
