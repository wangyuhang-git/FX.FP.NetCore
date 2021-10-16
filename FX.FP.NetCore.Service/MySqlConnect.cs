using FX.FP.NetCore.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.FP.NetCore.Service
{
    /// <summary>
    /// 创建mysql数据库连接
    /// </summary>
    public class MySqlConnect : IDisposable
    {
        public MySqlConnection connection;

        public static string conntionStr;
        public MySqlConnect(string connStr = "DefaultConnection")
        {
            conntionStr = ConfigHelper.GetSectionValue($"ConnectionStrings:{connStr}");
            connection = new MySqlConnection(conntionStr);
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
