using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace CalculatorEngine
{
    //our wrappper for the GameWindow class.
    public sealed class Display : GameWindow
    {
        /* FIELDS AND PROPERTIES */

        //the display window:
        private static int _width = 1920;
        private static int _height = 1080;
        public static int width { get => _width; private set => _width = value; }
        public static int height { get => _height; private set => _height = value; }

        /* CONSTRUCTORS */
        public Display(int width, int height)
            : base
            (
                 width, //width
                 height, //height
                 GraphicsMode.Default, //GraphicsMode: can use a new GraphicsMode(bits of color, bits of depth buffer, stenciling, anti-aliasing).
                 "Calculator", //window name 
                 GameWindowFlags.Fullscreen, //windowed mode, fixed window, fullscreen. 
                 device: DisplayDevice.Default,
                 major: 4, //major version
                 minor: 5, //minor version
                 flags: GraphicsContextFlags.ForwardCompatible
             )
        {
            //extra initialization, if needed.
        }
    }
}
