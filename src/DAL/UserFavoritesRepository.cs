using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicAI.DAL
{
    public class UserFavoritesRepository : IUserFavoritesRepository
    {
        public void AddFavorite(int userId, int uploadedSongId)
        {
            string sql = "INSERT INTO UserFavorites (UserID, UploadedSongID) VALUES (@UserID, @UploadedSongID);";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@UploadedSongID", uploadedSongId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveFavorite(int userId, int uploadedSongId)
        {
            string sql = "DELETE FROM UserFavorites WHERE UserID = @UserID AND UploadedSongID = @UploadedSongID;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@UploadedSongID", uploadedSongId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsFavorite(int userId, int uploadedSongId)
        {
            string sql = "SELECT COUNT(*) FROM UserFavorites WHERE UserID = @UserID AND UploadedSongID = @UploadedSongID;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@UploadedSongID", uploadedSongId);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public List<int> GetFavoriteSongIds(int userId)
        {
            var list = new List<int>();
            // 用FavoriteID倒序，获取最新收藏
            string sql = "SELECT UploadedSongID FROM UserFavorites WHERE UserID = @UserID ORDER BY FavoriteID DESC;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
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
    }
}
