using System.Net;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Async
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Download("https://docs.microsoft.com/en-us/dotnet/");

            
        }

        public static async void Download(string url)
        {
            // await returns the control flow of the program to the caller of the function
            // and finish execution before download completion.
            // await DownloadHtmlAsync(url);
            // ...
            // use Wait() to wait async be completed:
            DownloadHtmlAsync(url).Wait();
        }

        public static async Task DownloadHtmlAsync(string url)
        {
            var webClient = new WebClient();
            var html = await webClient.DownloadStringTaskAsync(url);

            using (var streamWriter = new StreamWriter(@"./resultAsync.html"))
            {
                await streamWriter.WriteAsync(html);
            }  
        }

        public static void DownloadHtml (string url)
        {
            var webClient = new WebClient();
            var html = webClient.DownloadString(url);

            using (var streamWriter = new StreamWriter(@"./result.html"))
            {
                streamWriter.Write(html);
            }
        }
    }
}
