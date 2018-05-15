using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //responsible for shading functions and data points from solving differential equations, etc. these points need a position and color.
    class EntityShader : ShaderProgram
    {
        /* PROPERTIES */
        //shader files. reference them from the Resources folder. note that these are pure files, not filePaths.
        private static string vertexShaderFile = Properties.Resources.continuousDataVertexShader;
        private static string fragmentShaderFile = Properties.Resources.continuousDataFragmentShader;

        //uniform variables.
        private int locationTransformationMatrix;
        private int locationProjectionMatrix;
        private int locationViewMatrix;


        /*CONSTRUCTORS */
        public EntityShader() : base(vertexShaderFile, fragmentShaderFile)
        {

        }


        /* METHODS */

        //bind our desired attributes:
        protected override void BindAllAttributes()
        {
            BindAttribute(0, "vPosition");
            BindAttribute(1, "vColor");
        }

        //figure out where are desired uniforms are located in the shaders.
        protected override void GetAllUniformLocations()
        {
            locationTransformationMatrix = GetUniformLocation("uTransformationMatrix");
            locationProjectionMatrix = GetUniformLocation("uProjectionMatrix");
            locationViewMatrix = GetUniformLocation("uViewMatrix");
        }

        //load the uniform variables:
        public void LoadTransformationMatrix(Matrix4 transformationMatrix)
        {
            LoadUniform(locationTransformationMatrix, transformationMatrix);
        }
        public void LoadProjectionMatrix(Matrix4 projectionMatrix)
        {
            LoadUniform(locationProjectionMatrix, projectionMatrix);
        }
        public void LoadViewMatrix(Camera camera)
        {
            Matrix4 viewMatrix = Mathematics.CreateViewMatrix(camera);
            LoadUniform(locationViewMatrix, viewMatrix);
        }
    }
}
