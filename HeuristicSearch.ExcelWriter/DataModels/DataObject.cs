using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch.ExcelWriter.DataModels
{
    public class DataObject
    {
        public List<MapDataObject> MapData { get; set; }

        public DataObject()
        {
            this.MapData = new List<MapDataObject>();
        }
    }
}
