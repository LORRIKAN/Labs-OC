using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static readonly string NL = Environment.NewLine;

        static void OutputResourcesNames(List<Resource> resources)
        {
            Console.Write("  ");
            foreach (var res in resources)
                Console.Write(res.Name);
            Console.WriteLine();

        }

        static void OutputProcessesWithGivenRes(List<Process> processes)
        {
            foreach (var process in processes)
            {
                Console.Write(process.Name + " ");

                foreach (var resource in process.GivenResources)
                    Console.Write(resource.Val + " ");

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void OutputProcessesNeededRes(List<Process> processes)
        {
            foreach (var process in processes)
            {
                Console.Write(process.Name + " ");

                foreach (var resource in process.NeededResources)
                    Console.Write(resource.Val + " ");

                Console.WriteLine();
            }
        }

        static void OutputResourcesVector(List<Resource> resources)
        {
            Console.Write("{ ");
            for (int i = 0; i < resources.Count; ++i)
            {
                Console.Write(resources[i].Val);
                if (i != resources.Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine(" }");
        }

        static bool BankerAlgorithm(List<Process> processes, List<Resource> availableResources)
        {
            Console.WriteLine("Алгоритм банкира:" + NL);

            bool safeCondition = true;

            while (processes.Any() && safeCondition)
            {
                Console.WriteLine("Предоставлено ресурсов:");
                OutputResourcesNames(availableResources);
                OutputProcessesWithGivenRes(processes);

                Console.WriteLine("Максимальная потребность:");
                OutputResourcesNames(availableResources);
                OutputProcessesNeededRes(processes);

                Console.Write("Ресурсов осталось:");
                OutputResourcesVector(availableResources);

                int processesNum = processes.Count;

                foreach (var process in processes)
                {
                    bool toProvideByResource = true;
                    foreach (var neededResource in process.NeededResources)
                    {
                        var givenRes = process.GivenResources.Find(r => r == neededResource);
                        var availableRes = availableResources.Find(r => r == neededResource);

                        if ((availableRes + givenRes) < neededResource)
                        {
                            toProvideByResource = false;
                            break;
                        }
                    }

                    if (toProvideByResource)
                    {
                        for (int i = 0; i < availableResources.Count; ++i)
                        {
                            var availableResource = availableResources[i];
                            var givenRes = process.GivenResources.Find(r => r == availableResource);
                            availableResource.Val += givenRes.Val;
                        }

                        Console.WriteLine("Процесс " + process.Name + " был завершён и отдал:");
                        OutputResourcesVector(process.GivenResources);
                        Console.WriteLine();
                        processes.Remove(process);
                        break;
                    }
                }

                if (processes.Count == processesNum)
                    safeCondition = false;
            }

            return safeCondition;
        }


        static void Main()
        {
            var R1 = new Resource("R1", 4);
            var R2 = new Resource("R2", 4);
            var R3 = new Resource("R3", 4);
            var R4 = new Resource("R4", 4);
            var resources = new List<Resource> { R1, R2, R3, R4 };

            var A = new Process("A", new List<Resource> { R1 - 2, R2 - 4, R3 - 4, R4 - 4 }, new List<Resource> { R1 - 2, R2 - 4, R3 - 2, R4 - 2 });
            var B = new Process("B", new List<Resource> { R1 - 2, R2 - 2, R3 - 4, R4 - 4 }, new List<Resource> { R1 - 2, R2 - 2, R3 - 2, R4 - 2 });
            var C = new Process("C", new List<Resource> { R1 - 4, R2 - 2, R3 - 2, R4 - 4 }, new List<Resource> { R1 - 2, R2 - 0, R3 - 2, R4 - 0 });
            var D = new Process("D", new List<Resource> { R1 - 4, R2 - 4, R3 - 2, R4 - 2 }, new List<Resource> { R1 - 4, R2 - 4, R3 - 2, R4 - 0 });

            var processes = new List<Process> { A, B, C, D };

            var availableResources = resources;
            foreach (var process in processes)
                foreach (var givenRes in process.GivenResources)
                {
                    var res = availableResources.Find(r => r == givenRes);
                    res.Val -= givenRes.Val;
                }

            bool safeCondition = BankerAlgorithm(processes, availableResources);

            if (safeCondition)
                Console.WriteLine("Состояние безопасно.");
            else
                Console.WriteLine("Состояние небезопасно.");
        }
    }
}