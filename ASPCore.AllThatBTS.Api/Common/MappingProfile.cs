using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using AutoMapper;

namespace ASPCore.AllThatBTS.Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserT, MakeUserM>();
            CreateMap<UserT, ReadUserM>();
            CreateMap<UserT, ModifyUserM>();

            CreateMap<MakeUserM, UserT>();
            CreateMap<ReadUserM, UserT>();
            CreateMap<ModifyUserM, UserT>();

            CreateMap<TokenT, TokenM>();
            CreateMap<TokenM, TokenT>();

            CreateMap<ArticleM, ArticleT>();
            CreateMap<ArticleT, ArticleM>();

            CreateMap<YoutubeDataM, YoutubeT>();
            CreateMap<YoutubeT, YoutubeDataM>();

            CreateMap<TwitterDataM, TwitterT>();
            CreateMap<TwitterT, TwitterDataM>();

            CreateMap<InstagramDataM, InstagramT>();
            CreateMap<InstagramT, InstagramDataM>();
        }
    }
}
