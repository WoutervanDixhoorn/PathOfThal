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

        public void Draw(SpriteBatch spriteBatch,string Text, int x, int y, Color? iColor = null ){
            spriteBatch.DrawString(font, Text, new Vector2(x,y), iColor ?? Color.White);
        }

        public void Load(){
            content = new ContentManager(ContentHandler.Instance.Content.ServiceProvider, "Content");
            font = content.Load<SpriteFont>(fontPath);
        }

        public void Unload(){
            content.Unload();
        }

    }
}