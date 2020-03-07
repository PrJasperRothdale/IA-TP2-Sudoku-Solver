﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver.SudokuGeneration
{
    class Grid
    {
        // Attributes
        public int[,] state { get; set; }
        public int[,] initialstate { get; set; }
        public GridDomain griddomain { get; set; }
        private Random random = new Random();

        // Constructor
        public Grid(int size)
        {
            state = new int[size, size];
            initialstate = new int[size, size];
            griddomain = new GridDomain(size);
        }

        // --------------------------------------------------- METHODS --------------------------------------------------- //

        // Generate randomly a sudoku
        // We assume a sudoku n*n only need n clues in each block 
        // To generate a sudoku we are going to add randomly n clues in each block
        // If there are too many fails, we assume that we have generate an impossible sudoku and the method failed
        // Return 0 if we failed or 1 if we achieved
        public int generateSudoku(int blockSize)
        {
            // We range each block
            for (int i = 1; i < blockSize * blockSize + 1; i++)
            {
                Tuple<int, int>[] block = getBlockXY(i, blockSize); // Get coordinates of all cells of i-th block
                int compt = 0;
                int cell = 0;
                int value = -1;

                // Count the number of fails, if too big : stop the program and reload the function
                int failed = 0;
                // Maximum errors allowed
                int err = 10;

                // While we don't have n clues in this block
                while (compt < blockSize)
                {
                    // Check if the random cell is already filled with a number
                    while (state[block[cell].Item1, block[cell].Item2] != 0)
                    {
                        cell = random.Next(blockSize * blockSize);  // Random cell
                    }
                    value = random.Next(1, blockSize * blockSize + 1); // Random value

                    bool b = check(block[cell].Item1, block[cell].Item2, value, blockSize); // Check if the value can be add in cell

                    // If we can add it
                    if (b == true)
                    {
                        // Update the value
                        int u = update(block[cell].Item1, block[cell].Item2, value, blockSize);

                        // If the update doesn't lead to an impossible sudoku we apply the change and increment compt
                        if (u == 1)
                        {
                            compt++;
                        }
                        // If we have failed to find a new cell to fill
                        else
                        {
                            // If too many fails
                            if (failed > err)
                            {
                                clearGridState();             // Don't forget to clear grid
                                griddomain.clearGridDomain(); // Clear grid domain
                                return 0;                     // Stop the function
                            }
                            else
                            {
                                failed++; // Add +1 to failed
                            }

                        }

                    }
                    // If we have failed to find a new cell to fill
                    else
                    {
                        // If too many fails
                        if (failed > err)
                        {
                            clearGridState();             // Don't forget to clear grid
                            griddomain.clearGridDomain(); // Clear grid domain
                            return 0;                     // Stop the function
                        }
                        else
                        {
                            failed++; // Add +1 to failed
                        }
                    }

                }
            }

            // We achieve to generate a correct sudoku
            return 1;
        }

        // Return the block number given a coordinate
        public int getBlocNumber(int x, int y, int BlockSize)
        {
            // Check if x and y have an acceptable value
            if (x < (BlockSize * BlockSize) & y < (BlockSize * BlockSize))
            {
                int BlocNumber = 1 + (x / BlockSize) * BlockSize + (y / BlockSize);
                return BlocNumber; // Return the block number
            }
            return 0;
        }

        // Return a list of tuple which correspond to a given block
        public Tuple<int, int>[] getBlockXY(int number, int BlockSize)
        {
            Tuple<int, int>[] block = new Tuple<int, int>[BlockSize * BlockSize]; // Init array of tuple of coordinate cell in same block
            int n = 0;
            int compt = 0;

            // Columns then rows
            for (int i = 0; i < BlockSize * BlockSize; i++)
            {
                for (int j = 0; j < BlockSize * BlockSize; j++)
                {
                    n = getBlocNumber(i, j, BlockSize);
                    if (n == number)
                    {
                        block[compt] = Tuple.Create(i, j);
                        compt++;
                    }
                }
            }
            return block;
        }

        // Check if a value v can be add in x,y
        public bool check(int x, int y, int v, int blockSize)
        {
            // Get the block number of the cell x,y
            int blockNumber = getBlocNumber(x, y, blockSize);
            // Get all the coordinate of cells with the same block number than the cell x,y
            Tuple<int, int>[] block = getBlockXY(blockNumber, blockSize);

            // If the value is in the domain of the cell x,y
            if (griddomain.domain[x, y, v - 1] == 0)
            {
                return false;
            }

            // If not, we return true, the value can be add
            return true;
        }

        // Update the domain matrix if the check method is true, return 0 if update an impossible sudoku
        public int update(int x, int y, int v, int blockSize)
        {
            // Get the block number of the cell x,y
            int blockNumber = getBlocNumber(x, y, blockSize);

            // Get all the coordinate of cells with the same block number than the cell x,y
            Tuple<int, int>[] block = getBlockXY(blockNumber, blockSize);

            // Minimum number of possibilities allowed in a sudoku
            int poss = 1;

            // We update the copydomain of cells on the same rows, columns and block
            for (int i = 0; i < blockSize * blockSize; i++)
            {

                // UPDATE ROW
                if (griddomain.copydomain[x, i, v - 1] != 2) // If the cell is not already filled
                {
                    griddomain.copydomain[x, i, v - 1] = 0;  // Update domain

                    // If this new update lead to less than 2 possibilites for the cell x,i return 0 and don't update (row)
                    if (griddomain.getPossibilites(x, i) < poss)
                    {
                        griddomain.syncDomaintoCopy();       //Reset the copydomain with domain
                        return 0;                            //Stop and return 0
                    }
                }

                // UPDATE COLUMN
                if (griddomain.copydomain[i, y, v - 1] != 2) // If the cell is not already filled
                {
                    griddomain.copydomain[i, y, v - 1] = 0;  // Update domain

                    // If this new update lead to less than 2 possibilites for the cell i,y return 0 and don't update (column)
                    if (griddomain.getPossibilites(i, y) < poss)
                    {
                        griddomain.syncDomaintoCopy();       // Reset the copydomain with domain
                        return 0;                            // Stop and return 0
                    }
                }

                // UPDATE BLOCKS
                if (griddomain.copydomain[block[i].Item1, block[i].Item2, v - 1] != 2) // If the cell is not already filled
                {
                    griddomain.copydomain[block[i].Item1, block[i].Item2, v - 1] = 0;  // Update domain

                    // If this new update lead to less than 2 possibilites for the cell block[i].Item1, block[i].Item2 return 0 and don't update (block)
                    if (griddomain.getPossibilites(block[i].Item1, block[i].Item2) < poss)
                    {
                        griddomain.syncDomaintoCopy();                                 // Reset the copydomain with domain
                        return 0;                                                      // Stop and return 0
                    }
                }
            }

            // Updates are finished and don't lead to an impossible sudoku, we can update to domain

            // We fill with 2, which means this cell has been filled
            for (int i = 0; i < blockSize * blockSize; i++)
            {
                griddomain.copydomain[x, y, i] = 2;
            }
            griddomain.syncCopytoDomain(); // Sync the changes

            // Update value of cell x,y
            state[x, y] = v;

            //Debug
            /*
            Console.WriteLine("Found " + v.ToString() + " in cell : " + x.ToString() + ", " + y.ToString());
            printGridState();

            int size = 2;
            String line = "";
            for (int i = 0; i < size * size; i++)
            {
                for (int j = 0; j < size * size; j++)
                {
                    line = " " + (i * size * size + j + 1).ToString() + " : ";
                    for (int k = 0; k < size * size; k++)
                    {
                        line += " " + griddomain.domain[i, j, k].ToString() + " ";
                    }
                    Console.WriteLine(line);
                }
            }
            */

            return 1;
        }

        // Allow to the user to import his own sudoku in the solver
        public void import(int size)
        {
            String res = "";
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {

                    printGridState(size);
                    Console.WriteLine("What is the value of the cell " + i.ToString() + ", " + j.ToString() + " ? ( write 0 for empty cells)");
                    res = Console.ReadLine();
                    int number;

                    bool b = true;

                    // Check if the user entered an integer superior than 2
                    while (b)
                    {
                        // If the user write an integer
                        if (Int32.TryParse(res, out number))
                        {
                            // If this integer is <0 or >size+1
                            if (number >= 0 & number <= state.GetLength(0))
                            {
                                b = false;
                                Console.Clear();
                                state[i, j] = number;
                            }
                            // If the integer is <0 or >size+1, then do a new request
                            else
                            {
                                Console.Clear();
                                printGridState(size);
                                Console.WriteLine("\n Incorrect value! \n");
                                Console.WriteLine("You must write down an integer between 0 and " + state.GetLength(0).ToString());
                                Console.WriteLine("What is the value of the cell " + i.ToString() + ", " + j.ToString() + " ? ( write 0 for empty cells)");
                                res = Console.ReadLine();
                            }
                        }
                        // If the user did not write down an integer, then do a new request
                        else
                        {
                            Console.Clear();
                            printGridState(size);
                            Console.WriteLine("\nYou must enter an integer ! \n");
                            Console.WriteLine("What is the value of the cell " + i.ToString() + ", " + j.ToString() + " ? ( write 0 for empty cells)");
                            res = Console.ReadLine();
                        }
                    }

                }
            }
        }

        // Clear grid state and initstate
        private void clearGridState()
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    state[i, j] = 0;
                    initialstate[i, j] = 0;
                }
            }
        }

        // Print the grid of the sudoku
        private void printGridState(int size)
        {

            // Get grid matrix state
            string line;

            for (int i = 0; i < state.GetLength(0); i++)
            {
                if (i % size == 0)
                {
                    Console.WriteLine("*---------------------------------*");
                }
                line = "";
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (j % size == 0)
                    {
                        line += " | " + state[i, j].ToString() + ' ';
                    }
                    else
                    {
                        line += ' ' + state[i, j].ToString() + ' ';
                    }

                }

                line += '|';

                Console.WriteLine(line);
            }

            Console.WriteLine("*---------------------------------*");
        }

    }
}
