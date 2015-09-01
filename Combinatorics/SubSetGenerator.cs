using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combinatorics
{
    public class SubSetGenerator
    {
        public static IEnumerable<int[]> GetSubsets(int[] givenSet)
        {
            foreach(int[] item in GetSubSet(givenSet,0,new List<int>()))
            {
                yield return item;
            }
            
        }

        static IEnumerable<int[]> GetSubSet(int[] a, int i, List<int> soFar)
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

        public static IEnumerable<int[]> GetKSubsets(int[] givenSet, int k)
        {
            foreach (int[] item in GetKSubSet(givenSet, 0, k, new List<int>()))
            {
                yield return item;
            }
        }

        static IEnumerable<int[]> GetKSubSet(int[] a, int i, int k, List<int> soFar)
        {
            if (soFar.Count == k)
                yield return soFar.ToArray<int>();
            else if(i<a.Length)
            {
                List<int> x = new List<int>(soFar);
                List<int> y = new List<int>(soFar);

                IEnumerable<int[]> m = GetKSubSet(a, i + 1, k, x);

                y.Add(a[i]);
                IEnumerable<int[]> n = GetKSubSet(a, i + 1, k, y);

                IEnumerable<int[]> all = m.Concat<int[]>(n);
                foreach (int[] ksubset in all)
                {
                    yield return ksubset;
                }
            }
        }

    }
}
