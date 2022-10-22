using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BezierCurves.Beziers;
using BezierCurves.Commands;
using HelixToolkit.Wpf;

namespace BezierCurves.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (SetValue(ref _selectedIndex, value))
                {
                    AddingPoint = _selectedIndex < 0 ? null : PointsCollection[_selectedIndex];
                }
            }
        }

        private int _bezierPrecision;
        public int BezierPrecision
        {
            get => _bezierPrecision;
            set
            {
                if (SetValue(ref _bezierPrecision, value))
                {
                    ComputeBezier3();
                }
            }
        }

        private bool _hasToComputeBezier3;
        public bool HasToComputeBezier3
        {
            get => _hasToComputeBezier3;
            set
            {
                if (SetValue(ref _hasToComputeBezier3, value))
                {
                    ComputeBezier3();
                }
            }
        }

        public ObservableCollection<SampleViewModel> PointsCollection { get; private set; }

        private SampleViewModel? _addingPoint;
        public SampleViewModel? AddingPoint
        {
            get => _addingPoint;
            set
            {
                if (_addingPoint != null && !PointsCollection.Contains(_addingPoint))
                {
                    _addingPoint.Dispose();
                }
                SetValue(ref _addingPoint, value);
            }
        }

        public NewPointCommand NewPointCommand { get; private set; }
        public DeletePointCommand DeletePointCommand { get; private set; }
        public AddPointCommand AddPointCommand { get; private set; }
        public DeselectPointCommand DeselectPointCommand { get; private set; }
        public ComputeBezier3Command ComputeBezier3Command { get; private set; }

        public Model3DGroup OriginModel { get; private set; }

        public Model3DGroup PointsModel { get; private set; }

        public GeometryModel3D Bezier3Model { get; private set; }

        public MainViewModel()
        {
            _selectedIndex = -1;
            _hasToComputeBezier3 = false;
            _bezierPrecision = 10;
            PointsCollection = new ObservableCollection<SampleViewModel>();
            NewPointCommand = new NewPointCommand(this);
            DeletePointCommand = new DeletePointCommand(this);
            AddPointCommand = new AddPointCommand(this);
            DeselectPointCommand = new DeselectPointCommand(this);
            ComputeBezier3Command = new ComputeBezier3Command(this);

            OriginModel = new Model3DGroup();
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(1, 0, 0), 0.1, Brushes.Red));
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 1, 0), 0.1, Brushes.Green));
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 0, 1), 0.1, Brushes.Blue));

            PointsModel = new Model3DGroup();

            Bezier3Model = new GeometryModel3D()
            {
                Material = MaterialHelper.CreateMaterial(Brushes.Gold)
            };

            PointsCollection.CollectionChanged += PointsCollection_CollectionChanged;
            _addingPoint = null;
        }

        private void PointsCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object? item in e.NewItems)
                    {
                        if (item is SampleViewModel sampleViewModel)
                        {
                            sampleViewModel.Sample.CoordonateChanged += AutoCompute;
                            sampleViewModel.Sample.TIn.CoordonateChanged += AutoCompute;
                            sampleViewModel.Sample.TOut.CoordonateChanged += AutoCompute;
                            PointsModel.Children.Add(sampleViewModel.Model);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (object? item in e.OldItems)
                    {
                        if (item is SampleViewModel sampleViewModel)
                        {
                            sampleViewModel.Sample.CoordonateChanged -= AutoCompute;
                            sampleViewModel.Sample.TIn.CoordonateChanged -= AutoCompute;
                            sampleViewModel.Sample.TOut.CoordonateChanged -= AutoCompute;
                            sampleViewModel.Dispose();

                            for (int i = 0; i < PointsModel.Children.Count; i++)
                            {
                                if (PointsModel.Children[i] == sampleViewModel.Model)
                                {
                                    PointsModel.Children.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                    }
                    break;
            }
            ComputeBezier3();
        }

        private void AutoCompute(object? sender, EventArgs e)
        {
            ComputeBezier3();
        }

        internal void AddPoint()
        {
            if (_addingPoint != null)
            {
                PointsCollection.Add(_addingPoint.Clone());
                DeselectPoint();
            }
        }

        internal void DeletePoint()
        {
            PointsCollection.Remove(_addingPoint);
        }

        internal void DeselectPoint()
        {
            SelectedIndex = -1;
            if (_addingPoint != null)
            {
                AddingPoint = null;
            }
        }

        internal void ComputeBezier3()
        {
            if (!_hasToComputeBezier3 || PointsCollection.Count <= 1)
            {
                Bezier3Model.Geometry = null;
                return;
            }
            Bezier3 bezier = new Bezier3(PointsCollection.Select(x => x.Sample).ToArray());
            bezier.Compute();
            Bezier3Model.Geometry = Helper3D.Helper3D.BuildBezier3(bezier, _bezierPrecision);
        }
    }
}
