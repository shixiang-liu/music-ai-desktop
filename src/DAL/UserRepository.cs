using MusicAI.Models;
using System;
using System.Data.SqlClient;

namespace MusicAI.DAL
{
    public class UserRepository : IUserRepository
    {
        public User GetUserById(int userId)
        {
            User user = null;
            string sql = "SELECT UserID, Username, PasswordHash, Email, SignupDate, ResetCode, ResetCodeExpires FROM dbo.Users WHERE UserID = @UserID;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapUserFromReader(reader);
                    }
                }
            }
            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;
            string sql = "SELECT UserID, Username, PasswordHash, Email, SignupDate, ResetCode, ResetCodeExpires FROM dbo.Users WHERE Username = @Username;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapUserFromReader(reader);
                    }
                }
            }
            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;
            string sql = "SELECT UserID, Username, PasswordHash, Email, SignupDate, ResetCode, ResetCodeExpires FROM dbo.Users WHERE Email = @Email;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = MapUserFromReader(reader);
                    }
                }
            }
            return user;
        }

        public int AddUser(User user)
        {
            string sql = @"
                INSERT INTO dbo.Users (Username, PasswordHash, Email, SignupDate)
                OUTPUT INSERTED.UserID
                VALUES (@Username, @PasswordHash, @Email, @SignupDate);";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@SignupDate", user.SignupDate == DateTime.MinValue ? DateTime.Now : user.SignupDate);

                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int newUserId))
                {
                    return newUserId;
                }
                return -1; // 插入失败
            }
        }

        public bool UpdateUser(User user)
        {
            string sql = @"
                UPDATE dbo.Users SET
                    PasswordHash = @PasswordHash,
                    Email = @Email,
                    ResetCode = @ResetCode,
                    ResetCodeExpires = @ResetCodeExpires
                WHERE UserID = @UserID;";

            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", user.UserID);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@ResetCode", (object)user.ResetCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ResetCodeExpires", user.ResetCodeExpires.HasValue ? (object)user.ResetCodeExpires.Value : DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // 将 SqlDataReader 映射为 User 对象
        private User MapUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                SignupDate = reader.GetDateTime(reader.GetOrdinal("SignupDate")),
                ResetCode = reader.IsDBNull(reader.GetOrdinal("ResetCode")) ? null : reader.GetString(reader.GetOrdinal("ResetCode")),
                ResetCodeExpires = reader.IsDBNull(reader.GetOrdinal("ResetCodeExpires")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ResetCodeExpires"))
            };
        }
    }
}
