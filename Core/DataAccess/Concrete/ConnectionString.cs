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
        public static string GetEfConnectionString()
        {
            return @$"Server={ServerIp};Database={DatabaseName};User Id={User};Password={Password};Encrypt=False;";
        }
        public static string GetScConnectionString()
        {
            return (DatabaseName == null || DatabaseName == "")?@$"Data Source={ServerIp};Integrated Security=false;User ID={User};Password={Password};Encrypt=False;": @$"Data Source={ServerIp};Initial Catalog={DatabaseName};Integrated Security=false;User ID={User};Password={Password};Encrypt=False;";
            //return (DatabaseName == null || DatabaseName == "") ? @$"Server={ServerIp};User Id={User};Password={Password};Encrypt=False;" : @$"Server={ServerIp};Database={DatabaseName};User Id={User};Password={Password};Encrypt=False;";
        }
    }
}
