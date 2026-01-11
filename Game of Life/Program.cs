using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            Generation gen = new Generation(25,60,0.2);
            // Console.WindowHeight = Console.LargestWindowHeight;
            // Console.WindowWidth = Console.LargestWindowWidth;

            gen = new Generation("000000\n001100\n001100\n000000");
            Console.ReadKey();

            for (int i = 0; i < 1000; i ++) {
                gen.Next();
                Thread.Sleep(50);
                Console.SetCursorPosition(0,0);
            }

            Game game = new Game();
            game.Play(10);

        }
    }
}
