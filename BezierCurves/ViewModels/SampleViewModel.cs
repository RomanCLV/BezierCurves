using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BezierCurves.EventArgs;
using BezierCurves.Interfaces;
using BezierCurves.Models;

namespace BezierCurves.ViewModels
{
    internal class SampleViewModel : ViewModelBase, ICloneable<SampleViewModel>
    {
        private ModifiedProperty _modifiedProperty;

        private readonly Sample _sample;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (SetValue(ref _name, value))
                {
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string FullName => ToString();

        public double X
        {
            get => _sample.X;
            set
            {
                if (_sample.X != value)
                {
                    _modifiedProperty = ModifiedProperty.X;
                    _sample.X = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double Y
        {
            get => _sample.Y;
            set
            {
                if (_sample.Y != value)
                {
                    _modifiedProperty = ModifiedProperty.Y;
                    _sample.Y = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(Y));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double Z
        {
            get => _sample.Z;
            set
            {
                if (_sample.Z != value)
                {
                    _modifiedProperty = ModifiedProperty.Z;
                    _sample.Z = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(Z));
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double IX
        {
            get => _sample.TIn.X;
            set
            {
                if (_sample.TIn.X != value)
                {
                    _modifiedProperty = ModifiedProperty.IX;
                    _sample.TIn.X = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(IX));
                }
            }
        }

        public double IY
        {
            get => _sample.TIn.Y;
            set
            {
                if (_sample.TIn.Y != value)
                {
                    _modifiedProperty = ModifiedProperty.IY;
                    _sample.TIn.Y = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(IY));
                }
            }
        }

        public double IZ
        {
            get => _sample.TIn.Z;
            set
            {
                if (_sample.TIn.Z != value)
                {
                    _modifiedProperty = ModifiedProperty.IZ;
                    _sample.TIn.Z = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(IZ));
                }
            }
        }

        public double OX
        {
            get => _sample.TOut.X;
            set
            {
                if (_sample.TOut.X != value)
                {
                    _modifiedProperty = ModifiedProperty.OX;
                    _sample.TOut.X = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(OX));
                }
            }
        }

        public double OY
        {
            get => _sample.TOut.Y;
            set
            {
                if (_sample.TOut.Y != value)
                {
                    _modifiedProperty = ModifiedProperty.OY;
                    _sample.TOut.Y = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(OY));
                }
            }
        }

        public double OZ
        {
            get => _sample.TOut.Z;
            set
            {
                if (_sample.TOut.Z != value)
                {
                    _modifiedProperty = ModifiedProperty.OZ;
                    _sample.TOut.Z = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(OZ));
                }
            }
        }

        public double ILength
        {
            get => _sample.TIn.Length;
            set
            {
                if (_sample.TIn.Length != value)
                {
                    _modifiedProperty = ModifiedProperty.ILength;
                    _sample.TIn.Length = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(ILength));
                }
            }
        }

        public double OLength
        {
            get => _sample.TOut.Length;
            set
            {
                if (_sample.TOut.Length != value)
                {
                    _modifiedProperty = ModifiedProperty.OLength;
                    _sample.TOut.Length = value;
                    _modifiedProperty = ModifiedProperty.None;
                    OnPropertyChanged(nameof(OLength));
                }
            }
        }

        public SampleViewModel(Sample sample)
        {
            _sample = sample;
            _name = string.Empty;
            _modifiedProperty = ModifiedProperty.None;

            _sample.CoordonateChanged += Sample_CoordonatesChanged;
            _sample.TIn.CoordonateChanged += SampleTIn_CoordonatesChanged;
            _sample.TOut.CoordonateChanged += SampleTOut_CoordonatesChanged;
        }

        private void Sample_CoordonatesChanged(object? sender, ModifiedPropertyEventArgs eventArgs)
        {
            if (_modifiedProperty == ModifiedProperty.X ||
                _modifiedProperty == ModifiedProperty.Y ||
                _modifiedProperty == ModifiedProperty.Z)
            {
                return;
            }
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
            OnPropertyChanged(nameof(Z));
        }

        private void SampleTIn_CoordonatesChanged(object? sender, ModifiedPropertyEventArgs eventArgs)
        {
            if (_modifiedProperty == ModifiedProperty.IX ||
                _modifiedProperty == ModifiedProperty.IY ||
                _modifiedProperty == ModifiedProperty.IZ ||
                _modifiedProperty == ModifiedProperty.ILength)
            {
                return;
            }
            OnPropertyChanged(nameof(IX));
            OnPropertyChanged(nameof(IY));
            OnPropertyChanged(nameof(IZ));
            OnPropertyChanged(nameof(ILength));
        }

        private void SampleTOut_CoordonatesChanged(object? sender, ModifiedPropertyEventArgs eventArgs)
        {
            if (_modifiedProperty == ModifiedProperty.OX ||
                _modifiedProperty == ModifiedProperty.OY ||
                _modifiedProperty == ModifiedProperty.OZ ||
                _modifiedProperty == ModifiedProperty.OLength)
            {
                return;
            }
            OnPropertyChanged(nameof(OX));
            OnPropertyChanged(nameof(OY));
            OnPropertyChanged(nameof(OZ));
            OnPropertyChanged(nameof(OLength));
        }

        internal void Reset()
        {
            _sample.Reset();
        }

        public override void Dispose()
        {
            _sample.CoordonateChanged -= Sample_CoordonatesChanged;
            _sample.TIn.CoordonateChanged -= SampleTIn_CoordonatesChanged;
            _sample.TOut.CoordonateChanged -= SampleTOut_CoordonatesChanged;
            base.Dispose();
        }

        public override string ToString()
        {
            return _name + " { " + $"{X} {Y} {Z}" + " }";
        }

        public SampleViewModel Clone()
        {
            return new SampleViewModel(_sample.Clone())
            {
                _name = _name,
            };
        }
    }
}
