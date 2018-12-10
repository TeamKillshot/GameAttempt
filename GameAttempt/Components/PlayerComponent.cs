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
        AnimatedSprite Sprite { get; set; }
        public int ID { get; set; }

        //variables
        int speed;
        TRender tiles;
        PlayerIndex index;
        Texture2D PlayerRect;
        Vector2 Position;
        Rectangle Bounds;
        Camera camera;
        bool isFalling = true;

        //PlayerStates
        public enum PlayerState { STILL, WALK, JUMP }
        PlayerState _current;

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
            _current = PlayerState.STILL;

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
                    Sprite = new AnimatedSprite(Game, 
                        Game.Content.Load<Texture2D>("Sprites/CharacterSpriteSheet"), Position, 11, Bounds);
                    ID = 1;
                    break;

                case PlayerIndex.Two:
                    Sprite = new AnimatedSprite(Game,
                        Game.Content.Load<Texture2D>("Sprites/CharacterSpriteSheet"), Position, 11, Bounds);
                    ID = 2;
                    break;

                case PlayerIndex.Three:
                    Sprite = new AnimatedSprite(Game,
                        Game.Content.Load<Texture2D>("Sprites/CharacterSpriteSheet"), Position, 11, Bounds);
                    ID = 3;
                    break;

                case PlayerIndex.Four:
                    Sprite = new AnimatedSprite(Game,
                        Game.Content.Load<Texture2D>("Sprites/CharacterSpriteSheet"), Position, 11, Bounds);
                    ID = 4;
                    break;
            }

            PlayerRect = Game.Content.Load<Texture2D>("Sprites/Collison");
        }

        public override void Update(GameTime gameTime)
        {
            _current = PlayerState.STILL;
            if (InputManager.IsKeyHeld(Keys.A))
            {
                Position -= new Vector2(9, 0);
                _current = PlayerState.WALK;
            }
            if (InputManager.IsKeyHeld(Keys.D))
            {
                Position += new Vector2(9, 0);
                _current = PlayerState.WALK;
            }
            if(InputManager.IsKeyPressed(Keys.W) && isFalling == false
                || InputManager.IsButtonPressed(Buttons.A) && isFalling == false)
            {
                Position -= new Vector2(0, 125);
                _current = PlayerState.JUMP;
                isFalling = true;
            }

            GamePadState state = GamePad.GetState(index);
            Position.X += state.ThumbSticks.Left.X * speed;

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 64, 64);
            previousPosition = Position;
            Position.Y += 4;

            foreach (Collider c in tiles.collisons)
            {
                Rectangle FloorRec = c.collider;

                if (Bounds.Intersects(FloorRec))
                {
                    Position = previousPosition;
                    isFalling = false;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            //Camera Cam = Game.Services.GetService<Camera>();

            spriteBatch.Begin(/*SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.CurrentCamTranslation*/);
            switch (_current)
            {
                case PlayerState.STILL:
                    Sprite = new AnimatedSprite(Game,
                        Game.Content.Load<Texture2D>("Sprites/Mike"), Position, 1, Bounds);
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
                case PlayerState.WALK:
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
                case PlayerState.JUMP:
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
            }
            //spriteBatch.Draw(PlayerRect, Bounds, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
