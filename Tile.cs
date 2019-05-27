using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace PathOfThal
{
    public class Tile
    {
        Vector2 position;
        int type;

        public Tile(int iType, int x, int y){
            type = iType;
            position = new Vector2(x,y);
        }

        public int GetType(){
            return type;
        }
    }
}