using System.Collections.Generic;
using System.Linq;
using Data;
using TinyCsvParser.Mapping;

namespace BLL
{
    public static class DataHandler
    {
        public static List<Point> FindMaxInAllData(List<Point> points)
        {
            var result = new List<Point>();
            for (var i = 1; i < points.Count - 1; i++)
            {
                var previous = points[i - 1];
                var current = points[i];
                var next = points[i + 1];
                if (current.X > previous.X && current.X < next.X && current.Y > previous.Y && current.Y > next.Y)
                {
                    result.Add(current);
                }
            }

            return result;
        }
        public static List<Point> FindMinInAllData(List<Point> points)
        {
            var result = new List<Point>();
            for (var i = 1; i < points.Count - 1; i++)
            {
                var previous = points[i - 1];
                var current = points[i];
                var next = points[i + 1];
                if (current.X > previous.X && current.X < next.X && current.Y < previous.Y && current.Y < next.Y)
                {
                    result.Add(current);
                }
            }

            return result;
        }

        public static List<Point> LinePointsInRadiusDependence(IEnumerable<Point> max, IEnumerable<Point> min)
        {
            var union = max.Union(min).ToList();
            var compare = new PointComparerByR();
            union.Sort(compare);

            return union.Select((t, i) => new Point(i, t.X * t.X)).ToList();
        }

        public static double FindRByN(List<Point> maxOrMin, double lambda)
        {
            return (maxOrMin[2].X * maxOrMin[2].X - maxOrMin[0].X * maxOrMin[0].X) / (2 * lambda) * 1000;
        }
    }
}