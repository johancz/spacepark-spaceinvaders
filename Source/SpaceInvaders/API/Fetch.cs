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
        private struct RequestURLs
        {
            internal static string People = "http://swapi.dev/api/people/";
            internal static string Starships = "http://swapi.dev/api/starships/";
        }

        // Generic method for fetching data from the API (swapi.com)
        public static async Task<List<T>> Data<T>(string requestUrl)
        {
            var client = new RestClient(_baseURL);
            APIResponse<T> response;
            List<T> persons = new List<T>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(_baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponse<T>>(request);

                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }

        //Fetch people from API
        public static Task<List<Person>> People(string input)
        {
            string requestUrl = RequestURLs.People;
            if (input != null)
            {
                requestUrl += $"?search={input}";
            }

            return Fetch.Data<Person>(requestUrl);
        }

        //Fetch Starships from API
        public static Task<List<Starships>> Starships()
        {
            return Fetch.Data<Starships>(RequestURLs.Starships);
        }
    }
}
