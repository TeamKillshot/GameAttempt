using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components.SceneManager;
using GameAttempt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Components
{
    class Scene
    {
        // Properties.
        public Texture2D Background { get; set; }
        public Vector2 Position { get; set; }

        //Menu scene.
        public List<Menu> MenuItems { get; }

        //Play Scene.
        public Player Player;
        public List<Player> playersList = new List<Player>();

        private _activeScreen _current;

        // Menu Scene Constructor.
        public Scene(Texture2D background_, List<Menu> menu_)
        {
            Background = background_;
            MenuItems = menu_;

            // set active screen to MENU
            _current = _activeScreen.MENU;
        }

        //Play Screen Constructor.
        public Scene(Player player)
        {
            player = Player;

            // Set active screen to PLAY
            _current = _activeScreen.PLAY;
        }

        //HighScores Scene Constructor
        //public Scene(Texture2D background_, List<Scores> scores_)
        //{
        //    Background = background_;
        //    ScoreItems = scores_;

        //    _current = _activeScreen.HIGHSCORE;
        //}

        public virtual void Update(GameTime gameTime_)
        {
            switch (_current)
            {
                case _activeScreen.MENU:
                    // Render the scene's menu items on top of this background.
                    foreach (Menu m in MenuItems)
                    {
                        m.Update(gameTime_);
                    }
                    break;

                case _activeScreen.PLAY:
                    // Update the scene's collectables.
                    Player.Update(gameTime_);
                    break;

                    //case _activeScreen.HIGHSCORE:
                    //    foreach(Scores s in ScoreItems)
                    //    {
                    //        s.Update(gameTime_);
                    //    }
                    //    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch_, List<Player> playerList)
        {
            // Draw the background first.
            spriteBatch_.Draw(Background, Position, Color.White);

            switch(_current)
            {
                case _activeScreen.MENU:
                    // Render the scene's menu items on top of this background.
                    foreach (Menu m in MenuItems)
                    {
                        spriteBatch_.Draw(m.Texture, m.Position, m.Tint);
                    }
                    break;

                //case _activeScreen.PAUSE:
                //    foreach(Menu m in MenuItems)
                //    {
                //        spriteBatch_.Draw(m.Image, m.Position, m.Tint);
                //    }
                //    break;

                case _activeScreen.PLAY:
                    // Render the Play items on top of this background.
                    Player.Draw(spriteBatch_, playerList);
                    break;


                    //case _activeScreen.HIGHSCORE:
                    //    foreach(Scores s in ScoreItems)
                    //    {
                    //        spriteBatch_.Draw(s.Image, s.Position, s.Tint);
                    //    }
                    //    break;
            }            
        }
    }
}
