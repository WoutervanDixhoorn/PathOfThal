using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public class Text
    {
        
        SpriteFont font;
        string fontPath;
        ContentManager content;
         
        public Text(string iFontname){
            fontPath = iFontname;   
        }

        public void Draw(SpriteBatch spriteBatch,string Text, int x, int y, int iScale, Color? iColor = null){
            spriteBatch.DrawString(font, Text, new Vector2(x,y) + new Vector2(1 * iScale, 1 * iScale), iColor ?? Color.White, 0 , Vector2.Zero, iScale, SpriteEffects.None, 1f);
        }

        public void Draw(SpriteBatch spriteBatch,string Text, int x, int y, Color? iColor = null){
            spriteBatch.DrawString(font, Text, new Vector2(x,y), iColor ?? Color.White);
        }

        public void DrawBordered(SpriteBatch spriteBatch, string Text, int x, int y, Color? iColor = null,Color? iBorderColor = null, float iScale = 1){
            //Vector2 origin = new Vector2(font.MeasureString(Text).X/2, font.MeasureString(Text).Y /2); 
            Vector2 origin = new Vector2(-4,-4);

            //Displace text for line
            spriteBatch.DrawString(font, Text, new Vector2(x,y) + new Vector2(1 * iScale, 1 * iScale), iBorderColor ?? Color.Black,0,origin,iScale,SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, Text, new Vector2(x,y) + new Vector2(-1 * iScale, -1 * iScale), iBorderColor ?? Color.Black,0,origin,iScale,SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, Text, new Vector2(x,y) + new Vector2(-1 * iScale, 1 * iScale), iBorderColor ?? Color.Black,0,origin,iScale,SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, Text, new Vector2(x,y) + new Vector2(1 * iScale, -1 * iScale), iBorderColor ?? Color.Black,0,origin,iScale,SpriteEffects.None, 1f);

            //Draw the text above displacement
            spriteBatch.DrawString(font, Text, new Vector2(x,y), iColor ?? Color.White,0,origin,iScale,SpriteEffects.None, 1f);
        }

        public void Load(){
            content = new ContentManager(ContentHandler.Instance.Content.ServiceProvider, "Content");
            font = content.Load<SpriteFont>(fontPath);
        }

        public void Unload(){
            content.Unload();
        }

        public SpriteFont GetSpriteFont(){
            return font;
        }

    }
}