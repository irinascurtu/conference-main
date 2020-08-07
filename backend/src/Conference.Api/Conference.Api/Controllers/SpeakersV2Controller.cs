using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api.Controllers
{
    /// <summary>
    /// Used to illustrate the versioning options
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersV2Controller : ControllerBase
    {
    }
}