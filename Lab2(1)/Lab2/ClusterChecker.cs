using System.Collections.Generic;
using System.Linq;
using static Lab2.ClusterType;

namespace Lab2
{
    public static class ClusterChecker
    {
        public static List<Cluster> GetBadClusters(List<Cluster> clusters)
        {
            var badClusters = new List<Cluster>();

            foreach (var cluster in clusters)
                if (cluster.ClusterType == Bad)
                    badClusters.Add(cluster);

            return badClusters;
        }

        public static Dictionary<Cluster, List<File>> GetCrossingClustersAndFiles(List<File> files)
        {
            var crossingFilesAndClusters = new Dictionary<Cluster, List<File>>();

            foreach (var file in files)
            {
                foreach (var cluster in file.Clusters)
                {
                    // проверка, содержит ли уже словарь выбранный кластер
                    if (!crossingFilesAndClusters.Any(d => d.Key == cluster))
                    {
                        var crossingFiles = files.Where(f => f.Clusters.Any(c => c == cluster) && f != file)
                            .ToList();

                        if (crossingFiles.Any())
                            crossingFilesAndClusters.Add(cluster, crossingFiles.Prepend(file).ToList());
                    }
                }
            }

            return crossingFilesAndClusters;
        }

        public static List<Cluster> GetMissingClusters(List<File> files, List<Cluster> clusters)
        {
            var missingClusters = new List<Cluster>();

            foreach (var cluster in clusters)
            {
                if (!files.Any(f => f.Clusters.Any(c => c == cluster)) && cluster.ClusterType != Empty)
                    missingClusters.Add(cluster);
            }

            return missingClusters;
        }
    }
}