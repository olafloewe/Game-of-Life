using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            // Set console size
            Console.WindowHeight = Console.LargestWindowHeight - 15;
            Console.WindowWidth = Console.LargestWindowWidth - 30;
            Console.ForegroundColor = ConsoleColor.White;

            Game game = new Game();
            game.Play();
        }
    }
} 