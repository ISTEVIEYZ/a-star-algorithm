using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HeuristicSearch.GridContent;
using HeuristicSearch.Collections;
using HeuristicSearch.Algorithms.Formulas;
using HeuristicSearch.Algorithms.DataStructures;

namespace HeuristicSearch.Algorithms.Abstract
{
    [Serializable]
    public abstract class PathAlgorithmSingleHeuristicBase : IPathAlgorithm
    {
        protected const int NUM_CELLS = 19200;

        protected int nodesExpanded;

        protected DateTime startTime;
        public TimeSpan? elapsedTime;

        protected MinimumHeap<Cell> fringe;
        protected bool[,] closedArray;
        protected bool[,] isInFringe;

        protected Cell[,] cellGrid;
        protected Cell start;
        protected Cell goal;

        public PathAlgorithmSingleHeuristicBase(Cell[,] cellGrid, Cell start, Cell goal)
        {
            fringe = new MinimumHeap<Cell>(NUM_CELLS);
            fringe.TieBreakFunction = AlgorithmFormulas.TieBreak;
            closedArray = new bool[120, 160];
            isInFringe = new bool[120, 160];

            this.nodesExpanded = 0;
            this.cellGrid = cellGrid;
            this.start = start;
            this.goal = goal;

            this.elapsedTime = null;
        }

        public abstract AlgorithmResults RunAlgorithm();

        protected AlgorithmResults getResults()
        {
            AlgorithmResults ar = new AlgorithmResults();
            ar.Success = true;
            ar.ElapsedTime = elapsedTime;

            Cell s = goal;

            bool addedParent = false;

            do
            {
                ar.Path.Add(s);
                if (s.Equals(start))
                    addedParent = true;

                s = s.Parent;

            } while (!addedParent);



            ar.NodesExpanded = nodesExpanded;
            ar.PathLength = goal.G;


            //using (System.IO.Stream stream = new System.IO.MemoryStream())
            //{
            //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    formatter.Serialize(stream, this);
            //    ar.MemUsed = stream.Length;
            //}

            return ar;
        }


    }
}
