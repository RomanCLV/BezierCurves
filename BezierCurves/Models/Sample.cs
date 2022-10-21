﻿using BezierCurves.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Models
{
    internal class Sample : I3DItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Tangente TIn { get; private set; }
        public Tangente TOut { get; private set; }

        public Sample()
        {
            X = 0;
            Y = 0;
            Z = 0;
            TIn = new Tangente();
            TOut = new Tangente();
        }
    }
}
