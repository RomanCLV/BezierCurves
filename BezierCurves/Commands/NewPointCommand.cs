using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BezierCurves.ViewModels;

namespace BezierCurves.Commands
{
    internal class NewPointCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public NewPointCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object? parameter)
        {
            _mainViewModel.AddingPoint?.Dispose();
            _mainViewModel.AddingPoint = new SampleViewModel(new Models.Sample());
        }
    }
}
