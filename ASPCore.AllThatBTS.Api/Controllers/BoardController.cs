using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using ASPCore.AllThatBTS.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

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

        #region 게시판
        [HttpGet("ReadBoardCategoryList")]
        public ListResponse<BoardCategoryM> ReadBoardCategoryList(string boardId)
        {
            ListResponse<BoardCategoryM> response = new ListResponse<BoardCategoryM>();

            List<BoardCategoryT> entities = boardService.GetBoardCategoryList(boardId);
            List<BoardCategoryM> modelList = mapper.Map<List<BoardCategoryT>, List<BoardCategoryM>>(entities);

            if (modelList.Count > 0)
            {
                response.ListResult = modelList;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시판의 카테고리 정보가 없습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }


        #endregion

        #region 게시물
        [HttpPost("ReadBoardList")]
        public Response<BoardListM> ReadBoardList(RequestBoardListM request)
        {
            Response<BoardListM> response = new Response<BoardListM>();

            NPoco.Page<ArticleT> entity = boardService.GetBoardList(request.PageNo,
                                                      request.PageSize,
                                                      request.PageBlockSize,
                                                      request.BoardId,
                                                      request.CategoryId,
                                                      request.SearchType,
                                                      request.SearchKeyword);

            int pageMaxNo = (int)((entity.TotalPages / request.PageBlockSize) + 1);
            int currPageBlock = (int)((request.PageNo / request.PageBlockSize) + 1);

            BoardPageInfoT pageInfoT = new BoardPageInfoT()
            {
                BoardId = request.BoardId,
                CurrentPageNo = request.PageNo,
                PageSize = request.PageSize,
                TotalCount = (int)entity.TotalItems,
                TotalPages = (int)entity.TotalPages,
                PageBlockSize = request.PageBlockSize,
                NextPageBlockYN = currPageBlock < pageMaxNo,
                PrevPageBlockYN = currPageBlock > 1
            };

            List<ArticleM> listModel = mapper.Map<List<ArticleT>, List<ArticleM>>(entity.Items);
            BoardPageInfoM pageInfoM = mapper.Map<BoardPageInfoT, BoardPageInfoM>(pageInfoT);

            BoardListM model = new BoardListM()
            {
                ListItems = listModel,
                BoardPageInfo = pageInfoM
            };



            if (model != null)
            {
                response.Result = model;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물 정보가 없습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [HttpGet("ReadArticle")]
        public Response<ArticleM> ReadArticle(string seq)
        {
            Response<ArticleM> response = new Response<ArticleM>();

            ArticleT entity = boardService.GetArticle(seq);
            ArticleM model = mapper.Map<ArticleT, ArticleM>(entity);

            if (model != null)
            {
                response.Result = model;
                response.Status = ((int)HttpStatusCode.OK).ToString();
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [HttpPost("WriteArticle")]
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [HttpPost("ModifyArticle")]
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
        #endregion

        #region 댓글
        [HttpGet("ReadCommentList")]
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
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "댓글이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
        [HttpPost("WriteComment")]
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
        [HttpGet("EraseComment")]
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
        [HttpGet("ReadSubCommentList")]
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
            }
            else
            {
                response.Status = ((int)HttpStatusCode.NotFound).ToString();
                response.ErrMsg = "게시물이 존재하지 않습니다.";
                logger.Log(LogLevel.Warn, response.ErrMsg);
            }

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
        [HttpPost("WriteSubComment")]
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }

        [HttpGet("EraseSubComment")]
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

            logger.Log(LogLevel.Info, string.Format("호출 성공 : {0}", MethodBase.GetCurrentMethod().Name));
            return response;
        }
        #endregion
    }
}