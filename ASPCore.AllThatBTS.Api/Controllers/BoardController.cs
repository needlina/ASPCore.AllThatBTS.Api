using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ASPCore.AllThatBTS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {

        protected readonly BoardService boardService;
        protected readonly IMapper mapper;
        protected readonly Logger logger;

        public BoardController(IMapper _mapper)
        {
            boardService = new BoardService();
            mapper = _mapper;

            logger = NLog.Web.NLogBuilder.ConfigureNLog(AppConfiguration.NLogPath).GetCurrentClassLogger();
        }

        //// 게시판 조회 페이지 컨트롤
        //[HttpGet("ReadUser")]
        //public Response<ReadUserM> ReadUser(string userNo)
        //{
        //    Response<ReadUserM> response = new Response<ReadUserM>();

        //    UserT userEntity = userService.GetUser(userNo);
        //    ReadUserM user = mapper.Map<UserT, ReadUserM>(userEntity);

        //    if (user != null)
        //    {
        //        response.Result = user;
        //        response.Status = ((int)HttpStatusCode.OK).ToString();
        //        logger.Log(LogLevel.Info, response.Message);
        //    }
        //    else
        //    {
        //        response.Status = ((int)HttpStatusCode.NotFound).ToString();
        //        response.ErrMsg = "사용자가 존재하지 않습니다.";
        //        logger.Log(LogLevel.Warn, response.ErrMsg);
        //    }

        //    return response;
        //}

        

        // 게시판 조회 리스트
        // 게시물 작성
        // 게시판 카테고리 조회
        // 게시물 수정
        // 게시물 삭제
        // 댓글 리스트 조회 페이지 컨트롤
        // 댓글 리스트 조회 (ajax 콜)
        // 댓글 추가
        // 대댓글 추가
        // 댓글 삭제 (DeleteFlag)
        // 댓글 수정
    }
}