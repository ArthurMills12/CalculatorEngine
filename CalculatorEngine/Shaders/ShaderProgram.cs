using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace CalculatorEngine
{
    //template for a generic shader program.
    abstract class ShaderProgram
    {
        /* PROPERTIES */

        //ID numbers:
        private int programID { get; set; } //combined shader program.
        private int vertexShaderID { get; set; } 
        private int fragmentShaderID { get; set; }


        /* CONSTRUCTORS */
        public ShaderProgram(string vertexShaderFile, string fragmentShaderFile)
        {
            //load up the given shaders:
            vertexShaderID = LoadShader(ShaderType.VertexShader, vertexShaderFile);
            fragmentShaderID = LoadShader(ShaderType.FragmentShader, fragmentShaderFile);

            //create the total shader program and attach the shaders to it:
            programID = GL.CreateProgram();
            GL.AttachShader(programID, vertexShaderID);
            GL.AttachShader(programID, fragmentShaderID);

            //prepare the shaders for use in the GPU by initializating memory locations, including uniform variables.
            BindAllAttributes();
            GL.LinkProgram(programID);
            GL.ValidateProgram(programID);
            GetAllUniformLocations();
        }


        /* METHODS */

        //point the GPU to the location of the shader and get an ID for it.
        private static int LoadShader(ShaderType shaderType, string textFile)
        {
            //get the shader ID:
            int shaderID = GL.CreateShader(shaderType);

            //try to load up the shader given the text file.
            try
            {
                GL.ShaderSource(shaderID, textFile);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            return shaderID;
        }

        //activate an attribute memory location in the shader.
        /// <summary>
        /// Opens the given memory location number in the shader to the given variable name.
        /// </summary>
        protected void BindAttribute(int attributeNumber, string attributeVariableName)
        {
            GL.BindAttribLocation(programID, attributeNumber, attributeVariableName);
        }

        //any subclass needs a way to bind the attributes that the desired shader program needs.
        protected abstract void BindAllAttributes();

        //similarly, we need to know where to bind uniform variables.
        /// <summary>
        /// Search through the shader program for the use of the following uniform variable name and return its location.
        /// </summary>
        protected int GetUniformLocation(string uniformVariableName)
        {
            return GL.GetUniformLocation(programID, uniformVariableName);
        }

        //any subclass needs to get all of the uniform locations so they can be initialized.
        protected abstract void GetAllUniformLocations();


        // load uniform variables:
        #region Load Uniform Overloads:
        protected void LoadUniform(int location, float value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, double value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, int value)
        {
            GL.Uniform1(location, value);
        }
        protected void LoadUniform(int location, bool value)
        {
            GL.Uniform1(location, value ? 1 : 0); //note that GLSL does not have booleans. so if we want to send "true", we instead send 1. if we send "false", we will send 0.
        }
        protected void LoadUniform(int location, Vector2 value)
        {
            GL.Uniform2(location, value);
        }
        protected void LoadUniform(int location, Vector3 value)
        {
            GL.Uniform3(location, value);
        }
        protected void LoadUniform(int location, Vector4 value)
        {
            GL.Uniform4(location, value);
        }
        protected void LoadUniform(int location, Vector4d value)
        {
            GL.Uniform4(location, value.X, value.Y, value.Z, value.W);
        }
        protected void LoadUniform(int location, Matrix4 value)
        {
            GL.UniformMatrix4(location, false, ref value);
        }
        #endregion


        //use the shader:
        public void Start()
        {
            GL.UseProgram(programID);
        }

        //stop the shader:
        public void Stop()
        {
            GL.UseProgram(0);
        }

        //clean up:
        public void CleanUp()
        {
            Stop();

            //detach and delete:
            GL.DetachShader(programID, vertexShaderID);
            GL.DetachShader(programID, fragmentShaderID);
            GL.DeleteShader(vertexShaderID);
            GL.DeleteShader(fragmentShaderID);
            GL.DeleteProgram(programID);
        }
    }
}
