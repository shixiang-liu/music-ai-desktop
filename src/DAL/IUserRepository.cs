using MusicAI.Models;

namespace MusicAI.DAL
{
    public interface IUserRepository
    {
        User GetUserById(int userId); // 根据用户ID获取用户
        User GetUserByUsername(string username); // 根据用户名获取用户
        User GetUserByEmail(string email); // 根据邮箱获取用户
        int AddUser(User user); // 添加新用户，返回用户ID
        bool UpdateUser(User user); // 更新用户信息
    }
}
