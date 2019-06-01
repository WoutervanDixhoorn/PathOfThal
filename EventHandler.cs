using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PathOfThal
{
    public class EventHandler
    {
        
        public enum Type{
            DIALOG
        }

        static string currentEventString;
        static IEvent currentEvent;
        static bool running = false;
        public static EventParser parser = new EventParser();

        public EventHandler(){
        
        }

        public static void Update(GameTime gameTime,Player player){
            //Gametime is temp, i dont delete the event
            if(running == false){
                currentEventString = player.currentEvent();
            }
            //TODO: FIX EVENT PARSER
            if(currentEventString != string.Empty && running == false){
                running = true;
                try{
                    currentEvent = parser.ParseEvent(currentEventString + ".txt");
                }finally{
                    player.GetCurrentMap().RemoveEventRef(currentEventString);
                }

                currentEvent.LoadEvent();
            }
            
            Console.WriteLine(currentEventString);

            if(running){
                currentEvent.UpdateEvent(gameTime);
                if(currentEvent.Done()){
                    running = false;
                    currentEvent.UnloadEvent();
                    currentEventString = "";
                    currentEvent = null;
                    player.GetCurrentMap().ToString();
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
                currentEvent.DrawEvent(spriteBatch);
            }
        }

    }
}