using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class Utils
    {
        public static void Print(int[] a)
        {
            Console.WriteLine();
            foreach (int val in a)
            {
                Console.Write(val+" ");
            }
            Console.WriteLine();
        }

        public static void Swap(int[] a, int x, int y)
        {
            int t=a[x];
            a[x] = a[y];
            a[y] = t;
        }
    }
}
