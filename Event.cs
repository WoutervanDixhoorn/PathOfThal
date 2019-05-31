using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public class Event
    {   
        IEvent eventObject;
        bool run;

        public Event(IEvent iEvent){
            eventObject = iEvent;
        }

        public void Load(){
            eventObject.LoadEvent();
        }

        public void Run(){
            run = true;
        }

        public void Update(GameTime gameTime){
            if(run){
                eventObject.UpdateEvent(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch){
            eventObject.DrawEvent(spriteBatch);
        }

        public void Draw(){
            eventObject.DrawEvent();
        }

    }
}