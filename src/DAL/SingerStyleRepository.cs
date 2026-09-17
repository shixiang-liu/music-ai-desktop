using MusicAI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicAI.DAL
{
    public class SingerStyleRepository : ISingerStyleRepository
    {
        public SingerStyle GetSingerStyleById(int singerStyleId)
        {
            SingerStyle style = null;
            string sql = "SELECT SingerStyleID, Name FROM dbo.SingerStyles WHERE SingerStyleID = @SingerStyleID;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@SingerStyleID", singerStyleId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        style = MapSingerStyleFromReader(reader);
                    }
                }
            }
            return style;
        }

        public SingerStyle GetSingerStyleByName(string name)
        {
            SingerStyle style = null;
            string sql = "SELECT SingerStyleID, Name FROM dbo.SingerStyles WHERE Name = @Name;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        style = MapSingerStyleFromReader(reader);
                    }
                }
            }
            return style;
        }

        public List<SingerStyle> GetAllSingerStyles()
        {
            var styles = new List<SingerStyle>();
            string sql = "SELECT SingerStyleID, Name, FeaturePath, IndexPath FROM dbo.SingerStyles ORDER BY Name;";
            using (SqlConnection conn = DbHelper.GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        styles.Add(MapSingerStyleFromReader(reader));
                    }
                }
            }
            return styles;
        }

        private SingerStyle MapSingerStyleFromReader(SqlDataReader reader)
        {
            return new SingerStyle
            {
                SingerStyleID = reader.GetInt32(reader.GetOrdinal("SingerStyleID")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                FeaturePath = reader.IsDBNull(reader.GetOrdinal("FeaturePath"))
                    ? "" : reader.GetString(reader.GetOrdinal("FeaturePath")),
                IndexPath = reader.IsDBNull(reader.GetOrdinal("IndexPath"))
                    ? "" : reader.GetString(reader.GetOrdinal("IndexPath"))
            };
        }

    }
}
