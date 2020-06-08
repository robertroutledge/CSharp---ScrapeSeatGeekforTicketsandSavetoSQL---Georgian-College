using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace WebScraping_Ebay
{
    class Program
    {
        static void Main(string[] args)
        {
            //var url = "https://www.ebay.ca/sch/i.html?_odkw=xbox+one&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR12.TRC2.A0.H0.Xlaptop.TRS0&_nkw=laptop&_sacat=200";
            //var httpclient = new HttpClient();
            //var htmlresult = httpclient.GetStringAsync(url);
            //Console.WriteLine(htmlresult.Result);
            GetHtmlAsync();
            Console.ReadLine();
        }
        static async void GetHtmlAsync()
        {
            var url = "https://www.ebay.ca/sch/i.html?_odkw=xbox+one&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR12.TRC2.A0.H0.Xlaptop.TRS0&_nkw=laptop&_sacat=200";
            var httpclient = new HttpClient();

            var html = await httpclient.GetStringAsync(url);

            //parse html doc
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);    //load doc for parsing
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("id", "")
                    .Equals("ListViewInner")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("li")
                .Where(node => node.GetAttributeValue("id", "")
                    .Contains("item")).ToList();
            Console.WriteLine(ProductListItems.Count());
            Console.WriteLine();
            foreach (var ProductListItem in ProductListItems)
            {
                //ID
                string pid = (ProductListItem.GetAttributeValue("listingid", ""));
                Console.WriteLine(pid);
                //ProductName
                string ProductName = ProductListItem.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t');
                Console.WriteLine(ProductName);
                //Price  
                string price = ProductListItem.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t');
                Console.WriteLine(price);


                //ID
                Console.WriteLine(ProductListItem.GetAttributeValue("listingid", ""));

                //ProductName
                Console.WriteLine(ProductListItem.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t'));

                //Price  
                Console.WriteLine(ProductListItem.Descendants("li").Where(node => node.GetAttributeValue("class", "").Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t'));

                //ListingType
                string ListingType = ProductListItem.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvformat")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t');
                //  Console.WriteLine(ListingType);
                //URL
                string purl = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                Console.WriteLine(purl);
                string csvRow = string.Format("{0},{1},{2}", pid, ProductName, price);

               // string path = @"D:\Nital\Summer 2020\BDAT 1007 - Social Data Mining\S20Practical\WebScraping_Ebay\data.txt";
                //File.AppendAllText(path, (csvRow));

                Console.WriteLine();

            }


        }
    }
}
