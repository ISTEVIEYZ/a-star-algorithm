using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch.ExcelWriter.DataModels
{
    public class AlgorithmResultsDataObject
    {
        public string AlgorithmTitle { get; private set; }
        public Algorithms.DataStructures.AlgorithmResults AlgorithmResults { get; private set; }

        public AlgorithmResultsDataObject(Algorithms.DataStructures.AlgorithmResults algorithmResults, string algorithmTitle)
        {
            this.AlgorithmResults = algorithmResults;
            this.AlgorithmTitle = algorithmTitle;
        }    
    }
}
