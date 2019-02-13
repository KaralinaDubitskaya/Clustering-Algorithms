using Clustering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace k_means
{
    public class ApplicationViewModel
    {
        private Command _kMeansCommand;

        public Command KMeansCommand
        {
            get
            {
                return _kMeansCommand ?? 
                    (_kMeansCommand = new Command(KMeansClustering));
            }
        }

        private void KMeansClustering()
        {

        }
    }
}
