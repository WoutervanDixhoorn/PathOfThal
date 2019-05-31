using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class Square
    {

        Texture2D rect, border;
        int width;
        int height;
        int borderSize = 0;
        Color[] squareData;
        Color[] borderData;
        Color color;
        Color borderColor;

        float alpha;
        public int Width{
            get{
                return width;
            }
        }

        public int Height{
            get{
                return height;
            }
        }

        public Square(int iWidth, int iHeight, Color iColor, int iBorderSize = 0, Color? iBorderColor = null){
            width = iWidth;
            height = iHeight;
            color = iColor;
            borderSize = iBorderSize;
            borderColor = iBorderColor ?? iColor;
        }

        public Square(int size, Color iColor, int iBorderSize = 0, Color? iBorderColor = null){
            width = size;
            height = size;
            color = iColor;
            borderSize = iBorderSize;
            borderColor = iBorderColor ?? iColor;
        }


        //TODO: FIX THE LOAD CODE COLOR DATA GET'S OVERWRITEN FOR SOME REASON!!!!
        public void Load(){
            //GraphicsDevice newGraphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());

            squareData = new Color[(width*height)];
            borderData = new Color[(width*height)];

            //Load square
            for(int i = 0; i < squareData.Length; i++){squareData[i] = color;}

            //Load Border
            for(int i = 0; i < height; i++){
                for(int j = 0; j < width; j++){
                    if(j >= i*width - borderSize || j <= borderSize){
                        borderData[i*width+j] = borderColor;
                    }else if(j >= width - borderSize){
                        borderData[i*width+j] = borderColor;
                    }else if(i >= height - borderSize || i <= borderSize){
                        borderData[i*width+j] = borderColor;
                    }                 
                }
            }

            rect = new Texture2D(ContentHandler.Instance.Graphics, width, height);
            border = new Texture2D(ContentHandler.Instance.Graphics, width, height);
            rect.SetData(squareData);
            border.SetData(borderData);
        }
        
        public void Unload(){
            rect.Dispose();
            border.Dispose();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 iPosition, float iAlpha = 1){
            spriteBatch.Draw(rect, iPosition, Color.White * iAlpha);
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y, float iAlpha = 1){
            spriteBatch.Draw(rect, new Vector2(x,y), Color.White * iAlpha);          
        }

        public void DrawBorder(SpriteBatch spriteBatch, int x, int y, float iAlpha = 1){
            spriteBatch.Draw(border, new Vector2(x,y), Color.White * iAlpha);
        }

        public void DrawBorder(SpriteBatch spriteBatch, Vector2 iPosition, float iAlpha = 1){
            spriteBatch.Draw(border, iPosition, Color.White * iAlpha);
        }

        public void setHeight(int iHeight){
            height = iHeight;
        }

        public void setWidth(int iWidth){
            width = iWidth;
        }

        public void setDimenions(int iWidth, int iHeight){
            width = iWidth;
            height = iHeight;
        }

        public void setDimenions(int size){
            width = size;
            height = size;
        }

        public override string ToString(){
            return "[Rect]\n" + "Width: " + width + "\nHeight: " + height + "\nColor: " + color;
        }
    }
}