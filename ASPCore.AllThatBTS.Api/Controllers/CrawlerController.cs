using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Enum;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Service;
using ASPCore.AllThatBTS.Api.Services;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        protected readonly CrawlerService crawlerService;
        protected readonly IMapper mapper;
        protected readonly Logger logger;
        // GET: /<controller>/
        public CrawlerController(IMapper _mapper)
        {
            crawlerService = new CrawlerService();
            mapper = _mapper;

            logger = NLog.Web.NLogBuilder.ConfigureNLog(AppConfiguration.NLogPath).GetCurrentClassLogger();
        }

        [AllowAnonymous]
        [HttpPost("GetYoutubeDataList")]
        public ListResponse<YoutubeDataM> GetYoutubeDataList(RequestCralwerDataListM request)
        {
            ListResponse<YoutubeDataM> response = new ListResponse<YoutubeDataM>();

            RequestCralwerDataListMValidator validator = new RequestCralwerDataListMValidator();
            ValidationResult results = validator.Validate(request);

            NPoco.Page<YoutubeT> youtubeDataEntities = crawlerService.GetYoutubeDataList(request.PageNo, request.PageSize, request.OrderCondition, request.OrderType);
            List<YoutubeDataM> youtubeDataList = mapper.Map<List<YoutubeT>, List<YoutubeDataM>>(youtubeDataEntities.Items);

            if (youtubeDataList != null || youtubeDataList.Count > 0)
            {
                response.ListResult = youtubeDataList;
                response.CurrentPage = youtubeDataEntities.CurrentPage;
                response.ItemsPerPage = youtubeDataEntities.ItemsPerPage;
                response.TotalItems = youtubeDataEntities.TotalItems;
                response.TotalPages = youtubeDataEntities.TotalPages;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                throw new NotFoundException("데이터가 존재하지 않습니다.", "데이터 조회 오류", LayerID.CrawlerController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [AllowAnonymous]
        [HttpPost("GetTwitterDataList")]
        public ListResponse<TwitterDataM> GetTwitterDataList(RequestCralwerDataListM request)
        {
            ListResponse<TwitterDataM> response = new ListResponse<TwitterDataM>();

            RequestCralwerDataListMValidator validator = new RequestCralwerDataListMValidator();
            ValidationResult results = validator.Validate(request);

            NPoco.Page<TwitterT> twitterDataEntities = crawlerService.GetTwitterDataList(request.PageNo, request.PageSize, request.OrderCondition, request.OrderType);
            List<TwitterDataM> twitterDataList = mapper.Map<List<TwitterT>, List<TwitterDataM>>(twitterDataEntities.Items);

            if (twitterDataList != null || twitterDataList.Count > 0)
            {
                response.ListResult = twitterDataList;
                response.CurrentPage = twitterDataEntities.CurrentPage;
                response.ItemsPerPage = twitterDataEntities.ItemsPerPage;
                response.TotalItems = twitterDataEntities.TotalItems;
                response.TotalPages = twitterDataEntities.TotalPages;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                throw new NotFoundException("데이터가 존재하지 않습니다.", "데이터 조회 오류", LayerID.CrawlerController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;

        }

        [AllowAnonymous]
        [HttpPost("GetInstagramDataList")]
        public ListResponse<InstagramDataM> GetInstagramDataList(RequestCralwerDataListM request)
        {
            ListResponse<InstagramDataM> response = new ListResponse<InstagramDataM>();

            RequestCralwerDataListMValidator validator = new RequestCralwerDataListMValidator();
            ValidationResult results = validator.Validate(request);

            NPoco.Page<InstagramT> instagramDataEntities = crawlerService.GetInstagramDataList(request.PageNo, request.PageSize, request.OrderCondition, request.OrderType);
            List<InstagramDataM> instagramDataList = mapper.Map<List<InstagramT>, List<InstagramDataM>>(instagramDataEntities.Items);

            if (instagramDataList != null || instagramDataList.Count > 0)
            {
                response.ListResult = instagramDataList;
                response.CurrentPage = instagramDataEntities.CurrentPage;
                response.ItemsPerPage = instagramDataEntities.ItemsPerPage;
                response.TotalItems = instagramDataEntities.TotalItems;
                response.TotalPages = instagramDataEntities.TotalPages;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                throw new NotFoundException("데이터가 존재하지 않습니다.", "데이터 조회 오류", LayerID.CrawlerController);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
    }
}
