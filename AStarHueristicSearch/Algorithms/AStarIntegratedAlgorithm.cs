using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.DataStructures;
using Microsoft.Xna.Framework;

namespace HeuristicSearch.Algorithms
{
    [Serializable]
    public class AStarIntegratedAlgorithm : Abstract.PathAlgorithmMultipleHeuristicBase
    {
        private bool[,] closedListNonAnchor;
        private bool[,] closedListAnchor;
        
        private CellData[,] cellDataI;

        private bool[,] itTracker;

        public AStarIntegratedAlgorithm(Cell[,] cellGrid, Cell start, Cell goal, int n, decimal w1, decimal w2) : base(cellGrid, start, goal, n, w1, w2)
        {
            closedListNonAnchor = new bool[NUM_GRID_ROW, NUM_GRID_COL];
            closedListAnchor = new bool[NUM_GRID_ROW, NUM_GRID_COL];

            cellDataI = new CellData[NUM_GRID_ROW, NUM_GRID_COL];

            itTracker = new bool[NUM_GRID_ROW, NUM_GRID_COL];

            for (int i = 0; i < NUM_GRID_ROW; i++)
                for (int j = 0; j < NUM_GRID_COL; j++)
                    cellDataI[i, j] = new CellData();
        }

        public override DataStructures.AlgorithmResults RunAlgorithm()
        {
            startTime = DateTime.Now;

            cellDataI[start.X, start.Y].G = 0;
            cellDataI[goal.X, goal.Y].G = decimal.MaxValue;

            cellDataI[start.X, start.Y].Parent = null;
            cellDataI[goal.X, goal.Y].Parent = null;

            for (int i = 0; i < n; i++)
            {
                fringe[i].Push(start, Key(vectorOf(start), i));
                isInFringe[i][start.X, start.Y] = true;
            }

            while (fringe[0].MinValue < decimal.MaxValue)
            {
                for (int i = 0; i < n; i++)
                {
                    if (fringe[i].MinValue <= w2 * fringe[0].MinValue)
                    {
                        if (cellDataI[goal.X, goal.Y].G <= fringe[i].MinValue)
                        {
                            if (cellDataI[goal.X, goal.Y].G < decimal.MaxValue)
                            {
                                return getResults(cellDataI);
                            }
                        }
                        else
                        {
                            Cell s = fringe[i].Pop();
                            isInFringe[i][s.X, s.Y] = false;
                            
                            ExpandState(s);
                            closedListNonAnchor[s.X, s.Y] = true;
                            nodesExpanded++;  
                        }
                    }
                    else
                    {
                        if (cellDataI[goal.X, goal.Y].G <= fringe[0].MinValue)
                        {
                            if (cellDataI[goal.X, goal.Y].G < decimal.MaxValue)
                            {
                                return getResults(cellDataI);
                            }
                        }
                        else
                        {
                            Cell s = fringe[0].Pop();
                            isInFringe[0][s.X, s.Y] = false;
                            
                            ExpandState(s);
                            closedListAnchor[s.X, s.Y] = true;
                            nodesExpanded++; 
                        }
                    }
                }
            }
            return new AlgorithmResults { Success = false };
        }

        private void ExpandState(Cell s)
        {
            for (int i = 0; i < n; i++)
            {
                if (isInFringe[i][s.X, s.Y])
                {
                    fringe[i].Remove(s);
                    isInFringe[i][s.X, s.Y] = false; 
                }
            }

            Cell[] sprimes = Algorithms.Formulas.AlgorithmFormulas.Successors(s, cellGrid);
            for (int i = 0; i < sprimes.Length; i++)
            {
                if (sprimes[i] == null)
                    continue;

                Cell sprime = sprimes[i];
                decimal cost = cellDataI[s.X, s.Y].G + Algorithms.Formulas.AlgorithmFormulas.C(s, sprime);

                if (!itTracker[sprime.X, sprime.Y])
                {
                    cellDataI[sprime.X, sprime.Y].G = decimal.MaxValue;
                    cellDataI[sprime.X, sprime.Y].Parent = null;
                    itTracker[sprime.X, sprime.Y] = true;
                }
                if (cellDataI[sprime.X, sprime.Y].G > cost)
                {
                    cellDataI[sprime.X, sprime.Y].G = cost;
                    cellDataI[sprime.X, sprime.Y].Parent = vectorOf(s);

                    if (!closedListAnchor[sprime.X, sprime.Y])
                    {
                        if (isInFringe[0][sprime.X, sprime.Y])
                            fringe[0].Remove(sprime);

                        fringe[0].Push(sprime, Key(vectorOf(sprime), 0));
                        isInFringe[0][sprime.X, sprime.Y] = true;

                        if (!closedListNonAnchor[sprime.X, sprime.Y])
                        {
                            for (int k = 1; k < n; k++)
                            {
                                if (Key(vectorOf(sprime), k) <= w2 * Key(vectorOf(sprime), 0))
                                {
                                    if (isInFringe[k][sprime.X, sprime.Y])
                                        fringe[k].Remove(sprime);

                                    fringe[k].Push(sprime, Key(vectorOf(sprime), 0));
                                    isInFringe[k][sprime.X, sprime.Y] = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override decimal Key(Vector2 cellPos, int n)
        {
            Cell s = cellAt(cellPos);
            cellDataI[(int)cellPos.X, (int)cellPos.Y].H = Heuristics[n](s, start, goal);
            return cellDataI[(int)cellPos.X, (int)cellPos.Y].G + w1 * cellDataI[(int)cellPos.X, (int)cellPos.Y].H;
        }
    }
}
