using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApiSW
{

    class Program
    {

        static HttpClient httpClient = new HttpClient();

        // Show content
        static void ShowContent(string content)
        {
            Console.WriteLine(content);
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            string request = "https://swapi.dev/api/planets/?page=";

            dynamic data;
            bool next = true;
            int pageNumber = 1;

            try
            {
                ArrayList planetList = new ArrayList();
                do
                {
                    string url = request + pageNumber.ToString();

                    HttpResponseMessage response = (await httpClient.GetAsync(url)).EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject(responseBody);

                    foreach (var i in data.results)
                    {
                        planetList.Add(new Planet { Name = i.name, Population = i.population == "unknown" ? 0 : i.population });
                    }
                    pageNumber++;
                    Newtonsoft.Json.Linq.JToken token = data["next"];
                    if (token.Type == Newtonsoft.Json.Linq.JTokenType.Null)
                    {
                        next = false;
                    }
                } while (next);

                using (ApplicationContext db = new ApplicationContext())
                {
                    var rows = from o in db.Planets select o;
                    foreach (var row in rows)
                    {
                        db.Planets.Remove(row);
                    }
                    db.SaveChanges();

                    foreach (Planet o in planetList)
                    {
                        db.Planets.Add(o);
                    }

                    db.SaveChanges();
                    Console.WriteLine("Objects saved successfully");

                    var planets = db.Planets.ToList();
                    Console.WriteLine("List of objects:");
                    foreach (Planet p in planets)
                    {
                        Console.WriteLine($"{p.Name} - {p.Population}");
                    }
                }

                // ShowContent(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}