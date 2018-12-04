using Components;
using Components.SceneManager;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameAttempt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TileEngine;

namespace GameAttempt
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TRender tiles;
        public Player player, player1, player2, player3, player4;
        public List<Player> playersList = new List<Player>();

        #region Properties and Variables for Menu
        //Dictionary<string, Texture2D> menuTextures = new Dictionary<string, Texture2D>();

        //List<Menu> menuChoices = new List<Menu>();
        //Menu play;
        //Menu scores;
        //Menu exit;

        //private _activeScreen _current;
        //Scene menuScene;
        //Scene playScene;
        //Scene activeScene;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            new InputManager(this);
            player = new Player(this);
            tiles = new TRender(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            #region Player Instances
            player1 = new Player(this);
            player1.Name = "Player1";
            player1.Sprite = Content.Load<Texture2D>("Sprites/Mike");

            player2 = new Player(this);
            player2.Name = "Player2";
            player2.Sprite = Content.Load<Texture2D>("Sprites/Spike");

            player3 = new Player(this);
            player3.Name = "Player3";

            player4 = new Player(this);
            player4.Name = "Player4";

            #endregion

            playersList.Add(player1);
            playersList.Add(player2);
            playersList.Add(player3);
            playersList.Add(player4);

            //world = new World(new Vector2(0, gravity));

            foreach (Player player in playersList)
            {
                player.GetPlayerIndex(player);
                player.GetPlayerPosition(player);
            }

            #region Load Menu Textures and Scenes

            //menuTextures = Loader.ContentLoad<Texture2D>(Content, "MenuTextures");

            //play = new Menu(menuTextures["MenuTexture2_Play"], new Vector2(520, 300), Color.White, false);
            //scores = new Menu(menuTextures["MenuTexture3_HighScore"], new Vector2(520, 420), Color.White, false);
            //exit = new Menu(menuTextures["MenuTexture4_Exit"], new Vector2(520, 540), Color.White, false);

            //List<Menu> menuItems = new List<Menu>();

            ////add them to the list
            //menuItems.Add(play);
            //menuItems.Add(scores);
            //menuItems.Add(exit);

            ////create the Menu
            //menuScene = new Scene(menuTextures["MenuTexture1_Background"], menuItems);

            //_current = _activeScreen.MENU;
            //activeScene = menuScene;

            //_current = _activeScreen.PLAY;
            //activeScene = playScene;

            #endregion

            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService<SpriteBatch>(spriteBatch);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region Update Menu

            //Vector2 mousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            //IsMouseVisible = true;

            ////Rectangle over the play button to know when the button is clicked, changes the both screen state enum and the scene variable
            //Rectangle playButtonPos = play.Bounds;
            //if (playButtonPos.Contains(mousePos) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    _current = _activeScreen.PLAY;
            //    activeScene = playScene;
            //}

            #endregion

            player.Update(gameTime, playersList, tiles);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            player.Draw(spriteBatch, playersList);

            #region Draw Menu
            //switch (_current)
            //{
            //    case _activeScreen.MENU:
            //        spriteBatch.Begin();
            //        activeScene.Draw(spriteBatch, playersList);
            //        spriteBatch.End();
            //        break;

            //    case _activeScreen.PLAY:
            //        spriteBatch.Begin();
            //        activeScene.Draw(spriteBatch, playersList);
            //        player.Draw(spriteBatch, playersList);
            //        spriteBatch.End();
            //        break;
            //}
            #endregion

            base.Draw(gameTime);
        }
    }
}
