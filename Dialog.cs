using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public class Dialog
    {
        
        string[] dialogText;
        Text text;
        bool visible;
        int page;

        //Screen info
        int viewWidth;
        int viewHeight;

        //Look
        Square backGround;
        Square outline;

        private SpriteBatch spriteBatch;

        public Dialog(params string[] pageText){
            spriteBatch = new SpriteBatch(ContentHandler.Instance.Graphics);
            dialogText = pageText;
            page = 0;
        }

        public void Load(){
            viewWidth = ContentHandler.Instance.Graphics.Viewport.Width;
            viewHeight = ContentHandler.Instance.Graphics.Viewport.Height;
            backGround = new Square(viewWidth, viewHeight/4, Color.Black);
            outline = new Square(viewWidth, (viewHeight/4) + 10, Color.Red);
            backGround.Load();
            outline.Load();
            text = new Text("CourierNewDialog");
            text.Load();
        }

        public void Update(){

        }

        public void Draw(){
            spriteBatch.Begin();
            outline.Draw(spriteBatch, new Vector2(0,15));
            backGround.Draw(spriteBatch, new Vector2(0,20));
            
            text.Draw(spriteBatch,dialogText[page],10,25);

            spriteBatch.End();
        }
    }
}