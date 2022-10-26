using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BezierCurves.Models;

namespace BezierCurves.Beziers
{
    internal class Bezier3
    {
        private Sample[] _samples;

        public Bezier3Coefficients3D[] Bezier3Coefficients3D { get; private set; }

        public Bezier3()
        {
            _samples = new Sample[0];
            Bezier3Coefficients3D = new Bezier3Coefficients3D[0]; 
        }

        public Bezier3(Sample[] samples) : this()
        {
            SetSamples(samples);
        }

        internal void SetSamples(Sample[] samples)
        {
            _samples = samples;
            Bezier3Coefficients3D = new Bezier3Coefficients3D[_samples.Count() - 1];
        }

        internal void Compute()
        {
            for (int i = 0; i < Bezier3Coefficients3D.Length; i++)
            {
                Bezier3Coefficients3D[i] = new Bezier3Coefficients3D(_samples[i], _samples[i + 1]);
                Bezier3Coefficients3D[i].Compute();
            }
        }
    }
}
