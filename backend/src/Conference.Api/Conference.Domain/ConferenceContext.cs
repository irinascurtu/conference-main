using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Conference.Domain
{
    public partial class ConferenceContext : DbContext
    {
        public ConferenceContext(DbContextOptions<ConferenceContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();

        }

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Talk> Talks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}