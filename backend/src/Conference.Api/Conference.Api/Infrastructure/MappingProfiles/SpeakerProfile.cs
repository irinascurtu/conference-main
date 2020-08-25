using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Api.Models.Speakers;
using Conference.Api.Models.Talks;
using Conference.Domain.Entities;

namespace Conference.Api.Infrastructure.MappingProfiles
{
    public class SpeakerProfile :Profile
    {
        public SpeakerProfile()
        {

            CreateMap<Speaker, SpeakerFullDto>();
            CreateMap<Speaker, SpeakerFriendlyDto>();
        }
    }
}
