using ASPCore.AllThatBTS.Api.Common;
using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Enum;
using NPoco;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ASPCore.AllThatBTS.Api.Repositories
{
    public class BoardRepository : BaseRepository
    {
        #region 게시판
        /// <summary>
        /// 게시판 조회 페이지 컨트롤
        /// </summary>
        /// <param name="boardId">게시판 ID</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <returns></returns>
        

        /// <summary>
        /// 게시판의 카테고리 리스트 조회
        /// </summary>
        /// <param name="boardId">게시판 ID</param>
        /// <returns></returns>
        public List<BoardCategoryT> SelectBoardCategoryList(string boardId)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                BOARD_ID = boardId
            };

            return Connection.Fetch<BoardCategoryT>(sql, parameters);
        }
        #endregion

        #region 게시물
        /// <summary>
        /// 게시물 리스트 조회
        /// </summary>
        /// <param name="boardId">게시판ID</param>
        /// <param name="pageSize">페이지 사이즈</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <param name="searchType">검색 조건 타입 : 제목,내용,작성자</param>
        /// <param name="searchKeyword">검색어</param>
        /// <returns></returns>
        public Page<ArticleT> SelectBoardList(int pageNo,
                                              int pageSize,
                                              int pageBlockSize,
                                              string boardId,
                                              string categoryId,
                                              string searchType,
                                              string searchKeyword)
        {

            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);
            var parameters = new object();

            switch (searchType)
            {
                case SearchType.All:
                    parameters = new
                    {
                        BOARD_ID = boardId,
                        CATEGORY_ID = categoryId,
                        SEARCH_TYPE = searchType,
                        SUBJECT = searchKeyword,
                        CONTENTS = searchKeyword
                    };
                    break;
                case SearchType.Subject:
                    parameters = new
                    {
                        BOARD_ID = boardId,
                        CATEGORY_ID = categoryId,
                        SUBJECT = searchKeyword,
                        CONTENTS = ""
                    };
                    break;
                case SearchType.Contents:
                    parameters = new
                    {
                        BOARD_ID = boardId,
                        CATEGORY_ID = categoryId,
                        SUBJECT = "",
                        CONTENTS = searchKeyword

                    };
                    break;
                default:
                    break;
            }

            Page<ArticleT> pageEntity = Connection.Page<ArticleT>(pageNo, pageSize, sql, parameters);


            return pageEntity;
        }

        /// <summary>
        /// 게시물 조회 (Detail)
        /// </summary>
        /// <param name="seq">Sequence 값</param>
        /// <returns></returns>
        public ArticleT SelectArticle(string seq)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = seq
            };

            return Connection.SingleOrDefault<ArticleT>(sql, parameters);
        }


        /// <summary>
        /// 게시물 작성
        /// </summary>
        /// <param name="article">게시물 모델</param>
        /// <returns></returns>
        public int InsertArticle(ArticleT article)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                BOARD_ID = article.BoardId,
                CATEGORY_ID = article.CategoryId,
                SUBJECT = article.Subject,
                CONTENTS = article.Contents,
                NICKNAME = article.NickName,
                SECRET = article.Secret
            };

            return Connection.Execute(sql, parameters);
        }
        
        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <param name="article">게시물 모델</param>
        /// <returns></returns>
        public int UpdateArticle(ArticleT article)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = article.Seq,
                BOARD_ID = article.BoardId,
                CATEGORY_ID = article.CategoryId,
                SUBJECT = article.Subject,
                CONTENTS = article.Contents,
                NICKNAME = article.NickName,
                SECRET = article.Secret
            };

            return Connection.Execute(sql, parameters);
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <param name="seq">게시물 Seq</param>
        /// <returns></returns>
        public int DeleteArticle(string seq)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = seq
            };

            return Connection.Execute(sql, parameters);
        }
        #endregion

        #region 댓글
        /// <summary>
        /// 댓글 리스트 조회
        /// </summary>
        /// <param name="seq">게시물 Sequence</param>
        /// <param name="pageSize">페이지 사이즈</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <returns></returns>
        public List<CommentT> SelectCommentList(string seq, 
                                                int pageSize,
                                                int pageNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = seq,
                PAGE_SIZE = pageSize,
                PAGE_NO = pageNo
            };

            return Connection.Fetch<CommentT>(sql, parameters);
        }


        /// <summary>
        /// 댓글 추가
        /// </summary>
        /// <param name="comment">댓글 모델</param>
        /// <returns></returns>
        public int InsertComment(CommentT comment)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                // 댓글 모델
            };

            return Connection.Execute(sql, parameters);
        }

        /// <summary>
        /// 댓글 삭제 (DeleteFlag)
        /// </summary>
        /// <param name="commentSeq">댓글 Sequence</param>
        /// <returns></returns>
        public int DeleteComment(string commentSeq)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = commentSeq
            };

            return Connection.Execute(sql, parameters);
        }

        /// <summary>
        /// 대댓글 리스트 조회
        /// </summary>
        /// <param name="seq">게시물 Sequence</param>
        /// <param name="commentSeq">댓글 Sequence</param>
        /// <param name="pageSize">페이지 사이즈</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <returns></returns>
        public List<CommentT> SelectSubCommentList(string seq,
                                                   string commentSeq,
                                                   int pageSize,
                                                   int pageNo)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SEQ = seq,
                COMMENT_SEQ = commentSeq,
                PAGE_SIZE = pageSize,
                PAGE_NO = pageNo
            };

            return Connection.Fetch<CommentT>(sql, parameters);
        }

        /// <summary>
        /// 대댓글 추가
        /// </summary>
        /// <param name="subComment">대댓글 모델</param>
        /// <returns></returns>
        public int InsertSubComment(CommentT subComment)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                // 대댓글 모델
            };

            return Connection.Execute(sql, parameters);
        }

        /// <summary>
        /// 대댓글 삭제
        /// </summary>
        /// <param name="subCommentSeq">대댓글 Sequence</param>
        /// <returns></returns>
        public int DeleteSubComment(string subCommentSeq)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                SUBCOMMENT_SEQ = subCommentSeq
            };

            return Connection.Execute(sql, parameters);
        }
        #endregion
    }
}
