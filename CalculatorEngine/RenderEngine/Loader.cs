using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace CalculatorEngine
{
    //sends data to the GPU. do NOT make this static so we can keep track of the VAOs, VBOs, and textures that get rendered. this helps with batch rendering and cleaning up.
    public class Loader
    {
        /* HOW TO LOAD SOMETHING:
         * 1. Get an array of positions of vertices.
         * 2. Get arrays of other data of the vertices.
         * 3. Store all of that into VBOs.
         * 4. Create a VAO.
         * 5. Bind the VAO.
         * 6. Store the data into the VAO attribute lists.
         * 7. Unbind the VAO.
         */


        /* PROPERTIES */
        //keep track of all VAOs and VBOs that are created so that we can dispose of them when we're done using them.
        private List<int> _vaos = new List<int>();
        private List<int> _vbos = new List<int>();
        public List<int> vaos { get => _vaos; set => _vaos = value; }
        public List<int> vbos { get => _vbos; set => _vbos = value; }


        /* METHODS */

        //initialize a VAO.
        /// <summary>
        /// Creates a VAO and binds it as the current VAO so that data can be passed to the GPU. 
        /// </summary>
        private int VAO_Initialize()
        {
            int vaoID;
            GL.GenVertexArrays(1, out vaoID); //generate a single VAO ID.
            vaos.Add(vaoID); //keep track of the VAO ID so we can delete it later.
            GL.BindVertexArray(vaoID); //bind the VAO to the GPU so we can put stuff in it.
            return vaoID;
        }


        #region AttributeList_StoreData Overloads:
        //given an array of data, put it into a VBO, and then put the VBO into an attribute list. a VAO must be loaded before this is called.
        /// <summary>
        /// Store FLOAT data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, float[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw); //sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 1, VertexAttribPointerType.Float, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        /// <summary>
        /// Store INTEGER data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, int[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(int) * data.Length, data, BufferUsageHint.StaticDraw); //sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 1, VertexAttribPointerType.Int, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        /// <summary>
        /// Store VECTOR2 data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, Vector2[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, 2 * sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw); //2 * sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 2, VertexAttribPointerType.Float, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        /// <summary>
        /// Store VECTOR2 DOUBLE data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, Vector2d[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, 2 * sizeof(double) * data.Length, data, BufferUsageHint.StaticDraw); //2 * sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 2, VertexAttribPointerType.Double, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        /// <summary>
        /// Store VECTOR3 data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, Vector3[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw); //2 * sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Float, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        /// <summary>
        /// Store VECTOR2 DOUBLE data into an attribute list of the currently-bound VAO.
        /// </summary>
        private void AttributeList_StoreData(int attributeNumber, Vector3d[] data)
        {
            //create a VBO to store the data:
            int vboID = GL.GenBuffer(); //create a VBO and get the ID.
            vbos.Add(vboID); //keep track of the VBO ID so we can delete it later.

            //set this VBO as the current VBO so we can store data in it.
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, 3 * sizeof(double) * data.Length, data, BufferUsageHint.StaticDraw); //2 * sizeof() * data.Length is the total memory space needed for the data.

            //add this VBO to the attribute list in the currently-bound VAO.
            GL.VertexAttribPointer(attributeNumber, 3, VertexAttribPointerType.Double, false, 0, 0);

            //unbind the VBO:
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); //unbinding is the same as binding ID number 0.
        }
        #endregion


        //unbind the current VAO.
        /// <summary>
        /// Calls GL.BindVertexArray(0) to unbind the currently-bound VAO.
        /// </summary>
        private void VAO_Unbind()
        {
            GL.BindVertexArray(0); //unbind is equivalent to binding ID zero.
        }


        #region VAO_Load Overloads:
        //given arrays of data, call the AttributeList_StoreData() methods to load them into the GPU to create the VAO. attribute list 0 will always be reserved for positional data.
        /// <summary>
        /// Load a single Vector3 into the attribute list: 0.
        /// </summary>
        public RawModel VAO_Load(Vector3 position)
        {
            //create and bind the VAO:
            int vaoID = VAO_Initialize();

            //store the data into the attribute lists of the VAO:
            AttributeList_StoreData(0, new Vector3[1] { position });

            //unbind the current VAO:
            VAO_Unbind();

            //return a raw model with a reference to the VAO ID:
            return new RawModel(vaoID, 1);
        }
        /// <summary>
        /// Load a single array of Vector3 into the attribute lists: 0.
        /// </summary>
        public RawModel VAO_Load(Vector3[] positions)
        {
            //create and bind the VAO:
            int vaoID = VAO_Initialize();

            //store the data into the attribute lists of the VAO:
            AttributeList_StoreData(0, positions);

            //unbind the current VAO:
            VAO_Unbind();

            //return a raw model with a reference to the VAO ID:
            return new RawModel(vaoID, positions.Length);
        }
        /// <summary>
        /// Load into the attribute lists: 0, 1.
        /// </summary>
        public RawModel VAO_Load(Vector3[] positions, Vector3[] data_1)
        {
            //create and bind the VAO:
            int vaoID = VAO_Initialize();

            //store the data into the attribute lists of the VAO:
            AttributeList_StoreData(0, positions);
            AttributeList_StoreData(1, data_1);

            //unbind the current VAO:
            VAO_Unbind();

            //return a raw model with a reference to the VAO ID:
            return new RawModel(vaoID, positions.Length);
        }
        /// <summary>
        /// Load into the attribute lists: 0, 1.
        /// </summary>
        public RawModel VAO_Load(Vector3[] positions, Vector2[] data_1)
        {
            //create and bind the VAO:
            int vaoID = VAO_Initialize();

            //store the data into the attribute lists of the VAO:
            AttributeList_StoreData(0, positions);
            AttributeList_StoreData(1, data_1);

            //unbind the current VAO:
            VAO_Unbind();

            //return a raw model with a reference to the VAO ID:
            return new RawModel(vaoID, positions.Length);
        }
        /// <summary>
        /// Load into the attribute lists: 0, 1, 2.
        /// </summary>
        public RawModel VAO_Load(Vector3[] positions, Vector2[] data_1, int[] data_2)
        {
            //create and bind the VAO:
            int vaoID = VAO_Initialize();

            //store the data into the attribute lists of the VAO:
            AttributeList_StoreData(0, positions);
            AttributeList_StoreData(1, data_1);
            AttributeList_StoreData(2, data_2);

            //unbind the current VAO:
            VAO_Unbind();

            //return a raw model with a reference to the VAO ID:
            return new RawModel(vaoID, positions.Length);
        }
        #endregion


        //clean up:
        public void CleanUp()
        {
            //delete all of the VBOs and VAOs that we've used:
            GL.DeleteVertexArrays(vaos.Count, vaos.ToArray());
            GL.DeleteBuffers(vbos.Count, vbos.ToArray());
        }
    }
}
