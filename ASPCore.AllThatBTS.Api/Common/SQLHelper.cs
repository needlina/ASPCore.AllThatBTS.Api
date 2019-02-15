using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Common
{
    public static class SQLHelper
    {
        public static string GetSqlByMethodName(string methodName)
        {
            string sql = string.Empty;
            string path = path = Path.Combine(Directory.GetCurrentDirectory(), @"\SQL\" + methodName + ".sql");
            if (File.Exists(path))
            {
                sql = File.ReadAllText(path);
            }
            
            return sql;
        }
    }
}
