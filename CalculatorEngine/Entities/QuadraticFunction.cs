﻿using System;
using System.Collections.Generic;
using OpenTK;

namespace CalculatorEngine
{
    class QuadraticFunction : Function
    {
        /* PROPERTIES */
        private float _mu;
        public float mu { get => _mu; set => _mu = value; }


        /* CONSTRUCTORS */
        public QuadraticFunction(float startTime, float finalTime, int steps, float scale, Vector3 position, Vector3 color, float mu) : base(startTime, finalTime, steps, scale, position, color)
        {
            this.mu = mu;
        }


        /* METHODS */
        public override float Image(float x) //standard one-parameter family of quadratic functions. the parameter is mu. 
        {
            return Convert.ToSingle(mu * x * (1 - x));
        }
    }
}
