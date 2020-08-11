using System;
using System.Collections.Generic;

namespace Lab2
{
    enum ClusterTypes
    {
        Normal,
        Eof,
        Bad,
        Empty
    }
    class Cluster
    {
        public string NextСluster { get; set; }
        public ClusterTypes clusterType;

        public Cluster() { }

        public Cluster(string cluster)
        {
            if (IsCluster(cluster))
                SetCluster(cluster);
            else
                throw new Exception("Неверный формат кластера!");
        }

        public static implicit operator Cluster(string cluster)
        {
            try
            {
                return new Cluster(cluster);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool IsFirst(List<Cluster> clusters)
        {
            if (clusterType == ClusterTypes.Empty)
                return false;
            string indexOfCluster = clusters.IndexOf(this).ToString();
            foreach (var cluster1 in clusters)
                if (cluster1.NextСluster == indexOfCluster)
                    return false;
            return true;
        }

        public bool IsBadEofOrEmpty()
        {
            if (clusterType == ClusterTypes.Bad || clusterType == ClusterTypes.Empty || clusterType == ClusterTypes.Eof)
                return true;
            else
                return false;
        }

        void SetCluster(string cluster)
        {
            if (string.IsNullOrWhiteSpace(cluster))
                clusterType = ClusterTypes.Empty;

            if (int.TryParse(cluster, out int _))
                clusterType = ClusterTypes.Normal;

            if (cluster == "bad")
                clusterType = ClusterTypes.Bad;

            if (cluster == "eof")
                clusterType = ClusterTypes.Eof;

            NextСluster = cluster;
        }

        bool IsCluster(string cluster)
        {
            cluster = cluster.ToLower();
            if (int.TryParse(cluster, out int _) || cluster == "eof" || cluster == "bad" || string.IsNullOrWhiteSpace(cluster))
                return true;
            else
                return false;
        }
    }
}