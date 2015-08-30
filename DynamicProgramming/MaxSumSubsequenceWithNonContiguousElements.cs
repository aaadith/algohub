using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    /*
     * given a list of numbers, find the maximum sum subsequence such that no two numbers in the subsequence are adjacent to each other in the
     * given list
     */
    public class MaxSumSubsequenceWithNonContiguousElements
    {
        int[] GetMaxSum(int[] a)
        {

            int maxsum = Int32.MinValue;
            int lastInSequence = -1;

            for (int i = 0; i < a.Length; i++)
            {
                Tuple<int, int> t = GetMaxSum(a, i);
                if (maxsum > t.Item1)
                {
                    maxsum = t.Item1;
                    lastInSequence = t.Item2;
                }
            }

            List<int> s = new List<int>();
            s.Add(a[lastInSequence]);
            while (lookup[lastInSequence].Item2 != lastInSequence)
            {
                lastInSequence = lookup[lastInSequence].Item2;
                s.Add(a[lastInSequence]);
            }

            s.Reverse();
            int[] maxSumSubsequence = s.ToArray<int>();
            return maxSumSubsequence;
        }

        //index => max sum, index of predecessor
        Dictionary<int, Tuple<int, int>> lookup = new Dictionary<int, Tuple<int, int>>();

        Tuple<int, int> GetMaxSum(int[] a, int i)
        {
            if (lookup.ContainsKey(i))
                return lookup[i];

            int maxsum = a[i];
            int predecessor = i;

            //j< i -1 ensures we skip the immediately preceding element
            for (int j = 0; j < i - 1; j++)
            {
                Tuple<int, int> t = GetMaxSum(a, j);
                int currentsum = t.Item1 + a[i];

                if (currentsum > maxsum)
                {
                    maxsum = currentsum;
                    predecessor = j;
                }
            }

            Tuple<int, int> result = new Tuple<int, int>(maxsum, predecessor);
            lookup[i] = result;
            return result;
        }
    }
   
}
