using System;
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
		Square rect0, rect1, rect2;
        public Square rect3;

        public Player player;
        Vector2 previousPlayerPos; 
        Vector2 Direction;
        bool ableToMove;
        Camera camera;

        //Dialog
        Dialog dialogTest;

        //ONLY IN DEBUG, SHOW COLLISION AND TILENUMBER. PRESS BUTTON Q OR W
        #if DEBUG
        bool ShowCollision = false;
        bool ShowTileNumber = false;
        #endif

        //InputHandling
        KeyboardState previousState;

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
            previousState = Keyboard.GetState();

            // TODO: Add your initialization logic here
            rect3 = new Square(size / 2, Color.White, 3);
            player = new Player(rect3, 2f);
            Direction = Vector2.Zero;
            previousPlayerPos = player.Position;
            ableToMove = true;

            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.Instance.Load(Content, GraphicsDevice);
            // TODO: use this.Content to load your game content here
			MapParser mapParser = new MapParser();
			map = mapParser.Parse("MapExample.txt");
			Console.WriteLine(map.ToString());
            dialogTest = new Dialog("Hallo ik ben wouter dit is een text om te kijken of het dialog system goed functioneerd.", "Hallo pagina 2");
            rect3.Load();
            map.Load();
            player.Load();
            dialogTest.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //InputHandling
            InputManger.Update();

            //ONLY IN DEBUG, SHOW COLLISION AND TILENUMBER. PRESS BUTTON Q OR W
            #if DEBUG
            if(InputManger.IsKeyPressed(Keys.Q)){
                ShowCollision = !ShowCollision;
            }else if(InputManger.IsKeyPressed(Keys.Q)){
                ShowTileNumber = !ShowTileNumber;
            }
            #endif

            //PlayerHandling
            player.Update(gameTime, map);
            dialogTest.Update(gameTime);

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

            dialogTest.Draw();

            base.Draw(gameTime);
        }
    }
}
