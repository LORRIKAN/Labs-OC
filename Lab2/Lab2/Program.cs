using System;
using System.Collections.Generic;
using File = System.Collections.Generic.List<Lab2.Cluster>;

namespace Lab2
{
    class Program
    {
        static string NL = Environment.NewLine;
        static void OutputListInConsole(List<File> files)
        {
            var usedClusters = new List<Cluster>();
            bool intersection = false;
            for (int i = 0; i < files.Count; ++i)
            {
                var file = files[i];
                Cluster cluster = new Cluster("");
                Console.Write($"Файл #{i + 1}: ");
                for (int j = 0; j < file.Count; ++j)
                {
                    cluster = file[j];
                    Console.Write(cluster.NextСluster + " ");
                    if (usedClusters.Contains(cluster))
                        intersection = true;
                    usedClusters.Add(cluster);
                }
                if (intersection)
                {
                    Console.WriteLine("  |Файл пересекается с другим");
                    intersection = false;
                    continue;
                }
                switch (cluster.clusterType)
                {
                    case ClusterTypes.Bad:
                        Console.WriteLine(" |Файл повреждён");
                        break;
                    case ClusterTypes.Eof:
                        Console.WriteLine(" |Файл в порядке");
                        break;
                    case ClusterTypes.Empty:
                        Console.WriteLine(" |Файл не закончен");
                        break;
                }
            }
            Console.WriteLine();
        }

        static List<File> Fix(List<File> listOfFiles)
        {
            var fixedListOfFiles = new List<File>();
            int i = 0;
            foreach (var file in listOfFiles)
            {
                var newFile = new File();
                foreach (var cluster in file)
                {
                    newFile.Add(cluster);
                    ++i;
                    if (cluster.clusterType != ClusterTypes.Eof && cluster.clusterType != ClusterTypes.Bad)
                        cluster.NextСluster = i.ToString();
                }
                fixedListOfFiles.Add(newFile);
            }
            return fixedListOfFiles;
        }

        static void Main()
        {
            while (true)
            {
                string filePath = GetFilePath.GetFilePathForRead();
                var allClusters = ClusterOperations.Read(filePath);
                var listOfFiles = ClusterOperations.FormListOfFiles(allClusters);
                OutputListInConsole(listOfFiles);
                ColorPrint.PrintWithColor(ConsoleColor.Green, $"Исправленная файловая система{NL}");
                OutputListInConsole(Fix(listOfFiles));
            }
        }
    }
}