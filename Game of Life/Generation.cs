using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life {
    internal class Generation {

        Cell[,] board;
        int generation = 1;

        // ============================== constructors ====================================================================================================

        // base constuctor
        private Generation(int x, int y) {
            // create board
            board = new Cell[x, y];

            // fill board
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    // create dead cells only
                    board[i, j] = new Cell();
                }
            }
        }

        // living cells constructor
        public Generation(int x, int y, int livingCells) : this(x, y) {
            FillBoard(livingCells);
        }

        // population density constructor
        public Generation(int x, int y, double populationDensity) : this(x,y) {
            FillBoard(populationDensity);
        }
        
        // string to game board constructor
        public Generation(String input) {
            // TODO string deconstruction
        }

        // ============================== fill board ====================================================================================================

        // fills the game board to have a the amount of passed living cells
        private void FillBoard(int livingCells) {
            // guard clause
            if (board.GetLength(0) * board.GetLength(1) < livingCells) throw new ArgumentException("Number of living cells exceeds board capacity.");
            
            Random rand = new Random();
            int generatedCells = 0;

            // generate living cells till limit reached
            while (generatedCells < livingCells) {
                // random coordinates for cell
                int x = rand.Next(0, board.GetLength(0));
                int y = rand.Next(0, board.GetLength(1));

                // if cell is already alive, skip
                if (board[x, y].IsAlive) continue;

                // set cell to alive and increment counter
                board[x, y] = new Cell(true);
                generatedCells++;
            }
        }

        // fills the game board to have a population density equals or slightly higher than passed
        private void FillBoard(double populationDensity) {
            // guard clause
            if (populationDensity < 0.0 || populationDensity > 1.0) throw new ArgumentException("Population density not in range (0 - 1).");

            Random rand = new Random();
            double currDensity = 0.0;
            int liveCells = 0;

            // generate living cells till limit reached
            while (currDensity <= populationDensity) {
                // random coordinates for cell
                int x = rand.Next(0, board.GetLength(0));
                int y = rand.Next(0, board.GetLength(1));

                // if cell is already alive, skip
                if (board[x, y].IsAlive) continue;

                // set cell to alive and calculate current density
                board[x, y] = new Cell(true);
                liveCells++;

                // current density calculation
                int totalCells = board.GetLength(0) * board.GetLength(1);
                currDensity = (double)liveCells / (double)totalCells;
            }
        }


        // ============================== game rules ====================================================================================================

        public void Next(int steps = 1) {

            for (int i = 0; i < steps; i++) {
                Cell[,] oldBoard = board;
                Cell[,] newBoard = new Cell[oldBoard.GetLength(0), oldBoard.GetLength(1)];

                // fill board
                for (int n = 0; n < newBoard.GetLength(0); n++) {
                    for (int m = 0; m < newBoard.GetLength(1); m++) {
                        // create dead cells only
                        newBoard[n, m] = new Cell();
                    }
                }

                Console.WriteLine($"Generation: {generation}");

                for (int x = 0; x < board.GetLength(0); x++) {
                    for (int y = 0; y < board.GetLength(1); y++) {
                        // overpopulation
                        if (sumNeighbours(x, y) < 2 || sumNeighbours(x, y) > 3 && oldBoard[x, y].IsAlive == true) newBoard[x, y].IsAlive = false;
                        // gets born
                        else if (sumNeighbours(x, y) == 3 && oldBoard[x, y].IsAlive == false) newBoard[x, y].IsAlive = true;
                        // stays alive
                        else if (sumNeighbours(x, y) == 2 || sumNeighbours(x, y) == 3 && oldBoard[x, y].IsAlive == true) newBoard[x,y].IsAlive = true;
                        // stays dead
                        else newBoard[x, y].IsAlive = false;
                    }
                }
                board = newBoard;
                generation++;
                Console.WriteLine(ToString());
            }
        }

        // count all neighbours of a cell, accounting for board borders
        private int sumNeighbours(int x, int y) {
            // set limits for x and y to not go out of bounds in array
            int minX = (x == 0) ? x : x - 1;
            int minY = (y == 0) ? y : y - 1;
            int limX = (x == board.GetLength(0) - 1) ? x : x + 1;
            int limY = (y == board.GetLength(1) - 1) ? y : y + 1;

            int neighbours = 0;

            // count up neighbours
            for (int i = minX; i <= limX; i++) {
                for (int j = minY; j <= limY; j++) {
                    if (i == x && j == y) continue;
                    if (board[i,j].IsAlive) neighbours++;
                }
            }

            return neighbours;
        }

        // ============================== overloads ====================================================================================================

        public static bool operator ==(Generation a, Generation b) {
            return a.ToString() == b.ToString();
        }

        public static bool operator !=(Generation a, Generation b) {
            return a.ToString() != b.ToString();
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        // to string method to represent the board
        public override string ToString() {
            string boardString = "";
            for (int i = 0; i < board.GetLength(0); i++) {
                for (int j = 0; j < board.GetLength(1); j++) {
                    boardString += $"{(board[i, j].ToString())} ";
                }
                boardString += "\n";
            }
            return boardString;
        }
    }
}
