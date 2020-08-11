using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void OutputInConsole(IEnumerable<int> enumerable)
        {
            foreach (var element in enumerable)
                Console.Write(element + " ");
        }

        static int FIFO(Tuple<List<int>, List<int>> pagesAndRequests, int blocksNum)
        {
            ColorPrint.PrintWithColor(ConsoleColor.Green, "FIFO");
            Console.WriteLine(" with " + blocksNum + " blocks");

            var pages = pagesAndRequests.Item1;
            var requests = pagesAndRequests.Item2;

            var pagesQueue = new Queue<int>(pages);

            int pageFaults = 0;

            OutputInConsole(pagesQueue);
            Console.WriteLine();

            foreach (var request in requests)
            {
                if (pagesQueue.Contains(request))
                {
                    OutputInConsole(pagesQueue);
                    Console.Write("<- " + request);
                }
                else if (!pagesQueue.Contains(request) && pagesQueue.Count == blocksNum)
                {
                    pagesQueue.Dequeue();
                    pagesQueue.Enqueue(request);

                    OutputInConsole(pagesQueue);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, "<- ");
                    Console.Write(request);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, " p");

                    ++pageFaults;
                }
                else if (!pagesQueue.Contains(request) && pagesQueue.Count < blocksNum)
                {
                    pagesQueue.Enqueue(request);

                    OutputInConsole(pagesQueue);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, "<- ");
                    Console.Write(request);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, " p");

                    ++pageFaults;
                }
                Console.WriteLine();
            }
            Console.WriteLine("Page faults: " + pageFaults);
            return pageFaults;
        }

        static void MoveAndCompress(int indexOfValue, int indexToMoveTo, List<int> collection)
        {
            int value = collection[indexOfValue];
            for (int i = indexOfValue; i < indexToMoveTo; ++i)
                collection[i] = collection[i + 1];
            collection[indexToMoveTo] = value;
        }

        static int LRU(Tuple<List<int>, List<int>> pagesAndRequests, int blocksNum)
        {
            ColorPrint.PrintWithColor(ConsoleColor.Yellow, "LRU");
            Console.WriteLine(" with " + blocksNum + " blocks");

            var pages = new List<int>(pagesAndRequests.Item1);
            var requests = pagesAndRequests.Item2;

            int pageFaults = 0;

            OutputInConsole(pages);
            Console.WriteLine();

            foreach (var request in requests)
            {

                if (pages.Contains(request))
                {
                    var indexOfRequest = pages.IndexOf(request);
                    MoveAndCompress(indexOfRequest, pages.Count - 1, pages);

                    OutputInConsole(pages);
                    Console.Write("<- " + request);
                }
                else if (!pages.Contains(request) && pages.Count == blocksNum)
                {
                    pages.RemoveAt(0);
                    pages.Add(request);

                    OutputInConsole(pages);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, "<- ");
                    Console.Write(request);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, " p");

                    ++pageFaults;
                }
                else if (!pages.Contains(request) && pages.Count < blocksNum)
                {
                    pages.Add(request);

                    OutputInConsole(pages);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, "<- ");
                    Console.Write(request);
                    ColorPrint.PrintWithColor(ConsoleColor.Red, " p");

                    ++pageFaults;
                }
                Console.WriteLine();
            }
            Console.WriteLine("Page faults: " + pageFaults);
            return pageFaults;
        }

        static void OutputBestAlgorithm(int pageFaultsFIFO, int pageFaultsLRU)
        {
            if (pageFaultsFIFO < pageFaultsLRU)
                Console.WriteLine("Наиболее подходящий алгоритм: FIFO");
            else if (pageFaultsLRU < pageFaultsFIFO)
                Console.WriteLine("Наиболее подходящий алгоритм: LRU");
            else if (pageFaultsFIFO == pageFaultsLRU)
                Console.WriteLine("Алгоритмы справились одинаково.");
        }

        static void Main()
        {
            var pagesAndRequests = GetPagesAndRequests.ReadFromFile();
            var pageFaultsFIFO = FIFO(pagesAndRequests, 4);
            Console.WriteLine();
            var pageFaultsLRU = LRU(pagesAndRequests, 4);

            OutputBestAlgorithm(pageFaultsFIFO, pageFaultsLRU);
            Console.WriteLine();

            pageFaultsFIFO = FIFO(pagesAndRequests, 5);
            Console.WriteLine();
            pageFaultsLRU = LRU(pagesAndRequests, 5);

            OutputBestAlgorithm(pageFaultsFIFO, pageFaultsLRU);

            Console.Read();
        }
    }
}