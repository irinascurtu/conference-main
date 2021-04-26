using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Api.Models.Talks;
using Conference.Data.Repositories;
using Core.Data;

namespace Conference.Api.Controllers
{
    [Route("api/speakers/{speakerId}/talks")]
    // [Route("api/v{version:apiVersion}/speakers/{speakerId}/talks")]
    [ApiController]
    [ApiVersion("3.0")]
    [Produces("application/json")]
    public class TalksV3Controller : ControllerBase
    {
        private readonly ITalkRepository talkRepository;
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMapper mapper;

        public TalksV3Controller(ITalkRepository talkRepository,
            ISpeakerRepository speakerRepository, IMapper mapper
        )
        {
            this.talkRepository = talkRepository;
            this.speakerRepository = speakerRepository;
            this.mapper = mapper;
        }

        [HttpGet(Name = "GetTalksForSpeaker")]
        public ActionResult<string> GetTalksForSpeaker(int speakerId)
        {
            return "Response from Version-3";
        }
    }
}
