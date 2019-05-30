using Microsoft.Xna.Framework.Input;

namespace PathOfThal
{
    public class InputManger
    {
        
        static KeyboardState currentKeyboardState;
        public static KeyboardState CurrentKeyboardState{
            get{
                return currentKeyboardState;
            }
        }
        static KeyboardState previousKeyboardState;

        public static bool IsKeyPressed(Keys key){
            return (currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key));
        }

        public static bool IsKeyDown(Keys key){
            return (currentKeyboardState.IsKeyDown(key));
        }

        public static bool IsKeyReleased(Keys key){
            return (currentKeyboardState.IsKeyUp(key)) && previousKeyboardState.IsKeyDown(key);
        }

        public static void Update()
        {
            // Get keyboard states
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

    }
}