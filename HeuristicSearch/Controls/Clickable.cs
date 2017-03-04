using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HeuristicSearch.Controls
{
    [Serializable]
    public abstract class Clickable
    {
        [NonSerialized]
        protected Rectangle _boundingRectangle;

        public bool IsMouseClick { get; private set; }
        protected bool IsMouseHover { get; private set; }
        protected bool IsMouseDown { get; private set; }

        public Clickable(Rectangle boundingRectangle)
        {
            _boundingRectangle = boundingRectangle;
        }

        public virtual void Update(GameTime gameTime, MouseState mouseState)
        {
            IsMouseClick = false;
            IsMouseClick = IsMouseDown && mouseState.LeftButton == ButtonState.Released;
            IsMouseHover = _boundingRectangle.Contains(mouseState.Position);
            IsMouseDown = IsMouseHover && mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
