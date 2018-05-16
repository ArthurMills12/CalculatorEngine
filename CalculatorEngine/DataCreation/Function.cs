using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    //returns a set of coordinate points that makes up a function.
    public static class Function
    {
        /* METHODS */

        //different functions:
        public static List<Vector3> GetSineFunction(float t0, float tf, int steps, float amplitude, float angularVelocity, float phaseShift)
        {
            List<Vector3> coordinates = new List<Vector3>();

            float dt = Math.Abs(tf - t0) / steps;
            for (float t = t0; t < tf; t += dt)
            {
                float image = amplitude * Mathematics.Sin(angularVelocity * t + phaseShift);
                Vector3 currentCoordinate = new Vector3(t, image, 0);
                coordinates.Add(currentCoordinate);
            }

            return coordinates;
        }
    }
}
