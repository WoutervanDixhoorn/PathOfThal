using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class InputManger
    {
        
        static KeyboardState currentState;
        public static KeyboardState CurrentKeyboardState{
            get{
                return currentState;
            }
        }
        static KeyboardState previousState;

        

    }
}