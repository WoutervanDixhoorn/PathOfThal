using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PathOfThal
{
    public class Camera
    {
        
        public Matrix Transform;
        Viewport viewport;
        Vector2 centre;

        public Camera(Viewport iViewport){
            viewport = iViewport;
        }

        public void Update(GameTime gameTime, Path path, Viewport iViewport){
            viewport = iViewport;

            centre = new Vector2(path.player.GetX() + (path.player.GetWidth() / 2) - (iViewport.Width / 2), 
                                    path.player.GetY() + (path.player.GetHeight() / 2) - (iViewport.Height / 2));
            Transform = Matrix.CreateScale(new Vector3(1,1,1)) *
                        Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0 ));

        }


    }
}