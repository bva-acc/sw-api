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
            //client.BaseAddress = new Uri("https://swapi.dev/api/planets/");
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));
            string request = "https://swapi.co/api/planets/?page=";
            int pageNumber = 1;
            string url = request + pageNumber.ToString();
            Console.WriteLine(url);
            try
            {
                ArrayList planetList = new ArrayList();
                do {

                    HttpResponseMessage response = (await httpClient.GetAsync(url)).EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(responseBody);
                    Console.WriteLine(data);

                    foreach (int i in data.results)
                    {
                        planetList.Add(data.results[i].name);
                        Console.WriteLine(data.results[i].name);
                    }
                } while (false);
                
                    
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
