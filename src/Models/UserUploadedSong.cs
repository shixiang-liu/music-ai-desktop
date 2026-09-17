using System;

namespace MusicAI.Models
{
    public class UserUploadedSong
    {
        public int UploadedSongID { get; set; }
        public int UserID { get; set; }
        public string OriginalFileName { get; set; }
        public string StoredFilePath { get; set; }
        public string FileType { get; set; }
        public int? DurationSeconds { get; set; }
        public string FileHash { get; set; }
        public bool IsFavorite { get; set; }
    }
}
