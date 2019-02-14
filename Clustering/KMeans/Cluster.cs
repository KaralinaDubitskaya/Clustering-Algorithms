using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMeans
{
    // Represent group of points and defines it center.
    public class Cluster
    {
        public List<Point> Points { get; set; }
        public Point Center { get; set; }

        public Cluster(Point center)
        {
            this.Points = new List<Point>();
            this.Points.Add(center);
            this.Center = center;
        }

        // Return a distance value between the point and the center of the class (cluster).
        public double GetDistance(Point point)
        {
            return GetDistance(this.Center, point);
        }

        // Returns the best center candidate.
        public Point GetBestCenter()
        {
            Point bestCenter = new Point(Points.Average(point => point.X), Points.Average(point => point.Y));
            double minDistance = double.MaxValue;
            Point result = new Point();
            foreach (var point in Points)
            {
                double distance = GetDistance(point, bestCenter);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = point;
                }
            }
            return result;
        }

        // Return a distance value between the points.
        private double GetDistance(Point pointA, Point pointB)
        {
            if (pointA == null || pointB == null)
            {
                throw new ArgumentNullException();
            }

            double dx = pointA.X - pointB.X;
            double dy = pointA.Y - pointB.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
