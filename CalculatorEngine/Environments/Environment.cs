using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorEngine
{
    //the MainGameLoop will choose an environment to render. different environments render different things to the screen.
    abstract class Environment
    {
        /* METHODS */
        //initialize the necessary things when the environment is created.
        public abstract void OnLoad();

        //when the frame is updated.
        public abstract void OnUpdateFrame();

        //when the frame is rendered.
        public abstract void OnRenderFrame(MasterRenderer masterRenderer);

        //choose how input will be handled.
        public abstract void ManageInput(InputData inputData);

        //close down everything and reset the screen.
        public abstract void CloseEnvironment();
    }
}
