using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Repositories;
using System.Collections.Generic;

namespace ASPCore.AllThatBTS.Api.Services
{
    public class BoardService
    {
        private readonly BoardRepository boardRepository;

        public BoardService()
        {
            boardRepository = new BoardRepository();
        }

        #region 게시판
        /// <summary>
        /// 게시판 조회 페이지 컨트롤
        /// </summary>
        /// <param name="boardId">게시판 ID</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <returns></returns>
        public BoardPageInfoT GetBoardPageInfo(string boardId, int pageNo, int pageSize)
        {
            return boardRepository.SelectBoardPageInfo(boardId, pageNo, pageSize);
        }

        /// <summary>
        /// 게시판의 카테고리 리스트 조회
        /// </summary>
        /// <param name="boardId">게시판 ID</param>
        /// <returns></returns>
        public List<BoardCategoryT> GetBoardCategoryList(string boardId)
        {
            return boardRepository.SelectBoardCategoryList(boardId);
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
        public List<ArticleT> GetBoardList(string boardId,
                                              int pageSize,
                                              int pageNo,
                                              string searchType,
                                              string searchKeyword)
        {
            return boardRepository.SelectBoardList(boardId, pageSize, pageNo, searchType, searchKeyword);
        }

        /// <summary>
        /// 게시물 조회 (Detail)
        /// </summary>
        /// <param name="seq">Sequence 값</param>
        /// <returns></returns>
        public ArticleT GetArticle(string seq)
        {
            return boardRepository.SelectArticle(seq);
        }


        /// <summary>
        /// 게시물 작성
        /// </summary>
        /// <param name="article">게시물 모델</param>
        /// <returns></returns>
        public int CreateArticle(ArticleT article)
        {
            return boardRepository.InsertArticle(article);
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <param name="article">게시물 모델</param>
        /// <returns></returns>
        public int SetArticle(ArticleT article)
        {
            return boardRepository.UpdateArticle(article);
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <param name="seq">게시물 Seq</param>
        /// <returns></returns>
        public int RemoveArticle(string seq)
        {
            return boardRepository.DeleteArticle(seq);
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
        public List<CommentT> GetCommentList(string seq,
                                                int pageSize,
                                                int pageNo)
        {
            return boardRepository.SelectCommentList(seq, pageSize, pageNo);
        }


        /// <summary>
        /// 댓글 추가
        /// </summary>
        /// <param name="comment">댓글 모델</param>
        /// <returns></returns>
        public int CreateComment(CommentT comment)
        {
            return boardRepository.InsertComment(comment);
        }

        /// <summary>
        /// 댓글 삭제 (DeleteFlag)
        /// </summary>
        /// <param name="commentSeq">댓글 Sequence</param>
        /// <returns></returns>
        public int RemoveComment(string commentSeq)
        {
            return boardRepository.DeleteComment(commentSeq);
        }

        /// <summary>
        /// 대댓글 리스트 조회
        /// </summary>
        /// <param name="seq">게시물 Sequence</param>
        /// <param name="commentSeq">댓글 Sequence</param>
        /// <param name="pageSize">페이지 사이즈</param>
        /// <param name="pageNo">페이지 번호</param>
        /// <returns></returns>
        public List<CommentT> GetSubCommentList(string seq,
                                                   string commentSeq,
                                                   int pageSize,
                                                   int pageNo)
        {
            return boardRepository.SelectSubCommentList(seq, commentSeq, pageSize, pageNo);
        }

        /// <summary>
        /// 대댓글 추가
        /// </summary>
        /// <param name="subComment">대댓글 모델</param>
        /// <returns></returns>
        public int CreateSubComment(CommentT subComment)
        {
            return boardRepository.InsertSubComment(subComment);
        }

        /// <summary>
        /// 대댓글 삭제
        /// </summary>
        /// <param name="subCommentSeq">대댓글 Sequence</param>
        /// <returns></returns>
        public int RemoveSubComment(string subCommentSeq)
        {
            return boardRepository.DeleteSubComment(subCommentSeq);
        }
        #endregion
    }
}
