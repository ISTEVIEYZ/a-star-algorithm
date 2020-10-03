using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace HeuristicSearch.GridContent
{
    [Serializable]
    public class Grid
    {
        public static Texture2D GridBackgroundTexture { get; set; }

        public Cell[,] Cells { get; set; }

        [NonSerialized]
        private Rectangle gridBackgroundRect;

        public Cell Goal
        {
            get
            {
                return Cells[(int)StartGoalPairs[startGoalPairIndex].Goal.X, (int)StartGoalPairs[startGoalPairIndex].Goal.Y];
            }
        }

        public Cell Start
        {
            get
            {
                return Cells[(int)StartGoalPairs[startGoalPairIndex].Start.X, (int)StartGoalPairs[startGoalPairIndex].Start.Y];
            }
        }

        [NonSerialized]
        private Vector2[] hardToTraverseCenters;

        private int startGoalPairIndex;
        public int StartGoalPairIndex
        {
            get
            {
                return startGoalPairIndex;
            }
            set
            {
                Vector2 oldStart = StartGoalPairs[startGoalPairIndex].Start;
                Vector2 oldGoal = StartGoalPairs[startGoalPairIndex].Goal;

                Vector2 start = StartGoalPairs[value].Start;
                Vector2 goal = StartGoalPairs[value].Goal;

                Cells[(int)oldStart.X, (int)oldStart.Y].IsStart = false;
                Cells[(int)oldGoal.X, (int)oldGoal.Y].IsGoal = false;

                Cells[(int)start.X, (int)start.Y].IsStart = true;
                Cells[(int)goal.X, (int)goal.Y].IsGoal = true;

                startGoalPairIndex = value;
            }
        }

        public Algorithms.DataStructures.AlgorithmResults AlgorithmResults { get; set; }

        public StartGoalPair[] StartGoalPairs { get; private set; }

        public Grid()
        {
            Cells = new Cell[120, 160];
            hardToTraverseCenters = new Vector2[8];
            StartGoalPairs = new StartGoalPair[10];

            gridBackgroundRect = new Rectangle(31, 31, 961, 721);
            startGoalPairIndex = 0;

            AlgorithmResults = new Algorithms.DataStructures.AlgorithmResults();
        }

        public void RandomlyGenerateGrid()
        {
            /// TODO: Delete this
            GlobalLogger.ClearLog();

            // Create cell array
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    Cells[i, j] = new Cell(this, i, j);

            // Create 8 unique random locations
            Random random = new Random(System.Guid.NewGuid().GetHashCode());
            Vector2 temp;
            int numLoc = 0;
            while (numLoc < 8)
            {
                temp = new Vector2(random.Next(0, 120), random.Next(0, 160));
                if (!hardToTraverseCenters.Contains(temp))
                    hardToTraverseCenters[numLoc++] = temp;
            }

            // Populate with a 50/50 ratio, hard to traverse and regular to traverse cells
            foreach (Vector2 location in hardToTraverseCenters)
                for (int i = (int)location.X - 15; i < (int)location.X + 15; i++)
                    for (int j = (int)location.Y - 15; j < (int)location.Y + 15; j++)
                        if (i > -1 && i < 120 && j > -1 && j < 160)
                            if (random.NextDouble() > 0.49f)
                                Cells[i, j].TraversalType = Cell.TraversalTypes.HARD;

            // Select a random cell along the boundary
            int numHighwaysCreated = 0;
            while (numHighwaysCreated < 4)
            {
                //GlobalLogger.Log(string.Format("{0}Creating highway: {1}{0}", System.Environment.NewLine, numHighwaysCreated + 1));

                double edgeRand = random.NextDouble();

                //GlobalLogger.Log(string.Format("Random number chosen for highway start direction: {0}", edgeRand));

                Vector2? startCoord = null;
                HighwayDirections? startDirection = null;

                if (edgeRand < 0.25f)
                {
                    //GlobalLogger.Log("Highway start direction: Down");
                    startDirection = HighwayDirections.DOWN;
                    startCoord = new Vector2(0, random.Next(1, 159));
                }
                else if (edgeRand < 0.50f)
                {
                    //GlobalLogger.Log("Highway start direction: Left");
                    startDirection = HighwayDirections.LEFT;
                    startCoord = new Vector2(random.Next(1, 119), 159);
                }
                else if (edgeRand < 0.75f)
                {
                    //GlobalLogger.Log("Highway start direction: Up");
                    startDirection = HighwayDirections.UP;
                    startCoord = new Vector2(119, random.Next(1, 159));
                }
                else
                {
                    //GlobalLogger.Log("Highway start direction: Right");
                    startDirection = HighwayDirections.RIGHT;
                    startCoord = new Vector2(random.Next(1, 119), 0);
                }

                if (CreateHighway(startCoord.Value, startDirection.Value))
                    numHighwaysCreated++;

                    //GlobalLogger.Log(string.Format("{0}HIGHWAY FAILED{0}", System.Environment.NewLine));
            }

            // Flag 20% of all cells as blocked as long as they are not a highway
            int numBlockedCells = 0;
            while (numBlockedCells < 3840)
            {
                int x = random.Next(0, 120);
                int y = random.Next(0, 160);

                if (!Cells[x, y].IsHighway)
                {
                    Cells[x, y].TraversalType = Cell.TraversalTypes.BLOCKED;
                    numBlockedCells++;
                }
            }

            // Choose 10 start/goal locations
            int a = 0;
            bool found = false;
            Cell tempCell;
            while (a < 10)
            {
                StartGoalPair sgpair = new StartGoalPair();

                // get start
                List<Vector2> borderCells = GetBorderCells(borderThickness: 20);
                found = false;
                do
                {
                    int rand = random.Next(0, borderCells.Count);
                    Vector2 startV = borderCells[rand];
                    tempCell = Cells[(int)startV.X, (int)startV.Y];

                    if (tempCell.TraversalType != Cell.TraversalTypes.BLOCKED)
                    {
                        sgpair.Start = startV;
                        found = true;
                    }

                } while (!found);

                // get goal
                found = false;
                do
                {
                    int rand = random.Next(0, borderCells.Count);
                    Vector2 goalV = borderCells[rand];
                    tempCell = Cells[(int)goalV.X, (int)goalV.Y];

                    if (tempCell.TraversalType != Cell.TraversalTypes.BLOCKED
                        && CalculateDistance(goalV, sgpair.Start) > 100)
                    {
                        sgpair.Goal = goalV;
                        found = true;
                    }

                } while (!found);

                if (!StartGoalPairs.Contains(sgpair))
                {
                    StartGoalPairs[a] = sgpair;
                    a++;
                }
            }

            StartGoalPairIndex = 0;
        }
        
        // Returns true if entire highway is successfully created
        public bool CreateHighway(Vector2 startCoord, HighwayDirections startDirection)
        {
            Random random = new Random(System.Guid.NewGuid().GetHashCode());
            List<Vector2> totalHighway = new List<Vector2>();
            List<Vector2> highwayPart = null;

            while ((highwayPart = CreateHighwayLeg(startCoord, startDirection, totalHighway)).Count == 20
                && !IsBoundaryCell(highwayPart.Last()))
            {
                // Add vector 2 to potential highway cell list
                totalHighway.AddRange(highwayPart);

                //GlobalLogger.Log(string.Format("Creating new highway leg: Current highway length: {0}", totalHighway.Count));

                // Get new startCoord and startDirection
                double newDirProbability = random.NextDouble();

                //GlobalLogger.Log(string.Format("Random number chosen for new highway direction: {0}", newDirProbability));

                startCoord = totalHighway.Last();                
                switch (startDirection)
                {
                    case HighwayDirections.DOWN:
                    case HighwayDirections.UP:
                        if (newDirProbability < 0.19)
                        {
                            //GlobalLogger.Log("Turning Left");
                            startDirection = HighwayDirections.LEFT;
                            //startCoord = GetNextCell(startCoord, HighwayDirections.LEFT);
                        }
                        else if (newDirProbability < 0.39)
                        {
                            //GlobalLogger.Log("Turning Right");
                            startDirection = HighwayDirections.RIGHT;
                            //startCoord = GetNextCell(startCoord, HighwayDirections.RIGHT);
                        }
                        else
                        {
                            //GlobalLogger.Log("Maintaining Direction");
                            //startCoord = GetNextCell(startCoord, startDirection);
                        }
                        break;
                    case HighwayDirections.LEFT:
                    case HighwayDirections.RIGHT:
                        if (newDirProbability < 0.19)
                        {
                            //GlobalLogger.Log("Turning Up");
                            startDirection = HighwayDirections.UP;
                            //startCoord = GetNextCell(startCoord, HighwayDirections.UP);
                        }
                        else if (newDirProbability < 0.39)
                        {
                            //GlobalLogger.Log("Turning Down");
                            startDirection = HighwayDirections.DOWN;
                            //startCoord = GetNextCell(startCoord, HighwayDirections.DOWN);
                        }
                        else
                        {
                            //GlobalLogger.Log("Maintaining Direction");
                            //startCoord = GetNextCell(startCoord, startDirection);
                        }
                        break;
                }
            }

            // Add cells that 'hit' another boundary
            totalHighway.AddRange(highwayPart);

            if (totalHighway.Count >= 100 && IsBoundaryCell(totalHighway.Last()))
            {
                //GlobalLogger.Log("Highway leg creation successful");
                // Iterate through all Vector2 in totalHighway and mark as highway
                foreach (Vector2 cell in totalHighway)
                {
                    Cells[(int)cell.X, (int)cell.Y].IsHighway = true;
                }
                return true;
            }
            else
            {
                //GlobalLogger.Log("Highway leg creation stopped");
                return false;
            }
        }

        // Returns a list of cells for possible next highway leg
        public List<Vector2> CreateHighwayLeg(Vector2 currentCoord, HighwayDirections currentDirection, List<Vector2> pendingHighway)
        {
            List<Vector2> highwayLeg = new List<Vector2>();

            int numCells = 0;

            //GlobalLogger.Log(string.Format("Leg start point: {0}", currentCoord.ToString()));

            while (numCells < 20 
                && currentCoord.X > -1 
                && currentCoord.X < 120 
                && currentCoord.Y > -1 
                && currentCoord.Y < 160 
                && !Cells[(int)currentCoord.X, (int)currentCoord.Y].IsHighway
                && (numCells == 0 || !pendingHighway.Contains(currentCoord)))
            {
                highwayLeg.Add(currentCoord);
                numCells++;
                currentCoord = GetNextCell(currentCoord, currentDirection);
            }

            //GlobalLogger.Log(string.Format("Leg end point: {0}", currentCoord.ToString()));

            return highwayLeg;
        }

        public void ClearCellValues()
        {
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    Cells[i, j].ClearValues();
        }
        
        public Vector2 GetNextCell(Vector2 start, HighwayDirections dir)
        {
            switch (dir)
            {
                case HighwayDirections.DOWN:
                    return new Vector2(start.X + 1, start.Y);                   
                case HighwayDirections.LEFT:
                    return new Vector2(start.X, start.Y - 1);                  
                case HighwayDirections.RIGHT:
                    return new Vector2(start.X, start.Y + 1);
                case HighwayDirections.UP:
                    return new Vector2(start.X - 1, start.Y);
                default:
                    return default(Vector2);
            }
        }

        public bool IsBoundaryCell(Vector2 v)
        {
            return v.X == 0 || v.X == 119 || v.Y == 0 || v.Y == 159;
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    Cells[i, j].Update(gameTime, mouseState);

            if (AlgorithmResults != null)
                AlgorithmResults.Update();
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            // Draw grid
            spriteBatch.Draw(
                texture: GridBackgroundTexture,
                sourceRectangle: null,
                effects: SpriteEffects.None,
                origin: Vector2.Zero,
                rotation: 0f,
                destinationRectangle: gridBackgroundRect,
                color: Color.DarkSlateGray,
                layerDepth: 0);

            // Draw all cells
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    Cells[i, j].Draw(spriteBatch);

            if (AlgorithmResults != null)
                AlgorithmResults.Draw(spriteBatch);
        }

        public List<Vector2> GetBorderCells(int borderThickness)
        {
            List<Vector2> borderCells = new List<Vector2>();
            for (int i = 0; i < borderThickness; i++)
                for (int j = 0; j < 160; j++)
                    borderCells.Add(new Vector2(i, j));

            for (int i = borderThickness; i < 120 - borderThickness; i++)
            {
                for (int j = 0; j < borderThickness; j++)
                    borderCells.Add(new Vector2(i, j));

                for (int j = 160 - borderThickness; j < 160; j++)
                    borderCells.Add(new Vector2(i, j));
            }

            for (int i = 120 - borderThickness; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    borderCells.Add(new Vector2(i, j));
            
            return borderCells;
        }

        public double CalculateDistance(Vector2 v1, Vector2 v2)
        {
            return Math.Sqrt(Math.Pow(v2.X - v1.X, 2) + Math.Pow(v2.Y - v1.Y, 2));
        }

        // Writes grid to file
        public void WriteToFile(string filepath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Save start locations
                for (int i = 0; i < 10; i++)
                {
                    sb.Append(string.Format("{0},{1} ", StartGoalPairs[i].Start.X, StartGoalPairs[i].Start.Y));
                    if (i == 9)
                        sb.AppendLine();
                }

                // Save goal locations
                for (int i = 0; i < 10; i++)
                {
                    sb.Append(string.Format("{0},{1} ", StartGoalPairs[i].Goal.X, StartGoalPairs[i].Goal.Y));
                    if (i == 9)
                        sb.AppendLine();
                }

                // Save hard to traverse areas
                foreach (Vector2 v in hardToTraverseCenters)
                    sb.AppendLine(string.Format("{0},{1}", v.X, v.Y));

                // Save grid
                for (int i = 0; i < 120; i++)
                {
                    for (int j = 0; j < 160; j++)
                    {
                        sb.Append(string.Format("{0} ", Cells[i, j].ToString()));
                    }
                    sb.AppendLine();
                }

                System.IO.File.WriteAllText(filepath, sb.ToString());
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(
                    string.Format("Error saving file:{0}{1}", System.Environment.NewLine, e.Message));
            }
        }

        // Loads grid from file
        public void LoadFromFile(string filepath)
        {
            // Clear current grid
            for (int i = 0; i < 120; i++)
                for (int j = 0; j < 160; j++)
                    Cells[i, j] = new Cell(this, i, j);

            for (int i = 0; i < 8; i++)
                hardToTraverseCenters[i] = default(Vector2);

            for (int i = 0; i < 10; i++)
                StartGoalPairs[i] = new StartGoalPair();

            // Load grid from file
            try
            {
                // Load all lines from the file
                string[] lines = System.IO.File.ReadAllLines(filepath);
                string[] temp;
                int xTemp;
                int yTemp;

                // Get start locations
                temp = lines[0].Split(' ');
                for (int i = 0; i < 10; i++)
                {
                    string[] tempCoord = temp[i].Split(',');
                    xTemp = int.Parse(tempCoord[0]);
                    yTemp = int.Parse(tempCoord[1]);
                    StartGoalPairs[i].Start = new Vector2(xTemp, yTemp);
                    if (i == 0)
                        Cells[xTemp, yTemp].IsStart = true;
                }

                // Get goal locations
                temp = lines[1].Split(' ');
                for (int i = 0; i < 10; i++)
                {
                    string[] tempCoord = temp[i].Split(',');
                    xTemp = int.Parse(tempCoord[0]);
                    yTemp = int.Parse(tempCoord[1]);
                    StartGoalPairs[i].Goal = new Vector2(xTemp, yTemp);
                    if (i == 0)
                        Cells[xTemp, yTemp].IsGoal = true;
                }

                StartGoalPairIndex = 0;
                
                // Get 8 centers of hard to traverse areas
                for (int i = 0; i < 8; i++)
                {
                    temp = lines[i + 2].Split(',');
                    xTemp = int.Parse(temp[0]);
                    yTemp = int.Parse(temp[1]);
                    hardToTraverseCenters[i] = new Vector2(xTemp, yTemp);
                }

                for (int i = 0; i < 120; i++)
                {
                    temp = lines[i + 10].Split(' ');
                    for (int j = 0; j < 160; j++)
                    {
                        switch (temp[j])
                        {
                            case "0":
                                Cells[i, j].TraversalType = Cell.TraversalTypes.BLOCKED;
                                break;
                            case "1":
                                Cells[i, j].TraversalType = Cell.TraversalTypes.REGULAR;
                                break;
                            case "2":
                                Cells[i, j].TraversalType = Cell.TraversalTypes.HARD;
                                break;
                            case "a":
                                Cells[i, j].TraversalType = Cell.TraversalTypes.REGULAR;
                                Cells[i, j].IsHighway = true;
                                break;
                            case "b":
                                Cells[i, j].TraversalType = Cell.TraversalTypes.HARD;
                                Cells[i, j].IsHighway = true;
                                break;
                            default:
                                throw new Exception("Grid file is in an invalid format");
                        }
                    }
                }
            }
            catch (Exception e)
            {
				System.Windows.Forms.MessageBox.Show(string.Format("Unable to load grid from file{0}{1}", System.Environment.NewLine, e.Message));
            }
        }


        public enum HighwayDirections { UP, DOWN, LEFT, RIGHT };
    }
}
