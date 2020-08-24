using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conference.Api.Models.Talks
{
    public class TalkV1Dto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
