using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Conference.Api.Models.Talks;
using Conference.Data.Repositories;
using Conference.Domain.Entities;
using Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api.Controllers
{
    [Route("api/speakers/{speakerId}/talks")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class TalksController : ControllerBase
    {
        private readonly ITalkRepository talkRepository;
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMapper mapper;

        public TalksController(ITalkRepository talkRepository,
            ISpeakerRepository speakerRepository, IMapper mapper
        )
        {
            this.talkRepository = talkRepository;
            this.speakerRepository = speakerRepository;
            this.mapper = mapper;
        }

        //[HttpGet(Name = "GetTalksForSpeaker")]
        [HttpGet(Name = "GetTalksForSpeaker"), MapToApiVersion("2.0")]
        public ActionResult<IEnumerable<TalkV1Dto>> GetTalksForSpeaker(int speakerId)
        {
            if (!speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }
            var talksForSpeaker = talkRepository.GetTalksForSpeaker(speakerId).ToList();
            return Ok(mapper.Map<IEnumerable<TalkV1Dto>>(talksForSpeaker));
        }

        [HttpGet("{talkId}", Name = "GetTalkForSpeaker")]
        public ActionResult<TalkV1Dto> GetTalksForSpeaker(int speakerId, int talkId)
        {
            if (!speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }

            var talkForSpeaker = talkRepository.GetTalk(speakerId, talkId);

            if (talkForSpeaker == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TalkV1Dto>(talkForSpeaker));
        }

        [HttpPost(Name = "CreateTalkForSpeaker")]
        public ActionResult<TalkV1Dto> CreateTalkForSpeaker(
            int speakerId, NewTalk newTalk)
        {
            #region dummyvalidation

            if (!speakerRepository.SpeakerExists(speakerId))
            {
                return NotFound();
            }
            #endregion

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var talkEntity = mapper.Map<Talk>(newTalk);

            talkRepository.AddTalk(speakerId, talkEntity);
            talkRepository.Save();

            var talkToReturn = mapper.Map<TalkV1Dto>(talkEntity);

            return CreatedAtRoute("GetTalkForSpeaker",
                new
                {
                    speakerId = speakerId,
                    talkId = talkToReturn.Id
                },
                talkToReturn);
        }

    }
}