using Microsoft.Xna.Framework.Content;

namespace PathOfThal
{
    public class ContentHandler
    {
        
        //Quick singleton
        private static ContentHandler instance;
        public static ContentHandler Instance{
            get{
                if(instance == null){
                    instance = new ContentHandler();
                }
                return instance;
            }
        }

        public ContentManager Content{private set; get;}

        public ContentHandler(){

        }

        public void Load(ContentManager iContent){
            this.Content = new ContentManager(iContent.ServiceProvider, "Content");
        }

    }
}