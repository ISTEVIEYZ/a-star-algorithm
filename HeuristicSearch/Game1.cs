using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HeuristicSearch.GridContent;
using HeuristicSearch.Controls;
using HeuristicSearch.Algorithms;
using HeuristicSearch.Algorithms.Abstract;

namespace HeuristicSearch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont textFont;

        Grid grid;
        Button[] buttons;  

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1212;
            graphics.PreferredBackBufferHeight = 783;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            grid = new Grid();

            GlobalLogger.ClearLog();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textFont = Content.Load<SpriteFont>("font");

            Controls.Button.ButtonTexture = Content.Load<Texture2D>("buttons");
            Texture2D tileTexture = Content.Load<Texture2D>("tile");
            Texture2D pixelTexture = Content.Load<Texture2D>("pixel");

            Cell.CellTexture = tileTexture;
            Grid.GridBackgroundTexture = tileTexture;
            Algorithms.DataStructures.AlgorithmResults.LinePixelTexture = pixelTexture;
            Algorithms.DataStructures.AlgorithmResults.SpriteFont = textFont;
            CellDetailHover.SpriteFont = textFont;

            grid.RandomlyGenerateGrid();

            buttons = new Button[5];

            buttons[0] = new Button(
                destinationRectangle: new Rectangle(1024, 32, 156, 39),
                sourceRectangle: new Rectangle(0, 0, 156, 39));
            buttons[0].Tag = "RegenerateGrid";

            buttons[1] = new Button(
                destinationRectangle: new Rectangle(1024, 73, 156, 39),
                sourceRectangle: new Rectangle(0, 39, 156, 39));
            buttons[1].Tag = "SaveToFile";

            buttons[2] = new Button(
                destinationRectangle: new Rectangle(1024, 114, 156, 39),
                sourceRectangle: new Rectangle(0, 78, 156, 39));
            buttons[2].Tag = "LoadFromFile";

            buttons[3] = new Button(
                destinationRectangle: new Rectangle(1024, 200, 156, 39),
                sourceRectangle: new Rectangle(0, 117, 156, 39));
            buttons[3].Tag = "RunAStar";

            buttons[4] = new Button(
                destinationRectangle: new Rectangle(1024, 500, 156, 39),
                sourceRectangle: new Rectangle(0, 234, 156, 39));
            buttons[4].Tag = "SelectStartGoal";

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            MouseState mouseState = Mouse.GetState();

            foreach (Button button in buttons)
            {
                button.Update(gameTime, mouseState);

                if (button.IsMouseClick)
                {
                    string filepath;
                    IPathAlgorithm a;

                    switch (button.Tag)
                    {
                        case "RegenerateGrid":
                            grid.RandomlyGenerateGrid();
                            grid.AlgorithmResults.Reset();
                            break;
                        case "SaveToFile":
                            filepath = DialogBoxes.SaveDialogBox.ShowDialog();
                            if (!string.IsNullOrWhiteSpace(filepath))
                                grid.WriteToFile(filepath);
                            break;
                        case "LoadFromFile":
                            filepath = DialogBoxes.LoadDialogBox.ShowDialog();
                            if (!string.IsNullOrWhiteSpace(filepath))
                            {
                                grid.LoadFromFile(filepath);
                                grid.AlgorithmResults.Reset();
                            }
                            break;
                        case "RunAStar":
                            grid.ClearCellValues();
                            using (DialogBoxes.SelectAlgorithmForm frm = new DialogBoxes.SelectAlgorithmForm(grid.Cells, grid.Start, grid.Goal))
                            {
                                frm.ShowDialog();
                                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    a = frm.PathAlgorithm;
                                    grid.AlgorithmResults.Reset();
                                    grid.AlgorithmResults = a.RunAlgorithm();
                                    if (grid.AlgorithmResults.Success)
                                        grid.AlgorithmResults.StartAnimation();
                                    else
                                        System.Windows.Forms.MessageBox.Show("No path found");
                                }
                            }

                            //a = new AStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                            //if (a.RunAlgorithm())
                            //{
                            //    grid.AlgorithmRunDetails = a.GetDetails();
                            //    grid.AlgorithmRunDetails.StartAnimation();
                            //}
                            //else
                            //{
                            //    grid.AlgorithmRunDetails.Reset();
                            //    System.Windows.Forms.MessageBox.Show("No path found");
                            //}
                            break;
                        //case "RunWeightedAStar":
                        //    using (DialogBoxes.WeightedAStartSelectForm frm = new DialogBoxes.WeightedAStartSelectForm())
                        //    {
                        //        frm.ShowDialog();
                        //        if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                        //        {
                        //            string h = frm.Heuristic;
                        //            decimal w = frm.Weight;
                        //            runHeuristic(h, w);
                        //        }
                        //    }
                        //    break;
                        //case "RunUniformCostSearch":
                        //    grid.ClearCellValues();
                        //    a = new Algorithms.UniformCostSearchPathAlgorithm(grid.Cells, grid.Start, grid.Goal);
                        //    if (a.RunAlgorithm())
                        //    {
                        //        grid.AlgorithmRunDetails = a.GetDetails();
                        //        grid.AlgorithmRunDetails.StartAnimation();
                        //    }
                        //    else
                        //    {
                        //        grid.AlgorithmRunDetails.Reset();
                        //        System.Windows.Forms.MessageBox.Show("No path found");
                        //    }
                        //    break;
                        case "SelectStartGoal":
                            using (DialogBoxes.StartGoalPairSelectForm frm = new DialogBoxes.StartGoalPairSelectForm(grid.StartGoalPairIndex, grid.StartGoalPairs))
                            {
                                frm.ShowDialog();
                                if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    grid.StartGoalPairIndex = frm.StartGoalPairIndex;
                                    grid.ClearCellValues();
                                    grid.AlgorithmResults.Reset();
                                }
                            }
                            break;
                            //case "SequentialAStar":
                            //    AStarSequentialHeuristic aseq = new AStarSequentialHeuristic(grid.Cells, grid.Start, grid.Goal, 2, 1, 1);
                            //    aseq.Heuristics = new Delegates.HDelegate[] { Algorithms.Formulas.Heuristics.Manhattan, Algorithms.Formulas.Heuristics.Chebyshev };
                            //    Algorithms.DataStructures.AlgorithmResults ar = aseq.RunAlgorithm();
                            //    if (ar.Success)
                            //    {
                            //        System.Windows.Forms.MessageBox.Show("Success");
                            //    }
                            //    else
                            //        System.Windows.Forms.MessageBox.Show("No path found");
                            //    break;
                    }
                }
            }

            grid.Update(gameTime, mouseState);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack);

            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

            grid.Draw(spriteBatch, graphics.GraphicsDevice);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        //private void runHeuristic(string heuristic, decimal weight)
        //{
        //    grid.ClearCellValues();
        //    Algorithms.WeightedAStarPathAlgorithm a = new Algorithms.WeightedAStarPathAlgorithm(grid.Cells, grid.Start, grid.Goal, weight);
            
        //    switch (heuristic)
        //    {
        //        case "Manhattan":
        //            a.H = Algorithms.Formulas.Heuristics.Manhattan;
        //            break;
        //        case "Euclidean":
        //            a.H = Algorithms.Formulas.Heuristics.Euclidean;
        //            break;
        //        case "Diagonal Distance":
        //            a.H = Algorithms.Formulas.Heuristics.DiagonalDistance;
        //            break;
        //        case "Chebyshev":
        //            a.H = Algorithms.Formulas.Heuristics.Chebyshev;
        //            break;
        //    }

        //    if (a.RunAlgorithm())
        //    {
        //        grid.AlgorithmRunDetails = a.GetDetails();
        //        grid.AlgorithmRunDetails.StartAnimation();
        //    }
        //    else
        //    {
        //        grid.AlgorithmRunDetails.Reset();
        //        System.Windows.Forms.MessageBox.Show("No path found");
        //    }
        //}
    }
}
