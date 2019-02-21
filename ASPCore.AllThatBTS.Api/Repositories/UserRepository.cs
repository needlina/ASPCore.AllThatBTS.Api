using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using NPoco;
using System.Collections.Generic;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserT SelectUserByEmail(string email)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                EMAIL = email
            };

            return Connection.SingleOrDefault<UserT>(sql, parameters);
        }

        public int InsertUser(UserT user)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                NICKNAME = user.NickName,
                EMAIL = user.Email,
                SECRET = user.Password,
                AUTH_TYPE = user.AuthType,
                CONFIRM_YN = user.ConfirmYN
            };

            return Connection.Execute(sql, parameters);
        }


        public UserT SelectUser(string userNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                USER_NO = userNo
            };

            return Connection.SingleOrDefault<UserT>(sql, parameters);
        }

        public List<UserT> SelectAllUsers()
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            return Connection.Fetch<UserT>(sql);
        }

        public int UpdateUser(UserT user)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                USER_NO = user.UserNo,
                NICKNAME = user.NickName,
                EMAIL = user.Email
            };

            return Connection.Execute(sql, parameters);
        }

        public int DeleteUser(string userNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                USER_NO = userNo
            };

            return Connection.Execute(sql, parameters);
        }
    }
}
