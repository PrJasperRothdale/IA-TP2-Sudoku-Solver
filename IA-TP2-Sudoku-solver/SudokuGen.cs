using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver
{
    class SudokuGen
    {
        private static int BLOC_SIZE = 2; //size
        private static int GRID_SIZE = BLOC_SIZE * BLOC_SIZE; //squaresize
        private static Sudoku instance;
        private SudokuGeneration.Grid grid { get; set; }

        public SudokuGen(int size)
        {
            grid = new SudokuGeneration.Grid(size * size);
            BLOC_SIZE = size;
            GRID_SIZE = BLOC_SIZE * BLOC_SIZE;
        }

        public int[,] generateSudoku(int size)
        {
            int res = grid.generateSudoku(size); ;
            // If we failed, we try till we achieve
            while (res == 0)
            {
                // Generate randomly a sudoku with a given block size (>= 2)
                res = grid.generateSudoku(size);
            }

            return grid.state;
        }
    }
}
