using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleApiSW
{
   
    class Program
    {

        static HttpClient client = new HttpClient();

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
            client.BaseAddress = new Uri("https://swapi.dev/api/planets/4/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
               var content = await client.GetStringAsync("https://swapi.dev/api/planets/4/");
                ShowContent(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
