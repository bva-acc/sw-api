using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

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
                do {
                    string url = request + pageNumber.ToString();

                    HttpResponseMessage response = (await httpClient.GetAsync(url)).EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject(responseBody);

                    foreach (var i in data.results)
                    {
                        planetList.Add(i.name);
                    }
                    pageNumber++;
                    Newtonsoft.Json.Linq.JToken token = data["next"];
                    if (token.Type == Newtonsoft.Json.Linq.JTokenType.Null) {
                        next = false;
                    }
                } while (next);

                foreach (object o in planetList)
                {
                    Console.WriteLine(o);
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
