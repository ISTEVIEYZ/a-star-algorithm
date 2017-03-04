using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeuristicSearch
{
    public static class GlobalLogger
    {
        private const string LOG_PATH = @"log.txt";

        public static void Log(string message)
        {
            System.IO.File.AppendAllText(LOG_PATH, string.Format("{0}{1}", message, System.Environment.NewLine));
        }

        public static void ClearLog()
        {
            System.IO.File.WriteAllText(LOG_PATH, string.Empty);
        }
    }
}
