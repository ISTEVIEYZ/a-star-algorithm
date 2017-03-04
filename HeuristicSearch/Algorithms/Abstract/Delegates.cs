using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.GridContent;

namespace HeuristicSearch.Algorithms.Abstract
{
    public static class Delegates
    {
        public delegate decimal HDelegate(Cell s, Cell start, Cell goal);
    }
}
