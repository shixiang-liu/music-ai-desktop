using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;

namespace MusicAI.BLL
{
    public interface IReportService
    {
        IEnumerable<UserSongUploadDTO> GenerateUserSongUploadReport();
    }
}
