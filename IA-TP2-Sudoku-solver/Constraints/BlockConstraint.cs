using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver.Constraints
{
    class BlockConstraint : Constraint
    {
        int[] target;
        int[] blockstart;
        int[] blockend;


        public BlockConstraint(int[] targetcoo, int[] blockstart, int length)
        {
            this.target = targetcoo;
            this.blockstart = blockstart;
            this.blockend = new[] { blockstart[0] + length, blockstart[1] + length };
        }

        public bool check(int[,] state)
        {
            for (int i = blockstart[0]; i <= blockend[0]; i++)
            {
                for (int j = blockstart[1]; j <= blockend[1]; j++)
                {
                    if ( (state[i,j] == state[target[0],target[1]]) && ( i != target[0] || j != target[1]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
