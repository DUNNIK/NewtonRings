using System.Collections.Generic;
using Emgu.CV;

namespace Data
{
    public class Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class PointComparerByY : IComparer<Point>
    {
        public int Compare(Point point1, Point point2)
        {
            if (point2 != null && point1 != null && point1.Y >= point2.Y)
            {
                return 1;
            }
            else if (point2 != null && point1 != null && point1.Y < point2.Y)
            {
                return -1;
            }
 
            return 0;
        }
    }
    public class PointComparerByX : IComparer<Point>
    {
        public int Compare(Point point1, Point point2)
        {
            if (point2 != null && point1 != null && point1.X >= point2.X)
            {
                return 1;
            }
            else if (point2 != null && point1 != null && point1.X < point2.X)
            {
                return -1;
            }
 
            return 0;
        }
    }

    public class Points : List<Point>
    {
        public Points(List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Add(points[i]);
            }
        }
        public System.Drawing.Point[] ConvertToDrawingPoints()
        {
            var result = new System.Drawing.Point[this.Count];
            for (int i = 0; i < Count; i++)
            {
                result[i].X = (int)this[i].X;
                result[i].Y = (int)this[i].Y;
            }

            return result;
        }
    }
}