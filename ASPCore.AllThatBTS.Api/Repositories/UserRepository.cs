using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using NPoco;
using System.Collections.Generic;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.BizDac
{
    public class UserRepository
    {

        private string connectionString;

        public UserRepository()
        {
            var appSetting = new AppConfiguration();
            connectionString = appSetting.SqlDataConnection;
        }

        public IDatabase Connection
        {
            get
            {
                return new Database(connectionString,
                                DatabaseType.MySQL,
                                MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            }
        }

        public int InsertUser(UserT user)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.ExecuteScalar<int>(sql, user.NickName);
        }

        public UserT SelectUser(string userNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.SingleOrDefault<UserT>(sql, userNo);
        }

        public List<UserT> SelectAllUsers()
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.Fetch<UserT>(sql);
        }

        public int UpdateUser(UserT user)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.ExecuteScalar<int>(sql, user.NickName);
        }

        public int DeleteUser(string userNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.ExecuteScalar<int>(sql, userNo);
        }







        

    }
}
