using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{

    
    public class ListComparer<T> : IEqualityComparer<List<T>>
    {

        public bool Equals(List<T> x, List<T> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<T> obj)
        {
            int hashcode = 0;
            foreach (T t in obj)
            {
                hashcode ^= t.GetHashCode();
            }
            return hashcode;
        }
    }

    public class ListInCollection
    {
        static void ListInHashSet()
        {
            List<int> a = new List<int> { 1, 2, 3 };
            List<int> b = new List<int> { 1, 2, 3 };

            HashSet<List<int>> h = new HashSet<List<int>>(new ListComparer<int>());
            h.Add(a);

            bool x = h.Contains(b);

        }

        static void ListAsDictionaryKey()
        {
            List<int> a = new List<int> { 1, 2, 3 };
            List<int> b = new List<int> { 1, 2, 3 };



            Dictionary<List<int>, int> d = new Dictionary<List<int>, int>(new ListComparer<int>());
            d[a] = 1;

            bool x = d.ContainsKey(b);

        }


    }
}
