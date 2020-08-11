using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static readonly string NL = Environment.NewLine;
        static void OutputInConsole(List<File> files, List<Cluster> badClusters,
            Dictionary<Cluster, List<File>> crossingClustersAndFiles, List<Cluster> lostClusters)
        {
            foreach (var file in files)
                Console.WriteLine(file.GetFileInStr() + NL);

            if (badClusters.Any())
            {
                Console.WriteLine("Bad-кластеры:");
                foreach (var badCluster in badClusters)
                    Console.WriteLine(badCluster);
            }

            if (crossingClustersAndFiles.Any())
            {
                Console.WriteLine(NL + "Пересекающиеся кластеры:");

                foreach (var crossingCluster in crossingClustersAndFiles.Keys)
                {
                    Console.WriteLine(crossingCluster + " Файлы, задействующие этот кластер:");
                    foreach (var file in crossingClustersAndFiles[crossingCluster])
                        Console.WriteLine(file.FileName);
                }
            }

            if (lostClusters.Any())
            {
                Console.WriteLine(NL + "Потерянные кластеры:");
                foreach (var missingCluster in lostClusters)
                    Console.WriteLine(missingCluster);
            }
        }

        static void Main()
        {
            var filesAndClusters = FileWorker.GetFilesAndClusters();
            var files = filesAndClusters.Item1;
            var clusters = filesAndClusters.Item2;
            files = FileWorker.LinkClustersToFiles(files, clusters);

            var badClusters = ClusterChecker.GetBadClusters(clusters);
            var crossingClustersAndFiles = ClusterChecker.GetCrossingClustersAndFiles(files);
            var lostClusters = ClusterChecker.GetMissingClusters(files, clusters);

            OutputInConsole(files, badClusters, crossingClustersAndFiles, lostClusters);
        }
    }
}