using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using SpaceInvaders.Objects;
using SpaceInvaders.Traveller;

namespace SpaceInvaders
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var peopleList = await FetchPeople();
            //var starshipList = await FetchStarships();

            Console.WriteLine("Welcome to SpacePark!\n");
            Thread.Sleep(1000);

            bool running = true;
            while (running)
            {
                int selectedMenu = ShowMenu("What do you want to do?", new[]
                {
                    "Register new traveller", //Index 0
                    "End current parking", //Index 1
                    "Exit program", //Index 2
                });
                Console.Clear();

                if (selectedMenu == 0)
                {
                    Console.WriteLine("Who are you traveller? ");

                    var peopleList = await FetchPeople();
                    if (peopleList.Count == 0)
                    {
                        Console.WriteLine("Sorry, you are not a Starwars character. Fuck off.");
                    }
                    else
                    {
                        Console.WriteLine($"Welcome: {peopleList[0].Name}");
                    }
                    Console.WriteLine();

                    //Method 1: Async API and loop through to see if we can find that name
                    //Method 2: Based on the person, call for another API with Async, and see which vehicles this character have + which planet he's from.
                    //Method 3: New Menu choice where the character can select his vehicle.
                    //Method 4: IF the vehicle fits / or IF the spaceship is not full, REGiSTER the parking and att into a database.
                    //Save the parking into a file so we can load it?
                }
                else if (selectedMenu == 1)
                {
                    Console.WriteLine("Thank you for choosing SpacePark! We hope to see you soon again :)\n");
                    //METHOD: Print the Invoice to the traveller. Also add the totalSum into the database.
                    //running = false;
                }
                else
                {
                    Console.WriteLine("Terminating program.");
                    running = false;
                }
            }
        }

        //API for People
        public static async Task<APIResponse> FetchDataPeople(string requestUrl)
        {
            string baseUrl = "http://swapi.dev/api/";
            string resource = requestUrl.Substring(baseUrl.Length);

            var client = new RestClient("http://swapi.dev/api/");
            var request = new RestRequest(resource, DataFormat.Json);
            // NOTE: The Swreponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object
            var response = await client.GetAsync<APIResponse>(request);
            return response;
        }

        //API for Starships
        public static async Task<APIResponseStarships> FetchDataStarship(string requestUrl)
        {
            string baseUrl = "http://swapi.dev/api/";
            string resource = requestUrl.Substring(baseUrl.Length);

            var client = new RestClient("http://swapi.dev/api/");
            var request = new RestRequest(resource, DataFormat.Json);
            // NOTE: The Swreponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object

            var response = await client.GetAsync<APIResponseStarships>(request);
            return response;
        }

        //Fetch people from API
        public static async Task<List<Person>> FetchPeople()
        {
            //Add to class list
            List<Person> persons = new List<Person>();

            APIResponse response;

            string input = Console.ReadLine();
            string requestUrl = $"http://swapi.dev/api/people/?search={input}";

            while (requestUrl != null)
            {
                response = await FetchDataPeople(requestUrl);
                persons.AddRange(response.Results);
                requestUrl = response.Next;
            }
            return persons;
        }

        //Fetch Starships from API
        public static async Task<List<Starships>> FetchStarships()
        {
            //Add to object list 
            List<Starships> starships = new List<Starships>();

            APIResponseStarships response;
            string requestUrl = "http://swapi.dev/api/starships/";

            while (requestUrl != null)
            {
                response = await FetchDataStarship(requestUrl);
                foreach (var p in response.Results)
                {
                    Console.WriteLine($"{p.Name}, Length: {p.Length}m");
                    starships.Add(p);
                }
                requestUrl = response.Next;
            }
            Console.WriteLine();
            return starships;
        }

        public static int ShowMenu(string prompt, string[] options)
        {
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty array of options.");
            }

            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }
    }
}
