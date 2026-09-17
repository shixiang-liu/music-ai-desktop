using MusicAI.BLL;
using MusicAI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Controls
{
    public partial class SongCardControl : UserControl
    {
        public event EventHandler PlayRequested;
        public event EventHandler FavoriteClicked;
        public UserUploadedSong SongData { get; set; }
        public int SongId { get; set; }
        private bool _isFavorite;

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                btnHeart.Image = value
                    ? Properties.Resources.heart_filled
                    : Properties.Resources.heart_outline;
            }
        }


        public string SongName
        {
            get => lblSongName.Text;
            set => lblSongName.Text = value;
        }

        public string Duration
        {
            get => lblDuration.Text;
            set => lblDuration.Text = $"时长：{value}";
        }

        public Image CoverImage
        {
            get => picCover.Image;
            set => picCover.Image = value;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            return new Size(this.Width, this.Height);
        }

        public bool Selected
        {
            get => checkBox1.Checked;
            set => checkBox1.Checked = value;
        }

        public SongCardControl()
        {
            InitializeComponent();
            this.Width = 430;
            this.Height = 35;
            this.AutoSize = false;
            // 允许整卡双击
            this.DoubleClick += (s, e) => PlayRequested?.Invoke(this, EventArgs.Empty);
            foreach (Control ctrl in this.Controls)
                ctrl.DoubleClick += (s, e) => this.OnDoubleClick(e);

            btnHeart.Click += (s, e) => FavoriteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnHeart_Click(object sender, EventArgs e)
        {
            IsFavorite = !IsFavorite;
        }
    }
}
