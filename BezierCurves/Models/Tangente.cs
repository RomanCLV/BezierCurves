using BezierCurves.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Models
{
    internal class Tangente : I3DItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length { get; set; }

        public Tangente()
        {
            X = 0;
            Y = 0;
            Z = 0;
            Length = 0;
        }
    }
}
