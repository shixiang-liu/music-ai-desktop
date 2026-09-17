namespace UI.Controls
{
    partial class SongListPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongListPanel));
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.songCardControl2 = new UI.Controls.SongCardControl();
            this.songCardControl1 = new UI.Controls.SongCardControl();
            this.flowPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.lblTitle.Location = new System.Drawing.Point(352, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(96, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "标题";
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.flowPanel.Controls.Add(this.songCardControl2);
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanel.Location = new System.Drawing.Point(0, 60);
            this.flowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Padding = new System.Windows.Forms.Padding(5);
            this.flowPanel.Size = new System.Drawing.Size(812, 462);
            this.flowPanel.TabIndex = 1;
            this.flowPanel.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(204)))));
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 2);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.chkSelectAll);
            this.panel2.Controls.Add(this.btnDeleteSelected);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(812, 60);
            this.panel2.TabIndex = 1;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(14, 35);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(59, 19);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "全选";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.AutoSize = true;
            this.btnDeleteSelected.FlatAppearance.BorderSize = 0;
            this.btnDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSelected.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnDeleteSelected.Location = new System.Drawing.Point(654, 18);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(111, 40);
            this.btnDeleteSelected.TabIndex = 2;
            this.btnDeleteSelected.Text = "删除所选";
            this.btnDeleteSelected.UseVisualStyleBackColor = false;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // songCardControl2
            // 
            this.songCardControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.songCardControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.songCardControl2.CoverImage = ((System.Drawing.Image)(resources.GetObject("songCardControl2.CoverImage")));
            this.songCardControl2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.songCardControl2.Duration = "时长：时长：时长：时长：时长：时长：时长";
            this.songCardControl2.IsFavorite = false;
            this.songCardControl2.Location = new System.Drawing.Point(15, 10);
            this.songCardControl2.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.songCardControl2.Name = "songCardControl2";
            this.songCardControl2.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.songCardControl2.Selected = false;
            this.songCardControl2.Size = new System.Drawing.Size(750, 62);
            this.songCardControl2.SongData = null;
            this.songCardControl2.SongId = 0;
            this.songCardControl2.SongName = "歌名";
            this.songCardControl2.TabIndex = 0;
            // 
            // songCardControl1
            // 
            this.songCardControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.songCardControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.songCardControl1.CoverImage = ((System.Drawing.Image)(resources.GetObject("songCardControl1.CoverImage")));
            this.songCardControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.songCardControl1.Duration = "时长：时长：时长：时长：时长：时长：时长：时长：时长：时长：时长：时长：时长：时长";
            this.songCardControl1.IsFavorite = false;
            this.songCardControl1.Location = new System.Drawing.Point(15, 10);
            this.songCardControl1.Margin = new System.Windows.Forms.Padding(10, 5, 10, 0);
            this.songCardControl1.Name = "songCardControl1";
            this.songCardControl1.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.songCardControl1.Selected = false;
            this.songCardControl1.Size = new System.Drawing.Size(720, 59);
            this.songCardControl1.SongData = null;
            this.songCardControl1.SongId = 0;
            this.songCardControl1.SongName = "歌名";
            this.songCardControl1.TabIndex = 0;
            // 
            // SongListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowPanel);
            this.Name = "SongListPanel";
            this.Size = new System.Drawing.Size(812, 522);
            this.flowPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private SongCardControl songCardControl1;
        private SongCardControl songCardControl2;
    }
}
