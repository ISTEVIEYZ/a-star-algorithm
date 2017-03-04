using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeuristicSearch.Algorithms;
using HeuristicSearch.GridContent;
using HeuristicSearch.Algorithms.Abstract;
using HeuristicSearch.ExcelWriter.DataModels;

namespace HeuristicSearch.ExcelWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid();
            DataObject data = new DataObject();
            
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Starting Map {0}...", i + 1);

                grid.LoadFromFile(string.Format(@"C:\Users\Alixxa\Desktop\map{0}.txt", i + 1));
                MapDataObject mapData = new MapDataObject();

                for (int j = 0; j < 10; j++)
                {
                    Console.WriteLine("Starting Start/Goal Pair {0}...", j + 1);

                    grid.StartGoalPairIndex = j;
                    StartGoalPairDataObject sgpData = new StartGoalPairDataObject(grid.Start.X, grid.Start.Y, grid.Goal.X, grid.Goal.Y);

                    IPathAlgorithm temp;
                    List<IPathAlgorithm> pathAlgorithmList = new List<IPathAlgorithm>();

                    #region initAlgorithms
                    // Algorithm Titles
                    List<string> algorithmTitles = new List<string>();

                    // A* Path Algorithms
                    temp = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    (temp as AStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DefaultHeuristic;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Default");

                    temp = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    (temp as AStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Manhattan;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Manhattan");

                    temp = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    (temp as AStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Euclidean;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Euclidean");

                    temp = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    (temp as AStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DiagonalDistance;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Diagonal");

                    temp = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    (temp as AStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Chebyshev;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Chebyshev");

                    // Weighted A* Path Algorithms
                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 1.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DefaultHeuristic;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Default (w = 1.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 2.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DefaultHeuristic;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Default (w = 2.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 1.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Manhattan;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Manhattan (w = 1.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 2.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Manhattan;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Manhattan (w = 2.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 1.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Euclidean;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Euclidean (w = 1.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 2.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Euclidean;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Euclidean (w = 2.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 1.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DiagonalDistance;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Diagonal (w = 1.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 2.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.DiagonalDistance;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Diagonal (w = 2.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 1.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Chebyshev;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Chebyshev (w = 1.25)");

                    temp = new WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, 2.25M);
                    (temp as WeightedAStarPathAlgorithm).H = Algorithms.Formulas.Heuristics.Chebyshev;
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Weighted Chebyshev (w = 2.25)");

                    // Uniform Cost Path Algorithm
                    temp = new UniformCostSearchPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Uniform Cost");

                    // Sequential A* Path Algorithm
                    List<Algorithms.Abstract.Delegates.HDelegate> hListManAnchor = new List<Delegates.HDelegate>()
                    {
                        Algorithms.Formulas.Heuristics.Manhattan,
                        Algorithms.Formulas.Heuristics.DefaultHeuristic,
                        Algorithms.Formulas.Heuristics.Euclidean,
                        Algorithms.Formulas.Heuristics.DiagonalDistance,
                        Algorithms.Formulas.Heuristics.Chebyshev,
                    };

                    List<Algorithms.Abstract.Delegates.HDelegate> hListEucAnchor = new List<Delegates.HDelegate>()
                    {
                        Algorithms.Formulas.Heuristics.Euclidean,
                        Algorithms.Formulas.Heuristics.Manhattan,
                        Algorithms.Formulas.Heuristics.DefaultHeuristic,
                        Algorithms.Formulas.Heuristics.DiagonalDistance,
                        Algorithms.Formulas.Heuristics.Chebyshev,
                    };

                    temp = new AStarSequentialHeuristic(grid.Cells, grid.Start, grid.Goal, 5, 1.25M, 2M);
                    (temp as AStarSequentialHeuristic).Heuristics = hListManAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Sequential (Anchor = Manhattan, w1 = 1.25, w2 = 2)");

                    temp = new AStarSequentialHeuristic(grid.Cells, grid.Start, grid.Goal, 5, 1.5M, 2.25M);
                    (temp as AStarSequentialHeuristic).Heuristics = hListManAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Sequential (Anchor = Manhattan, w1 = 1.5, w2 = 2.25)");

                    temp = new AStarSequentialHeuristic(grid.Cells, grid.Start, grid.Goal, 5, 1.25M, 2M);
                    (temp as AStarSequentialHeuristic).Heuristics = hListEucAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Sequential (Anchor = Euclidean, w1 = 1.25, w2 = 2)");

                    temp = new AStarSequentialHeuristic(grid.Cells, grid.Start, grid.Goal, 5, 1.5M, 2.25M);
                    (temp as AStarSequentialHeuristic).Heuristics = hListEucAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Sequential (Anchor = Euclidean, w1 = 1.5, w2 = 2.25)");

                    // A* Integrated Algorithms
                    temp = new AStarIntegratedAlgorithm(grid.Cells, grid.Start, grid.Goal, 5, 1.25M, 2M);
                    (temp as AStarIntegratedAlgorithm).Heuristics = hListManAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Integrated (Anchor = Manhattan, w1 = 1.25, w2 = 2)");

                    temp = new AStarIntegratedAlgorithm(grid.Cells, grid.Start, grid.Goal, 5, 1.5M, 2.25M);
                    (temp as AStarIntegratedAlgorithm).Heuristics = hListManAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Integrated (Anchor = Manhattan, w1 = 1.5, w2 = 2.25)");

                    temp = new AStarIntegratedAlgorithm(grid.Cells, grid.Start, grid.Goal, 5, 1.25M, 2M);
                    (temp as AStarIntegratedAlgorithm).Heuristics = hListEucAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Integrated (Anchor = Euclidean, w1 = 1.25, w2 = 2)");

                    temp = new AStarIntegratedAlgorithm(grid.Cells, grid.Start, grid.Goal, 5, 1.5M, 2.25M);
                    (temp as AStarIntegratedAlgorithm).Heuristics = hListEucAnchor.ToArray();
                    pathAlgorithmList.Add(temp);
                    algorithmTitles.Add("A* Integrated (Anchor = Euclidean, w1 = 1.5, w2 = 2.25)");
                    #endregion

                    int t = 0;
                    foreach (IPathAlgorithm pa in pathAlgorithmList)
                    {
                        AlgorithmResultsDataObject ar = new AlgorithmResultsDataObject(pa.RunAlgorithm(), algorithmTitles[t++]);
                        sgpData.AlgorithmResultsData.Add(ar);
                    }

                    Console.WriteLine("Start/Goal Pair {0} algorithms finished", j + 1);
                    mapData.StartGoalPairs.Add(sgpData);
                }

                Console.WriteLine("Map {0} finished...", i + 1);
                data.MapData.Add(mapData);
            }


            Excel.Export(data);
        }
    }
}
