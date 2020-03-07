using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver
{
    class Solver
    {
        private Sudoku sudoku;
        private int[,] initialstate;
        public Solver(int[,] sudoku_state)
        {
            sudoku = new Sudoku(sudoku_state);
            initialstate = (int[,])sudoku_state.Clone();
        }

        public void solve()
        {
            Console.WriteLine("Avant :");
            printState(initialstate);

            // SOLVE
            // SUDOKU
            // HERE
            // APPEL DE FONCTION / PAS DE CODE DIRECTEMENT ICI

            Console.WriteLine("Apres :");
            printState(initialstate);
        }

        private static void printState(int[,] state)
        {
            string horizontalborder = "*";
            for (int i = 0; i < state.GetLength(0); i++)
            {
                horizontalborder += " - ";
            }
            horizontalborder += "*";

            Console.WriteLine(horizontalborder);

            for (int i = 0; i < state.GetLength(0); i++)
            {
                string line = "|";
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    line += " " + state[i, j] + " ";
                }
                line += "|";
                Console.WriteLine(line);

            }

            Console.WriteLine(horizontalborder);
        }

    }
}
