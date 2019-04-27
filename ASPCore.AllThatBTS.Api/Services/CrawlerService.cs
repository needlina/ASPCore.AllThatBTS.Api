using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Enum;
using ASPCore.AllThatBTS.Api.Repository;
using NPoco;
using System.Collections.Generic;

namespace ASPCore.AllThatBTS.Api.Service
{
    public class CrawlerService
    {
        private readonly CrawlerRepository crawlerRepository;

        public CrawlerService()
        {
            crawlerRepository = new CrawlerRepository();
        }

        public Page<YoutubeT> GetYoutubeDataList(int pageNo,
                                                 int pageSize,
                                                 string orderCondition,
                                                 string orderType)
        {
            return crawlerRepository.SelectYoutubeData(pageNo, pageSize, orderCondition, orderType);
        }

        public Page<TwitterT> GetTwitterDataList(int pageNo,
                                                 int pageSize,
                                                 string orderCondition,
                                                 string orderType)
        {
            return crawlerRepository.SelectTwitterData(pageNo, pageSize, orderCondition, orderType);
        }

        public Page<InstagramT> GetInstagramDataList(int pageNo,
                                                     int pageSize,
                                                     string orderCondition,
                                                     string orderType)
        {
            return crawlerRepository.SelectInstagramData(pageNo, pageSize, orderCondition, orderType);
        }
    }
}
