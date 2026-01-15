using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            // Set console size
            Console.WindowHeight = Console.LargestWindowHeight - 15;
            Console.WindowWidth = Console.LargestWindowWidth - 30;

            Generation gen = new Generation(26,25,0.2);
            // Generation gen = new Generation("100000\n001100\n001100\n000000");
            Console.WriteLine($"Generation 0:\n{gen}");
            Console.ReadKey();

            // hold the previous generation for comparison
            Generation previousGeneration;

            for (int i = 0; i < 1000; i ++) {
                // Console settings for smooth animation
                Console.SetCursorPosition(0, 0);
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

            /*
            Game game = new Game();
            game.Play(10);
            */
        }
    }
}
