using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PathOfThal
{
    public class EventHandler
    {
        
        public enum Type{
            DIALOG
        }

        static IEvent currentEvent;
        static bool running = false;

        public EventHandler(){

        }

        public static void Update(GameTime gameTime,Player player){
            //Gametime is temp, i dont delete the event
            if(running == false && gameTime.TotalGameTime.Seconds < 10){
                currentEvent = player.currentEvent();
            }

            if(currentEvent != null && running == false){
                running = true;
                currentEvent.LoadEvent();
            }

            if(running){
                currentEvent.UpdateEvent(gameTime);
                if(currentEvent.Done()){
                    running = false;
                    currentEvent.UnloadEvent();
                    currentEvent = null;
                }
            }

        }

        public static void Load(){
            currentEvent.LoadEvent();
        }

        public static void Unload(){
            currentEvent.UnloadEvent();
        }

        public static void Draw(SpriteBatch spriteBatch){
            if(currentEvent != null){
                try{
                    currentEvent.DrawEvent();
                }finally{
                    currentEvent.DrawEvent(spriteBatch);
                }
            }
        }

    }
}