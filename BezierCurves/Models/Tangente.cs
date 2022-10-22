using BezierCurves.EventArgs;
using BezierCurves.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace BezierCurves.Models
{
    internal class Tangente : I3DItem, ICloneable<Tangente>
    {
        private double _x;
        private double _y;
        private double _z;

        public EventHandler<ModifiedPropertyEventArgs>? CoordonateChanged { get; set; }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                ComputeLength();
                OnCoordonatesChanged();
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                ComputeLength();
                OnCoordonatesChanged();
            }
        }

        public double Z
        {
            get => _z;
            set
            {
                _z = value;
                ComputeLength();
                OnCoordonatesChanged();
            }
        }

        public double Length { get; set; }

        public Tangente()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            ComputeLength();
        }

        private void ComputeLength()
        {
            Length = Math.Sqrt(Math.Pow(_x, 2) + Math.Pow(_y, 2) + Math.Pow(_z, 2));
        }

        internal void Reset()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            ComputeLength();
            OnCoordonatesChanged();
        }

        private void OnCoordonatesChanged()
        {
            CoordonateChanged?.Invoke(this, new ModifiedPropertyEventArgs(ModifiedProperty.None));
        }

        public Point3D GetPoint3D()
        {
            return new Point3D(X, Y, Z);
        }

        public Tangente Clone()
        {
            return new Tangente()
            {
                _x = _x,
                _y = _y,
                _z = _z,
                Length = Length
            };
        }
    }
}
