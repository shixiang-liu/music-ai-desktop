using MusicAI.Models;
using MusicAI.DAL;
using MusicAI.Common;
using System;

namespace MusicAI.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public string Login(string account, string password)
        {
            User user = _userRepository.GetUserByUsername(account);
            if (user == null)
            {
                user = _userRepository.GetUserByEmail(account);
            }

            if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null; // 用户不存在或密码错误
            }

            // 登录成功，直接返回用户名
            return user.Username;
        }

        public bool Register(string username, string password, string email)
        {
            if (_userRepository.GetUserByUsername(username) != null)
            {
                return false; // 用户名已存在
            }
            if (_userRepository.GetUserByEmail(email) != null)
            {
                return false; // 邮箱已存在
            }

            var newUser = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password), // 哈希处理密码
                Email = email,
                SignupDate = DateTime.Now
            };

            int newUserId = _userRepository.AddUser(newUser);
            return newUserId > 0; // 返回新用户ID大于0表示成功
        }

        public bool SendResetCode(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false; // 邮箱未注册
            }

            string code = CryptoHelper.GenerateSecureCode(6); // 生成6位验证码
            user.ResetCode = code;
            user.ResetCodeExpires = DateTime.Now.AddMinutes(10); // 验证码有效期10分钟

            if (_userRepository.UpdateUser(user)) // 保存验证码到数据库
            {
                try
                {
                    EmailHelper.SendVerificationCode(email, code); // 发送邮件
                    return true;
                }
                catch (Exception)
                {
                    // 可记录日志
                    return false;
                }
            }
            return false;
        }

        public bool ResetPassword(string email, string code, string newPassword)
        {
            var user = _userRepository.GetUserByEmail(email);

            // 校验用户、验证码及有效期
            if (user == null || user.ResetCode != code || !user.ResetCodeExpires.HasValue || user.ResetCodeExpires.Value < DateTime.Now)
            {
                return false;
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword); // 哈希新密码
            user.ResetCode = null;      // 清除已用验证码
            user.ResetCodeExpires = null;

            return _userRepository.UpdateUser(user);
        }

        public UserDTO GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null) return null;

            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email,
                SignupDate = user.SignupDate
            };
        }
    }
}
