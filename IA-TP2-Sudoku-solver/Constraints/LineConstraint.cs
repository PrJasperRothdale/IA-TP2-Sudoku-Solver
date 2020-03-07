using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver.Constraints
{
    class LineConstraint : Constraint
    {
        int[] target;
        int row;

        public LineConstraint(int[] targetcoo, int row)
        {
            this.target = targetcoo;
            this.row = row;
        }

        public bool check(int[,] state)
        {
            for(int i = 0; i < state.GetLength(0); i++)
            {
                if ( (state[row, i] == state[target[0],target[1]]) && ( row != target[0] || i != target[1] ) )
                {
                    return false;
                }
            }

            return true;
        }

    }
}
