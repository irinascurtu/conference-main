using System.Collections.Generic;
using System.Linq;
using Conference.Api.Models.Speakers;
using Conference.Data.Repositories;
using Conference.Domain.Entities;
using Core.Data;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


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

        [HttpGet(Name = "GetSpeakers")]
        public ActionResult<IEnumerable<Speaker>> GetSpeakers()
        {
            var speakersFromRepo = speakerRepository.GetSpeakers();
            //should map from a dto //call dto instead of model
            return Ok(speakersFromRepo);
        }

        [HttpGet("{speakerId}", Name = "CheckSpeaker")]
        public IActionResult GetSpeaker(int speakerId)
        {

            var speakerFromRepo = speakerRepository.GetSpeaker(speakerId);
            if (speakerFromRepo == null)
            {
                return NotFound();
            }

            return Ok(speakerFromRepo);
        }


        [HttpHead("{speakerId}", Name = "CheckSpeaker")]
        public IActionResult CheckIfSpeakerExists(int speakerId)
        {
            var speakerExists = speakerRepository.SpeakerExists(speakerId);
            if (speakerExists)
            {
                return Ok();
            }

            return NoContent();
        }

       ///todo:create more to ilustrate different status codes
        /// + api behavior
        [HttpPut(Name = "UpdateSpeaker")]
        public ActionResult<Speaker> UpdateSpeaker(SpeakerForCreate updatedSpeaker)
        {
            if (ModelState.IsValid)
            {
                //add in db
                //return CreatedAtAction()
            }
            return Ok();
        }

        ///todo:create more to ilustrate different status codes
        /// + api behavior
        [HttpPatch(Name = "PatchSpeaker")]
        public ActionResult<Speaker> PatchSpeaker(SpeakerForCreate updatedSpeaker)
        {
            return Ok();
        }
    }
}
