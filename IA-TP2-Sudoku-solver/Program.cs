﻿using System;

namespace IA_TP2_Sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = presentation();
            int _mode = mode();
            SudokuGenerator sg = new SudokuGenerator();
            int[,] sudoku = new int[size,size];

            if (_mode == 1)
            {
                sudoku = sg.generateSudoku(size);
            }
            else
            {
                sudoku = sg.generateSudoku(size);
            }

            Solver solver = new Solver(sudoku);

        }




        private static int presentation()
        {
            // Starting Menu //
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("-------------------- Welcome to the Sudoku Solver --------------------");
            Console.WriteLine("--------------------------------------------------------------------\n\n");
            Console.WriteLine("Write the size of the sudoku you want (write an integer > 2) : ");

            // Take response of the user
            String size = "";
            size = Console.ReadLine();

            int number;
            bool b = true;

            // Check if the user entered an integer superior than 2
            while (b)
            {
                // If the user write an integer
                if (Int32.TryParse(size, out number))
                {
                    // If this integer is > 2
                    if (number >= 2)
                    {
                        b = false;       // Just to be sure
                        Console.Clear();
                        return (number);
                    }
                    // If the integer is not > 2, then do a new request
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nYour integer must be superior than 2! \n");
                        Console.WriteLine("Write the size of the sudoku you want (write an integer > 2) : ");
                        size = Console.ReadLine();
                    }
                }
                // If the user did not write down an integer, then do a new request
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nYou must enter an integer ! \n");
                    Console.WriteLine("Write the size of the sudoku you want (write an integer > 2) : ");
                    size = Console.ReadLine();
                }
            }

            // Just a return to grant int function
            return -1;
        }

        // Allows the user to choose the sudoku solver mode
        private static int mode()
        {
            Console.WriteLine("Choose your mode (1 : random sudoku, 2 : import your own sudoku) : ");

            // Take response of the user
            String mode = "";
            mode = Console.ReadLine();

            int number;
            bool b = true;

            // Check if the user entered an integer superior than 2
            while (b)
            {
                // If the user write an integer
                if (Int32.TryParse(mode, out number))
                {
                    // If this integer is > 2
                    if (number == 2 | number == 1)
                    {
                        b = false;       // Just to be sure
                        Console.Clear();
                        return (number);
                    }
                    // If the integer is not > 2, then do a new request
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\nYou must write down '1' or '2' \n");
                        Console.WriteLine("Choose your mode (1 : random sudoku, 2 : import your own sudoku) : ");
                        mode = Console.ReadLine();
                    }
                }
                // If the user did not write down an integer, then do a new request
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nYou must write down '1' or '2' \n");
                    Console.WriteLine("Write the size of the sudoku you want (write an integer > 2) : ");
                    mode = Console.ReadLine();
                }
            }
            // Just a return to grant int function
            return -1;
        }


    }
}
