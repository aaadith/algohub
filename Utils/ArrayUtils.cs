using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class ArrayUtils
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

        public static string ToString1(this int[] a)
        {
            StringBuilder s = new StringBuilder();

            foreach (int val in a)
            {
                s.Append(val + " ");
            }

            return s.ToString();
        }

        /*
         * Swaps elements at positions x and y of the given array a of type T
         */
        public static void Swap<T>(T[] a, int x, int y)
        {
            if (x == y)
                return;

            T tmp=a[x];
            a[x] = a[y];
            a[y] = tmp;
        }
    }
}
