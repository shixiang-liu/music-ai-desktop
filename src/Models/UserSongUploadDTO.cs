using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserSongUploadDTO
    {
        public string Username { get; set; }
        public string OriginalSongName { get; set; }
        public string Email { get; set; }
        public DateTime UploadTimestamp { get; set; }
    }
}
