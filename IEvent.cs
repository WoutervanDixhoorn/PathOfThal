using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public interface IEvent
    {

        void LoadEvent();

        void UpdateEvent(GameTime gameTime);

        void UnloadEvent();

        void DrawEvent();
        void DrawEvent(SpriteBatch spriteBatch);

        bool Done();
    }
}