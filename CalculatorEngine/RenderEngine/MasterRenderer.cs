using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace CalculatorEngine
{
    //handles rendering for everything.
    class MasterRenderer
    {
        /* PROPERTIES */

        //renderers:
        private EntityRenderer entityRenderer;

        //shaders:
        private EntityShader entityShader;

        //batches:
        private List<Entity> batch_Entities { get; set; }

        //projection matrix:
        public Matrix4 projectionMatrix;
        public static float fieldOfView = Convert.ToSingle(Math.PI / 3); //field of view angle for the frustrum.
        public static float nearPlane = 0.1f; //location of the nearest objects viewable by the projection matrix.
        public static float farPlane = 1000; //location of the furthest objects viewable by the projection matrix.


        /* CONSTRUCTORS */
        public MasterRenderer()
        {
            //enable special caps:
            SetRenderSettings();

            //initialize projection matrix, shaders, and renderers:
            this.projectionMatrix = CreateProjectionMatrix();
            this.entityShader = new EntityShader();
            this.entityRenderer = new EntityRenderer(entityShader, this.projectionMatrix);

            //initialize batches:
            batch_Entities = new List<Entity>();
        }


        /* METHODS */

        //set basic initial rendering settings:
        private void SetRenderSettings()
        {
            //we should not bother rendering triangles whose normal vectors are pointing away from the camera. we will "cull" those faces here.
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            //check z-values of objects and render the ones closest to the camera.
            GL.Enable(EnableCap.DepthTest);

            //allows the shaders to access the gl_PointSize variable, changing the size of point primitives.
            //GL.Enable(EnableCap.VertexProgramPointSize); 
        }

        //create the projection matrix:
        private Matrix4 CreateProjectionMatrix()
        {
            //create the projection matrix using the fields above.
            return Matrix4.CreatePerspectiveFieldOfView(fieldOfView, (float)Display.width / (float)Display.height, nearPlane, farPlane);
        }

        //prepare the screen for rendering by clearing it each frame.
        public void Prepare()
        {
            GL.ClearColor(0, 0, 0, 1); //black.
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        //clear all batches.
        private void ClearBatches()
        {
            //call this for every batch.
            this.batch_Entities.Clear();
        }

        //process a given object and add it to its corresponding batch.
        public void ProcessEntity(params Entity[] entities)
        {
            foreach (Entity entity in entities)
            {
                this.batch_Entities.Add(entity);
            }
        }


        //render everything in the batches using the proper renderers.
        public void Render(Camera camera)
        {
            //reset the screen:
            Prepare();

            //entities:
            entityShader.Start();
            entityShader.LoadViewMatrix(camera);
            entityRenderer.Render(batch_Entities);
            entityShader.Stop();
            
            //clear the batches:
            ClearBatches();
        }


        //clean up:
        public void CleanUp()
        {
            entityShader.CleanUp();
        }
    }
}
