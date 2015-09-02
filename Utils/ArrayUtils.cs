using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class ArrayUtils
    {
        public static string ToString<T>(T[] a)
        {
            StringBuilder s = new StringBuilder();
            s.Append("{ ");
            s.Append(string.Join(" , ", a));
            s.Append(" }");

            string str = s.ToString();
            return str;
        
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

        public static int GetMedian(int[] a) 
        {
            int median;
            if(a.Length%2==1)
            {
                int medianPosition = a.Length / 2;
                median = a[medianPosition];
            }
            else
            {
                int median1 = a[a.Length / 2];
                int median2 = a[a.Length / 2 - 1];
                median = (median1 + median2) / 2;
            }
            return median;
        }

         
    }
}
