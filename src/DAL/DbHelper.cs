using System.Configuration;
using System.Data.SqlClient;

namespace MusicAI.DAL
{
    public static class DbHelper
    {
        private static readonly string ConnectionString;

        static DbHelper()
        {
            // 从配置文件中读取数据库连接字符串
            ConnectionString = ConfigurationManager.ConnectionStrings["MusicAI_ConnectionString"]?.ConnectionString;
            if (string.IsNullOrEmpty(ConnectionString))
            {
                // 如果连接字符串为空，抛出异常
                throw new ConfigurationErrorsException("未找到数据库连接字符串 'MusicAI_ConnectionString'。");
            }
        }

        public static SqlConnection GetConnection()
        {
            // 返回一个新的数据库连接
            return new SqlConnection(ConnectionString);
        }
    }
}