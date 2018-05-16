using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //set of points to be rendered to the screen. the points have an origin specified by Entity.Transform and GPU data referenced by Entity.RawModel. 
    class Data
    {
        /* PROPERTIES */
        public Vector3[] coordinates { get; set; }
        public Vector3[] colors { get; set; }
        public Entity entity { get; set; }


        /* CONSTRUCTORS */
        public Data(Vector3[] coordinates, Vector3[] colors, Entity entity)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            this.entity = entity;
        }
        public Data(Vector3[] coordinates, Vector3[] colors, Transform transform, RawModel rawModel)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            this.entity = new Entity(transform, rawModel);
        }
        public Data(Vector3[] coordinates, Vector3[] colors, Transform transform)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            RawModel rawModel = Loader.VAO_Load(coordinates, colors);
            this.entity = new Entity(transform, rawModel);
        }
        public Data(Vector3[] coordinates, Vector3[] colors, Vector3 originPosition, float rotationX, float rotationY, float rotationZ, float scale)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            Transform transform = new Transform(originPosition, rotationX, rotationY, rotationZ, scale);
            RawModel rawModel = Loader.VAO_Load(coordinates, colors);
            this.entity = new Entity(transform, rawModel);
        }

        public Data(Vector3[] coordinates, Vector3 color, Entity entity)
        {
            this.coordinates = coordinates;
            GetSolidColorArray(color);
            this.entity = entity;
        }
        public Data(Vector3[] coordinates, Vector3 color, Transform transform, RawModel rawModel)
        {
            this.coordinates = coordinates;
            GetSolidColorArray(color);
            this.entity = new Entity(transform, rawModel);
        }
        public Data(Vector3[] coordinates, Vector3 color, Transform transform)
        {
            this.coordinates = coordinates;
            GetSolidColorArray(color);
            RawModel rawModel = Loader.VAO_Load(coordinates, colors);
            this.entity = new Entity(transform, rawModel);
        }
        public Data(Vector3[] coordinates, Vector3 color, Vector3 originPosition, float rotationX, float rotationY, float rotationZ, float scale)
        {
            this.coordinates = coordinates;
            GetSolidColorArray(color);
            Transform transform = new Transform(originPosition, rotationX, rotationY, rotationZ, scale);
            RawModel rawModel = Loader.VAO_Load(coordinates, colors);
            this.entity = new Entity(transform, rawModel);
        }


        /* METHODS */
        private void GetSolidColorArray(Vector3 color)
        {
            int length = coordinates.Length;
            colors = new Vector3[length];
            for (int i = 0; i < length; i++)
            {
                colors[i] = color;
            }
        }
    }
}
