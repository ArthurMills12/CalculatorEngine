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
        InputManager inputManager;
        


        //rendering:
        Loader loader;
        MasterRenderer masterRenderer;
        Camera camera;

        //temporary:
        List<Entity> entities;

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
            //initialize render components. NOTE: I did try putting this in the constructor, but Loader was not initialized before rendering began. putting this here is safer, apparently.
            loader = new Loader();
            masterRenderer = new MasterRenderer();
            camera = new Camera();
            inputManager = new InputManager(this.display);


            Transform transform = new Transform(new Vector3(0, 0, -10), 0, 0, 0, 1);
            Vector3 position1 = new Vector3(0, 0, 0);
            Vector3 position2 = new Vector3(0.5f, 0.5f, -2);
            Vector3 position3 = new Vector3(0, 1f, 0);
            
            Vector3 white = new Vector3(1, 1, 1);

            Vector3[] positions = new Vector3[3] { position1, position2, position3 };
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
            //manage input through the input manager. do this upon frame updates.
            inputManager.ManageInput();

            //for now, we call camera.Move(). later this will only be called in environments that want to move the camera.
            camera.Move();

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
            //call all clean up methods:
            loader.CleanUp();
            masterRenderer.CleanUp();
        }


        //managing input:


        //when the window is focused/unfocused.
        private void OnFocusedChanged(object sender, EventArgs e)
        {
            inputManager.isWindowFocused = this.display.Focused;
        }

        //when the mouse cursor leaves the screen.
        private void OnMouseLeave(object sender, EventArgs e)
        {
            inputManager.isMouseInWindow = false;
        }

        //when the mouse cursor enters the screen.
        private void OnMouseEnter(object sender, EventArgs e)
        {
            inputManager.isMouseInWindow = true;
        }

        //when the mouse wheel is turned.
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            inputManager.mouseWheelDelta = e.Delta;
        }


        /* METHODS */

    }
}
