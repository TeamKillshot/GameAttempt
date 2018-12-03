using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components.SceneManager
{
    public enum _activeScreen { MENU, PLAY, PAUSE, HIGHSCORE, };
    public static class SceneManager
    { 
        public static SpriteFont GameFont;
        public static GraphicsDevice graphicsDevice;
    }
}
