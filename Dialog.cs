using System.Linq;
using System;
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
        string displayText = "";
        string nextChar = "";
        int pageTime = 0;

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

        public void Update(GameTime gameTime){
            int LetterTime = gameTime.TotalGameTime.Milliseconds;


            //Draw text letter by letter
            if(LetterTime % 100 <= 3 && dialogText[page].Length > 0){
                nextChar = dialogText[page].Substring(0,1);
                dialogText[page] =  dialogText[page].Remove(0,1);
                displayText += nextChar;

                //Wrap text
                //40 is a temp value, if not put there '\n' would kept adding. Need to fix this some how. 
                //Only able to print 2 lines right now
                //120 is a temp value, as long as it work will keep it there
                if(text.GetSpriteFont().MeasureString(displayText).X > viewWidth - 120 && text.GetSpriteFont().MeasureString(displayText).Y < 40){
                    
                    //check for closest space, dot or comma
                    if(nextChar == " " || nextChar == "," || nextChar == "."){
                        displayText += '\n';
                    }
                }
            }

            //change page. Temp, want the player to do it by him self or after like 10 sec
            if(dialogText[page].Length == 0 && page < dialogText.Length - 1){
                pageTime += gameTime.ElapsedGameTime.Milliseconds;
                if(pageTime > 3000){
                    displayText = string.Empty;
                    page++;
                    pageTime = 0;
                }   
            }

            //Space to speed text up
            if(true){

            }

            //Enter to go to next page
            if(true){

            }
        }

        public void Draw(){
            spriteBatch.Begin();
            outline.Draw(spriteBatch, new Vector2(0,15));
            backGround.Draw(spriteBatch, new Vector2(0,20));
            
            text.Draw(spriteBatch,displayText,10,25);

            spriteBatch.End();
        }
    }
}