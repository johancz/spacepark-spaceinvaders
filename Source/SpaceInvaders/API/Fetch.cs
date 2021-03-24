using RestSharp;
using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceInvaders.API
{
    public static class Fetch
    {
        private const string _baseURL = "http://swapi.dev/api/";

        //Fetch people from API
        public static async Task<List<Person>> People(string input)
        {
            var client = new RestClient(_baseURL);
            string requestUrl = $"http://swapi.dev/api/people/?search={input}";
            APIResponse<Person> response;
            List<Person> persons = new List<Person>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(_baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponse<Person>>(request);

                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }

        //Fetch Starships from API
        public static async Task<List<Starships>> Starships()
        {
            var client = new RestClient(_baseURL);
            string requestUrl = "http://swapi.dev/api/starships/";
            APIResponse<Starships> response;
            List<Starships> starships = new List<Starships>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(_baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponse<Starships>>(request);

                starships.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return starships;
        }
    }
}
