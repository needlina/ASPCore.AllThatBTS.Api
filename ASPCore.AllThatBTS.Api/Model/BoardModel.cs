using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Model
{
    public class ArticleM
    {
        public string Seq { get; set; }
        public string BoardId { get; set; }
        public string CategoryId { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }
        public int ReadCount { get; set; }
        public int RecommendCount { get; set; }
        public int CommentsCount { get; set; }
        public string ImageExistYN { get; set; }
        public string NickName { get; set; }
        public string Secret { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string UpdateUser { get; set; }
        DateTime UpdateDateTime { get; set; }
    }

    public class WriteArticleM
    {
        public string BoardId { get; set; }
        public string CategoryId { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }
        public string ImageExistYN { get; set; }
        public string NickName { get; set; }
        public string Secret { get; set; }
    }

    public class ModifyArticleM
    {
        public string Seq { get; set; }
        public string BoardId { get; set; }
        public string CategoryId { get; set; }
        public string Subject { get; set; }
        public string Contents { get; set; }
        public string ImageExistYN { get; set; }
        public string NickName { get; set; }
        public string Secret { get; set; }
    }

    public class BoardCategoryM
    {
        public string Seq { get; set; }
        public string BoardId { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class CommentM
    {
        public string Seq { get; set; }
        public string BoardSeq { get; set; }
        public string ParentCommentSeq { get; set; }
        public int Depth { get; set; }
        public string Comments { get; set; }
        public int RecommentCount { get; set; }
        public int CommentsCount { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string UpdateUser { get; set; }
        DateTime UpdateDateTime { get; set; }
    }

    public class WriteCommentM
    {
        public string BoardSeq { get; set; }
        public string Comments { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

    public class WriteSubCommentM
    {
        public string BoardSeq { get; set; }
        public string ParentCommentSeq { get; set; }
        public int Depth { get; set; }
        public string Comments { get; set; }
        public string CreateUser { get; set; }
    }

    public class RecommendInfoM
    {
        public string Seq { get; set; }
        public string BoardSeq { get; set; }
        public string CommentSeq { get; set; }
        public string RecommendType { get; set; }
        public string CreateUser { get; set; }
        DateTime CreateDateTime { get; set; }
    }

    public class BoardPageInfoM
    {
        public string BoardId { get; set; }
        public int CurrentPageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageBlockSize { get; set; }
        public bool PrevPageBlockYN { get; set; }
        public bool NextPageBlockYN { get; set; }
    }

    public class BoardListM
    {
        public List<ArticleM> ListItems { get; set; }
        public BoardPageInfoM BoardPageInfo { get; set; }
    }


    public class RequestBoardListM
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public int PageBlockSize { get; set; } = 10;
        public string BoardId { get; set; } = "";
        public string CategoryId { get; set; } = "";
        public string SearchType { get; set; } = "";
        public string SearchKeyword { get; set; } = "";
    }
}
