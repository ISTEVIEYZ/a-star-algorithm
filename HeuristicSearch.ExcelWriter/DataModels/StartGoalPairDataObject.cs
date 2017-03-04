using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch.ExcelWriter.DataModels
{
    public class StartGoalPairDataObject
    {
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int GoalX { get; private set; }
        public int GoalY { get; private set; }

        public List<AlgorithmResultsDataObject> AlgorithmResultsData { get; set; }

        public StartGoalPairDataObject(int startX, int startY, int goalX, int goalY)
        {
            this.StartX = startX;
            this.StartY = startY;
            this.GoalX = goalX;
            this.GoalY = goalY;

            this.AlgorithmResultsData = new List<AlgorithmResultsDataObject>();
        }
    }
}
