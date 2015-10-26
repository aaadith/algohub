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

        public static string ToString<T>(T[,] a)
        {
            int rows = a.GetLength(0), cols = a.GetLength(1);

            StringBuilder s = new StringBuilder();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    s.Append(a[i, j]);
                    s.Append('\t');
                }
                s.Append('\n');
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

        public static T GetMedian<T>(T[] a, bool sorted=false) 
        {
            if(!sorted)
                Array.Sort(a);

            int medianPosition = a.Length / 2;
            T median = a[medianPosition];            
            return median;
        }

         
    }
}
