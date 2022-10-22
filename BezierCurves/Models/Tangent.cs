using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using BezierCurves.Interfaces;

namespace BezierCurves.Models
{
    internal class Tangent : I3DItem, ICloneable<Tangent>
    {
        private double _x;
        private double _y;
        private double _z;

        public EventHandler? CoordonateChanged { get; set; }

        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    ComputeLength();
                    OnCoordonatesChanged();
                }
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    ComputeLength();
                    OnCoordonatesChanged();
                }
            }
        }

        public double Z
        {
            get => _z;
            set
            {
                if (_z != value)
                {
                    _z = value;
                    ComputeLength();
                    OnCoordonatesChanged();
                }
            }
        }

        public double Length { get; set; }

        public Tangent()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            ComputeLength();
        }

        public Tangent(double x, double y, double z) : this()
        {
            _x = x;
            _y = y;
            _z = z;
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

        internal void SetFrom(Tangent tangente)
        {
            X = tangente._x;
            Y = tangente._y;
            Z = tangente._z;
        }


        internal void SetFromOpposite(Tangent tangente)
        {
            X = -tangente._x;
            Y = -tangente._y;
            Z = -tangente._z;
        }

        private void OnCoordonatesChanged()
        {
            CoordonateChanged?.Invoke(this, EventArgs.Empty);
        }

        public Point3D GetPoint3D()
        {
            return new Point3D(X, Y, Z);
        }

        public Tangent Clone()
        {
            return new Tangent()
            {
                _x = _x,
                _y = _y,
                _z = _z,
                Length = Length
            };
        }
    }
}
