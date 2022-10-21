using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.EventArgs
{
    internal class ModifiedPropertyEventArgs : System.EventArgs
    {
        public ModifiedProperty ModifiedProperty { get; }

        public ModifiedPropertyEventArgs(ModifiedProperty modifiedProperty)
        {
            ModifiedProperty = modifiedProperty;
        }
    }
}
