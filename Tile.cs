using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PathOfThal
{
    public class Tile
    {
        Vector2 position;
        int type;
        float Alpha;
        Type tileType;
        Square tileSquare;

        public int X{
            get{
                return (int)position.X;
            }
            set{
                position.X = value;
            }
        }

        public int Y{
            get{
                return (int)position.Y;
            }
            set{
                position.Y = value;
            }
        }

        public enum Type{
            NONE,
            TILE,
            SOLID
        }

        #region Type getters
        public static Type NONE{
            get{
                return Type.NONE;
            }
        }

        public static Type SOLID{
            get{
                return Type.SOLID;
            }
        }

        public static Type TILE{
            get{
                return Type.TILE;
            }
        }

        #endregion

        public Tile(int iType, int x, int y, Type iTileType = Type.TILE){
            type = iType;
            tileType = iTileType;
            position = new Vector2(x,y);

            //Declare tile color
            //TODO: There is probably a better way to find the color
            Color tileColor = Color.White;
            switch(iType){
                case 1:
                    tileColor = Color.Blue;
                    Alpha = 1f;
                    break;
                case 2:
                    tileColor = Color.White;
                    Alpha = 0.8f;
                    break;
                default:
                    iTileType = Type.NONE;
                    Alpha = 0f;
                    break;
            }

            tileSquare = new Square(100,100,tileColor);

        }

        public void Load(){
            tileSquare.Load();
        }

        public void Draw(SpriteBatch spriteBatch){
            //Console.WriteLine(ToString() + " | " + spriteBatch);
            tileSquare.Draw(spriteBatch, position, Alpha);
        }

        public int GetWidth(){
            return tileSquare.Width;
        }

        public int GetHeight(){
            return tileSquare.Height;
        }

        public int GetTileNumber(){
            return type;
        }

        public Type GetTileType(){
            return tileType;
        }

        public override string ToString(){
                return " " + tileType;
        }
    }
}