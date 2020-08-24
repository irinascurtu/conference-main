using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conference.Domain;
using Conference.Domain.Entities;
using Core.Domain;

namespace Core.Data
{
    public interface ITalkRepository
    {
        void AddTalk(int speakerId, Talk talk);
        void DeleteTalk(Talk talk);
        Talk GetTalk(int speakerId, int talkId);
        IEnumerable<Talk> GetTalksForSpeaker(int speakerId);
        void UpdateTalk(Talk talks);
        bool Save();
        IEnumerable<Talk> GetAllTalks();
        Talk GetTalk(int talkId);
    }

    public class TalkRepository : ITalkRepository
    {
        private readonly ConferenceContext context;
        public TalkRepository(ConferenceContext context)
        {
            this.context = context;
        }

        public void AddTalk(int speakerId, Talk talk)
        {
            talk.SpeakerId = speakerId;
            context.Talks.Add(talk);
        }

        public void DeleteTalk(Talk talk)
        {
            context.Talks.Remove(talk);
        }

        public Talk GetTalk(int speakerId, int talkId)
        {
            return context.Talks.FirstOrDefault(c => c.SpeakerId == speakerId && c.Id == talkId);
        }

        public Talk GetTalk(int talkId)
        {
            return context.Talks.FirstOrDefault(c => c.Id == talkId);
        }

        public IEnumerable<Talk> GetTalksForSpeaker(int speakerId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            return context.Talks
                .Where(c => c.SpeakerId == speakerId)
                .OrderBy(c => c.Id).ToList();
        }

        public IEnumerable<Talk> GetAllTalks()
        {
            return context.Talks.ToList();
        }

        public void UpdateTalk(Talk talks)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

    }
}
