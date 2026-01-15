using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            // Set console size
            Console.WindowHeight = Console.LargestWindowHeight - 15;
            Console.WindowWidth = Console.LargestWindowWidth - 30;

            Generation gen = new Generation(26,25,0.2);
            gen = new Generation("000000\n001100\n001100\n000000");

            // hold the previous generation for comparison
            Generation previousGeneration;

            for (int i = 0; i < 1000; i ++) {
                Console.Clear();
                // save the current generation
                previousGeneration = new Generation(gen.ToString());
                // evolve to the next generation
                gen.Next();
                Console.WriteLine(gen);

                // check for stable state
                if (previousGeneration == gen) {
                    Console.WriteLine("Stable state reached at generation " + i);
                    break;
                }

                Thread.Sleep(50);
                // Console.SetCursorPosition(0,0);
                Console.ReadKey();
            }

            /*
            Game game = new Game();
            game.Play(10);
            */
        }
    }
}
