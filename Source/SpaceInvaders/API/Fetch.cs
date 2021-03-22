using RestSharp;
using SpaceInvaders.Objects;
using SpaceInvaders.Traveller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.API
{
    public static class Fetch
    {
        //API for People
        public static async Task<APIResponseTraveller> DataPeople(string requestUrl)
        {
            string baseUrl = "http://swapi.dev/api/";
            string resource = requestUrl.Substring(baseUrl.Length);

            var client = new RestClient("http://swapi.dev/api/");
            var request = new RestRequest(resource, DataFormat.Json);

            return await client.GetAsync<APIResponseTraveller>(request);
        }

        //API for Starships
        public static async Task<APIResponseStarships> DataStarships(string requestUrl)
        {
            string baseUrl = "http://swapi.dev/api/";
            string resource = requestUrl.Substring(baseUrl.Length);

            var client = new RestClient("http://swapi.dev/api/");
            var request = new RestRequest(resource, DataFormat.Json);

            var response = await client.GetAsync<APIResponseStarships>(request);
            return response;
        }

        //Fetch people from API
        public static async Task<List<Person>> People(string input)
        {
            //Add to class list
            List<Person> persons = new List<Person>();
            APIResponseTraveller response;


            //We use user input to search through the API for a Starwars character
            string requestUrl = $"http://swapi.dev/api/people/?search={input}";
            Console.WriteLine("\nLoading...");

            while (requestUrl != null)
            {
                response = await DataPeople(requestUrl);
                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }

        //Fetch Starships from API
        public static async Task<List<Starships>> Starships()
        {
            //Add to object list 
            List<Starships> starships = new List<Starships>();

            APIResponseStarships response;
            string requestUrl = "http://swapi.dev/api/starships/";

            while (requestUrl != null)
            {
                response = await DataStarships(requestUrl);
                starships.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return starships;
        }

    }
}
