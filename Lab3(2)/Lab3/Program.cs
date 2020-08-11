using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите путь к файлу: ");

            string path = Console.ReadLine();

            var blocks = GetBlocksFromFile(path);
            var requests = GetRequestsFromFile(path);

            Console.WriteLine("Алгоритм FIFO для 4 страничных блоков:");
            FIFO(blocks, requests);

            blocks = GetBlocksFromFile(path);

            Console.WriteLine("Алгоритм LRU для 4 страничных блоков:");
            LRU(blocks, requests);

            blocks = GetBlocksFromFile(path);
            blocks.Add(null);

            Console.WriteLine("Алгоритм FIFO для 5 страничных блоков:");
            FIFO(blocks, requests);

            blocks = GetBlocksFromFile(path);
            blocks.Add(null);

            Console.WriteLine("Алгоритм LRU для 5 страничных блоков:");
            LRU(blocks, requests);
        }

        static List<int?> GetBlocksFromFile(string path)
        {
            string array = File.ReadLines(path).First();

            string[] block = array.Split(' ');
            List<int?> blocks = new List<int?>();

            Console.Write("Блоки: ");

            for (int i = 0; i < block.Length; i++)
            {
                blocks.Add(Convert.ToInt32(block[i]));
            }
            for (int i = 0; i < block.Length; i++)
            {
                Console.Write(blocks[i] + " ");
            }
            Console.WriteLine();
            return blocks;
        }
        static List<int> GetRequestsFromFile(string path)
        {
            string array = File.ReadLines(path).Skip(1).First();

            string[] tabl = array.Split(' ');
            List<int> requests = new List<int>();
            Console.Write("Запросы: ");
            for (int i = 0; i < tabl.Length; i++)
            {
                requests.Add(Convert.ToInt32(tabl[i]));
            }
            for (int i = 0; i < tabl.Length; i++)
            {
                Console.Write(requests[i] + " ");
            }
            Console.WriteLine();
            return requests;
        }



        static void FIFO(List<int?> blocks, List<int> requests)
        {
            int pageFaults = 0;

            for (int i = 0; i < requests.Count; i++)
            {
                bool flag = true;


                for (int j = 0; j < blocks.Count; j++)
                {

                    if (blocks[j] == requests[i])
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag == false)
                {
                    pageFaults++;
                    if (blocks.Contains(null))
                    {
                        var indexOfEmptyElem = blocks.IndexOf(null);
                        blocks[indexOfEmptyElem] = requests[i];
                    }
                    else
                    {
                        int k = 0;
                        while (k < blocks.Count - 1)
                        {
                            blocks[k] = blocks[k + 1];
                            k++;
                        }
                        blocks[blocks.Count - 1] = requests[i];
                    }
                }

                for (int k = 0; k < blocks.Count; k++)
                {
                    Console.Write(blocks[k] + " ");
                }

                if (flag == false)
                    Console.Write(" <-p");

                Console.WriteLine();
            }
            Console.WriteLine($"{pageFaults} страничных прерываний");
        }

        static void LRU(List<int?> blocks, List<int> requests)
        {
            int pageFaults = 0;

            for (int i = 0; i < requests.Count; i++)
            {
                bool flag = true;

                for (int j = 0; j < blocks.Count; j++)
                {

                    if (blocks[j] == requests[i])
                    {
                        blocks.RemoveAt(j);
                        blocks.Add(requests[i]);
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag == false)
                {
                    pageFaults++;
                    if (blocks.Contains(null))
                    {
                        var indexOfEmptyElem = blocks.IndexOf(null);
                        blocks[indexOfEmptyElem] = requests[i];
                    }
                    else
                    {
                        int k = 0;
                        while (k < blocks.Count - 1)
                        {
                            blocks[k] = blocks[k + 1];
                            k++;
                        }
                        blocks[blocks.Count - 1] = requests[i];
                    }
                }

                for (int k = 0; k < blocks.Count; k++)
                {
                    Console.Write(blocks[k] + " ");
                }

                if (flag == false)
                    Console.Write(" <-p");

                Console.WriteLine();
            }
            Console.WriteLine($"{pageFaults} страничных прерываний");
        }
    }
}