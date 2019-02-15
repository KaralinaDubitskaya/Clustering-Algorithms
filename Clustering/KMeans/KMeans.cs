using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ClusterModel;

namespace KMeans
{
    public static class KMeans 
    {
        private const uint MIN_NUMBER_OF_POINTS = 1000;
        private const uint MAX_NUMBER_OF_POINTS = 100000;
        private const uint MIN_NUMBER_OF_CLASSES = 2;
        private const uint MAX_NUMBER_OF_CLASSES = 20;

        private static Random random = new Random();

        public static List<Cluster> Calculate(List<Point> points, int numberOfClasses)
        {
            if (points.Count < MIN_NUMBER_OF_POINTS || points.Count > MAX_NUMBER_OF_POINTS)
            {
                throw new ArgumentOutOfRangeException
                    ($"Number of points should be between {MIN_NUMBER_OF_POINTS} and {MAX_NUMBER_OF_POINTS}.");
            }
            else if (numberOfClasses < MIN_NUMBER_OF_CLASSES || numberOfClasses > MAX_NUMBER_OF_CLASSES)
            {
                throw new ArgumentOutOfRangeException
                    ($"Number of classes should be between {MIN_NUMBER_OF_CLASSES} and {MAX_NUMBER_OF_CLASSES}.");
            }
            else
            {
                // Generate clusters.
                var clusters = new List<Cluster>();
                for (int i = 0; i < numberOfClasses; i++)
                {
                    // Set random centers to the generated clusters.
                    clusters.Add(new Cluster(GetRandomPoint(points)));
                }

                bool areCentersRecalculated = false;
                do
                {
                    // Delete all points except the central one.
                    Clustering.ClearClusters(clusters);
                    // Add each point to the nearest cluster.
                    Clustering.AddPointsToClusters(clusters, points);
                    // Recalculate central points.
                    areCentersRecalculated = RecalculateCenters(clusters);
                }
                while (areCentersRecalculated);

                return clusters;
            }
        }

        private static Point GetRandomPoint(List<Point> points)
        {
            return points.ElementAt(random.Next(points.Count));
        }
        
        // Returns true if after recalculating some clusters changed their centers. 
        private static bool RecalculateCenters(IEnumerable<Cluster> clusters)
        {
            bool areCentersRecalculated = false;
            Parallel.ForEach(clusters, cluster =>
            {
                Point bestCenter = cluster.GetBestCenter();
                if (bestCenter != cluster.Center)
                {
                    cluster.Center = bestCenter;
                    areCentersRecalculated = true;
                }
            });

            return areCentersRecalculated;
        }
    }
}

