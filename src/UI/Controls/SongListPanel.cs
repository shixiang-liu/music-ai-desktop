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
using System.IO;
using System.Diagnostics;
using MusicAI.Common;

public enum SongListType { Recent, Favorites }

namespace UI.Controls
{
    public partial class SongListPanel : UserControl
    {
        public SongListType CurrentListType { get; private set; }
        public event Action<UserUploadedSong> PlaySongRequested;
        public event Action<UserUploadedSong> FavoriteToggled;
        public ISongService SongService { get; set; }
        public UserDTO CurrentUser { get; set; }
        public event Action RequireReloadRecent;
        public event Action<int> OnCardFavoriteClicked;

        public SongListPanel()
        {
            InitializeComponent();
        }

        public void SetTitle(string title, SongListType type)
        {
            lblTitle.Text = title;
            CurrentListType = type;
            bool showDeleteUI = (type == SongListType.Recent);
            chkSelectAll.Visible = showDeleteUI;
            btnDeleteSelected.Visible = showDeleteUI;
        }

        public void LoadSongs(List<UserUploadedSong> songs)
        {
            flowPanel.Controls.Clear();
            foreach (var song in songs)
            {
                var card = new SongCardControl
                {
                    SongName = song.OriginalFileName,
                    Duration = FormatDuration(song.DurationSeconds ?? 0),
                    IsFavorite = song.IsFavorite,
                    // 只用文件路径取封面
                    CoverImage = LoadCover(song.StoredFilePath),
                    SongData = song,
                    SongId = song.UploadedSongID,
                    Margin = new Padding(2)
                };

                // 事件绑定
                card.PlayRequested += (s, e) => PlaySongRequested?.Invoke(song);
                card.FavoriteClicked += (s, e) =>
                {
                    bool isFav = SongService.IsFavorite(CurrentUser.UserID, card.SongId);
                    if (isFav)
                    {
                        SongService.RemoveFavorite(CurrentUser.UserID, card.SongId);
                        card.IsFavorite = false;
                    }
                    else
                    {
                        SongService.AddFavorite(CurrentUser.UserID, card.SongId);
                        card.IsFavorite = true;
                    }

                    MainForm main = this.FindForm() as MainForm;
                    main?.SyncLikeStatus(card.SongId);
                };
                flowPanel.Controls.Add(card);
            }
        }

        private string FormatDuration(int seconds)
        {
            var ts = TimeSpan.FromSeconds(seconds);
            return ts.ToString(@"mm\:ss");
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            var selectedCards = flowPanel.Controls
                  .OfType<SongCardControl>()
                  .Where(card => card.Selected).ToList();

            if (selectedCards.Count == 0)
            {
                MessageBox.Show("请选择要删除的歌曲！");
                return;
            }

            if (MessageBox.Show("确定要删除所选歌曲吗？", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            foreach (var card in selectedCards)
            {
                SongService.DeleteRecentPlay(CurrentUser.UserID, card.SongId);
            }

            // 通知主窗体，像点按钮一样刷新整个面板
            RequireReloadRecent?.Invoke();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool selectAll = chkSelectAll.Checked;
            foreach (SongCardControl card in flowPanel.Controls.OfType<SongCardControl>())
            {
                card.Selected = selectAll;
            }
        }

        // 只用文件路径
        private Image LoadCover(string songFilePath)
        {
            if (!string.IsNullOrEmpty(songFilePath) && File.Exists(songFilePath))
            {
                var img = MusicUtils.GetAlbumArt(songFilePath);
                if (img != null) return img;
            }
            return Properties.Resources.music;
        }

        public void RefreshCardLikeStatus(int songId)
        {
            foreach (var card in flowPanel.Controls.OfType<SongCardControl>())
            {
                if (card.SongId == songId)
                {
                    bool isFav = SongService.IsFavorite(CurrentUser.UserID, songId);
                    card.IsFavorite = isFav;
                }
            }
        }

    }
}
