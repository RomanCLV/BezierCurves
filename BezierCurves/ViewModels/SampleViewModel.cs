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
        internal Sample Sample { get; private set; }

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
            get => Sample.X;
            set
            {
                if (Sample.X != value)
                {
                    Sample.X = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double Y
        {
            get => Sample.Y;
            set
            {
                if (Sample.Y != value)
                {
                    Sample.Y = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double Z
        {
            get => Sample.Z;
            set
            {
                if (Sample.Z != value)
                {
                    Sample.Z = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public double IX
        {
            get => Sample.TIn.X;
            set
            {
                if (Sample.TIn.X != value)
                {
                    Sample.TIn.X = value;
                }
            }
        }

        public double IY
        {
            get => Sample.TIn.Y;
            set
            {
                if (Sample.TIn.Y != value)
                {
                    Sample.TIn.Y = value;
                }
            }
        }

        public double IZ
        {
            get => Sample.TIn.Z;
            set
            {
                if (Sample.TIn.Z != value)
                {
                    Sample.TIn.Z = value;
                }
            }
        }

        public double OX
        {
            get => Sample.TOut.X;
            set
            {
                if (Sample.TOut.X != value)
                {
                    Sample.TOut.X = value;
                }
            }
        }

        public double OY
        {
            get => Sample.TOut.Y;
            set
            {
                if (Sample.TOut.Y != value)
                {
                    Sample.TOut.Y = value;
                }
            }
        }

        public double OZ
        {
            get => Sample.TOut.Z;
            set
            {
                if (Sample.TOut.Z != value)
                {
                    Sample.TOut.Z = value;
                }
            }
        }

        public double ILength
        {
            get => Sample.TIn.Length;
            set
            {
                if (Sample.TIn.Length != value)
                {
                    Sample.TIn.Length = value;
                }
            }
        }

        public double OLength
        {
            get => Sample.TOut.Length;
            set
            {
                if (Sample.TOut.Length != value)
                {
                    Sample.TOut.Length = value;
                }
            }
        }

        public bool AreTangentsContinous
        {
            get => Sample.AreTangentsContinous;
            set
            {
                if (Sample.AreTangentsContinous != value)
                {
                    Sample.AreTangentsContinous = value;
                }
            }
        }

        public Model3DGroup Model { get; set; }

        private readonly GeometryModel3D _sphereGeometry;
        private readonly GeometryModel3D _tangentInGeometry;
        private readonly GeometryModel3D _tangentOutGeometry;

        public SampleViewModel(Sample sample)
        {
            Sample = sample;
            _name = string.Empty;

            _sphereGeometry = Helper3D.Helper3D.BuildSphere(new Point3D(), 0.2, Brushes.Tomato);
            _tangentInGeometry = Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(),  0.1, Brushes.LightBlue);
            _tangentOutGeometry = Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(), 0.1, Brushes.LightCoral);
            
            UpdateTransform(_sphereGeometry);
            UpdateTransform(_tangentInGeometry);
            UpdateTransform(_tangentOutGeometry);
            UpdateTangentGeometry(_tangentInGeometry, Sample.TIn, 0.1);
            UpdateTangentGeometry(_tangentOutGeometry, Sample.TOut, 0.1);

            Model = new Model3DGroup();
            Model.Children.Add(_sphereGeometry);
            Model.Children.Add(_tangentInGeometry);
            Model.Children.Add(_tangentOutGeometry);

            Sample.CoordonateChanged += Sample_CoordonatesChanged;
            Sample.AreTangentsContinousChanged += Sample_AreTangentsContinousChanged;
            Sample.TIn.CoordonateChanged += SampleTIn_CoordonatesChanged;
            Sample.TOut.CoordonateChanged += SampleTOut_CoordonatesChanged;
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
            UpdateTangentGeometry(_tangentInGeometry, Sample.TIn, 0.1);
            OnPropertyChanged(nameof(IX));
            OnPropertyChanged(nameof(IY));
            OnPropertyChanged(nameof(IZ));
            OnPropertyChanged(nameof(ILength));
        }

        private void SampleTOut_CoordonatesChanged(object? sender, EventArgs eventArgs)
        {
            UpdateTangentGeometry(_tangentOutGeometry, Sample.TOut, 0.1);
            OnPropertyChanged(nameof(OX));
            OnPropertyChanged(nameof(OY));
            OnPropertyChanged(nameof(OZ));
            OnPropertyChanged(nameof(OLength));
        }

        internal void Reset()
        {
            Sample.Reset();
        }

        private void UpdateTransform(GeometryModel3D geometryModel3D)
        {
            TranslateTransform3D transform = (TranslateTransform3D)geometryModel3D.Transform;
            transform.OffsetX = X;
            transform.OffsetY = Y;
            transform.OffsetZ = Z;
        }

        private void UpdateTangentGeometry(GeometryModel3D tangenteGeometry, Tangent tangente, double diameter)
        {
            tangenteGeometry.Geometry = Helper3D.Helper3D.BuildArrowGeometry(new Point3D(), tangente.GetPoint3D(), diameter);
        }

        internal Point3D GetSamplePoint3D()
        {
            return Sample.GetPoint3D();
        }

        public override void Dispose()
        {
            Sample.CoordonateChanged -= Sample_CoordonatesChanged;
            Sample.AreTangentsContinousChanged -= Sample_AreTangentsContinousChanged;
            Sample.TIn.CoordonateChanged -= SampleTIn_CoordonatesChanged;
            Sample.TOut.CoordonateChanged -= SampleTOut_CoordonatesChanged;
            base.Dispose();
        }

        public override string ToString()
        {
            return _name + " { " + $"{X} {Y} {Z}" + " }";
        }

        public SampleViewModel Clone()
        {
            return new SampleViewModel(Sample.Clone())
            {
                _name = _name,
            };
        }
    }
}
