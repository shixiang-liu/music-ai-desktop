using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class WaitForm : Form
    {
        public bool IsCanceled { get; private set; } = false;

        public WaitForm()
        {
            InitializeComponent();
            this.Text = "请稍候";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 300;
            this.Height = 130;

            var label = new Label();
            label.Text = "AI翻唱生成中，请稍候…";
            label.Dock = DockStyle.Top;
            label.Height = 50;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Font = new System.Drawing.Font("微软雅黑", 12, System.Drawing.FontStyle.Bold);
            this.Controls.Add(label);

            var btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Width = 80;
            btnCancel.Height = 30;
            btnCancel.Top = 60;
            btnCancel.Left = (this.ClientSize.Width - btnCancel.Width) / 2;
            btnCancel.Click += (s, e) => {
                IsCanceled = true;
                this.Close();
            };
            this.Controls.Add(btnCancel);
        }
    }
}
