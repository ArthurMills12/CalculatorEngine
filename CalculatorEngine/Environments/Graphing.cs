using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //standard graphing calculator environment. graphs functions and sets of data somehow specified by the user. 
    class Graphing : Environment
    {
        /* GOAL:
         * Create sets of data with the Data class object.
         * Store those sets into a list.
         * Render everything in that list.
         * 
         */


        /* PROPERTIES */
        public List<Data> dataSet { get; set; }





        /* CONSTRUCTORS */
        public Graphing()
        {
            dataSet = new List<Data>();
        }



        /* METHODS */

        //graphing:

        private Data GetSineFunctionData(Vector3 origin, float t0, float tf, int steps, float amplitude, float angularVelocity, float phaseShift, Vector3 color)
        {
            Vector3[] coordinates = Function.GetSineFunction(t0, tf, steps, amplitude, angularVelocity, phaseShift).ToArray();

            return new Data(coordinates, color, origin, 0, 0, 0, 1);
        }










        //overrides:

        public override void CloseEnvironment()
        {
            //throw new NotImplementedException();
        }

        public override void ManageInput(InputData inputData)
        {
            //throw new NotImplementedException();
        }

        public override void OnLoad()
        {
            dataSet.Add(GetSineFunctionData(new Vector3(0, 0, -10), -3.2f, 3.2f, 1000, 3, 1, 0, new Vector3(1, 1, 1)));
        }

        public override void OnRenderFrame(MasterRenderer masterRenderer)
        {
            foreach (Data data in dataSet)
            {
                if (data != null)
                {
                    masterRenderer.ProcessEntity(data.entity);
                }
            }
        }

        public override void OnUpdateFrame()
        {
            //throw new NotImplementedException();
        }
    }
}
