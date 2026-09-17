using MusicAI.BLL;
using MusicAI.DAL;
using System;
using System.Windows.Forms;

namespace UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IUserRepository userRepository = new UserRepository();
            ISingerStyleRepository singerStyleRepository = new SingerStyleRepository();
            IUserUploadedSongRepository userUploadedSongRepository = new UserUploadedSongRepository();
            IUserFavoritesRepository favoritesRepository = new UserFavoritesRepository();
            IRecentPlaysRepository recentPlaysRepository = new RecentPlaysRepository();
            IUserService userService = new UserService(userRepository);
            ISongService songService = new SongService(
                userUploadedSongRepository,
                singerStyleRepository,
                userRepository,
                favoritesRepository,
                recentPlaysRepository
            );

            Application.Run(new LoginForm(userService, songService));
        }
    }
}