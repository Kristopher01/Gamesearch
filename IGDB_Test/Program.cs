using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace IGDB_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Game you want information for...");
            var chosenGame = Console.ReadLine();
            GetGameInfo(chosenGame);
            //GetGame(2830);
            Console.ReadLine();
        }

        private static void GetGameInfo(string gameName)
        {
            //var json = new SimpleJson();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.DefaultRequestHeaders.Add("user-key", "f10bdd92c9ab47aee35f515863d45909");
                
                var url = "https://api-2445582011268.apicast.io";
                var page = "games";
                var parms = string.Format("?search={0}", gameName);
                //var key = "?user_key=f10bdd92c9ab47aee35f515863d45909";

                var fullUrl = string.Format("{0}/{1}/{2}", url, page, parms);
                var response = client.GetStringAsync(new Uri(fullUrl)).Result;
                //Console.WriteLine(response);

                
                JArray o = JArray.Parse(response);

                Console.WriteLine("Which game are you look for?");
                Console.WriteLine();
                foreach (var item in o)
                {
                    //Console.WriteLine(item["id"]);
                    GetGameTitles((int)item["id"]);
                }

                string chosenID = Console.ReadLine();
                GetGame(int.Parse(chosenID));
                //Console.WriteLine((int)o.SelectToken("id[0]"));

            }
        }

       private static void GetGameTitles(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.DefaultRequestHeaders.Add("user-key", "f10bdd92c9ab47aee35f515863d45909");

                var url = "https://api-2445582011268.apicast.io";
                var page = "games";
                var parms = string.Format("{0}?Fields=*", id);

                var fullUrl = string.Format("{0}/{1}/{2}", url, page, parms);
                var response = client.GetStringAsync(new Uri(fullUrl)).Result;
                JArray o = JArray.Parse(response);

                //var test2 = from data in o select new { name = data["name"], 

                var test = o.Select(c => new { id = (string)c["id"], name = (string)c["name"] }).FirstOrDefault();
                Console.WriteLine("ID: {0} - Name: {1}", test.id, test.name);

            }
        }

        private static void GetGame(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.DefaultRequestHeaders.Add("user-key", "f10bdd92c9ab47aee35f515863d45909");

                var url = "https://api-2445582011268.apicast.io";
                var page = "games";
                var parms = string.Format("{0}?Fields=*", id);

                var fullUrl = string.Format("{0}/{1}/{2}", url, page, parms);
                var response = client.GetStringAsync(new Uri(fullUrl)).Result;
                JArray o = JArray.Parse(response);
                //Console.WriteLine(o);

                var data = o.Select(c => new { id = (int)c["id"], name = (string)c["name"], url = (string)c["url"], summary = (string)c["summary"], storyline = (string)c["storyline"], hypes = (int)c["hypes"], rating = (long)c["rating"] }).FirstOrDefault();

                Console.WriteLine("ID: {0} \r\n \r\n Name: {1} \r\n \r\n Url: {2} \r\n \r\n Summary {3} \r\n \r\n Storyline: {4} \r\n \r\n Rating: {5} \r\n \r\n Hypes: {6}", data.id, data.name, data.url, data.summary, data.storyline, data.rating, data.hypes);

                //var name = from c in o select (string)c["name"];
                //Console.WriteLine(string.Format("Name = {0}", name.FirstOrDefault()));
                //Console.WriteLine();

                //var url2 = from c in o select (string)c["url"];
                //Console.WriteLine(string.Format("URL = {0}", url2.FirstOrDefault()));
                //Console.WriteLine();

                //var summary = from c in o select (string)c["summary"];
                //Console.WriteLine(string.Format("Summary = {0}", summary.FirstOrDefault()));
                //Console.WriteLine();

                //var rating = from c in o select (string)c["rating"];
                //Console.WriteLine(string.Format("Rating = {0}", rating.FirstOrDefault()));
                //Console.WriteLine();

                //var popularity = from c in o select (string)c["popularity"];
                //Console.WriteLine(string.Format("Popularity = {0}", popularity.FirstOrDefault()));

            }
        }
    }

    public class GameInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string summary { get; set; }
        public string storyline { get; set; }
        public Collection collection { get; set; }
        public Franchise franchise { get; set; }
        public int hypes { get; set; }
        public float rating { get; set; }
        public float popularity { get; set; }
    }

    public class Collection
    {
        public int id { get; set; }
        public string name { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int[] games { get; set; }
    }

    public class Franchise
    {
        public int id { get; set; }
        public string name { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int[] games { get; set; }
    }
}
