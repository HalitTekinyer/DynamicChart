using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete
{
    public static class ConnectionString
    {
        public static string ServerIp { get; set; }
        public static string DatabaseName { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string Get()
        {
            return @$"Server={ServerIp};Database={DatabaseName};User Id={User};Password={Password};Encrypt=False;";
        }
    }
}
