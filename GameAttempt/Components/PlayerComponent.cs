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
		SpriteEffects s;

        //variables
        int speed;
        TRender tiles;
        PlayerIndex index;
        Texture2D PlayerRect;
        Vector2 Position;
        Rectangle Bounds;
        Camera camera;

        //PlayerStates
        public enum PlayerState { STILL, WALK, JUMP, FALL }
        PlayerState _current, _previousState;
        bool walkRight, walkLeft;

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
            _current = PlayerState.FALL;
            _previousState = PlayerState.STILL;

            camera = new Camera(Vector2.Zero,
            new Vector2(tiles.tileMap.GetLength(1) * tiles.tsWidth,
             tiles.tileMap.GetLength(0) * tiles.tsHeight),
                GraphicsDevice.Viewport);

            Game.Services.AddService<Camera>(camera);
			Game.Services.AddService<SpriteEffects>(s);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            switch(index)
            {
                default:
                    Sprite = new AnimatedSprite(Game,
                        Game.Content.Load<Texture2D>("Sprites/CharacterSpriteSheet"), Position, 11, Bounds);
                    break;

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
            camera.FollowCharacter(Position, GraphicsDevice.Viewport);
            previousPosition = Position;
            GamePadState state = GamePad.GetState(index);

            switch (_current)
            {
                case PlayerState.FALL:
                    Position.Y += 3;

                    foreach (Collider c in tiles.collisons)
                    {
                        Rectangle FloorRec = c.collider;

                        if (Bounds.Intersects(FloorRec))
                        {
                            Position = previousPosition;
                            c.collisionColor = Color.Red;
                            _current = PlayerState.STILL;
                            break;
                        }
                        else
                        {
                            c.collisionColor = Color.White;
                            _current = PlayerState.FALL;
                        }

                        if (_current != PlayerState.FALL) break;
                    }

                    break;

                case PlayerState.STILL:

                    break;

                case PlayerState.WALK:

                    Position.X += state.ThumbSticks.Left.X * speed;

                    if(state.ThumbSticks.Left.X <= 0)
                    {
                        walkLeft = true;
                    }


                    break;

                case PlayerState.JUMP:
                    break;
            }

            #region Uneeded?
            if (InputManager.IsKeyHeld(Keys.A))
            {
				s = SpriteEffects.None;
                Position -= new Vector2(9, 0);
                _current = PlayerState.WALK;
            }
            if (InputManager.IsKeyHeld(Keys.D))
            {
				s = SpriteEffects.FlipHorizontally;
                Position += new Vector2(9, 0);
                _current = PlayerState.WALK;
            }
            if(InputManager.IsKeyPressed(Keys.W)
                || InputManager.IsButtonPressed(Buttons.A))
            {
                Position -= new Vector2(0, 125);
                _current = PlayerState.JUMP;
            }

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 128, 128);

            Position.Y += 4;

            #endregion

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            Camera Cam = Game.Services.GetService<Camera>();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.CurrentCamTranslation);
            switch(_current)
            {
                case PlayerState.STILL:
                    spriteBatch.Draw(Sprite.SpriteImage, Sprite.BoundingRect, Color.White);
                    break;
                case PlayerState.JUMP:
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
                case PlayerState.WALK:
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
                case PlayerState.FALL:
                    spriteBatch.Draw(Sprite.SpriteImage, Bounds, Color.White);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
