using FX.FP.NetCore.Common;
using System;

namespace FX.FP.NetCore.Service
{
    public class MySqlService
    {
        public static string conntionStr;
        private string _connStr = "DefaultConnection";
        public MySqlService(string connStr = "")
        {
            if (!string.IsNullOrEmpty(connStr))
            {
                _connStr = connStr;
            }
            conntionStr = ConfigHelper.GetSectionValue($"ConnectionStrings:{_connStr}");
        }
    }
}
