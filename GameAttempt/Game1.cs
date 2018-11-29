using Components;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GameAttempt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TileEngine;
using TileEngine.TileEngine;

namespace GameAttempt
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Collider tileRec;
        TRender tiles;

        public Player player, player1, player2, player3, player4;

        List<Player> playersList = new List<Player>();

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

            //player.Position.X = player.Body.Position.X;
            //player.Position.Y = player.Body.Position.Y;

            foreach (Player player in playersList)
                foreach(Collider c in tiles.collisons)
            {
                if (player.Bounds.Intersects(c.GetCollidingRectangle()) && player.hasCollided == false)
                {
                    player.hasCollided = true;
                }
            }

            player.Update(gameTime, playersList);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(gameTime, spriteBatch, playersList);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
