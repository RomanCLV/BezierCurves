using BezierCurves.EventArgs;
using BezierCurves.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Models
{
    internal class Sample : I3DItem, ICloneable<Sample>
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
                OnCoordonatesChanged();
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnCoordonatesChanged();
            }
        }

        public double Z
        {
            get => _z;
            set
            {
                _z = value;
                OnCoordonatesChanged();
            }
        }

        public Tangente TIn { get; private set; }
        public Tangente TOut { get; private set; }

        public Sample()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            TIn = new Tangente();
            TOut = new Tangente();
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
            CoordonateChanged?.Invoke(this, new ModifiedPropertyEventArgs(ModifiedProperty.None));
        }

        public Sample Clone()
        {
            return new Sample()
            {
                _x = _x,
                _y = _y,
                _z = _z,
                TIn = TIn.Clone(),
                TOut = TOut.Clone()
            };
        }
    }
}
