using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorEngine
{
    //having this components gives the object a raw model so that it can be rendered, as well as a transform so it has its own coordinate reference frame.
    public class Entity
    {
        /* PROPERTIES */
        public Transform transform { get; set; }
        public RawModel rawModel { get; set; }


        /* CONSTRUCTORS */
        public Entity(Transform transform, RawModel rawModel)
        {
            this.transform = transform;
            this.rawModel = rawModel;
        }
    }
}
