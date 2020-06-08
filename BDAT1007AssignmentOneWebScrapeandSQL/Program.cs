using BDAT1007AssignmentOneWebScrapeandSQL.Utilities;
using System;

namespace BDAT1007AssignmentOneWebScrapeandSQL
{
    class Program
    {
        static void Main(string[] args)
        {

                FromSeatGeek.GetHtmlAsync();
                Console.WriteLine("Job done, press any key and enter to continue");
                Console.ReadLine();

        }
    }
}
