using Components;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileEngine;

namespace GameAttempt.Components
{
    public sealed class PlayerAttempt : GameComponent
    {
        #region Properties and Variables

        public PlayerIndex index;
        public Vector2 Position;
        public Rectangle Bounds;
        //Player player;
        public Vector2 previousPosition;
        public Vector2 nextPosition;
        public Vector2 Jump = new Vector2(0, 15);
        public Texture2D Sprite { get; set; }
        public string Name { get; set; }
        public Body Body { get; set; }
        Matrix Matrix { get; set; }
        float gravity = 9.8f;
        World world;

        public List<PlayerAttempt> playerList = new List<PlayerAttempt>();

        //public bool isConnected = false;
        public bool isColliding = false;

        private int speed = 120;
        #endregion

        public PlayerAttempt(Game _game)
            : base(_game)
        {
            GamePad.GetState(index);
            _game.Components.Add(this);
        }

        public void GetPlayerPosition(PlayerAttempt player)
        {
            player.world = new World(new Vector2(0, gravity));

            player.Position = new Vector2(200, 300);
            player.Body = BodyFactory.CreateCircle(world, 1, 1);
            //player.Body.Restitution = 1f;
            //player.Body.Mass = 0.1f;
            player.Body.BodyType = BodyType.Dynamic;

            player.Position.X = player.Body.Position.X;
            player.Position.Y = player.Body.Position.Y;
        }

        public void GetPlayerIndex(PlayerAttempt player)
        {
            #region Check GameStates
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            GamePadState state2 = GamePad.GetState(PlayerIndex.Two);
            GamePadState state3 = GamePad.GetState(PlayerIndex.Three);
            GamePadState state4 = GamePad.GetState(PlayerIndex.Four);


            if (player.Name == "Player1" && state.IsConnected)
            {
                player.index = PlayerIndex.One;
                //player.isConnected = true;
            }
            else if (player.Name == "Player2" && state2.IsConnected)
            {
                player.index = PlayerIndex.Two;
                //player.isConnected = true;
            }
            else if (player.Name == "Players3" && !state.IsConnected)
            {
                player.index = PlayerIndex.Three;
                //player.isConnected = true;
            }
            else if (player.Name == "Player4" && !state.IsConnected)
            {
                player.index = PlayerIndex.Four;
                //player.isConnected = true;
            }
            #endregion
        }

        public void Update(GameTime gameTime, List<PlayerAttempt> playerList, TRender tiles)
        {
            #region Player1 Controller


            //if (InputManager.IsButtonPressed(Buttons.DPadRight))
            //{
            //    player.Position.X += speed;
            //}
            //if (InputManager.IsButtonPressed(Buttons.DPadLeft))
            //{
            //    player.Position.X -= speed;
            //}

            #endregion

            foreach (PlayerAttempt player in playerList)
            {
                player.world.Step(5f);
                player.nextPosition.Y = player.Position.Y += speed;
                player.nextPosition.X = player.Position.X;

                foreach (Collider c in tiles.collisons)
                {
                    if (player.Bounds.Intersects(c.collider)
                        && player.isColliding == false)
                    {
                        player.isColliding = true;
                        player.Body.IgnoreGravity = true;
                        Collision(player);
                    }
                    else if(player.Bounds.Y >= c.collider.Y)
                    {
                        player.Body.IgnoreGravity = false;
                        player.Position = player.previousPosition;
                    }
                }

                if (player.isColliding == false)
                {
                    PlayerMovement(player);
                }
            }
        }

        public unsafe void PlayerMovement(PlayerAttempt player)
        {
            if (player != null /*&& player.isConnected == true*/)
            {
                player.previousPosition = player.Position;
                player.Bounds = new Rectangle(player.nextPosition.ToPoint(), new Point(64, 64));

                player.Position.X = player.Body.Position.X;
                player.Position.Y = player.Body.Position.Y;

                GamePadState state = GamePad.GetState(player.index);
                player.Body.ApplyForce(state.ThumbSticks.Left * speed);

                if(InputManager.IsButtonPressed(Buttons.A))
                {
                    player.Body.ApplyLinearImpulse(player.Jump);
                }

                if (InputManager.IsKeyPressed(Keys.A))
                {
                    player.Body.ApplyForce(new Vector2(-200, 0) * speed);
                }
                if (InputManager.IsKeyHeld(Keys.D))
                {
                    player.Body.ApplyForce(new Vector2(200, 0) * speed);
                }

            }
        }

        public void Collision(PlayerAttempt player)
        {
            if (player.isColliding == true)
            {
                player.Position.X = player.previousPosition.X;
                player.Position.Y = player.previousPosition.Y;
                player.Body.Position = player.Position;
                player.isColliding = false;
            }
        }

        public void Draw(SpriteBatch spritebatch, List<PlayerAttempt> playerList)
        {
            spritebatch.Begin();
            foreach (PlayerAttempt player in playerList)
            {
                if (player.Sprite != null /*&& player.isConnected == true*/)
                {
                    spritebatch.Draw(player.Sprite, player.Bounds, Color.White);
                }
            }
            spritebatch.End();
        }
    }
}
