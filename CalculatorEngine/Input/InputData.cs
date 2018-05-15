using System.Drawing;
using OpenTK;
using OpenTK.Input;

namespace CalculatorEngine
{
    //neatly packages input data so that it can easily be passed to classes that need to know the current state of the mouse/keyboard.
    class InputData
    {
        /* PROPERTIES */
        private KeyboardState _keyboardState;
        private MouseState _mouseState;
        private float _mouseWheelDelta;
        private Vector2 _mousePositionGLCoordinates;
        private bool _isWindowFocused;
        private bool _isMouseInWindow;

        public KeyboardState keyboardState { get => _keyboardState; set => _keyboardState = value; }
        public MouseState mouseState { get => _mouseState; set => _mouseState = value; }
        public float mouseWheelDelta { get => _mouseWheelDelta; set => _mouseWheelDelta = value; }
        public Vector2 mousePositionGLCoordinates { get => _mousePositionGLCoordinates; set => _mousePositionGLCoordinates = value; }
        public bool isWindowFocused { get => _isWindowFocused; set => _isWindowFocused = value; }
        public bool isMouseInWindow { get => _isMouseInWindow; set => _isMouseInWindow = value; }

        /* CONSTRUCTORS */
        public InputData(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePositionGLCoordinates, bool isWindowFocused, bool isMouseInWindow)
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
            this.mouseWheelDelta = mouseWheelDelta;
            this.mousePositionGLCoordinates = new Vector2(mousePositionGLCoordinates.X, mousePositionGLCoordinates.Y);
            this.isWindowFocused = isWindowFocused;
            this.isMouseInWindow = isMouseInWindow;
        }
    }
}