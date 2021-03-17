using System;
using System.Threading;
using RestSharp;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
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

                if (selectedMenu == 0) //ADD EXPENSE
                {
                    Console.WriteLine("Who are you traveller? ");
                    string name = Console.ReadLine();
                    Console.WriteLine();

                    //Method 1: Async API and loop through to see if we can find that name
                    //Method 2: Based on the person, call for another API with Async, and see which vehicles this character have + which planet he's from.
                    //Method 3: New Menu choice where the character can select his vehicle.
                    //Method 4: IF the vehicle fits / or IF the spaceship is not full, REGiSTER the parking and att into a database.
                    //Save the parking into a file so we can load it?
                }
                else if (selectedMenu == 1) //SHOWS ALL EXPENSES
                {
                    Console.WriteLine("Thank you for choosing SpacePark! We hope to see you soon again :)\n");
                    //METHOD: Print the Invoice to the traveller. Also add the totalSum into the database.
                    //running = false;
                }
                else //SHOWS EXPENSES SORTED BY CATEGORY
                {
                    Console.WriteLine("Terminating program.");
                    running = false;
                }
            }
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
