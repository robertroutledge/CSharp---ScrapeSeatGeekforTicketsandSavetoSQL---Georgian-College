using System;
using System.Collections.Generic;

namespace BDAT1007AsgOneWebApp.Models
{
    public partial class TicketOptions
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string TeamName { get; set; }
        public string League { get; set; }
        public string Esource { get; set; }
        public string Seatgeek { get; set; }
        public string Ticketmaster { get; set; }
        public string Stubhub { get; set; }
    }
}
