using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Conference.Api.Infrastructure.Attributes
{
    public sealed class V2Attribute : ApiVersionAttribute
    {
        public V2Attribute() : base(new ApiVersion(2, 0)) { }
    }
}
