using System;
using System.Security.Cryptography;
using System.Text;

namespace MusicAI.Common
{
    public static class CryptoHelper
    {
        // Demo key only — replace with a per-installation key (or DPAPI) before any real deployment.
        private const string KeyString = "0123456789abcdef0123456789abcdef";
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(KeyString);



        public static string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = new byte[16];
                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new System.IO.MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new System.IO.StreamWriter(cs))
                {
                    sw.Write(input);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = new byte[16];
                    using (var decryptor = aes.CreateDecryptor())
                    using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(input)))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new System.IO.StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        // 生成指定位数的验证码（只包含大写字母和数字）
        public static string GenerateSecureCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rng = new RNGCryptoServiceProvider();
            var data = new byte[length];
            var result = new char[length];

            rng.GetBytes(data);
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[data[i] % chars.Length];
            }
            return new string(result);
        }
    }
}
