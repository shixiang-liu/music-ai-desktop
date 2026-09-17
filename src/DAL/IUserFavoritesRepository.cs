using System.Collections.Generic;

namespace MusicAI.DAL
{
    public interface IUserFavoritesRepository
    {
        void AddFavorite(int userId, int uploadedSongId);
        void RemoveFavorite(int userId, int uploadedSongId);
        bool IsFavorite(int userId, int uploadedSongId);
        List<int> GetFavoriteSongIds(int userId);
    }
}
