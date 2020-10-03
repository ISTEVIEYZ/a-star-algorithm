using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeuristicSearch.Controls;
using Microsoft.Xna.Framework.Input;

namespace HeuristicSearch.GridContent
{
    [Serializable]
    public class Cell : HeuristicSearch.Controls.Clickable
    {
        public static Texture2D CellTexture { get; set; }

        private Cell parent = null;
        public Cell Parent { get { return parent; } set { parent = value; } }

        private decimal g = decimal.MaxValue;
        public decimal G { get { return g; } set { g = value; } }
        
        private decimal h = decimal.MaxValue;
        public decimal H { get { return h; } set { h = value; } }

        private decimal f = decimal.MaxValue;
        public decimal F { get { return f; } set { f = value; } }
        
        private Grid parentGrid;

        public bool IsGoal { get; set; }
        public bool IsStart { get; set; }
        public bool IsHighway { get; set; }

        private const int CELL_EDGE_SIZE = 5;
        private const int CELL_MARGIN = 1;

        private const int GRID_START_X = 32;
        private const int GRID_START_Y = 32;

        [NonSerialized]
        private Rectangle cellRect;

        [NonSerialized]
        private Rectangle cellBorderRect;

        private CellDetailHover cellDetailHover;

        public int X { get; private set; }
        public int Y { get; private set; }
        
        public TraversalTypes TraversalType { get; set; }
        
        public Cell(Grid g, int x, int y) : base(new Rectangle(GRID_START_Y + y * (CELL_EDGE_SIZE + CELL_MARGIN)
                                                      ,GRID_START_X + x * (CELL_EDGE_SIZE + CELL_MARGIN)
                                                      ,CELL_EDGE_SIZE
                                                      ,CELL_EDGE_SIZE)) // clickable area
        {
            cellRect = _boundingRectangle; // drawn area same as clickable area

            cellBorderRect = new Rectangle(GRID_START_Y + (y * (CELL_EDGE_SIZE + CELL_MARGIN)) - 1
                                           ,GRID_START_X + (x * (CELL_EDGE_SIZE + CELL_MARGIN)) - 1
                                           ,CELL_EDGE_SIZE + 2
                                           ,CELL_EDGE_SIZE + 2); // border area

            X = x;
            Y = y;

            parentGrid = g; 

            this.TraversalType = TraversalTypes.REGULAR;
            this.IsHighway = false;

            cellDetailHover = new CellDetailHover(this);
        }

        public override string ToString()
        {
            if (IsHighway)
            {
                if (TraversalType == TraversalTypes.REGULAR)
                    return "a";
                else if (TraversalType == TraversalTypes.HARD)
                    return "b";
            }
            else
            {
                switch (TraversalType)
                {
                    case TraversalTypes.BLOCKED:
                        return "0";
                    case TraversalTypes.REGULAR:
                        return "1";
                    case TraversalTypes.HARD:
                        return "2";
                }
            }
            return null;
        }

        public override void Update(GameTime gameTime, MouseState mouseState)
        {
            base.Update(gameTime, mouseState);

            //if (IsMouseClick)
            //    System.Windows.Forms.MessageBox.Show(
            //        string.Format("Cell [{1}, {2}]{0}F: {3}{0}G: {4}{0}H: {5}", 
            //            System.Environment.NewLine, 
            //            X, 
            //            Y, 
            //            f == decimal.MaxValue ? "Inf" : f.ToString(), 
            //            g == decimal.MaxValue ? "Inf" : g.ToString(), 
            //            h == decimal.MaxValue ? "Inf" : h.ToString()));

            if (IsMouseHover)
                cellDetailHover.Update(gameTime, mouseState);
        }

        public void ClearValues()
        {
            f = decimal.MaxValue;
            g = decimal.MaxValue;
            h = decimal.MaxValue;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color cellColor = Color.Transparent;

            if (IsMouseHover)
            {
                spriteBatch.Draw(
                    texture: CellTexture,
                    sourceRectangle: null,
                    effects: SpriteEffects.None,
                    origin: Vector2.Zero,
                    rotation: 0f,
                    destinationRectangle: cellBorderRect,
                    color: Color.Red,
                    layerDepth: 0.1f);

                cellDetailHover.Draw(spriteBatch);
            }

            if (IsHighway)
            {
                if (TraversalType == TraversalTypes.REGULAR)
                    cellColor = Color.MediumSlateBlue;
                else if (TraversalType == TraversalTypes.HARD)
                    cellColor = Color.DarkSlateBlue;
            }
            else
            {
                switch (TraversalType)
                {
                    case TraversalTypes.REGULAR:
                        cellColor = Color.ForestGreen;
                        break;
                    case TraversalTypes.HARD:
                        cellColor = Color.DarkGreen;
                        break;
                    case TraversalTypes.BLOCKED:
                        cellColor = Color.Black;
                        break;
                }
            }

            if (IsStart)
                cellColor = Color.Cyan;
            if (IsGoal)
                cellColor = Color.Firebrick;

            spriteBatch.Draw(
                texture: CellTexture,
                sourceRectangle: null,
                effects: SpriteEffects.None,
                origin: Vector2.Zero,
                rotation: 0f,
                destinationRectangle: cellRect,
                color: cellColor,
                layerDepth: 0.5f);
        }

        public override bool Equals(object obj)
        {
            return obj is Cell && (obj as Cell).X == this.X && (obj as Cell).Y == this.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public enum TraversalTypes { REGULAR, HARD, BLOCKED };
    }
}
