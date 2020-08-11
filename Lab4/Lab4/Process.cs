using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Process
    {
        public string Name { get; private set; }

        public Process(string processVector)
        {
            Name = string.Join("", processVector.TakeWhile(a => a != ' '));
            processVector = processVector.Remove(0, Name.Length + 1);
            processVector.Split(' ').ToList().ForEach(a => GivenRes.Add(int.Parse(a)));
        }

        public List<int> GivenRes { get; private set; } = new List<int>();

        public List<int> NeededRes { get; private set; } = new List<int>();

        public string MaxResVector 
        { 
            set
            {
                var maxValues = value.Split(' ').Select(a => int.Parse(a)).ToList();
                for (int i = 0; i < maxValues.Count; ++i)
                    NeededRes.Add(maxValues[i] - GivenRes[i]);
            } 
        }
    }
}