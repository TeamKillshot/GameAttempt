using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine.TileEngine
{
    public class Collider
    {
        Rectangle collider;
        Texture2D collisonImage;


        public Collider(Texture2D Image, Vector2 startPos, Vector2 size)
        {
            collisonImage = Image;
            collider = new Rectangle((int)startPos.X, (int)startPos.Y, (int)size.X, (int)size.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        { 	
			spriteBatch.Draw(collisonImage, collider, Color.White);
		}
	}
}
