using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.Formulas;
using HeuristicSearch.Algorithms.DataStructures;
using HeuristicSearch.Algorithms.Abstract;
using Microsoft.Xna.Framework;

namespace HeuristicSearch.Algorithms
{
    [Serializable]
    public class AStarSequentialHeuristic : PathAlgorithmMultipleHeuristicBase
    {
        private List<CellData[,]> cellData;

        private List<bool[,]> closedList;
        private List<bool[,]> itTracker;

        public AStarSequentialHeuristic(Cell[,] cellGrid, Cell start, Cell goal, int n, decimal w1, decimal w2) : base(cellGrid, start, goal, n, w1, w2)
        {
            cellData = new List<CellData[,]>();
            closedList = new List<bool[,]>();
            itTracker = new List<bool[,]>();

            for (int i = 0; i < n; i++)
            {
                cellData.Add(new CellData[NUM_GRID_ROW, NUM_GRID_COL]);

                for (int j = 0; j < NUM_GRID_ROW; j++)
                    for (int k = 0; k < NUM_GRID_COL; k++)
                        cellData[i][j, k] = new CellData();

                closedList.Add(new bool[NUM_GRID_ROW, NUM_GRID_COL]);
                itTracker.Add(new bool[NUM_GRID_ROW, NUM_GRID_COL]);
            }
        }

        public override AlgorithmResults RunAlgorithm()
        {
            startTime = DateTime.Now;

            for (int i = 0; i < n; i++)
            {
                cellData[i][start.X, start.Y].G = 0;
                cellData[i][goal.X, goal.Y].G = decimal.MaxValue;

                cellData[i][start.X, start.Y].Parent = null;
                cellData[i][goal.X, goal.Y].Parent = null;

                fringe[i].Push(start, Key(vectorOf(start), i));
                isInFringe[i][start.X, start.Y] = true;
            }
            while (fringe[0].MinValue < decimal.MaxValue)
            {
                for (int i = 1; i < n; i++)
                {
                    if (fringe[i].MinValue <= w2 * fringe[0].MinValue)
                    {
                        if (cellData[i][goal.X, goal.Y].G <= fringe[i].MinValue)
                        {
                            if (cellData[i][goal.X, goal.Y].G < decimal.MaxValue)
                            {
                                return getResults(cellData[i]);
                            }
                        }
                        else
                        {
                            Cell s = fringe[i].Pop();
                            isInFringe[i][s.X, s.Y] = false;
                            ExpandState(s, i);
                            closedList[i][s.X, s.Y] = true;
                            nodesExpanded++;
                        }
                    }
                    else
                    {
                        if (cellData[0][goal.X, goal.Y].G <= fringe[0].MinValue)
                        {
                            if (cellData[0][goal.X, goal.Y].G < decimal.MaxValue)
                            {
                                return getResults(cellData[0]);
                            }
                        }
                        else
                        {
                            Cell s = fringe[0].Pop();
                            isInFringe[0][s.X, s.Y] = false;
                            ExpandState(s, 0);
                            closedList[0][s.X, s.Y] = true;
                            nodesExpanded++;
                        }
                    }
                }
            }

            return new AlgorithmResults { Success = false };
        }

        public void ExpandState(Cell s, int i)
        {
            Cell[] sprimes = AlgorithmFormulas.Successors(s, cellGrid);
            for (int k = 0; k < sprimes.Length; k++)
            {
                if (sprimes[k] == null)
                    continue;

                Cell sprime = sprimes[k];
                decimal cost = cellData[i][s.X, s.Y].G + AlgorithmFormulas.C(s, sprime);

                if (!itTracker[i][sprime.X, sprime.Y])
                {
                    cellData[i][sprime.X, sprime.Y].G = decimal.MaxValue;
                    cellData[i][sprime.X, sprime.Y].Parent = null;

                    itTracker[i][sprime.X, sprime.Y] = true;
                }
                if (cellData[i][sprime.X, sprime.Y].G > cost)
                {
                    cellData[i][sprime.X, sprime.Y].G = cost;
                    cellData[i][sprime.X, sprime.Y].Parent = vectorOf(s);

                    if (!closedList[i][sprime.X, sprime.Y])
                    {
                        if (isInFringe[i][sprime.X, sprime.Y])
                        {
                            fringe[i].Remove(sprime);
                        }
                        fringe[i].Push(sprime, Key(vectorOf(sprime), i));
                        isInFringe[i][sprime.X, sprime.Y] = true;
                    }
                }
            }
        }

        protected override decimal Key(Vector2 cellPos, int n)
        {
            Cell s = cellAt(cellPos);
            cellData[n][(int)cellPos.X, (int)cellPos.Y].H = Heuristics[n](s, start, goal);
            return cellData[n][(int)cellPos.X, (int)cellPos.Y].G + w1 * cellData[n][(int)cellPos.X, (int)cellPos.Y].H;
        }
    }
}
