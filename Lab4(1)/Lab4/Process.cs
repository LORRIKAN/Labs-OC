using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Process
    {
        public string Name { get; private set; }

        public List<Resource> GivenResources { get; private set; }

        public List<Resource> NeededResources { get; private set; }

        public Process(string name, List<Resource> givenResources, List<Resource> neededResources)
        {
            Name = name;
            GivenResources = givenResources;
            NeededResources = neededResources;
        }
    }
}