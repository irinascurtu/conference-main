using System;
using System.Collections.Generic;
using System.Text;

namespace Conference.Domain.EntitiesExtensions
{
    public class TicketSummary
    {
        public string TicketNumber { get; set; }
        public string AttendeeFirstName { get; set; }
        public string AttendeeLastName { get; set; }
        public string AttendeeEmail { get; set; }
    }
}
