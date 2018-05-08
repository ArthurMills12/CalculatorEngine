using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            MainRenderLoop mainRenderLoop = new MainRenderLoop(1920, 1080, 60);
        }
    }
}
