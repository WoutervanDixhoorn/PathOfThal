using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public class Player
    {
        
        Square playerRect;
        int speed;
        public Vector2 Position;

        public Player(Square rect, int iSpeed){
            playerRect = rect;
            speed = iSpeed;
            Position = Vector2.Zero;
        }

        public void Load(){

        }

        public void Update(GameTime gameTime, Vector2 dir){
            Position += speed * dir;
        }

        public void Draw(SpriteBatch spriteBatch){
            playerRect.DrawBorder(spriteBatch, Position);
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

        public bool isColliding(Map iMap){
            Console.WriteLine( "X: " + (GetX() + playerRect.Width) + " Y: " + (GetY() + playerRect.Height));
            return iMap.getTile(GetX() + playerRect.Width - 5, GetY()).GetTileType() == Tile.SOLID;
           
        }
    }
}