using MusicAI.BLL;
using System;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;

namespace UI
{
    // 注册窗体，提供用户注册功能
    public partial class RegisterForm : Form
    {
        private readonly IUserService _userService;

        public RegisterForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // 注册按钮点击
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 用户名校验
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Length < 3)
            {
                MessageBox.Show("用户名必须至少3个字符！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 两次密码一致性校验
            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("两次输入的密码不一致！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 密码强度校验
            if (txtPassword.Text.Length < 6 || !txtPassword.Text.Any(char.IsDigit) || !txtPassword.Text.Any(char.IsLetter))
            {
                MessageBox.Show("密码必须至少6位，且同时包含字母和数字！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 邮箱格式校验
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("请输入有效的邮箱地址！", "注册错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 调用业务层注册
            bool success = _userService.Register(txtUsername.Text, txtPassword.Text, txtEmail.Text);

            if (success)
            {
                MessageBox.Show("注册成功！现在您可以登录了。", "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("注册失败！用户名或邮箱可能已被占用。", "注册失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 邮箱格式验证
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // 返回按钮
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
