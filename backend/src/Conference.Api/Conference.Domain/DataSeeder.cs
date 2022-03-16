using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Conference.Domain.Entities;
using Core.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Conference.Domain
{
    
    public static class DataSeeder
    {

        public static void SeedData(ConferenceContext _context)
        {
            if (!_context.Speakers.Any())
            {
                _context.Speakers.AddRange(LoadSpeakers());
                _context.SaveChanges();
            }
        }

        private static List<Speaker> LoadSpeakers()
        {
            var jsonPath = @"D:\learning\MyGit\conference-main\backend\src\Conference.Api\Conference.Domain\data.json";
            using (StreamReader file = File.OpenText(jsonPath))
            {

                JsonSerializer serializer = new JsonSerializer();
                serializer.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                var speakers= (List<Speaker>)serializer.Deserialize(file, typeof(List<Speaker>));
                return speakers;
            }
        }
    }
}
