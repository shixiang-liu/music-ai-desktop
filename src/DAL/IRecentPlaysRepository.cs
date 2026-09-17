using System.Collections.Generic;

public interface IRecentPlaysRepository
{
    void AddRecentPlay(int userId, int uploadedSongId);
    List<int> GetRecentPlayedSongIds(int userId, int maxCount = 50);
    void DeleteRecentPlay(int userId, int songId);
}
