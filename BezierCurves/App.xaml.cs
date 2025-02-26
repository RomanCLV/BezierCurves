﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BezierCurves.ViewModels;
using BezierCurves.Views;

namespace BezierCurves
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow
            {
                DataContext = new MainViewModel()
            };
            window.Show();

            base.OnStartup(e);
        }
    }
}
