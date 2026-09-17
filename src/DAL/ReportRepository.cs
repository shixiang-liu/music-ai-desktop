using MusicAI.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class ReportRepository : IReportRepository
    {
        public IEnumerable<UserSongUploadDTO> GetUserSongUploadDetails()
        {
            var details = new List<UserSongUploadDTO>();
            string spName = "GetReport_UserSongUploadDetails";

            using (var conn = DbHelper.GetConnection())
            using (var cmd = new SqlCommand(spName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        details.Add(new UserSongUploadDTO
                        {
                            Username = reader["Username"].ToString(),
                            OriginalSongName = reader["OriginalFileName"].ToString(),
                            Email = reader["Email"].ToString(),
                            UploadTimestamp = (DateTime)reader["UploadTimestamp"]
                        });
                    }
                }
            }
            return details;
        }
    }
}
