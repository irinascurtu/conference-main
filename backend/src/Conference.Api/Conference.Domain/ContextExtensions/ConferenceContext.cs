using System;
using System.Collections.Generic;
using System.Text;
using Conference.Domain.EntitiesExtensions;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Conference.Domain.ContextExtensions
{
    public partial class ConferenceContext
    {
      //  public DbSet<TicketSummary> TicketSummaries { get; set; }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<TicketSummary>().HasNoKey();
           
        }
    }
}
