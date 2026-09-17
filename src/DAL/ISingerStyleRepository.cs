using MusicAI.Models;
using System.Collections.Generic;

namespace MusicAI.DAL
{
    public interface ISingerStyleRepository
    {
        SingerStyle GetSingerStyleById(int singerStyleId);
        SingerStyle GetSingerStyleByName(string name);
        List<SingerStyle> GetAllSingerStyles();
    }
}
