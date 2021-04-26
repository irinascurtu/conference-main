using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Api.Infrastructure.Attributes;
using Conference.Api.Models.Talks;
using Conference.Data.Repositories;
using Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api.Controllers
{
   [Route("api/speakers/{speakerId}/talks")]
   // [Route("api/v{version:apiVersion}/speakers/{speakerId}/talks")]

    [ApiController]
    [ApiVersion("2.0")]
    //[V2]
    public class TalksV2Controller : ControllerBase
    {
        private readonly ITalkRepository talkRepository;
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMapper mapper;

        public TalksV2Controller(ITalkRepository talkRepository,
            ISpeakerRepository speakerRepository, IMapper mapper
        )
        {
            this.talkRepository = talkRepository;
            this.speakerRepository = speakerRepository;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetTalksForSpeaker")]
        public ActionResult<IEnumerable<TalkV2Dto>> GetTalksForSpeaker(int speakerId)
        {
            if (!speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }
            var talksForSpeaker = talkRepository.GetTalksForSpeaker(speakerId).ToList();
            return Ok(mapper.Map<IEnumerable<TalkV2Dto>>(talksForSpeaker));
        }
    }
}
