using BezierCurves.EventArgs;
using BezierCurves.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Models
{
    internal class Tangente : I3DItem, ICloneable<Tangente>
    {
        private double _x;
        private double _y;
        private double _z;
        private double _length;

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

        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                OnCoordonatesChanged();
            }
        }

        public Tangente()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _length = 0;
        }

        internal void Reset()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _length = 0;
            OnCoordonatesChanged();
        }

        private void OnCoordonatesChanged()
        {
            CoordonateChanged?.Invoke(this, new ModifiedPropertyEventArgs(ModifiedProperty.None));
        }

        public Tangente Clone()
        {
            return new Tangente()
            {
                _x = _x,
                _y = _y,
                _z = _z,
                _length = _length
            };
        }
    }
}
