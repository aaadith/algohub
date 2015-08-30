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

        public static void Swap(int[] a, int x, int y)
        {
            int t=a[x];
            a[x] = a[y];
            a[y] = t;
        }
    }
}
