using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;
using System.Drawing;

namespace CalculatorEngine
{
    //manages global input and calls the given environment's input manager.
    class InputManager
    {
        /* PROPERTIES */
        private KeyboardState keyboardState { get; set; }
        private MouseState mouseState { get; set; }
        private bool _isWindowFocused = true;
        private bool _isMouseInWindow;
        private float _mouseWheelDelta;
        private Point _mousePosition;
        public bool isWindowFocused { get => _isWindowFocused; set => _isWindowFocused = value; }
        public bool isMouseInWindow { get => _isMouseInWindow; set => _isMouseInWindow = value; }
        public float mouseWheelDelta { get => _mouseWheelDelta; set => _mouseWheelDelta = value; }
        public Point mousePosition { get => _mousePosition; set => _mousePosition = value; }
        public InputData inputData { get; set; }
        private Display display { get; set; }

        /* CONSTRUCTORS */
        public InputManager(Display display) //needs a reference to the display so we can close it and call the display.PointToClient() method to get the mouse position.
        {
            this.display = display;
        }


        /* METHODS */

        //get input by updating the inputData data structure.
        private void GetInput()
        {
            //get mouse, keyboard states.
            this.keyboardState = Keyboard.GetState();
            this.mouseState = Mouse.GetCursorState();
            this.mousePosition = this.display.PointToClient(new Point(mouseState.X, mouseState.Y));

            //package the input information for use in other classes.
            inputData = new InputData(keyboardState, mouseState, mouseWheelDelta, mousePosition, isWindowFocused, isMouseInWindow);
        }

        //manage global keypresses: any environment must also allow for these keybindings.
        private void ManageGlobalKeyPresses()
        {
            //we cannot expect everyone that uses this to have quick fingers, so a key press is normally registered for multiple frame updates--resulting in multiple calculations for no reason.
            //avoid this issue by keeping track of the last key pressed.
            //Key lastKeyPressed = Key.Escape;

            //close the display: end the program.
            if (keyboardState.IsKeyDown(Key.Escape))
            {
                display.Close();
            }
        }

        //call the input manager method in the given environment.
        public void ManageInput(/*Environment environment*/)
        {
            //set the inputData class and call global and environment inputs only if the mouse is in the window and the window is focused.
            GetInput();
            //only manage input if the window is focused.
            if (isWindowFocused && isMouseInWindow)
            {
                ManageGlobalKeyPresses();
                //environment.ManageInput(inputData);
            }
        }
    }
}
