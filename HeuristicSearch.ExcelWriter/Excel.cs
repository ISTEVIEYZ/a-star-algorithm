using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using HeuristicSearch.ExcelWriter.DataModels;

namespace HeuristicSearch.ExcelWriter
{
    public static class Excel
    {
        private const string SAVE_FILE_PATH = @"C:\Users\Alixxa\Desktop\benchmarks.xlsx";

        private static readonly string[] ALGO_RESULTS_TITLES = new string[] { "Average Time (ms)", "Average Nodes Expanded", "Average Path Length", "Memory Requirements (kB)" };

        public static void Export(DataObject data)
        {
            Console.WriteLine("Beginning Export");

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            xlApp.DisplayAlerts = false;

            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
                return;
            }

            int row = 1, col = 1;
            int mapNo = 1;

            bool includeTitles = false;

            foreach (MapDataObject mapData in data.MapData)
            {
                Console.WriteLine("Exporting Map {0}...", mapNo);

                col = 1;

                ws.Cells[row++, col] = string.Format("Map {0}", mapNo++);

                ws.Cells[row, col++] = "Start";
                ws.Cells[row, col++] = "Goal";

                row += 2;

                foreach (StartGoalPairDataObject sgpData in mapData.StartGoalPairs)
                {
                    col = 1;
                    ws.Cells[row, col++] = string.Format("({0}, {1})", sgpData.StartX, sgpData.StartY);
                    ws.Cells[row, col++] = string.Format("({0}, {1})", sgpData.GoalX, sgpData.GoalY);

                    includeTitles = mapData.StartGoalPairs.IndexOf(sgpData) == 0;
                    col = 3;
                    foreach (AlgorithmResultsDataObject algoData in sgpData.AlgorithmResultsData)
                    {
                        if (includeTitles)
                        {
                            ws.Cells[row - 2, col] = algoData.AlgorithmTitle;

                            Range rng = ws.Range[ws.Cells[row - 2, col], ws.Cells[row - 2, col + 3]];
                            rng.Merge();
                            rng.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.AliceBlue);

                            int t = col;
                            foreach (string title in ALGO_RESULTS_TITLES)
                            {
                                ws.Cells[row - 1, t++] = title;
                            }
                        }

                        ws.Cells[row, col++] = algoData.AlgorithmResults.ElapsedTime.Value.TotalMilliseconds;
                        ws.Cells[row, col++] = algoData.AlgorithmResults.NodesExpanded;
                        ws.Cells[row, col++] = algoData.AlgorithmResults.PathLength;
                        ws.Cells[row, col++] = algoData.AlgorithmResults.MemUsed;
                    }
                    includeTitles = false;
                    row++;

                }
                row += 2;
            }

            // ws.UsedRange.AutoFit();


            wb.SaveAs(SAVE_FILE_PATH, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);

            

            wb.Close();
            xlApp.Quit();

            Console.Write("Export Complete");
        }
    }
}
