using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using SpaceInvaders.Objects;
using SpaceInvaders.Traveller;
using SpaceInvaders.API;
using SpaceInvaders.Helpers;
using SpaceInvaders.Database;

namespace SpaceInvaders
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Added this line to Parse double values to not mix "." and ","
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Console.WriteLine("Welcome to SpacePark!\n");
            Thread.Sleep(1000);

            while (true)
            {
                int selectedMenu = Menu.Options("What do you want to do?", new[]
                {
                    "Register new traveller", //Index 0

                    "End current parking", //Index 1

                    "Exit program", //Index 2
                });
                Console.Clear();

                if (selectedMenu == 0)
                {
                    Console.WriteLine("Who are you traveller? ");

                    var peopleList = await Fetch.People(Console.ReadLine());
                    
                    Console.WriteLine();

                    if (peopleList.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Sorry, you are not a Starwars character. Back to the void with ya!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        int selectedMenuPerson = 0;
                        if (peopleList.Count > 1)
                        {
                            selectedMenuPerson = Menu.Options("Please select ", peopleList.Select(p => p.Name).ToArray());
                        }

                        Person selectedPerson = peopleList[selectedMenuPerson];

                        if (DatabaseQueries.CheckParking(selectedPerson.Name) != null)
                        {
                            Console.WriteLine("You have already parked\nPress any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }

                        // Fetch all Starships
                        var allStarships = await Fetch.Starships();
                        Console.Clear();
                        List<Starships> personalShips = allStarships.Join(selectedPerson.Starships,
                                                                      s1 => s1.URL, s2 => s2,
                                                                      (s1, s2) => s1).ToList();

                        //If the character doesn't own a starship
                        if (personalShips.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"There is no registered starship under the name of {selectedPerson.Name}.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Welcome, {selectedPerson.Name}!\n");
                            Console.ForegroundColor = ConsoleColor.White;

                            int selectedShipIndex = Menu.Options("Please select your ship", personalShips.Select(p => p.Name).ToArray());
                            Starships selectedShip = personalShips[selectedShipIndex];
                            Console.WriteLine("\nLoading...");
                            Thread.Sleep(2000);
                            Console.Clear();

                            //SLÄNG IN NEDAN I FUNKTION
                            //We check if the starship fits in the parkinglot
                            //Parsing because the Length attribute is a String type
                            if (double.TryParse(selectedShip.Length, out double result))
                            {
                                if (result <= 30)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"You selected: {selectedShip.Name}, Length: {selectedShip.Length}m");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    //Add parking into database
                                    DatabaseQueries.AddParking(selectedPerson, selectedShip);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"We're sorry but your {selectedShip.Name} is too big ({selectedShip.Length}m) for our parking lots. (Maximum length: 30m)");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    //Do nothing as we cannot park the ship
                                }
                            }
                        }
                    }
                    Console.WriteLine();
                }
                else if (selectedMenu == 1)
                {
                    Console.WriteLine("Thank you for choosing SpacePark! We hope to see you soon again :)\n");
                    //METHOD: Print the Invoice to the traveller. Also add the totalSum into the database.
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Terminating program.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }
        }

       
    }
}
