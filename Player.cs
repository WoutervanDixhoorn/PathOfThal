using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class Player
    {
        
        Square playerRect;
        public float speed;
        Vector2 dir;
        public Vector2 Position;
    
        Map map;

        public Player(Square rect, float iSpeed){
            playerRect = rect;
            speed = iSpeed;
            Position = Vector2.Zero;
        }

        public void Load(Map iMap){
            map = iMap;
        }

        public void Update(GameTime gameTime){
            KeyboardState state = Keyboard.GetState();
        
            dir = new Vector2(InputManger.IsKeyDown(Keys.Left) ? -1*speed : (InputManger.IsKeyDown(Keys.Right) ? 1*speed : 0*speed) , InputManger.IsKeyDown(Keys.Up) ? -1*speed: (InputManger.IsKeyDown(Keys.Down) ? 1*speed : 0*speed));;

            //Console.WriteLine("Pos: " + (int)Position.X/100 + " " + (int)Position.Y/100);
                
            //TODO: Fix pixel gap between colider and player
            if(dir.X > 0 && isCollidingRight(map)){
                dir.X = 0;
            }
            if(dir.X < 0 && isCollidingLeft(map)){
                dir.X = 0;
            }
            if(dir.Y < 0 && isCollidingTop(map)){
                dir.Y = 0;
            }
            if(dir.Y > 0 && isCollidingBottom(map)){
                dir.Y = 0;
            }

            Position += dir;
            //dir = Vector2.Zero;
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

        public Map GetCurrentMap(){
            return map;
        }

        //Collision Detection
        public bool isCollidingLeft(Map iMap){ 
            return ((iMap.getTile(GetX() + (int)dir.X, GetY()).GetTileType() == Tile.SOLID) || 
                    iMap.getTile(GetX() + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.SOLID);  
        }
        public bool isCollidingRight(Map iMap){
            return ((iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY()).GetTileType() == Tile.SOLID) || 
                    iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.SOLID);        }
        public bool isCollidingTop(Map iMap){
            return (iMap.getTile((GetX() + playerRect.Width), (GetY()) + (int)dir.Y).GetTileType() == Tile.SOLID) ||
                    iMap.getTile((GetX()), (GetY()) + (int)dir.Y).GetTileType() == Tile.SOLID;  
        }
        public bool isCollidingBottom(Map iMap){
            //Console.WriteLine((GetX() + playerRect.Width)/100 + " | " + ((GetY() + GetHeight() + (int)dir.Y)/100));
            //Console.WriteLine(iMap.getTile((GetX() + playerRect.Width), (GetY() + GetHeight() + (int)dir.Y)).GetTileType());
            return (iMap.getTile((GetX() + playerRect.Width), (GetY() + GetHeight() + (int)dir.Y)).GetTileType() == Tile.SOLID) ||
                    iMap.getTile((GetX()),GetY() + GetHeight() + (int)dir.Y).GetTileType() == Tile.SOLID;  
        }

        //Event Detection
        public string isEventLeft(Map iMap){
            string Event = ""; 

            if(iMap.getTile(GetX() + (int)dir.X, GetY()).GetTileType() == Tile.EVENT){
                Event = iMap.getTile(GetX() + (int)dir.X, GetY()).GetEventRef();
            }else if(iMap.getTile(GetX() + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.EVENT){
                Event = iMap.getTile(GetX() + (int)dir.X, GetY() + playerRect.Height).GetEventRef();
            }

            return Event;
        }
        public string isEventRight(Map iMap){
            string Event = "";

            if(iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY()).GetTileType() == Tile.EVENT){
                Event = iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY()).GetEventRef();
            }else if(iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY() + playerRect.Height).GetTileType() == Tile.EVENT){
                Event = iMap.getTile(GetX() + playerRect.Width + (int)dir.X, GetY() + playerRect.Height).GetEventRef();
            }
                     
            return Event;      }
        public string isEventTop(Map iMap){
            string Event = "";

            if(iMap.getTile((GetX() + playerRect.Width), (GetY()) + (int)dir.Y).GetTileType() == Tile.EVENT){
                Event = iMap.getTile((GetX() + playerRect.Width), (GetY()) + (int)dir.Y).GetEventRef();
            }else if(iMap.getTile((GetX()), (GetY()) + (int)dir.Y).GetTileType() == Tile.EVENT){
                Event = iMap.getTile((GetX()), (GetY()) + (int)dir.Y).GetEventRef();
            }

            return Event;  
        }
        public string isEventBottom(Map iMap){
            string Event  = "";

            if(iMap.getTile((GetX() + playerRect.Width), (GetY() + GetHeight() + (int)dir.Y)).GetTileType() == Tile.EVENT){
                Event = iMap.getTile((GetX() + playerRect.Width), (GetY() + GetHeight() + (int)dir.Y)).GetEventRef();
            }else if(iMap.getTile((GetX()),GetY() + GetHeight() + (int)dir.Y).GetTileType() == Tile.EVENT){
                Event = iMap.getTile((GetX()),GetY() + GetHeight() + (int)dir.Y).GetEventRef();
            }

            return Event; 
        }

        public string currentEvent(){
            string EventString = string.Empty;


            if(dir.X > 0 && isEventRight(map) != string.Empty){
                EventString = isEventRight(map);

            }
            if(dir.X < 0 && isEventLeft(map) != string.Empty){
                
                EventString = isEventLeft(map);

            }
            if(dir.Y < 0 && isEventTop(map) != string.Empty){
                
                EventString = isEventTop(map);

            }
            if(dir.Y > 0 && isEventBottom(map) != string.Empty){
                
                EventString = isEventTop(map);

            }


            return EventString;
        }
    }
}