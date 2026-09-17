using AxWMPLib;
using MusicAI.Common;
using MusicAI.BLL;
using MusicAI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UI.Controls;
using WMPLib;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly IUserService _userService;
        private readonly ISongService _songService;
        private string _currentUsername;
        private UserDTO _currentUser;
        private HomePanel _homePanel;
        private List<UserUploadedSong> _playlist = new List<UserUploadedSong>();
        private int _currentPlaylistIndex = -1;
        private bool _isPlaying = false;
        private bool _isDragging = false;
        private Point _dragStartPoint;
        private int _currentSongId = 0;
        private readonly IReportService _reportService;

        public enum PlayMode
        {
            Order,      // 顺序播放
            Random,     // 随机播放
            RepeatOne   // 单曲循环
        }
        private PlayMode _playMode = PlayMode.Order;
        private Random _random = new Random();
        private bool _needAutoPlay = false;

        public MainForm(string username, IUserService userService, ISongService songService)
        {
            InitializeComponent();
            _currentUsername = username ?? throw new ArgumentNullException(nameof(username));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _songService = songService ?? throw new ArgumentNullException(nameof(songService));
            _reportService = new ReportService();
            UpdatePlayerUI();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadUserInfo();

            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", false);
            axWindowsMediaPlayer1.settings.setMode("shuffle", false);
            axWindowsMediaPlayer1.settings.volume = musicVolumeBar?.Volume ?? 50;
            axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
            _playMode = PlayMode.RepeatOne;
            if (tmrProgressUpdate != null)
                tmrProgressUpdate.Start();
            ShowHomePanel();
        }

        private void LoadUserInfo()
        {
            _currentUser = _userService.GetUserByUsername(_currentUsername);
            if (_currentUser != null)
            {
                lblWelcome.Text = $"欢迎，{_currentUser.Username}！";
                lblSignupDate.Text = _currentUser.SignupDate.HasValue
                                    ? $"注册时间：{_currentUser.SignupDate.Value:yyyy-MM-dd HH:mm:ss}"
                                    : "注册时间：未知";
            }
            else
            {
                MessageBox.Show("无法加载用户信息，请尝试重新登录。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnUploadSong_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("用户信息无效，无法上传歌曲。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "音频文件 (*.mp3;*.wav)|*.mp3;*.wav";
                openFileDialog.Title = "选择要上传的歌曲文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string clientFilePath = openFileDialog.FileName;
                        string originalFileName = Path.GetFileName(clientFilePath);
                        string fileType = Path.GetExtension(clientFilePath).TrimStart('.');

                        string fileHash = HashUtils.GetFileHash(clientFilePath);

                        var existingSong = _songService.GetUploadedSongByHash(_currentUser.UserID, fileHash);
                        if (existingSong != null)
                        {
                            _songService.AddRecentPlay(_currentUser.UserID, existingSong.UploadedSongID);
                            LoadSongToPlayer(existingSong.StoredFilePath, existingSong.OriginalFileName);
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                            UpdatePlayerControls(existingSong);
                            return;
                        }

                        string uploadsDirName = "Uploads";
                        string userUploadsDir = Path.Combine(Application.StartupPath, uploadsDirName, _currentUser.UserID.ToString());
                        if (!Directory.Exists(userUploadsDir))
                            Directory.CreateDirectory(userUploadsDir);

                        string storedFileName = $"{Guid.NewGuid()}_{originalFileName}";
                        string storedFilePathOnServer = Path.Combine(userUploadsDir, storedFileName);
                        File.Copy(clientFilePath, storedFilePathOnServer, true);

                        int durationSeconds = 0;
                        try
                        {
                            var tagFile = TagLib.File.Create(storedFilePathOnServer);
                            durationSeconds = (int)tagFile.Properties.Duration.TotalSeconds;
                        }
                        catch { }

                        int uploadedSongId = _songService.UploadSong(
                            _currentUser.UserID,
                            originalFileName,
                            storedFilePathOnServer,
                            fileType,
                            durationSeconds,
                            fileHash
                        );

                        if (uploadedSongId > 0)
                        {
                            var newSong = _songService.GetLastUploadedSongByUserId(_currentUser.UserID);
                            _songService.AddRecentPlay(_currentUser.UserID, newSong.UploadedSongID);
                            LoadSongToPlayer(newSong.StoredFilePath, newSong.OriginalFileName);
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                            UpdatePlayerControls(newSong);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"上传歌曲失败：{ex.Message}", "上传错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadSongToPlayer(string filePath, string songName)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                try
                {
                    axWindowsMediaPlayer1.URL = filePath;
                    lblCurrentTrackName.Text = songName;
                    musicProgressBar.Value = 0;
                    _isPlaying = false;
                    UpdatePlayerUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载歌曲失败: {ex.Message}\n文件路径: {filePath}", "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblCurrentTrackName.Text = "加载失败";
                    musicProgressBar.Value = 0;
                    UpdatePlayerUI();
                }
            }
            else
            {
                lblCurrentTrackName.Text = "歌曲文件无效或不存在";
                musicProgressBar.Value = 0;
                UpdatePlayerUI();
            }
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(axWindowsMediaPlayer1.URL))
            {
                if (_currentPlaylistIndex >= 0 && _currentPlaylistIndex < _playlist.Count)
                {
                    LoadSongToPlayer(_playlist[_currentPlaylistIndex].StoredFilePath, _playlist[_currentPlaylistIndex].OriginalFileName);
                }
                else
                {
                    MessageBox.Show("请先上传或选择一首歌曲。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (_isPlaying)
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            else
                axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void AxWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            WMPPlayState state = (WMPPlayState)e.newState;
            if (state == WMPPlayState.wmppsMediaEnded)
            {
                if (_playMode == PlayMode.RepeatOne)
                {
                    PlaySongByIndex(_currentPlaylistIndex);
                }
                else if (_playMode == PlayMode.Order)
                {
                    int nextIndex = (_currentPlaylistIndex + 1) % _playlist.Count;
                    PlaySongByIndex(nextIndex);
                }
                else if (_playMode == PlayMode.Random)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = _random.Next(_playlist.Count);
                    } while (_playlist.Count > 1 && randomIndex == _currentPlaylistIndex);
                    PlaySongByIndex(randomIndex);
                }
            }
            else if (state == WMPPlayState.wmppsReady)
            {
                if (_needAutoPlay)
                {
                    try
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("自动play异常：" + ex.Message);
                    }
                    _needAutoPlay = false;
                }
            }
            else if (state == WMPPlayState.wmppsPlaying)
            {
                _isPlaying = true;
                tmrProgressUpdate?.Start();
                tmrAlbumArtRotate.Start();
            }
            else if (state == WMPPlayState.wmppsPaused || state == WMPPlayState.wmppsStopped)
            {
                _isPlaying = false;
                tmrProgressUpdate?.Stop();
                tmrAlbumArtRotate.Stop();
                lblCurrentTime.Text = "00:00";
            }
            UpdatePlayerUI();
        }

        private void UpdatePlayerUI()
        {
            if (btnPlayPause != null && Properties.Resources.play != null && Properties.Resources.pause != null)
                btnPlayPause.Image = _isPlaying ? Properties.Resources.pause : Properties.Resources.play;
            else if (btnPlayPause != null)
                btnPlayPause.Text = _isPlaying ? "暂停" : "播放";

            if (_currentPlaylistIndex != -1 && _currentPlaylistIndex < _playlist.Count)
                lblCurrentTrackName.Text = _playlist[_currentPlaylistIndex].OriginalFileName;
            else
                lblCurrentTrackName.Text = "未加载歌曲";
        }

        // 定时刷新进度条和时间
        private void tmrProgressUpdate_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.currentMedia != null && axWindowsMediaPlayer1.currentMedia.duration > 0)
            {
                double duration = axWindowsMediaPlayer1.currentMedia.duration;
                double position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                musicProgressBar.Maximum = (int)(duration * 1000);
                musicProgressBar.Value = (int)(position * 1000);
                lblCurrentTime.Text = TimeSpan.FromSeconds(position).ToString(@"mm\:ss");
                lblTotalTime.Text = TimeSpan.FromSeconds(duration).ToString(@"mm\:ss");
            }
            else
            {
                musicProgressBar.Value = 0;
                lblCurrentTime.Text = "00:00";
                lblTotalTime.Text = "00:00";
            }
        }

        private void btnNextTrack_Click(object sender, EventArgs e)
        {
            if (_playlist.Count == 0) return;
            int nextIndex = _playMode == PlayMode.Random
                ? GetRandomNextIndex()
                : (_currentPlaylistIndex + 1) % _playlist.Count;
            PlaySongByIndex(nextIndex);
        }

        private int GetRandomNextIndex()
        {
            int nextIndex;
            do
            {
                nextIndex = _random.Next(_playlist.Count);
            } while (_playlist.Count > 1 && nextIndex == _currentPlaylistIndex);
            return nextIndex;
        }

        private void btnPreviousTrack_Click(object sender, EventArgs e)
        {
            if (_playlist.Count == 0) return;
            int prevIndex = _playMode == PlayMode.Random
                ? GetRandomNextIndex()
                : (_currentPlaylistIndex - 1 + _playlist.Count) % _playlist.Count;
            PlaySongByIndex(prevIndex);
        }

        // -------- 主功能按钮区 --------
        private void btnHome_Click(object sender, EventArgs e)
        {
            ShowHomePanel();
        }
        private void btnRecentPlayed_Click(object sender, EventArgs e)
        {
            ShowSongListPanel(SongListType.Recent);
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            ShowSongListPanel(SongListType.Favorites);
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {
            ShowAboutPanel();
        }

        private void ShowHomePanel()
        {
            pnlMainContent.Controls.Clear();
            if (_homePanel == null)
            {
                _homePanel = new HomePanel();
                _homePanel.UploadSongClicked += BtnUploadSong_Click;
                _homePanel.OnSongDropped += HomePanel_OnSongDropped;
                _homePanel.GenerateRequested += HomePanel_GenerateRequested;
            }
            var singerStyles = _songService.GetAllSingerStyles();
            _homePanel.cmbSingerStyle.DataSource = singerStyles;
            _homePanel.cmbSingerStyle.DisplayMember = "Name";
            _homePanel.cmbSingerStyle.ValueMember = "SingerStyleID";
            pnlMainContent.Controls.Add(_homePanel);
        }

        private void ShowSongListPanel(SongListType type)
        {
            pnlMainContent.Controls.Clear();
            List<UserUploadedSong> songs = type == SongListType.Recent
                ? _songService.GetRecentPlayedSongs(_currentUser.UserID)
                : _songService.GetFavoriteSongs(_currentUser.UserID);

            foreach (var song in songs)
            {
                song.IsFavorite = _songService.IsFavorite(_currentUser.UserID, song.UploadedSongID);
            }

            string panelTitle = (type == SongListType.Recent) ? "最近播放" : "我的最爱";
            var panel = new SongListPanel();
            panel.SongService = _songService;
            panel.CurrentUser = _currentUser;
            panel.SetTitle(panelTitle, type);
            panel.LoadSongs(songs);
            panel.PlaySongRequested += PlaySong;
            panel.FavoriteToggled += ToggleFavorite;
            panel.RequireReloadRecent += () => ShowSongListPanel(type);
            panel.OnCardFavoriteClicked += SimulateLikeTrackClick;
            pnlMainContent.Controls.Add(panel);
        }


        private void ShowReportPanel()
        {
            pnlMainContent.Controls.Clear();

            var panel = new ReportPanel();

            panel.ReportService = _reportService;

            pnlMainContent.Controls.Add(panel);

            panel.LoadReport();
        }

        private void SimulateLikeTrackClick(int songId)
        {
            _currentSongId = songId;
            btnLikeTrack.PerformClick();
        }

        private void ShowAboutPanel()
        {
            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(new AboutPanel());
        }

        // 收藏切换
        private void ToggleFavorite(UserUploadedSong song)
        {
            if (_songService.IsFavorite(_currentUser.UserID, song.UploadedSongID))
                _songService.RemoveFavorite(_currentUser.UserID, song.UploadedSongID);
            else
                _songService.AddFavorite(_currentUser.UserID, song.UploadedSongID);

            SyncLikeStatus(song.UploadedSongID);
        }

        private void PlaySong(UserUploadedSong song)
        {
            int index = _playlist.FindIndex(s => s.UploadedSongID == song.UploadedSongID);
            if (index == -1)
            {
                _playlist.Add(song);
                index = _playlist.Count - 1;
            }
            PlaySongByIndex(index);
        }

        public void SyncLikeStatus(int songId)
        {
            bool isFav = _songService.IsFavorite(_currentUser.UserID, songId);

            // 更新底部播放栏按钮
            if (_currentSongId == songId)
            {
                btnLikeTrack.Image = isFav
                    ? Properties.Resources.heart_filled
                    : Properties.Resources.heart_outline;
            }

            // 递归查找 SongCardControl
            foreach (Control ctrl in pnlMainContent.Controls)
            {
                if (ctrl is UI.Controls.SongListPanel songListPanel)
                {
                    foreach (var songCard in GetAllSongCards(songListPanel))
                    {
                        if (songCard.SongId == songId)
                        {
                            songCard.IsFavorite = isFav;
                        }
                    }
                }
            }
        }


        private IEnumerable<SongCardControl> GetAllSongCards(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is SongCardControl card)
                    yield return card;
                else if (ctrl.HasChildren)
                {
                    foreach (var subCard in GetAllSongCards(ctrl))
                        yield return subCard;
                }
            }
        }

        // 旋转专辑封面
        private void tmrAlbumArtRotate_Tick(object sender, EventArgs e)
        {
            if (picAlbumArt is RoundPictureBox roundPic)
            {
                roundPic.RotationAngle += 0.8f;
            }
        }

        private void HomePanel_OnSongDropped(object sender, string filePath)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("用户信息无效，无法上传歌曲。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UploadSongFromFile(filePath);
        }

        private void UploadSongFromFile(string clientFilePath)
        {
            try
            {
                string originalFileName = Path.GetFileName(clientFilePath);
                string fileType = Path.GetExtension(clientFilePath).TrimStart('.');
                string fileHash = HashUtils.GetFileHash(clientFilePath);
                var existingSong = _songService.GetUploadedSongByHash(_currentUser.UserID, fileHash);
                if (existingSong != null)
                {
                    _songService.AddRecentPlay(_currentUser.UserID, existingSong.UploadedSongID);
                    LoadSongToPlayer(existingSong.StoredFilePath, existingSong.OriginalFileName);
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    UpdatePlayerControls(existingSong);
                    return;
                }

                string uploadsDirName = "Uploads";
                string userUploadsDir = Path.Combine(Application.StartupPath, uploadsDirName, _currentUser.UserID.ToString());
                if (!Directory.Exists(userUploadsDir))
                    Directory.CreateDirectory(userUploadsDir);

                string storedFileName = $"{Guid.NewGuid()}_{originalFileName}";
                string storedFilePathOnServer = Path.Combine(userUploadsDir, storedFileName);
                File.Copy(clientFilePath, storedFilePathOnServer, true);

                int durationSeconds = 0;
                try
                {
                    var tagFile = TagLib.File.Create(storedFilePathOnServer);
                    durationSeconds = (int)tagFile.Properties.Duration.TotalSeconds;
                }
                catch { }

                int uploadedSongId = _songService.UploadSong(
                    _currentUser.UserID,
                    originalFileName,
                    storedFilePathOnServer,
                    fileType,
                    durationSeconds,
                    fileHash
                );

                if (uploadedSongId > 0)
                {
                    var newSong = _songService.GetLastUploadedSongByUserId(_currentUser.UserID);
                    _songService.AddRecentPlay(_currentUser.UserID, newSong.UploadedSongID);
                    LoadSongToPlayer(newSong.StoredFilePath, newSong.OriginalFileName);
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    UpdatePlayerControls(newSong);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上传歌曲失败：{ex.Message}", "上传错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 下面是窗口拖动、音量、进度等通用UI事件
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnlTopBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // 只响应左键
            {
                _isDragging = true;
                _dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void pnlTopBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - _dragStartPoint.X,
                                     currentScreenPos.Y - _dragStartPoint.Y);
            }
        }

        private void pnlTopBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = false;
            }
        }

        // 音量调节
        private void musicVolumeBar_VolumeChanged(object sender, int volume)
        {
            axWindowsMediaPlayer1.settings.volume = volume;
        }

        // 进度条拖动跳转
        private void musicProgressBar_ValueChanged(object sender, int value)
        {
            if (axWindowsMediaPlayer1.currentMedia != null && axWindowsMediaPlayer1.currentMedia.duration > 0)
            {
                double duration = axWindowsMediaPlayer1.currentMedia.duration;
                double newPosition = value / 1000.0;
                if (newPosition > duration) newPosition = duration;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = newPosition;
            }
        }

        private void UpdatePlayerControls(UserUploadedSong song)
        {
            if (song == null) return;
            lblCurrentTrackName.Text = song.OriginalFileName;
            btnLikeTrack.Image = _songService.IsFavorite(_currentUser.UserID, song.UploadedSongID)
                ? Properties.Resources.heart_filled
                : Properties.Resources.heart_outline;

            // 尝试获取专辑图片
            Image cover = MusicUtils.GetAlbumArt(song.StoredFilePath);
            picAlbumArt.Image = cover ?? Properties.Resources.music;
            _currentSongId = song.UploadedSongID;

            if (_playlist == null)
                _playlist = new List<UserUploadedSong>();

            int index = _playlist.FindIndex(s => s.UploadedSongID == song.UploadedSongID);
            if (index == -1)
            {
                _playlist.Add(song);
                _currentPlaylistIndex = _playlist.Count - 1;
            }
            else
            {
                _currentPlaylistIndex = index;
            }
        }

        private void btnLikeTrack_Click(object sender, EventArgs e)
        {
            if (_currentUser == null || _currentPlaylistIndex == -1) return;
            var song = _playlist[_currentPlaylistIndex];
            bool isFav = _songService.IsFavorite(_currentUser.UserID, song.UploadedSongID);
            if (isFav)
                _songService.RemoveFavorite(_currentUser.UserID, song.UploadedSongID);
            else
                _songService.AddFavorite(_currentUser.UserID, song.UploadedSongID);
            SyncLikeStatus(song.UploadedSongID);

            foreach (Control ctrl in pnlMainContent.Controls)
            {
                if (ctrl is UI.Controls.SongListPanel songListPanel)
                {
                    foreach (var songCard in songListPanel.Controls.OfType<UI.Controls.SongCardControl>())
                    {
                        if (songCard.SongId == song.UploadedSongID)
                        {
                            songCard.IsFavorite = _songService.IsFavorite(_currentUser.UserID, song.UploadedSongID);
                        }
                    }
                }
            }
        }

        // 生成翻唱请求处理
        private async void HomePanel_GenerateRequested(object sender, EventArgs e)
        {
            if (_currentPlaylistIndex == -1)
            {
                MessageBox.Show("请先选择要转换的歌曲！");
                return;
            }

            var song = _playlist[_currentPlaylistIndex];
            string originalName = Path.GetFileNameWithoutExtension(song.OriginalFileName);

            var homePanel = _homePanel;
            var selectedStyle = homePanel?.cmbSingerStyle?.SelectedItem as SingerStyle;

            if (selectedStyle != null)
            {
                if (string.IsNullOrWhiteSpace(selectedStyle.FeaturePath) || string.IsNullOrWhiteSpace(selectedStyle.IndexPath))
                {
                    MessageBox.Show($"当前风格“{selectedStyle.Name}”暂不支持改歌手，请选择其它歌手。", "不支持的风格", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string saveFileName = selectedStyle == null
                ? originalName
                : $"{originalName}_{selectedStyle.Name}翻唱";

            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "选择保存路径";
                sfd.Filter = "WAV 文件|*.wav|MP3 文件|*.mp3|所有文件|*.*";
                sfd.FileName = saveFileName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (selectedStyle == null)
                        {
                            bool ok = AudioConverter.ConvertAudioFormat(song.StoredFilePath, sfd.FileName);
                            if (ok)
                                MessageBox.Show("生成成功！\n文件已保存到：" + sfd.FileName);
                            else
                                MessageBox.Show("转码失败！");
                        }
                        else
                        {
                            using (var waitForm = new WaitForm())
                            {
                                var t = Task.Run(async () =>
                                {
                                    /* await CallRvcConvertApiAsync(
                                        song.StoredFilePath,
                                        selectedStyle.FeaturePath,
                                        selectedStyle.IndexPath,
                                        sfd.FileName
                                    ); */
                                    await Task.Delay(5000000);
                                });

                                waitForm.Show();
                                while (!t.IsCompleted && !waitForm.IsCanceled)
                                {
                                    Application.DoEvents();
                                }
                                waitForm.Close();

                                if (waitForm.IsCanceled)
                                {
                                    MessageBox.Show("用户已取消生成。");
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存失败：" + ex.Message);
                    }
                }
            }
        }

        // 调用 RVC 转换 API
        public async Task CallRvcConvertApiAsync(
            string inputAudioPath,
            string featureLibraryPath,
            string indexPath,
            string savePath,
            int speakerId = 0,
            int pitchChange = 0)
        {
            if (string.IsNullOrWhiteSpace(inputAudioPath) || !File.Exists(inputAudioPath))
                throw new ArgumentException("音频路径无效！");
            if (string.IsNullOrWhiteSpace(featureLibraryPath) || !File.Exists(featureLibraryPath))
                throw new ArgumentException("特征库路径无效！");
            if (string.IsNullOrWhiteSpace(indexPath) || !File.Exists(indexPath))
                throw new ArgumentException("index 路径无效！");

            var payload = new
            {
                data = new object[]
                {
                    speakerId,      // 说话人ID，默认为0
                    inputAudioPath,
                    pitchChange,    // 音高变化
                    null,           // F0 曲线（不填）
                    "rmvpe",        // 音高提取算法
                    featureLibraryPath,
                    indexPath,
                    0.90,           // 检索特征占比
                    6,              // 中值滤波半径
                    0,              // 重采样
                    0,              // 音量包络融合比例
                    0.20            // 清辅音保护
                }
            };

            using (var client = new HttpClient())
            {
                var json = System.Text.Json.JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:7899/run/infer_convert", content);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var doc = System.Text.Json.JsonDocument.Parse(responseJson);
                var base64 = doc.RootElement.GetProperty("data")[1].GetProperty("data").GetString();

                const string prefix = "data:audio/wav;base64,";
                if (base64.StartsWith(prefix))
                    base64 = base64.Substring(prefix.Length);

                File.WriteAllBytes(savePath, Convert.FromBase64String(base64));
            }
        }

        // 音频格式转换器
        public static class AudioConverter
        {
            public static bool ConvertAudioFormat(string inputPath, string outputPath)
            {
                if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
                    throw new ArgumentException("输入文件不存在！");
                if (string.IsNullOrWhiteSpace(outputPath))
                    throw new ArgumentException("输出文件路径无效！");

                string ffmpegExe = "ffmpeg";

                var psi = new ProcessStartInfo
                {
                    FileName = ffmpegExe,
                    Arguments = $"-y -i \"{inputPath}\" \"{outputPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return process.ExitCode == 0 && File.Exists(outputPath);
                }
            }
        }

        private void btnPlayMode_Click(object sender, EventArgs e)
        {
            _playMode = (PlayMode)(((int)_playMode + 1) % Enum.GetValues(typeof(PlayMode)).Length);
            UpdatePlayModeButton();
        }

        private void UpdatePlayModeButton()
        {
            switch (_playMode)
            {
                case PlayMode.Order:
                    btnPlayMode.Image = Properties.Resources.icon_order;
                    toolTip1.SetToolTip(btnPlayMode, "顺序播放");
                    break;
                case PlayMode.Random:
                    btnPlayMode.Image = Properties.Resources.icon_shuffle;
                    toolTip1.SetToolTip(btnPlayMode, "随机播放");
                    break;
                case PlayMode.RepeatOne:
                    btnPlayMode.Image = Properties.Resources.icon_repeat_one;
                    toolTip1.SetToolTip(btnPlayMode, "单曲循环");
                    break;
            }
        }

        private void PlaySongByIndex(int index)
        {
            if (_playlist == null || _playlist.Count == 0) return;
            if (index < 0 || index >= _playlist.Count) return;

            _currentPlaylistIndex = index;
            var song = _playlist[_currentPlaylistIndex];

            axWindowsMediaPlayer1.URL = song.StoredFilePath;
            _needAutoPlay = true;

            lblCurrentTrackName.Text = song.OriginalFileName;
            musicProgressBar.Value = 0;
            Image cover = MusicUtils.GetAlbumArt(song.StoredFilePath);
            picAlbumArt.Image = cover ?? Properties.Resources.music;
            btnLikeTrack.Image = _songService.IsFavorite(_currentUser.UserID, song.UploadedSongID)
                ? Properties.Resources.heart_filled
                : Properties.Resources.heart_outline;
            _songService.AddRecentPlay(_currentUser.UserID, song.UploadedSongID);
            _currentSongId = song.UploadedSongID;
            UpdatePlayerUI();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ShowReportPanel();
        }
    }
}
