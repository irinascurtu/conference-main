using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Conference.Api.Infrastructure;
using Conference.Api.Models.Speakers;
using Conference.Data.Repositories;
using Conference.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Net.Http.Headers;

namespace Conference.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMapper mapper;

        public SpeakersController(ISpeakerRepository speakerRepository, IMapper mapper)
        {
            this.speakerRepository = speakerRepository;
            this.mapper = mapper;
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

            return Ok(speakersFromRepo);
        }

        [Produces("application/json",
            "application/vnd.dni.speaker.full+json",
            "application/vnd.dni.speaker.full.hateoas+json",
            "application/vnd.dni.speaker.friendly.hateoas+json",
            "application/vnd.dni.speaker.friendly.hateoas+xml"
            )]
        [HttpGet("{speakerId}", Name = "GetSpeaker")]
        public IActionResult GetSpeaker(int speakerId, [FromHeader(Name = "Accept")] string mediaType)
        {

            var speakerFromRepo = speakerRepository.GetSpeaker(speakerId);
            if (speakerFromRepo == null)
            {
                return NotFound();
            }

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var includeLinks = parsedMediaType.SubTypeWithoutSuffix
                .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);


            if (parsedMediaType.SubTypeWithoutSuffix == "vnd.dni.speaker.full.hateoas")
            {
                var fullEntity = mapper.Map<SpeakerFullDto>(speakerFromRepo);
                var link = new LinkHelper<SpeakerFullDto>(fullEntity);

                link.Links.Add(new Link
                {
                    Href = Url.Link("GetSpeaker", null),
                    Rel = "self",
                    method = "GET"
                });

                link.Links.Add(new Link
                {
                    Href = Url.Link("UpdateSpeaker", new { speakerId = speakerFromRepo.Id }),
                    Rel = "update-speaker",
                    method = "PUT"
                });

                link.Links.Add(new Link
                {
                    Href = Url.Link("DeleteSpeaker", new { speakerId = speakerFromRepo.Id }),
                    Rel = "delete-speaker",
                    method = "DELETE"
                });

                return Ok(link);
            }

       
            if (parsedMediaType.SubTypeWithoutSuffix == "vnd.dni.speaker.friendly.hateoas")
            {
                var friendlyEntity = mapper.Map<SpeakerFriendlyDto>(speakerFromRepo);
                var link = new LinkHelper<SpeakerFriendlyDto>(friendlyEntity);
                link.Links.Add(new Link
                {
                    Href = Url.Link("GetSpeaker", null),
                    Rel = "self",
                    method = "GET"
                });

                link.Links.Add(new Link
                {
                    Href = Url.Link("UpdateSpeaker", new { speakerId = speakerFromRepo.Id }),
                    Rel = "update-speaker",
                    method = "PUT"
                });

                link.Links.Add(new Link
                {
                    Href = Url.Link("DeleteSpeaker", new { speakerId = speakerFromRepo.Id }),
                    Rel = "delete-speaker",
                    method = "DELETE"
                });
                return Ok(link);
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


        [HttpPut("{speakerId}", Name = "UpdateSpeaker")]
        public ActionResult<Speaker> UpdateSpeaker(int speakerId, SpeakerForCreate updatedSpeaker)
        {
            if (ModelState.IsValid)
            {
                //add in db
                //return CreatedAtAction()
            }
            return Ok();
        }

        //[HttpDelete(Name = "DeleteSpeaker")]
        [HttpDelete("{speakerId}", Name = "DeleteSpeaker")]
        public ActionResult<Speaker> DeleteSpeaker(int speakerId)
        {
            return Ok();
        }
    }
}
