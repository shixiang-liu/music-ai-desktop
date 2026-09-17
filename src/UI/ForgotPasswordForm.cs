using System;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using MusicAI.BLL;

namespace UI
{
    // 密码重置窗体，提供密码重置功能
    public partial class ForgotPasswordForm : Form
    {
        private readonly IUserService _userService;
        private Timer timerCountdown;
        private int remainingSeconds = 60;

        public ForgotPasswordForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // 发送验证码按钮
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            lblTip.Visible = false;

            if (!IsValidEmail(txtEmail.Text))
            {
                ShowErrorTip("请输入有效的邮箱地址。");
                return;
            }

            try
            {
                bool success = _userService.SendResetCode(txtEmail.Text);
                if (!success)
                {
                    ShowErrorTip("邮箱未注册或无法发送验证码。");
                    return;
                }
                StartCountdown();
                MessageBox.Show("验证码已发送至您的邮箱，请注意查收。", "验证码已发送", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowErrorTip($"发送验证码失败：{ex.Message}");
            }
        }

        // 重置密码按钮
        private void btnReset_Click(object sender, EventArgs e)
        {
            lblTip.Visible = false;

            if (string.IsNullOrWhiteSpace(txtCode.Text) || txtCode.Text.Trim().Length != 6)
            {
                ShowErrorTip("请输入6位验证码。");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || txtNewPassword.Text.Length < 6
                || !txtNewPassword.Text.Any(char.IsDigit) || !txtNewPassword.Text.Any(char.IsLetter))
            {
                ShowErrorTip("新密码必须至少6位，且同时包含字母和数字。");
                return;
            }

            try
            {
                bool success = _userService.ResetPassword(txtEmail.Text, txtCode.Text.Trim(), txtNewPassword.Text);
                if (!success)
                {
                    ShowErrorTip("密码重置失败！验证码错误、已过期，或新密码不符合要求。");
                    return;
                }
                MessageBox.Show("密码重置成功！现在您可以使用新密码登录了。", "密码重置成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                timerCountdown?.Stop();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"密码重置过程中发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 开始验证码倒计时
        private void StartCountdown()
        {
            remainingSeconds = 60;
            btnSendCode.Enabled = false;
            btnSendCode.Text = $"重新发送 ({remainingSeconds}秒)";

            if (timerCountdown == null)
            {
                timerCountdown = new Timer { Interval = 1000 };
                timerCountdown.Tick += TimerCountdown_Tick;
            }
            timerCountdown.Start();
        }

        private void TimerCountdown_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds > 0)
            {
                btnSendCode.Text = $"重新发送 ({remainingSeconds}秒)";
            }
            else
            {
                timerCountdown.Stop();
                btnSendCode.Text = "发送验证码";
                btnSendCode.Enabled = true;
                lblTip.Visible = false;
            }
        }

        // 返回按钮
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 邮箱格式校验
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

        // 在标签上显示错误
        private void ShowErrorTip(string message)
        {
            lblTip.Text = message;
            lblTip.ForeColor = Color.Red;
            lblTip.Visible = true;
        }

        // 窗体关闭时清理 Timer
        private void ForgotPasswordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCountdown?.Stop();
            timerCountdown?.Dispose();
            timerCountdown = null;
        }
    }
}
