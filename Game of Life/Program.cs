using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            Generation gen = new Generation(75,25,0.2);
            Console.WindowHeight = Console.LargestWindowHeight - 15;
            Console.WindowWidth = Console.LargestWindowWidth - 30;

            // gen = new Generation("000000\n001100\n001100\n000000");

            Generation previousGeneration;

            for (int i = 0; i < 1000; i ++) { 
                // save the current generation
                previousGeneration = new Generation(gen.ToString());
                // evolve to the next generation
                gen.Next();
                /*
                Console.WriteLine("Compare");
                Console.WriteLine(previousGeneration == gen);
                Console.WriteLine(previousGeneration);
                Console.WriteLine(gen);
                Console.ReadKey();
                */
                if (previousGeneration == gen) {
                    Console.WriteLine("Stable state reached at generation " + i);
                    break;
                }

                Thread.Sleep(50);
                Console.SetCursorPosition(0,0);
            }
            

            /*
            Game game = new Game();
            game.Play(10);
            */
        }
    }
}
