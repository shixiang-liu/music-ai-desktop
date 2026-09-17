using MusicAI.Models;

namespace MusicAI.BLL
{
    // 用户服务接口，定义用户相关的业务操作
    public interface IUserService
    {
        string Login(string account, string password);              // 用户登录
        bool Register(string username, string password, string email); // 用户注册
        bool SendResetCode(string email);                           // 发送密码重置验证码
        bool ResetPassword(string email, string code, string newPassword); // 重置密码
        UserDTO GetUserByUsername(string username);                 // 获取用户信息
    }
}
