using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab3
{
    public static class GetPagesAndRequests
    {
        public static Tuple<List<int>, List<int>> ReadFromFile()
        {
            var pages = new List<int>();
            var requests = new List<int>();
            using (var reader = new StreamReader(@".\PagesAndRequests.txt"))
            {
                var str = reader.ReadLine();
                pages = str.Split(' ').ToList().Select(a => int.Parse(a)).ToList();
                str = reader.ReadLine();
                requests = str.Split(' ').ToList().Select(a => int.Parse(a)).ToList();
            }
            return new Tuple<List<int>, List<int>>(pages, requests);
        }
    }
}