using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_of_Life {
    internal class Game {

        // Generation gen = new Generation("100000\n001100\n001100\n000000");
        private Generation gen;

        public Game(){
        
        }

        public void Create(int width, int heigth, int cells){
            this.gen = new Generation(width, heigth, cells);
        }

        public void Create(int width, int heigth, double popDensity){
            this.gen = new Generation(width, heigth, popDensity);
        }

        public void Create(String input){
            this.gen = new Generation(input);
        }

        public void Play(int generations = int.MaxValue) {
            if(gen is null) throw new Exception("Generation not initialized. Please create a generation before playing the game.");
            Console.WriteLine($"Generation 0:\n{gen}");
            Console.ReadKey();

            // hold the previous generation for comparison
            Generation previousGeneration;

            for (int i = 0; i < generations; i++)
            {
                // Console settings for smooth animation
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                // save the current generation
                previousGeneration = new Generation(gen.ToString());

                // evolve and display the next generation
                Console.WriteLine($"Generation {gen.generation}:\n{gen}");
                gen.Next();

                // check for stable state
                if (previousGeneration == gen)
                {
                    Console.WriteLine("\nStable state reached at generation " + i);
                    break;
                }

                Thread.Sleep(50);
            }
        }
    }
}