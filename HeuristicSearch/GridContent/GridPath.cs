using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeuristicSearch.GridContent
{
    public class GridPath
    {
        public static Texture2D LinePixelTexture { get; set; }

        private const int TIMER_INTERVAL = 20;

        private List<Cell> path;

        private List<Tuple<Vector2, Vector2>> pathLineVectors;

        private Color lineColor;

        public GridPath(List<Cell> path, Color lineColor)
        {
            this.path = path;
            this.lineColor = lineColor;
            this.pathLineVectors = new List<Tuple<Vector2, Vector2>>();
        }

        public void StartAnimation()
        {
            System.Timers.Timer timer = new System.Timers.Timer(TIMER_INTERVAL);
            int i = path.Count - 1;
            
            timer.Elapsed += (s, e) =>
            {
                if (i < 1)
                    timer.Stop();
                else
                {
                    Cell c1 = path[i--];
                    Cell c2 = path[i];

                    pathLineVectors.Add(new Tuple<Vector2, Vector2>(
                        new Vector2((32 + (int)c1.Y * 6) + 3f, (32 + (int)c1.X * 6) + 3f),
                        new Vector2((32 + (int)c2.Y * 6) + 3f, (32 + (int)c2.X * 6) + 3f)));
                }
            };

            timer.AutoReset = true;
            timer.Start();
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < pathLineVectors.Count; i++)
            {
                DrawLine(spriteBatch, pathLineVectors[i].Item1, pathLineVectors[i].Item2, lineColor, 1);
            }
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(LinePixelTexture, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
