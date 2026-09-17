namespace UI.Controls
{
    partial class SongCardControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSongName = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.picCover = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnHeart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Location = new System.Drawing.Point(106, 11);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(37, 15);
            this.lblSongName.TabIndex = 0;
            this.lblSongName.Text = "歌名";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(106, 34);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(37, 15);
            this.lblDuration.TabIndex = 1;
            this.lblDuration.Text = "时长";
            // 
            // picCover
            // 
            this.picCover.Image = global::UI.Properties.Resources.music;
            this.picCover.Location = new System.Drawing.Point(38, 11);
            this.picCover.Name = "picCover";
            this.picCover.Size = new System.Drawing.Size(41, 38);
            this.picCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCover.TabIndex = 2;
            this.picCover.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnHeart
            // 
            this.btnHeart.BackColor = System.Drawing.Color.Transparent;
            this.btnHeart.FlatAppearance.BorderSize = 0;
            this.btnHeart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeart.Image = global::UI.Properties.Resources.heart_outline;
            this.btnHeart.Location = new System.Drawing.Point(669, 10);
            this.btnHeart.Margin = new System.Windows.Forms.Padding(0);
            this.btnHeart.Name = "btnHeart";
            this.btnHeart.Size = new System.Drawing.Size(35, 35);
            this.btnHeart.TabIndex = 5;
            this.btnHeart.TabStop = false;
            this.btnHeart.UseVisualStyleBackColor = false;
            this.btnHeart.Click += new System.EventHandler(this.btnHeart_Click);
            // 
            // SongCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnHeart);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.picCover);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSongName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.Name = "SongCardControl";
            this.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.Size = new System.Drawing.Size(720, 60);
            ((System.ComponentModel.ISupportInitialize)(this.picCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnHeart;
    }
}
