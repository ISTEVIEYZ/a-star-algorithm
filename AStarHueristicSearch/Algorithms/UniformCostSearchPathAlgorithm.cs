using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.Formulas;
using HeuristicSearch.Algorithms.Abstract;
using HeuristicSearch.Algorithms.DataStructures;

namespace HeuristicSearch.Algorithms
{
    [Serializable]
    public class UniformCostSearchPathAlgorithm : PathAlgorithmSingleHeuristicBase
    {
        public UniformCostSearchPathAlgorithm(Cell[,] cellGrid, Cell start, Cell goal) : base(cellGrid, start, goal)
        {

        }

        public override AlgorithmResults RunAlgorithm()
        {
            startTime = DateTime.Now;

            start.F = 0;
            start.G = 0; 
            start.Parent = start;

            fringe.Push(start, start.G);

            while (!fringe.IsEmpty) 
            {
                Cell s = fringe.Pop();
                isInFringe[s.X, s.Y] = false;

                if (s.Equals(goal))
                {
                    elapsedTime = DateTime.Now.Subtract(startTime);
                    return getResults();
                }

                closedArray[s.X, s.Y] = true;
                nodesExpanded++;

                Cell[] sprimes = AlgorithmFormulas.Successors(s, cellGrid);
                for (int i = 0; i < 8; i++)
                {
                    if (sprimes[i] == null)
                        continue;

                    if (!closedArray[sprimes[i].X, sprimes[i].Y]) 
                    {
                        if (!isInFringe[sprimes[i].X, sprimes[i].Y])
                        {
                            sprimes[i].G = decimal.MaxValue;
                            sprimes[i].Parent = null; 
                        }

                        UpdateVertex(s, sprimes[i]);
                    }
                }
            }

            elapsedTime = DateTime.Now.Subtract(startTime);
            return new AlgorithmResults { Success = false };
        }

        private void UpdateVertex(Cell s, Cell sprime)
        {
            decimal totalCost = s.G + AlgorithmFormulas.C(s, sprime);
            if (totalCost < sprime.G)
            {
                sprime.G = totalCost;
                sprime.F = sprime.G;

                sprime.Parent = s;

                if (isInFringe[sprime.X, sprime.Y])
                    fringe.Remove(sprime);

                fringe.Push(sprime, sprime.G);
                isInFringe[sprime.X, sprime.Y] = true;
            }
        }
    }
}
