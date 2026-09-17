using MusicAI.Models;
using System.Collections.Generic;

public interface IUserUploadedSongRepository
{
    UserUploadedSong GetUploadedSongById(int uploadedSongId);
    List<UserUploadedSong> GetUploadedSongsByUserId(int userId);
    int AddUploadedSong(UserUploadedSong uploadedSong);
    UserUploadedSong GetLastUploadedSongByUserId(int userId);
    UserUploadedSong GetUploadedSongByHash(int userId, string fileHash);
    UserUploadedSong GetByUserIdAndHash(int userId, string hash);
}
