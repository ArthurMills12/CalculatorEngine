using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //set of points to be rendered to the screen. the points have an origin specified by Entity.Transform and GPU data referenced by Entity.RawModel. 
    class Data
    {
        /* PROPERTIES */
        public List<Vector3> coordinates { get; set; }
        public List<Vector3> colors { get; set; }
        public Entity entity { get; set; }


        /* CONSTRUCTORS */
        public Data(List<Vector3> coordiantes, List<Vector3> colors, Entity entity)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            this.entity = entity;
        }
        public Data(List<Vector3> coordiantes, List<Vector3> colors, Transform transform, RawModel rawModel)
        {
            this.coordinates = coordinates;
            this.colors = colors;
            this.entity = new Entity(transform, rawModel);
        }
    }
}
