using System;

namespace MusicAI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // 存储密码的哈希值
        public string Email { get; set; }
        public DateTime SignupDate { get; set; } // 在数据库中是 NOT NULL DEFAULT GETDATE()
        public string ResetCode { get; set; }
        public DateTime? ResetCodeExpires { get; set; }
    }
}
