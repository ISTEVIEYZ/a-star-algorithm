using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using HeuristicSearch.Collections;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.DataStructures;

namespace HeuristicSearch.Algorithms.Abstract
{
    [Serializable]
    public abstract class PathAlgorithmMultipleHeuristicBase : IPathAlgorithm
    {
        protected const int NUM_GRID_ROW = 120;
        protected const int NUM_GRID_COL = 160;

        protected int n;

        protected int nodesExpanded;

        protected decimal w1;
        protected decimal w2;

        public Abstract.Delegates.HDelegate[] Heuristics;

        protected List<MinimumHeap<Cell>> fringe;
        protected List<bool[,]> isInFringe;

        protected Cell[,] cellGrid;
        protected Cell start;
        protected Cell goal;

        protected DateTime startTime;

        public PathAlgorithmMultipleHeuristicBase(Cell[,] cellGrid, Cell start, Cell goal, int n, decimal w1, decimal w2)
        {
            this.n = n;
            this.w1 = w1;
            this.w2 = w2;
            this.cellGrid = cellGrid;
            this.start = start;
            this.goal = goal;
            this.nodesExpanded = 0;

            fringe = new List<MinimumHeap<Cell>>();
            isInFringe = new List<bool[,]>();


            for (int i = 0; i < n; i++)
            {
                fringe.Add(new MinimumHeap<Cell>(NUM_GRID_ROW * NUM_GRID_COL));
                fringe[i].TieBreakFunction = Algorithms.Formulas.AlgorithmFormulas.TieBreak;

                isInFringe.Add(new bool[NUM_GRID_ROW, NUM_GRID_COL]);
            }
        }

        protected Vector2 vectorOf(Cell s)
        {
            return new Vector2(s.X, s.Y);
        }

        protected Cell cellAt(Vector2 cellPos)
        {
            return cellGrid[(int)cellPos.X, (int)cellPos.Y];
        }

        protected abstract decimal Key(Vector2 cellPos, int n);

        public abstract AlgorithmResults RunAlgorithm();

        protected AlgorithmResults getResults(CellData[,] finalCellData)
        {
            AlgorithmResults ar = new AlgorithmResults();


            Vector2 s = vectorOf(goal);

            while (!s.Equals(vectorOf(start)))
            {
                ar.Path.Add(cellAt(s));
                s = finalCellData[(int)s.X, (int)s.Y].Parent.Value;
            }


            ar.Path.Add(start);

            for (int i = 0; i < NUM_GRID_ROW; i++)
                for (int j = 0; j < NUM_GRID_COL; j++)
                {
                    cellGrid[i, j].H = finalCellData[i, j].H;
                    cellGrid[i, j].G = finalCellData[i, j].G;
                    cellGrid[i, j].F = finalCellData[i, j].H + finalCellData[i, j].G;
                }

            cellGrid[start.X, start.Y].G = 0;
            cellGrid[start.X, start.Y].F = cellGrid[start.X, start.Y].H;

            ar.PathLength = goal.G;
            
            ar.NodesExpanded = nodesExpanded;
            ar.ElapsedTime = DateTime.Now.Subtract(startTime);

            //using (System.IO.Stream stream = new System.IO.MemoryStream())
            //{
            //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    formatter.Serialize(stream, this);
            //    ar.MemUsed = stream.Length;
            //}

            ar.Success = true;
            
            return ar;
        }
    } 
}
