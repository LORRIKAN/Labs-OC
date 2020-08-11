using System.Collections.Generic;
using System.IO;
using File = System.Collections.Generic.List<Lab2.Cluster>;

namespace Lab2
{
    static class ClusterOperations
    {
        public static List<Cluster> Read(string filePath)
        {
            var clusters = new List<Cluster>();
            using (var streamReader = new StreamReader(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    var cluster = streamReader.ReadLine();
                    clusters.Add(cluster);
                }
            }
            return clusters;
        }

        private static void FormFile(Cluster startCluster, List<Cluster> clusters, File file)
        {
            if (startCluster.IsBadEofOrEmpty())
                return;
            Cluster nextCluster = DeepCopy.DeepCopier.Copy(clusters[int.Parse(startCluster.NextСluster)]);
            file.Add(nextCluster);
            if (nextCluster.clusterType != ClusterTypes.Bad && nextCluster.clusterType != ClusterTypes.Empty
                && nextCluster.clusterType != ClusterTypes.Eof)
                FormFile(nextCluster, clusters, file);
        }

        public static List<File> FormListOfFiles(List<Cluster> clusters)
        {
            List<File> files = new List<File>();
            foreach (var cluster in clusters)
            {
                if (cluster.IsFirst(clusters))
                {
                    var file = new File
                    {
                        cluster
                    };
                    FormFile(cluster, clusters, file);
                    files.Add(file);
                }
            }
            return files;
        }
    }
}