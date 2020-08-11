using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static string NL = Environment.NewLine;
        static Tuple <List<Resource>, List<Process>> GetResourcesAndProcesses()
        {
            var resources = new List<Resource>();
            var processes = new List<Process>();
            using (var reader = new StreamReader("Example.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string lineOfResources = reader.ReadLine();

                    lineOfResources.Split(',').ToList().ForEach(res => resources.Add(new Resource(res)));

                    reader.ReadLine();

                    while (true)
                    {
                        string processVector = reader.ReadLine();
                        if (string.IsNullOrEmpty(processVector))
                            break;
                        processes.Add(new Process(processVector));
                    }

                    foreach (var process in processes)
                        process.MaxResVector = reader.ReadLine();
                }
            }
            return new Tuple<List<Resource>, List<Process>>(resources, processes);
        }

        static void OutputProcessesAndGivenAndNeededResources(List<Process> processes, List<Resource> vectorA)
        {
            Console.WriteLine("Предоставлено ресурсов:");
            Console.Write(" ");
            foreach (var resource in vectorA)
                Console.Write(resource.Name);
            Console.WriteLine();

            foreach (var process in processes) 
            {
                Console.Write(process.Name + " ");
                foreach (var givenResource in process.GivenRes)
                    Console.Write(givenResource + " ");
                Console.WriteLine();
            }

            Console.WriteLine("Ресурсов необходимо:");
            Console.Write(" ");
            foreach (var resource in vectorA)
                Console.Write(resource.Name);
            Console.WriteLine();

            foreach (var process in processes)
            {
                Console.Write(process.Name + " ");
                foreach (var neededResource in process.NeededRes)
                    Console.Write(neededResource + " ");
                Console.WriteLine();
            }

            Console.Write("Вектор A={");
            for (int i = 0; i < vectorA.Count; ++i)
            {
                Console.Write(vectorA[i].ResourceVol);
                if (i != vectorA.Count - 1)
                    Console.Write(", ");
            }
            Console.WriteLine("}" + NL);
        }

        static Tuple<bool, string> DoTask(List<Process> processes, List<Resource> vectorA)
        {
            bool unsafeCondition = false;
            string processOrder = "";
            while (processes.Count != 0 && !unsafeCondition)
            {
                int processesNum = processes.Count;
                OutputProcessesAndGivenAndNeededResources(processes, vectorA);

                foreach (var process in processes)
                {
                    bool toProvideByResource = true;
                    for (int i = 0; i < process.NeededRes.Count; ++i)
                        if (vectorA[i].ResourceVol < process.NeededRes[i])
                            toProvideByResource = false;

                    if (toProvideByResource)
                    {
                        for (int i = 0; i < process.GivenRes.Count; ++i)
                            vectorA[i].ResourceVol += process.GivenRes[i];
                        processOrder += process.Name + " ";
                        processes.Remove(process);
                        break;
                    }
                }

                if (processes.Count == processesNum)
                    unsafeCondition = false;
            }
            return new Tuple<bool, string>(unsafeCondition, processOrder);
        }

        static void Main()
        {
            var resourcesAndProcesses = GetResourcesAndProcesses();
            var resources = resourcesAndProcesses.Item1;
            var processes = resourcesAndProcesses.Item2;

            Console.Write("Имеется ресурсов: ");
            foreach (var resource in resources)
                Console.Write(resource.Name + "=" + resource.ResourceVol + " ");
            Console.WriteLine(NL);

            var vectorA = resources;

            foreach (var process in processes)
                for (int i = 0; i < process.GivenRes.Count; ++i)
                    vectorA[i].ResourceVol = vectorA[i].ResourceVol - process.GivenRes[i];

            var result = DoTask(processes, vectorA);
            var unsafeCondition = result.Item1;
            var processesOrder = result.Item2;

            if (!unsafeCondition)
                Console.WriteLine("Состояние безопасное.");
            else
                Console.WriteLine("Состояние опасное.");
            Console.WriteLine("Последовательность процессов, которым были выделены ресурсы: " + processesOrder);
        }
    }
}