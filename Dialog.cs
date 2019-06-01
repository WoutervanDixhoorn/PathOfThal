
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class Dialog : IEvent
    {
        
        string[] dialogText;
        Text text;
        bool visible = false;
        bool done = false;
        bool eventDone = false;
        int page;
        string displayText = "";
        string nextChar = "";
        int pageTime = 0;
        int letterSpeed = 3;
        int pageSpeed = 3;

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

        public void LoadEvent(){
            viewWidth = ContentHandler.Instance.Graphics.Viewport.Width;
            viewHeight = ContentHandler.Instance.Graphics.Viewport.Height;
            backGround = new Square(viewWidth, viewHeight/4, Color.Black);
            outline = new Square(viewWidth, (viewHeight/4) + 10, Color.Red);
            backGround.Load();
            outline.Load();
            text = new Text("CourierNewDialog");
            text.Load();
            visible = true;
        }

        public void UnloadEvent(){
            backGround.Unload();
            outline.Unload();
            text.Unload();
            
            eventDone = true;
            visible = false;
        }

        public void UpdateEvent(GameTime gameTime){
            //Update logic outside dialog

            int LetterTime = gameTime.TotalGameTime.Milliseconds;

            //Draw text letter by letter
            if(LetterTime % 100 <= letterSpeed && dialogText[page].Length > 0){
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
                if(pageTime > pageSpeed * 1000){
                    displayText = string.Empty;
                    page++;
                    pageTime = 0;
                }   
            }else if(page == dialogText.Length - 1 && dialogText[page].Length == 0){
                done = true;
            }

            //Space to speed text up
            if(InputManger.IsKeyDown(Keys.Space)){
                letterSpeed = 80;
                pageSpeed = 1;
            }else{
                letterSpeed = 3;
                pageSpeed = 3;
            }

            //Enter to go to next page
            if(InputManger.IsKeyDown(Keys.Enter) && page < dialogText.Length - 1){
                displayText = string.Empty;
                page++;
                pageTime = 0;
            }

            //Make dialog inviseble after al pages
            if(InputManger.IsKeyReleased(Keys.Enter) && done){
                UnloadEvent();
            }
        }

        public void DrawEvent(SpriteBatch iSpriteBatch){
            spriteBatch.Begin();

            if(visible){
                outline.Draw(spriteBatch, new Vector2(0,15));
                backGround.Draw(spriteBatch, new Vector2(0,20));
                text.Draw(spriteBatch,displayText,10,25);
            }

            spriteBatch.End();
        }

        public bool Done(){
            return eventDone;
        }

        public override string ToString(){
            string DialogS = string.Empty;
            foreach(string s in dialogText){
                DialogS += "\t\"" + s + '\"' + '\n';
            }

            return "[DIALOG]\n" + DialogS;
        }
        
    }
}