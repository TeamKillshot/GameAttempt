using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAttempt.Components
{
    class PlayerComponent : DrawableGameComponent
    {
        //Properties
        Vector2 previousPosition { get; set; }
        Texture2D Sprite { get; set; }
        public int ID { get; set; }

        //variables
        int speed;
        TRender tiles;
        PlayerIndex index;
        Vector2 Position;
        Rectangle Bounds;
        Camera camera;
        bool isCollided = false;
        bool isFalling = true;

        public PlayerComponent(Game game): base(game)
        {
            GamePad.GetState(index);
            game.Components.Add(this);
            tiles = new TRender(game);
        }

        public override void Initialize()
        {
            Position = new Vector2(200, 300);
            speed = 9;
            ID = (int)index;

            camera = new Camera(Vector2.Zero,
            new Vector2(tiles.tileMap.GetLength(1) * tiles.tsWidth,
             tiles.tileMap.GetLength(0) * tiles.tsHeight),
                GraphicsDevice.Viewport);

            Game.Services.AddService<Camera>(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            switch(index)
            {
                case PlayerIndex.One:
                    Sprite = Game.Content.Load<Texture2D>("Sprites/Mike");
                    ID = 1;
                    break;

                case PlayerIndex.Two:
                    Sprite = Game.Content.Load<Texture2D>("Sprites/Spike");
                    ID = 2;
                    break;

                case PlayerIndex.Three:
                    Sprite = Game.Content.Load<Texture2D>("Sprites/Floor");
                    ID = 3;
                    break;

                case PlayerIndex.Four:
                    Sprite = Game.Content.Load<Texture2D>("Sprites/Collision");
                    ID = 4;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyHeld(Keys.A))
            {
                Position -= new Vector2(9, 0);
            }
            if (InputManager.IsKeyHeld(Keys.D))
            {
                Position += new Vector2(9, 0);
            }
            if(InputManager.IsKeyPressed(Keys.W) || InputManager.IsButtonPressed(Buttons.A) && isFalling == false)
            {
                Position -= new Vector2(0, 125);
                isFalling = true;
            }

            GamePadState state = GamePad.GetState(index);
            Position.X += state.ThumbSticks.Left.X * speed;

            Bounds = new Rectangle(Position.ToPoint(), new Point(64, 64));
            previousPosition = Position;
            Position.Y += 4;

            //PlayerMove();

            foreach (Collider c in tiles.collisons)
            {
                if (Bounds.Intersects(c.collider) && isCollided == false)
                {
                    Position = previousPosition;
                    isFalling = false;

                    if(Bounds.Left >= c.collider.Right)
                    {
                        Position = previousPosition;
                    }
                    else if(Bounds.Right <= c.collider.Left)
                    {
                        Position = previousPosition;
                    }
                }
            }

            base.Update(gameTime);
        }

        public void PlayerMove()
        {
            if (this != null /*&& player.isConnected == true*/)
            {
                Bounds = new Rectangle(Position.ToPoint(), new Point(64, 64));

                GamePadState state = GamePad.GetState(index);
                Position *= (state.ThumbSticks.Left);

                if (InputManager.IsButtonPressed(Buttons.A))
                {
                    //player.Body.ApplyLinearImpulse(player.Jump);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            //Camera Cam = Game.Services.GetService<Camera>();

            spriteBatch.Begin(/*SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.CurrentCamTranslation*/);
            spriteBatch.Draw(Sprite, Bounds, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
