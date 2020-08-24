using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiVersionNeutral]
    [ApiController]
    public class PingController : ControllerBase
    {
        // GET: api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "pong", "ping" };
        }
    }
}
