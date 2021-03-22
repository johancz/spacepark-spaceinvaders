﻿using RestSharp;
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
        private const string _baseURL = "http://swapi.dev/api/";

        //Fetch people from API
        public static async Task<List<Person>> People(string input)
        {
            Console.WriteLine("\nLoading...");

            var client = new RestClient(_baseURL);
            string requestUrl = $"http://swapi.dev/api/people/?search={input}";
            APIResponseTraveller response;
            List<Person> persons = new List<Person>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(_baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponseTraveller>(request);

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
            APIResponseStarships response;
            List<Starships> starships = new List<Starships>();

            while (requestUrl != null)
            {
                string resource = requestUrl.Substring(_baseURL.Length);
                var request = new RestRequest(resource, DataFormat.Json);
                response = await client.GetAsync<APIResponseStarships>(request);

                starships.AddRange(response.Results);
                requestUrl = response.Next;
            }

            return starships;
        }

    }
}