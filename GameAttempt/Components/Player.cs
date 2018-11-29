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
    public sealed class Player : GameComponent
    {
        #region Properties and Variables

        public PlayerIndex index;
        public Vector2 Position;
        public Rectangle Bounds;
        //Player player;
        public Vector2 previousPosition;
        public Texture2D Sprite { get; set; }
        public string Name { get; set; }
        public Body Body { get; set; }
        Matrix Matrix { get; set; }
        float gravity = 9.8f;
        World world;

        public List<Player> playerList = new List<Player>();

        //public bool IsConnected = false;
        public bool isColliding = false;
        public bool canMove = true;

        private int speed = 25;
        #endregion

        public Player(Game _game)
            : base(_game)
        {
            GamePad.GetState(index);
            _game.Components.Add(this);
        }

        public void GetPlayerPosition(Player player)
        {
            player.world = new World(new Vector2(0, gravity));

            player.Position = new Vector2(200, 200);
            player.Body = BodyFactory.CreateCircle(world, 1, 1);
            player.Body.Restitution = 1f;
            //player.Body.Mass = 1f;
            player.Body.BodyType = BodyType.Dynamic;

            player.Position.X = player.Body.Position.X;
            player.Position.Y = player.Body.Position.Y;
        }

        public void GetPlayerIndex(Player player)
        {
            #region Check GameStates
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            GamePadState state2 = GamePad.GetState(PlayerIndex.Two);
            GamePadState state3 = GamePad.GetState(PlayerIndex.Three);
            GamePadState state4 = GamePad.GetState(PlayerIndex.Four);
            

            if (player.Name == "Player1" && state.IsConnected)
            {
                player.index = PlayerIndex.One;
                //player.IsConnected = true;
            }
            else if (player.Name == "Player2" && state2.IsConnected)
            {
                player.index = PlayerIndex.Two;
                //player.IsConnected = true;
            }
            else if (player.Name == "Players3" && !state.IsConnected)
            {
                player.index = PlayerIndex.Three;
                //player.IsConnected = true;
            }
            else if (player.Name == "Player4" && !state.IsConnected)
            {
                player.index = PlayerIndex.Four;
                //player.IsConnected = true;
            }
            #endregion
        }

        public void Update(GameTime gameTime, List<Player> playerList, TRender tiles)
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

            foreach(Player player in playerList)
            {
                player.world.Step(1f);

                foreach (Collider c in tiles.collisons)
                {
                    if (player.Bounds.Intersects(c.GetCollidingRectangle()) && player.isColliding == false)
                    {
                        player.canMove = false;
                        player.isColliding = true;
                        Collision(player);
                    }
                    else
                    {
                        player.isColliding = false;
                        player.canMove = true;
                    }
                }

                if(player.isColliding == false && player.canMove == true)
                {
                    PlayerMovement(player);
                }
            }
        }

        public unsafe void PlayerMovement(Player player)
        {
            if (player != null /*&& player.IsConnected == true*/)
            {
                player.previousPosition = player.Position;
                player.Bounds = new Rectangle(player.Position.ToPoint(), new Point(88, 88));

                player.Position.X = player.Body.Position.X;
                player.Position.Y = player.Body.Position.Y;

                GamePadState state = GamePad.GetState(player.index);
                player.Body.ApplyForce(state.ThumbSticks.Left * speed);

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


        public void Collision(Player player)
        {
            if (player.isColliding == true)
            {
                player.Position = player.previousPosition;
                player.isColliding = false;
                player.canMove = true;
            }
            else
            {
                player.isColliding = false;
                player.canMove = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spritebatch, List<Player> playerList)
        {
            //spritebatch.Begin();
            foreach (Player player in playerList)
            {
                if (player.Sprite != null /*&& player.IsConnected == true*/)
                {
                    spritebatch.Draw(player.Sprite, player.Bounds, Color.White);
                }
            }
            //spritebatch.End();
        }
    }
}
