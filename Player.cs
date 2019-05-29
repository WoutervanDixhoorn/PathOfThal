using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class Player
    {
        
        Square playerRect;
        public int speed;
        Vector2 dir;
        public Vector2 Position;

        //TEMP INPUT HANDLING
        KeyboardState state;
        KeyboardState prevState;
        public Player(Square rect, int iSpeed){
            playerRect = rect;
            speed = iSpeed;
            Position = Vector2.Zero;
            state = Keyboard.GetState();
        }

        public void Load(){

        }

        public void Update(GameTime gameTime, Map iMap){
            KeyboardState state = Keyboard.GetState();
        
            dir = new Vector2(IsButtonDown(Keys.Left, state, prevState) ? -1*speed : (IsButtonDown(Keys.Right, state, prevState) ? 1*speed : 0*speed) , IsButtonDown(Keys.Up, state, prevState) ? -1*speed : (IsButtonDown(Keys.Down, state, prevState) ? 1*speed : 0*speed));;

            //Console.WriteLine("Pos: " + (int)Position.X/100 + " " + (int)Position.Y/100);

            //TODO: Collision needs to work from all directions
            //      And player glitches when coliding, need to handle the position
            if(dir.X < 0 && isCollidingLeft(iMap)){
                dir.X = 0;
            }
            if(dir.X > 0 && isCollidingRight(iMap)){
                dir.X = 0;
            }
            if(dir.Y > 0 && isCollidingBottom(iMap)){
                dir.Y = 0;
            }
            if(dir.Y < 0 && isCollidingTop(iMap)){
                dir.Y = 0;
            }

            Console.WriteLine(iMap.getTile((int)Position.X/100,(int)Position.Y/100));

            prevState = state;

            Position += dir;
            //dir = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch){
            playerRect.Draw(spriteBatch, Position);
        }

        public int GetX(){
            return (int)Position.X;
        }

        public int GetY(){
            return (int)Position.Y;
        }

        public int GetWidth(){
            return playerRect.Width;
        }

        public int GetHeight(){
            return playerRect.Height;
        }

        //Collision Detection
        public bool isCollidingLeft(Map iMap){ 
            return (iMap.getTile(GetX() + (int)dir.X, GetY()).GetTileType() == Tile.SOLID) || 
                    iMap.getTile(GetX() + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.SOLID;
            
        }
        public bool isCollidingRight(Map iMap){
            return (iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY()).GetTileType() == Tile.SOLID) || 
                    iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.SOLID;  
        }
        public bool isCollidingTop(Map iMap){
            return (iMap.getTile((GetX() + playerRect.Width), (GetY()) + (int)dir.Y).GetTileType() == Tile.SOLID) ||
                    iMap.getTile((GetX()), (GetY()) + (int)dir.Y).GetTileType() == Tile.SOLID;  
        }
        public bool isCollidingBottom(Map iMap){
            return (iMap.getTile((GetX() + playerRect.Width), (GetY() + GetHeight() + (int)dir.Y) + (int)dir.Y).GetTileType() == Tile.SOLID) ||
                    iMap.getTile((GetX()),GetY()+GetHeight()+(int)dir.Y).GetTileType() == Tile.SOLID;  
        }

        //TEMP INPUT HANDLING
        #region tempInput
        public bool IsButtonPressed(Keys key, KeyboardState state, KeyboardState previousState){
            return (state.IsKeyDown(key) && !previousState.IsKeyDown(key));
        }

        public bool IsButtonDown(Keys key, KeyboardState state, KeyboardState previousState){
            return (state.IsKeyDown(key));
        }

        public bool isButtonReleased(Keys key, KeyboardState state, KeyboardState previousState){
            return (state.IsKeyUp(key)) && previousState.IsKeyUp(key);
        }
        #endregion
    }
}