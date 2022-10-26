using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Interfaces
{
    internal interface ICloneable<T>
    {
        internal T Clone();
    }
}
