using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HeuristicSearch.GridContent;

namespace HeuristicSearch.Algorithms.Formulas
{
    public static class AlgorithmFormulas
    {
        private static readonly decimal DIAG_REG_REG = (decimal)Math.Sqrt(2.0f);
        private static readonly decimal DIAG_HARD_HARD = (decimal)Math.Sqrt(8.0f);
        private static readonly decimal DIAG_REG_HARD = (decimal)((Math.Sqrt(2.0f) + Math.Sqrt(8.0f)) / 2.0f);

        public static decimal C(Cell s, Cell sprime)
        {
            decimal cost = 0;

            Cell.TraversalTypes v1Type = s.TraversalType;
            Cell.TraversalTypes v2Type = sprime.TraversalType;

            if (isDiagonal(vectorOf(s), vectorOf(sprime)))
            {
                if (v1Type == Cell.TraversalTypes.REGULAR && v2Type == Cell.TraversalTypes.REGULAR)
                    cost = DIAG_REG_REG;
                else if (v1Type == Cell.TraversalTypes.HARD && v2Type == Cell.TraversalTypes.HARD)
                    cost = DIAG_HARD_HARD;
                else if (v1Type == Cell.TraversalTypes.REGULAR && v2Type == Cell.TraversalTypes.HARD
                    || v1Type == Cell.TraversalTypes.HARD && v2Type == Cell.TraversalTypes.REGULAR)
                    cost = DIAG_REG_HARD;
            }
            else
            {
                if (v1Type == Cell.TraversalTypes.REGULAR && v2Type == Cell.TraversalTypes.REGULAR)
                    cost = (decimal)(s.IsHighway && sprime.IsHighway ? 0.25 : 1.0);
                else if (v1Type == Cell.TraversalTypes.HARD && v2Type == Cell.TraversalTypes.HARD)
                    cost = (decimal)(s.IsHighway && sprime.IsHighway ? 0.5 : 2.0f);
                else if (v1Type == Cell.TraversalTypes.REGULAR && v2Type == Cell.TraversalTypes.HARD
                    || v1Type == Cell.TraversalTypes.HARD && v2Type == Cell.TraversalTypes.REGULAR)
                    cost = (decimal)(s.IsHighway && sprime.IsHighway ? 0.375 : 1.5f);
            }


            return cost;
        }

        public static Cell[] Successors(Cell s, Cell[,] cellGrid)
        {
            Cell[] succ = new Cell[8];
            int index = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (s.X + i > 119 || s.X + i < 0 || s.Y + j > 159 || s.Y + j < 0)
                        continue;
                    
                    if (cellGrid[s.X + i, s.Y + j].TraversalType != Cell.TraversalTypes.BLOCKED)
                        succ[index++] = cellGrid[s.X + i, s.Y + j];
                }
            }

            return succ;
        }

        public static double TieBreak(Cell parent, Cell child)
        {
            if (parent.G == decimal.MaxValue)
                return -1;

            else
                return (double)child.G - (double)parent.G;
        }

        private static bool isDiagonal(Vector2 v1, Vector2 v2)
        {
            return v1.X != v2.X && v1.Y != v2.Y;
        }

        private static Vector2 vectorOf(Cell c)
        {
            return new Vector2((int)c.X, (int)c.Y);
        }
    }
}
