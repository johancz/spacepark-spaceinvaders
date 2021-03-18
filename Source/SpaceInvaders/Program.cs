using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await FetchPeople();
        }
        
        public static async Task<APIResponse> FetchData(string requestUrl)
        {
            string baseUrl = "http://swapi.dev/api/";

            string resource = requestUrl.Substring(baseUrl.Length);

            var client = new RestClient("http://swapi.dev/api/");
            var request = new RestRequest(resource, DataFormat.Json);
            // NOTE: The Swreponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object
            var response = await client.GetAsync<APIResponse>(request);
            return response;
        }

        public static async Task FetchPeople()
        {
            List<Person> persons = new List<Person>();

            APIResponse response;

            string requestUrl = "http://swapi.dev/api/people/";
            
            while (requestUrl != null)
            {
                response = await FetchData(requestUrl);

                foreach (var p in response.Results)
                {
                    Console.WriteLine(p.Name);
                    Console.WriteLine(p.Homeworld);

                    foreach (var s in p.Starships)
                    {
                        Console.WriteLine(s);
                    }
                }

                requestUrl = response.Next;
            }
        }
    }
}
