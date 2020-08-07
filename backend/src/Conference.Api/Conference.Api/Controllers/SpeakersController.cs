using System.Collections.Generic;
using System.Linq;
using Conference.Data.Repositories;
using Conference.Domain.Entities;
using Core.Data;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Conference.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerRepository speakerRepository;

        public SpeakersController(ISpeakerRepository speakerRepository)
        {
            this.speakerRepository = speakerRepository;
        }


        [HttpOptions]
        public IActionResult GetSpeakersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        //[HttpGet(Name = "GetSpeakers")]
        //[HttpHead]
        //public IActionResult GetSpeakers()
        //{
        //    var speakersFromRepo = _speakerRepository.GetSpeakers();
        //    //map from repo to DTO

        //    return Ok(_mapper.Map<IEnumerable<SpeakerDto>>(speakersFromRepo));
        //}


        //[HttpGet(Name = "GetSpeakers")]
        //[HttpHead]
        [HttpGet]
        public ActionResult<IEnumerable<Speaker>> GetSpeakers()
        {
            var speakersFromRepo = speakerRepository.GetSpeakers();
            return Ok(speakersFromRepo);
        }

        [HttpGet("{speakerId}", Name = "GetSpeaker")]
        public IActionResult GetSpeaker(int speakerId)
        {

            var speakerFromRepo = speakerRepository.GetSpeaker(speakerId);

            if (speakerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(speakerFromRepo);
        }

    }
}
