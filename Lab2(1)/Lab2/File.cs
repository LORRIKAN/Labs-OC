using System;
using System.Collections.Generic;
using static Lab2.ClusterType;

namespace Lab2
{
    public class File
    {
        readonly string NL = Environment.NewLine;

        public int FirstClusterNum { get; }

        public List<Cluster> Clusters { get; } = new List<Cluster>();

        public string FileName { get; }

        public File(string fileName, string firstClusterNum)
        {
            FileName = fileName;
            FirstClusterNum = int.Parse(firstClusterNum);
        }

        public string GetFileInStr()
        {
            string fileStr = $"Файл \"{FileName}\":" + NL;

            foreach (var cluster in Clusters)
            {
                fileStr += cluster;
                fileStr += NL;

                if (cluster.ClusterType == Eof)
                {
                    fileStr += $"Файл \"{FileName}\" закончен на {cluster.ClusterNum} кластере и не повреждён.";
                }

                else if (cluster.ClusterType != Normal)
                {
                    fileStr += $"Файл \"{FileName}\" повреждён, так как тип его последнего кластера " +
                        $"\"{cluster.ClusterType}\".";
                    break;
                }
            }

            return fileStr;
        }
    }
}