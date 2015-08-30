using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinatorics
{
    public class PowerSetGenerator
    {
        public IEnumerable<int[]> GetSubsets(int[] givenSet)
        {
            foreach(int[] item in GetSubSet(givenSet,0,new List<int>()))
            {
                yield return item;
            }
            
        }

        IEnumerable<int[]> GetSubSet(int[] a, int i, List<int> soFar)
        {
            if (i == a.Length)
                yield return soFar.ToArray<int>();
            else
            {
                List<int> x = new List<int>(soFar);
                List<int> y = new List<int>(soFar);

                IEnumerable<int[]> m = GetSubSet(a, i + 1, x);

                y.Add(a[i]);
                IEnumerable<int[]> n = GetSubSet(a, i + 1, y);

                IEnumerable<int[]> all = m.Concat<int[]>(n);
                foreach (int[] subset in all)
                {
                    yield return subset;
                }
            }
        }
    }
}
