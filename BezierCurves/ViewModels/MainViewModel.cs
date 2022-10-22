using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using BezierCurves.Commands;

namespace BezierCurves.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetValue(ref _selectedIndex, value);
        }

        public ObservableCollection<SampleViewModel> PointsCollection { get; private set; }

        private SampleViewModel? _addingPoint;
        public SampleViewModel? AddingPoint
        {
            get => _addingPoint;
            set
            {
                _addingPoint?.Dispose();
                SetValue(ref _addingPoint, value);
            }
        }

        public NewPointCommand NewPointCommand { get; private set; }
        public DeletePointCommand DeletePointCommand { get; private set; }
        public AddPointCommand AddPointCommand { get; private set; }
        public DeselectPointCommand DeselectPointCommand { get; private set; }

        public Model3D OriginModel { get; private set; }

        public MainViewModel()
        {
            _selectedIndex = -1;
            PointsCollection = new ObservableCollection<SampleViewModel>();
            NewPointCommand = new NewPointCommand(this);
            DeletePointCommand = new DeletePointCommand(this);
            AddPointCommand = new AddPointCommand(this);
            DeselectPointCommand = new DeselectPointCommand(this);

            Model3DGroup model3DGroup = new Model3DGroup();
            model3DGroup.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(1, 0, 0), 0.1, Brushes.Red));
            model3DGroup.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 1, 0), 0.1, Brushes.Green));
            model3DGroup.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 0, 1), 0.1, Brushes.Blue));
            OriginModel = model3DGroup;

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
                            sampleViewModel.PropertyChanged += SampleViewModel_PropertyChanged;
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    foreach (object? item in e.OldItems)
                    {
                        if (item is SampleViewModel sampleViewModel)
                        {
                            sampleViewModel.PropertyChanged -= SampleViewModel_PropertyChanged;
                        }
                    }
                    break;
            }
        }

        private void SampleViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(PointsCollection));
        }

        internal void AddPoint()
        {
            if (_addingPoint != null)
            {
                PointsCollection.Add(_addingPoint.Clone());
                AddingPoint = null;
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
    }
}
