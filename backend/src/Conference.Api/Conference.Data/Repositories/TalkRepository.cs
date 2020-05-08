using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conference.Domain;
using Core.Domain;

namespace Core.Data
{
    public interface ITalkRepository
    {
        void AddTalk(int speakerId, Talk talk);
        void DeleteTalk(Talk talk);
        Talk GetTalk(int speakerId, int talkId);
        IEnumerable<Talk> GetTalks(int speakerId);
        void UpdateTalk(Talk talks);
        bool Save();
    }

    public class TalkRepository : ITalkRepository
    {
        private readonly ConferenceContext _context;
        public TalkRepository(ConferenceContext context)
        {
            this._context = context;
        }

        public void AddTalk(int speakerId, Talk talk)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            if (talk == null)
            {
                throw new ArgumentNullException(nameof(talk));
            }
            // always set the SpeakerId to the passed-in speakerId
            talk.SpeakerId = speakerId;
            _context.Talks.Add(talk);
        }

        public void DeleteTalk(Talk talk)
        {
            _context.Talks.Remove(talk);
        }
        
        public Talk GetTalk(int speakerId, int talkId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            if (talkId == 0)
            {
                throw new ArgumentNullException(nameof(talkId));
            }

            return _context.Talks
                .Where(c => c.SpeakerId == speakerId && c.Id == talkId).FirstOrDefault();
        }

        public IEnumerable<Talk> GetTalks(int speakerId)
        {
            if (speakerId == 0)
            {
                throw new ArgumentNullException(nameof(speakerId));
            }

            return _context.Talks
                .Where(c => c.SpeakerId == speakerId)
                .OrderBy(c => c.Title).ToList();
        }

        public void UpdateTalk(Talk talks)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
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
