using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BezierCurves.Interfaces
{
    internal interface I3DItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public EventHandler? CoordonateChanged { get; set; }

        public Point3D GetPoint3D();
    }
}
