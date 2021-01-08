using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Data;

namespace BLL
{
    public static class VisibilityFunctionMethods
    {
        public static List<double> FindRAverage(IEnumerable<Point> max, List<Point> min)
        {
            return max.Select((t, i) => t.X + min[i].X).ToList();
        }

        public static List<double> FindVExperimental(IEnumerable<Point> max, List<Point> min)
        {
            return max.Select((t, i) => (t.Y - min[i].Y) / (t.Y + min[i].Y)).ToList();
        }

        public static List<double> OpticalPathDifference(IEnumerable<double> rAverage, double r)
        {
            return rAverage.Select(t => t * t / r).ToList();
        }

        public static List<double> FindVTheoretical(List<double> opticalPathDifference, double l, double dL)
        {
            var result = new List<double>();
            for (int i = 0; i < opticalPathDifference.Count; i++)
            {
                var a = Math.Abs(Math.Sin(dL * Math.PI * opticalPathDifference[i] / l / l) /
                                 (dL * Math.PI * opticalPathDifference[i] / l / l));
                result.Add(a);
            }

            return result;
        }

        public static double FindL(List<double> opticalPathDifference)
        {
            return opticalPathDifference[opticalPathDifference.Count / 2] / 6;
        }

        public static double FindErrorL(List<double> opticalPathDifference, double l)
        {
            return l * l / opticalPathDifference[opticalPathDifference.Count / 2];
        }
        public static List<Point> CreatePointForVisibility(List<double> x, List<double> y)
        {
            return x.Select((t, i) => new Point(t, y[i])).ToList();
        }
    }
}