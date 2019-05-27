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
			rect0 = new Square(size,Color.Yellow, 5);
			rect1 = new Square(size,Color.Blue, 5);
            rect2 = new Square(size, Color.BlueViolet, 5);
            rect3 = new Square(size, Color.White, 7);

            player = new Player(rect3, 5);
            Direction = Vector2.Zero;
            previousPlayerPos = player.Position;
            ableToMove = true;

            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
			MapParser mapParser = new MapParser();
			map = mapParser.Parse("MapExample.txt");
			Console.WriteLine(map.ToString());

			rect0.Load(GraphicsDevice);
			rect1.Load(GraphicsDevice);
            rect2.Load(GraphicsDevice);
            rect3.Load(GraphicsDevice);

            player.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //InputHandling
            KeyboardState state = Keyboard.GetState();
           //TODO: ANYTHING THAT USES INPUT IN HERE!!         
            previousState = state;

            //PlayerHandling
            player.Update(gameTime, map);

            //Camera
            camera.Update(gameTime, this, GraphicsDevice.Viewport);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
			map.Draw(spriteBatch, 0, 0,rect0, rect1, rect2);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool IsButtonPressed(Keys key, KeyboardState state){
            return (state.IsKeyDown(key) && !previousState.IsKeyDown(key));
        }

        public bool IsButtonDown(Keys key, KeyboardState state){
            return (state.IsKeyDown(key));
        }

        public bool isButtonReleased(Keys key, KeyboardState state){
            return (state.IsKeyUp(key)) && previousState.IsKeyUp(key);
        }
    }
}
