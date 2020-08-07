using System;
using Conference.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Conference.Domain
{
    public partial class ConferenceContext : DbContext
    {
        public ConferenceContext()
        {
        }

        public ConferenceContext(DbContextOptions<ConferenceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Speaker> Speakers { get; set; }
        public virtual DbSet<Talk> Talks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Talk>(entity =>
            {
                entity.HasIndex(e => e.SpeakerId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
