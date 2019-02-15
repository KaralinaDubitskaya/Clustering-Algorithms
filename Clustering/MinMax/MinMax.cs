using ClusterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MinMax
{
    public static class MinMax
    {
        private static Random random = new Random();

        public static List<Cluster> Calculate(List<Point> points)
        {
            Point firstCenter = points[random.Next(points.Count)];
            List<Cluster> clusters = new List<Cluster>() { new Cluster(firstCenter) };

            bool isCompleted = false;
            do
            {
                Clustering.ClearClusters(clusters);
                Clustering.AddPointsToClusters(clusters, points);
                Point? newCenter = GetNewCenter(clusters);

                if (newCenter != null)
                {
                    clusters.Add(new Cluster((Point)newCenter));
                }
                else
                {
                    isCompleted = true;
                }
            }
            while (!isCompleted);

            return clusters;
        }

        // Returns null if clustering is completed otherwise returns a new center.
        private static Point? GetNewCenter(List<Cluster> clusters)
        {
            double averageDistance = GetAverageCenterDistance(clusters);
            (Point point, double maxDistance) mostDistantPoint = GetMostDistantPoint(clusters);
            if (mostDistantPoint.maxDistance > averageDistance / 2)
            {
                return mostDistantPoint.point;
            }
            return null;
        }

        // Returns an average distance between the clusters's centers. 
        private static double GetAverageCenterDistance(List<Cluster> clusters)
        {
            double sum = 0;
            for (int i = 0; i < clusters.Count; i++)
            {
                for (int j = i + 1; j < clusters.Count; j++)
                {
                    sum += clusters[i].GetDistance(clusters[j].Center);
                }
            }

            int count = Enumerable.Range(1, clusters.Count - 1).Sum();
            return (count == 0) ? 0 : sum / count;
        }

        // Returns the most distant point for the given clusters.
        private static (Point point, double maxDistance) GetMostDistantPoint(List<Cluster> clusters)
        {
            double maxDistance = 0;
            Point maxPoint = new Point();

            foreach (var cluster in clusters)
            {
                (Point point, double maxDistance) distantPoint = GetMostDistantPoint(cluster);
                if (distantPoint.maxDistance > maxDistance)
                {
                    maxDistance = distantPoint.maxDistance;
                    maxPoint = distantPoint.point;
                }
            }

            return (point: maxPoint, maxDistance: maxDistance);
        }

        // Returns the most distant point from the center.
        private static (Point point, double maxDistance) GetMostDistantPoint(Cluster cluster)
        {
            double maxDistance = 0;
            Point maxPoint = new Point();
            
            foreach (var point in cluster.Points)
            {
                double distance = cluster.GetDistance(point);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    maxPoint = point;
                }
            }

            return (point: maxPoint, maxDistance: maxDistance);
        }
    }
}
