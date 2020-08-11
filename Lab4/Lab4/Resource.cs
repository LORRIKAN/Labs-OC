using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Resource
    {
        public string Name { get; private set; }

        public Resource(string resource)
        {
            Name = string.Join("", resource.TakeWhile(a => a != '='));
            ResourceVol = int.Parse(resource.Substring(Name.Length + 1)); // учитывая знак равно (+1)
        }

        public int ResourceVol { get; set; }
    }
}