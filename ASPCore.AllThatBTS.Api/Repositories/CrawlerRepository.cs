using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using NPoco;
using System.Collections.Generic;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Repository
{
    public class CrawlerRepository : BaseRepository
    {
        public Page<YoutubeT> SelectYoutubeData(int pageNo,
                                                    int pageSize,
                                                    string orderCondition,
                                                    string orderType)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            sql += " ORDER BY " + orderCondition + " " + orderType;

            Page<YoutubeT> pageEntity = Connection.Page<YoutubeT>(pageNo, pageSize, sql);
            return pageEntity;

        }

        public Page<TwitterT> SelectTwitterData(int pageNo,
                                                    int pageSize,
                                                    string orderCondition,
                                                    string orderType)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            sql += " ORDER BY " + orderCondition + " " + orderType;

            Page<TwitterT> pageEntity = Connection.Page<TwitterT>(pageNo, pageSize, sql);
            return pageEntity;
        }

        public Page<InstagramT> SelectInstagramData(int pageNo,
                                                        int pageSize,
                                                        string orderCondition,
                                                        string orderType)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            sql += " ORDER BY " + orderCondition + " " + orderType;

            Page<InstagramT> pageEntity = Connection.Page<InstagramT>(pageNo, pageSize, sql);
            return pageEntity;
        }
    }
}
