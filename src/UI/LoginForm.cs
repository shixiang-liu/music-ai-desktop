using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MusicAI.BLL;
using MusicAI.Common;
using MusicAI.DAL;

namespace UI
{
    // 登录窗体，提供用户登录功能
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;
        private readonly ISongService _songService;

        public LoginForm(IUserService userService, ISongService songService)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _songService = songService ?? throw new ArgumentNullException(nameof(songService));
            LoadSavedCredentials();
        }

        // 登录
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = _userService.Login(txtAccount.Text, txtPassword.Text);
            if (username == null)
            {
                MessageBox.Show("用户名/邮箱或密码错误", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 始终保存账号
            Properties.Settings.Default.SavedUsername = txtAccount.Text;

            // 只在勾选“记住密码”时保存加密密码
            if (chkRemember.Checked)
            {
                Properties.Settings.Default.SavedPassword = CryptoHelper.Encrypt(txtPassword.Text);
            }
            else
            {
                Properties.Settings.Default.SavedPassword = string.Empty;
            }
            Properties.Settings.Default.Save();

            this.Hide();
            MainForm mainForm = new MainForm(username, _userService, _songService);
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
        }
        
        // 注册
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(_userService);
            this.Hide();
            registerForm.FormClosed += (s, args) => this.Show();
            registerForm.ShowDialog();
        }

        // 忘记密码
        private void btnReset_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotPasswordForm = new ForgotPasswordForm(_userService);
            this.Hide();
            forgotPasswordForm.FormClosed += (s, args) => this.Show();
            forgotPasswordForm.ShowDialog();
        }

        // 加载保存的账号、密码
        private void LoadSavedCredentials()
        {
            // 检查数据库是否有账号（防止首次无账号时UI混乱）
            bool hasUser = false;
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Users", conn))
            {
                conn.Open();
                int userCount = (int)cmd.ExecuteScalar();
                hasUser = userCount > 0;
            }

            // 填写账号
            txtAccount.Text = Properties.Settings.Default.SavedUsername ?? "";

            // 填写密码和勾选状态
            string savedPasswordEncrypted = Properties.Settings.Default.SavedPassword;
            if (!string.IsNullOrEmpty(savedPasswordEncrypted))
            {
                try
                {
                    txtPassword.Text = CryptoHelper.Decrypt(savedPasswordEncrypted);
                    chkRemember.Checked = true;
                }
                catch
                {
                    txtPassword.Text = "";
                    chkRemember.Checked = false;
                }
            }
            else
            {
                txtPassword.Text = "";
                chkRemember.Checked = false;
            }
        }

        // 取消记住密码时，立即清空本地保存的密码和输入框
        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkRemember.Checked)
            {
                Properties.Settings.Default.SavedPassword = string.Empty;
                Properties.Settings.Default.Save();
                txtPassword.Text = "";
            }
        }
    }
}