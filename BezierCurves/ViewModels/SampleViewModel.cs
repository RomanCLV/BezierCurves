using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BezierCurves.Interfaces;
using BezierCurves.Models;

namespace BezierCurves.ViewModels
{
    internal class SampleViewModel : ViewModelBase, ICloneable<SampleViewModel>
    {
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
                    _sample.X = value;
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
                    _sample.Y = value;
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
                    _sample.Z = value;
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
                    _sample.TIn.X = value;
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
                }
            }
        }

        public bool AreTangentsContinous
        {
            get => _sample.AreTangentsContinous;
            set
            {
                if (_sample.AreTangentsContinous != value)
                {
                    _sample.AreTangentsContinous = value;
                }
            }
        }

        public Model3DGroup Model { get; set; }

        private readonly GeometryModel3D _sphereGeometry;
        private readonly GeometryModel3D _tangentInGeometry;
        private readonly GeometryModel3D _tangentOutGeometry;

        public SampleViewModel(Sample sample)
        {
            _sample = sample;
            _name = string.Empty;

            _sphereGeometry = Helper3D.Helper3D.BuildSphere(new Point3D(), 0.2, Brushes.Gold);
            _tangentInGeometry = Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(),  0.1, Brushes.IndianRed);
            _tangentOutGeometry = Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(), 0.1, Brushes.Indigo);
            
            UpdateTransform(_sphereGeometry);
            UpdateTransform(_tangentInGeometry);
            UpdateTransform(_tangentOutGeometry);
            UpdateTangentGeometry(_tangentInGeometry, _sample.TIn, 0.1);
            UpdateTangentGeometry(_tangentOutGeometry, _sample.TOut, 0.1);

            Model = new Model3DGroup();
            Model.Children.Add(_sphereGeometry);
            Model.Children.Add(_tangentInGeometry);
            Model.Children.Add(_tangentOutGeometry);

            _sample.CoordonateChanged += Sample_CoordonatesChanged;
            _sample.AreTangentsContinousChanged += Sample_AreTangentsContinousChanged;
            _sample.TIn.CoordonateChanged += SampleTIn_CoordonatesChanged;
            _sample.TOut.CoordonateChanged += SampleTOut_CoordonatesChanged;
        }

        private void Sample_CoordonatesChanged(object? sender, EventArgs eventArgs)
        {
            UpdateTransform(_sphereGeometry);
            UpdateTransform(_tangentInGeometry);
            UpdateTransform(_tangentOutGeometry);

            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
            OnPropertyChanged(nameof(Z));
        }

        private void Sample_AreTangentsContinousChanged(object? sender, EventArgs eventArgs)
        {
            OnPropertyChanged(nameof(AreTangentsContinous));
        }

        private void SampleTIn_CoordonatesChanged(object? sender, EventArgs eventArgs)
        {
            UpdateTangentGeometry(_tangentInGeometry, _sample.TIn, 0.1);
            OnPropertyChanged(nameof(IX));
            OnPropertyChanged(nameof(IY));
            OnPropertyChanged(nameof(IZ));
            OnPropertyChanged(nameof(ILength));
        }

        private void SampleTOut_CoordonatesChanged(object? sender, EventArgs eventArgs)
        {
            UpdateTangentGeometry(_tangentOutGeometry, _sample.TOut, 0.1);
            OnPropertyChanged(nameof(OX));
            OnPropertyChanged(nameof(OY));
            OnPropertyChanged(nameof(OZ));
            OnPropertyChanged(nameof(OLength));
        }

        internal void Reset()
        {
            _sample.Reset();
        }

        private void UpdateTransform(GeometryModel3D geometryModel3D)
        {
            Trace.WriteLine("SampleVM UpdateTransform");
            TranslateTransform3D transform = (TranslateTransform3D)geometryModel3D.Transform;
            transform.OffsetX = X;
            transform.OffsetY = Y;
            transform.OffsetZ = Z;
        }

        private void UpdateTangentGeometry(GeometryModel3D tangenteGeometry, Tangent tangente, double diameter)
        {
            Trace.WriteLine("SampleVM UpdateTangentGeometry");
            tangenteGeometry.Geometry = Helper3D.Helper3D.BuildArrowGeometry(new Point3D(), tangente.GetPoint3D(), diameter);
        }

        internal Point3D GetSamplePoint3D()
        {
            return _sample.GetPoint3D();
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
