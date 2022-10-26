using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BezierCurves.Beziers
{
    internal class Bezier3Coefficients
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public double D { get; private set; }

        private readonly Func<Point3D, double> _select;
        private readonly Point3D _p0;
        private readonly Point3D _p1;
        private readonly Point3D _p2;
        private readonly Point3D _p3;

        public Bezier3Coefficients(Func<Point3D, double> select, Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            _select = select;
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
        }

        internal void Compute()
        {
            A = _select(_p3) - _select(_p0) + 3 * (_select(_p1) - _select(_p2));
            B = 3 * (_select(_p0) - 2 * _select(_p1) + _select(_p2));
            C = 3 * (_select(_p1) - _select(_p0));
            D = _select(_p0);
        }
    }
}
