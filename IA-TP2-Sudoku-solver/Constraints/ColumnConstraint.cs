using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver.Constraints
{
    class ColumnConstraint : Constraint
    {
        int[] target;
        int column;


        public ColumnConstraint(int[] targetcoo, int column)
        {
            this.target = targetcoo;
            this.column = column;
        }

        public bool check(int[,] state)
        {
            for (int i = 0; i < state.GetLength(0); i++)
            {
                if ( (state[i, column] == state[target[0], target[1]]) && ( i != target[0] || column != target[1] ) )
                {
                    return false;
                }
            }

            return true;
        }

    }
}
