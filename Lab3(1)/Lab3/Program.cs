using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static readonly string NL = Environment.NewLine;

        static readonly int additionalBlock = -1;
        static string MakeBlocksString(IEnumerable<int> blocks)
        {
            string blocksString = "";
            foreach (var block in blocks)
                if (block != additionalBlock)
                    blocksString += block + " ";
            return blocksString;
        }

        static IEnumerable<int> AddBlock(IEnumerable<int> blocks, int blockToAdd)
        {

            if (!blocks.Contains(additionalBlock))
            {
                var queuedBlocks = new Queue<int>(blocks);
                queuedBlocks.Dequeue();
                queuedBlocks.Enqueue(blockToAdd);
                blocks = queuedBlocks;
            }
            else
            {
                var listedBlocks = blocks.ToList();
                int indexToInsert = listedBlocks.IndexOf(additionalBlock);
                listedBlocks.Remove(additionalBlock);
                listedBlocks.Insert(indexToInsert, blockToAdd);
                blocks = listedBlocks;
            }

            return blocks;
        }

        static int FIFO(IEnumerable<int> blocks, IEnumerable<int> blocksToAdd)
        {
            Console.WriteLine("Алгоритм FIFO для " + blocks.Count() + " страничных блоков:");

            Console.WriteLine(MakeBlocksString(blocks) + '\t' + MakeBlocksString(blocksToAdd));

            int pageFaults = 0;

            var blocksToAddToOutput = blocksToAdd.ToList();

            foreach (var blockToAdd in blocksToAdd)
            {
                string blocksToOutputInConsole = "";

                if (!blocks.Contains(blockToAdd))
                {
                    blocks = AddBlock(blocks, blockToAdd);
                    blocksToOutputInConsole += " <-p";
                    ++pageFaults;
                }

                blocksToAddToOutput.Remove(blockToAdd);

                blocksToOutputInConsole = blocksToOutputInConsole.Insert(0, MakeBlocksString(blocks));

                Console.WriteLine(blocksToOutputInConsole + '\t' + MakeBlocksString(blocksToAddToOutput));
            }
            return pageFaults;
        }

        static int LRU(IEnumerable<int> blocks, IEnumerable<int> blocksToAdd)
        {
            Console.WriteLine("Алгоритм LRU для " + blocks.Count() + " страничных блоков:");

            Console.WriteLine(MakeBlocksString(blocks) + '\t' + MakeBlocksString(blocksToAdd));

            int pageFaults = 0;

            var blocksToAddToOutput = blocksToAdd.ToList();

            foreach (var blockToAdd in blocksToAdd)
            {
                string blocksToOutputInConsole = "";
                if (!blocks.Contains(blockToAdd))
                {
                    blocks = AddBlock(blocks, blockToAdd);
                    blocksToOutputInConsole += " <-p";
                    ++pageFaults;
                }
                else
                {
                    var existedBlock = blocks.ToList().Find(b => b == blockToAdd);
                    var defaultValsNum = 0;
                    if (blocks.Contains(additionalBlock))
                        defaultValsNum += blocks.Where(b => b == additionalBlock).Count();

                    var blocksAsList = blocks.ToList();

                    blocksAsList.Remove(existedBlock);
                    blocksAsList.Insert(blocks.Count() - (defaultValsNum + 1), existedBlock);

                    blocks = blocksAsList;
                }

                blocksToAddToOutput.Remove(blockToAdd);

                blocksToOutputInConsole = blocksToOutputInConsole.Insert(0, MakeBlocksString(blocks));

                Console.WriteLine(blocksToOutputInConsole + '\t' + MakeBlocksString(blocksToAddToOutput));
            }
            return pageFaults;
        }

        static string CompareAlgorithmsEfficiency(int FIFOPageFaults, int LRUPageFaults)
        {
            if (FIFOPageFaults < LRUPageFaults)
                return "FIFO оказался более эффективным алгоритмом.";
            else if (LRUPageFaults < FIFOPageFaults)
                return "LRU оказался более эффективным алгоритмом.";

            return "Алгоритмы одинакого эффективны.";
        }

        static void Main()
        {
            var fourBlocks = new List<int> { 8, 2, 9, 6 };
            var fiveBlocks = new List<int> { 8, 2, 9, 6, additionalBlock };
            var blocksToAdd = new List<int> { 7, 8, 9, 2, 1, 0, 8, 9, 2, 4, 6, 8, 2, 1, 8, 9 };

            var FIFOPageFaults = FIFO(fourBlocks, blocksToAdd);
            Console.WriteLine("Страничных прерываний: " + FIFOPageFaults + NL);
            var LRUPageFaults = LRU(fourBlocks, blocksToAdd);
            Console.WriteLine("Страничных прерываний: " + LRUPageFaults + NL);
            Console.WriteLine(CompareAlgorithmsEfficiency(FIFOPageFaults, LRUPageFaults) + NL);

            FIFOPageFaults = FIFO(fiveBlocks, blocksToAdd);
            Console.WriteLine("Страничных прерываний: " + FIFOPageFaults + NL);
            LRUPageFaults = LRU(fiveBlocks, blocksToAdd);
            Console.WriteLine("Страничных прерываний: " + LRUPageFaults + NL);
            Console.WriteLine(CompareAlgorithmsEfficiency(FIFOPageFaults, LRUPageFaults) + NL);
        }
    }
}