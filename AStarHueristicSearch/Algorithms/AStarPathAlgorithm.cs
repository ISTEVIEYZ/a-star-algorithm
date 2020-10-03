using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.Abstract;
using HeuristicSearch.Algorithms.Formulas;
using HeuristicSearch.Algorithms.DataStructures;

namespace HeuristicSearch.Algorithms
{
    [Serializable]
    public class AStarPathAlgorithm : PathAlgorithmSingleHeuristicBase
    {
        public Delegates.HDelegate H { get; set; }

        public AStarPathAlgorithm(Cell[,] cellGrid, Cell start, Cell goal) : base(cellGrid, start, goal)
        {

        }

        public override AlgorithmResults RunAlgorithm()
        {
            startTime = DateTime.Now;

            start.H = H(start, start, goal);
            start.G = 0;
            start.F = start.G + start.H;


            start.Parent = start; 
            fringe.Push(start, start.G + H(start, start, goal));
            
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
                decimal h = H(sprime, start, goal);

                sprime.G = totalCost;
                sprime.H = h;
                sprime.F = sprime.G + h;

                sprime.Parent = s;

                if (isInFringe[sprime.X, sprime.Y])
                    fringe.Remove(sprime);

                fringe.Push(sprime, sprime.G + h);
				isInFringe[sprime.X, sprime.Y] = true;
            }
        }
    }
}
