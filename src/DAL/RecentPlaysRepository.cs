using MusicAI.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;

public class RecentPlaysRepository : IRecentPlaysRepository
{
    // 新增最近播放（如已有则不插入重复）
    public void AddRecentPlay(int userId, int uploadedSongId)
    {
        string sql = @"
            IF NOT EXISTS (SELECT 1 FROM RecentPlays WHERE UserID = @UserID AND UploadedSongID = @UploadedSongID)
            BEGIN
                INSERT INTO RecentPlays (UserID, UploadedSongID) VALUES (@UserID, @UploadedSongID)
            END
        ";
        using (SqlConnection conn = DbHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@UploadedSongID", uploadedSongId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    // 获取最近播放的歌曲ID列表（按自增ID倒序，最新在前）
    public List<int> GetRecentPlayedSongIds(int userId, int maxCount = 50)
    {
        var list = new List<int>();
        string sql = "SELECT TOP (@MaxCount) UploadedSongID FROM RecentPlays WHERE UserID = @UserID ORDER BY RecentPlayID DESC;";
        using (SqlConnection conn = DbHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@MaxCount", maxCount);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetInt32(0));
                }
            }
        }
        return list;
    }

    // 删除最近播放记录
    public void DeleteRecentPlay(int userId, int songId)
    {
        string sql = "DELETE FROM RecentPlays WHERE UserID = @UserID AND UploadedSongID = @SongID;";
        using (SqlConnection conn = DbHelper.GetConnection())
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@SongID", songId);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
