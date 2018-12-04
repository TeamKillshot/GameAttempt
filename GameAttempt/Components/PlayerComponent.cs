using Components;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
        Body Body;
        World world;
        bool isCollided = false;

        public PlayerComponent(Game game): base(game)
        {
            GamePad.GetState(index);
            game.Components.Add(this);
            world = new World(new Vector2(0, 9.8f));
            tiles = new TRender(game);
        }

        public override void Initialize()
        {
            //player.world = new World(new Vector2(0, gravity));
            //player.Body = BodyFactory.CreateCircle(world, 1, 1);

            Position = new Vector2(200, 300);
            speed = 15;
            Body = BodyFactory.CreateBody(world, Position);
            Body.BodyType = BodyType.Dynamic;
            Position.X = Body.Position.X;
            Position.Y = Body.Position.Y;
            //Bounds = new Rectangle(Position.ToPoint(), new Point(64, 64));
            ID = (int)index;
            //previousPosition = Position;
            Sprite = Game.Content.Load<Texture2D>("Sprites/Mike");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            world.Step(5f);

            PlayerMove();

            foreach (Collider c in tiles.collisons)
            {
                if (Bounds.Intersects(c.collider) && isCollided == false)
                {
                    isCollided = true;
                    Body.IgnoreGravity = true;
                    Collision();
                }
                else if (Bounds.Y >= c.collider.Y)
                {
                    Body.IgnoreGravity = false;
                    Position = previousPosition;
                }

                if (isCollided == false)
                {
                    PlayerMove();
                }

                base.Update(gameTime);
            }
        }

        public void Collision()
        {
            Position.X = previousPosition.X;
            Position.Y = previousPosition.Y;
            Body.Position = Position;
            isCollided = false;
        }

        public void PlayerMove()
        {
            if (this != null /*&& player.isConnected == true*/)
            {
                Bounds = new Rectangle(Position.ToPoint(), new Point(64, 64));
                Position.X = Body.Position.X;
                Position.Y = Body.Position.Y;

                GamePadState state = GamePad.GetState(index);
                Body.ApplyForce(state.ThumbSticks.Left);

                if (InputManager.IsButtonPressed(Buttons.A))
                {
                    //player.Body.ApplyLinearImpulse(player.Jump);
                }
                previousPosition = Position;

                if (InputManager.IsKeyPressed(Keys.A))
                {
                    Body.ApplyForce(new Vector2(-200, 0) * speed); ;
                }
                if (InputManager.IsKeyHeld(Keys.D))
                {
                    Body.ApplyForce(new Vector2(200, 0) * speed); ;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();
            //Camera Cam = Game.Services.GetService<Camera>();

            spriteBatch.Begin(/*SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Cam.CurrentCamTranslation*/);
            spriteBatch.Draw(Sprite, Position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
