using System;
using System.Collections.Generic;
using System.IO;
using static Lab2.ClusterType;

namespace Lab2
{
    public static class FileWorker
    {
        public static Tuple<List<File>, List<Cluster>> GetFilesAndClusters()
        {
            var files = new List<File>();
            var clusters = new List<Cluster>();

            using (var reader = new StreamReader("1.txt"))
            {
                bool filesTableRead = false;
                while (!filesTableRead)
                {
                    var str = reader.ReadLine();
                    if (str.Contains("."))
                    {
                        var file = str.Split('.');
                        var fileName = file[0];
                        var firstClusterNum = file[1];
                        files.Add(new File(fileName, firstClusterNum));
                    }
                    else
                        filesTableRead = true;
                }

                int clustersCounter = 0;
                while (!reader.EndOfStream)
                {
                    var cluster = reader.ReadLine();
                    clusters.Add(new Cluster(cluster, clustersCounter));
                    ++clustersCounter;
                }
            }

            return new Tuple<List<File>, List<Cluster>>(files, clusters);
        }

        private static void LinkClustersToFile(File file, Cluster cluster, List<Cluster> clusters)
        {
            file.Clusters.Add(cluster);

            if (cluster.ClusterType == Normal)
            {
                var nextClusterNum = cluster.NextClusterNum;
                var nextCluster = clusters[nextClusterNum];
                LinkClustersToFile(file, nextCluster, clusters);
            }
        }

        public static List<File> LinkClustersToFiles(List<File> files, List<Cluster> clusters)
        {
            foreach (var file in files)
            {
                var firstCluster = clusters[file.FirstClusterNum];
                LinkClustersToFile(file, firstCluster, clusters);
            }

            return files;
        }
    }
}