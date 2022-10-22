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
            set
            {
                if (SetValue(ref _selectedIndex, value))
                {
                    AddingPoint = _selectedIndex < 0 ? null : PointsCollection[_selectedIndex];
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
                _addingPoint?.Dispose();
                SetValue(ref _addingPoint, value);
            }
        }

        public NewPointCommand NewPointCommand { get; private set; }
        public DeletePointCommand DeletePointCommand { get; private set; }
        public AddPointCommand AddPointCommand { get; private set; }
        public DeselectPointCommand DeselectPointCommand { get; private set; }

        public Model3DGroup OriginModel { get; private set; }

        public Model3DGroup PointsModel { get; private set; }

        public MainViewModel()
        {
            _selectedIndex = -1;
            PointsCollection = new ObservableCollection<SampleViewModel>();
            NewPointCommand = new NewPointCommand(this);
            DeletePointCommand = new DeletePointCommand(this);
            AddPointCommand = new AddPointCommand(this);
            DeselectPointCommand = new DeselectPointCommand(this);

            OriginModel = new Model3DGroup();
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(1, 0, 0), 0.1, Brushes.Red));
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 1, 0), 0.1, Brushes.Green));
            OriginModel.Children.Add(Helper3D.Helper3D.BuildArrow(new Point3D(), new Point3D(0, 0, 1), 0.1, Brushes.Blue));

            PointsModel = new Model3DGroup();

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
                            sampleViewModel.PropertyChanged -= SampleViewModel_PropertyChanged;
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
    }
}
