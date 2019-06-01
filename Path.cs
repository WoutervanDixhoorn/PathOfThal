using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class Path : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //My variables
        int size = 100;
		Map map;
        public Square rect3;

        public Player player;
        Camera camera;

        //THREADING EXPIRIMENT
        Thread t;
        

        //ONLY IN DEBUG, SHOW COLLISION AND TILENUMBER. PRESS BUTTON Q OR W
        #if DEBUG
        bool ShowCollision = false;
        bool ShowTileNumber = false;
        #endif

        public Path()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
			Window.AllowUserResizing = false; 
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();


        }

        protected override void Initialize()
        {   

            // TODO: Add your initialization logic here
            rect3 = new Square(size / 2, Color.White, 3);
            player = new Player(rect3, 2f);

            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.Instance.Load(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here
			MapParser mapParser = new MapParser();
            EventParser evetnParser = new EventParser();
			map = mapParser.Parse("MapExample.txt");
			Console.WriteLine(map.ToString());
            rect3.Load();
            map.Load();
            player.Load(map);


        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //InputHandling
            InputManger.Update();

            //Event handling
            EventHandler.Update(gameTime, player);

            //ONLY IN DEBUG, SHOW COLLISION AND TILENUMBER. PRESS BUTTON Q OR W
            #if DEBUG
            if(InputManger.IsKeyPressed(Keys.Q)){
                ShowCollision = !ShowCollision;
            }else if(InputManger.IsKeyPressed(Keys.Q)){
                ShowTileNumber = !ShowTileNumber;
            }
            #endif

            //PlayerHandling
            player.Update(gameTime);

            //Camera
            camera.Update(gameTime, this, GraphicsDevice.Viewport);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
			map.Draw(spriteBatch, 0, 0);
            
            //ONLY IN DEBUG, SHOW COLLISION AND TILENUMBER. PRESS BUTTON Q OR W
            #if DEBUG
            if(ShowCollision)
                map.DrawColisions(spriteBatch);
            if(ShowTileNumber)
                map.DrawTileNumbers(spriteBatch);
            #endif
            
            player.Draw(spriteBatch);
            spriteBatch.End();

            //event
            EventHandler.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
