using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
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

        public Response<BoardPageInfoM> ReadBoardPageInfo([FromBody]string boardId, [FromBody]int pageNo, [FromBody]int pageSize)
        {
            Response<BoardPageInfoM> response = new Response<BoardPageInfoM>();

            BoardPageInfoT entity = boardService.GetBoardPageInfo(boardId, pageNo, pageSize);
            BoardPageInfoM model = mapper.Map<BoardPageInfoT, BoardPageInfoM>(entity);

            if (model != null)
            {
                response.Result = model;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시판 페이지 정보가 없습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;

        }

        public ListResponse<BoardCategoryM> ReadBoardCategoryList(string boardId)
        {
            ListResponse<BoardCategoryM> response = new ListResponse<BoardCategoryM>();

            List<BoardCategoryT> entities = boardService.GetBoardCategoryList(boardId);
            List<BoardCategoryM> modelList = mapper.Map<List<BoardCategoryT>, List<BoardCategoryM>>(entities);

            if (modelList.Count > 0)
            {
                response.ListResult = modelList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시판의 카테고리 정보가 없습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public List<ArticleM> ReadBoardList(string boardId,
                                              int pageSize,
                                              int pageNo,
                                              string searchType,
                                              string searchKeyword)
        {
            ListResponse<ArticleM> response = new ListResponse<ArticleM>();

            List<ArticleT> entities = boardService.GetBoardList(boardId, pageSize, pageNo, searchType, searchKeyword);
            List<ArticleM> modelList = mapper.Map<List<ArticleT>, List<ArticleM>>(entities);

            if (modelList.Count > 0)
            {
                response.ListResult = modelList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public ArticleM SelectArticle(string seq)
        {
            Response<ArticleM> response = new Response<ArticleM>();

            ArticleT entity = boardService.GetArticle(seq);
            ArticleM model = mapper.Map<ArticleT, ArticleM>(entity);

            if (model != null)
            {
                response.Result = model;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public Response WriteArticle(WriteArticleM article)
        {
            int result = 0;
            Response response = new Response();

            ArticleT entity = mapper.Map<WriteArticleM, ArticleT>(article);

            result = boardService.CreateArticle(entity);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "글 작성이 완료되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.BadRequest).ToString();
                response.ErrMsg = "글 작성이 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public Response ModifyArticle(ModifyArticleM article)
        {
            int result = 0;
            Response response = new Response();

            ArticleT entity = mapper.Map<ModifyArticleM, ArticleT>(article);

            result = boardService.SetArticle(entity);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "글 수정이 완료되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.BadRequest).ToString();
                response.ErrMsg = "글 수정이 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        [HttpGet("EraseArticle")]
        public Response EraseArticle(string seq)
        {
            int result = 0;
            Response response = new Response();

            result = boardService.RemoveArticle(seq);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "글이 삭제되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.ErrMsg = "글 삭제가 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public ListResponse<CommentM> ReadCommentList(string seq,
                                                         int pageSize,
                                                         int pageNo)
        {
            ListResponse<CommentM> response = new ListResponse<CommentM>();

            List<CommentT> entities = boardService.GetCommentList(seq, pageSize, pageNo);
            List<CommentM> modelList = mapper.Map<List<CommentT>, List<CommentM>>(entities);

            if (modelList.Count > 0)
            {
                response.ListResult = modelList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "댓글이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public Response WriteComment(WriteCommentM comment)
        {
            int result = 0;
            Response response = new Response();

            CommentT entity = mapper.Map<WriteCommentM, CommentT>(comment);

            result = boardService.CreateComment(entity);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "댓글 작성이 완료되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.BadRequest).ToString();
                response.ErrMsg = "댓글 작성이 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }


        public Response EraseComment(string commentSeq)
        {
            int result = 0;
            Response response = new Response();

            result = boardService.RemoveComment(commentSeq);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "댓글이 삭제되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.ErrMsg = "댓글 삭제가 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public ListResponse<CommentM> ReadSubCommentList(string seq,
                                                 string commentSeq,
                                                 int pageSize,
                                                 int pageNo)
        {
            ListResponse<CommentM> response = new ListResponse<CommentM>();

            List<CommentT> entities = boardService.GetSubCommentList(seq, commentSeq, pageSize, pageNo);
            List<CommentM> modelList = mapper.Map<List<CommentT>, List<CommentM>>(entities);

            if (modelList.Count > 0)
            {
                response.ListResult = modelList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }

        public Response WriteSubComment(WriteSubCommentM comment)
        {
            int result = 0;
            Response response = new Response();

            CommentT entity = mapper.Map<WriteSubCommentM, CommentT>(comment);

            result = boardService.CreateSubComment(entity);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "대댓글 작성이 완료되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.BadRequest).ToString();
                response.ErrMsg = "대댓글 작성이 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
        }


        public Response EraseSubComment(string subCommentSeq)
        {
            int result = 0;
            Response response = new Response();

            result = boardService.RemoveSubComment(subCommentSeq);

            if (result > 0)
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.Message = "대댓글이 삭제되었습니다.";
                logger.Log(LogLevel.Info, response.Message);
            }
            else
            {
                response.Status = ((int)HttpStatusCode.OK).ToString();
                response.ErrMsg = "대댓글 삭제가 실패하였습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            return response;
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
    }
}