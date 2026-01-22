using System;
using System.IO;
using System.Threading;

namespace Game_of_Life {
    internal class Game {

        // file related variables
        string readText;
        string path = Path.Combine(Environment.CurrentDirectory, "savefile.txt");
        Generation gen;

        public Game() {

        }

        public void GUI() {



        }

        private void CreateRandom() {
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
            // Open the file to read from.
            readText = File.ReadAllText(path);

            Console.WriteLine($"Generation 0:\n{gen}");
            Console.ReadKey();
        }

        // create game from string input
        private void CreateFromString(string input) {
            gen = new Generation(input);

            Console.WriteLine($"Generation 0:\n{gen}");
            Console.ReadKey();
        }

        private void CreateCustom() {
            // request data from user
            Console.Write("Enter width: ");
            Console.ReadLine();

            Console.Write("Enter height: ");
            Console.ReadLine();
        }

        public void Play(int generations = int.MaxValue) {
            CreateRandom();
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
                    File.WriteAllText(path, gen.ToString());






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

/*

old play loop:

for (int i = 0; i < generations; i++) {
    // Console settings for smooth animation
    Console.SetCursorPosition(0, 1);
    Console.CursorVisible = false;

    // save the current generation
    previousGeneration = new Generation(gen.ToString());

    // evolve and display the next generation
    Console.WriteLine($"Generation {gen.generation}:\n{gen}");
    gen.Next();

    // check for stable state
    if (previousGeneration == gen) {
        Console.WriteLine("\nStable state reached at generation " + i);
        break;
    }

    Thread.Sleep(50);
}
*/

