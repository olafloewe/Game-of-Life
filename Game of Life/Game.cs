using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Game_of_Life {
    internal class Game {

        // file related variables
        string readText;
        string path = Path.Combine(Environment.CurrentDirectory, "savefile.txt");
        Generation gen;

        public Game() {
            GUI();
        }

        // GUI to choose game options
        public void GUI() {

            // choose game creation method

            // CreateRandom();
            StartMenu();
            // CreateFromFile("savefile.txt");

        }





        // start menu after successful login to select options to proceed
        private void StartMenu() {
            // welcome user
            Console.Clear();
            Console.WriteLine($"Welcome to Conways game of life!\n");

            Console.WriteLine("Please select an option:\n");

            // build selection dictionary
            Dictionary<String, Delegate> link = new Dictionary<String, Delegate>();
            link.Add("Generate Random Board", new Action(() => CreateRandom() ));
            link.Add("Generate Custom Board", new Action(() => {
                int width;
                int height;
                double density;

                do {
                    Console.Clear();
                    Console.WriteLine("Custom Board Generation:\n");

                    // request data from user
                    Console.Write("Enter width(1-50): ");
                    width = ReadInput(50);

                    Console.Write("Enter height(1-100): ");
                    height = ReadInput(100);

                    Console.Write("Enter population density: ");
                    if (!double.TryParse(Console.ReadLine(), out density)) throw new Exception("Oops, something went wrong with parsing your double.");

                } while (width <= 0 || height <= 0 || density <= 0.0 || density > 1.0 || width > 50 || height > 100);

                // generate custom board
                CreateCustom(width, height, density);
            }));
            link.Add("Load Saved File", new Action(() => {
                CreateFromFile(path);
            }));

            // asks user to select a page
            SelectionPage(link);
        }

        // builds a selection page and calls delegate
        private void SelectionPage(Dictionary<String, Delegate> dict) {
            for (int i = 0; i < dict.Count(); i++) Console.WriteLine($"{i + 1}. {dict.Keys.ToArray()[i]}"); // display options
            int selection = ReadInput(dict.Count()); // ask for input
            dict.Values.ToArray()[selection - 1].DynamicInvoke(); // execute selected option
        }

        // read and return a key input stroke from the console to be used for GUI
        private static int ReadInput(int inputRange) {

            string input = Console.ReadLine();
            // parse key to string
            if (!int.TryParse(input, out int result)) throw new Exception("Oops, something went wrong with parsing your int.");

            return result;
        }

        private void CreateRandom() {
            Console.Clear();
            Random rnd = new Random();

            // "random" width and height between 1 and 12
            int width = rnd.Next(10, 50);
            int height = rnd.Next(10, 100);

            // "random" density between 0.1 and 0.7
            double density = rnd.NextDouble() * 0.6 + 0.1;

            gen = new Generation(width, height, density);

            Console.WriteLine($"Dimensions: {height}x{width} populationdensity: {density}\nGeneration 0:\n{gen}");
            Console.ReadKey();
        }

        // create game from file input
        private void CreateFromFile(string input) {
            Console.Clear();
            // Open the file to read from.
            readText = File.ReadAllText(path);
            
            gen = new Generation(readText);
            Console.WriteLine($"\nGeneration 0:\n{gen}");
            Console.ReadKey();
        }

        private void CreateCustom(int width, int height, double density) {
            Console.Clear();
            gen = new Generation(width, height, density);
        }

        public void Play(int generations = int.MaxValue) {
            // hold the previous generation for comparison
            Generation previousGeneration = new Generation(gen.ToString());
            Generation evenGeneration = new Generation(gen.ToString());
            int sleepTime = 50;

            int counter = 0;
            bool exit = false;

            do {
                // play loop
                do {
                    // Console settings for smooth animation
                    Console.SetCursorPosition(0, 1);
                    Console.CursorVisible = false;

                    // save the current generation
                    previousGeneration = new Generation(gen.ToString());
                    // save every even generation (cycle length 1)
                    if (counter % 2 == 0) evenGeneration = new Generation(gen.ToString());

                    // evolve and display the next generation
                    Console.WriteLine($"Generation {gen.generation}:\n{gen}");
                    Console.WriteLine("Press Esc to exit and any other key to pause.");
                    gen.Next();

                    // check for stable state
                    if (previousGeneration == gen) {
                        Console.WriteLine("\nStable state reached at generation " + counter);
                        exit = true;
                        break;
                    }
                    // increase sleep time if cycle of length 2 is detected
                    if (counter > 2 && evenGeneration == gen) {
                        Console.WriteLine("\nCycle state with cycle length 1 reached at generation " + counter);
                        sleepTime = 400;
                        counter--; // neutralize counter increase
                    }

                    Thread.Sleep(sleepTime);

                    counter++;
                } while (counter < generations && !Console.KeyAvailable);

                // check for user input (interupt key)
                ConsoleKey interuptkey = Console.ReadKey(true).Key;
                if (interuptkey != ConsoleKey.Escape) {
                    // pause menu
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game Paused.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press 's' to save and exit.");
                    Console.WriteLine("Press 'c' to continue without saving.");
                    Console.WriteLine("Press 'e' to exit without saving.");

                    // TODO implement save / exit logic

                    ConsoleKey key = Console.ReadKey(true).Key;

                    // save to file
                    if (key == ConsoleKey.S) {  
                        File.WriteAllText(path, gen.ToString());

                        Console.Clear();
                        Console.Write("Saved file.\nShutting down");
                        for (int i = 0; i < 3; i++) {
                            Thread.Sleep(1000);
                            Console.Write(".");
                        }
                        break;

                    }

                    // shutdown
                    if (key == ConsoleKey.E) {
                        Console.Clear();
                        Console.Write("Shutting down");
                        for (int i = 0; i < 3; i++) {
                            Thread.Sleep(1000);
                            Console.Write(".");
                        }
                        break;
                    }

                    // exit condition DOSNT WORK ???
                    exit = (key == ConsoleKey.Escape);
                    Console.Clear();
                }
                // exit condition
                exit = (interuptkey == ConsoleKey.Escape);
            } while (!exit);
        }
    }
}