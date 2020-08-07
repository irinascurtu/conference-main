using System;
using System.Collections.Generic;
using System.Linq;
using Conference.Domain;
using Conference.Domain.Entities;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Conference.Data.Repositories
{
    public interface ISpeakerRepository
    {
        void AddSpeaker(Speaker speaker);
        bool SpeakerExists(int speakerId);
        void DeleteSpeaker(Speaker speaker);
        Speaker GetSpeaker(int speakerId);
        IEnumerable<Speaker> GetSpeakers();
        IEnumerable<Speaker> GetSpeakers(IEnumerable<int> speakerIds);
        void UpdateSpeaker(Speaker speaker);
        bool Save();
    }

    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly ConferenceContext context;

        public SpeakerRepository(ConferenceContext context)
        {
            this.context = context;
        }


        public void AddSpeaker(Speaker speaker)
        {
            context.Speakers.Add(speaker);
        }

        public bool SpeakerExists(int speakerId)
        {
            return context.Speakers.Any(a => a.Id == speakerId);
        }

        public void DeleteSpeaker(Speaker speaker)
        {
            if (speaker == null)
            {
                throw new ArgumentNullException(nameof(speaker));
            }

            context.Speakers.Remove(speaker);
        }

        public Speaker GetSpeaker(int speakerId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            return context.Speakers.FirstOrDefault(a => a.Id == speakerId);
        }

        public IEnumerable<Speaker> GetSpeakers()
        {
            return context.Speakers.ToList();
        }

        public IEnumerable<Speaker> GetSpeakers(IEnumerable<int> speakerIds)
        {
            if (speakerIds == null)
            {
                throw new ArgumentNullException(nameof(speakerIds));
            }

            return context.Speakers.Where(a => speakerIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public void UpdateSpeaker(Speaker speaker)
        {
            // no code in this implementation
        }


        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
