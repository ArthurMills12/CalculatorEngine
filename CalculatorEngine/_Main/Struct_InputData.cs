using System.Drawing;
using OpenTK;
using OpenTK.Input;

namespace CalculatorEngine
{
    //neatly packages input data so that it can easily be passed to classes that need to know the current state of the mouse/keyboard.
    struct InputData
    {
        /* PROPERTIES */
        private KeyboardState _keyboardState;
        private MouseState _mouseState;
        private float _mouseWheelDelta;
        private Vector2 _mousePositionGLCoordinates;
        private bool _isWindowFocused;
        private bool _isMouseInWindow;

        public KeyboardState KeyboardState { get => _keyboardState; set => _keyboardState = value; }
        public MouseState MouseState { get => _mouseState; set => _mouseState = value; }
        public float MouseWheelDelta { get => _mouseWheelDelta; set => _mouseWheelDelta = value; }
        public Vector2 MousePositionGLCoordinates { get => _mousePositionGLCoordinates; set => _mousePositionGLCoordinates = value; }
        public bool IsWindowFocused { get => _isWindowFocused; set => _isWindowFocused = value; }
        public bool IsMouseInWindow { get => _isMouseInWindow; set => _isMouseInWindow = value; }

        /* CONSTRUCTORS */
        public InputData(KeyboardState keyboardState, MouseState mouseState, float mouseWheelDelta, Point mousePositionGLCoordinates, bool isWindowFocused, bool isMouseInWindow)
        {
            _keyboardState = keyboardState;
            _mouseState = mouseState;
            _mouseWheelDelta = mouseWheelDelta;
            _mousePositionGLCoordinates = new Vector2(mousePositionGLCoordinates.X, mousePositionGLCoordinates.Y);
            _isWindowFocused = isWindowFocused;
            _isMouseInWindow = isMouseInWindow;
        }
    }
}