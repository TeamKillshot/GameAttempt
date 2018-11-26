using GameAttempt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TileEngine.TileEngine;

namespace TileEngine
{
	
	public class TRender : DrawableGameComponent
	{
		#region Properties
		TManager tileManager;
		Texture2D tSheet;
		List<Collider> collisons = new List<Collider>();
		int tsWidth;						// gets the width of tSheet
		int tsHeight;						// gets teh height of tSheet
		int tsRows = 12;					// how many sprites in a column
		int tsColumns = 22;					// how many Sprites in a Row
		List<TRef> tRefs = new List<TRef>();
		int[,] tileMap = new int[,]
		{
			
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  },
			{   1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  1,  0,  0,  0,  1,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  1,  1,  1,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },
			{   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  },

		};
		
		#endregion 

		public TRender(Game game) : base(game)
		{

			game.Components.Add(this);
			tileManager = new TManager();
			tSheet = Game.Content.Load<Texture2D>
									  ("Sprites/tank tiles 64 x 64");    // get TileSheet

			// create a new tile from the TileSheet in list (locX, locY, IndexNum)
			tRefs.Add(new TRef(6, 4, 0));	// blank space
			tRefs.Add(new TRef(9, 4, 1));	// Ground with grass
			tRefs.Add(new TRef(4, 3, 2));	// Ground 
            //tRefs.Add(new TRef(0, 0, 3));   // Player
			

			string[] tNames = { "Empty", "Ground1", "Ground2" /*"Player"*/}; // names of tiles
			string[] impassableTiles = { "Ground1" /*"Player"*/ };

			tsWidth = tSheet.Width / tsColumns;					// gets Width of tiles
			tsHeight = tSheet.Height / tsRows;					// gets Height of tiles

			// creates Layer of Ground
			tileManager.addLayer("Background", tNames, 
								 tileMap, tRefs, tsWidth, tsHeight);

			// sets Ground as Active Layer
			tileManager.ActiveLayer = tileManager.GetLayer("Background");

			// Creates a set of impassable tiles
			tileManager.ActiveLayer.makeImpassable(impassableTiles);

			// sets the current tile
			tileManager.CurrentTile = tileManager.ActiveLayer.Tiles[0, 0];

			//Sets Collison tiles
			SetupCollison();
		}

		public void SetupCollison()
		{
			foreach (Tile t in tileManager.ActiveLayer.Impassable)
			{
				collisons.Add(new Collider(Game.Content.Load<Texture2D>("Sprites/Collison"),
							  new Vector2(t.X * t.TileWidth, t.Y * t.TileHeight), 
							  new Vector2(t.TileWidth, t.TileHeight)));
			}

		}
	
	
		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null);

			foreach (Tile t in tileManager.ActiveLayer.Tiles)
			{
				Vector2 position = new Vector2(t.X * t.TileWidth, 
											   t.Y * t.TileHeight);

				spriteBatch.Draw(tSheet, new Rectangle(position.ToPoint(),
													   new Point(t.TileWidth,
													   t.TileHeight)),
													   
										 
										 new Rectangle(t.TRefs.TLocX * t.TileWidth,
													   t.TRefs.TLocY * t.TileHeight,
													   t.TileWidth,
													   t.TileHeight),
										 Color.White);
				
			}
			foreach (var item in collisons)
				item.draw(spriteBatch);
			

			spriteBatch.End();
			base.Draw(gameTime);
		}
		
	}
}
