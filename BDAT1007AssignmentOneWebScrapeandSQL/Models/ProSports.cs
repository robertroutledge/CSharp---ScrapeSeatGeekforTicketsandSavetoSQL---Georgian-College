using System;
using System.Collections.Generic;
using System.Text;

namespace BDAT1007AssignmentOneWebScrapeandSQL.Utilities
{
    public class ProSports
    {
        public string Category { get; set; }
        public string TeamName { get; set; }
        public string League { get; set; }
        public string ESource = "SeatGeek";
        //The Column ID in SQL is the Primary Key
    }

    public class TicketURLs
    {
        public string TeamName { get; set; }
        public string SeatGeek { get; set; }
        public string Ticketmaster = "Placeholder, Scrape TicketMaster";
        public string StubHub = "Placeholder, scrape StubHub";
        //the column ID in SQL is the Primary Key
        //Foreign Key TeamName is used to link the many possible URLs to the one ProSport Record
    }
}
