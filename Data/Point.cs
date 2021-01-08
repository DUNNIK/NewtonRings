using System.Collections.Generic;

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
    public class PointComparerByI : IComparer<Point>
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
    public class PointComparerByR : IComparer<Point>
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
}