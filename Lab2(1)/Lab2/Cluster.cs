using static Lab2.ClusterType;

namespace Lab2
{
    public class Cluster
    {
        public int ClusterNum { get; }

        public int NextClusterNum { get; }

        public string ClusterType { get; set; }

        public Cluster(string clusterStr, int clusterNum)
        {
            SetClusterType(clusterStr);

            if (this.ClusterType == Normal)
                this.NextClusterNum = int.Parse(clusterStr);

            ClusterNum = clusterNum;
        }

        public static implicit operator string(Cluster cluster) =>
            $"Кластер {cluster.ClusterNum}. Тип \"{cluster.ClusterType}\".";

        public static bool operator !=(Cluster cluster1, Cluster cluster2)
        {
            return !(cluster1 == cluster2);
        }

        public static bool operator ==(Cluster cluster1, Cluster cluster2)
        {
            return cluster1.ClusterNum == cluster2.ClusterNum;
        }

        private void SetClusterType(string clusterStr)
        {
            string loweredClusterStr = clusterStr.ToLower();

            if (loweredClusterStr.Contains("bad"))
                ClusterType = Bad;
            else if (loweredClusterStr.Contains("eof"))
                ClusterType = Eof;
            else if (int.TryParse(loweredClusterStr, out int _))
                ClusterType = Normal;
            else
                ClusterType = Empty;
        }
    }

    public static class ClusterType
    {
        public static string Normal => "Нормальный кластер";
        public static string Bad => "Bad-кластер";
        public static string Eof => "Конец файла";
        public static string Empty => "Пустой кластер";
    }
}