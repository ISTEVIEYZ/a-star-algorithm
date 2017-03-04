using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch.ExcelWriter.DataModels
{
    public class MapDataObject
    {
        public List<StartGoalPairDataObject> StartGoalPairs { get; set; }

        public MapDataObject()
        {
            this.StartGoalPairs = new List<StartGoalPairDataObject>();
        }
    }
}
