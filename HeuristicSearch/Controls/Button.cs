using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeuristicSearch.Controls
{
    [Serializable]
    public class Button : Clickable
    {
        public static Texture2D ButtonTexture { get; set; }

        public string Tag { get; set; }

        [NonSerialized]
        private Rectangle _sourceRectangle;

        [NonSerialized]
        private Rectangle _destinationRectangle;

        public Button(Rectangle sourceRectangle, Rectangle destinationRectangle) : base(destinationRectangle)
        {
            _sourceRectangle = sourceRectangle;
            _destinationRectangle = destinationRectangle;
        }

        public override void Update(GameTime gameTime, MouseState mouseState)
        {
            base.Update(gameTime, mouseState);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color drawColor = Color.White;
            if (IsMouseHover)
                drawColor = Color.LightGray;
            if (IsMouseDown)
                drawColor = Color.Gray;

            spriteBatch.Draw(
                texture: ButtonTexture,
                sourceRectangle: _sourceRectangle,
                destinationRectangle: _destinationRectangle,
                color: drawColor,
                layerDepth: 0);
        }
    }
}
