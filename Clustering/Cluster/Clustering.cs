using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClusterModel
{
    public static class Clustering
    {
        // Deletes all points of the cluster, leaving the only central one.
        public static void ClearClusters(List<Cluster> clusters)
        {
            foreach (var cluster in clusters)
            {
                cluster.Points = new List<Point> { cluster.Center };
            }
        }

        // Distributes the points between the classes.
        public static void AddPointsToClusters(IEnumerable<Cluster> clusters, IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                var cluster = GetCluster(point, clusters);
                cluster?.Points.Add(point);
            }
        }

        // Returns cluster for the point.
        private static Cluster GetCluster(Point point, IEnumerable<Cluster> clusters)
        {
            double minDistance = Double.MaxValue;
            Cluster resultCluster = null;

            foreach (var cluster in clusters)
            {
                // Skip the point if it's a center of the cluster.
                if (point == cluster.Center)
                {
                    return null;
                }

                double distance = cluster.GetDistance(point);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    resultCluster = cluster;
                }
            }

            return resultCluster;
        }
    }
}
