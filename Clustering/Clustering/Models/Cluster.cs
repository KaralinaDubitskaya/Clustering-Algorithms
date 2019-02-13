using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Clustering.Models
{
    // Represent group of points and defines it center.
    public class Cluster
    {
        public List<Point> Points { get; set; }
        public Point Center { get; private set; }
    }
}
