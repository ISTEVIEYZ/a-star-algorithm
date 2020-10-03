using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.GridContent;

namespace HeuristicSearch.Algorithms.Formulas
{
    public static class Heuristics
    {
        private static readonly decimal DIAG_MIN = (decimal)Math.Sqrt(2.0f);

        public static decimal DefaultHeuristic(Cell sprime, Cell start, Cell goal)
        {
            return (decimal)(Math.Sqrt(2.0f) * Math.Min(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y)) + Math.Max(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y)) - Math.Min(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y)));
        }

        public static decimal Manhattan(Cell sprime, Cell start, Cell goal)
        {
            decimal dx = Math.Abs((decimal)sprime.X - (decimal)goal.X);
            decimal dy = Math.Abs((decimal)sprime.Y - (decimal)goal.Y);

            return 0.25M * (dx + dy);
        }

        public static decimal Euclidean(Cell sprime, Cell start, Cell goal)
        {
            decimal dx = sprime.X - goal.X;
            decimal dy = sprime.Y - goal.Y;

            return 0.25M * (decimal)Math.Sqrt((double)(dx * dx) + (double)(dy * dy));
        }

        public static decimal DiagonalDistance(Cell sprime, Cell start, Cell goal)
        {
            decimal dmax = Math.Max(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y));
            decimal dmin = Math.Min(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y));

            decimal cn = 0.25M;
            decimal cd = DIAG_MIN;

            return cd * dmin + cn * (dmax - dmin);
        }

        public static decimal Chebyshev(Cell sprime, Cell start, Cell goal)
        {
            return Math.Max(Math.Abs(sprime.X - goal.X), Math.Abs(sprime.Y - goal.Y));
        }
    }
}
