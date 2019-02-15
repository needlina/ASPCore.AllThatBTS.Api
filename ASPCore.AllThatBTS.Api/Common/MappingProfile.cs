using ASPCore.AllThatBTS.Api.Entities;
using ASPCore.AllThatBTS.Api.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
