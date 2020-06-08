using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BDAT1007AssignmentOneWebScrapeandSQL.Utilities
{
    public class FromSeatGeek
    {
        public static async void GetHtmlAsync()
        {
            //Set up variables to connect to the website and store the data gathered from it
            var teamandeventlist = new List<string>();
            var seatgeekurl = "https://seatgeek.com";

            //Create the html document and node needed to parse SeatGeek to find relevant data.
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(seatgeekurl);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);    //load doc for parsing

            //parse html doc to get to the body and class that has entertainer/sports data
            var node = htmlDocument.DocumentNode.SelectSingleNode("//body");
            var Headers = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("header-tabs nav")).ToList();

            //Initiate new lists for my Data Models
            List<ProSports> Teams = new List<ProSports>();
            List<TicketURLs> ticketwebsite = new List<TicketURLs>();

            //Parse the HTML page so that I am reading the class that gives relevant info
            //Seatgeek did not use lists, so I had to find different html attributes to use
            var DropDown = Headers[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("nav__container nav__container--sports")).ToList();
            var Categories = DropDown[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("nav__container__list")).ToList();

            //Cycle through the leagues and teams to build out my sports team data
            //The Node Categories has all the relevant information in a way that can be filtered,
            //by creating one more list that filters for teams. 
            foreach (var leagues in Categories)
            {
                //details provides the <a> attribute of each node, which is where I can extract team name and URL.
                var leaguedetails = leagues.Descendants("a").ToList();

                foreach (var teamdetails in leaguedetails)
                {
                    //initiate a new item in my list
                    ProSports team = new ProSports();
                    TicketURLs teamurls = new TicketURLs();

                    //extract the information needed on each team and add them to the model
                    team.League = leagues.GetAttributeValue("attached", "").ToUpper();
                    team.TeamName = teamdetails.InnerText.Trim();
                    team.Category = "sports";

                    //building the list showing there are a variety of ticket sellers that could be scraped for each team

                    teamurls.SeatGeek = seatgeekurl + teamdetails.GetAttributeValue("href", "");
                    teamurls.TeamName = teamdetails.InnerText.Trim();
                    //add this record to my sports model 
                    Teams.Add(team);
                    ticketwebsite.Add(teamurls);
                }

            }

            //Paramaters to connect to SQL Database
            string sqlparams = "Server=tcp:robertroutledgegeorgian.database.windows.net,1433;Initial Catalog=BDAT1007;Persist Security Info=False;User ID=admin123;Password=Essc1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlconn = new SqlConnection(sqlparams);

            //using my SQL connection, with a try/catch to print on the screen if the connection fails
            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    Console.WriteLine("Connection Open");
                }
                catch
                {
                    Console.WriteLine("Connection Failed");
                }
                //drops the table if it exists, creates it with the appropriate columns
                SqlCommand cmd1 = new SqlCommand("DROP TABLE IF EXISTS TicketOptions CREATE TABLE TicketOptions (ID int IDENTITY(1,1) PRIMARY KEY, Category varchar(50),TeamName varchar(50), League varchar(50), ESource varchar(50), Seatgeek varchar(150), Ticketmaster varchar(150), Stubhub varchar(150));", sqlconn);
                cmd1.ExecuteNonQuery();

                //Creates the insertion query as a string variable, then interates over the e
                string Teamsquery = "INSERT INTO TicketOptions (Category, TeamName, League, ESource) VALUES (@Category,@TeamName,@League,@ESource)";
                string Updatequery = "UPDATE TicketOptions SET SeatGeek=@Seatgeek, Ticketmaster=@Ticketmaster, Stubhub=@Stubhub WHERE Teamname=@Teamname";
                //Add the Teams data to SQL Database
                foreach (var item in Teams)
                {
                    //each item in the Contact list for the team necessitates a new row in SQL

                    SqlCommand cmd2 = new SqlCommand(Teamsquery, sqlconn);
                    cmd2.Parameters.AddWithValue("@Category", item.Category);
                    cmd2.Parameters.AddWithValue("@League", item.League);
                    cmd2.Parameters.AddWithValue("@TeamName", item.TeamName);
                    cmd2.Parameters.AddWithValue("@ESource", item.ESource);
                    cmd2.ExecuteNonQuery();
                    //lets me check that unique items are being written by displaying them in the terminal
                    Console.WriteLine("Item added {0}", item.TeamName);

                    foreach (var urldata in ticketwebsite)
                    {
                        if (item.TeamName == urldata.TeamName)
                        {
                            SqlCommand cmd4 = new SqlCommand(Updatequery, sqlconn);
                            cmd4.Parameters.AddWithValue("@Seatgeek", urldata.SeatGeek);
                            cmd4.Parameters.AddWithValue("@Ticketmaster", urldata.Ticketmaster);
                            cmd4.Parameters.AddWithValue("@Stubhub", urldata.StubHub);
                            cmd4.Parameters.AddWithValue("@TeamName", urldata.TeamName);
                            cmd4.ExecuteNonQuery();
                        }
                    }

                }

                sqlconn.Close();

            }
        }

    }
}
