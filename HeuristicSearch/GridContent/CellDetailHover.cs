using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeuristicSearch.GridContent
{
    [Serializable]
    public class CellDetailHover
    {
        public static SpriteFont SpriteFont { get; set; }

        private const int GRID_START_Y = 32;
        private const int GRID_START_X = 32;
        private const int CELL_EDGE_SIZE = 5;
        private const int CELL_MARGIN = 1;

        [NonSerialized]
        private readonly Vector2 DRAW_VECTOR = new Vector2(1007, 375);


        [NonSerialized]
        private Rectangle boundingRect;
        private bool isMouseHover;

        private Cell parentCell;

        public CellDetailHover(Cell cell)
        {
            boundingRect = new Rectangle(GRID_START_Y + cell.Y * (CELL_EDGE_SIZE + CELL_MARGIN)
                                                      , GRID_START_X + cell.X * (CELL_EDGE_SIZE + CELL_MARGIN)
                                                      , CELL_EDGE_SIZE
                                                      , CELL_EDGE_SIZE);

            isMouseHover = false;
            parentCell = cell;
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            isMouseHover = boundingRect.Contains(mouseState.Position);

            //if (boundingRect.Contains(mouseState.Position))
            //{
            //    isMouseHover = true;
            //    drawX = mouseState.X - 50;
            //    drawY = mouseState.Y - 110;

            //    if (drawX < 1)
            //        drawX = 1;
            //    if (drawY < 1)
            //        drawY = 1;
            //}
            //else
            //    isMouseHover = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isMouseHover)
            {
                spriteBatch.DrawString(
                    spriteFont: SpriteFont,
                    text: this.ToString(),
                    position: DRAW_VECTOR,
                    color: Color.Snow,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: 1,
                    effects: SpriteEffects.None,
                    layerDepth: 0.9f);
            }
        }

        public override string ToString()
        {
            return string.Format("Cell ({1}, {2}){0}F: {3}{0}G: {4}{0}H: {5}",
                System.Environment.NewLine,
                parentCell.X,
                parentCell.Y,
                parentCell.F == decimal.MaxValue ? "Infinity" : parentCell.F.ToString(),
                parentCell.G == decimal.MaxValue ? "Infinity" : parentCell.G.ToString(),
                parentCell.H == decimal.MaxValue ? "Infinity" : parentCell.H.ToString());
        }
    }
}
