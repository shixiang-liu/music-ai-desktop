using MusicAI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicAI.DAL
{
    public class UserUploadedSongRepository : IUserUploadedSongRepository
    {
        // 根据上传歌曲ID获取记录  
        public UserUploadedSong GetUploadedSongById(int uploadedSongId)
        {
            UserUploadedSong uploadedSong = null;
            string sql = "SELECT UploadedSongID, UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash FROM dbo.UserUploadedSongs WHERE UploadedSongID = @UploadedSongID;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UploadedSongID", uploadedSongId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uploadedSong = MapUserUploadedSongFromReader(reader);
                    }
                }
            }
            return uploadedSong;
        }

        // 根据用户ID获取上传的所有歌曲  
        public List<UserUploadedSong> GetUploadedSongsByUserId(int userId)
        {
            List<UserUploadedSong> uploadedSongs = new List<UserUploadedSong>();
            string sql = "SELECT UploadedSongID, UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash FROM dbo.UserUploadedSongs WHERE UserID = @UserID;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        uploadedSongs.Add(MapUserUploadedSongFromReader(reader));
                    }
                }
            }
            return uploadedSongs;
        }

        // 添加上传的歌曲记录，返回新记录的ID  
        public int AddUploadedSong(UserUploadedSong song)
        {
            string sql = @"INSERT INTO UserUploadedSongs
                   (UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash)
                   VALUES (@UserID, @OriginalFileName, @StoredFilePath, @FileType, @DurationSeconds, @FileHash); 
                   SELECT SCOPE_IDENTITY();";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", song.UserID);
                cmd.Parameters.AddWithValue("@OriginalFileName", song.OriginalFileName);
                cmd.Parameters.AddWithValue("@StoredFilePath", song.StoredFilePath);
                cmd.Parameters.AddWithValue("@FileType", song.FileType);
                cmd.Parameters.AddWithValue("@DurationSeconds", (object)song.DurationSeconds ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FileHash", song.FileHash);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // 获取用户最后上传的歌曲（现在只能用自增ID倒序）
        public UserUploadedSong GetLastUploadedSongByUserId(int userId)
        {
            UserUploadedSong uploadedSong = null;
            string sql = @"  
                   SELECT TOP 1 UploadedSongID, UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash  
                   FROM dbo.UserUploadedSongs  
                   WHERE UserID = @UserID  
                   ORDER BY UploadedSongID DESC;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uploadedSong = MapUserUploadedSongFromReader(reader);
                    }
                }
            }
            return uploadedSong;
        }

        // 映射数据库记录到实体对象  
        private UserUploadedSong MapUserUploadedSongFromReader(SqlDataReader reader)
        {
            return new UserUploadedSong
            {
                UploadedSongID = reader.GetInt32(reader.GetOrdinal("UploadedSongID")),
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                OriginalFileName = reader.GetString(reader.GetOrdinal("OriginalFileName")),
                StoredFilePath = reader.GetString(reader.GetOrdinal("StoredFilePath")),
                FileType = reader.GetString(reader.GetOrdinal("FileType")),
                DurationSeconds = reader.IsDBNull(reader.GetOrdinal("DurationSeconds")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DurationSeconds")),
                FileHash = reader.GetString(reader.GetOrdinal("FileHash"))
            };
        }

        // 根据用户ID和文件哈希获取歌曲
        public UserUploadedSong GetUploadedSongByHash(int userId, string fileHash)
        {
            const string sql = "SELECT TOP 1 UploadedSongID, UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash FROM UserUploadedSongs WHERE UserID = @UserID AND FileHash = @FileHash";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@FileHash", fileHash);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUserUploadedSongFromReader(reader);
                    }
                }
            }
            return null;
        }

        // 根据用户ID和文件哈希获取上传的歌曲
        public UserUploadedSong GetByUserIdAndHash(int userId, string hash)
        {
            string sql = "SELECT UploadedSongID, UserID, OriginalFileName, StoredFilePath, FileType, DurationSeconds, FileHash FROM UserUploadedSongs WHERE UserID = @UserID AND FileHash = @FileHash";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@FileHash", hash);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapUserUploadedSongFromReader(reader);
                }
            }
            return null;
        }

    }
}
