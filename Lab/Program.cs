using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Utils;
using Combinatorics;

namespace Utils
{
    
    public class Program
    {

        static IEnumerable<T[]> GetPermutations<T>(T[] a, int i)
        {
            if (i == a.Length)
                yield return a;

            for (int j = i; j < a.Length; j++)
            {
                ArrayUtils.Swap<T>(a, i, j);
                foreach (T[] permutation in GetPermutations<T>(a, i + 1))
                    yield return permutation;
                ArrayUtils.Swap<T>(a, i, j);
            }
        }

        static void Main(string[] args)
        {
            int[] a = new int[] { 1, 2, 3 };

            PermutationsGenerator p = new PermutationsGenerator();
            foreach (int[] permutation in PermutationsGenerator.GetPermutations<int>(a))
                ArrayUtils.Print(permutation);
        }
    }
}
