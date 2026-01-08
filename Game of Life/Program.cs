using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game_of_Life {
    internal class Program {
        static void Main(string[] args) {

            Generation gen = new Generation(25,25,0.5);
            
            for (int i = 0; i < 1000; i ++) {
                gen.Next();
                Thread.Sleep(100);
                Console.Clear();

            }

            Game game = new Game();
            game.Play(10);

        }
    }
}
