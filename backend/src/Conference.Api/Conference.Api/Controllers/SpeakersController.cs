using System.Collections.Generic;
using Conference.Data.Repositories;
using Core.Data;
using Microsoft.AspNetCore.Mvc;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Conference.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerRepository _speakerRepository;

        public SpeakersController(ISpeakerRepository speakerRepository)
        {
            _speakerRepository = speakerRepository;
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

        [HttpGet(Name = "GetSpeakers")]
        [HttpHead]
        public IActionResult GetSpeakers()
        {
            var speakersFromRepo = _speakerRepository.GetSpeakers();
          
            return Ok(speakersFromRepo);
        }

        [HttpGet("{speakerId}", Name = "GetSpeaker")]
        public IActionResult GetSpeaker(int speakerId)
        {

            var speakerFromRepo = _speakerRepository.GetSpeaker(speakerId);

            if (speakerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(speakerFromRepo);
        }

    }
}
