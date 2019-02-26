using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using NPoco;
using System;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Repository
{
    public class AuthRepository : BaseRepository
    {
        public int UpsertToken(TokenT apiToken)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                USER_NO = apiToken.UserNo,
                ACCESS_TOKEN = apiToken.AccessToken,
                REFRESH_TOKEN = apiToken.RefreshToken,
                SCOPE = apiToken.Scope,
                ACCESS_EXPIRE_DT = apiToken.AccessTokenExpireDate,
                REFRESH_EXPIRE_DT = apiToken.RefreshTokenExpireDate
            };

            return Connection.Execute(sql, parameters);
        }

        internal TokenT SelectToken(string userNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                USER_NO = userNo
            };

            return Connection.SingleOrDefault<TokenT>(sql, parameters);
        }

        public TokenT SelectTokenByRefreshToken(string refreshToken)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                REFRESH_TOKEN = refreshToken
            };

            return Connection.SingleOrDefault<TokenT>(sql, parameters);
        }
    }
}
