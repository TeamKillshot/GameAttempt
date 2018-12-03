using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Components
{
    public class Menu
    {
        protected Game thisGame;

        public Texture2D Texture { get; set; }
        public Rectangle Bounds;
        public Vector2 Position { get; set; }
        public Color Tint { get; set; }
        public bool WasClicked { get; set; }

        public Menu(Texture2D texture, Vector2 position, Color tint, bool wasClicked)
        {
            Texture = texture;
            Position = position;
            Bounds = new Rectangle(Position.ToPoint(), new Point(250,250));
            tint = Tint;
            wasClicked = WasClicked;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && Bounds.Contains(mousePos))
            {
                WasClicked = true;
            }

            if(WasClicked == true)
            {
                Tint = Color.PaleVioletRed;
                WasClicked = false;
            }
            else
            {
                Tint = Color.White;
            }
        }

    }
}
