using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Model
{
    public class RequestCralwerDataListM
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public string OrderCondition { get; set; }
        public string OrderType { get; set; }
    }

    public class YoutubeDataM
    {
        public string Id { get; set; }
        public string YoutubeId { get; set; }
        public string ChannelName { get; set; }
        public string Title { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public ulong? ViewCount { get; set; }
        public ulong? LikeCount { get; set; }
        public ulong? DislikeCount { get; set; }
        public ulong? CommentCount { get; set; }
        public DateTime? PublishDatetime { get; set; }
        public string Url { get; set; }
        public string DeleteYN { get; set; }
        public string DeletedDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

    }
    public class TwitterDataM
    {
        public string Id { get; set; }
        public string TwitterId { get; set; }
        public string AccountName { get; set; }
        public string TweetText { get; set; }
        public string hashTags { get; set; }
        public int RetweetCount { get; set; }
        public string Url { get; set; }
        public string DeleteYN { get; set; }
        public string DeletedDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    public class InstagramDataM
    {
        public string Id { get; set; }
        public string InstagramId { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string DeleteYN { get; set; }
        public string DeletedDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
