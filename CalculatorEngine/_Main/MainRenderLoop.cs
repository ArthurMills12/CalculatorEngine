using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System.Drawing;

namespace CalculatorEngine
{
    //where the rendering logic goes: actually runs the program here.
    class MainRenderLoop
    {
        /* PROPERTIES */

        //display:
        private Display display { get; set; }
        private int _width;
        private int _height;
        private int _framesPerSecond;
        public int width { get => _width; private set => _width = value; }
        public int height { get => _height; private set => _height = value; }
        public int framesPerSecond { get => _framesPerSecond; private set => _framesPerSecond = value; }

        //input:
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

        //rendering:
        Loader loader;
        MasterRenderer masterRenderer;
        Camera camera;


        //temporary:
        private List<Function> functions;
        private List<Entity> entities;
        private List<Vector3> temporary;


        /* CONSTRUCTORS */
        public MainRenderLoop(int windowWidth, int windowHeight, int framesPerSecond)
        {
            //initialization:
            this.width = windowWidth;
            this.height = windowHeight;
            this.framesPerSecond = framesPerSecond;
            this.display = new Display(windowWidth, windowHeight);

            //subscribe to display events:
            //manage window and rendering:
            this.display.Load += OnLoad;
            this.display.Resize += OnResize;
            this.display.UpdateFrame += OnUpdateFrame;
            this.display.RenderFrame += OnRenderFrame;
            this.display.Closed += OnClosed;

            //manage input:
            this.display.FocusedChanged += OnFocusedChanged;
            this.display.MouseEnter += OnMouseEnter;
            this.display.MouseLeave += OnMouseLeave;
            this.display.MouseWheel += OnMouseWheel;

            //run the dislay:
            this.display.Run(framesPerSecond);
        }


        /* EVENT METHODS */

        //managing the window and rendering:

        //when the window starts.
        private void OnLoad(object sender, EventArgs e)
        {
            //initialize:
            loader = new Loader();
            masterRenderer = new MasterRenderer();
            camera = new Camera();

            Transform transform = new Transform(new Vector3(0, 0, -10), 0, 0, 0, 1);
            Vector3 position1 = new Vector3(0, 0, 0);
            Vector3 position2 = new Vector3(0.5f, 0.5f, 0);
            Vector3 position3 = new Vector3(0, 1f, 0);

            temporary = new List<Vector3>();
            temporary.Add(position1);
            temporary.Add(position2);
            temporary.Add(position3);

            Vector3 white = new Vector3(1, 1, 1);

            Vector3[] positions = new Vector3[3] { position1, position2 , position3};
            Vector3[] colors = new Vector3[3] { white, white, white };

            RawModel rawModel = loader.VAO_Load(positions, colors);
            Entity entity = new Entity(transform, rawModel);
            entities = new List<Entity>();
            entities.Add(entity);
        }

        //when the screen is resized.
        private void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, width, height); //set the Viewport to the same size of the window.
        }

        //when the frame updates, based upon framerate.
        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            //manage input:
            this.keyboardState = Keyboard.GetState();
            this.mouseState = Mouse.GetCursorState();
            this.mousePosition = this.display.PointToClient(new Point(mouseState.X, mouseState.Y));

            //package the input information for use in other classes.
            inputData = new InputData(keyboardState, mouseState, mouseWheelDelta, mousePosition, isWindowFocused, isMouseInWindow);

            //only manage input if the window is focused.
            if (isWindowFocused)
            {
                ManageInput(keyboardState, mouseState);
            }

            camera.Move();

            Matrix4 transformationMatrix = Mathematics.CreateTransformationMatrix(new Transform(new Vector3(0, 0, 0), 0, 0, 0, 1));
            Matrix4 projectionMatrix = masterRenderer.projectionMatrix;
            Matrix4 viewMatrix = Mathematics.CreateViewMatrix(camera);

            //Console.WriteLine($"Transformation: \n{ transformationMatrix }\n");
            //Console.WriteLine($"Projection: \n{projectionMatrix}\n");
            //Console.WriteLine($"View: \n{viewMatrix}\n");
            Vector4 position1 = viewMatrix * new Vector4(temporary[0].X, temporary[0].Y, temporary[0].Z, 1);
            Vector4 position2 = viewMatrix * new Vector4(temporary[1].X, temporary[1].Y, temporary[1].Z, 1);
            Vector4 position3 = viewMatrix * new Vector4(temporary[2].X, temporary[2].Y, temporary[2].Z, 1);
            //Console.WriteLine($"{position1} {position2} {position3}");
            //Vector4 position1 = projectionMatrix * viewMatrix * transformationMatrix * new Vector4(temporary[1].X, temporary[1].Y, temporary[1].Z, 1);
            //Console.WriteLine($"{position1}");
        }

        //when the frame is rendered, fixed timestep.
        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            masterRenderer.ProcessEntity(entities.ToArray());
            masterRenderer.Render(camera);

            //finish up rendering:
            display.SwapBuffers();
        }

        //when the window is closed.
        private void OnClosed(object sender, EventArgs e)
        {

        }


        //managing input:


        //when the window is focused/unfocused.
        private void OnFocusedChanged(object sender, EventArgs e)
        {
            this.isWindowFocused = this.display.Focused;
        }

        //when the mouse cursor leaves the screen.
        private void OnMouseLeave(object sender, EventArgs e)
        {
            this.isMouseInWindow = false;
        }

        //when the mouse cursor enters the screen.
        private void OnMouseEnter(object sender, EventArgs e)
        {
            this.isMouseInWindow = true;
        }

        //when the mouse wheel is turned.
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.mouseWheelDelta = e.Delta;
        }


        /* METHODS */


        /* INPUT */
        private void ManageInput(KeyboardState keyboardState, MouseState mouseState)
        {
            //we cannot expect everyone that uses this to have quick fingers, so a key press is normally registered for multiple frame updates--resulting in multiple calculations for no reason.
            //avoid this issue by keeping track of the last key pressed.
            Key lastKeyPressed = Key.Escape;
            
            //close the display: end the program.
            if (keyboardState.IsKeyDown(Key.Escape))
            {
                display.Close();
            }
        }
    }
}
