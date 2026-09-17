using MusicAI.Models;
using MusicAI.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicAI.BLL
{
    public class SongService : ISongService
    {
        private readonly IUserUploadedSongRepository _uploadedSongRepository;
        private readonly ISingerStyleRepository _singerStyleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserFavoritesRepository _favoritesRepository;
        private readonly IRecentPlaysRepository _recentPlaysRepository;

        public SongService(
            IUserUploadedSongRepository uploadedSongRepository,
            ISingerStyleRepository singerStyleRepository,
            IUserRepository userRepository,
            IUserFavoritesRepository favoritesRepository,
            IRecentPlaysRepository recentPlaysRepository)
        {
            _uploadedSongRepository = uploadedSongRepository ?? throw new ArgumentNullException(nameof(uploadedSongRepository));
            _singerStyleRepository = singerStyleRepository ?? throw new ArgumentNullException(nameof(singerStyleRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _favoritesRepository = favoritesRepository ?? throw new ArgumentNullException(nameof(favoritesRepository));
            _recentPlaysRepository = recentPlaysRepository ?? throw new ArgumentNullException(nameof(recentPlaysRepository));
        }

        // 收藏
        public void AddFavorite(int userId, int uploadedSongId)
            => _favoritesRepository.AddFavorite(userId, uploadedSongId);

        public void RemoveFavorite(int userId, int uploadedSongId)
            => _favoritesRepository.RemoveFavorite(userId, uploadedSongId);

        public bool IsFavorite(int userId, int uploadedSongId)
            => _favoritesRepository.IsFavorite(userId, uploadedSongId);

        public List<UserUploadedSong> GetFavoriteSongs(int userId)
        {
            var favIds = _favoritesRepository.GetFavoriteSongIds(userId);
            var all = _uploadedSongRepository.GetUploadedSongsByUserId(userId);
            // 只返回用户上传且被自己收藏的歌
            return all.Where(s => favIds.Contains(s.UploadedSongID)).ToList();
        }

        // 最近播放
        public void AddRecentPlay(int userId, int uploadedSongId)
            => _recentPlaysRepository.AddRecentPlay(userId, uploadedSongId);

        public void DeleteRecentPlay(int userId, int songId)
            => _recentPlaysRepository.DeleteRecentPlay(userId, songId);

        public List<UserUploadedSong> GetRecentPlayedSongs(int userId)
        {
            var recentIds = _recentPlaysRepository.GetRecentPlayedSongIds(userId);
            var all = _uploadedSongRepository.GetUploadedSongsByUserId(userId);
            return all.Where(s => recentIds.Contains(s.UploadedSongID)).ToList();
        }

        public int UploadSong(int userId, string originalFileName, string storedFilePathOnServer, string fileType, int? duration, string fileHash)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new ApplicationException("用户不存在。");

            if (string.IsNullOrWhiteSpace(storedFilePathOnServer) || !File.Exists(storedFilePathOnServer))
                throw new ArgumentException("无效的服务器文件路径或文件不存在。", nameof(storedFilePathOnServer));

            var newUpload = new UserUploadedSong
            {
                UserID = userId,
                OriginalFileName = originalFileName,
                StoredFilePath = storedFilePathOnServer,
                FileType = fileType,
                DurationSeconds = duration,
                FileHash = fileHash
            };
            return _uploadedSongRepository.AddUploadedSong(newUpload);
        }

        public UserUploadedSong GetLastUploadedSongByUserId(int userId)
            => _uploadedSongRepository.GetLastUploadedSongByUserId(userId);

        public UserUploadedSong GetUploadedSongByHash(int userId, string fileHash)
            => _uploadedSongRepository.GetUploadedSongByHash(userId, fileHash);

        public List<string> GetAllSingerStyleNames()
            => _singerStyleRepository.GetAllSingerStyles().Select(s => s.Name).ToList();

        public List<SingerStyle> GetAllSingerStyles()
            => _singerStyleRepository.GetAllSingerStyles();
    }
}
