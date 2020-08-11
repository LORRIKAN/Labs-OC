using System;
using System.Collections.Generic;
using System.Linq;
using Resources = System.Collections.Generic.Dictionary<string, int>;

namespace Lab4
{
    class Program
    {
        static void OutputAll(Dictionary<string, Resources> processesAndGivenResources,
            Dictionary<string, Resources> processesAndResourcesRequired,
            Resources remainResources)
        {
            var maxStrLength = processesAndGivenResources.Keys.Select(s => s.Length).Max();
            string indent = " ";
            for (int i = 0; i < maxStrLength; i++)
                indent += " ";

            string resGivenStr = "Предоставлено ресурсов:";
            string maxResRequiredStr = "Максимальная потребность:";
            Console.WriteLine(indent + resGivenStr + " " + maxResRequiredStr);

            string indent2 = "";
            for (int i = 0; i < resGivenStr.Length / 2; i++)
                indent2 += " ";

            string indent3 = "";
            for (int i = 0; i < maxResRequiredStr.Length / 2; i++)
                indent3 += " ";

            foreach (var process in processesAndGivenResources)
            {
                Console.Write(process.Key + indent2);
                foreach (var resource in processesAndGivenResources[process.Key])
                    Console.Write(resource.Value + " ");

                Console.Write(indent3);

                foreach (var resource in processesAndResourcesRequired[process.Key])
                    Console.Write(resource.Value + " ");

                Console.WriteLine();
            }

            Console.Write("Осталось ресурсов:");
            OutputRes(remainResources);
            Console.WriteLine();
        }

        static void OutputRes(Resources resources)
        {
            foreach (var resource in resources)
                Console.Write(resource.Value + " ");
        }

        static bool DoAlgorithm(Dictionary<string, Resources> processesAndGivenResources, 
            Dictionary<string, Resources> processesAndResourcesRequired,
            Resources remainResources)
        {
            bool safe = true;

            while (safe && processesAndGivenResources.Any() && processesAndResourcesRequired.Any())
            {
                safe = false;
                foreach (var process in processesAndResourcesRequired)
                {
                    var affortableRes = new List<bool>();

                    var requiredResources = process.Value;
                    foreach (var resource in requiredResources)
                    {
                        var resName = resource.Key;
                        var requiredResVal = resource.Value;

                        var givenResources = processesAndGivenResources[process.Key];
                        var givenResVal = givenResources[resName];

                        var remainResVal = remainResources[resName];

                        var distributableResVal = givenResVal + remainResVal;

                        if (distributableResVal >= requiredResVal)
                            affortableRes.Add(true);
                        else
                            affortableRes.Add(false);
                    }

                    if (!affortableRes.Contains(false))
                    {
                        safe = true;
                        Console.Write($"Процесс {process.Key} завершился и освободил: ");
                        var givenRes = processesAndGivenResources[process.Key];
                        OutputRes(givenRes);

                        for (int i = 0; i < remainResources.Count; i++)
                        {
                            var resourceName = remainResources.ElementAt(i).Key;
                            remainResources[resourceName] += givenRes[resourceName];
                        }

                        Console.Write('\t' + "Осталось ресурсов: ");
                        OutputRes(remainResources);
                        Console.WriteLine();

                        processesAndGivenResources.Remove(process.Key);
                        processesAndResourcesRequired.Remove(process.Key);
                        break;
                    }
                }
            }

            return safe;
        }

        static void Main()
        {
            var allResourcesAvailable = new Resources
            {
                { "R1", 20 },
                { "R2", 20 },
                { "R3", 20 },
            };

            var processesAndGivenResources = new Dictionary<string, Resources>
            {
                { "A", new Resources() { { "R1", 4 }, { "R2", 3 }, { "R3", 2 } } },
                { "B", new Resources() { { "R1", 5 }, { "R2", 4 }, { "R3", 3 } } },
                { "C", new Resources() { { "R1", 4 }, { "R2", 5 }, { "R3", 4 } } },
                { "D", new Resources() { { "R1", 3 }, { "R2", 4 }, { "R3", 5 } } },
                { "E", new Resources() { { "R1", 2 }, { "R2", 3 }, { "R3", 4 } } },

            };

            var processesAndResourcesRequired = new Dictionary<string, Resources>
            {
                { "A", new Resources() { { "R1", 8 }, { "R2", 8 }, { "R3", 8 } } },
                { "B", new Resources() { { "R1", 7 }, { "R2", 7 }, { "R3", 7 } } },
                { "C", new Resources() { { "R1", 6 }, { "R2", 6 }, { "R3", 6 } } },
                { "D", new Resources() { { "R1", 7 }, { "R2", 7 }, { "R3", 7 } } },
                { "E", new Resources() { { "R1", 8 }, { "R2", 8 }, { "R3", 8 } } },

            };

            var remainRes = new Resources();
            foreach (var availableRes in allResourcesAvailable)
            {
                var resName = availableRes.Key;
                var resVal = availableRes.Value;
                foreach (var process in processesAndGivenResources)
                {
                    resVal -= process.Value[resName];
                }
                remainRes.Add(resName, resVal);
            }

            OutputAll(processesAndGivenResources, processesAndResourcesRequired, remainRes);
            bool safe = DoAlgorithm(processesAndGivenResources, processesAndResourcesRequired, remainRes);

            if (safe)
                Console.WriteLine("Состояние безопасное.");
            else
                Console.WriteLine("Состояние небезопасное.");
        }
    }
}