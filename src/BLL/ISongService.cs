using MusicAI.Models;
using System.Collections.Generic;

namespace MusicAI.BLL
{
    public interface ISongService
    {
        int UploadSong(int userId, string originalFileName, string storedFilePathOnServer, string fileType, int? duration, string fileHash);
        List<string> GetAllSingerStyleNames();
        List<SingerStyle> GetAllSingerStyles();
        UserUploadedSong GetLastUploadedSongByUserId(int userId);
        UserUploadedSong GetUploadedSongByHash(int userId, string fileHash);

        // 收藏
        void AddFavorite(int userId, int uploadedSongId);
        void RemoveFavorite(int userId, int uploadedSongId);
        bool IsFavorite(int userId, int uploadedSongId);
        List<UserUploadedSong> GetFavoriteSongs(int userId);

        // 最近播放
        void AddRecentPlay(int userId, int uploadedSongId);
        void DeleteRecentPlay(int userId, int songId);
        List<UserUploadedSong> GetRecentPlayedSongs(int userId);
    }
}
