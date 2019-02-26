using NPoco;
using System;

namespace ASPCore.AllThatBTS.Api.Entities
{
    [TableName("TB_BOARD")]
    public class ArticleT
    {
        [Column("SEQ")]
        public string Seq { get; set; }
        [Column("BOARD_ID")]
        public string BoardId { get; set; }
        [Column("CATEGORY_ID")]
        public string CategoryId { get; set; }
        [Column("SUBJECT")]
        public string Subject { get; set; }
        [Column("CONTENTS")]
        public string Contents { get; set; }
        [Column("READ_CNT")]
        public int ReadCount { get; set; }
        [Column("RECOMMEND_CNT")]
        public int RecommendCount { get; set; }
        [Column("COMMENTS_CNT")]
        public int CommentsCount { get; set; }
        [Column("IMAGE_EXIST_YN")]
        public string ImageExistYN { get; set; }
        [Column("NICKNAME")]
        public string NickName { get; set; }
        [Column("SECRET")]
        public string Secret { get; set; }
        [Column("CREATE_USER")]
        public string CreateUser { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_USER")]
        public string UpdateUser { get; set; }
        [Column("UPDATE_DT")]
        DateTime UpdateDateTime { get; set; }
    }

    [TableName("BOARD_CATEGORY")]
    public class BoardCategoryT
    {
        [Column("SEQ")]
        public string Seq { get; set; }
        [Column("BOARD_ID")]
        public string BoardId { get; set; }
        [Column("CATEGORY_ID")]
        public string CategoryId { get; set; }
        [Column("CATEGORY_NAME")]
        public string CategoryName { get; set; }
        [Column("CREATE_USER")]
        public string CreateUser { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_USER")]
        public string UpdateUser { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDateTime { get; set; }
    }

    public class CommentT
    {
        [Column("SEQ")]
        public string Seq { get; set; }
        [Column("BOARD_SEQ")]
        public string BoardSeq { get; set; }
        [Column("PARENT_COMMENT_SEQ")]
        public string ParentCommentSeq { get; set; }
        [Column("DEPTH")]
        public int Depth { get; set; }
        [Column("COMMENTS")]
        public string Comments { get; set; }
        [Column("RECOMMEND_CNT")]
        public int RecommentCount { get; set; }
        [Column("COMMENTS_CNT")]
        public int CommentsCount { get; set; }
        [Column("CREATE_USER")]
        public string CreateUser { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_USER")]
        public string UpdateUser { get; set; }
        [Column("UPDATE_DT")]
        DateTime UpdateDateTime { get; set; }
    }

    public class RecommendInfoT
    {
        [Column("SEQ")]
        public string Seq { get; set; }
        [Column("BOARD_SEQ")]
        public string BoardSeq { get; set; }
        [Column("COMMENT_SEQ")]
        public string CommentSeq { get; set; }
        [Column("RECOMMEND_TYPE")]
        public string RecommendType { get; set; }
        [Column("CREATE_USER")]
        public string CreateUser { get; set; }
        [Column("CREATE_DT")]
        DateTime CreateDateTime { get; set; }
    }

    public class BoardPageInfoT
    {
        [Column("BOARD_ID")]
        public string BoardId { get; set; }
        [Column("CURRENT_PAGE_NO")]
        public int CurrentPageNo { get; set; }
        [Column("PAGE_SIZE")]
        public int PageSize { get; set; }
        [Column("TOTAL_CNT")]
        public int TotalCount { get; set; }
        [Column("TOTAL_PAGE_CNT")]
        public int TotalPages { get; set; }
        [Column("PAGEBLOCK_SIZE")]
        public int PageBlockSize { get; set; }
        [Column("PREV_PAGE_BLOCK_YN")]
        public bool PrevPageBlockYN { get; set; }
        [Column("NEXT_PAGE_BLOCK_YN")]
        public bool NextPageBlockYN { get; set; }
    }
}
