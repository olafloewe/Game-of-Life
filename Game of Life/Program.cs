using System;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            // Set console size
            Console.WindowHeight = Console.LargestWindowHeight - 15;
            Console.WindowWidth = Console.LargestWindowWidth - 30;

            // TODO 
            // serialization
            // deserialization
            // dynacmic resizing with list
            // cycle length detection
            // re organize code (move to different classes)

            Game game = new Game();
            game.Create(50, 40, 0.3);
            game.Play();
            
        }
    }
}
