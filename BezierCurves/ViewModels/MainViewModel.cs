using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private IEnumerable<SampleViewModel> _pointsCollections;

        public ObservableCollection<SampleViewModel> PointsCollections { get; private set; }

        public SampleViewModel? AddingPoint { get; set; }

        public MainViewModel()
        {
            _pointsCollections = new List<SampleViewModel>();
            PointsCollections = new ObservableCollection<SampleViewModel>();

            AddingPoint = null;
        }
    }
}
