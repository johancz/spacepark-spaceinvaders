using System;
using System.IO;
using System.Net;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            var webClient = new WebClient();
            var api = webClient.DownloadString(new Uri(@"https://swapi.dev/api/people/"));

            using (var streamWriter = new StreamWriter(@"C:\Users\Mazdak\Documents\people.json"))
            {
                streamWriter.Write(api);
            }

        }
    }
}
