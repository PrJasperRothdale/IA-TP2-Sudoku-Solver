using System;
using System.Collections.Generic;
using System.Text;

namespace IA_TP2_Sudoku_solver
{
    class Sudoku
    {
        int[,] state;
        int[,][] domain;
        List<Constraint> constraints;

        public Sudoku(int[,] initial_state)
        {
            state = (int[,]) initial_state.Clone();
            domain = new int[state.GetLength(0), state.GetLength(1)][];
            constraints = generateSudokuConstraints();
        }

        private List<Constraint> generateSudokuConstraints()
        {
            List<Constraint> constraints = new List<Constraint>();
            constraints.AddRange(generateLineConstraints());
            constraints.AddRange(generateColumnConstraints());
            constraints.AddRange(generateBlockConstraints());

            return constraints;
        }

        public int[,][] getDomain()
        {
            domain = getDomain(state);
            return domain;
        }

        public int[,][] getDomain(int[,] sudoku_state)
        {
            int[,][] ndom = new int[sudoku_state.GetLength(0), sudoku_state.GetLength(1)][];
            for (int i = 0; i < sudoku_state.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku_state.GetLength(1); j++)
                {
                    int[,] scopy = (int[,])sudoku_state.Clone();
                    List<int> domrow = new List<int>();

                    for (int k = 0; k < sudoku_state.GetLength(0); k++)
                    {
                        scopy[i, j] = k;

                        if (isConsistent(scopy))
                        {
                            domrow.Add(k);
                        }
                    }

                    ndom[i, j] = domrow.ToArray();
                }
            }

            return ndom;

        }

        public bool isConsistent(int[,] sudoku_state)
        {
            foreach (Constraint constraint in constraints)
            {
                if ( !constraint.check(state) )
                {
                    return false;
                }
            }

            return true;
        }

        private List<Constraints.LineConstraint> generateLineConstraints()
        {
            List<Constraints.LineConstraint> lineconstraints = new List<Constraints.LineConstraint>();
            for (int i=0; i < state.GetLength(0); i++)
            {
                for (int j=0; j < state.GetLength(1); j++)
                {
                    lineconstraints.Add(new Constraints.LineConstraint( new[] { i, j }, i ));
                }
            }
            return lineconstraints;
        }

        private List<Constraints.BlockConstraint> generateBlockConstraints()
        {
            List<Constraints.BlockConstraint> blockconstraints = new List<Constraints.BlockConstraint>();

            int block_size = (int) Math.Sqrt(state.GetLength(0));

            for (int i=0; i < state.GetLength(0); i++)
            {
                int blockx = (i / block_size) * block_size;
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    int blocky = (j / block_size) * block_size;
                    blockconstraints.Add(new Constraints.BlockConstraint(new[] { i, j }, new[] { blockx, blocky }, block_size));
                }
            }

            return blockconstraints;
        }

        private List<Constraints.ColumnConstraint> generateColumnConstraints()
        {
            List<Constraints.ColumnConstraint> columnconstraints = new List<Constraints.ColumnConstraint>();
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    columnconstraints.Add(new Constraints.ColumnConstraint(new[] { i, j }, j));
                }
            }
            return columnconstraints;
        }

        private int[,][] initDomain()
        {
            int[,][] domain = new int[state.GetLength(0), state.GetLength(1)][] ;//new int[state.GetLength(0), state.GetLength(1), state.GetLength(0)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    domain[i, j] = new int[state.GetLength(0)];
                    int[] values = new int[state.GetLength(0)];

                    for (int k = 0; k < state.GetLength(0); k++)
                    {
                        values[k] = k;
                    }

                    domain[i, j] = values;
                }
            }

            return domain;
        }

    }
}
