using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Resource
    {
        public string Name { get; set; }

        public int Val { get; set; }

        public Resource(string name, int val)
        {
            Name = name;
            Val = val;
        }

        public static Resource operator +(Resource a, Resource b) => new Resource(a.Name, a.Val + b.Val);

        public static Resource operator -(Resource a, Resource b) => new Resource(a.Name, a.Val - b.Val);

        public static Resource operator +(Resource a, int b) => new Resource(a.Name, a.Val - b);

        public static Resource operator -(Resource a, int b) => new Resource(a.Name, a.Val - b);

        public static bool operator <(Resource a, Resource b) => a.Val < b.Val;

        public static bool operator >(Resource a, Resource b) => a.Val > b.Val;

        public static bool operator <=(Resource a, Resource b) => a.Val <= b.Val;

        public static bool operator >=(Resource a, Resource b) => a.Val >= b.Val;

        public static bool operator ==(Resource left, Resource right)
        {
            return EqualityComparer<Resource>.Default.Equals(left, right);
        }

        public static bool operator !=(Resource left, Resource right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is Resource resource &&
                   Name == resource.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}