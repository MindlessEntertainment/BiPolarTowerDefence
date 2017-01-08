using Microsoft.Xna.Framework.Input;

namespace BiPolarTowerDefence.Utilities
{
    public class Input
    {
        private static Input _input;
        public KeyboardState PreviousKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }

        private Input()
        {
            PreviousKeyboardState = Keyboard.GetState();
            CurrentKeyboardState = Keyboard.GetState();
        }

        public static Input Instance()
        {
            return _input ?? (_input = new Input());
        }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool PressedKey(Keys key)
        {
            return PreviousKeyboardState.IsKeyUp(key) && CurrentKeyboardState.IsKeyDown(key);
        }
    }
}