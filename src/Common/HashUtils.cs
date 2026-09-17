using System;
using System.IO;
using System.Security.Cryptography;

namespace MusicAI.Common
{
    public static class HashUtils
    {
        public static string GetFileHash(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(stream);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
