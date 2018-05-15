using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //a set of coordinate points that makes up a function.
    public abstract class Function
    {
        /* PROPERTIES */
        //position in world space.
        private Vector3 position { get; set; }
        public Entity entity { get; set; }

        //coordinates that make up the function.
        private List<Vector3> _coordinates;
        public List<Vector3> coordinates { get => _coordinates; set => _coordinates = value; }

        public Vector3 color { get; private set; }

        private float _startTime;
        public float startTime { get => _startTime; set => _startTime = value; }

        private float _finalTime;
        public float finalTime { get => _finalTime; set => _finalTime = value; }

        private int _steps;
        public int steps { get => _steps; set => _steps = value; }
        public float timeStep { get => Math.Abs(finalTime - startTime) / steps; }

        private float _scale;
        public float scale { get => _scale; set => _scale = value; }

        public Function(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color)
        {
            this.startTime = startTime;
            this.finalTime = finalTime;
            this.steps = steps;
            this.scale = scale;
            this.color = color;
            this.position = position;
            coordinates = new List<Vector3>();
        }


        /* METHODS */
        public abstract float Image(float x); //this is the rule f(x) for the function. 

        public void GetCoordinates()
        {
            for (float x = startTime; x < finalTime; x += timeStep)
            {
                Vector3 currentCoordinate = new Vector3(scale * x, scale * Image(x), 0);
                coordinates.Add(currentCoordinate);
            }
        }

        public virtual Entity GetEntity(Loader loader) //get a rawModel using the coordinates list.
        {
            GetCoordinates();
            Console.WriteLine(coordinates.Count);
            RawModel rawModel = loader.VAO_Load(coordinates.ToArray(), GetColorArray());
            Transform transform = new Transform(position, 0, 0, 0, 1);

            return new Entity(transform, rawModel);
        }

        public virtual Vector3[] GetColorArray()
        {
            Vector3[] colorArray = new Vector3[coordinates.Count];
            for (int i = 0; i < colorArray.Length; i++)
            {
                colorArray[i] = color;
            }
            return colorArray;
        }

    }
}
