using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorEngine
{
    //any object that is to be rendered to the screen will implement this interface.
    interface IRenderable
    {
        RawModel rawModel { get; set; }

        RawModel GetRawModel(Loader loader);
    }
}
