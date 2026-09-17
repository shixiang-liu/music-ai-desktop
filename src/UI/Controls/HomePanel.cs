using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UI.Controls
{
    public partial class HomePanel : UserControl
    {
        public event EventHandler UploadSongClicked;
        public event EventHandler GenerateRequested;
        private bool _isDragOver = false;
        public event EventHandler<string> OnSongDropped;

        public HomePanel()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadSongClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateRequested?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Any(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
                                   f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase)))
                {
                    _isDragOver = true;
                    this.Invalidate(); // 触发重绘
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            _isDragOver = false;
            this.Invalidate();
            e.Effect = DragDropEffects.None;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);
            _isDragOver = false;
            this.Invalidate();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                // 只处理第一个音频文件，或者你也可以支持批量上传
                foreach (string file in files)
                {
                    if (file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
                        file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
                    {
                        // 触发上传
                        OnSongDropped?.Invoke(this, file);
                        // 如果只想上传第一个就break;
                        // break;
                    }
                }
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            _isDragOver = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_isDragOver)
            {
                // 半透明遮罩
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(90, 60, 130, 220))) // 蓝色半透明
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
                // 高亮虚线边框
                using (Pen pen = new Pen(Color.DeepSkyBlue, 4))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, 2, 2, this.Width - 4, this.Height - 4);
                }
                // 提示文本
                string tip = "松开上传歌曲";
                var size = e.Graphics.MeasureString(tip, this.Font);
                e.Graphics.DrawString(tip, this.Font, Brushes.White,
                    (Width - size.Width) / 2, (Height - size.Height) / 2);
            }
        }

    }
}
