using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using HelixToolkit.Wpf;
using BezierCurves.Beziers;
using System.Diagnostics;

namespace BezierCurves.Helper3D
{
    internal static class Helper3D
    {
        internal static GeometryModel3D BuildSphere(Point3D center, double radius, Brush color)
        {
            MeshBuilder builder = new MeshBuilder(false, false);
            builder.AddSphere(center, radius);
            return BuildMesh(builder, color);
        }

        internal static GeometryModel3D BuildArrow(Point3D point1, Point3D point2, double diameter, Brush color)
        {
            return BuildMesh(BuildArrowGeometry(point1, point2, diameter), color);
        }

        internal static Geometry3D BuildArrowGeometry(Point3D point1, Point3D point2, double diameter)
        {
            MeshBuilder builder = new MeshBuilder(false, false);
            builder.AddArrow(point1, point2, diameter);
            return builder.ToMesh(true);
        }

        private static GeometryModel3D BuildMesh(MeshBuilder builder, Brush color)
        {
            // Create a mesh from the builder (and freeze it)
            MeshGeometry3D mesh = builder.ToMesh(true);
            return BuildMesh(mesh, color);
        }

        private static GeometryModel3D BuildMesh(Geometry3D geometry, Brush color)
        {
            // Create some materials
            Material material = MaterialHelper.CreateMaterial(color);

            return new GeometryModel3D
            {
                Geometry = geometry,
                Material = material,
                BackMaterial = material,
                Transform = new TranslateTransform3D()
            };
        }

        internal static Geometry3D BuildBezier3(Bezier3 bezier, int precision = 10)
        {
            MeshBuilder builder = new MeshBuilder(false, false);
            double step = 1.0 / precision;
            Point3D current;
            Point3D next;
            foreach (Bezier3Coefficients3D item in bezier.Bezier3Coefficients3D)
            {
                current = ComputePoint3D(item, 0);
                for (int i = 0; i < precision; i++)
                {
                    next = ComputePoint3D(item, (i + 1) * step);
                    builder.AddCylinder(current, next, 0.05);
                    current = next;
                }
            }
            return builder.ToMesh(true);
        }

        private static Point3D ComputePoint3D(Bezier3Coefficients3D bezier3Coefficients3D, double t)
        {
            double x = bezier3Coefficients3D.XCoefficients.A * Math.Pow(t, 3) +
                       bezier3Coefficients3D.XCoefficients.B * Math.Pow(t, 2) +
                       bezier3Coefficients3D.XCoefficients.C * Math.Pow(t, 1) +
                       bezier3Coefficients3D.XCoefficients.D;

            double y = bezier3Coefficients3D.YCoefficients.A * Math.Pow(t, 3) +
                       bezier3Coefficients3D.YCoefficients.B * Math.Pow(t, 2) +
                       bezier3Coefficients3D.YCoefficients.C * Math.Pow(t, 1) +
                       bezier3Coefficients3D.YCoefficients.D;

            double z = bezier3Coefficients3D.ZCoefficients.A * Math.Pow(t, 3) +
                       bezier3Coefficients3D.ZCoefficients.B * Math.Pow(t, 2) +
                       bezier3Coefficients3D.ZCoefficients.C * Math.Pow(t, 1) +
                       bezier3Coefficients3D.ZCoefficients.D;
            return new Point3D(x, y, z);
        }
    }
}
