using Clustering.Models;
using KMeans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace k_means
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private int _screenWidth;
        private int _screenHeight;

        public int NumberOfPoints { get; set; }
        public int NumberOfClasses { get; set; }

        public DrawingImage DrawingImage { get; set; }

        private Command _kMeansCommand;
        public Command KMeansCommand
        {
            get
            {
                return _kMeansCommand ?? 
                    (_kMeansCommand = new Command(KMeansClustering));
            }
        }

        public ApplicationViewModel(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            DrawingImage = new DrawingImage();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void KMeansClustering()
        {
            try
            {
                List<Point> points = GetRandomPoints(NumberOfPoints, _screenWidth, _screenHeight);
                List<Cluster> clusters = KMeans.KMeans.Calculate(points, NumberOfClasses);
                DisplayClusters(clusters);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Ooops, some error occured :(", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Randomize random points for the specified screen.
        private List<Point> GetRandomPoints(int count, int width, int height)
        {
            Random random = new Random();
            List<Point> points = new List<Point>();
            for (int i = 0; i < count; i++)
            {
                var point = new Point(random.Next(width), random.Next(height));
                points.Add(point);
            }
            return points;
        }
        
        private void DisplayClusters(List<Cluster> clusters)
        {
            var drawingGroup = new DrawingGroup();
            var random = new Random();

            foreach (var cluster in clusters)
            {
                Color randomColor = Color.FromRgb
                    ((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
                DrawCluster(drawingGroup, cluster, randomColor);
            }

            DrawingImage = new DrawingImage(drawingGroup);
            OnPropertyChanged("DrawingImage");
        }

        private void DrawCluster(DrawingGroup drawingGroup, Cluster cluster, Color color)
        {
            var drawing = new GeometryGroup();
            var brush = new SolidColorBrush(color);
            
            foreach (var point in cluster.Points)
            {
                var radius = (point == cluster.Center) ? 6 : 1;
                drawing.Children.Add(new EllipseGeometry(point, radius, radius));
            }

            drawingGroup.Children.Add(new GeometryDrawing(brush, new Pen(brush, 1), drawing));
        }
    }
}
