using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Api.Models.Talks;
using Conference.Domain.Entities;

namespace Conference.Api.Infrastructure.MappingProfiles
{
    public class TalkProfile : Profile
    {
        public TalkProfile()
        {
            CreateMap<Talk, TalkV1Dto>();
            CreateMap<NewTalk, Talk>();
            //CreateMap<TalkUpdateDto, Talk>();

            //CreateMap<Talk, Models.TalkUpdateDto>();
        }
    }
}
