using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Conference.Api.Infrastructure
{
    
    public class ApiVersioningErrorProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            //You can initialize your own class here. Below is just a sample.
            var errorResponse = new
            {
                ResponseCode = 101,
                ResponseMessages = "Something went wrong while selecting the api version",
                HelpLink = "https://conference-api.com/versioning/"
            };
            var response = new ObjectResult(errorResponse);
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            return response;
        }
    }
}
