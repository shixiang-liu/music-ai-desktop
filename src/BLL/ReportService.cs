using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace MusicAI.BLL
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository = new ReportRepository();

        public IEnumerable<UserSongUploadDTO> GenerateUserSongUploadReport()
        {
            return _reportRepository.GetUserSongUploadDetails();
        }
    }
}
