using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            Generation gen = new Generation(50,100,0.5);
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.LargestWindowWidth;

            for (int i = 0; i < 1000; i ++) {
                gen.Next();
                Thread.Sleep(100);
                Console.SetCursorPosition(0,0);
            }

            Game game = new Game();
            game.Play(10);

        }
    }
}
