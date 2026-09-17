namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlNavigation = new System.Windows.Forms.Panel();
            this.btnReport = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLogoTitle = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnFavorites = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnRecentPlayed = new System.Windows.Forms.Button();
            this.pnlTopBar = new System.Windows.Forms.Panel();
            this.lblSignupDate = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.pnlPlayerControls = new System.Windows.Forms.Panel();
            this.musicVolumeBar = new MusicVolumeBar();
            this.musicProgressBar = new UI.Controls.MusicProgressBar();
            this.btnPlayMode = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picAlbumArt = new UI.Controls.RoundPictureBox();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnLikeTrack = new System.Windows.Forms.Button();
            this.btnNextTrack = new System.Windows.Forms.Button();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnPreviousTrack = new System.Windows.Forms.Button();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.lblCurrentTrackName = new System.Windows.Forms.Label();
            this.tmrProgressUpdate = new System.Windows.Forms.Timer(this.components);
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.homePanel1 = new UI.Controls.HomePanel();
            this.tmrAlbumArtRotate = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlTopBar.SuspendLayout();
            this.pnlPlayerControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbumArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.pnlMainContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.pnlNavigation.Controls.Add(this.btnReport);
            this.pnlNavigation.Controls.Add(this.pictureBox1);
            this.pnlNavigation.Controls.Add(this.lblLogoTitle);
            this.pnlNavigation.Controls.Add(this.btnAbout);
            this.pnlNavigation.Controls.Add(this.btnFavorites);
            this.pnlNavigation.Controls.Add(this.btnHome);
            this.pnlNavigation.Controls.Add(this.btnRecentPlayed);
            this.pnlNavigation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNavigation.ForeColor = System.Drawing.Color.White;
            this.pnlNavigation.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(193, 754);
            this.pnlNavigation.TabIndex = 0;
            // 
            // btnReport
            // 
            this.btnReport.AutoSize = true;
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(173)))));
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(34, 393);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(156, 51);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "数据报表";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::UI.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(14, 40);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblLogoTitle
            // 
            this.lblLogoTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLogoTitle.AutoSize = true;
            this.lblLogoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblLogoTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLogoTitle.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lblLogoTitle.ForeColor = System.Drawing.Color.White;
            this.lblLogoTitle.Location = new System.Drawing.Point(63, 43);
            this.lblLogoTitle.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblLogoTitle.Name = "lblLogoTitle";
            this.lblLogoTitle.Size = new System.Drawing.Size(122, 45);
            this.lblLogoTitle.TabIndex = 0;
            this.lblLogoTitle.Text = "AI音乐";
            // 
            // btnAbout
            // 
            this.btnAbout.AutoSize = true;
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnAbout.FlatAppearance.BorderSize = 0;
            this.btnAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(173)))));
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnAbout.ForeColor = System.Drawing.Color.White;
            this.btnAbout.Location = new System.Drawing.Point(34, 453);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(110, 51);
            this.btnAbout.TabIndex = 0;
            this.btnAbout.Text = "关  于";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnFavorites
            // 
            this.btnFavorites.AutoSize = true;
            this.btnFavorites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnFavorites.FlatAppearance.BorderSize = 0;
            this.btnFavorites.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnFavorites.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(173)))));
            this.btnFavorites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFavorites.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnFavorites.ForeColor = System.Drawing.Color.White;
            this.btnFavorites.Location = new System.Drawing.Point(34, 325);
            this.btnFavorites.Name = "btnFavorites";
            this.btnFavorites.Size = new System.Drawing.Size(156, 51);
            this.btnFavorites.TabIndex = 0;
            this.btnFavorites.Text = "我的最爱";
            this.btnFavorites.UseVisualStyleBackColor = false;
            this.btnFavorites.Click += new System.EventHandler(this.btnFavorites_Click);
            // 
            // btnHome
            // 
            this.btnHome.AutoSize = true;
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(173)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(34, 191);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(110, 51);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "主  页";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnRecentPlayed
            // 
            this.btnRecentPlayed.AutoSize = true;
            this.btnRecentPlayed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnRecentPlayed.FlatAppearance.BorderSize = 0;
            this.btnRecentPlayed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(120)))));
            this.btnRecentPlayed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(173)))));
            this.btnRecentPlayed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecentPlayed.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btnRecentPlayed.ForeColor = System.Drawing.Color.White;
            this.btnRecentPlayed.Location = new System.Drawing.Point(34, 258);
            this.btnRecentPlayed.Name = "btnRecentPlayed";
            this.btnRecentPlayed.Size = new System.Drawing.Size(156, 51);
            this.btnRecentPlayed.TabIndex = 0;
            this.btnRecentPlayed.Text = "最近播放";
            this.btnRecentPlayed.UseVisualStyleBackColor = false;
            this.btnRecentPlayed.Click += new System.EventHandler(this.btnRecentPlayed_Click);
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.AutoScroll = true;
            this.pnlTopBar.Controls.Add(this.lblSignupDate);
            this.pnlTopBar.Controls.Add(this.btnClose);
            this.pnlTopBar.Controls.Add(this.btnMinimize);
            this.pnlTopBar.Controls.Add(this.lblWelcome);
            this.pnlTopBar.Location = new System.Drawing.Point(192, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1003, 87);
            this.pnlTopBar.TabIndex = 3;
            this.pnlTopBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTopBar_MouseDown);
            this.pnlTopBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlTopBar_MouseMove);
            this.pnlTopBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlTopBar_MouseUp);
            // 
            // lblSignupDate
            // 
            this.lblSignupDate.AutoSize = true;
            this.lblSignupDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSignupDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.lblSignupDate.Location = new System.Drawing.Point(389, 61);
            this.lblSignupDate.Name = "lblSignupDate";
            this.lblSignupDate.Size = new System.Drawing.Size(260, 20);
            this.lblSignupDate.TabIndex = 5;
            this.lblSignupDate.Text = "注册时间：yyyy-MM-dd HH:mm:ss";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("黑体", 15F);
            this.btnClose.Location = new System.Drawing.Point(968, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "×";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("黑体", 15F);
            this.btnMinimize.Location = new System.Drawing.Point(937, 3);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(27, 30);
            this.btnMinimize.TabIndex = 16;
            this.btnMinimize.Text = "-";
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.lblWelcome.Location = new System.Drawing.Point(434, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(220, 37);
            this.lblWelcome.TabIndex = 4;
            this.lblWelcome.Text = "欢迎，用户名！";
            // 
            // pnlPlayerControls
            // 
            this.pnlPlayerControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.pnlPlayerControls.Controls.Add(this.musicVolumeBar);
            this.pnlPlayerControls.Controls.Add(this.musicProgressBar);
            this.pnlPlayerControls.Controls.Add(this.btnPlayMode);
            this.pnlPlayerControls.Controls.Add(this.label1);
            this.pnlPlayerControls.Controls.Add(this.picAlbumArt);
            this.pnlPlayerControls.Controls.Add(this.axWindowsMediaPlayer1);
            this.pnlPlayerControls.Controls.Add(this.btnLikeTrack);
            this.pnlPlayerControls.Controls.Add(this.btnNextTrack);
            this.pnlPlayerControls.Controls.Add(this.btnPlayPause);
            this.pnlPlayerControls.Controls.Add(this.btnPreviousTrack);
            this.pnlPlayerControls.Controls.Add(this.lblCurrentTime);
            this.pnlPlayerControls.Controls.Add(this.lblTotalTime);
            this.pnlPlayerControls.Controls.Add(this.lblCurrentTrackName);
            this.pnlPlayerControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlayerControls.Location = new System.Drawing.Point(0, 754);
            this.pnlPlayerControls.Name = "pnlPlayerControls";
            this.pnlPlayerControls.Size = new System.Drawing.Size(1193, 88);
            this.pnlPlayerControls.TabIndex = 1;
            // 
            // musicVolumeBar
            // 
            this.musicVolumeBar.BackColorCustom = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(245)))));
            this.musicVolumeBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.musicVolumeBar.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.musicVolumeBar.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(212)))));
            this.musicVolumeBar.Location = new System.Drawing.Point(1064, 49);
            this.musicVolumeBar.Margin = new System.Windows.Forms.Padding(2);
            this.musicVolumeBar.Name = "musicVolumeBar";
            this.musicVolumeBar.Size = new System.Drawing.Size(107, 26);
            this.musicVolumeBar.TabIndex = 24;
            this.musicVolumeBar.Text = "musicVolumeBar1";
            this.musicVolumeBar.ThumbColor = System.Drawing.Color.White;
            this.musicVolumeBar.Volume = 50;
            this.musicVolumeBar.VolumeChanged += new System.EventHandler<int>(this.musicVolumeBar_VolumeChanged);
            // 
            // musicProgressBar
            // 
            this.musicProgressBar.BackColorCustom = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(245)))));
            this.musicProgressBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.musicProgressBar.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.musicProgressBar.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(162)))), ((int)(((byte)(212)))));
            this.musicProgressBar.Location = new System.Drawing.Point(470, 56);
            this.musicProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this.musicProgressBar.Maximum = 100;
            this.musicProgressBar.Name = "musicProgressBar";
            this.musicProgressBar.Size = new System.Drawing.Size(461, 19);
            this.musicProgressBar.TabIndex = 23;
            this.musicProgressBar.Text = "musicProgressBar1";
            this.musicProgressBar.ThumbColor = System.Drawing.Color.White;
            this.musicProgressBar.Value = 0;
            this.musicProgressBar.ValueChanged += new System.EventHandler<int>(this.musicProgressBar_ValueChanged);
            // 
            // btnPlayMode
            // 
            this.btnPlayMode.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayMode.FlatAppearance.BorderSize = 0;
            this.btnPlayMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayMode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPlayMode.Image = global::UI.Properties.Resources.icon_repeat_one;
            this.btnPlayMode.Location = new System.Drawing.Point(807, 16);
            this.btnPlayMode.Name = "btnPlayMode";
            this.btnPlayMode.Size = new System.Drawing.Size(42, 38);
            this.btnPlayMode.TabIndex = 22;
            this.btnPlayMode.UseVisualStyleBackColor = false;
            this.btnPlayMode.Click += new System.EventHandler(this.btnPlayMode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1091, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "音量";
            // 
            // picAlbumArt
            // 
            this.picAlbumArt.BackColor = System.Drawing.Color.Transparent;
            this.picAlbumArt.Location = new System.Drawing.Point(34, 8);
            this.picAlbumArt.Margin = new System.Windows.Forms.Padding(2);
            this.picAlbumArt.Name = "picAlbumArt";
            this.picAlbumArt.RotationAngle = 0F;
            this.picAlbumArt.Size = new System.Drawing.Size(118, 78);
            this.picAlbumArt.TabIndex = 0;
            this.picAlbumArt.TabStop = false;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(1422, 96);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(10, 10);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // btnLikeTrack
            // 
            this.btnLikeTrack.BackColor = System.Drawing.Color.Transparent;
            this.btnLikeTrack.FlatAppearance.BorderSize = 0;
            this.btnLikeTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLikeTrack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLikeTrack.Image = ((System.Drawing.Image)(resources.GetObject("btnLikeTrack.Image")));
            this.btnLikeTrack.Location = new System.Drawing.Point(571, 19);
            this.btnLikeTrack.Name = "btnLikeTrack";
            this.btnLikeTrack.Size = new System.Drawing.Size(38, 35);
            this.btnLikeTrack.TabIndex = 7;
            this.btnLikeTrack.UseVisualStyleBackColor = false;
            this.btnLikeTrack.Click += new System.EventHandler(this.btnLikeTrack_Click);
            // 
            // btnNextTrack
            // 
            this.btnNextTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnNextTrack.FlatAppearance.BorderSize = 0;
            this.btnNextTrack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.btnNextTrack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.btnNextTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextTrack.ForeColor = System.Drawing.Color.White;
            this.btnNextTrack.Image = ((System.Drawing.Image)(resources.GetObject("btnNextTrack.Image")));
            this.btnNextTrack.Location = new System.Drawing.Point(747, 23);
            this.btnNextTrack.Name = "btnNextTrack";
            this.btnNextTrack.Size = new System.Drawing.Size(25, 23);
            this.btnNextTrack.TabIndex = 18;
            this.btnNextTrack.Text = " ";
            this.btnNextTrack.UseVisualStyleBackColor = false;
            this.btnNextTrack.Click += new System.EventHandler(this.btnNextTrack_Click);
            // 
            // btnPlayPause
            // 
            this.btnPlayPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnPlayPause.FlatAppearance.BorderSize = 0;
            this.btnPlayPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.btnPlayPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.btnPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayPause.ForeColor = System.Drawing.Color.White;
            this.btnPlayPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayPause.Image")));
            this.btnPlayPause.Location = new System.Drawing.Point(687, 18);
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.Size = new System.Drawing.Size(27, 32);
            this.btnPlayPause.TabIndex = 17;
            this.btnPlayPause.Text = " ";
            this.btnPlayPause.UseVisualStyleBackColor = false;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnPreviousTrack
            // 
            this.btnPreviousTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.btnPreviousTrack.FlatAppearance.BorderSize = 0;
            this.btnPreviousTrack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.btnPreviousTrack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.btnPreviousTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousTrack.ForeColor = System.Drawing.Color.White;
            this.btnPreviousTrack.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousTrack.Image")));
            this.btnPreviousTrack.Location = new System.Drawing.Point(628, 23);
            this.btnPreviousTrack.Name = "btnPreviousTrack";
            this.btnPreviousTrack.Size = new System.Drawing.Size(27, 23);
            this.btnPreviousTrack.TabIndex = 16;
            this.btnPreviousTrack.Text = " ";
            this.btnPreviousTrack.UseVisualStyleBackColor = false;
            this.btnPreviousTrack.Click += new System.EventHandler(this.btnPreviousTrack_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(376, 53);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(89, 20);
            this.lblCurrentTime.TabIndex = 15;
            this.lblCurrentTime.Text = "当前播放";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.ForeColor = System.Drawing.Color.White;
            this.lblTotalTime.Location = new System.Drawing.Point(947, 53);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(109, 20);
            this.lblTotalTime.TabIndex = 14;
            this.lblTotalTime.Text = "歌曲总时长";
            // 
            // lblCurrentTrackName
            // 
            this.lblCurrentTrackName.AutoSize = true;
            this.lblCurrentTrackName.Font = new System.Drawing.Font("黑体", 14F);
            this.lblCurrentTrackName.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTrackName.Location = new System.Drawing.Point(145, 29);
            this.lblCurrentTrackName.Name = "lblCurrentTrackName";
            this.lblCurrentTrackName.Size = new System.Drawing.Size(82, 24);
            this.lblCurrentTrackName.TabIndex = 12;
            this.lblCurrentTrackName.Text = "歌曲名";
            // 
            // tmrProgressUpdate
            // 
            this.tmrProgressUpdate.Interval = 200;
            this.tmrProgressUpdate.Tick += new System.EventHandler(this.tmrProgressUpdate_Tick);
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.Controls.Add(this.homePanel1);
            this.pnlMainContent.Location = new System.Drawing.Point(192, 83);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1003, 670);
            this.pnlMainContent.TabIndex = 4;
            // 
            // homePanel1
            // 
            this.homePanel1.AllowDrop = true;
            this.homePanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.homePanel1.Location = new System.Drawing.Point(0, 0);
            this.homePanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.homePanel1.Name = "homePanel1";
            this.homePanel1.Size = new System.Drawing.Size(1002, 671);
            this.homePanel1.TabIndex = 0;
            // 
            // tmrAlbumArtRotate
            // 
            this.tmrAlbumArtRotate.Interval = 30;
            this.tmrAlbumArtRotate.Tick += new System.EventHandler(this.tmrAlbumArtRotate_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1193, 842);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlNavigation);
            this.Controls.Add(this.pnlPlayerControls);
            this.Font = new System.Drawing.Font("黑体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI音乐";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlNavigation.ResumeLayout(false);
            this.pnlNavigation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.pnlPlayerControls.ResumeLayout(false);
            this.pnlPlayerControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAlbumArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.pnlMainContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlNavigation;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Button btnFavorites;
        private System.Windows.Forms.Button btnRecentPlayed;
        private System.Windows.Forms.Panel pnlTopBar;
        private System.Windows.Forms.Timer tmrProgressUpdate;
        private System.Windows.Forms.Label lblCurrentTrackName;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnPreviousTrack;
        private System.Windows.Forms.Button btnNextTrack;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnLikeTrack;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlPlayerControls;
        private System.Windows.Forms.Timer tmrAlbumArtRotate;
        private Controls.RoundPictureBox picAlbumArt;
        private System.Windows.Forms.Label label1;
        private Controls.HomePanel homePanel1;
        private System.Windows.Forms.Button btnPlayMode;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.MusicProgressBar musicProgressBar;
        private MusicVolumeBar musicVolumeBar;
        private System.Windows.Forms.Label lblSignupDate;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLogoTitle;
        private System.Windows.Forms.Button btnReport;
    }
}

