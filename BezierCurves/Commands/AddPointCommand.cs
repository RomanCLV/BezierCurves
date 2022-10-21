using BezierCurves.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierCurves.Commands
{
    internal class AddPointCommand : CommandBase
    {
        private readonly MainViewModel _mainViewModel;

        public AddPointCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object? parameter)
        {
            _mainViewModel.AddPoint();
        }
    }
}
