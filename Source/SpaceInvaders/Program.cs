using SpaceInvaders.API;
using SpaceInvaders.Database;
using SpaceInvaders.Helpers;
using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Program
    {
        private const double maxLengthToParkStarship = 35;
        private const int totalParkingLots = 5;

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

                if (selectedMenu == 0) // menu option: Register new traveller
                {
                    var selectedPerson = ChoosePerson("Who are you traveller?").Result;

                    if (selectedPerson == null)
                    {
                        continue; // Go back to the start menu.
                    }

                    // If the person is already parked, go back to the start menu
                    if (DatabaseQueries.CheckParking(selectedPerson.Name) != null)
                    {
                        Console.WriteLine($"\nThere is already an active parking registered on {selectedPerson.Name}\n\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        continue; // Go back to the start menu.
                    }

                    // Fetch all Starships
                    var allStarships = await Fetch.Starships();
                    Console.Clear();
                    List<Starships> personalShips = allStarships.Join(selectedPerson.Starships,
                                                                    s1 => s1.URL, s2 => s2,
                                                                    (s1, s2) => s1).ToList();

                    // If the character doesn't own a starship, go back to the start menu
                    if (personalShips.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"There is no registered starship under the name of {selectedPerson.Name}.\n");
                        Console.ResetColor();
                        continue; // Go back to the start menu.
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Welcome, {selectedPerson.Name}!\n");
                    Console.ResetColor();

                    int selectedShipIndex = Menu.Options("Please select your ship", personalShips.Select(p => p.Name).ToArray());
                    Starships selectedShip = personalShips[selectedShipIndex];
                    Console.WriteLine("\nLoading...");
                    Thread.Sleep(2000);
                    Console.Clear();

                    // We check if the starship fits in the parkinglot
                    // Parsing because the Length attribute is a String type
                    if (double.TryParse(selectedShip.Length, out double result))
                    {
                        if (result <= maxLengthToParkStarship)
                        {
                            if (DatabaseQueries.OccupiedParkings() >= totalParkingLots)
                            {
                                Console.WriteLine("Parking lot is full, try again later. PEACE!\n");
                                continue;
                            }
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"You selected: {selectedShip.Name}, Length: {selectedShip.Length}m");

                            //Add parking into database
                            DatabaseQueries.AddParking(selectedPerson, selectedShip);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"We're sorry but your {selectedShip.Name} is too big ({selectedShip.Length}m) for our parking lots. (Maximum length: 35m)");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();
                }

                else if (selectedMenu == 1) // menu option: End current parking
                {
                    var selectedPerson = ChoosePerson("Who is leaving our beautiful parking station?").Result;

                    if (selectedPerson == null)
                    {
                        continue; // Go back to the start menu.
                    }

                    //If there is a active parking, see method CheckParking, then print InVoice.
                    if (DatabaseQueries.CheckParking(selectedPerson.Name) != null)
                    {
                        Console.Clear();
                        DatabaseQueries.EndParking(selectedPerson);
                        Console.WriteLine("Thank you for choosing SpacePark! We hope to see you soon again :)\n");
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("There is no current parking under the name: " + selectedPerson.Name + "\n");
                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else // menu option: Exit program
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Terminating program.");
                    Console.ResetColor();
                    break;
                }
            }
        }

        private async static Task<Person> ChoosePerson(string prompt)
        {
            string input = "";
            while (input == "")
            {
                Console.WriteLine($"{prompt} (enter at least one character)");
                input = Console.ReadLine();
                Console.Clear();
            }

            Console.WriteLine("Loading...");
            var peopleList = await Fetch.People(input);
            Console.Clear();

            // If the person is not a Star Wars character, go back to the start menu
            if (peopleList.Count == 0)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, you are not a Starwars character. Back to the void with ya!\n");
                Console.ResetColor();
                return null;
            }

            int selectedMenuPerson = 0;
            if (peopleList.Count > 1)
            {
                Console.Clear();
                selectedMenuPerson = Menu.Options("Please select ", peopleList.Select(p => p.Name).ToArray());
                Console.WriteLine("\nLoading..");
            }
            return peopleList[selectedMenuPerson];
        }
    }
}
