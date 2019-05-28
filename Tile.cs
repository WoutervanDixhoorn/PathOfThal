using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PathOfThal
{
    public class Tile
    {
        Vector2 position;
        int type;
        Type tileType;

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
        }

        public int GetTileNumber(){
            return type;
        }

        public Type GetTileType(){
            return tileType;
        }

        public override string ToString(){
            if(tileType == SOLID)
                return "\'" + type;
            else{
                return " " + type;
            }
        }
    }
}