using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BezierCurves.Interfaces;
using BezierCurves.Models;

namespace BezierCurves.ViewModels
{
    internal class SampleViewModel : ViewModelBase
    {
        private readonly Sample _sample;

        private string _name;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public double X
        {
            get => _sample.X;
            set
            {
                if (_sample.X != value)
                {
                    _sample.X = value;
                    OnPropertyChanged(nameof(X));
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
                    _sample.Y = value;
                    OnPropertyChanged(nameof(Y));
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
                    _sample.Z = value;
                    OnPropertyChanged(nameof(Z));
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
                    _sample.TIn.X = value;
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
                    _sample.TIn.Y = value;
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
                    _sample.TIn.Z = value;
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
                    _sample.TOut.X = value;
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
                    _sample.TOut.Y = value;
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
                    _sample.TOut.Z = value;
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
                    _sample.TIn.Length = value;
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
                    _sample.TOut.Length = value;
                    OnPropertyChanged(nameof(OLength));
                }
            }
        }

        public SampleViewModel(Sample sample)
        {
            _sample = sample;
            _name = string.Empty;
        }

        public override string ToString()
        {
            return _name + "{ " + $"{X} {Y} {Z}" + " }";
        }
    }
}
