using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeuristicSearch.GridContent
{
    public class AlgorithmRunDetails
    {
        public static Texture2D LinePixelTexture { get; set; }
        public static SpriteFont SpriteFont { get; set; }

        private readonly Rectangle textBackgroundRectBorder = new Rectangle(998, 338, 207, 154);
        private readonly Rectangle textBackgroundRect = new Rectangle(1000, 340, 203, 150);
        private readonly Vector2 textPosition = new Vector2(1007, 350);
        private readonly Color textColor = Color.Snow;
        private readonly Color lineColor = Color.Cyan;
        private const int TIMER_INTERVAL = 10;

        public List<Cell> Path { get; set; }
        public TimeSpan? ElapsedTime { get; set; }

        private List<Tuple<Vector2, Vector2>> pathLineVectors;

        private System.Timers.Timer animationTimer;

        public AlgorithmRunDetails()
        {
            this.pathLineVectors = new List<Tuple<Vector2, Vector2>>();
            this.Path = new List<Cell>();
        }

        public void StartAnimation()
        {
            animationTimer = new System.Timers.Timer(TIMER_INTERVAL);
            int i = Path.Count - 1;

            animationTimer.Elapsed += (s, e) =>
            {
                if (i < 1)
                    animationTimer.Stop();
                else
                {
                    Cell c1 = Path[i--];
                    Cell c2 = Path[i];

                    pathLineVectors.Add(new Tuple<Vector2, Vector2>(
                        new Vector2((32 + (int)c1.Y * 6) + 3f, (32 + (int)c1.X * 6) + 3f),
                        new Vector2((32 + (int)c2.Y * 6) + 3f, (32 + (int)c2.X * 6) + 3f)));
                }
            };

            animationTimer.AutoReset = true;
            animationTimer.Start();
        }

        public void Reset()
        {
            if (animationTimer != null)
                animationTimer.Stop();
            pathLineVectors.Clear();
            Path.Clear();
            ElapsedTime = null;
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

            spriteBatch.Draw(
                texture: LinePixelTexture,
                sourceRectangle: null,
                effects: SpriteEffects.None,
                origin: Vector2.Zero,
                rotation: 0f,
                destinationRectangle: textBackgroundRectBorder,
                color: Color.Black,
                layerDepth: 0.0f);

            spriteBatch.Draw(
                texture: LinePixelTexture,
                sourceRectangle: null,
                effects: SpriteEffects.None,
                origin: Vector2.Zero,
                rotation: 0f,
                destinationRectangle: textBackgroundRect,
                color: Color.SteelBlue,
                layerDepth: 0.1f);

            string time;
            if (ElapsedTime.HasValue)
                time = (ElapsedTime.Value.Ticks / 10000f).ToString();
            else
                time = "-- ";

            spriteBatch.DrawString(
                spriteFont: SpriteFont,
                text: string.Format("Elapsed time: {0}ms", time),
                position: textPosition,
                color: textColor,
                rotation: 0,
                origin: Vector2.Zero,
                scale: 1,
                effects: SpriteEffects.None,
                layerDepth: 0.5f);
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(
                texture: LinePixelTexture,
                destinationRectangle: r,
                sourceRectangle: null,
                effects: SpriteEffects.None,
                color: color, 
                rotation: angle, 
                origin: Vector2.Zero,
                layerDepth: 1.0f);
        }
    }
}
