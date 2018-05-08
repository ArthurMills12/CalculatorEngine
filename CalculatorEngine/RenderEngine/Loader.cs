using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace CalculatorEngine
{
    //sends data to the GPU. do NOT make this static so we can keep track of the VAOs, VBOs, and textures that get rendered. this helps with batch rendering and cleaning up.
    class Loader
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
        private List<int> _textures = new List<int>();
        public List<int> vaos { get => _vaos; set => _vaos = value; }
        public List<int> vbos { get => _vbos; set => _vbos = value; }
        public List<int> textures { get => _textures; set => _textures = value; }
    }
}
