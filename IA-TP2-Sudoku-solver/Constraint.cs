using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver
{
    interface Constraint
    {
        public bool check(int[,] state);
    }
}
