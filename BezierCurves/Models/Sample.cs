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
    internal class Sample : I3DItem, ICloneable<Sample>
    {
        private double _x;
        private double _y;
        private double _z;

        public EventHandler? CoordonateChanged { get; set; }
        public EventHandler? AreTangentsContinousChanged { get; set; }

        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
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
                    OnCoordonatesChanged();
                }
            }
        }

        public Tangent TIn { get; private set; }
        public Tangent TOut { get; private set; }

        private bool _areTangentsContinous;
        public bool AreTangentsContinous
        {
            get => _areTangentsContinous;
            set
            {
                if (value != _areTangentsContinous)
                {
                    _areTangentsContinous = value;
                    if (_areTangentsContinous)
                    {
                        TOut.SetFromOpposite(TIn);
                    }
                    OnAreTangentsContinousChanged();
                }
            }
        }

        public Sample()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _areTangentsContinous = true;
            TIn = new Tangent();
            TOut = new Tangent();

            TIn.CoordonateChanged += TangentIn_CoordonatesChanged;
            TOut.CoordonateChanged += TangentOut_CoordonatesChanged;
        }

        private void TangentIn_CoordonatesChanged(object? sender, EventArgs e)
        {
            if (_areTangentsContinous)
            {
                TOut.SetFromOpposite(TIn);
            }
        }

        private void TangentOut_CoordonatesChanged(object? sender, EventArgs e)
        {
            if (_areTangentsContinous)
            {
                TIn.SetFromOpposite(TOut);
            }
        }

        internal void Reset()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            TIn.Reset();
            TOut.Reset();
            OnCoordonatesChanged();
        }

        private void OnCoordonatesChanged()
        {
            CoordonateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnAreTangentsContinousChanged()
        {
            AreTangentsContinousChanged?.Invoke(this, EventArgs.Empty);
        }

        public Point3D GetPoint3D()
        {
            return new Point3D(X, Y, Z);
        }

        public Sample Clone()
        {
            Sample sample = new Sample()
            {
                _x = _x,
                _y = _y,
                _z = _z,
                _areTangentsContinous = _areTangentsContinous,
            };
            sample.TIn.SetFrom(TIn);
            sample.TOut.SetFrom(TOut);
            return sample;
        }
    }
}
