using BezierCurves.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Commands
{
    internal class ComputeBezier3Command : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public ComputeBezier3Command(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _mainViewModel.PointsCollection.CollectionChanged += PointsCollection_CollectionChanged;
        }

        private void PointsCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            return _mainViewModel.PointsCollection.Count > 1 && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _mainViewModel.ComputeBezier3();
        }
    }
}
