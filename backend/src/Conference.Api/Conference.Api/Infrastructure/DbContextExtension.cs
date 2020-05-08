using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conference.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Conference.Api.Infrastructure
{
    public static class DbContextExtension
    {
        public static void EnsureSeeded(this ConferenceContext context)
        {
            DataSeeder.SeedData(context);
        }
    }

}

