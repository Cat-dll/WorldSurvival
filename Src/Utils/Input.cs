using System;
using Microsoft.Xna.Framework.Input;

namespace WorldSurvival.Utils
{
    public enum MouseButton
    {
        Left,
        Mid,
        Right
    }

    public static class Input
    {
        private static KeyboardState _oldKeyboardState;

        private static KeyboardState _currentKeyboardState;

        private static MouseState _oldMouseState;

        private static MouseState _currentMouseState;

        public static int MouseX { get; private set; }

        public static int MouseY { get; private set; }

        public static Keys LastKey { get; private set; }

        public static void Update()
        {
            _oldKeyboardState = _currentKeyboardState;
            _oldMouseState = _currentMouseState;

            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();

            MouseX = _currentMouseState.X;
            MouseY = _currentMouseState.Y;

            // More efficient ?
            var k = Keys.None;
            if (_currentKeyboardState.GetPressedKeyCount() > 0)
                k = _currentKeyboardState.GetPressedKeys()[0];

            LastKey = k != LastKey ? k : LastKey; 
        }

        public static bool IsKeyDown(Keys k) => _currentKeyboardState.IsKeyDown(k);

        public static bool IsKeyReleased(Keys k) => _currentKeyboardState.IsKeyUp(k);

        public static bool IsKeyPressed(Keys k) => IsKeyDown(k) && !_oldKeyboardState.IsKeyDown(k);

        public static bool IsButtonDown(MouseButton b) => GetButtonState(b, _currentMouseState) == ButtonState.Pressed;

        public static bool IsButtonClick(MouseButton b) => GetButtonState(b, _currentMouseState) == ButtonState.Pressed &&
                                                           GetButtonState(b, _oldMouseState) == ButtonState.Released;

        public static bool IsButtonRelease(MouseButton b) => GetButtonState(b, _currentMouseState) == ButtonState.Released;

        public static float GetScrollWheelValue() => _currentMouseState.ScrollWheelValue;

        private static ButtonState GetButtonState(MouseButton b, MouseState state) =>
            b switch 
            {
                MouseButton.Left => state.LeftButton,
                MouseButton.Right => state.RightButton,
                MouseButton.Mid => state.MiddleButton,
                _ => throw new ArgumentException("Invalid mouse button!"),
            };
    }
}