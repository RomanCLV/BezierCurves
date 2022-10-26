using BezierCurves.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BezierCurves.Beziers
{
    internal class Bezier3Coefficients3D
    {
        public Bezier3Coefficients XCoefficients { get; private set; }
        public Bezier3Coefficients YCoefficients { get; private set; }
        public Bezier3Coefficients ZCoefficients { get; private set; }

        private readonly Sample _start;
        private readonly Sample _end;

        public Bezier3Coefficients3D(Sample start, Sample end)
        {
            _start = start;
            _end = end;
            Point3D p0 = _start.GetPoint3D();
            Point3D p3 = _end.GetPoint3D();
            Point3D p1 = new Point3D(p0.X + _start.TOut.X, p0.Y + _start.TOut.Y, p0.Z + _start.TOut.Z);
            Point3D p2 = new Point3D(p3.X + _end.TIn.X, p3.Y + _end.TIn.Y, p3.Z + _end.TIn.Z);

            XCoefficients = new Bezier3Coefficients(p => p.X, p0, p1, p2, p3);
            YCoefficients = new Bezier3Coefficients(p => p.Y, p0, p1, p2, p3);
            ZCoefficients = new Bezier3Coefficients(p => p.Z, p0, p1, p2, p3);
        }

        internal void Compute()
        {
            XCoefficients.Compute();
            YCoefficients.Compute();
            ZCoefficients.Compute();
        }
    }
}
